//-----------------------------------------------------------------------
// <copyright file="MainPage.xaml.cs" company="eggfly">
//     Copyright (c) eggfly. All rights reserved.
// </copyright>
// <author>eggfly</author>
//-----------------------------------------------------------------------

namespace RemoteCapture.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Shapes;
    using Microsoft.Devices;
    using Microsoft.Phone.Controls;
    using Microsoft.Phone.Shell;
    using Microsoft.Xna.Framework.GamerServices;
    using RemoteCapture.Common;

    /// <summary>
    /// Main Page
    /// </summary>
    public partial class MainPage : PhoneApplicationPage
    {
        /// <summary>
        /// Different Precent
        /// </summary>
        private const double CompareDifferentPrecent = 0.85;

        /// <summary>
        /// Photo Camera
        /// </summary>
        private PhotoCamera camera = new PhotoCamera();

        /// <summary>
        /// Capture Source
        /// </summary>
        private CaptureSource captureSource = new CaptureSource();

        /// <summary>
        /// two buffer
        /// </summary>
        private byte[] buffer1, buffer2;

        /// <summary>
        /// current buffer
        /// </summary>
        private byte[] currentBuffer;
        
        /// <summary>
        /// Pump Preview Frames
        /// </summary>
        private bool pumpPreviewFrames = false;

        /// <summary>
        /// pump thread
        /// </summary>
        private Thread pumpThread;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPage"/> class.
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Called when a page becomes the active page in a frame.
        /// </summary>
        /// <param name="e">An object that contains the event data.</param>
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            this.camera = new PhotoCamera();
            this.VideoBrush.SetSource(this.camera);
            this.camera.Initialized += this.OnCameraInitialized;
            this.camera.CaptureImageAvailable += this.OnCameraCaptureImageAvailable;
            this.camera.CaptureCompleted += this.OnCameraCaptureCompleted;
            ////var videoCaptureDevice = CaptureDeviceConfiguration.GetDefaultVideoCaptureDevice();
            ////if (videoCaptureDevice != null)
            ////{
            ////    this.captureSource.VideoCaptureDevice = videoCaptureDevice;
            ////}

            base.OnNavigatedTo(e);
        }

        /// <summary>
        /// Called when [camera capture image available].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Microsoft.Devices.ContentReadyEventArgs"/> instance containing the event data.</param>
        private void OnCameraCaptureImageAvailable(object sender, ContentReadyEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("CaptureImageAvailable");
        }

        /// <summary>
        /// Called when [camera capture completed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Microsoft.Devices.CameraOperationCompletedEventArgs"/> instance containing the event data.</param>
        private void OnCameraCaptureCompleted(object sender, CameraOperationCompletedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("CaptureCompleted");
        }

        /// <summary>
        /// Called when [camera initialized].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Microsoft.Devices.CameraOperationCompletedEventArgs"/> instance containing the event data.</param>
        private void OnCameraInitialized(object sender, CameraOperationCompletedEventArgs e)
        {
            this.camera.FlashMode = FlashMode.Off;
        }

        /// <summary>
        /// Pumps the preview frames.
        /// </summary>
        private void PumpPreviewFrames()
        {
            this.buffer1 = this.buffer1 ?? new byte[(int)this.camera.PreviewResolution.Width * (int)this.camera.PreviewResolution.Height];
            this.buffer2 = this.buffer2 ?? new byte[(int)this.camera.PreviewResolution.Width * (int)this.camera.PreviewResolution.Height];
            this.currentBuffer = this.buffer1;

            try
            {
                while (this.pumpPreviewFrames)
                {
                    this.camera.GetPreviewBufferY(this.currentBuffer);
                    double result = BitmapCompare.Compare(this.buffer1, this.buffer2);

                    this.Dispatcher.BeginInvoke(() =>
                    {
                        this.textBlock1.Text = result.ToString();
                        if (result < CompareDifferentPrecent)
                        {
                            this.ellipse1.Fill = new SolidColorBrush(Colors.Red);
                        }
                        else
                        {
                            this.ellipse1.Fill = new SolidColorBrush(Colors.Green);
                        }
                    });

                    this.currentBuffer = (this.currentBuffer == this.buffer1) ? this.buffer2 : this.buffer1;

                    Thread.Sleep(500);
                }
            }
            catch (Exception e)
            {
                this.Dispatcher.BeginInvoke(() =>
                {
                    this.textBlock1.Text = e.Message;
                });
            }
        }

        /// <summary>
        /// Handles the Click event of the button1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            this.Button1.IsEnabled = false;
            ////this.camera.CaptureImage();
            if (this.buffer1 == null)
            {
                this.buffer1 = new byte[(int)this.camera.PreviewResolution.Width * (int)this.camera.PreviewResolution.Height];
            }

            this.camera.GetPreviewBufferY(this.buffer1);
            this.Button1.IsEnabled = true;
        }

        /// <summary>
        /// Handles the Click event of the Button2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            this.Button2.IsEnabled = false;
            if (this.buffer2 == null)
            {
                this.buffer2 = new byte[(int)this.camera.PreviewResolution.Width * (int)this.camera.PreviewResolution.Height];
            }

            this.camera.GetPreviewBufferY(this.buffer2);
            this.Button2.IsEnabled = true;
        }

        /// <summary>
        /// Handles the Click event of the Button3 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            if (this.buffer1 != null && this.buffer2 != null)
            {
                double result = BitmapCompare.Compare(this.buffer1, this.buffer2);
                this.textBlock1.Text = result.ToString();
            }
        }

        /// <summary>
        /// Handles the Click event of the Button4 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            this.pumpPreviewFrames = true;
            this.pumpThread = new Thread(this.PumpPreviewFrames);
            this.pumpThread.Start();
            Guide.IsScreenSaverEnabled = false;
            PhoneApplicationService.Current.ApplicationIdleDetectionMode = IdleDetectionMode.Enabled;
        }

        /// <summary>
        /// Handles the Click event of the Button5 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            this.pumpPreviewFrames = false;
        }
    }
}
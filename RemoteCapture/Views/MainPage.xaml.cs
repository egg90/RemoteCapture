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
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Shapes;
    using Microsoft.Devices;
    using Microsoft.Phone.Controls;

    /// <summary>
    /// Main Page
    /// </summary>
    public partial class MainPage : PhoneApplicationPage
    {
        /// <summary>
        /// Photo Camera
        /// </summary>
        private PhotoCamera camera = new PhotoCamera();

        /// <summary>
        /// Capture Source
        /// </summary>
        private CaptureSource captureSource = new CaptureSource();

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
        /// Handles the Click event of the button1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            this.camera.CaptureImage();
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
    }
}
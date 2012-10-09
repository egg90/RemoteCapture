//-----------------------------------------------------------------------
// <copyright file="MainPageViewModel.cs" company="eggfly">
//     Copyright (c) eggfly. All rights reserved.
// </copyright>
// <author>eggfly</author>
//-----------------------------------------------------------------------

namespace RemoteCapture.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Threading;
    using System.Windows;
    using SimpleMvvmToolkit;
    using SimpleMvvmToolkit.ModelExtensions;

    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// Use the <strong>mvvmprop</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// </summary>
    public class MainPageViewModel : ViewModelBase<MainPageViewModel>
    {
        /// <summary>
        /// App title
        /// </summary>
        private string appTitle = "Simple MVVM Toolkit for WP7";

        /// <summary>
        /// Add Header property using the mvvmprop code snippet
        /// </summary>
        private string pageTitle = "hello mvvm";

        #region Initialization and Cleanup

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPageViewModel"/> class.
        /// </summary>
        public MainPageViewModel()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the app title.
        /// </summary>
        /// <value>
        /// The app title.
        /// </value>
        public string AppTitle
        {
            get
            {
                if (this.IsInDesignMode)
                {
                    return "Application Title";
                }

                return this.appTitle;
            }

            set
            {
                this.appTitle = value;
                NotifyPropertyChanged(m => m.AppTitle);
            }
        }

        /// <summary>
        /// Gets or sets the page title.
        /// </summary>
        /// <value>
        /// The page title.
        /// </value>
        public string PageTitle
        {
            get
            {
                if (this.IsInDesignMode)
                {
                    return "page title";
                }

                return this.pageTitle;
            }

            set
            {
                this.pageTitle = value;
                NotifyPropertyChanged(m => m.PageTitle);
            }
        }

        #endregion

        #region Methods

        #endregion

        #region Completion Callbacks

        #endregion

        #region Helpers

        #endregion
    }
}
//-----------------------------------------------------------------------
// <copyright file="ViewModelLocator.cs" company="eggfly">
//     Copyright (c) eggfly. All rights reserved.
// </copyright>
// <author>eggfly</author>
//-----------------------------------------------------------------------

/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:GirlsWeekly"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

namespace RemoteCapture.Locators
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using RemoteCapture.ViewModels;
    using SimpleMvvmToolkit;

    /// <summary>
    /// ViewModel Locator
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Gets the main view model.
        /// </summary>
        public MainPageViewModel MainPageViewModel
        {
            get { return new MainPageViewModel(); }
        }
    }
}
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

//導航頁面
using minecraft_command_helper.Pages;
using Microsoft.UI;
using Windows.UI.WindowManagement;

namespace minecraft_command_helper
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            this.ExtendsContentIntoTitleBar = true;
            this.SetTitleBar(AppTitleBar);
            nv.SelectedItem = nv.MenuItems.OfType<NavigationViewItem>().First();
            NavView_Navigate(typeof(HomePage));
        }

        private void NaviSelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                NavView_Navigate(typeof(SettingsPage));
            }
            else if (args.SelectedItemContainer != null)
            {
                string pageName = "minecraft_command_helper.Pages." + args.SelectedItemContainer.Tag.ToString();
                Type pageType = Type.GetType(pageName);
                NavView_Navigate(pageType);
            }
        }

        private void NavView_Navigate(Type navPageType)
        {
            Type preNavPageType = ContentFrame.CurrentSourcePageType;

            if (navPageType != null && !Equals(preNavPageType, navPageType))
            {
                ContentFrame.Navigate(navPageType);
            }
        }

        private void NavView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (ContentFrame.CanGoBack) ContentFrame.GoBack();
        }

        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            nv.IsBackEnabled = ContentFrame.CanGoBack;

            var currentPageType = ContentFrame.SourcePageType;
            var pageName = currentPageType.FullName.ToString()[31..];
            AppTitle.Text = pageName;

            if (currentPageType == typeof(SettingsPage))
            {
                nv.SelectedItem = (NavigationViewItem) nv.SettingsItem;
            }
            else if (currentPageType != null)
            {
                nv.SelectedItem = nv.MenuItems.OfType<NavigationViewItem>().First(n => n.Tag.Equals(pageName));
            }
        }
    }
}

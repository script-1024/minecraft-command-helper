using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
//using Microsoft.UI.Xaml.Controls.Primitives;
//using Microsoft.UI.Xaml.Data;
//using Microsoft.UI.Xaml.Input;
//using Microsoft.UI.Xaml.Media;
//using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Windowing;
using System;
//using System.Collections.Generic;
//using System.IO;
using System.Linq;
using WinRT.Interop;
using Windows.Graphics;
//using Windows.Foundation;
//using Windows.Foundation.Collections;
using Vanara.PInvoke;
using minecraft_command_helper.Pages;
using Windows.System;

namespace minecraft_command_helper
{
    public sealed partial class MainWindow : Window
    {
        private IntPtr hwnd;
        private AppWindow appWindow;

        public MainWindow()
        {
            this.InitializeComponent();

            //获取窗口句柄
            hwnd = WindowNative.GetWindowHandle(this);
            WindowId id = Win32Interop.GetWindowIdFromWindow(hwnd);
            appWindow = AppWindow.GetFromWindowId(id);

            // 设置标题栏，检查是否支持新方法 (Win11)
            if (AppWindowTitleBar.IsCustomizationSupported())
            {
                var titleBar = appWindow.TitleBar;
                titleBar.ExtendsContentIntoTitleBar = true;

                // 标题栏按键背景色设置为透明
                titleBar.ButtonBackgroundColor = Colors.Transparent;
                titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
                // 获取系统缩放率
                var scale = (float)User32.GetDpiForWindow(hwnd) / 96;
                titleBar.SetDragRectangles(new RectInt32[] { new RectInt32((int)(50 * scale), 0, 10000, (int)(50 * scale)) });
            }
            else
            {
                //不支持就用系统自带的方法
                //this.ExtendsContentIntoTitleBar = true;
                //this.SetTitleBar(AppTitleBar);
            }

            this.OnLoad();
        }

        private void OnLoad()
        {
            TitleBar.SetTitle("MC 指令助手");
            nv.SelectedItem = nv.MenuItems.OfType<NavigationViewItem>().First();
            NavView_Navigate(typeof(HomePage));
        }


        private void Window_SizeChanged(object sender, WindowSizeChangedEventArgs args)
        {
            var width = appWindow.Size.Width;
            var height = appWindow.Size.Height;

            if (width < 800 || height < 600) 
            {
                width = (width < 800) ? 800 : width;
                height = (height < 600) ? 600 : height;
                appWindow.Resize(new SizeInt32(width, height));
                
            }

            if (width <= 1200) 
            { 
                //SearchBox.Width = width - 600; 
                if (width < 1024) 
                {
                    //SearchBox.Margin = new Thickness(width*0.5-360, 8, 0, 8);
                }
            }
            else
            {
                //SearchBox.Margin = new Thickness(width*0.5-460, 8, 0, 8);
            }
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

        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            var currentPageType = ContentFrame.SourcePageType;
            var pageName = currentPageType.FullName.ToString()[31..];

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

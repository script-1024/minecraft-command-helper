using System;
using System.Linq;

using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Windowing;

using minecraft_command_helper.Pages;
using Script1024.Library;

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
            hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            WindowId id = Win32Interop.GetWindowIdFromWindow(hwnd);
            appWindow = AppWindow.GetFromWindowId(id);

            // 设置窗口最小尺寸
            WindowProc.SetWndMinSize(hwnd, 800, 600);

            // 设置标题栏，检查是否支持新方法 (Win11)
            if (AppWindowTitleBar.IsCustomizationSupported())
            {
                var titleBar = appWindow.TitleBar;
                titleBar.ExtendsContentIntoTitleBar = true;

                // 标题栏按键背景色设置为透明
                titleBar.ButtonBackgroundColor = Colors.Transparent;
                titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
            }
            else
            {
                //不支持就用系统自带的方法
                this.ExtendsContentIntoTitleBar = true;
                this.SetTitleBar(TitleBar);
            }
            
            this.OnLoaded();
        }

        private void OnLoaded()
        {
            TitleBar.SetTitle("MC 指令助手");
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

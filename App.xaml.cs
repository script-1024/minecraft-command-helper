using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using Microsoft.UI.Windowing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Vanara.PInvoke;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WinRT.Interop;

namespace minecraft_command_helper
{
    public partial class App : Application
    {
        private IntPtr hwnd;

        public App()
        {
            this.InitializeComponent();
        }

        public static AppWindow AppWindow { get; private set; }

        public static Window MainWindow { get; private set; }

        public static UserControl AppTitleBar { get; private set; }

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            MainWindow = new MainWindow();

            //获取当前窗口句柄
            hwnd = WindowNative.GetWindowHandle(MainWindow);
            WindowId id = Win32Interop.GetWindowIdFromWindow(hwnd);

            //获取应用窗口对象
            AppWindow = AppWindow.GetFromWindowId(id);
            AppWindow.TitleBar.ExtendsContentIntoTitleBar = true;

            MainWindow.Activate();
        }
    }
}

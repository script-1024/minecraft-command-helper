using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace minecraft_command_helper
{
    public sealed partial class AppTitleBar : UserControl
    {
        public AppTitleBar()
        {
            this.InitializeComponent();
            this.Loaded += OnLoaded;
            this.SizeChanged += OnSizeChanged;
        }

        public void SetTitle(string text="", bool visible=true)
        {
            if (text != "") Title.Text = text;
            if (visible) Title.Visibility = Visibility.Visible;
            else Title.Visibility = Visibility.Collapsed;
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
            => UpdateDragRects();

        private void OnLoaded(object sender, RoutedEventArgs e)
            => UpdateDragRects();

        private void UpdateDragRects()
        {
            var titleBar = App.AppWindow.TitleBar;

            // 当前控件的实际宽度.
            var totalSpace = ActualWidth;
            var height = ActualHeight;

            // 搜索框的左边界相对于整个控件左边界的偏移值.
            var searchLeftOffset = SearchBox.ActualOffset.X;

            // 搜索框的右边界相对于整个控件左边界的偏移值.
            var searchRightOffset = searchLeftOffset + SearchBox.ActualWidth;

            var leftSpace = searchLeftOffset;
            var rightSpace = totalSpace - searchLeftOffset - SearchBox.ActualWidth;

            var leftRect = new RectInt32(0, 0, Convert.ToInt32(leftSpace), Convert.ToInt32(height));
            var rightRect = new RectInt32(Convert.ToInt32(searchRightOffset), 0, Convert.ToInt32(rightSpace), Convert.ToInt32(height));

            titleBar.SetDragRectangles(new RectInt32[] { leftRect, rightRect });
        }
    }
}

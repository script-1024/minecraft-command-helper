using System;
using Microsoft.UI.Xaml;
using Windows.Graphics;

namespace minecraft_command_helper
{
    public sealed partial class AppTitleBar : Microsoft.UI.Xaml.Controls.UserControl
    {
        public AppTitleBar()
        {
            this.InitializeComponent();
            this.Loaded += OnLoaded;
            this.SizeChanged += OnSizeChanged;
        }

        public void SetTitle(string text, bool visible=true)
        {
            if (text != null) Title.Text = text;
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

            // 当前控件的实际宽度
            var totalSpace = ActualWidth;
            var height = Convert.ToInt32(ActualHeight);

            // 搜索框的左边界相对于整个控件左边界的偏移值
            var searchLeftOffset = SearchBox.ActualOffset.X;

            // 搜索框的右边界相对于整个控件左边界的偏移值
            var searchRightOffset = searchLeftOffset + SearchBox.ActualWidth;

            var leftSpace = searchLeftOffset;
            var rightSpace = totalSpace - searchLeftOffset - SearchBox.ActualWidth;

            var leftRect = new RectInt32(0, 0, Convert.ToInt32(leftSpace), height);
            var rightRect = new RectInt32(Convert.ToInt32(searchRightOffset), 0, Convert.ToInt32(rightSpace), height);

            titleBar.SetDragRectangles(new RectInt32[] { leftRect, rightRect });
        }
    }
}

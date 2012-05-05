// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="Ascend">
//   Copyright © 2012 All Rights Reserved
// </copyright>
// <summary>
//   Interaction logic for MainWindow.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RegionCapture
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        /// <summary> Selected rectangle. </summary>
        private Rect rectArea = Rect.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
        }

        /// <summary> Closes the application. </summary>
        /// <param name="sender"> The sender. </param>
        /// <param name="e"> The event arguments. </param>
        private void CloseClicked(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary> Changes the selected region when the mouse is clicked. </summary>
        /// <param name="sender"> The sender. </param>
        /// <param name="e"> The event arguments. </param>
        private void CanvasMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == System.Windows.Input.MouseButton.Left)
            {
                var canvas = (Canvas)sender;
                var pos = e.GetPosition(canvas);

                this.rect.Width = 0;
                this.rect.Height = 0; 
                Canvas.SetLeft(this.rect, pos.X);
                Canvas.SetTop(this.rect, pos.Y);

                this.rectArea = Rect.Empty;
            }
        }

        /// <summary> Resizes the selected region as the mouse moves. </summary>
        /// <param name="sender"> The sender. </param>
        /// <param name="e"> The event arguments. </param>
        private void CanvasMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                var canvas = (Canvas)sender;
                var pos = e.GetPosition(canvas);

                var left = Canvas.GetLeft(this.rect);
                var top = Canvas.GetTop(this.rect);

                var width = pos.X - left;
                var height = pos.Y - top;

                if (height > 0 && width > 0)
                {
                    this.rect.Width = width;
                    this.rect.Height = height;                    
                }

                this.rectArea = new Rect(left, top, width, height);
            }
        }

        /// <summary> If the region is appropriate, returns it and launches the display window. </summary>
        /// <param name="sender"> The sender. </param>
        /// <param name="e"> The event arguments. </param>
        private void AcceptClicked(object sender, RoutedEventArgs e)
        {
            if (this.rectArea == Rect.Empty)
            {
                return;
            }

            var win = new DisplayWindow(this.rectArea);
            win.Show();

            this.Close();
        }
    }
}

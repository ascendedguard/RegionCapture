// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DisplayWindow.xaml.cs" company="Ascend">
//   Copyright © 2012 All Rights Reserved
// </copyright>
// <summary>
//   Interaction logic for DisplayWindow.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RegionCapture
{
    using System;
    using System.Timers;
    using System.Windows;

    /// <summary>
    /// Interaction logic for DisplayWindow.xaml
    /// </summary>
    public partial class DisplayWindow
    {
        /// <summary> Selected region rectangle. </summary>
        private readonly Rect captureArea;

        /// <summary> Update timer for updating the screen. </summary>
        private readonly Timer updateTimer;

        /// <summary> Initializes a new instance of the <see cref="DisplayWindow"/> class. </summary>
        public DisplayWindow()
        {
            this.InitializeComponent();
        }

        /// <summary> Initializes a new instance of the <see cref="DisplayWindow"/> class. </summary>
        /// <param name="rect"> The rect. </param>
        public DisplayWindow(Rect rect) : this()
        {
            this.captureArea = rect;

            this.updateTimer = new Timer(150);
            this.updateTimer.Elapsed += this.UpdateTimerElapsed;
            this.updateTimer.Start();
        }

        /// <summary> Updates the screen when the timer elapses. </summary>
        /// <param name="sender"> The sender. </param>
        /// <param name="e"> The event arguments. </param>
        private void UpdateTimerElapsed(object sender, ElapsedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(
                (Action)delegate { this.imgOverlay.Source = CaptureScreenshot.Capture(this.captureArea); });
        }
    }
}

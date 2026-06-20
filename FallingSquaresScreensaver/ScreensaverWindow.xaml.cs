using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Threading;

using Input = System.Windows.Input;

using Wpf = System.Windows;

namespace FallingSquaresScreensaver
{
    public partial class ScreensaverWindow: Wpf.Window
    {
        List<Square> squares = new();

        DispatcherTimer timer = new();

        Random random = new();

        double startX;
        double startY;

        public bool AllowExit = true;

        bool canExit = false;

        public ScreensaverWindow()
        {
            InitializeComponent();

            Loaded += Start;

            KeyDown += Exit;

            MouseMove += ExitMouse;

            MouseDown += Exit;
        }

        async void Start(object sender, Wpf.RoutedEventArgs e)
        {
            for (int i = 0; i < 30; i++)
            {
                var sq = new Square(random, Width, Height);

                squares.Add(sq);

                Scene.Children.Add(sq.Rect);
            }

            timer.Interval = TimeSpan.FromMilliseconds(16);

            timer.Tick += Frame;

            timer.Start();

            Focus();

            // Wait for Windows to end.
            await System.Threading.Tasks.Task.Delay(500);

            startX =Input.Mouse.GetPosition(this).X;

            startY =Input.Mouse.GetPosition(this).Y;

            canExit = AllowExit;
        }

        void Frame(object sender, EventArgs e)
        {
            foreach (var sq in squares)
            {
                sq.Update(random, Width, Height);

                Canvas.SetLeft(sq.Rect, sq.X);

                Canvas.SetTop(sq.Rect, sq.Y);
            }
        }

        void Exit(object sender, EventArgs e)
        {
            if (canExit)
            {
                Wpf.Application.Current.Shutdown();
            }
        }

        void ExitMouse(object sender, Input.MouseEventArgs e )
        {
            if (!canExit)
            {
                return;
            }

            var pos = Input.Mouse.GetPosition(this);

            if (Math.Abs(pos.X - startX) > 12 || Math.Abs(pos.Y - startY) > 12)
            {
                Wpf.Application.Current.Shutdown();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace FallingSquaresScreensaver
{
    public partial class MainWindow: Window
    {
        List<Square> squares =new();

        Random random = new();

        DispatcherTimer timer = new();

        public MainWindow()
        {
            InitializeComponent();

            Loaded += Start;
        }

        void Start(object sender, RoutedEventArgs e)
        {
            Generate();

            timer.Interval =
                TimeSpan.FromMilliseconds(
                    16
                );

            timer.Tick += Frame;

            timer.Start();
        }

        void Generate()
        {
            for (
                int i = 0;
                i < 22;
                i++
            )
            {
                var sq = new Square(random, ActualWidth,ActualHeight);

                squares.Add(sq);

                Scene.Children.Add(sq.Rect);
            }
        }

        void Frame(object sender, EventArgs e)
        {
            foreach (var sq in squares)
            {
                sq.Update(
                    random,
                    ActualWidth,
                    ActualHeight
                );

                Canvas.SetLeft(
                    sq.Rect,
                    sq.X
                );

                Canvas.SetTop(
                    sq.Rect,
                    sq.Y
                );
            }
        }
    }
}
using System;
using Media = System.Windows.Media;
using Shapes = System.Windows.Shapes;

// Yes, the original plan was to make a Screensaver, since this whole thing is inspired by a similar MacOS built in Screensaver.
namespace FallingSquaresScreensaver
{
    public class Square
    {
        public Shapes.Rectangle Rect;

        public double X;
        public double Y;

        double Speed;
        double Drift;

        double Size;

        public Square(Random random, double width, double height)
        {
            Size = random.Next(50,240);

            Rect =
                new Shapes.Rectangle
                {
                    Width = Size,
                    Height = Size,

                    Fill =
                            new Media.SolidColorBrush(
                            Media.Color.FromRgb(
                                (byte)random.Next(50, 255),
                                (byte)random.Next(50, 255),
                                (byte)random.Next(50, 255)
                            )
                        ),

                    Opacity =
                        random.NextDouble()
                        * 0.25
                        + 0.2
                };

            Respawn(random, width, height, true);
        }

        public void Respawn(Random random, double width, double height, bool initial = false)
        {
            X =
                random.NextDouble()
                *
                (
                    width
                    +
                    Size
                )
                -
                Size;

            if (initial)
            {
                Y =
                    random.NextDouble()
                    *
                    (
                        height
                        +
                        Size
                    )
                    -
                    Size;
            }
            else
            {
                Y =
                    -Size;
            }

            // More visible
            Speed =
                0.25
                +
                (
                    Size
                    /
                    240
                )
                *
                0.6;

            // Minimum Drift
            Drift =
                (
                    random.NextDouble()
                    -
                    0.5
                )
                *
                0.08;
        }

        public void Update(
            Random random,
            double width,
            double height
        )
        {
            // ALWAYS falls, how cool is that?!?!?
            Y += Speed;

            X += Drift;

            if (X < -Size)
             {
                X = width;
            }

            if (X > width)
            {
                X = -Size;
            }

            if (Y > height + Size)
            {
                Respawn(
                    random,
                    width,
                    height
                );
            }
        }
    }
}
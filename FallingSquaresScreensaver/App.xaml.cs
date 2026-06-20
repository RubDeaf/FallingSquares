using Wpf = System.Windows;
using Forms = System.Windows.Forms;

namespace FallingSquaresScreensaver
{
    public partial class App: Wpf.Application
    {
        // Yes, this wallpaper can become an actual screensaver.
        const bool WallpaperMode = true;

        // If WallpaperMode is true, it acts as a wallpaper.
        // However, if false, it appears on all device screens until a key is pressed or mouse movement is detected.

        protected override void OnStartup(Wpf.StartupEventArgs e)
        {
            base.OnStartup(e);

            ShutdownMode = Wpf.ShutdownMode.OnExplicitShutdown;

            if (WallpaperMode)
            {
                StartWallpaper();
            }
            else
            {
                StartScreensaver();
            }
        }

        void StartWallpaper()
        {
            var cursor =Forms.Cursor.Position;

            var screen =Forms.Screen.FromPoint(cursor);

            var window = new ScreensaverWindow();

            window.AllowExit = false;
            window.Left = screen.Bounds.Left;
            window.Top = screen.Bounds.Top;
            window.Width = screen.Bounds.Width;
            window.Height = screen.Bounds.Height;

            window.Show();
        }

        void StartScreensaver()
        {
            foreach (var screen in Forms.Screen.AllScreens)
            {
                var window = new ScreensaverWindow();

                window.AllowExit = true;
                window.Left = screen.Bounds.Left;
                window.Top = screen.Bounds.Top;
                window.Width = screen.Bounds.Width;
                window.Height = screen.Bounds.Height;
                window.Show();
            }
        }
    }
}
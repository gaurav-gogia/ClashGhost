using ClashGhost.Pages;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ClashGhost
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private int x1, x2;
        public MainPage()
        {
            this.InitializeComponent();

            Stopwatch sw = Stopwatch.StartNew();

            ManipulationMode = ManipulationModes.TranslateRailsX | ManipulationModes.TranslateRailsY;
            ManipulationStarted += (s, e) => x1 = (int)e.Position.X;
            ManipulationCompleted += (s, e) =>
            {
                x2 = (int)e.Position.X;
                if (x1 > x2)
                    swypeActionResult(false);
                else
                    swypeActionResult(true);
            };

            if (sw.ElapsedMilliseconds > 5000)
            {
                intruct.Visibility = Visibility.Visible;
                intruct2.Visibility = Visibility.Visible;
            }
        }
        
        private void swypeActionResult(bool leftRight)
        {
            switch (leftRight)
            {
                case false:
                    Frame.Navigate(typeof(Login));
                    break;
                case true:
                    Frame.Navigate(typeof(Register), true);
                    break;
            }
        }
    }
}

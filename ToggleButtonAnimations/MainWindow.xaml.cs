using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ToggleButtonAnimations
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TranslateTransform TranslateTransform;
        private DispatcherTimer StopDotTimer;
        private bool IsOnline;
        private int MarginXOfDot;
        private int DotSpeed;

        public MainWindow()
        {
            InitializeComponent();

            InitializeTranslateTransform();
            InitializeTimer();

            MarginXOfDot = Convert.ToInt32(BorderOnOffDot.Margin.Left);
            DotSpeed = 8;
        }

        private void BtnOnOff_Click(object sender, RoutedEventArgs e)
        {
            StopDotTimer.Start();
            IsOnline = !IsOnline;
        }

        private void InitializeTimer()
        {
            StopDotTimer = new DispatcherTimer();
            StopDotTimer.Interval = TimeSpan.FromMilliseconds(10);
            StopDotTimer.Tick += Timer_Tick;
        }

        private void InitializeTranslateTransform()
        {
            TranslateTransform = new TranslateTransform();
            BorderOnOffDot.RenderTransform = TranslateTransform;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (IsOnline)
            {
                TranslateTransform.X += DotSpeed;

                if (TranslateTransform.X + BorderOnOffDot.Width > GrdBtnContent.ActualWidth - (DotSpeed) - MarginXOfDot)
                {
                    StopDotTimer.Stop();
                }

                BorderToggleButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#33CC33"));
                LblOnOff.Content = "Online";
                LblOnOff.Foreground = Brushes.White;

            }
            else
            {
                TranslateTransform.X -= DotSpeed;

                if (TranslateTransform.X == 0)
                {
                    StopDotTimer.Stop();
                }

                BorderToggleButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CCCCCC"));
                LblOnOff.Content = "Offline";
                LblOnOff.Foreground = Brushes.Black;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            StopDotTimer.Tick -= Timer_Tick;
        }
    }
}
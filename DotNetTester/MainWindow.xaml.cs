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

namespace DotNetTester
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        [System.Runtime.InteropServices.DllImport("msvcrt.dll", CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        private static extern void _tzset();

        private void OnBtn001Clicked(object sender, RoutedEventArgs e)
        {
            string localTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            MessageBox.Show(localTime, "Local Time");
        }
       
        
        private void OnBtn002Clicked(object sender, RoutedEventArgs e)
        {
            TimeZoneInfo.ClearCachedData();     // tzset에 해당
        }
        
        private void OnBtn003Clicked(object sender, RoutedEventArgs e)
        {
            _tzset();     // 안먹음.
        }
        
        private void OnBtn004Clicked(object sender, RoutedEventArgs e)
        {
        }
        
        private void OnBtn005Clicked(object sender, RoutedEventArgs e)
        {
        }
        
        private void OnBtn006Clicked(object sender, RoutedEventArgs e)
        {
        }
        
        private void OnBtn007Clicked(object sender, RoutedEventArgs e)
        {
        }
        
        private void OnBtn008Clicked(object sender, RoutedEventArgs e)
        {
        }
        
        private void OnBtn009Clicked(object sender, RoutedEventArgs e)
        {
        }
        
        private void OnBtn010Clicked(object sender, RoutedEventArgs e)
        {
        }
        
        private void OnBtn011Clicked(object sender, RoutedEventArgs e)
        {
        }
        
        private void OnBtn012Clicked(object sender, RoutedEventArgs e)
        {
        }

        private void OnBtn013Clicked(object sender, RoutedEventArgs e)
        {
        }

        private void OnBtn014Clicked(object sender, RoutedEventArgs e)
        {
        }
        
        private void OnBtn015Clicked(object sender, RoutedEventArgs e)
        {
        }
        
        private void OnBtn016Clicked(object sender, RoutedEventArgs e)
        {
        }
        
        private void OnBtn017Clicked(object sender, RoutedEventArgs e)
        {
        }
        
        private void OnBtn018Clicked(object sender, RoutedEventArgs e)
        {
        }
        
        private void OnBtn019Clicked(object sender, RoutedEventArgs e)
        {
        }
        
        private void OnBtn020Clicked(object sender, RoutedEventArgs e)
        {
        }
        
        private void OnBtn021Clicked(object sender, RoutedEventArgs e)
        {
        }
        
        private void OnBtn022Clicked(object sender, RoutedEventArgs e)
        {
        }
        
        private void OnBtn023Clicked(object sender, RoutedEventArgs e)
        {
        }
        
        private void OnBtn024Clicked(object sender, RoutedEventArgs e)
        {
        }
        
        private void OnBtn025Clicked(object sender, RoutedEventArgs e)
        {
        }
        
        private void OnBtn026Clicked(object sender, RoutedEventArgs e)
        {
        }
        
        private void OnBtn027Clicked(object sender, RoutedEventArgs e)
        {
        }
        
        private void OnBtn028Clicked(object sender, RoutedEventArgs e)
        {
        }
        
        private void OnBtn029Clicked(object sender, RoutedEventArgs e)
        {
        }
        
        private void OnBtn030Clicked(object sender, RoutedEventArgs e)
        {
        }
    }
}
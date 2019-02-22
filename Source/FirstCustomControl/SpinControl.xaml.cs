using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FirstCustomControl
{
    /// <summary>
    /// UserControl1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SpinControl : UserControl
    {
		private BitmapImage[] images = new BitmapImage[3];

		public SpinControl()
        {
            InitializeComponent();
        }
		
		private void SpinControl_Loaded(object sender, RoutedEventArgs e)
		{
			images[0] = new BitmapImage(new Uri("Cherries.png", UriKind.Relative));
			images[1] = new BitmapImage(new Uri("Jackpot.jpg", UriKind.Relative));
			images[2] = new BitmapImage(new Uri("Limes.jpg", UriKind.Relative));
		}

		public int Spin()
		{
			Random r = new Random(DateTime.Now.Millisecond);
			int randomNumber = r.Next(3);
			this.imgDisplay.Source = images[randomNumber];

			((Storyboard)Resources["SpinImageStoryBoard"]).Begin();
			return randomNumber;
		}
	}
}

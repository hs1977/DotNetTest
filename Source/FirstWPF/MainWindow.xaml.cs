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

namespace FirstWPF
{
	/// <summary>
	/// MainWindow.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class MainWindow : Window
	{
		List<BitmapImage> images = new List<BitmapImage>();

		private bool isSpinning = false;

		private int currImage = 0;
		private const int MAX_IMAGES = 6;

		public MainWindow()
		{
			InitializeComponent();

			lstStyles.Items.Add("GrowingButtonStyle");
			lstStyles.Items.Add("TiltButton");
			lstStyles.Items.Add("BigGreenButton");
			lstStyles.Items.Add("BasicControlStyle");
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			try
			{
				string path = Environment.CurrentDirectory;

				images.Add(new BitmapImage(new Uri(string.Format(@"{0}\Images\바다1.jpg", path))));
				images.Add(new BitmapImage(new Uri(string.Format(@"{0}\Images\바다3.jpg", path))));
				images.Add(new BitmapImage(new Uri(string.Format(@"{0}\Images\바다5.jpg", path))));

				images.Add(new BitmapImage(new Uri(@"/Images/박보영3.jpg", UriKind.Relative)));
				images.Add(new BitmapImage(new Uri(@"/Images/박보영4.jpg", UriKind.Relative)));
				images.Add(new BitmapImage(new Uri(@"/Images/박보영5.jpg", UriKind.Relative)));

				imageHolder.Source = images[currImage];
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void BtnPreviousImage_Click(object sender, RoutedEventArgs e)
		{
			if(--currImage < 0)
			{
				currImage = MAX_IMAGES - 1;
			}
				
			imageHolder.Source = images[currImage];
		}

		private void BtnNextImage_Click(object sender, RoutedEventArgs e)
		{
			if(++currImage == MAX_IMAGES)
			{
				currImage = 0;
			}
				
			imageHolder.Source = images[currImage];
		}

		private void BtnTest1_Click(object sender, RoutedEventArgs e)
		{
			Resources["ApplicationBrush"] = new SolidColorBrush(Colors.Red);
			Resources["WindowBrush"] = new SolidColorBrush(Colors.Red);
		}

		private void BtnTest2_Click(object sender, RoutedEventArgs e)
		{
			Resources["FileBrush"] = new SolidColorBrush(Colors.Red);
			Resources["LibraryBrush"] = new SolidColorBrush(Colors.Red);

			DoubleAnimation dblAnim = new DoubleAnimation();
			dblAnim.From = 1.0;
			dblAnim.To = 0.0;
			dblAnim.AutoReverse = true;
			dblAnim.RepeatBehavior = new RepeatBehavior(2);

			// Now, animation the RotateTransform object. 
			btnTest2.BeginAnimation(Button.OpacityProperty, dblAnim);
		}

		private void BtnTest2_MouseEnter(object sender, MouseEventArgs e)
		{
			if (!isSpinning)
			{
				isSpinning = true;

				// Make a double animation object, and register
				// with the Completed event. 
				DoubleAnimation dblAnim = new DoubleAnimation();
				dblAnim.Completed += (o, s) => { isSpinning = false; };

				// Button has 2 seconds to finish the spin!
				dblAnim.Duration = new Duration(TimeSpan.FromSeconds(2));

				// Set the start value and end value. 
				dblAnim.From = 0;
				dblAnim.To = 360;
				dblAnim.RepeatBehavior = new RepeatBehavior(2);

				// Now, create a RotateTransform object, and set 
				// it to the RenderTransform property of our
				// button
				RotateTransform rt = new RotateTransform();
				btnTest2.RenderTransform = rt;

				// Now, animation the RotateTransform object. 
				rt.BeginAnimation(RotateTransform.AngleProperty, dblAnim);
			}
		}

		private void LstStyles_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Style currStyle = (Style)TryFindResource(lstStyles.SelectedValue);

			if (currStyle != null)
			{
				// Set the style of the button type.
				btnTest3.Style = currStyle;
			}
		}
	}
}

using System;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace WPFStudy
{
	class StudyWindow : Window
	{
		SolidColorBrush scbrush = new SolidColorBrush(Colors.Black);
	//	SolidColorBrush scbrush = Brushes.Black;		// 읽기 전용이라 Runtime Error

		int index = 0;
        PropertyInfo[] props;

		public StudyWindow()
		{
			Title = "Huiva Huiva Main Window";

		//	LinearGradientBrush lgbrush = new LinearGradientBrush(Colors.Red, Colors.Blue, new Point(0, 0), new Point(1, 1));
		//	Background = lgbrush;

			RadialGradientBrush rgbrush = new RadialGradientBrush(Colors.White, Colors.Red);
			Background = rgbrush;
			rgbrush.SpreadMethod = GradientSpreadMethod.Repeat;

			Content = "Huiva Huiva Main Window";
			Foreground = rgbrush;

			props = typeof(Brushes).GetProperties(BindingFlags.Public | BindingFlags.Static);
		}

		protected override void OnMouseDown(MouseButtonEventArgs args)
		{
			string strMessage = string.Format("Window clicked with {0} button at point ({1})", args.ChangedButton, args.GetPosition(this));
			MessageBox.Show(strMessage, Title);
		}

		protected override void OnMouseMove(MouseEventArgs args)
        {
            double width = ActualWidth - 2 * SystemParameters.ResizeFrameVerticalBorderWidth;
            double height = ActualHeight - 2 * SystemParameters.ResizeFrameHorizontalBorderHeight - SystemParameters.CaptionHeight;

            Point ptMouse = args.GetPosition(this);
            Point ptCenter = new Point(width / 2, height / 2);

            Vector vectMouse = ptMouse - ptCenter;

            double angle = Math.Atan2(vectMouse.Y, vectMouse.X);
            Vector vectEllipse = new Vector(width / 2 * Math.Cos(angle), height / 2 * Math.Sin(angle));
            Byte byLevel = (byte) (255 * (1 - Math.Min(1, vectMouse.Length / vectEllipse.Length)));

            Color clr = scbrush.Color;
            clr.R = clr.G = clr.B = byLevel;
            scbrush.Color = clr;

			Background = scbrush;
        }

		protected override void OnKeyDown(KeyEventArgs args)
        {
            if(args.Key == Key.Down || args.Key == Key.Up)
            {
                index += args.Key == Key.Up ? 1 : props.Length - 1;
                index %= props.Length;
                SetTitleAndBackground();
            }
            base.OnKeyDown(args);
        }

		protected override void OnTextInput(TextCompositionEventArgs args)
        {
            base.OnTextInput(args);
            string str = Content as string;

            if(args.Text == "\b")
            {
                if(str.Length > 0)
				{
					str = str.Substring(0, str.Length - 1);
				}
            }
            else
            {
                str += args.Text;
            }

            Content = str;
        }

        void SetTitleAndBackground()
        {
            Title = "Flip Through the Brushes - " + props[index].Name;
            Background = (Brush)props[index].GetValue(null, null);
        }
	}
}

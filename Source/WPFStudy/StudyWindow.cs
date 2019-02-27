using System;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace WPFStudy
{
	class StudyWindow : Window
	{
		SolidColorBrush brush = new SolidColorBrush(Colors.Black);
	//	SolidColorBrush brush = Brushes.Black;		// 읽기 전용이라 Runtime Error

		int index = 0;
        PropertyInfo[] props;

		public StudyWindow()
		{
			Title = "Huiva Huiva Main Window";
			Background = brush;

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

            Color clr = brush.Color;
            clr.R = clr.G = clr.B = byLevel;
            brush.Color = clr;
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

        void SetTitleAndBackground()
        {
            Title = "Flip Through the Brushes - " + props[index].Name;
            Background = (Brush)props[index].GetValue(null, null);
        }
	}
}

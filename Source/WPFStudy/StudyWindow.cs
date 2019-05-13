using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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

			MouseDown += MouseEventHandler1;
			MouseDown += MouseEventHandler2;
			MouseUp += AllPurposeEventHandler;
			PreviewMouseDown += AllPurposeEventHandler;
			PreviewMouseUp += AllPurposeEventHandler;
		}

		void AllPurposeEventHandler(object sender, RoutedEventArgs args)
		{
			// Display event information.
			TextBlock text = new TextBlock();
			text.Text = String.Format("{0,-30} {1,-15} {2,-15} {3,-15}",
			                          args.RoutedEvent.Name,
			                          TypeWithoutNamespace(sender),
			                          TypeWithoutNamespace(args.Source),
			                          TypeWithoutNamespace(args.OriginalSource));

			System.Console.WriteLine(text.Text);
		}

        void MouseEventHandler1(object sender, RoutedEventArgs args)
        {
            // Display event information.
            TextBlock text = new TextBlock();
            text.Text = String.Format("{0,-30} {1,-15} {2,-15} {3,-15}",
                                      args.RoutedEvent.Name,
                                      TypeWithoutNamespace(sender),
                                      TypeWithoutNamespace(args.Source),
                                      TypeWithoutNamespace(args.OriginalSource));

            System.Console.WriteLine(text.Text);
        }

        void MouseEventHandler2(object sender, RoutedEventArgs args)
        {
            // Display event information.
            TextBlock text = new TextBlock();
            text.Text = String.Format("{0,-30} {1,-15} {2,-15} {3,-15}",
                                      args.RoutedEvent.Name,
                                      TypeWithoutNamespace(sender),
                                      TypeWithoutNamespace(args.Source),
                                      TypeWithoutNamespace(args.OriginalSource));

            System.Console.WriteLine(text.Text);
        }

        string TypeWithoutNamespace(object obj)
		{
			string[] astr = obj.GetType().ToString().Split('.');
			return astr[astr.Length - 1];
		}

		private void PrintThread(string str)
		{
			Thread cur_thread = Thread.CurrentThread; 
			System.Console.WriteLine(str + cur_thread.ManagedThreadId.ToString());
		}

		private int LongRun(int Sec)					// 새로운 스레드 진입함수
		{
			PrintThread("Before LongRun:");

			Thread.Sleep(1000 * Sec);

			PrintThread("After LongRun:");
			return Sec;
		}

		private void AsnycCaller()
		{
			PrintThread("Before AsnycCaller:");
			AsyncRun();
			
			for(int i = 0; i < 7; i++)
			{
				Thread.Sleep(1000);
			}
			
			PrintThread("After AsnycCaller:");
		}

		private async void AsyncRun()	// asyn return type should void, Task, Task<T>
		{
			PrintThread("Before AsyncRun:");

			var task = Task<int>.Run(() => LongRun(5));	// 새로운 스레드가 생성된다.
			await task;		// await를 만나면 async 함수가 바로 반환된다. 
							// 만일 UI Thread에서 async 함수를 호출했을 경우 await가 끝난 이후부터 async 함수 끝까지 코드를 UI Thread가 수행한다.
							// 그러나 UI Thread가 아닌 스레드에서 async 함수를 호출했을 경우 await가 끝난 이후부터 async 함수 끝까지 코드를 await하는 task의 스레드가 수행한다.

			PrintThread("After AsyncRun:");
		}

		protected override void OnMouseDown(MouseButtonEventArgs args)
		{
			PrintThread("Before OnMouseDown:");

			/*
			if(args.ChangedButton == MouseButton.Left)
			{
				AsnycCaller();
			}
			else if(args.ChangedButton == MouseButton.Right)
			{
				Task.Run(() => AsnycCaller());	// 새로운 스레드가 생성된다.
			}
			*/

			PrintThread("After OnMouseDown:");

		//	args.Handled = true;	// 주석 처리해야 MouseEventHandler1, 2가 호출된다.

		//	string strMessage = string.Format("Window clicked with {0} button at point ({1})", args.ChangedButton, args.GetPosition(this));
		//	MessageBox.Show(strMessage, Title);
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

		protected override void OnMouseUp(MouseButtonEventArgs args)
		{
			PrintThread("Before OnMouseUp:");

			PrintThread("After OnMouseUp:");
		}

		protected override void OnKeyDown(KeyEventArgs args)
        {
			PrintThread("OnKeyDown");

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

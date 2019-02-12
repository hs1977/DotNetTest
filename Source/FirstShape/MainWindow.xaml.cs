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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FirstShape
{
	/// <summary>
	/// MainWindow.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class MainWindow : Window
	{
		private enum SelectedShape
		{
			None,
			Circle,
			Rectangle,
			Line
		}

		private SelectedShape currentShape = SelectedShape.None;

		private bool isFlipped = false;

		public MainWindow()
		{
			InitializeComponent();
		}

		private void CircleOption_Click(object sender, RoutedEventArgs e)
		{
			currentShape = SelectedShape.Circle;
		}

		private void CanvasDrawingArea_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			Shape shapeToRender = null;

			// configure the correct shape to draw.
			switch(currentShape)
			{
			case SelectedShape.Circle:
				shapeToRender = new Ellipse()
				{
					Height = 35, Width = 35
				};

				RadialGradientBrush brush = new RadialGradientBrush();
				brush.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FF87E71B"), 0.589));
				brush.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FF2BA92B"), 0.013));
				brush.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FF34B71B"), 1));
				shapeToRender.Fill = brush;
				break;
			case SelectedShape.Rectangle:
				shapeToRender = new Rectangle()
				{
					Fill = Brushes.Red, Height = 35, Width = 35, RadiusX = 10, RadiusY = 10
				};

				break;
			case SelectedShape.Line:
				shapeToRender = new Line()
				{
					Stroke = Brushes.Blue,
					StrokeThickness = 10,
					X1 = 0,
					X2 = 50,
					Y1 = 0,
					Y2 = 50,
					StrokeStartLineCap = PenLineCap.Triangle,
					StrokeEndLineCap = PenLineCap.Round
				};

				break;
			default:
				return;
			}

			if(isFlipped)
			{
				RotateTransform rotate = new RotateTransform(-180);
				shapeToRender.RenderTransform = rotate;
			}

			// Set top / left to draw in the canvas.
			Canvas.SetLeft(shapeToRender, e.GetPosition(canvasDrawingArea).X);
			Canvas.SetTop(shapeToRender, e.GetPosition(canvasDrawingArea).Y);
			// Draw shape!
			canvasDrawingArea.Children.Add(shapeToRender);
		}

		private void RectOption_Click(object sender, RoutedEventArgs e)
		{
			currentShape = SelectedShape.Rectangle;
		}

		private void LineOption_Checked(object sender, RoutedEventArgs e)
		{
			currentShape = SelectedShape.Line;
		}

		private void CanvasDrawingArea_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
		{
			Point pt = e.GetPosition((Canvas)sender);
			HitTestResult result = VisualTreeHelper.HitTest(canvasDrawingArea, pt);

			// If the result is not null, they DID click on a shape!
			if(result != null)
			{
				canvasDrawingArea.Children.Remove(result.VisualHit as Shape);
			}
		}

		private void FlipCanvas_Click(object sender, RoutedEventArgs e)
		{
			if(flipCanvas.IsChecked == true)
			{
				RotateTransform rotate = new RotateTransform(-180);
				canvasDrawingArea.LayoutTransform = rotate;
				isFlipped = true;
			}
			else
			{
				canvasDrawingArea.LayoutTransform = null;
				isFlipped = false;
			}
		}
	}
}

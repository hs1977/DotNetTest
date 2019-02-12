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
using System.Windows.Ink;
using System.IO;
using System.Windows.Annotations;
using System.Windows.Annotations.Storage;
using System.Windows.Markup;

namespace FirstBlend
{
	/// <summary>
	/// MainWindow.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			MyInkCanvas.EditingMode = InkCanvasEditingMode.Ink;
			inkRadio.IsChecked = true;
			comboColors.SelectedIndex = 0;
			EnableAnnotations();
			btnSaveDoc.Click += (o, s) =>
			{
				using(FileStream fStream = File.Open("documentData.xaml", FileMode.Create))
				{
					XamlWriter.Save(this.myDocumentReader.Document, fStream);
				}
			};
			btnLoadDoc.Click += (o, s) =>
			{
				using(FileStream fStream = File.Open("documentData.xaml", FileMode.Open))
				{
					try
					{
						FlowDocument doc = XamlReader.Load(fStream) as FlowDocument;
						this.myDocumentReader.Document = doc;
					}
					catch(Exception ex)
					{
						MessageBox.Show(ex.Message, "Error Loading Doc!");
					}
				}
			};

			SetBindings();
		}

		private void SetBindings()
		{
			// Create a Binding object.
			Binding b = new Binding();
			// Register the converter, source and path.
			b.Converter = new DoubleToInt();
			b.Source = this.mySB;
			b.Path = new PropertyPath("Value");
			// Call the SetBinding method on the Label.
			labelSBThumb.SetBinding(Label.ContentProperty, b);
		}

		private void EnableAnnotations()
		{
			// Create the AnnotationService object that works
			// with our FlowDocumentReader.
			AnnotationService anoService = new AnnotationService(myDocumentReader);
			// Create a MemoryStream which will hold the annotations.
			MemoryStream anoStream = new MemoryStream();
			// Now, create a XML-based store based on the MemoryStream.
			// You could use this object to programmatically add, delete
			// or find annotations.
			AnnotationStore store = new XmlStreamStore(anoStream);
			// Enable the annotation services.
			anoService.Enable(store);
		}

		private void RadioButton_Click(object sender, RoutedEventArgs e)
		{
			switch((sender as RadioButton).Content.ToString())
			{
			case "Ink Mode!":
				MyInkCanvas.EditingMode = InkCanvasEditingMode.Ink;
				break;

			case "Erase Mode!":
				MyInkCanvas.EditingMode = InkCanvasEditingMode.EraseByStroke;
				break;

			case "Select Mode!":
				MyInkCanvas.EditingMode = InkCanvasEditingMode.Select;
				break;
			}
		}

		private void ColorChanged(object sender, SelectionChangedEventArgs e)
		{
			string color = (comboColors.SelectedItem as StackPanel).Tag.ToString();
			MyInkCanvas.DefaultDrawingAttributes.Color = (Color)ColorConverter.ConvertFromString(color);
		}

		private void SaveData(object sender, System.Windows.RoutedEventArgs e)
		{
			// Save all data on the InkCanvas to a local file.
			using(FileStream fs = new FileStream("StrokeData.bin", FileMode.Create))
			{
				MyInkCanvas.Strokes.Save(fs);
				fs.Close();
			}
		}

		private void LoadData(object sender, System.Windows.RoutedEventArgs e)
		{
			// Fill StrokeCollection from file.
			using(FileStream fs = new FileStream("StrokeData.bin", FileMode.Open, FileAccess.Read))
			{
				StrokeCollection strokes = new StrokeCollection(fs);
				MyInkCanvas.Strokes = strokes;
				fs.Close();
			}
		}

		private void Clear(object sender, System.Windows.RoutedEventArgs e)
		{
			// Clear all strokes.
			MyInkCanvas.Strokes.Clear();
		}
	}
}

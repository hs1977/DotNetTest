using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace WPFTester
{
	/// <summary>
	/// MainWindow.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class MainWindow : Window
	{
		string mouseActivity = string.Empty;
		private string dataToShow = string.Empty;
		private Control ctrlToExamine = null;

		public MainWindow()
		{
			InitializeComponent();
		}

		private void BtnClickMe_Click(object sender, RoutedEventArgs e)
		{
			AddEventInfo(sender, e);
			MessageBox.Show(mouseActivity, "Fancy Button!");

			mouseActivity = "";
		}

		private void AddEventInfo(object sender, RoutedEventArgs e)
		{
			mouseActivity += string.Format(
				"{0} sent a {1} event named {2}.\n", sender,
				e.RoutedEvent.RoutingStrategy,
				e.RoutedEvent.Name);
		}

		private void OuterEllipse_MouseDown(object sender, MouseButtonEventArgs e)
		{
			AddEventInfo(sender, e);
		}

		private void OuterEllipse_PreviewMouseDown(object sender, MouseButtonEventArgs e)
		{
			AddEventInfo(sender, e);
		}

		private void BtnShowLogicalTree_Click(object sender, RoutedEventArgs e)
		{
			dataToShow = "";
			BuildLogicalTree(0, this);
			txtDisplayArea.Text = dataToShow;
		}

		void BuildLogicalTree(int depth, object obj)
		{
			// Add the type name to the dataToShow member variable.
			dataToShow += new string(' ', depth) + obj.GetType().Name + "\n";

			// If an item is not a DependencyObject, skip it. 
			if (!(obj is DependencyObject))
				return;

			// Make a recursive call for each logical child
			foreach (object child in LogicalTreeHelper.GetChildren(obj as DependencyObject))
				BuildLogicalTree(depth + 4, child);
		}

		private void BtnShowVisualTree_Click(object sender, RoutedEventArgs e)
		{
			dataToShow = "";
			BuildVisualTree(0, this);
			txtDisplayArea.Text = dataToShow;
		}

		void BuildVisualTree(int depth, DependencyObject obj)
		{
			// Add the type name to the dataToShow member variable.
			dataToShow += new string(' ', depth) + obj.GetType().Name + "\n";

			// Make a recursive call for each visual child
			for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
				BuildVisualTree(depth + 1, VisualTreeHelper.GetChild(obj, i));
		}

		private void BtnTemplate_Click(object sender, RoutedEventArgs e)
		{
			dataToShow = "";
			ShowTemplate();
			this.txtDisplayArea.Text = dataToShow;
		}

		private void ShowTemplate()
		{
			// Remove the control which is currently in the preview area. 
			if (ctrlToExamine != null)
				stackTemplatePanel.Children.Remove(ctrlToExamine);
			try
			{
				// Load PresentationFramework, and create an instance of the
				// specified control.  Give it a size for display purposes, then add to the 
				// empty StackPanel. 
				System.Reflection.Assembly asm = Assembly.Load("PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35");
				ctrlToExamine = (Control)asm.CreateInstance(txtFullName.Text);
				ctrlToExamine.Height = 200;
				ctrlToExamine.Width = 200;
				ctrlToExamine.Margin = new Thickness(5);
				stackTemplatePanel.Children.Add(ctrlToExamine);

				// Define some XML settings to preserve indentation.
				XmlWriterSettings xmlSettings = new XmlWriterSettings();
				xmlSettings.Indent = true;

				// Create a StringBuilder to hold the XAML.
				StringBuilder strBuilder = new StringBuilder();

				// Create an XmlWriter based on our settings.
				XmlWriter xWriter = XmlWriter.Create(strBuilder, xmlSettings);

				// Now save the XAML into the XmlWriter object based on the ControlTemplate.
				XamlWriter.Save(ctrlToExamine.Template, xWriter);

				// Display XAML in the text box
				dataToShow = strBuilder.ToString();
			}
			catch (Exception ex)
			{
				dataToShow = ex.Message;
			}
		}

		private void BtnSpin_MouseDown(object sender, MouseButtonEventArgs e)
		{
			imgSpin.Spin();
		}
	}
}

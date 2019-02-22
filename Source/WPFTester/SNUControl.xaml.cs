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

namespace WPFTester
{
    /// <summary>
    /// SNUControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SNUControl : UserControl
    {
        public SNUControl()
        {
            InitializeComponent();
        }

		private void MySNUControl_MouseDown(object sender, MouseButtonEventArgs e)
		{
			VisualStateManager.GoToState(this, "MouseDown", true);
		}

		private void MySNUControl_MouseLeave(object sender, MouseEventArgs e)
		{
			VisualStateManager.GoToState(this, "MouseLeave", true);
		}

		private void MySNUControl_MouseEnter(object sender, MouseEventArgs e)
		{
			VisualStateManager.GoToState(this, "MouseEnter", true);
		}
	}
}

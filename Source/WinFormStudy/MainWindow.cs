using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormStudy
{
	public partial class MainWindow : Form
	{
		public MainWindow()
		{
			InitializeComponent();

			FormClosing += MainWindow_FormClosing;
			Load += MainWindow_Load;
			FormClosed += MainWindow_FormClosed;
			Activated += MainWindow_Activated;
			Deactivate += MainWindow_Deactivate;
		}

		private void MainWindow_Deactivate(object sender, EventArgs e)
		{
		}

		private void MainWindow_Activated(object sender, EventArgs e)
		{
			int a = 0;
		}

		private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
		{
		}

		private void MainWindow_Load(object sender, EventArgs e)
		{
		}

		private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
		{
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void MainWindow_MouseMove(object sender, MouseEventArgs e)
		{

		}

		private void testDlgToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TestDlg dlg = new TestDlg();

			if(dlg.ShowDialog() == DialogResult.OK)
			{
				String str = dlg.m_EditText.Text;
				MessageBox.Show(str, "OK");
			}
			else if(dlg.ShowDialog() == DialogResult.Cancel)
			{
				String str = dlg.m_EditText.Text;
				MessageBox.Show(str, "Cancel");
			}
		}

		private void derivedDlgToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DerivedDlg dlg = new DerivedDlg();

			if(dlg.ShowDialog() == DialogResult.OK)
			{
				String str = dlg.m_EditText.Text;
				MessageBox.Show(str, "OK");
			}
			else if(dlg.ShowDialog() == DialogResult.Cancel)
			{
				String str = dlg.m_EditText.Text;
				MessageBox.Show(str, "Cancel");
			}
		}
	}
}

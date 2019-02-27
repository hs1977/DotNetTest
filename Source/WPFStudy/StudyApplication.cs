using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WPFStudy
{
	

	class StudyApplication : Application
	{
		protected override void OnStartup(StartupEventArgs args)
		{
			base.OnStartup(args);

			StudyWindow main = new StudyWindow();
			main.Show();

			/*
			for (int i = 0; i < 2; i++)
			{
				Window subwin = new Window();
				subwin.Title = "Extra Window No. " + (i + 1);
				subwin.ShowInTaskbar = false;
				subwin.Show();
			}
			*/
		}
		protected override void OnSessionEnding(SessionEndingCancelEventArgs args)
		{
			base.OnSessionEnding(args);

			MessageBoxResult result = MessageBox.Show
			(
				"Do you want to save your data?",
				MainWindow.Title,
				MessageBoxButton.YesNoCancel,
				MessageBoxImage.Question, MessageBoxResult.Yes
			);

			args.Cancel = (result == MessageBoxResult.Cancel);
		}
	}
}

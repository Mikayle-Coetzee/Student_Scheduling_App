using _10023767_P2.View;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace _10023767_P2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //protected override void OnStartup(StartupEventArgs e)
        //{
        //    base.OnStartup(e);
        //    _10023767_PROG6212.View.StartUpWindow window = new View.StartUpWindow();
        //    _10023767_PROG6212.ViewModel.LoginViewModel VM = new ViewModel.LoginViewModel();
        //    window.DataContext = VM;
        //}


        protected void ApplicationStart(object sender, StartupEventArgs e)
        {
            PresentationTraceSources.SetTraceLevel(this, PresentationTraceLevel.High);

            var startUpWindow = new StartUpWindow();
            startUpWindow.Show();
            startUpWindow.IsVisibleChanged += (s, ev) =>
            {
                if (startUpWindow.IsVisible == false && startUpWindow.IsLoaded)
                {
                    var mainWindowView = new MainWindowView();
                    mainWindowView.Show();
                    startUpWindow.Close();
                }
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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

namespace WearAndGo_Admin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void PageLoad(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = Models.ViewFrames._View_LoadingPage;
            await Task.Delay(2000);
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                Application.Current.MainWindow.Content = Models.ViewFrames._View_Intro;


                // go to landing page if a user does not signed in
                //if (Properties.Settings.Default.UserEmail != "")
                //{
                //    Application.Current.MainWindow.Content = ViewFrames._CabinetIndex;
                //}
                //else
                //{
                //    Application.Current.MainWindow.Content = ViewFrames._LandingPage;

                //}

            }
        }
    }
}

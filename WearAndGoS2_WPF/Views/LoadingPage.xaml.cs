using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Net.NetworkInformation;
using Newtonsoft.Json.Linq;
using Flurl.Http;
using System.Linq;
using System.Windows.Media.Imaging;
using System.IO;

namespace WearAndGoS2_WPF.Views
{
    /// <summary>
    /// Interaction logic for LoadingPage.xaml
    /// </summary>
    public partial class LoadingPage : Page
    {
        public LoadingPage()
        {
            InitializeComponent();
        }

        private async void PageLoaded(object sender, RoutedEventArgs e)
        {
            await Task.Delay(2000);
            if (NetworkInterface.GetIsNetworkAvailable())
            {


                // go to landing page if a user does not signed in
                if (Properties.Settings.Default.UserEmail != "")
                {
                    Application.Current.MainWindow.Content = ViewFrames._CabinetIndex;
                } else
                {
                    Application.Current.MainWindow.Content = ViewFrames._LandingPage;

                }

            }
        }

        
    }
}

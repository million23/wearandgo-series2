using ModernWpf.Controls;
using Newtonsoft.Json.Linq;
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

namespace WearAndGoS2_WPF.Views.Cabinet
{
    /// <summary>
    /// Interaction logic for Account.xaml
    /// </summary>
    public partial class Account : System.Windows.Controls.Page
    {
        public Account()
        {
            InitializeComponent();
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            out_principalName.Text = Properties.Settings.Default.UserEmail;
        }

        private async void LogOut(object sender, RoutedEventArgs e)
        {
            var dialog = new ContentDialog()
            {
                Title = "Log out",
                Content = "Do you want to log out?",
                CloseButtonText = "No",
                PrimaryButtonText = "Yes"
            };

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                Properties.Settings.Default.UserEmail = "";
                Properties.Settings.Default.Save();

                await Controllers.Auth.Client.LogoutAsync();
                Models.UserCartContent.CartContent.RemoveAll();
                Models.UserCartContent.MainUser.RemoveAll();
                Application.Current.MainWindow.Content = ViewFrames._LandingPage;

            }

        }
    }
}

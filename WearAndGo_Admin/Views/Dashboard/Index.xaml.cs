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

namespace WearAndGo_Admin.Views.Dashboard
{
    /// <summary>
    /// Interaction logic for Index.xaml
    /// </summary>
    public partial class Index : Page
    {
        public Index()
        {
            InitializeComponent();
        }

        private async void LogOut(object sender, RoutedEventArgs e)
        {
            var dialog = new ModernWpf.Controls.ContentDialog
            {
                Title = "User Log Out",
                Content = "Do you want to log out?",
                DefaultButton = ModernWpf.Controls.ContentDialogButton.Close,
                CloseButtonText = "No",
                PrimaryButtonText = "Yes"
            };
            var result = await dialog.ShowAsync();
            if (result == ModernWpf.Controls.ContentDialogResult.Primary)
            {
                Application.Current.MainWindow.Content = Models.ViewFrames._View_Intro;
            }
        }

        private void GetData(object sender, RoutedEventArgs e)
        {
            var adminData = JObject.Parse(Properties.Settings.Default.AdminJsonData);
            out_accountholder.Text = (string)adminData["AccountHolder"];
            out_accountID.Text = (string)adminData["AccountID"];
            out_accountType.Text = (bool)adminData["CanCreateAccounts"] ? "Administrator" : "Product Lister";
        }
    }
}

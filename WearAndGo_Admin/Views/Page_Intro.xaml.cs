using System;
using System.Collections.Generic;
using System.IO;
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
using Newtonsoft.Json.Linq;

namespace WearAndGo_Admin.Views
{
    /// <summary>
    /// Interaction logic for Page_Intro.xaml
    /// </summary>
    public partial class Page_Intro : Page
    {
        public Page_Intro()
        {
            InitializeComponent();
        }

        private int loginCount = 0;

        private async void CheckAccount(object sender, RoutedEventArgs e)
        {
            if (CheckFromData())
            {
                var admindata = JObject.Parse(Properties.Settings.Default.AdminJsonData);
                string AdminName;

                AdminName = (string)admindata["AccountHolder"];


                var dialog = new ModernWpf.Controls.ContentDialog
                {
                    Title = "Welcome Message",
                    Content = $"Hello {AdminName}, please proceed to the dashboard",
                    CloseButtonText = "Hello!",
                    DefaultButton = ModernWpf.Controls.ContentDialogButton.Close
                };
                await dialog.ShowAsync();
                Application.Current.MainWindow.Content = Models.ViewFrames._View_Dashboard_Index;
            }
            else
            {
                loginCount++;
                if (loginCount != 4)
                {
                    var dialog = new ModernWpf.Controls.ContentDialog
                    {
                        Title = "Log in Error",
                        Content = "Username or Password incorrect, please retry",
                        CloseButtonText = "Retry",
                        DefaultButton = ModernWpf.Controls.ContentDialogButton.Close
                    };
                    await dialog.ShowAsync();

                } else
                {
                    var dialog = new ModernWpf.Controls.ContentDialog
                    {
                        Title = "Log in Limit Reached",
                        Content = "You tried logging in 3 times, the app will close now.",
                        CloseButtonText = "Ok",
                        DefaultButton = ModernWpf.Controls.ContentDialogButton.Close
                    };
                    await dialog.ShowAsync();
                    Environment.Exit(1);
                }

            }
        }

        private bool CheckFromData()
        {
            bool returnValue = false;
            var FromFile = new JObject();
            FromFile = JObject.Parse(File.ReadAllText($"./Data/AdminAccounts.json"));

            foreach (var item in FromFile["Accounts"])
            {
                if (in_username.Text == (string)item["UserName"] && in_password.Password == (string)item["Password"])
                {
                    Properties.Settings.Default.AdminJsonData = item.ToString();
                    Properties.Settings.Default.Save();

                    returnValue = true;
                }
            }

            return returnValue;
        }
    }
}

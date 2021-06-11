using ModernWpf.Controls;
using Newtonsoft.Json.Linq;
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

namespace WearAndGo_Admin.Views.Dashboard.AccountsSidepane
{
    /// <summary>
    /// Interaction logic for AccountsSidepade_RemoveAccount.xaml
    /// </summary>
    public partial class AccountsSidepade_RemoveAccount : System.Windows.Controls.Page
    {
        public AccountsSidepade_RemoveAccount()
        {
            InitializeComponent();
        }

        private async void RemoveAccount(object sender, RoutedEventArgs e)
        {
            if (in_AccountID.Text == string.Empty)
            {
                var dialog = new ContentDialog
                {
                    Title = "Missing Input",
                    Content = "An Item ID must be specified",
                    CloseButtonText = "Ok",
                    DefaultButton = ContentDialogButton.Close
                };

                await dialog.ShowAsync();
            }
            else
            {
                var dialog = new ContentDialog
                {
                    Title = "Confirm Delete",
                    Content = "Do you want to delete this Account? This is not reversible.\n\nPlease refresh the data grid if you do.",
                    CloseButtonText = "No",
                    PrimaryButtonText = "Yes",
                    DefaultButton = ContentDialogButton.Close
                };
                var result = await dialog.ShowAsync();
                if (result == ContentDialogResult.Primary)
                {
                    var fromFile = JObject.Parse(File.ReadAllText($"./Data/AdminAccounts.json"));
                    var fromArray = JArray.Parse(fromFile["Accounts"].ToString());

                    var index = -1;

                    if (GetAdminAccountsCount() > 1)
                    {
                        for (int i = 0; i < fromArray.Count; i++)
                        {
                            if (fromArray[i]["AccountID"].ToString() == in_AccountID.Text)
                            {
                                index = i;
                            }
                        }

                        fromArray.RemoveAt(index);

                        fromFile["Accounts"] = fromArray;
                        File.WriteAllText($"./Data/AdminAccounts.json", fromFile.ToString());
                    } else
                    {
                        var adminaccountprotection = new ContentDialog
                        {
                            Title = "Admin Account Protection",
                            Content = "The database must have at least 1 admin account",
                            CloseButtonText = "Ok",
                            DefaultButton = ContentDialogButton.Close
                        };
                        await adminaccountprotection.ShowAsync();
                    }


                }


            }
        }

        private int GetAdminAccountsCount()
        {

            var fromFile = JObject.Parse(File.ReadAllText($"./Data/AdminAccounts.json"));
            var fromArray = JArray.Parse(fromFile["Accounts"].ToString());

            var adminAccounts = 0;
            foreach (var item in fromArray)
            {
                if ((bool)item["CanCreateAccounts"])
                {
                    adminAccounts++;
                }
            }

            return adminAccounts;
        }
    }
}

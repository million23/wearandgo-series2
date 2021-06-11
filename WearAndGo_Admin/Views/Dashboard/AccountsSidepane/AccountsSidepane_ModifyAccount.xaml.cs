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
    /// Interaction logic for AccountsSidepane_ModifyAccount.xaml
    /// </summary>
    public partial class AccountsSidepane_ModifyAccount : System.Windows.Controls.Page
    {
        public AccountsSidepane_ModifyAccount()
        {
            InitializeComponent();
        }

        private async void ModifyAccount(object sender, RoutedEventArgs e)
        {
            if (in_accountID.Text == string.Empty)
            {
                var dialog = new ContentDialog
                {
                    Title = "Missing Input",
                    Content = "An Account ID must be specified",
                    CloseButtonText = "Ok",
                    DefaultButton = ContentDialogButton.Close
                };

                await dialog.ShowAsync();
            } else
            {
                var dialog = new ContentDialog
                {
                    Title = "Confirm Delete",
                    Content = "Do you want to modify this item? If you left an input blank, its state will not be updated.\n\nPlease refresh the data grid if you do.",
                    CloseButtonText = "No",
                    PrimaryButtonText = "Yes",
                    DefaultButton = ContentDialogButton.Close
                };
                var result = await dialog.ShowAsync();
                if (result == ContentDialogResult.Primary)
                {
                    var found = false;

                    var fromFile = JObject.Parse(File.ReadAllText($"./Data/AdminAccounts.json"));
                    var fromArray = JArray.Parse(fromFile["Accounts"].ToString());

                    foreach (var item in fromArray)
                    {
                        if (item["AccountID"].ToString() == in_accountID.Text)
                        {
                            found = true;

                            if (in_holder.Text != string.Empty)
                            {
                                item["AccountHolder"] = in_holder.Text;
                            }
                            if (in_username.Text != string.Empty)
                            {
                                item["UserName"] = in_username.Text;
                            }
                            if (in_password.Password != string.Empty)
                            {
                                item["Password"] = in_password.Password;
                            }
                        }
                    }

                    if (found)
                    {
                        fromFile["Accounts"] = fromArray;
                        File.WriteAllText($"./Data/AdminAccounts.json", fromFile.ToString());
                    } else
                    {
                        var ErrorDialog = new ContentDialog
                        {
                            Title = "Account Unidentified",
                            Content = "Seems like this account does not exist in the database.",
                            CloseButtonText = "Ok",
                            DefaultButton = ContentDialogButton.Close
                        };
                        await ErrorDialog.ShowAsync();
                    }
                }
            }
        }
    }
}

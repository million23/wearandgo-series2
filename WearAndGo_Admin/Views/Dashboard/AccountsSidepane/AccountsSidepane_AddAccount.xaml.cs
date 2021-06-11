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
    /// Interaction logic for AccountsSidepane_AddAccount.xaml
    /// </summary>
    public partial class AccountsSidepane_AddAccount : System.Windows.Controls.Page
    {
        public AccountsSidepane_AddAccount()
        {
            InitializeComponent();
        }

        private async void AddAccount(object sender, RoutedEventArgs e)
        {
            if (
                in_accountHolder.Text != string.Empty ||
                in_accountID.Text != string.Empty ||
                in_accountPassword.Password != string.Empty ||
                in_accountUsername.Text != string.Empty
               )
            {

                var dialog = new ContentDialog
                {
                    Title = "Adding Item Confirmation",
                    Content = "Do you want to add this item to the databse",
                    PrimaryButtonText = "Yes",
                    CloseButtonText = "No",
                    DefaultButton = ContentDialogButton.Close,
                };
                var result = await dialog.ShowAsync();
                if (result == ContentDialogResult.Primary)
                {
                    var fromdata = JObject.Parse(File.ReadAllText($"./Data/AdminAccounts.json"));
                    JArray fromArray = JArray.Parse(fromdata["Accounts"].ToString());

                    var NewAccount = new JObject();
                    NewAccount["AccountID"] = in_accountID.Text;
                    NewAccount["UserName"] = in_accountUsername.Text;
                    NewAccount["Password"] = in_accountPassword.Password;
                    NewAccount["AccountHolder"] = in_accountHolder.Text;
                    NewAccount["CanCreateAccounts"] = in_accountPrivilege.IsOn;

                    fromArray.Add(NewAccount);
                    fromdata["Accounts"] = fromArray;


                    in_accountHolder.Text = string.Empty;
                    in_accountID.Text = string.Empty;
                    in_accountPassword.Password = string.Empty;
                    in_accountUsername.Text = string.Empty;
                    in_accountPrivilege.IsOn = false;

                    File.WriteAllText("./Data/AdminAccounts.json", fromdata.ToString());
                }
            }
        }
    }
}

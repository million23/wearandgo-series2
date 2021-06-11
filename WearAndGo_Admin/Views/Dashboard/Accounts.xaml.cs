using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace WearAndGo_Admin.Views.Dashboard
{
    /// <summary>
    /// Interaction logic for Accounts.xaml
    /// </summary>
    public partial class Accounts : Page
    {
        public Accounts()
        {
            InitializeComponent();
        }

        private void GetData(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = null;
            string MyDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var fromFile = JObject.Parse(File.ReadAllText($"./Data/AdminAccounts.json"));

            DataTable thisTable = new DataTable();
            thisTable.Columns.Add("ID");
            thisTable.Columns.Add("Account Holder");
            thisTable.Columns.Add("Username");
            thisTable.Columns.Add("Password");
            thisTable.Columns.Add("Administrative Control");

            foreach (var item in fromFile["Accounts"])
            {
                //MessageBox.Show((string)item["name"]);

                var row = thisTable.NewRow();
                row["ID"] = item["AccountID"];
                row["Account Holder"] = item["AccountHolder"];
                row["Username"] = item["UserName"];
                row["Password"] = item["Password"];
                row["Administrative Control"] = item["CanCreateAccounts"];
                

                thisTable.Rows.Add(row);
            }

            dataGrid.ItemsSource = thisTable.DefaultView;
        }

        private void RefreshList(object sender, RoutedEventArgs e)
        {
            GetData(null, null);
        }

        private void sp_modifyItem(object sender, RoutedEventArgs e)
        {
            SidePane.IsPaneOpen = true;
            SideFrame.Navigate(Models.AccountFrames._SP_A_ModifyItem);
        }

        private void sp_removeItem(object sender, RoutedEventArgs e)
        {
            SidePane.IsPaneOpen = true;
            SideFrame.Navigate(Models.AccountFrames._SP_A_RemoveItem);
        }

        private void sp_addItem(object sender, RoutedEventArgs e)
        {
            SidePane.IsPaneOpen = true;
            SideFrame.Navigate(Models.AccountFrames._SP_A_AddItem);
        }
    }
}

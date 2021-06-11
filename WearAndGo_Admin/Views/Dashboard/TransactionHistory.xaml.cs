using ModernWpf.Controls;
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
    /// Interaction logic for TransactionHistory.xaml
    /// </summary>
    public partial class TransactionHistory : System.Windows.Controls.Page
    {
        public TransactionHistory()
        {
            InitializeComponent();
        }

        private void GetData(object sender, RoutedEventArgs e)
        {
            MainGrid.ItemsSource = null;
            string MyDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var fromFile = JObject.Parse(File.ReadAllText($"{MyDocumentsPath}/WearAndGoS2/TransactionHistory.json"));

            var thisTable = new DataTable();
            thisTable.Columns.Add("Transaction ID");
            thisTable.Columns.Add("Owner");
            thisTable.Columns.Add("Date Purchased");
            thisTable.Columns.Add("Total Purchase");

            foreach (var item in fromFile["History"])
            {
                var row = thisTable.NewRow();
                row["Transaction ID"] = item["TransactionID"];
                row["Owner"] = item["User"];
                row["Date Purchased"] = item["Date"];
                row["Total Purchase"] = item["Total"];

                thisTable.Rows.Add(row);
            }

            MainGrid.ItemsSource = thisTable.DefaultView;
        }

        private void RefreshList(object sender, RoutedEventArgs e)
        {
            GetData(null, null);
        }

        private void FindInList(object sender, RoutedEventArgs e)
        {
            splitView.IsPaneOpen = true;
        }

        private async void SearchFromList(object sender, RoutedEventArgs e)
        {
            bool arefound = false;
            splitView.IsPaneOpen = false;

            if (search_transactionID.Text == string.Empty && search_owner.Text == string.Empty)
            {
                var dialog = new ContentDialog
                {
                    Title = "Search Error",
                    Content = "Please fill in one of the search textbox",
                    CloseButtonText = "Ok",
                    DefaultButton = ContentDialogButton.Close
                };
                await dialog.ShowAsync();
            } else
            {

                string MyDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                var fromFile = JObject.Parse(File.ReadAllText($"{MyDocumentsPath}/WearAndGoS2/TransactionHistory.json"));


                var thisTable = new DataTable();
                thisTable.Columns.Add("Transaction ID");
                thisTable.Columns.Add("Owner");
                thisTable.Columns.Add("Date Purchased");
                thisTable.Columns.Add("Total Purchase");


                foreach (var item in fromFile["History"])
                {
                    if ((string)item["TransactionID"] == search_transactionID.Text)
                    {
                        var row = thisTable.NewRow();
                        row["Transaction ID"] = item["TransactionID"];
                        row["Owner"] = item["User"];
                        row["Date Purchased"] = item["Date"];
                        row["Total Purchase"] = item["Total"];

                        thisTable.Rows.Add(row);
                        arefound = true;

                    }else if ((string)item["User"] == search_owner.Text)
                    {
                        var row = thisTable.NewRow();
                        row["Transaction ID"] = item["TransactionID"];
                        row["Owner"] = item["User"];
                        row["Date Purchased"] = item["Date"];
                        row["Total Purchase"] = item["Total"];

                        thisTable.Rows.Add(row);
                        arefound = true;
                    }
                }

                if (!arefound)
                {
                    var dialog = new ContentDialog
                    {
                        Title = "Unidentified Object",
                        Content = "We cannot find that information. Try again.",
                        CloseButtonText = "Ok",
                        DefaultButton = ContentDialogButton.Close
                    };
                    await dialog.ShowAsync();
                } else
                {
                    MainGrid.ItemsSource = thisTable.DefaultView;
                }

                search_owner.Text = string.Empty;
                search_transactionID.Text = string.Empty;

            }

        }
    }
}

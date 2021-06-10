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
    public partial class TransactionHistory : Page
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
    }
}

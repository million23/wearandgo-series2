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
    /// Interaction logic for ProductListing.xaml
    /// </summary>
    public partial class ProductListing : Page
    {
        public ProductListing()
        {
            InitializeComponent();
        }
        private void sp_modifyItem(object sender, RoutedEventArgs e)
        {
            SidePane.IsPaneOpen = true;
        }

        private void sp_removeItem(object sender, RoutedEventArgs e)
        {
            SidePane.IsPaneOpen = true;
        }

        private void sp_addItem(object sender, RoutedEventArgs e)
        {
            SidePane.IsPaneOpen = true;
            SideFrame.Navigate(Models.ProductListingFrames._SP_PL_AddItem);
        }

        private void ShowAllData(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = null;
            string MyDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var fromFile = JObject.Parse(File.ReadAllText($"{MyDocumentsPath}/WearAndGoS2/MainDatabase.json"));

            DataTable thisTable = new DataTable();
            thisTable.Columns.Add("ID");
            thisTable.Columns.Add("Name");
            thisTable.Columns.Add("Type");
            thisTable.Columns.Add("Price");
            thisTable.Columns.Add("Variants");
            thisTable.Columns.Add("Image URI");

            foreach (var item in fromFile["items"])
            {
                //MessageBox.Show((string)item["name"]);
                
                var row = thisTable.NewRow();
                row["ID"] = item["id"];
                row["Name"] = item["name"];
                switch ((string)item["kind"])
                {
                    case "M":
                        row["Type"] = "Men";
                        break;
                    case "W":
                        row["Type"] = "Women";
                        break;
                    case "F":
                        row["Type"] = "Footwears";
                        break;
                    default:
                        row["Type"] = "UNSPECIFIED";
                        break;
                }
                row["Price"] = item["price"];
                string out_variant = string.Empty;
                foreach (var variant in item["variant"])
                {
                    out_variant += $"{variant},";
                }
                row["Variants"] = out_variant;
                row["Image URI"] = item["image"];

                thisTable.Rows.Add(row);
            }

            dataGrid.ItemsSource = thisTable.DefaultView;
        }

        private void ShowMenData(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = null;
            string MyDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var fromFile = JObject.Parse(File.ReadAllText($"{MyDocumentsPath}/WearAndGoS2/MainDatabase.json"));

            DataTable thisTable = new DataTable();
            thisTable.Columns.Add("ID");
            thisTable.Columns.Add("Name");
            thisTable.Columns.Add("Price");
            thisTable.Columns.Add("Variants");
            thisTable.Columns.Add("Image URI");

            foreach (var item in fromFile["items"])
            {
                //MessageBox.Show((string)item["name"]);
                if ((string)item["kind"] == "M")
                {
                    var row = thisTable.NewRow();
                    row["ID"] = item["id"];
                    row["Name"] = item["name"];
                    row["Price"] = item["price"];
                    string out_variant = string.Empty;
                    foreach (var variant in item["variant"])
                    {
                        out_variant += $"{variant},";
                    }
                    row["Variants"] = out_variant;
                    row["Image URI"] = item["image"];

                    thisTable.Rows.Add(row);
                }

            }

            dataGrid.ItemsSource = thisTable.DefaultView;
        }

        private void ShowWomenData(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = null;
            string MyDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var fromFile = JObject.Parse(File.ReadAllText($"{MyDocumentsPath}/WearAndGoS2/MainDatabase.json"));

            DataTable thisTable = new DataTable();
            thisTable.Columns.Add("ID");
            thisTable.Columns.Add("Name");
            thisTable.Columns.Add("Price");
            thisTable.Columns.Add("Variants");
            thisTable.Columns.Add("Image URI");

            foreach (var item in fromFile["items"])
            {
                //MessageBox.Show((string)item["name"]);
                if ((string)item["kind"] == "W")
                {
                    var row = thisTable.NewRow();
                    row["ID"] = item["id"];
                    row["Name"] = item["name"];
                    row["Price"] = item["price"];
                    string out_variant = string.Empty;
                    foreach (var variant in item["variant"])
                    {
                        out_variant += $"{variant},";
                    }
                    row["Variants"] = out_variant;
                    row["Image URI"] = item["image"];

                    thisTable.Rows.Add(row);
                }

            }

            dataGrid.ItemsSource = thisTable.DefaultView;
        }

        private void ShowFootwearData(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = null;
            string MyDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var fromFile = JObject.Parse(File.ReadAllText($"{MyDocumentsPath}/WearAndGoS2/MainDatabase.json"));

            DataTable thisTable = new DataTable();
            thisTable.Columns.Add("ID");
            thisTable.Columns.Add("Name");
            thisTable.Columns.Add("Price");
            thisTable.Columns.Add("Variants");
            thisTable.Columns.Add("Image URI");

            foreach (var item in fromFile["items"])
            {
                //MessageBox.Show((string)item["name"]);
                if ((string)item["kind"] == "F")
                {
                    var row = thisTable.NewRow();
                    row["ID"] = item["id"];
                    row["Name"] = item["name"];
                    row["Price"] = item["price"];
                    string out_variant = string.Empty;
                    foreach (var variant in item["variant"])
                    {
                        out_variant += $"{variant},";
                    }
                    row["Variants"] = out_variant;
                    row["Image URI"] = item["image"];

                    thisTable.Rows.Add(row);
                }

            }

            dataGrid.ItemsSource = thisTable.DefaultView;
        }

        private void PageLoad(object sender, RoutedEventArgs e)
        {
            ShowAllData(null, null);
        }
    }
}

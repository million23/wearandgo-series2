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
using Page = System.Windows.Controls.Page;

namespace WearAndGo_Admin.Views.Dashboard.ProductListingSidepane
{
    /// <summary>
    /// Interaction logic for ProductListingSidepage_RemoveItem.xaml
    /// </summary>
    public partial class ProductListingSidepage_RemoveItem : Page
    {
        public ProductListingSidepage_RemoveItem()
        {
            InitializeComponent();
        }

        private async void RemoveItem(object sender, RoutedEventArgs e)
        {
            if (in_ItemID.Text == string.Empty)
            {
                var dialog = new ContentDialog
                {
                    Title = "Missing Input",
                    Content = "An Item ID must be specified",
                    CloseButtonText = "Ok",
                    DefaultButton = ContentDialogButton.Close
                };

                await dialog.ShowAsync();
            } else
            {
                var dialog = new ContentDialog
                {
                    Title = "Confirm Delete",
                    Content = "Do you want to delete this item? This is not reversible.\n\nPlease refresh the data grid if you do.",
                    CloseButtonText = "No",
                    PrimaryButtonText = "Yes",
                    DefaultButton = ContentDialogButton.Close
                };
                var result = await dialog.ShowAsync();
                if (result == ContentDialogResult.Primary)
                {
                    string MyDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    var fromFile = JObject.Parse(File.ReadAllText($"{MyDocumentsPath}/WearAndGoS2/MainDatabase.json"));
                    var fromArray = JArray.Parse(fromFile["items"].ToString());

                    var index = -1;
                    bool found = false;
                    
                    for (int i = 0; i < fromArray.Count; i++)
                    {
                        if (fromArray[i]["id"].ToString() == in_ItemID.Text)
                        {
                            index = i;
                            found = true;
                        }
                    }

                    if (found)
                    {
                        fromArray.RemoveAt(index);
                        fromFile["items"] = fromArray;
                        File.WriteAllText($"{MyDocumentsPath}/WearAndGoS2/MainDatabase.json", fromFile.ToString());
                    } else
                    {
                        var ErrorDialog = new ContentDialog
                        {
                            Title = "Item Unidentified",
                            Content = "Seems like this item does not exist in the databse.",
                            CloseButtonText = "Ok",
                            DefaultButton = ContentDialogButton.Close
                        };
                        await ErrorDialog.ShowAsync();
                    }

                    in_ItemID.Text = string.Empty;
                }


            }
        }
    }
}

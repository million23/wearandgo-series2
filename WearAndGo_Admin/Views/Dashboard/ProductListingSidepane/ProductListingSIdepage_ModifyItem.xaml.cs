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
    /// Interaction logic for ProductListingSIdepage_ModifyItem.xaml
    /// </summary>
    public partial class ProductListingSIdepage_ModifyItem : Page
    {
        public ProductListingSIdepage_ModifyItem()
        {
            InitializeComponent();
        }

        private async void ModifyItem(object sender, RoutedEventArgs e)
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
                    Content = "Do you want to modify this item? If you left an input blank, its state will not be updated.\n\nPlease refresh the data grid if you do.",
                    CloseButtonText = "No",
                    PrimaryButtonText = "Yes",
                    DefaultButton = ContentDialogButton.Close
                };
                var result = await dialog.ShowAsync();
                if (result == ContentDialogResult.Primary)
                {
                    var found = false;

                    string MyDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    var fromFile = JObject.Parse(File.ReadAllText($"{MyDocumentsPath}/WearAndGoS2/MainDatabase.json"));
                    var fromArray = JArray.Parse(fromFile["items"].ToString());

                    foreach (var item in fromArray)
                    {
                        if (item["id"].ToString() == in_ItemID.Text)
                        {
                            found = true;

                            if (in_imageURI.Text != string.Empty)
                            {
                                item["image"] = in_imageURI.Text;
                            }
                            if (in_type.SelectedIndex != -1)
                            {
                                string type = string.Empty;
                                ComboBoxItem SelectedItemType = (ComboBoxItem)in_type.SelectedItem;
                                switch (SelectedItemType.Content.ToString())
                                {
                                    case "Men":
                                        type = "M";
                                        break;
                                    case "Women":
                                        type = "W";
                                        break;
                                    case "Footwear":
                                        type = "F";
                                        break;
                                }
                                item["kind"] = type;
                            }
                            if (in_itemVariants.Text != string.Empty)
                            {
                                string[] variants = in_itemVariants.Text.Split(',');
                                JArray JVariants = new JArray();
                                foreach (var variant in variants)
                                {
                                    JVariants.Add(variant);
                                }

                                item["variant"] = JVariants;
                            }
                            if (in_name.Text != string.Empty)
                            {
                                item["name"] = in_name.Text;
                            }
                            if (in_price.Text != string.Empty)
                            {
                                item["price"] = float.Parse(in_price.Text);
                            }

                        }
                    }

                    if (found)
                    {
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
                }

                
            }
        }
    }
}

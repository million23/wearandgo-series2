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
    /// Interaction logic for ProductListingSidepage_AddItem.xaml
    /// </summary>
    public partial class ProductListingSidepage_AddItem : Page
    {
        public ProductListingSidepage_AddItem()
        {
            InitializeComponent();
        }

        private string CheckoutID()
        {
            Random random = new Random();
            const string validCharacters = "BCDFGHJKMPQRTVWXYZ2346789";
            return new string(Enumerable.Repeat(validCharacters, 10)
                .Select(thisString => thisString[random.Next(thisString.Length)]).ToArray());
        }

        private void GenerateID(object sender, RoutedEventArgs e)
        {
            in_ItemID.Text = CheckoutID();
        }

        private async void AddItem(object sender, RoutedEventArgs e)
        {
            #region error catcher
            if (
                in_ItemID.Text == string.Empty ||
                in_name.Text == string.Empty ||
                in_type.SelectedIndex == -1 ||
                in_price.Text == string.Empty ||
                in_itemVariants.Text == string.Empty ||
                in_imageURI.Text == string.Empty
                )
            {
                var dialog = new ContentDialog
                {
                    Title = "Missing Input",
                    Content = "Please fill information on all the input fields",
                    CloseButtonText = "Ok",
                    DefaultButton = ContentDialogButton.Close
                };
                await dialog.ShowAsync();
            }
            #endregion
            else
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
                    //MessageBox.Show(SelectedItemType.Content.ToString());


                    string[] variants = in_itemVariants.Text.Split(',');
                    JArray JVariants = new JArray();
                    foreach (var variant in variants)
                    {
                        JVariants.Add(variant);
                    }

                    #region adding new item
                    string MyDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    var fromFile = JObject.Parse(File.ReadAllText($"{MyDocumentsPath}/WearAndGoS2/MainDatabase.json"));

                    var fromdata = JObject.Parse(File.ReadAllText($"{MyDocumentsPath}/WearAndGoS2/MainDatabase.json"));
                    JArray fromArray = JArray.Parse(fromdata["items"].ToString());

                    var NewItem = new JObject();
                    NewItem["id"] = in_ItemID.Text;
                    NewItem["kind"] = type;
                    NewItem["name"] = in_name.Text;
                    NewItem["image"] = in_imageURI.Text;
                    NewItem["price"] = float.Parse(in_price.Text);
                    NewItem["variant"] = JVariants;

                    fromArray.Add(NewItem);
                    fromdata["items"] = fromArray;

                    File.WriteAllText($"{MyDocumentsPath}/WearAndGoS2/MainDatabase.json", fromdata.ToString());

                    //MessageBox.Show(fromArray.ToString());
                    #endregion


                    var confirmation = new ContentDialog
                    {
                        Title = "Item Added",
                        Content = "Please refresh the data grid",
                        CloseButtonText = "Ok",
                        DefaultButton = ContentDialogButton.Close
                    };

                    in_ItemID.Text = string.Empty;
                    in_name.Text = string.Empty;
                    in_type.SelectedIndex = -1;
                    in_price.Text = string.Empty;
                    in_itemVariants.Text = string.Empty;
                    in_imageURI.Text = string.Empty;

                    await confirmation.ShowAsync();
                }


                
            }


            


        }
    }
}

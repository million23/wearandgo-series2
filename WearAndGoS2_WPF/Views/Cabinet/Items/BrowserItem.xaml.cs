using ModernWpf.Controls;
using Newtonsoft.Json;
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
using System.Xml;
using Flurl.Http;

namespace WearAndGoS2_WPF.Views.Cabinet.Items
{
    /// <summary>
    /// Interaction logic for BrowserItem.xaml
    /// </summary>
    public partial class BrowserItem : UserControl
    {
        public BrowserItem()
        {
            InitializeComponent();
        }
        public string ProductName { get; set; }
        public string ImageURI { get; set; }
        public string ID { get; set; }
        public string Type { get; set; }
        public float Price { get; set; }

        private void ItemLoaded(object sender, RoutedEventArgs e)
        {

        }

        private async void AddToCart(object sender, RoutedEventArgs e)
        {
            var dialog = new ContentDialog()
            {
                Title = "Confirmation",
                Content = $"Do you want to add {prod_name.Text} to your cart?",
                CloseButtonText = "No",
                PrimaryButtonText = "Yes"
            };

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                if (VariantBox.Text != "")
                {
                    Models.UserCartContent.ClientList = JArray.Parse(File.ReadAllText("./Data/CartData.json"));

                    for (int i = 0; i < Models.UserCartContent.ClientList.Count; i++)
                    {
                        if (Models.UserCartContent.ClientList[i]["Owner"].ToString() == Properties.Settings.Default.UserEmail)
                        {
                            Models.UserCartContent.ClientList[i].Remove();
                        }
                    }

                    var jsonInput = new JObject();
                    jsonInput["ID"] = prod_id.Text.ToUpper();
                    jsonInput["Name"] = prod_name.Text;
                    jsonInput["Variant"] = VariantBox.Text;
                    jsonInput["Price"] = Price.ToString();
                    jsonInput["Type"] = Type;

            
                    Models.UserCartContent.CartContent.Add(jsonInput);
                    Models.UserCartContent.MainUser["Owner"] = Properties.Settings.Default.UserEmail;
                    Models.UserCartContent.MainUser["Cart"] = Models.UserCartContent.CartContent;
                    Models.UserCartContent.ClientList.Add(Models.UserCartContent.MainUser);

                    File.WriteAllText("./Data/CartData.json", Models.UserCartContent.ClientList.ToString());

                } else
                {
                    var errdialog = new ContentDialog()
                    {
                        Title = "Something went wrong",
                        Content = $"Please choose a Variant first",
                        CloseButtonText = "Ok"
                    };
                    await errdialog.ShowAsync();
                }

            }

        }
    }
}

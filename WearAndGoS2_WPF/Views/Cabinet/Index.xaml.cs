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
using Flurl.Http;
using ModernWpf;
using Newtonsoft.Json.Linq;

namespace WearAndGoS2_WPF.Views.Cabinet
{
    /// <summary>
    /// Interaction logic for Index.xaml
    /// </summary>
    public partial class Index : Page
    {
        public Index()
        {
            InitializeComponent();
        }

        private async void ChangePane(ModernWpf.Controls.NavigationView sender, ModernWpf.Controls.NavigationViewItemInvokedEventArgs args)
        {
            //MessageBox.Show(args.InvokedItemContainer.Tag.ToString());
            switch (args.InvokedItemContainer.Tag.ToString())
            {
                case "Browser":
                    CabinetFrame.Navigate(ViewFrames._CabinetBrowse);
                    await Task.Delay(300);
                    FrameHeader.Content = "Browse to our community-driven content";
                    break;     
                case "Cart":
                    CabinetFrame.Navigate(ViewFrames._CabinetCart);
                    await Task.Delay(300);
                    FrameHeader.Content = "Your handpicked content";
                    break;     
                case "Account":
                    CabinetFrame.Navigate(ViewFrames._CabinetAccount);
                    await Task.Delay(300);
                    FrameHeader.Content = "Your safe information";
                    break;    
                case "Help":
                    CabinetFrame.Navigate(ViewFrames._CabinetHelp);
                    await Task.Delay(300);
                    FrameHeader.Content = "Your concerns";
                    break;
                case "About":
                    CabinetFrame.Navigate(ViewFrames._CabinetAbout);
                    await Task.Delay(300);
                    FrameHeader.Content = "About the Developers";
                    break;
            }
        }

        private async void PageLoaded(object sender, RoutedEventArgs e)
        {
            ViewFrames._CabinetBrowse.BrowseFrame.Content = ViewFrames._CabinetBrowseMen;
            CabinetFrame.Navigate(ViewFrames._CabinetAccount);
            await Task.Delay(300);
            FrameHeader.Content = "About the Developers";
            header_email.Content = Properties.Settings.Default.UserEmail;
            GetItems();

            // get user content

            Models.UserCartContent.ClientList = JArray.Parse(File.ReadAllText("./Data/CartData.json"));
            foreach (var item in Models.UserCartContent.ClientList)
            {
                if (item["Owner"].ToString() == Properties.Settings.Default.UserEmail)
                {
                    Models.UserCartContent.CartContent = JArray.Parse(item["Cart"].ToString());
                    Models.UserCartContent.MainUser = JObject.Parse(item.ToString());
                }

            }
        }
        public async void GetItems()
        {

            string url = "https://api.npoint.io/64f6f7ad3975e962ffc9";
            //string url = "https://api.jsonbin.io/b/60b4b1f7b104de5acdda6d9e/latest";
            // clear browser UI
            ViewFrames._CabinetBrowseMen.ItemPanel.Children.Clear();
            ViewFrames._CabinetBrowseWomen.ItemPanel.Children.Clear();
            ViewFrames._CabinetBrowseFootwear.ItemPanel.Children.Clear();

            // load database
            //await url.WithHeader("X-Master-Key", "$2b$10$TmKEJtRFh2jvh.gOZfI6c.FQosCyM2L6U88iqYEGB6LBYbTB55X5.").GetJsonAsync();
            //dynamic result = await url.WithHeaders(new { X_Master_Key = "$2b$10$fTXTmJDIu2hwzf.B/X486u2ACAj.T4zCm2GxAh4z.0MTZ7ViNrn0S", Accept = "text/json" }).GetJsonAsync();
            string MyDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string result = await url.GetStringAsync();

            JObject itemsDatabase = JObject.Parse(File.ReadAllText($"{MyDocumentsPath}/WearAndGoS2/MainDatabase.json"));
            for (int i = 0; i < itemsDatabase["items"].Count(); i++)
            {
                //MessageBox.Show((string)itemsDatabase["items"][i]["variant"].Count().ToString());
                if ((string)itemsDatabase["items"][i]["kind"] == "M")
                {
                    var variants = itemsDatabase["items"][i]["variant"];
                    var browseritem = new Items.BrowserItem();

                    browseritem.prod_image.Source = new BitmapImage(new System.Uri((string)itemsDatabase["items"][i]["image"]));
                    browseritem.prod_name.Text = (string)itemsDatabase["items"][i]["name"];
                    browseritem.prod_id.Text = (string)itemsDatabase["items"][i]["id"];
                    browseritem.ID = (string)itemsDatabase["items"][i]["id"];
                    browseritem.prod_price.Text = $"₱ {itemsDatabase["items"][i]["price"].ToString()}";
                    browseritem.Price = (float)itemsDatabase["items"][i]["price"];
                    browseritem.Type = (string)itemsDatabase["items"][i]["kind"];

                    browseritem.VariantBox.Items.Clear();
                    foreach (var item in variants)
                    {
                        //MessageBox.Show(item.ToString());
                        var comboboxitem = new ComboBoxItem() { Content = item.ToString().ToUpper() };
                        browseritem.VariantBox.Items.Add(comboboxitem);
                    }


                    // append to browser
                    ViewFrames._CabinetBrowseMen.ItemPanel.Children.Add(browseritem);

                }
                if ((string)itemsDatabase["items"][i]["kind"] == "W")
                {
                    var variants = itemsDatabase["items"][i]["variant"];
                    var browseritem = new Cabinet.Items.BrowserItem();

                    browseritem.prod_image.Source = new BitmapImage(new System.Uri((string)itemsDatabase["items"][i]["image"]));
                    browseritem.prod_name.Text = (string)itemsDatabase["items"][i]["name"];
                    browseritem.prod_id.Text = (string)itemsDatabase["items"][i]["id"];
                    browseritem.ID = (string)itemsDatabase["items"][i]["id"];
                    browseritem.prod_price.Text = $"₱ {itemsDatabase["items"][i]["price"].ToString()}";
                    browseritem.Price = (float)itemsDatabase["items"][i]["price"];
                    browseritem.Type = (string)itemsDatabase["items"][i]["kind"];

                    browseritem.VariantBox.Items.Clear();
                    foreach (var item in variants)
                    {
                        //MessageBox.Show(item.ToString());
                        var comboboxitem = new ComboBoxItem() { Content = item.ToString().ToUpper() };
                        browseritem.VariantBox.Items.Add(comboboxitem);
                    }


                    // append to browser
                    ViewFrames._CabinetBrowseWomen.ItemPanel.Children.Add(browseritem);
                }
                if ((string)itemsDatabase["items"][i]["kind"] == "F")
                {
                    var variants = itemsDatabase["items"][i]["variant"];
                    var browseritem = new Cabinet.Items.BrowserItem();

                    browseritem.prod_image.Source = new BitmapImage(new System.Uri((string)itemsDatabase["items"][i]["image"]));
                    browseritem.prod_name.Text = (string)itemsDatabase["items"][i]["name"];
                    browseritem.prod_id.Text = (string)itemsDatabase["items"][i]["id"];
                    browseritem.ID = (string)itemsDatabase["items"][i]["id"];
                    browseritem.prod_price.Text = $"₱ {itemsDatabase["items"][i]["price"].ToString()}";
                    browseritem.Price = (float)itemsDatabase["items"][i]["price"];
                    browseritem.Type = (string)itemsDatabase["items"][i]["kind"];

                    browseritem.VariantBox.Items.Clear();
                    foreach (var item in variants)
                    {
                        //MessageBox.Show(item.ToString());
                        var comboboxitem = new ComboBoxItem() { Content = item.ToString().ToUpper() };
                        browseritem.VariantBox.Items.Add(comboboxitem);
                    }


                    // append to browser
                    ViewFrames._CabinetBrowseFootwear.ItemPanel.Children.Add(browseritem);
                }
            }

        }
    }
}

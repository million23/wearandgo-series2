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
using Path = System.IO.Path;

namespace WearAndGoS2_WPF.Views.Cabinet
{
    /// <summary>
    /// Interaction logic for Cart.xaml
    /// </summary>
    public partial class Cart : System.Windows.Controls.Page
    {
        public Cart()
        {
            InitializeComponent();
        }


        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            UpdateCartList();   
        }

        public void UpdateCartList()
        {
            float total = 0;
            int cartindex = 0;
            CartList.Children.Clear();
            if (Models.UserCartContent.CartContent.Count < 1)
            {
                var text = new TextBlock
                {
                    Text = "Seems like its empty",
                    FontSize = 32
                };
                CartList.Children.Add(text);
                ui_totalSum.Text = "Cart is empty right now";
            } else
            {

                foreach (var item in Models.UserCartContent.CartContent)
                {
                    var cartitem = new Items.CartItem();
                    CartList.Children.Add(cartitem);

                    cartitem.Index = cartindex;
                    cartindex++;

                    total += float.Parse(item["Price"].ToString());

                    cartitem.out_prodID.Text = item["ID"].ToString();
                    cartitem.out_prodName.Text = item["Name"].ToString();
                    cartitem.out_prodPrice.Text = $"P {item["Price"].ToString()}";
                    cartitem.out_prodVariant.Text = item["Variant"].ToString();
                    cartitem.out_image.Source = new BitmapImage(new Uri(item["Image"].ToString()));

                    ui_totalSum.Text = $"₱ {total}";
                }
            }
        }

        private async void ProceedToCounter(object sender, RoutedEventArgs e)
        {
            if (Models.UserCartContent.CartContent.Count > 0)
            {
                var dialog = new ModernWpf.Controls.ContentDialog
                {
                    Title = "Checkout Confirmation",
                    Content = "Do you want to check out?\n\nAll of your content from the cart will be removed and you will be logged out. This is not reversible. Do you wish to proceed?",
                    PrimaryButtonText = "Yes",
                    CloseButtonText = "No",
                    DefaultButton = ModernWpf.Controls.ContentDialogButton.Close
                };

                var result = await dialog.ShowAsync();

                if (result == ModernWpf.Controls.ContentDialogResult.Primary)
                {
                    float total = 0;
                    JArray items = new JArray();

                    foreach (var item in Models.UserCartContent.ClientList)
                    {
                        if (item["Owner"].ToString() == Properties.Settings.Default.UserEmail)
                        {
                            foreach (var item1 in item["Cart"])
                            {
                                var thisItem = new JObject();
                                total += float.Parse(item1["Price"].ToString());
                                thisItem["ID"] = item1["ID"];
                                thisItem["Name"] = item1["Name"];
                                thisItem["Price"] = item1["Price"];
                                thisItem["Variant"] = item1["Variant"];
                                items.Add(thisItem);
                            }
                        }
                    }
                    var fromData = JObject.Parse(File.ReadAllText("./Data/TransactionHistory.json"));
                    var history = new JArray();
                    history = (JArray)fromData["History"];

                    var jsonInput = new JObject();
                    jsonInput["TransactionID"] = CheckoutID();
                    jsonInput["Date"] = DateTime.Now.ToString("MM/dd/yyyy@HH:mm:ss");
                    jsonInput["Total"] = total.ToString();
                    jsonInput["User"] = Properties.Settings.Default.UserEmail;
                    jsonInput["ItemList"] = items;

                    history.Add(jsonInput);

                    string MyDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                    //MessageBox.Show(MyDocumentsPath);

                    File.WriteAllText("./Data/TransactionHistory.json", fromData.ToString());
                    File.WriteAllText($"{MyDocumentsPath}/WearAndGoS2/TransactionHistory.json", fromData.ToString());

                    // show transaction ID for cashout
                    var transactionOut = new ContentDialog
                    {
                        Title = $"{jsonInput["TransactionID"].ToString()}",
                        Content = "Here is your Transaction ID.\n\nPlease keep this ID to verify in checkout",
                        CloseButtonText = "Close and Log Out"
                    };
                    await transactionOut.ShowAsync();

                    // cache clear and user log out
                    ClearTrace();
                }
                
            }
            else
            {
                var dialog = new ModernWpf.Controls.ContentDialog
                {
                    Title = "Cannot proceed to checkout",
                    Content = "Looks like your cart is empty, fill it up before you proceed to checkout",
                    CloseButtonText = "Ok"
                };
                await dialog.ShowAsync();
            }
        }

        private string CheckoutID()
        {
            Random random = new Random();
            const string validCharacters = "BCDFGHJKLMNPQRSTVWXYZ0123456789";
            return new string(Enumerable.Repeat(validCharacters, 20)
                .Select(thisString => thisString[random.Next(thisString.Length)]).ToArray());
        }

        private async void RemoveAllItems(object sender, RoutedEventArgs e)
        {
            if (Models.UserCartContent.CartContent.Count > 0)
            {
                var dialog = new ModernWpf.Controls.ContentDialog
                {
                    Title = "Removal Confirmation",
                    Content = "Do you want to remove all of the cart's content? This is not reversible.",
                    PrimaryButtonText = "Yes",
                    CloseButtonText = "No",
                    DefaultButton = ModernWpf.Controls.ContentDialogButton.Close
                };

                var result = await dialog.ShowAsync();

                if (result == ModernWpf.Controls.ContentDialogResult.Primary)
                {
                    Models.UserCartContent.CartContent.RemoveAll();
                    Models.UserCartContent.MainUser["Cart"] = Models.UserCartContent.CartContent;

                    foreach (var item in Models.UserCartContent.ClientList)
                    {
                        if (item["Owner"].ToString() == Properties.Settings.Default.UserEmail)
                        {
                            item["Cart"] = Models.UserCartContent.CartContent;
                            File.WriteAllText("./Data/CartData.json", Models.UserCartContent.ClientList.ToString());
                        }
                    }

                    UpdateCartList();
                }

                
            } else
            {
                var dialog = new ModernWpf.Controls.ContentDialog
                {
                    Title = "Cannot remove cart items",
                    Content = "Looks like your cart is empty, fill it up before you can clear this out",
                    CloseButtonText = "Ok"
                };
                await dialog.ShowAsync();
            }
        }

        private async void ClearTrace()
        {
            Models.UserCartContent.CartContent.RemoveAll();
            Models.UserCartContent.MainUser["Cart"] = Models.UserCartContent.CartContent;

            foreach (var item in Models.UserCartContent.ClientList)
            {
                if (item["Owner"].ToString() == Properties.Settings.Default.UserEmail)
                {
                    item["Cart"] = Models.UserCartContent.CartContent;
                    File.WriteAllText("./Data/CartData.json", Models.UserCartContent.ClientList.ToString());
                }
            }

            Models.UserCartContent.MainUser.RemoveAll();

            UpdateCartList();

            Properties.Settings.Default.UserEmail = "";
            Properties.Settings.Default.Save();
            await Controllers.Auth.Client.LogoutAsync();
            Application.Current.MainWindow.Content = ViewFrames._LandingPage;

        }
    }
}

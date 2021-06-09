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

namespace WearAndGoS2_WPF.Views.Cabinet.Items
{
    /// <summary>
    /// Interaction logic for CartItem.xaml
    /// </summary>
    public partial class CartItem : UserControl
    {
        public CartItem()
        {
            InitializeComponent();
        }
        public int Index { get; set; }

        private async void RemoveItem(object sender, RoutedEventArgs e)
        {
            var dialog = new ModernWpf.Controls.ContentDialog
            {
                Title = "Confirm Remove",
                Content = "Do you want to remove this item?",
                PrimaryButtonText = "Yes",
                CloseButtonText = "No",
                DefaultButton = ModernWpf.Controls.ContentDialogButton.Close
            };

            var result = await dialog.ShowAsync();
            if (result == ModernWpf.Controls.ContentDialogResult.Primary)
            {

                Models.UserCartContent.ClientList = JArray.Parse(File.ReadAllText("./Data/CartData.json"));
                Models.UserCartContent.CartContent[Index].Remove();
                Models.UserCartContent.MainUser["Cart"] = Models.UserCartContent.CartContent;

                foreach (var item in Models.UserCartContent.ClientList)
                {
                    if (item["Owner"].ToString() == Properties.Settings.Default.UserEmail)
                    {
                        item["Cart"] = Models.UserCartContent.CartContent;
                        File.WriteAllText("./Data/CartData.json", Models.UserCartContent.ClientList.ToString());
                    }
                }


                ViewFrames._CabinetCart.UpdateCartList();
            }

        }
    }
}

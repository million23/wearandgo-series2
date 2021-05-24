using System;
using System.Collections.Generic;
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

namespace WearAndGoS2_WPF.Views.Cabinet
{
    /// <summary>
    /// Interaction logic for Cart.xaml
    /// </summary>
    public partial class Cart : Page
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
            int cartindex = 0;
            CartList.Children.Clear();
            foreach (var item in Models.UserCartContent.CartContent)
            {
                var cartitem = new Items.CartItem();
                CartList.Children.Add(cartitem);

                cartitem.Index = cartindex;
                cartindex++;

                cartitem.out_prodID.Text = item["ID"].ToString();
                cartitem.out_prodName.Text = item["Name"].ToString();
                cartitem.out_prodPrice.Text = $"P {item["Price"].ToString()}";
                cartitem.out_prodVariant.Text = item["Variant"].ToString();
            }
        }

    }
}

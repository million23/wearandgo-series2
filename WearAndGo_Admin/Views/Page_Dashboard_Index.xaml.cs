using Newtonsoft.Json.Linq;
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

namespace WearAndGo_Admin.Views
{
    /// <summary>
    /// Interaction logic for Page_Dashboard_Index.xaml
    /// </summary>
    public partial class Page_Dashboard_Index : Page
    {
        public Page_Dashboard_Index()
        {
            InitializeComponent();
        }

        public void PageLoad(object sender, RoutedEventArgs e)
        {
            DashboardFrame.Navigate(Models.DashboardFrames._ProductListing);
        }

        private void ChangePanes(ModernWpf.Controls.NavigationView sender, ModernWpf.Controls.NavigationViewItemInvokedEventArgs args)
        {
            switch (args.InvokedItemContainer.Content)
            {
                case "Home":
                    DashboardFrame.Navigate(Models.DashboardFrames._Index);
                    break;
                case "Access Accounts":
                    DashboardFrame.Navigate(Models.DashboardFrames._Accounts);
                    break;
                case "Product Listing":
                    DashboardFrame.Navigate(Models.DashboardFrames._ProductListing);
                    break;
                case "Transaction History":
                    DashboardFrame.Navigate(Models.DashboardFrames._TransactionHistory);
                    break;
                default:
                    DashboardFrame.Navigate(Models.DashboardFrames._Index);
                    break;
            }
        }
    }
}

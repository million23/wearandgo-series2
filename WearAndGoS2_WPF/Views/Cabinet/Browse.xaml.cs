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
using Flurl.Http;
using Newtonsoft.Json.Linq;

namespace WearAndGoS2_WPF.Views.Cabinet
{
    /// <summary>
    /// Interaction logic for Browse.xaml
    /// </summary>
    public partial class Browse : Page
    {
        public Browse()
        {
            InitializeComponent();
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
        }

        private void ChangePane(ModernWpf.Controls.NavigationView sender, ModernWpf.Controls.NavigationViewItemInvokedEventArgs args)
        {
            switch (args.InvokedItemContainer.Tag.ToString())
            {
                case "Men":
                    BrowseFrame.Content = ViewFrames._CabinetBrowseMen;
                    break;
                case "Women":
                    BrowseFrame.Content = ViewFrames._CabinetBrowseWomen;
                    break;
                case "Footwear":
                    BrowseFrame.Content = ViewFrames._CabinetBrowseFootwear;
                    break;
                default:
                    break;
            }
        }
    }
}

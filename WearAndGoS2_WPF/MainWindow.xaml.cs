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
using WearAndGoS2_WPF.Views;

namespace WearAndGoS2_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            MainContent.Content = (ViewFrames._LoadingPage);

            string MyDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (!Directory.Exists($"{MyDocumentsPath}/WearAndGoS2/"))
            {
                Directory.CreateDirectory($"{MyDocumentsPath}/WearAndGoS2");

                if (!File.Exists($"{MyDocumentsPath}/WearAndGoS2/TransactionHistory.json"))
                {
                    File.Create($"{MyDocumentsPath}/WearAndGoS2/TransactionHistory.json");
                }
            } 
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WearAndGoS2_WPF.Views;
using Auth0.OidcClient;

namespace WearAndGoS2_WPF.Views
{
    class ViewFrames
    {

        // views
        public static LoadingPage _LoadingPage = new LoadingPage();
        public static LandingPage _LandingPage = new LandingPage();

        // cabinet root
        public static Cabinet.Index _CabinetIndex = new Cabinet.Index();
        public static Cabinet.About _CabinetAbout = new Cabinet.About();
        public static Cabinet.Help _CabinetHelp = new Cabinet.Help();
        public static Cabinet.Cart _CabinetCart = new Cabinet.Cart();
        public static Cabinet.Account _CabinetAccount = new Cabinet.Account();
        public static Cabinet.Browse _CabinetBrowse = new Cabinet.Browse();
        
        // browse views
        public static Cabinet.Browse_Men _CabinetBrowseMen = new Cabinet.Browse_Men();
        public static Cabinet.Browse_Women _CabinetBrowseWomen = new Cabinet.Browse_Women();
        public static Cabinet.Browse_Footwear _CabinetBrowseFootwear = new Cabinet.Browse_Footwear();

    }
}

﻿using System.Windows.Controls;

namespace WearAndGo_Admin.Models
{
    class ViewFrames
    {
        public static Page _View_LoadingPage = new Views.Page_Loading();
        public static Page _View_Intro = new Views.Page_Intro();
        public static Page _View_Dashboard_Index = new Views.Page_Dashboard_Index();
    }
    class DashboardFrames
    {
        public static Page _Index = new Views.Dashboard.Index();
        public static Page _Accounts = new Views.Dashboard.Accounts();
        public static Page _ProductListing = new Views.Dashboard.ProductListing();
        public static Page _TransactionHistory = new Views.Dashboard.TransactionHistory();

    }
}
using ModernWpf.Controls;
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
using WearAndGoS2_WPF.Controllers;
using Microsoft.Identity.Client;
using Flurl.Http;
using Newtonsoft.Json.Linq;
using System.IO;

namespace WearAndGoS2_WPF.Views
{
    /// <summary>
    /// Interaction logic for LandingPage.xaml
    /// </summary>
    public partial class LandingPage : System.Windows.Controls.Page
    {
        public LandingPage()
        {
            InitializeComponent();
        }

        private async void LogIn(object sender, RoutedEventArgs e)
        {
            LogInButtonUI.Visibility = Visibility.Collapsed;
            LoadingUI.Visibility = Visibility.Visible;

            try
            {

                if (Properties.Settings.Default.UserEmail != string.Empty)
                {

                    await Task.Delay(1000);
                    LogInButtonUI.Visibility = Visibility.Visible;
                    LoadingUI.Visibility = Visibility.Collapsed;
                    Application.Current.MainWindow.Content = ViewFrames._CabinetIndex;
                }
                else
                {
                    #region MSAL
                    //var result = await Auth.App.AcquireTokenInteractive(Auth.Scopes).ExecuteAsync();
                    //// Auth.Credentials.PostLogoutRedirectUri = Auth.Credentials.RedirectUri;
                    //// var result = await Auth.Client.LoginAsync();

                    //string json = await "https://graph.microsoft.com/beta/me"
                    //    .WithOAuthBearerToken(result.AccessToken)
                    //    .GetStringAsync();

                    //JObject userobject = JObject.Parse(json);

                    //Properties.Settings.Default.UserObject = json;
                    //Properties.Settings.Default.UserID = (string)userobject["id"];
                    //Properties.Settings.Default.UserFullName = (string)userobject["displayName"];

                    //Properties.Settings.Default.Save();


                    //await Task.Delay(1000);
                    //LogInButtonUI.Visibility = Visibility.Visible;
                    //LoadingUI.Visibility = Visibility.Collapsed;
                    //Application.Current.MainWindow.Content = ViewFrames._CabinetIndex;

                    #endregion

                    #region auth0
                    var result = await Auth.Client.LoginAsync();
                    if (!result.IsError)
                    {
                        foreach (var item in result.User.Claims)
                        {
                            switch (item.Type)
                            {
                                case "name":
                                    Properties.Settings.Default.UserEmail = item.Value;
                                    break;
                                case "nickname":
                                    Properties.Settings.Default.UserFullName = item.Value;
                                    break;
                                default:
                                    break;
                            }
                        }
                        Properties.Settings.Default.Save();
                        //MessageBox.Show(Properties.Settings.Default.UserEmail);
                        //MessageBox.Show(Properties.Settings.Default.UserFullName);
                        //await Auth.Client.LogoutAsync();

                        await Task.Delay(1000);
                        LogInButtonUI.Visibility = Visibility.Visible;
                        LoadingUI.Visibility = Visibility.Collapsed;
                        Application.Current.MainWindow.Content = ViewFrames._CabinetIndex;
                    } else
                    {
                        var dialog = new ContentDialog
                        {
                            Title = "Oops!",
                            Content = $"Something went wrong. Try again.",
                            CloseButtonText = "Retry",
                            DefaultButton = ContentDialogButton.Close
                        };
                        await dialog.ShowAsync();
                        await Task.Delay(1000);
                        LogInButtonUI.Visibility = Visibility.Visible;
                        LoadingUI.Visibility = Visibility.Collapsed;
                    }

                    #endregion

                }



                //if (result)
                //{
                //    #region dialog
                //    var dialog = new ContentDialog
                //    {
                //        Title = "Oops!",
                //        Content = $"Something went wrong. Try again.",
                //        CloseButtonText = "Retry",
                //        DefaultButton = ContentDialogButton.Close
                //    };
                //    await dialog.ShowAsync();
                //    #endregion
                //}
                //else
                //{

                //    MessageBox.Show(result.User.FindFirst(c => c.Type == "email")?.Value);
                //}

            }
            catch (Exception error)
            {
                #region dialog
                var dialog = new ContentDialog
                {
                    Title = "Oops!",
                    Content = $"Something went wrong\n\n{error.Message}",
                    CloseButtonText = "Retry",
                    DefaultButton = ContentDialogButton.Close
                };
                await dialog.ShowAsync();
                LogInButtonUI.Visibility = Visibility.Visible;
                LoadingUI.Visibility = Visibility.Collapsed;
                #endregion
            }
        }

        private void PageLoad(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.UserEmail != string.Empty)
            {
                LogIn(null, null);
            }
        }
    }
}

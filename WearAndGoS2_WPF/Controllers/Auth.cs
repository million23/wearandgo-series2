using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auth0.OidcClient;
using Microsoft.Identity.Client;

namespace WearAndGoS2_WPF.Controllers
{
    class Auth
    {
        // auth client
        public static Auth0ClientOptions Credentials = new Auth0ClientOptions
        {
            Domain = "millioncreations.us.auth0.com",
            ClientId = "ZthoxzWhVnDLlMwmyUGZTc8QWKot0b5n"
        };
        public static Auth0Client Client = new Auth0Client(Credentials);

        // MS Auth Client
        public static string[] Scopes = new string[]
                    {
                        "https://graph.microsoft.com/user.read",
                    };
        private static string clientID = "e2cc6241-6006-40b7-aa30-1ff15656c583";
        public static IPublicClientApplication App = PublicClientApplicationBuilder.Create(clientID)
                        .WithDefaultRedirectUri()
                        .Build();


    }
}

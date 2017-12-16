using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Google.Apis.Services;
using System.Security.Cryptography.X509Certificates;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Analytics.v3;
using System.Threading;
using Google.Apis.Util.Store;
using Google.Apis.Analytics.v3.Data;
using System.IO;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            string[] scopes = new string[] { AnalyticsService.Scope.AnalyticsReadonly }; // view and manage your Google Analytics data
          
              var clientId = "417254395222-98f1k06rrg29uo7kp09jhrmi04fsqs0f.apps.googleusercontent.com";      // From https://console.developers.google.com
            var clientSecret = "U14XxYtUf7Eb_WNKPMknT5BU";          // From https://console.developers.google.com
                                               // here is where we Request the user to give us access, or use the Refresh Token that was previously stored in %AppData%
            //var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(new ClientSecrets
            //{
            //    ClientId = clientId,
            //    ClientSecret = clientSecret
            //},
            //                                                            scopes,
            //                                                            Environment.UserName,
            //                                                            CancellationToken.None,
            //                                                            new FileDataStore("Daimto.GoogleAnalytics.Auth.Store")).Result;

            UserCredential credential;
            using (var stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read))
            {
                credential = await  GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                   scopes,
                    "user", CancellationToken.None, new FileDataStore("Books.ListMyLibrary"));
            }

            //// Create the service.
            //var service = new BooksService(new BaseClientService.Initializer()
            //{
            //    HttpClientInitializer = credential,
            //    ApplicationName = "Books API Sample",
            //});



            var service = new AnalyticsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Analytics API Sample",
            });

            ManagementResource.ProfilesResource.ListRequest list = service.Management.Profiles.List("41801427", "UA-41801427-3");
            Profiles feed = list.Execute();

            // service.ApiKey = "417254395222-98f1k06rrg29uo7kp09jhrmi04fsqs0f.apps.googleusercontent.com";
            //var service = new DiscoveryService(new BaseClientService.Initializer
            //{
            //    ApplicationName = "Discovery Sample",
            //    ApiKey = "[YOUR_API_KEY_HERE]",
            //});
        }
    }
}

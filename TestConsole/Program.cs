using Google.Apis.AnalyticsReporting.v4;
using Google.Apis.AnalyticsReporting.v4.Data;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace TestConsole
{
    class Program
    {
     
        static void Main(string[] args)
        {

            testGAnalytics();


        }
        public static void testGAnalytics()
        {

            var clientId = "417254395222-98f1k06rrg29uo7kp09jhrmi04fsqs0f.apps.googleusercontent.com";      // From https://console.developers.google.com
            var clientSecret = "U14XxYtUf7Eb_WNKPMknT5BU";
            var apiKey = "AIzaSyCl3-DesUv835tvrVJLJOqodkORN1zBURk";
        
            string[] scopes = new string[] { AnalyticsReportingService.Scope.AnalyticsReadonly }; // view and manage your Google Analytics data
            var service = GoogleSamplecSharpSample.Analyticsreportingv4.Auth.Oauth2Example.GetAnalyticsreportingService("client_secrets.json","tikasa", scopes);
            var dateRange = new DateRange { StartDate = "2017-07-01", EndDate = "2017-07-10" };

            // Create the Metrics object.
            var sessions = new Metric { Expression = "ga:sessions", Alias = "Sessions" };

            //Create the Dimensions object.
            var browser = new Dimension { Name = "ga:browser" };


            // Create the ReportRequest object.
            var reportRequest = new ReportRequest
            {
                ViewId = "84137773",
                DateRanges = new List<DateRange> { dateRange },
                Dimensions = new List<Dimension> { browser },
                Metrics = new List<Metric> { sessions }
            };

            var requests = new List<ReportRequest> { reportRequest };

            // Create the GetReportsRequest object.
            var getReport = new GetReportsRequest { ReportRequests = requests };

          var a=  service.Reports.BatchGet(getReport);
        //var keyFilePath = AppDomain.CurrentDomain.BaseDirectory+ "tikasaApp-d6cf29115e9c.p12";    // Downloaded from https://console.developers.google.com
        //var serviceAccountEmail = "haichay88@gmail.com";  // found https://console.developers.google.com

        ////loading the Key file
        //var certificate = new X509Certificate2(keyFilePath, "notasecret", X509KeyStorageFlags.Exportable);
        //var credential = new ServiceAccountCredential(new ServiceAccountCredential.Initializer(serviceAccountEmail)
        //{
        //    Scopes = scopes
        //}.FromCertificate(certificate));


        //UserCredential credential;
        //    using (var stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read))
        //    {
        //        credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
        //            GoogleClientSecrets.Load(stream).Secrets,
        //           scopes,
        //            "user", CancellationToken.None, new FileDataStore("Books.ListMyLibrary"));
        //    }

            //// Create the service.
            //var service = new BooksService(new BaseClientService.Initializer()
            //{
            //    HttpClientInitializer = credential,
            //    ApplicationName = "Books API Sample",
            //});



            //var service = new AnalyticsReportingService(new BaseClientService.Initializer
            //{
            //    ApplicationName = "GAReportDownloader",
            //    HttpClientInitializer = credential
            //});
           
            // Content here
            //Console.WriteLine("Hello from Google Analytics. Starting..");

            //// Create the DateRange object.
            //var dateRange = new DateRange { StartDate = "2017-07-01", EndDate = "2017-07-10" };

            //// Create the Metrics object.
            //var sessions = new Metric { Expression = "ga:sessions", Alias = "Sessions" };

            ////Create the Dimensions object.
            //var browser = new Dimension { Name = "ga:browser" };

          
            //// Create the ReportRequest object.
            //var reportRequest = new ReportRequest
            //{
            //    ViewId = "84137773",
            //    DateRanges = new List<DateRange> { dateRange },
            //    Dimensions = new List<Dimension> { browser },
            //    Metrics = new List<Metric> { sessions }
            //};

            //var requests = new List<ReportRequest> { reportRequest };

            //// Create the GetReportsRequest object.
            //var getReport = new GetReportsRequest { ReportRequests = requests };

            //// Call the batchGet method.
            //var response = service.Reports.BatchGet(getReport).Execute();

            Console.WriteLine();

        }
       
        static async Task<UserCredential> GetCredential()
        {
            var clientId = "417254395222-98f1k06rrg29uo7kp09jhrmi04fsqs0f.apps.googleusercontent.com";      // From https://console.developers.google.com
            var clientSecret = "U14XxYtUf7Eb_WNKPMknT5BU";

            var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
              new ClientSecrets
              {
                  ClientId = clientId,
                  ClientSecret = clientSecret
              },
              new[] { AnalyticsReportingService.Scope.AnalyticsReadonly },
              "user",
              CancellationToken.None);
            return  credential;
        }

        public void gettoken()
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("client_id", "417254395222-o9cpa9im295dcvg8eun6vs07ecvi48us.apps.googleusercontent.com");
            parameters.Add("client_secret", "TYXxl0UP_YJPigEU4oh99Ajt");
            parameters.Add("grant_type", "authorization_code");
            parameters.Add("redirect_uri", "http://localhost:34962/Home/Auth");
           

            WebClient client = new WebClient();
            var result = client.UploadValues("https://api.instagram.com/oauth/access_token", parameters);

            var response = System.Text.Encoding.Default.GetString(result);

        }
        public static string ReceiveTokenGmail(string code, string GoogleWebAppClientID, string GoogleWebAppClientSecret, string RedirectUrl)
        {
            string postString = "&client_id=" + GoogleWebAppClientID + @"&client_secret=" + GoogleWebAppClientSecret + "&redirect_uri=" + RedirectUrl+ "&grant_type=authorization_code";

            string url = "https://accounts.google.com/o/oauth2/v2/auth";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url.ToString());
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            UTF8Encoding utfenc = new UTF8Encoding();
            byte[] bytes = utfenc.GetBytes(postString);
            Stream os = null;
            try
            {
                request.ContentLength = bytes.Length;
                os = request.GetRequestStream();
                os.Write(bytes, 0, bytes.Length);
            }
            catch
            { }
            string result = "";

            HttpWebResponse webResponse = (HttpWebResponse)request.GetResponse();
            Stream responseStream = webResponse.GetResponseStream();
            StreamReader responseStreamReader = new StreamReader(responseStream);
            result = responseStreamReader.ReadToEnd();

            return result;
        }
    }
}

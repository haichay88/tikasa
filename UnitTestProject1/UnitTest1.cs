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
using System.Text;
using System.Web;
using System.Net;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
          
           
          
            using (WebClient webClient = new WebClient())
            {
                webClient.Encoding = Encoding.UTF8;
                string s = webClient.DownloadString("http://www.whois.net.vn/whois.php?domain=songlamplus.vn&act=getwhois");
            }



            //var arr = tet.Split(new string[] { "<br/>" }, StringSplitOptions.None);
          

            //string test = HttpUtility.HtmlDecode(arr[6].Split(':')[1]);
        }
    }
}

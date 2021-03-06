﻿using Google.Apis.AnalyticsReporting.v4;
using Google.Apis.AnalyticsReporting.v4.Data;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using MyFinance.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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

            getWhoIs();


        }
        public static void getWhoIs()
        {
            var whoisText = Whois.Lookup("phoxanh.com.vn");
          

            WebClient cl = new WebClient();
            //string test= cl.DownloadString("http://www.whois.net.vn/whois.php?domain=songlamplus.vn&act=getwhois");
            Console.WriteLine(whoisText);
            Console.WriteLine("Press any key to exit..");
            Console.Read();
        }
     
    }

    public class Whois
    {
        private const int Whois_Server_Default_PortNumber = 43;
        private const string Domain_Record_Type = "domain";
        private const string DotCom_Whois_Server = "whois.verisign-grs.com";
        private const string CreatedDateConst = "Creation Date:";
        private const string RegisterConst = "Registrar:";
        public static string[] globalExt = new string[] {"com", "net"};
        
        /// <summary>
        /// Retrieves whois information
        /// </summary>
        /// <param name="domainName">The registrar or domain or name server whose whois information to be retrieved</param>
        /// <param name="recordType">The type of record i.e a domain, nameserver or a registrar</param>
        /// <returns></returns>
        public static string Lookup(string domainName)
        {
            string ext = new string(domainName.SkipWhile(c => c != '.').Skip(1).ToArray());
            if (globalExt.Contains(ext))
            {
                using (TcpClient whoisClient = new TcpClient())
                {
                    whoisClient.Connect(DotCom_Whois_Server, Whois_Server_Default_PortNumber);

                    string domainQuery = Domain_Record_Type + " " + domainName + "\r\n";
                    byte[] domainQueryBytes = Encoding.ASCII.GetBytes(domainQuery.ToCharArray());

                    Stream whoisStream = whoisClient.GetStream();
                    whoisStream.Write(domainQueryBytes, 0, domainQueryBytes.Length);

                    StreamReader whoisStreamReader = new StreamReader(whoisClient.GetStream(), Encoding.ASCII);
                    // string text = whoisStreamReader.ReadToEnd();
                    string streamOutputContent = "";
                    List<string> whoisData = new List<string>();
                    while (null != (streamOutputContent = whoisStreamReader.ReadLine()))
                    {
                        streamOutputContent = streamOutputContent.Trim();
                        if (streamOutputContent.Contains(RegisterConst))
                        {
                            var test = streamOutputContent.Split(':');
                            if (test.Length > 0)
                                whoisData.Add(test[1]);
                        }
                        if (streamOutputContent.Contains(CreatedDateConst))
                        {
                            var test = streamOutputContent.Split(':');
                            if (test.Length > 0)
                                whoisData.Add(test[1]);
                        }


                    }

                    whoisClient.Close();

                    return String.Join(Environment.NewLine, whoisData);
                }
            }
            else
            {
                using (WebClient cl = new WebClient())
                {
                    var tet = cl.DownloadString("http://www.whois.net.vn/whois.php?domain=songlamplus.vn&act=getwhois");

                  
                  
                   
                    var arr = tet.Split(new string[] { "<br/>" }, StringSplitOptions.None);


                    return Decode(arr[6].Split(':')[1]);
                }

            }
            
        }

        public static string Decode(string path)
        {
            // This StreamReader constructor defaults to UTF-8
            using (StreamReader reader = new StreamReader(path))
                return reader.ReadToEnd();
        }
    }
}

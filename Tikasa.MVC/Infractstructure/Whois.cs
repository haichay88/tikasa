using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web;
using Tikasa.Model;

namespace Tikasa.MVC.Infractstructure
{
    public class Whois
    {
        private const int Whois_Server_Default_PortNumber = 43;
        private const string Domain_Record_Type = "domain";
        private const string DotCom_Whois_Server = "whois.verisign-grs.com";
        private const string CreatedDateConst = "Creation Date:";
        private const string RegisterConst = "Registrar:";
        public static string[] globalExt = new string[] { "com", "net" };
        /// <summary>
        /// Retrieves whois information
        /// </summary>
        /// <param name="domainName">The registrar or domain or name server whose whois information to be retrieved</param>
        /// <param name="recordType">The type of record i.e a domain, nameserver or a registrar</param>
        /// <returns></returns>
        public static Who Lookup(string domainName)
        {
            Who who = new Who();
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
                    string streamOutputContent = "";
                    List<string> whoisData = new List<string>();
                   
                    while (null != (streamOutputContent = whoisStreamReader.ReadLine()))
                    {
                        streamOutputContent = streamOutputContent.Trim();
                        if (streamOutputContent.Contains(RegisterConst))
                        {
                            var test = streamOutputContent.Split(':');
                            if (test.Length > 0)
                                who.Registrar = test[1];
                        }
                        if (streamOutputContent.Contains(CreatedDateConst))
                        {
                            var test = streamOutputContent.Split(':');
                            if (test.Length > 0)
                                who.CreationDate = test[1];
                        }


                    }

                    whoisClient.Close();

                    return who;
                }
            }
            else
            {
                using (WebClient cl = new WebClient())
                {
                    string add = "http://www.whois.net.vn/whois.php?domain="+domainName+"&act=getwhois";

                    using (WebClient webClient = new WebClient())
                    {
                        webClient.Encoding = Encoding.UTF8;
                        string data = webClient.DownloadString(add);
                        var arr = data.Split(new string[] { "<br/>" }, StringSplitOptions.None);
                        who.CreationDate = arr[3].Split(':')[1];
                        who.Registrar = arr[6].Split(':')[1];
                        return who;
                    }

                  
                }
            }
               
        }
    }

   
}
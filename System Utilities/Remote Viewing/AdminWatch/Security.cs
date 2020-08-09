using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Threading;

namespace AdminWatch
{
    public static class Security
    {

        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int description, int reservedValue);

        //checks internet connectivity auxilliary method
        private static bool IsConnected()
        {
            int description;
            return InternetGetConnectedState(out description, 0);
        }

        //checks internet connection, if there is no connection, loop message until there is connection
        public static void CheckInternetConnection()
        {
            while (!IsConnected())
            {
                Console.WriteLine("No internet connection. Please connect to a network.");
                Thread.Sleep(2000);
            }
        }

        /*
         * takes data as string and caller as a string, sends off email to admin containing the data
         * in body of email and method that called the service as subject header
        */
        public static void EmailAdmin(string data, string caller)
        {
            CheckInternetConnection();
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.EnableSsl = true;
            MailMessage emailMessage = new MailMessage();
            emailMessage.To.Add(new MailAddress(ConfigurationManager.AppSettings["admin"]));
            emailMessage.Subject = string.Format("Network Diagnostics - {0}", caller);
            emailMessage.IsBodyHtml = false;
            emailMessage.Body = data;
            client.Send(emailMessage);
            Console.WriteLine("Admin has been emailed.");
        }

        //downloads text file that contains list of blacklisted ip addresses
        public static void DownloadBlacklist()
        {
            Console.WriteLine("Updating blacklist database...");
            string source = "https://raw.githubusercontent.com/stamparm/ipsum/master/ipsum.txt";
            string fileName = "BlackList.txt";
            WebClient client = new WebClient();
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
                CheckInternetConnection();
                client.DownloadFile(source, fileName);
            }
            else
            {
                CheckInternetConnection();
                client.DownloadFile(source, fileName);
            }
            Console.WriteLine("Update complete.");
        }

        /*
         * returns a list of ip addresses taken from the downloaded blacklist file
         */
        public static List<IPAddress> ProcessBlacklist()
        {
            List<IPAddress> blacklist = new List<IPAddress>();
            IEnumerable<string> lines = File.ReadLines("Blacklist.txt");
            foreach (string line in lines)
            {
                if (!line.StartsWith("#"))
                {
                    string cleanLine = line.Substring(0, (line.Length - 2));
                    IPAddress ip = IPAddress.Parse(cleanLine.Trim());
                    blacklist.Add(ip);
                }
            }
            return blacklist;
        }

        //takes ip address as param. returns hostname of ip address if available
        public static string ResolveIpAddress(IPAddress ip)
        {
            string hostname = string.Empty;
            try
            {
                IPHostEntry ip2domain = Dns.GetHostEntry(ip.ToString());
                hostname = ip2domain.HostName;
            }
            catch (Exception exception)
            {
                hostname = "N/A";
            }
            return hostname;
        }

        //clears window
        public static void ClearTerminal()
        {
            Console.Clear();
        }
    }
}
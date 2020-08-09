using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

namespace AdminWatch
{
    class CommandCenter
    {
        //checks to see if PacketSniffer is active, returns true if so
        private static bool IsSnifferActive()
        {
            Process[] processes = Process.GetProcessesByName("PacketSniffer");
            if (processes.Length == 0)
                return false;
            else
                return true;
        }

        //calls api to get current public ip address of machine.
        private static string GetCurrentIpAddress()
        {
            return IPAddress.Parse(new WebClient().DownloadString("https://api.ipify.org/")).ToString();
        }
        
        /*
         * grabs public ip address, timestamp, runs command prompt on admin priv in order to get all established connections
		 * uses netstat command, filtered by ESTABLISHED to find established connections
         * sends email to admin with data, prints data to console
         */
        private static void NetworkWatch()
        {
            StringBuilder stringQueue = new StringBuilder();
            string currentIP = string.Empty;
            currentIP = GetCurrentIpAddress();
            Console.WriteLine(string.Format("Timestamp: {0}\nCurrent Public IP Address: {1}", DateTime.Now, currentIP));
            ProcessStartInfo processInfo = new ProcessStartInfo("cmd.exe", "/c netstat -f | findstr ESTABLISHED")
            {
                CreateNoWindow = false,
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                Verb = "runas",
                WorkingDirectory = @"C:\Windows\System32\"
            };

            Process process = Process.Start(processInfo);
            process.OutputDataReceived += (sender, arguments) => stringQueue.AppendLine(arguments.Data);
            process.BeginOutputReadLine();
            process.WaitForExit();
            process.Close();
            Security.EmailAdmin(string.Format("Local Device Analysis\n\nTimestamp: {0}\nCurrent Public IP used: {1}\n\n{2}", DateTime.Now, currentIP, stringQueue.ToString()), "AdminWatch");
            Console.WriteLine(stringQueue.ToString());
            stringQueue.Clear();
        }

        /*
         * downloads blacklist, starts up PacketSniffer, checks internet connection. checks if PacketSniffer is active, if not then starts it up
         * grabs established connections, has a countdown from 10 to 0, afterwards clears window. runs on a 15 minute interval
         */
        static void Main(string[] args)
        {
            int updateCountdown = 10;
            Security.DownloadBlacklist();
            //start sniffer at startup
            Console.WriteLine("Initiating Network Analyzer...");            

            //gets exe file after installation
            Process.Start(Path.GetFullPath("PacketSniffer.exe"));

            //gets exe file for debugging
            //Process.Start(Path.GetFullPath("..\\..\\..\\PacketSniffer\\bin\\debug\\PacketSniffer.exe"));
            while (true)
            {
                Security.CheckInternetConnection();
                //check if sniffer is still active
                if (!IsSnifferActive())
                {
                    Security.CheckInternetConnection();
                    Process.Start(Path.GetFullPath("PacketSniffer.exe"));
                    //Process.Start(Path.GetFullPath("..\\..\\..\\PacketSniffer\\bin\\debug\\PacketSniffer.exe"));
                }
                NetworkWatch();
                if (updateCountdown == 0)
                {
                    Security.DownloadBlacklist();
                    updateCountdown = 10;
                    Security.ClearTerminal();
                }
                else
                {
                    updateCountdown--;
                }
                Thread.Sleep(900000);
            }
        }
    }
}
using AdminWatch;
using Newtonsoft.Json;
using PacketDotNet;
using PacketSniffer.Models;
using RestSharp;
using SharpPcap;
using SharpPcap.LibPcap;
using SharpPcap.Npcap;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace PacketSniffer
{
    class CommandCenter
    {
        private static List<IPAddress> currentTrafficQueue = new List<IPAddress>();
        private static List<IPAddress> previousTrafficQueue;
        private static IPAddress currentDeviceLocalIp;
        private static StringBuilder currentDetailsQueue = new StringBuilder();
        private static StringBuilder previousDetailsQueue;
        private static List<IPAddress> blacklist = Security.ProcessBlacklist();
        private static StringBuilder report = new StringBuilder();
        private static bool isDeviceActive = false;
        private static Dictionary<string, string> ipDictionary = new Dictionary<string, string>();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int cCmdShow);

        //Check to see whether or not main application is running
        private static bool IsAdminWatchActive()
        {
            Process[] processes = Process.GetProcessesByName("AdminWatch");
            if (processes.Length == 0)
                return false;
            else
                return true;
        }

        /*
         * get list of all device interfaces on machine, iterate through list and check for wifi/ethernet connectivity
         * collect packets for 00:01:30, print to console.
         */
        private static void Sniffer()
        {
            NpcapDeviceList devices = NpcapDeviceList.Instance;
            NpcapDevice activeDevice = null;
            foreach (NpcapDevice currentDevice in devices)
            {
                IsDeviceActive(currentDevice);
                if ((isDeviceActive && ((currentDevice.ToString().Contains("FriendlyName: Wi-Fi")) || (currentDevice.ToString().Contains("FriendlyName: Ethernet")))))
                {
                    activeDevice = currentDevice;
                    break;
                }                
            }
            foreach (PcapAddress ipaddress in activeDevice.Addresses)
            {
                if (ipaddress.Addr != null && ipaddress.Addr.ipAddress != null)
                {
                    if (ipaddress.Addr.ipAddress.ToString().Contains("192.168"))
                    {
                        currentDeviceLocalIp = ipaddress.Addr.ipAddress;
                        break;
                    }
                }
            }
            
            activeDevice.OnPacketArrival += new SharpPcap.PacketArrivalEventHandler(PacketHandler);
            Console.WriteLine(string.Format("{0}\n", activeDevice));
            Console.WriteLine("Initiating Packet Capture...");
            activeDevice.Open(DeviceMode.Promiscuous, 1000);
            activeDevice.Filter = "ip and tcp";
            activeDevice.StartCapture();
            Thread.Sleep(90000);
            activeDevice.StopCapture();
            activeDevice.Close();
            Console.WriteLine("...Packet Capture complete.");
            Console.WriteLine("==============================================\n\n\n");
        }

        /* 
         * handles how packets are processed. prints off timestamp, length of packet, and readable packet information
         * saves each packet to a queue for later processing. email admin if there are any issues
        */
        private static void PacketHandler(object sender, CaptureEventArgs e)
        {
            try
            {
                PacketDotNet.Packet packet = PacketDotNet.Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data);
                DateTime time = e.Packet.Timeval.Date;
                int length = e.Packet.Data.Length;
                string details = string.Format("{0} Length={1}\n{2}\n\n", DateTime.Now, length, packet.ToString());
                Console.WriteLine(details);
                currentDetailsQueue.AppendLine(details);
                IPv4Packet ipv4Packet = packet.PayloadPacket as IPv4Packet;
                if ((currentTrafficQueue.Contains(ipv4Packet.SourceAddress) == false) && (ipv4Packet.SourceAddress.ToString().Contains("192.168") == false))
                    currentTrafficQueue.Add(ipv4Packet.SourceAddress);
            }
            catch (Exception exception)
            {
                Security.CheckInternetConnection();
                Security.EmailAdmin(exception.ToString(), "PacketHandler");
            }            
        }

        //checks device/interface to see if there is traffic on it, same logic as Sniffer
        private static void IsDeviceActive(NpcapDevice device)
        {
            device.OnPacketArrival += new SharpPcap.PacketArrivalEventHandler(DeviceConnectivityHandler);
            device.Open(DeviceMode.Promiscuous, 1000);
            device.Filter = "ip and tcp";
            device.StartCapture();
            Thread.Sleep(1000);
            device.StopCapture();
            device.Close();
        }

        //handles how packets are processed for connectivity check. if there are packts, sets flag to true. same logic as PacketHandler
        private static void DeviceConnectivityHandler(object sender, CaptureEventArgs e)
        {
            PacketDotNet.Packet packet = PacketDotNet.Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data);
            string details = string.Format("{0}", packet.ToString());
            if ((!string.IsNullOrEmpty(details)) && (!string.IsNullOrWhiteSpace(details)))
                isDeviceActive = true;
        }

        /*
         * gets an ip address as a param. passes ip to api to get all pertinent information about ip address
         * saves information in a dictionary where ip address is the key, and the response from api is the value
         */
        private static void GetIpDetails(IPAddress ipAddress)
        {
            RestClient client = new RestClient(string.Format("https://ipapi.co/{0}/json",ipAddress.ToString()));
            RestRequest request = new RestRequest()
            {
                Method = Method.GET
            };
            IRestResponse response = client.Execute(request);
            IpDetails specs = JsonConvert.DeserializeObject<IpDetails>(response.Content);
            StringBuilder details = new StringBuilder();
            details.AppendLine(string.Format("Hostname: {0}", Security.ResolveIpAddress(ipAddress)));
            details.AppendLine(string.Format("City: {0}", specs.city));
            details.AppendLine(string.Format("Region: {0}", specs.region));
            details.AppendLine(string.Format("Region Code: {0}", specs.region_code));
            details.AppendLine(string.Format("Country: {0}", specs.country));
            details.AppendLine(string.Format("Country Code: {0}", specs.country_code));
            details.AppendLine(string.Format("Country Code ISO3: {0}", specs.country_code_iso3));
            details.AppendLine(string.Format("Country Capital: {0}", specs.country_capital));
            details.AppendLine(string.Format("Country TLD: {0}", specs.country_tld));
            details.AppendLine(string.Format("Country Name: {0}", specs.country_name));
            details.AppendLine(string.Format("Continent Code: {0}", specs.continent_code));
            details.AppendLine(string.Format("IN EU: {0}", specs.in_eu));
            details.AppendLine(string.Format("Postal: {0}", specs.postal));
            details.AppendLine(string.Format("Latitude: {0}", specs.latitude));
            details.AppendLine(string.Format("Longitude: {0}", specs.longitude));
            details.AppendLine(string.Format("Timezone: {0}", specs.timezone));
            details.AppendLine(string.Format("UTC Offset: {0}", specs.utc_offset));
            details.AppendLine(string.Format("ASN: {0}", specs.asn));
            details.AppendLine(string.Format("Org: {0}", specs.org));

            ipDictionary.Add(ipAddress.ToString(), details.ToString());
        }

		/*
         * gets a list of ip addresses as a param. iterates through each ip and creates a rule on firewall to block each ip
		 * uses admin priv to call netsh command to add rule to firewall
         */
        private static void BanIPs(List<IPAddress> banList)
        {
            StringBuilder stringQueue = new StringBuilder();
            foreach (IPAddress ip in banList)
            {
                ProcessStartInfo processInfo = new ProcessStartInfo("cmd.exe", string.Format("/c netsh advfirewall add rule name='BLOCKED IP' interface=any dir=in action=block remoteip={0}",ip.ToString()))
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
            }
        }

        /*
         * iterates through all packets in queue, appends packets to report, gets all pertinent information about each ip address and stores in dictionary
         * runs every ip address against ip blacklist, add to ban list if ip is found on blacklist. iterates through dictionary and adds all entries to report
         * bans all ip addresses in ban list, adds those ips to report. clears dictionary and queue, returns report.
         */
        private static string IpAnalysis()
        {
            List<IPAddress> banList = new List<IPAddress>();
            StringBuilder banReport = new StringBuilder();
            banReport.AppendLine("=====================");
            banReport.AppendLine("Blocked IP Addresses\n\n");
            foreach (IPAddress ipAddress in currentTrafficQueue)
            {
                GetIpDetails(ipAddress);
                if (blacklist.Contains(ipAddress))
                {
                    banReport.AppendLine(ipAddress.ToString());
                    banList.Add(ipAddress);
                }
            }

            foreach (string ip in ipDictionary.Keys)
            {
                report.AppendLine(string.Format("{0} : {1}", ip.ToString(), ipDictionary[ip.ToString()]));
                report.AppendLine();
            }

            if (banList.Count > 0)
                BanIPs(banList);
            else
                banReport.AppendLine("No IPs were banned.");
            banReport.AppendLine("=====================");
            previousTrafficQueue = currentTrafficQueue;
            currentTrafficQueue.Clear();
            ipDictionary.Clear();
            report.AppendLine("\n\n");
            report.AppendLine(banReport.ToString());
            return report.ToString();
        }

        /*
         * calls system process to hide window, checks to see if AdminWatch is active, if not, shuts down
         * starts sniffing network traffic, emails data to admin, clears window, runs on 5 minute intervals
         */
        static void Main(string[] args)
        {            
            IntPtr window = Process.GetCurrentProcess().MainWindowHandle;
            ShowWindow(window, 0);
            //run at all times and as long as AdminWatch program is still active, on 5 minute intervals
            while (true && IsAdminWatchActive())
            {
                Sniffer();
                Security.EmailAdmin(string.Format("{0}\n\n{1}", currentDetailsQueue.ToString(), IpAnalysis()), "PacketSniffer");
                previousDetailsQueue = currentDetailsQueue;
                currentDetailsQueue.Clear();
                Security.ClearTerminal();
                Thread.Sleep(300000);
            }
        }
    }
}
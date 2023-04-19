using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemoteIT
{
    public partial class Form1 : Form
    {
        private readonly TcpClient client = new TcpClient();
        private NetworkStream stream;
        private int portUsed;

        private static Image GetDesktopImage()
        {
            //get screen bounds
            Rectangle screenBounds = Screen.PrimaryScreen.Bounds;

            //create a bitmap image representing the screen
            Bitmap remoteScreen = new Bitmap(screenBounds.Width, screenBounds.Height, PixelFormat.Format32bppArgb);

            //create graphics from the screen
            Graphics graphic = Graphics.FromImage(remoteScreen);

            //copy graphic from screen
            graphic.CopyFromScreen(screenBounds.X, screenBounds.Y, 0, 0, screenBounds.Size, CopyPixelOperation.SourceCopy);

            return remoteScreen;
        }

        private void SendDesktopImage()
        {
            while (client.Connected)
            {
                //attempt to get stream and send remote screen
                try
                {
                    //set up a binary formatter and serialize remote screen
                    BinaryFormatter formatter = new BinaryFormatter();

                    //get the stream
                    stream = client.GetStream();

                    //serialize stream and send
                    formatter.Serialize(stream, GetDesktopImage());
                }
                catch (IOException error)
                {
                    //if host has been disconnected close all open connections
                    timer1.Stop();
                    client.Close();
                    connectDisconnect.Text = "Connect";
                    share.Text = "Share Screen";
                    share.Enabled = false;
                    MessageBox.Show("Connection to host has been interrupted! Closing all connections...");
                    break;
                }
            }
        }


        public Form1()
        {
            InitializeComponent();
        }

        private void ConnectDisconnect_Click(object sender, EventArgs e)
        {
            //check if it's connecting or disconnecting
            if (connectDisconnect.Text == "Connect")
            {
                //check to see if port input is valid
                int port;
                if ((int.TryParse(port2Use.Text, out port) == true) && (port > 0))
                {
                    //attempt to connect
                    try
                    {
                        client.Connect(hostIP.Text, port);
                        connectDisconnect.Text = "Disconnect";

                        //enable screen sharing
                        share.Enabled = true;
                        MessageBox.Show("Connected successfully!");
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Could not connect to IP.");
                    }
                }
                else
                {
                    MessageBox.Show("Port must be a number and must be positive");
                }
            }
            else
            {
                //close the connection
                client.Close();
                connectDisconnect.Text = "Connect";

                //disable share screen
                share.Enabled = false;
                MessageBox.Show("Connection terminated!");
            }
        }

        private void Share_Click(object sender, EventArgs e)
        {
            if (share.Text.StartsWith("Share") == true)
            {
                //start sharing screen
                share.Text = "Stop Sharing";
                MessageBox.Show("You are now sharing your screen.", "RIT Sharing", MessageBoxButtons.OK);
                timer1.Start();
            }
            else
            {
                //stop sharing screen
                timer1.Stop();
                share.Text = "Share Screen";
                MessageBox.Show("You are no longer sharing your screen.");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SendDesktopImage();            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //disable share screen until client is connected to server
            share.Enabled = false;
        }
    }
}
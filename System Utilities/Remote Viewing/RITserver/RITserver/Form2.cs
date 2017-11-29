using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RITserver
{
    public partial class Form2 : Form
    {
        private readonly int port;
        private TcpClient client;
        private TcpListener server;
        private NetworkStream stream;
        private readonly Thread thread;
        private readonly Thread remote;

        public Form2(int portUsed)
        {
            port = portUsed;
            client = new TcpClient();
            thread = new Thread(StartListening);
            remote = new Thread(ReceiveDesktopImage);
            InitializeComponent();
        }

        private void StartListening()
        {
            //connect to the client while not already connected
            while (!client.Connected)
            {
                server.Start();
                client = server.AcceptTcpClient();
            }
            remote.Start();
        }

        private void StopListening()
        {
            //disconnect and dispose
            server.Stop();
            client = null;

            //abort all threads
            if (thread.IsAlive)
                thread.Abort();

            if (remote.IsAlive)
                remote.Abort();
        }

        private void ReceiveDesktopImage()
        {
            while (client.Connected)
            {
                //set up a binary formatter and set up the remote screen image
                BinaryFormatter formatter = new BinaryFormatter();

                //get the remote screen stream
                stream = client.GetStream();

                //deserialize the stream into an image
                remoteScreen.Image = (Image) formatter.Deserialize(stream);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            //call the super
            base.OnLoad(e);

            //start the server and thread
            server = new TcpListener(IPAddress.Any, port);
            thread.Start();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            //call the super
            base.OnFormClosing(e);

            //stop the thread
            StopListening();
        }
    }
}
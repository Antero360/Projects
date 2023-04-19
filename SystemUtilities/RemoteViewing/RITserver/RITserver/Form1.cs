using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RITserver
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void PortListener_Click(object sender, EventArgs e)
        {
            //make sure input is correct
            int port;
            if ((int.TryParse(port2Listen.Text, out port) == true) && (port > 0))
            {
                //set up remote screen
                Form2 remoteView = new Form2(port);
                remoteView.Show();
            }
            else
            {
                MessageBox.Show("ERROR! Port must be a number and must be positive.");
            }
        }
    }
}
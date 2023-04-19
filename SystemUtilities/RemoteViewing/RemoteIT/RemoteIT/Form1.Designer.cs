namespace RemoteIT
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ip = new System.Windows.Forms.Label();
            this.port = new System.Windows.Forms.Label();
            this.hostIP = new System.Windows.Forms.TextBox();
            this.port2Use = new System.Windows.Forms.TextBox();
            this.connectDisconnect = new System.Windows.Forms.Button();
            this.share = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.appDescription = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ip
            // 
            this.ip.AutoSize = true;
            this.ip.Location = new System.Drawing.Point(24, 70);
            this.ip.Name = "ip";
            this.ip.Size = new System.Drawing.Size(20, 13);
            this.ip.TabIndex = 0;
            this.ip.Text = "IP:";
            // 
            // port
            // 
            this.port.AutoSize = true;
            this.port.Location = new System.Drawing.Point(149, 71);
            this.port.Name = "port";
            this.port.Size = new System.Drawing.Size(29, 13);
            this.port.TabIndex = 1;
            this.port.Text = "Port:";
            // 
            // hostIP
            // 
            this.hostIP.Location = new System.Drawing.Point(27, 87);
            this.hostIP.Name = "hostIP";
            this.hostIP.Size = new System.Drawing.Size(100, 20);
            this.hostIP.TabIndex = 2;
            // 
            // port2Use
            // 
            this.port2Use.Location = new System.Drawing.Point(152, 87);
            this.port2Use.Name = "port2Use";
            this.port2Use.Size = new System.Drawing.Size(100, 20);
            this.port2Use.TabIndex = 3;
            // 
            // connectDisconnect
            // 
            this.connectDisconnect.Location = new System.Drawing.Point(9, 133);
            this.connectDisconnect.Name = "connectDisconnect";
            this.connectDisconnect.Size = new System.Drawing.Size(267, 31);
            this.connectDisconnect.TabIndex = 4;
            this.connectDisconnect.Text = "Connect";
            this.connectDisconnect.UseVisualStyleBackColor = true;
            this.connectDisconnect.Click += new System.EventHandler(this.ConnectDisconnect_Click);
            // 
            // share
            // 
            this.share.Location = new System.Drawing.Point(9, 170);
            this.share.Name = "share";
            this.share.Size = new System.Drawing.Size(267, 31);
            this.share.TabIndex = 5;
            this.share.Text = "Share Screen";
            this.share.UseVisualStyleBackColor = true;
            this.share.Click += new System.EventHandler(this.Share_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // appDescription
            // 
            this.appDescription.Dock = System.Windows.Forms.DockStyle.Top;
            this.appDescription.Location = new System.Drawing.Point(0, 0);
            this.appDescription.Name = "appDescription";
            this.appDescription.Size = new System.Drawing.Size(284, 49);
            this.appDescription.TabIndex = 6;
            this.appDescription.Text = "Remote IT (RIT) Client allows for easy tech support by connecting one party to a " +
    "trusted party who has the RIT server, and allowing them to share their screen.  " +
    "";
            this.appDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 207);
            this.Controls.Add(this.appDescription);
            this.Controls.Add(this.share);
            this.Controls.Add(this.connectDisconnect);
            this.Controls.Add(this.port2Use);
            this.Controls.Add(this.hostIP);
            this.Controls.Add(this.port);
            this.Controls.Add(this.ip);
            this.MaximumSize = new System.Drawing.Size(300, 246);
            this.MinimumSize = new System.Drawing.Size(300, 246);
            this.Name = "Form1";
            this.Text = "Remote IT (RIT)";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ip;
        private System.Windows.Forms.Label port;
        private System.Windows.Forms.TextBox hostIP;
        private System.Windows.Forms.TextBox port2Use;
        private System.Windows.Forms.Button connectDisconnect;
        private System.Windows.Forms.Button share;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label appDescription;
    }
}


namespace RITserver
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
            this.port = new System.Windows.Forms.Label();
            this.port2Listen = new System.Windows.Forms.TextBox();
            this.portListener = new System.Windows.Forms.Button();
            this.appDescription = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // port
            // 
            this.port.AutoSize = true;
            this.port.Location = new System.Drawing.Point(53, 80);
            this.port.Name = "port";
            this.port.Size = new System.Drawing.Size(29, 13);
            this.port.TabIndex = 0;
            this.port.Text = "Port:";
            // 
            // port2Listen
            // 
            this.port2Listen.Location = new System.Drawing.Point(85, 77);
            this.port2Listen.Name = "port2Listen";
            this.port2Listen.Size = new System.Drawing.Size(113, 20);
            this.port2Listen.TabIndex = 1;
            // 
            // portListener
            // 
            this.portListener.Location = new System.Drawing.Point(11, 122);
            this.portListener.Name = "portListener";
            this.portListener.Size = new System.Drawing.Size(261, 23);
            this.portListener.TabIndex = 2;
            this.portListener.Text = "Listen";
            this.portListener.UseVisualStyleBackColor = true;
            this.portListener.Click += new System.EventHandler(this.PortListener_Click);
            // 
            // appDescription
            // 
            this.appDescription.Location = new System.Drawing.Point(3, 3);
            this.appDescription.Name = "appDescription";
            this.appDescription.Size = new System.Drawing.Size(277, 49);
            this.appDescription.TabIndex = 3;
            this.appDescription.Text = "Remote IT (RIT) Client allows for easy tech support by connecting one party to a " +
    "trusted party who has the RIT server, and allowing them to share their screen.  " +
    "";
            this.appDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 154);
            this.Controls.Add(this.appDescription);
            this.Controls.Add(this.portListener);
            this.Controls.Add(this.port2Listen);
            this.Controls.Add(this.port);
            this.MaximumSize = new System.Drawing.Size(300, 193);
            this.MinimumSize = new System.Drawing.Size(300, 193);
            this.Name = "Form1";
            this.Text = "RemoteIT (RIT) server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label port;
        private System.Windows.Forms.TextBox port2Listen;
        private System.Windows.Forms.Button portListener;
        private System.Windows.Forms.Label appDescription;
    }
}


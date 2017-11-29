namespace RITserver
{
    partial class Form2
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
            this.remoteScreen = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.remoteScreen)).BeginInit();
            this.SuspendLayout();
            // 
            // remoteScreen
            // 
            this.remoteScreen.BackColor = System.Drawing.Color.Gray;
            this.remoteScreen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.remoteScreen.Location = new System.Drawing.Point(0, 0);
            this.remoteScreen.Name = "remoteScreen";
            this.remoteScreen.Size = new System.Drawing.Size(984, 661);
            this.remoteScreen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.remoteScreen.TabIndex = 0;
            this.remoteScreen.TabStop = false;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(984, 661);
            this.Controls.Add(this.remoteScreen);
            this.MaximumSize = new System.Drawing.Size(1000, 700);
            this.MinimumSize = new System.Drawing.Size(1000, 700);
            this.Name = "Form2";
            this.Text = "RIT viewer";
            ((System.ComponentModel.ISupportInitialize)(this.remoteScreen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox remoteScreen;
    }
}
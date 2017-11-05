namespace DigitConverter
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
            this.descriptionHeader = new System.Windows.Forms.Label();
            this.lowerBound = new System.Windows.Forms.TextBox();
            this.upperBound = new System.Windows.Forms.TextBox();
            this.convertButton = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // descriptionHeader
            // 
            this.descriptionHeader.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.descriptionHeader.AutoSize = true;
            this.descriptionHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.descriptionHeader.Location = new System.Drawing.Point(2, 4);
            this.descriptionHeader.MinimumSize = new System.Drawing.Size(333, 13);
            this.descriptionHeader.Name = "descriptionHeader";
            this.descriptionHeader.Size = new System.Drawing.Size(342, 13);
            this.descriptionHeader.TabIndex = 0;
            this.descriptionHeader.Text = "Enter a range of positive numbers to convert to binary and hexadecimal";
            this.descriptionHeader.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.descriptionHeader.Click += new System.EventHandler(this.descriptionHeader_Click);
            // 
            // lowerBound
            // 
            this.lowerBound.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lowerBound.Location = new System.Drawing.Point(5, 33);
            this.lowerBound.Name = "lowerBound";
            this.lowerBound.Size = new System.Drawing.Size(100, 20);
            this.lowerBound.TabIndex = 1;
            // 
            // upperBound
            // 
            this.upperBound.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.upperBound.Location = new System.Drawing.Point(144, 32);
            this.upperBound.Name = "upperBound";
            this.upperBound.Size = new System.Drawing.Size(105, 20);
            this.upperBound.TabIndex = 2;
            // 
            // convertButton
            // 
            this.convertButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.convertButton.Location = new System.Drawing.Point(269, 29);
            this.convertButton.Name = "convertButton";
            this.convertButton.Size = new System.Drawing.Size(75, 23);
            this.convertButton.TabIndex = 3;
            this.convertButton.Text = "CONVERT";
            this.convertButton.UseVisualStyleBackColor = true;
            this.convertButton.Click += new System.EventHandler(this.convertButton_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(5, 59);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(339, 247);
            this.dataGridView1.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(117, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "to";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(348, 312);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.convertButton);
            this.Controls.Add(this.upperBound);
            this.Controls.Add(this.lowerBound);
            this.Controls.Add(this.descriptionHeader);
            this.MaximumSize = new System.Drawing.Size(364, 351);
            this.MinimumSize = new System.Drawing.Size(364, 351);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label descriptionHeader;
        private System.Windows.Forms.TextBox lowerBound;
        private System.Windows.Forms.TextBox upperBound;
        private System.Windows.Forms.Button convertButton;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
    }
}


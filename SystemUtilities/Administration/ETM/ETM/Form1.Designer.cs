namespace ETM
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
            this.processView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.task2Run = new System.Windows.Forms.TextBox();
            this.startProcess = new System.Windows.Forms.Button();
            this.killProcess = new System.Windows.Forms.Button();
            this.totalProcesses = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // processView
            // 
            this.processView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.processView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.processView.Location = new System.Drawing.Point(258, 6);
            this.processView.Name = "processView";
            this.processView.Size = new System.Drawing.Size(393, 413);
            this.processView.TabIndex = 0;
            this.processView.UseCompatibleStateImageBehavior = false;
            this.processView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Tag = "1";
            this.columnHeader1.Text = "PID";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Tag = "1";
            this.columnHeader2.Text = "Name";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Tag = "1";
            this.columnHeader3.Text = "Description";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Tag = "1";
            this.columnHeader4.Text = "Status";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Tag = "1";
            this.columnHeader5.Text = "Username";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Tag = "1";
            this.columnHeader6.Text = "Memory (private working set)";
            // 
            // task2Run
            // 
            this.task2Run.Location = new System.Drawing.Point(12, 34);
            this.task2Run.Name = "task2Run";
            this.task2Run.Size = new System.Drawing.Size(207, 20);
            this.task2Run.TabIndex = 1;
            // 
            // startProcess
            // 
            this.startProcess.Location = new System.Drawing.Point(12, 60);
            this.startProcess.Name = "startProcess";
            this.startProcess.Size = new System.Drawing.Size(75, 23);
            this.startProcess.TabIndex = 2;
            this.startProcess.Text = "Start";
            this.startProcess.UseVisualStyleBackColor = true;
            this.startProcess.Click += new System.EventHandler(this.StartProcess_Click);
            // 
            // killProcess
            // 
            this.killProcess.Location = new System.Drawing.Point(12, 94);
            this.killProcess.Name = "killProcess";
            this.killProcess.Size = new System.Drawing.Size(75, 23);
            this.killProcess.TabIndex = 3;
            this.killProcess.Text = "Kill";
            this.killProcess.UseVisualStyleBackColor = true;
            this.killProcess.Click += new System.EventHandler(this.KillProcess_Click);
            // 
            // totalProcesses
            // 
            this.totalProcesses.AutoSize = true;
            this.totalProcesses.Location = new System.Drawing.Point(9, 6);
            this.totalProcesses.Name = "totalProcesses";
            this.totalProcesses.Size = new System.Drawing.Size(0, 13);
            this.totalProcesses.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 425);
            this.Controls.Add(this.totalProcesses);
            this.Controls.Add(this.killProcess);
            this.Controls.Add(this.startProcess);
            this.Controls.Add(this.task2Run);
            this.Controls.Add(this.processView);
            this.Name = "Form1";
            this.Text = "ETM (Emergency Task Manager)";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView processView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.TextBox task2Run;
        private System.Windows.Forms.Button startProcess;
        private System.Windows.Forms.Button killProcess;
        private System.Windows.Forms.Label totalProcesses;
    }
}


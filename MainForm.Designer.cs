namespace BritishLibraryDownloader
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnGetInfo = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.mainThread = new System.ComponentModel.BackgroundWorker();
            this.txtManuScripts = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbManuScriptsInfo = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtShowPagesNumber = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.gbDownloadInfo = new System.Windows.Forms.GroupBox();
            this.lblQuality = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label6 = new System.Windows.Forms.Label();
            this.lblSaveTo = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFrom = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnStartDownload = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.downLoadThread = new System.ComponentModel.BackgroundWorker();
            this.gbManuScriptsInfo.SuspendLayout();
            this.gbDownloadInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGetInfo
            // 
            this.btnGetInfo.Location = new System.Drawing.Point(261, 53);
            this.btnGetInfo.Name = "btnGetInfo";
            this.btnGetInfo.Size = new System.Drawing.Size(75, 23);
            this.btnGetInfo.TabIndex = 0;
            this.btnGetInfo.Text = "Get Info";
            this.btnGetInfo.UseVisualStyleBackColor = true;
            this.btnGetInfo.Click += new System.EventHandler(this.button1_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 404);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(358, 18);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 1;
            // 
            // mainThread
            // 
            this.mainThread.DoWork += new System.ComponentModel.DoWorkEventHandler(this.mainThread_DoWork);
            this.mainThread.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.mainThread_ProgressChanged);
            this.mainThread.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.mainThread_RunWorkerCompleted);
            // 
            // txtManuScripts
            // 
            this.txtManuScripts.Location = new System.Drawing.Point(124, 27);
            this.txtManuScripts.Name = "txtManuScripts";
            this.txtManuScripts.Size = new System.Drawing.Size(212, 20);
            this.txtManuScripts.TabIndex = 2;
            this.txtManuScripts.Text = "Grenville XLI";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "ManuScripts Name:";
            // 
            // gbManuScriptsInfo
            // 
            this.gbManuScriptsInfo.Controls.Add(this.label5);
            this.gbManuScriptsInfo.Controls.Add(this.txtShowPagesNumber);
            this.gbManuScriptsInfo.Controls.Add(this.label2);
            this.gbManuScriptsInfo.Controls.Add(this.txtTitle);
            this.gbManuScriptsInfo.Controls.Add(this.label1);
            this.gbManuScriptsInfo.Controls.Add(this.btnGetInfo);
            this.gbManuScriptsInfo.Controls.Add(this.txtManuScripts);
            this.gbManuScriptsInfo.Location = new System.Drawing.Point(12, 12);
            this.gbManuScriptsInfo.Name = "gbManuScriptsInfo";
            this.gbManuScriptsInfo.Size = new System.Drawing.Size(358, 143);
            this.gbManuScriptsInfo.TabIndex = 4;
            this.gbManuScriptsInfo.TabStop = false;
            this.gbManuScriptsInfo.Text = "Get Document Information";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 115);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Number Of Pages:";
            // 
            // txtShowPagesNumber
            // 
            this.txtShowPagesNumber.Location = new System.Drawing.Point(124, 112);
            this.txtShowPagesNumber.Name = "txtShowPagesNumber";
            this.txtShowPagesNumber.ReadOnly = true;
            this.txtShowPagesNumber.Size = new System.Drawing.Size(212, 20);
            this.txtShowPagesNumber.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Info:";
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(124, 86);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.ReadOnly = true;
            this.txtTitle.Size = new System.Drawing.Size(212, 20);
            this.txtTitle.TabIndex = 2;
            // 
            // gbDownloadInfo
            // 
            this.gbDownloadInfo.Controls.Add(this.lblQuality);
            this.gbDownloadInfo.Controls.Add(this.trackBar1);
            this.gbDownloadInfo.Controls.Add(this.label6);
            this.gbDownloadInfo.Controls.Add(this.lblSaveTo);
            this.gbDownloadInfo.Controls.Add(this.btnSave);
            this.gbDownloadInfo.Controls.Add(this.label4);
            this.gbDownloadInfo.Controls.Add(this.txtTo);
            this.gbDownloadInfo.Controls.Add(this.label3);
            this.gbDownloadInfo.Controls.Add(this.txtFrom);
            this.gbDownloadInfo.Location = new System.Drawing.Point(12, 161);
            this.gbDownloadInfo.Name = "gbDownloadInfo";
            this.gbDownloadInfo.Size = new System.Drawing.Size(358, 174);
            this.gbDownloadInfo.TabIndex = 5;
            this.gbDownloadInfo.TabStop = false;
            this.gbDownloadInfo.Text = "Download Information";
            // 
            // lblQuality
            // 
            this.lblQuality.AutoSize = true;
            this.lblQuality.Location = new System.Drawing.Point(325, 66);
            this.lblQuality.Name = "lblQuality";
            this.lblQuality.Size = new System.Drawing.Size(0, 13);
            this.lblQuality.TabIndex = 11;
            // 
            // trackBar1
            // 
            this.trackBar1.AutoSize = false;
            this.trackBar1.Location = new System.Drawing.Point(95, 58);
            this.trackBar1.Maximum = 13;
            this.trackBar1.Minimum = 5;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(215, 30);
            this.trackBar1.TabIndex = 10;
            this.trackBar1.Value = 10;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(47, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Quality:";
            // 
            // lblSaveTo
            // 
            this.lblSaveTo.ForeColor = System.Drawing.Color.DimGray;
            this.lblSaveTo.Location = new System.Drawing.Point(21, 129);
            this.lblSaveTo.Name = "lblSaveTo";
            this.lblSaveTo.Size = new System.Drawing.Size(315, 32);
            this.lblSaveTo.TabIndex = 4;
            this.lblSaveTo.Text = "Please Select SaveFolder";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(20, 103);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(108, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save To";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(197, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "To:";
            // 
            // txtTo
            // 
            this.txtTo.Location = new System.Drawing.Point(226, 26);
            this.txtTo.Name = "txtTo";
            this.txtTo.Size = new System.Drawing.Size(84, 20);
            this.txtTo.TabIndex = 6;
            this.txtTo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTo_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(56, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "From:";
            // 
            // txtFrom
            // 
            this.txtFrom.Location = new System.Drawing.Point(95, 26);
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.Size = new System.Drawing.Size(84, 20);
            this.txtFrom.TabIndex = 4;
            this.txtFrom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFrom_KeyPress);
            // 
            // btnStartDownload
            // 
            this.btnStartDownload.Location = new System.Drawing.Point(12, 341);
            this.btnStartDownload.Name = "btnStartDownload";
            this.btnStartDownload.Size = new System.Drawing.Size(358, 28);
            this.btnStartDownload.TabIndex = 4;
            this.btnStartDownload.Text = "Start Download";
            this.btnStartDownload.UseVisualStyleBackColor = true;
            this.btnStartDownload.Click += new System.EventHandler(this.btnStartDownload_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.ForeColor = System.Drawing.Color.Gray;
            this.lblInfo.Location = new System.Drawing.Point(12, 372);
            this.lblInfo.MaximumSize = new System.Drawing.Size(318, 0);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(25, 13);
            this.lblInfo.TabIndex = 12;
            this.lblInfo.Text = "Info";
            // 
            // downLoadThread
            // 
            this.downLoadThread.DoWork += new System.ComponentModel.DoWorkEventHandler(this.downLoadThread_DoWork);
            this.downLoadThread.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.downLoadThread_RunWorkerCompleted);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 430);
            this.Controls.Add(this.btnStartDownload);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.gbDownloadInfo);
            this.Controls.Add(this.gbManuScriptsInfo);
            this.Controls.Add(this.progressBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BL Downloader";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.gbManuScriptsInfo.ResumeLayout(false);
            this.gbManuScriptsInfo.PerformLayout();
            this.gbDownloadInfo.ResumeLayout(false);
            this.gbDownloadInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGetInfo;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.ComponentModel.BackgroundWorker mainThread;
        private System.Windows.Forms.TextBox txtManuScripts;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbManuScriptsInfo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.GroupBox gbDownloadInfo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFrom;
        private System.Windows.Forms.Label lblSaveTo;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label lblQuality;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnStartDownload;
        private System.Windows.Forms.Label lblInfo;
        private System.ComponentModel.BackgroundWorker downLoadThread;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtShowPagesNumber;
    }
}


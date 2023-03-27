using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BritishLibraryDownloader
{
    public partial class MainForm : Form
    {




        public MainForm()
        {
            InitializeComponent();
        }
        private BritishLibraryDownloader _bld = new BritishLibraryDownloader();
        private ManuScriptsInfo _pages = null;
        private List<string> _selectedPage = new List<string>();
        private void MainForm_Load(object sender, EventArgs e)
        {
            Application.EnableVisualStyles();
            gbDownloadInfo.Enabled = false;
            lblQuality.Text = trackBar1.Value.ToString();
            _bld.ProcessInfo += _bld_ProcessInfo;
            gbDownloadInfo.Enabled = false;
            btnStartDownload.Enabled = false;
            lblSaveTo.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);


        }

        private void _bld_ProcessInfo(object sender, ProcessEventArgs e)
        {
            Invoke(new Action(() => 
            { 
                lblInfo.Text = e.Message;
                progressBar1.Value = e.PageNumber;
            }));

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (!string.IsNullOrEmpty(txtManuScripts.Text))
            {
                _bld.ManuScripts = txtManuScripts.Text;
                progressBar1.Style = ProgressBarStyle.Marquee;
                progressBar1.MarqueeAnimationSpeed = 10;
                btnGetInfo.Enabled = false;
                gbDownloadInfo.Enabled = false;
                if (!mainThread.IsBusy)
                    mainThread.RunWorkerAsync();
            }

           
        }

        
        private void mainThread_DoWork(object sender, DoWorkEventArgs e)
        {
            _pages = _bld.GetPages();
            
        }

        private void mainThread_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void mainThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.MarqueeAnimationSpeed = 0;
            btnGetInfo.Enabled = true ;
            if (_pages.Pages != null)
            {

                txtTitle.Text = _pages.Name.Trim();
                txtShowPagesNumber.Text = _pages.Pages.Length.ToString();
                gbDownloadInfo.Enabled = true;
                txtFrom.Text = "1";
                txtTo.Text = _pages.Pages.Length.ToString();
                lblInfo.Text = "Page(s) has been found.";
                btnStartDownload.Enabled = true;
            }
            else
            {
                lblInfo.Text = "No Page(s) Found! Please Check manuScript's name.";
               
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                lblSaveTo.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            lblQuality.Text = trackBar1.Value.ToString();
        }

        private bool _start_stop_flag = false;
        private void btnStartDownload_Click(object sender, EventArgs e)
        {
            _start_stop_flag ^= true;
            if (!downLoadThread.IsBusy)
            {

               
                lblInfo.Text = "";
                btnStartDownload.Text = "Stop Download";
                gbDownloadInfo.Enabled = false;
                gbManuScriptsInfo.Enabled = false;

                int start;
                int end;
                int.TryParse(txtFrom.Text, out start);
                int.TryParse(txtTo.Text, out end);
                if (start < 1)
                    start = 1;
                if (end > int.Parse(txtShowPagesNumber.Text))
                    end = int.Parse(txtShowPagesNumber.Text);
                if (start <= end)
                {
                    _selectedPage.Clear();
                    for (int i = int.Parse(txtFrom.Text); i <= int.Parse(txtTo.Text); i++)
                        _selectedPage.Add(_pages.Pages[i - 1]);

                    progressBar1.Minimum = 0;
                    if ((end - start) == 0)
                        progressBar1.Maximum = 1;
                    else
                        progressBar1.Maximum = (end - start)+1;
                    downLoadThread.RunWorkerAsync();
                }
                else
                {
                    lblInfo.Text = "Page Number Not Match.";
                }

               
            }
            else
            {

                for (int i = 0; i < 25; i++)
                    _bld.StopProcess();
                btnStartDownload.Text = "Start Download";
                gbDownloadInfo.Enabled = true;
                gbManuScriptsInfo.Enabled = true;
                
            }
          
        }

        private void downLoadThread_DoWork(object sender, DoWorkEventArgs e)
        {
            int res = 10;
            string save = "";
            Invoke(new Action(() => { res = trackBar1.Value; }));
            Invoke(new Action(() => { save = lblSaveTo.Text; }));

            if (!string.IsNullOrEmpty(save))
                _bld.DownoadPages(_selectedPage.ToArray(), res, save );
          
        }

        private void downLoadThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnStartDownload.Enabled = true;
            gbDownloadInfo.Enabled = true;
            gbManuScriptsInfo.Enabled = true;
            btnStartDownload.Text = "Start Download";
        }

        private void txtFrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }

        private void txtTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }
    }

}

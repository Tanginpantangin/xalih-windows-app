using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using Ionic.Zip;

namespace AutoUpdate
{
    /// <summary>
    /// Auto Update Form
    /// </summary>
    public partial class FrmAutoUpdate : Form
    {
        #region Constant

        private const string PROCESS_NAME = "XalihAkharCam";
        private const string FILE_NAME = "XalihAkharCam.zip";
        private const string PROCESS_PAXALIH = "XalihAkharCam.exe";
        private const string MES_SUCESS = "Cập nhật phần mềm thành công.";

        #endregion

        #region Events

        /// <summary>
        /// Form show event
        /// </summary>
        private void FrmAutoUpdate_Shown(object sender, EventArgs e)
        {
            this.timer1.Start();
            this.backgroundWorker1.RunWorkerAsync();
        }

        /// <summary>
        /// Cancel all key press
        /// </summary>
        private void FrmAutoUpdate_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }

        /// <summary>
        /// Timer tick to update status bar
        /// </summary>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar.Value == progressBar.Maximum)
            {

                progressBar.Value = progressBar.Minimum;
            }
            progressBar.PerformStep();
        }

        /// <summary>
        /// Extract file
        /// </summary>
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //kill application running
            Process[] list = Process.GetProcessesByName(PROCESS_NAME);
            foreach (Process item in list)
            {
                item.Kill();
            }

            //extract file
            this.MyExtract();
        }

        /// <summary>
        /// update completed
        /// </summary>
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Close form
            if (e.Error != null)
            {
                this.Close();
            }
            this.Close();

            //show message
            MessageBox.Show(MES_SUCESS);

            //Start Paxalih process
            System.Diagnostics.Process.Start(PROCESS_PAXALIH);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Constructor
        /// </summary>
        public FrmAutoUpdate()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Extract zip file
        /// </summary>
        private void MyExtract()
        {
            using (ZipFile zip1 = ZipFile.Read(FILE_NAME))
            {
                // here, we extract every entry, but we could extract conditionally
                // based on entry name, size, date, checkbox status, etc.  
                foreach (ZipEntry e in zip1)
                {
                    //e.Extract(unpackDirectory, ExtractExistingFileAction.OverwriteSilently);
                    e.Extract(ExtractExistingFileAction.OverwriteSilently);
                }
            }
        }

        #endregion
    }
}

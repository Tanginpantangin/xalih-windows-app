using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Resources;
using System.Globalization;
using System.Net;
using System.IO;

namespace Cam.Windows.Main
{
    /// <summary>
    /// From main
    /// </summary>
    public partial class FrmMain : Windows.Forms.FrmBase
    {
        #region Constant

        private const string FILE_LASTEST_VERSION = "LastestVersion.txt";
        private const string FILE_SOURCE = "XalihAkharCam.zip";
        private const string FILE_GUIDE = "UseGuide.pdf";
        private const string PROCESS_AUTO_UPDATE = "AutoUpdate.exe";

        #endregion

        #region Variables

        private Service.Paxalih _paxalihSv = new Service.Paxalih();
        private bool _notAct = false;
        private bool _runUpdate = false;

        //Auto update
        private string _lastedVersion = String.Empty;

        //Size combobox
        private int[] sizeArr = { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };

        //Title drag and drop
        private bool dragtitle = false;
        private int mouseX;
        private int mouseY;

        #endregion

        #region Form

        /// <summary>
        /// Contructor
        /// </summary>
        public FrmMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Load event
        /// </summary>
        private void FrmMain_Load(object sender, EventArgs e)
        {
            //Add handle events for title control
            foreach(Control control in this.pnlTitle.Controls)
            {
                control.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlTitle_MouseDown);
                control.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlTitle_MouseMove);
                control.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlTitle_MouseUp);
            }
            
            //Init size combobox
            this.cmbSize1.DataSource = this.sizeArr;
            this.cmbSize2.DataSource = this.sizeArr.Clone();

            //restore language
            this.RestoreLang();

            //Set langue
            this.rdoCam_CheckedChanged(null, null);

            this.bgwCheckUpdateVersion.RunWorkerAsync();
            this.bgwSendLog.RunWorkerAsync();
        }

        /// <summary>
        /// Form show event
        /// </summary>
        private void FrmMain_Shown(object sender, EventArgs e)
        {
            this.txtSource.Focus();
        }

        /// <summary>
        /// Title panel Mouse Down
        /// </summary>
        private void pnlTitle_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                dragtitle = true;
                mouseX = System.Windows.Forms.Cursor.Position.X - this.Left;
                mouseY = System.Windows.Forms.Cursor.Position.Y - this.Top;
            }
        }

        /// <summary>
        /// Title panel Mouse Move
        /// </summary>
        private void pnlTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragtitle)
            {
                this.Top = System.Windows.Forms.Cursor.Position.Y - mouseY;
                this.Left = System.Windows.Forms.Cursor.Position.X - mouseX;
            }
        }

        /// <summary>
        ///  Title panel Mouse Up
        /// </summary>
        private void pnlTitle_MouseUp(object sender, MouseEventArgs e)
        {
            dragtitle = false;
        }

        /// <summary>
        /// Minimize icon click
        /// </summary>
        private void lblMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        /// <summary>
        /// Minimize icon mouse enter
        /// </summary>
        private void lblMinimize_MouseEnter(object sender, EventArgs e)
        {
            Label control = (Label)sender;
            control.BorderStyle = BorderStyle.FixedSingle;
        }

        /// <summary>
        /// Minimize mouse leave
        /// </summary>
        private void lblMinimize_MouseLeave(object sender, EventArgs e)
        {
            Label control = (Label)sender;
            control.BorderStyle = BorderStyle.None;
        }

        /// <summary>
        /// Close icon click
        /// </summary>
        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Details

        /// <summary>
        ///  language radio button checked changed 
        /// </summary>
        private void rdoCam_CheckedChanged(object sender, EventArgs e)
        {
            Model.LangID seletedLang = Model.LangID.Cam;
            if (this.rdoCam.Checked)
            {
                this.SetLanguage("ms-MY");
                this.rdoCam.Font = new Font(this.rdoCam.Font, FontStyle.Bold);
                this.rdoEnglish.Font = new Font(this.rdoEnglish.Font, FontStyle.Regular);
                this.rdoVietnamese.Font = new Font(this.rdoVietnamese.Font, FontStyle.Regular);
            }
            else if (this.rdoEnglish.Checked)
            {
                seletedLang = Model.LangID.English;
                this.SetLanguage("en-US");
                this.rdoEnglish.Font = new Font(this.rdoEnglish.Font, FontStyle.Bold);
                this.rdoCam.Font = new Font(this.rdoCam.Font, FontStyle.Regular);
                this.rdoVietnamese.Font = new Font(this.rdoVietnamese.Font, FontStyle.Regular);
            }
            else
            {
                seletedLang = Model.LangID.Vietnamese;
                this.SetLanguage("vi-VN");
                this.rdoVietnamese.Font = new Font(this.rdoVietnamese.Font, FontStyle.Bold);
                this.rdoCam.Font = new Font(this.rdoCam.Font, FontStyle.Regular);
                this.rdoEnglish.Font = new Font(this.rdoEnglish.Font, FontStyle.Regular);
            }

            //Save current value
            int fromSelected = default(int);
            string fromSize = default(string);
            int toSelected = default(int);
            string toSize = default(string);
            bool restoreData = false;
            if (this.cmbSource.DataSource != default(object))
            {
                restoreData = true;
                fromSelected = Convert.ToInt32(this.cmbSource.SelectedValue);
                fromSize = this.cmbSize1.Text;
                toSelected =  Convert.ToInt32(this.cmbDestination.SelectedValue);
                toSize = this.cmbSize2.Text;
            }

            this._notAct = true;
            this.InitCmbCharType(this.cmbSource);
            this._notAct = false;
            this.cmbSource_SelectedValueChanged(null, null);

            //Store current value
            if (restoreData)
            {
                this.cmbSource.SelectedValue = fromSelected;
                this.cmbSize1.Text = fromSize;
                this.cmbDestination.SelectedValue = toSelected;
                this.cmbSize2.Text = toSize;
            }

            //Write seleced value to File
            Service.Utility.WriteToFile(((int)seletedLang).ToString(), Model.Constant.LANG_SELECTED_FILE);
        }
        
        /// <summary>xt
        /// source combobox selected changed
        /// </summary>
        private void cmbSource_SelectedValueChanged(object sender, EventArgs e)
        {

            if (this._notAct) return;

            Model.Enum itemSelected = Service.Utility.NumToEnum<Model.Enum>(Convert.ToInt32(this.cmbSource.SelectedValue));

            this._notAct = true;
            this.InitCmbCharType(this.cmbDestination, false, itemSelected);
            this._notAct = false;

            this._paxalihSv.combobox_SelectedChanged(itemSelected, this.txtSource, this.cmbSize1);
            this.cmbDestination_SelectedIndexChanged(null, null);
        }

        /// <summary>
        /// destination combobox selected changed
        /// </summary>
        private void cmbDestination_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this._notAct) return;

            Model.Enum itemSelected = Service.Utility.NumToEnum<Model.Enum>(Convert.ToInt32(this.cmbDestination.SelectedValue));
            this._paxalihSv.combobox_SelectedChanged(itemSelected, this.txtDestination, this.cmbSize2);
        }

        /// <summary>
        /// size source key press event
        /// </summary>
        private void txtSize1_KeyPress(object sender, KeyPressEventArgs e)
        {
            this._paxalihSv.Size_KeyPress(this.cmbSize1, e);
        }

        /// <summary>
        /// size source text changed event
        /// </summary>
        private void txtSize1_TextChanged(object sender, EventArgs e)
        {
            this._paxalihSv.Size_TextChanged(this.cmbSize1, this.txtSource);
        }

        /// <summary>
        /// size destination text changed event
        /// </summary>
        private void txtSize2_TextChanged(object sender, EventArgs e)
        {
            this._paxalihSv.Size_TextChanged(this.cmbSize2, this.txtDestination);
        }

        /// <summary>
        /// size destination text changed event
        /// </summary>
        private void txtSize2_KeyPress(object sender, KeyPressEventArgs e)
        {
            this._paxalihSv.Size_KeyPress(this.cmbSize2, e);
        }

        /// <summary>
        /// Clear button click
        /// </summary>
        private void btnClear_Click(object sender, EventArgs e)
        {
            this.txtSource.Text = string.Empty;
            this.txtDestination.Text = string.Empty;

            this.txtSource.Focus();
        }

        /// <summary>
        /// Convert button click
        /// </summary>
        private void btnConvert_Click(object sender, EventArgs e)
        {
            //clear destination
            this.txtDestination.Text = String.Empty;

            //Get selected value of source combobox
            Model.Enum sourceType = Service.Utility.NumToEnum<Model.Enum>(Convert.ToInt32(this.cmbSource.SelectedValue));
            Model.Enum destinationType = Service.Utility.NumToEnum<Model.Enum>(Convert.ToInt32(this.cmbDestination.SelectedValue));

            //Set Log Data
            Service.Log.SetLogData(this.txtSource.Text.Trim(), sourceType, destinationType);

            //Check input invalid data
            string data = this.txtSource.Text.Trim();
            if (!this.InputCheck(ref data, sourceType)) return;

            //Converting
            this.txtDestination.Text = this.ConvertData(data, sourceType, destinationType);

            //Clear Log Data
            Service.Log.ClearLogData();
        }

        /// <summary>
        /// Swap button mouse enter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSwap_MouseEnter(object sender, EventArgs e)
        {
            this.btnSwap.BackgroundImage = global::Cam.Windows.Main.Properties.Resources.SwapHover;
        }

        /// <summary>
        /// Swap button Mouse leave
        /// </summary>
        private void btnSwap_MouseLeave(object sender, EventArgs e)
        {
            this.btnSwap.BackgroundImage = global::Cam.Windows.Main.Properties.Resources.Swap;
        }

        /// <summary>
        /// Swap button click
        /// </summary>
        private void btnSwap_Click(object sender, EventArgs e)
        {
            //Save current value
            int fromSelected = Convert.ToInt32(this.cmbSource.SelectedValue);
            string fromSize = this.cmbSize1.Text;
            int toSelected = Convert.ToInt32(this.cmbDestination.SelectedValue);
            string toSize = this.cmbSize2.Text;

            //Store current value
            this.cmbSource.SelectedValue = toSelected;
            this.cmbSize1.Text = toSize;
            this.cmbDestination.SelectedValue = fromSelected;
            this.cmbSize2.Text = fromSize;
        }

        /// <summary>
        /// Web link Click
        /// </summary>
        private void lnkWeb_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.tanginpantangin.com");
        }

        /// <summary>
        /// Help link click
        /// </summary>
        private void lnkHelp_Click(object sender, EventArgs e)
        {
            string filePath = System.IO.Path.Combine(Application.StartupPath, FILE_GUIDE);
            if (File.Exists(filePath))
            {
                System.Diagnostics.Process.Start(filePath);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Restore Language
        /// Author: jabraok
        /// </summary>
        private void RestoreLang()
        {
            try
            {
                string oldLang = Service.Utility.ReadFile(Model.Constant.LANG_SELECTED_FILE);
                if (!string.IsNullOrEmpty(oldLang))
                {
                    Model.LangID selectedLang = (Model.LangID)Convert.ToInt32(oldLang);
                    switch (selectedLang)
                    {
                        case Model.LangID.Cam:
                            this.rdoCam.Checked = true;
                            break;

                        case Model.LangID.English:
                            this.rdoEnglish.Checked = true;
                            break;

                       default:
                            this.rdoVietnamese.Checked = true;
                            break;
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Check input data
        /// </summary>
        /// <param name="data">data input</param>
        /// <param name="dataType">data type</param>
        private bool InputCheck(ref String data, Model.Enum dataType)
        {
            //check source empty
            if (String.IsNullOrEmpty(data))
            {
                this.ShowMessageInfo(Model.MesseageCode.MES_PLEASE_INPUT);
                this.txtSource.Focus();
                return false;
            }

            //Check input is value
            string datareplaced = data;
            if (!this._paxalihSv.ValidateInput(ref datareplaced, dataType))
            {

                if (this.ShowMessageQuestion(Model.MesseageCode.MES_DATA_INVALID, null) == DialogResult.No)
                {
                    this.txtSource.Focus();
                    return false;
                }

                data = datareplaced;
            }

            return true;
        }

        /// <summary>
        /// Convert data
        /// </summary>
        /// <param name="data">data input</param>
        /// <param name="dataType">data type</param>
        private String ConvertData(String data, Model.Enum sourceType, Model.Enum destinationType)
        {

            String result = String.Empty;
            switch (sourceType)
            {
                case Model.Enum.FontGilaiPraong:
                case Model.Enum.FontYapata:
                case Model.Enum.FontCamEFEO:
                case Model.Enum.FontUniCamKur:
                case Model.Enum.FontUniCamVN:
                case Model.Enum.FontKTT:

                    if (this._paxalihSv.IsFont(destinationType))
                    {

                        Service.FontToFontPaxalih fontPaxalih = new Service.FontToFontPaxalih();
                        result = fontPaxalih.DoConvert(data, sourceType, destinationType);
                    }
                    else
                    {

                        Service.CamToTransPaxalih rumiPaxalih = new Service.CamToTransPaxalih();
                        result = rumiPaxalih.DoConvert(data, sourceType, destinationType);
                    }

                    break;

                default:

                    if (this._paxalihSv.IsFont(destinationType))
                    {

                        Service.TransToCamPaxalih camPaxalih = new Service.TransToCamPaxalih();
                        result = camPaxalih.DoConvert(data, sourceType, destinationType);
                    }
                    else
                    {

                        Service.TransToTransPaxalih transPaxalih = new Service.TransToTransPaxalih();
                        result = transPaxalih.DoConvert(data, sourceType, destinationType);
                    }

                    break;
            }

            return result;
        }

        /// <summary>
        /// Set language
        /// </summary>
        /// <param name="cultureName">culturename</param>
        private void SetLanguage(string cultureName)
        {
            this._culture = CultureInfo.CreateSpecificCulture(cultureName);
            this._resourceManager = new ResourceManager("Cam.Windows.Forms.Lang.sap", typeof(Windows.Forms.FrmBase).Assembly);

            this.lblTitle.Text = this._resourceManager.GetString("FRM_NAME", this._culture).ToUpper();
            this.Text = this.lblTitle.Text;
            this.lblFrom.Text = this._resourceManager.GetString("LBL_FROM", this._culture);
            this.lblTo.Text = this._resourceManager.GetString("LBL_TO", this._culture);
            this.lblSize1.Text = this._resourceManager.GetString("LBL_SIZE", this._culture);
            this.lblSize2.Text = this.lblSize1.Text;
            this.btnClear.Text = this._resourceManager.GetString("BTN_CLEAR", this._culture);
            this.btnConvert.Text = this._resourceManager.GetString("BTN_CONVERT", this._culture);
            this.lnkHelp.Text = this._resourceManager.GetString("LNK_HELP", this._culture);
        }

        #endregion

        #region Auto Update

        /// <summary>
        /// CheckUpdateVersion_DoWork
        /// </summary>
        private void bgwCheckUpdateVersion_DoWork(object sender, DoWorkEventArgs e)
        {
            //downlaod version file
            using (var client = new WebClient())
            {
                using (var file = File.Create(FILE_LASTEST_VERSION))
                {
                    var bytes = client.DownloadData(Model.Constant.URL_LASTEST_VERSION);
                    file.Write(bytes, 0, bytes.Length);
                }
            }

            //Check version
            string currentVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            using (StreamReader reader = new StreamReader(FILE_LASTEST_VERSION))
            {
                this._lastedVersion = reader.ReadLine();
                if (!String.IsNullOrEmpty(this._lastedVersion) && String.Compare(this._lastedVersion, currentVersion) != 0)
                {
                    this._runUpdate = true;
                }
            }
        }

        /// <summary>
        /// CheckUpdateVersion_RunWorkerCompleted
        /// </summary>
        private void bgwCheckUpdateVersion_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            if (e.Error != null)
            {
                return;
            }

            if (!this._runUpdate)
            {
                return;
            }

            if (this.ShowMessageQuestion(Model.MesseageCode.MES_UPDATE_QUESTION, new string[] { this._lastedVersion }) == DialogResult.No) return;
            this.bgwDownload.RunWorkerAsync();
        }

        /// <summary>
        /// Download_DoWork
        /// </summary>
        private void bgwDownload_DoWork(object sender, DoWorkEventArgs e)
        {
            using (var client = new WebClient())
            {
                using (var file = File.Create(FILE_SOURCE))
                {
                    var bytes = client.DownloadData(Model.Constant.URL_SOURCE_FILE);
                    file.Write(bytes, 0, bytes.Length);
                }
            }
        }

        /// <summary>
        /// Download_RunWorkerCompleted
        /// </summary>
        private void bgwDownload_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                this.ShowMessageInfo(Model.MesseageCode.MES_UPDATE_FAILED);
                return;
            }

            System.Diagnostics.Process.Start(PROCESS_AUTO_UPDATE);
        }

        #endregion

        #region Auto Send Log File

        /// <summary>
        /// SendLog_DoWork
        /// </summary>
        private void bgwSendLog_DoWork(object sender, DoWorkEventArgs e)
        {
            //Check exist log file
            string logfile = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Model.Constant.LOG_FILE);
            if (File.Exists(logfile))
            {
                //Send email
                Service.Email emailSv = new Service.Email();
                emailSv.SendMail(logfile);

                //Delete File
                File.Delete(logfile);
            }
        }

        /// <summary>
        /// endLog_RunWorkerCompleted
        /// </summary>
        private void bgwSendLog_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }
        }

        #endregion
    }
}

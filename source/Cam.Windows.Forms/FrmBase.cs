using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Resources;
using System.Globalization;
using System.IO;

namespace Cam.Windows.Forms
{

    /// <summary>
    /// From base
    /// </summary>
    public partial class FrmBase : Form
    {
        #region Variables
        
        protected ResourceManager _resourceManager;
        protected CultureInfo _culture;
        
        #endregion

        #region Public Methods

        /// <summary>s
        /// Contructor
        /// </summary>
        public FrmBase()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Show messeage Info
        /// </summary>
        /// <param name="messageCode">messageCode</param>
        /// <param name="paramsArray">paramsArray</param>
        public DialogResult ShowMessageInfo(Model.MesseageCode messageCode, String[] paramsArray = null)
        {

            String message = this._resourceManager.GetString(messageCode.ToString(), this._culture);
            if (paramsArray != null)
            {

                StringBuilder sb = new StringBuilder();
                sb.AppendFormat(message, paramsArray);
                message = sb.ToString();
            }

            return MessageBox.Show(message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information); ;
        }

        /// <summary>
        /// Show messeage Question
        /// </summary>
        /// <param name="messageCode">messageCode</param>
        /// <param name="paramsArray">paramsArray</param>
        public DialogResult ShowMessageQuestion(Model.MesseageCode messageCode, String[] paramsArray = null)
        {

            String message = this._resourceManager.GetString(messageCode.ToString(), this._culture);
            if (paramsArray != null)
            {

                StringBuilder sb = new StringBuilder();
                sb.AppendFormat(message, paramsArray);
                message = sb.ToString();
            }

            return MessageBox.Show(message, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information); ;
        }

        /// <summary>
        /// Init data for Char Type Combobox
        /// </summary>
        /// <param name="cmbCharType">Combobox</param>
        /// <param name="isAll">get all data</param>
        /// <param name="charType">charType selected</param>
        public void InitCmbCharType(System.Windows.Forms.ComboBox cmbCharType, bool isAll = true, Cam.Model.Enum charType = Model.Enum.FontYapata)
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("value");
            dt.Columns.Add("display");

            foreach (Model.Enum val in Enum.GetValues(typeof(Model.Enum)))
            {
                if (!isAll)
                {
                    if (val == charType) continue;
                    if ((charType == Model.Enum.TransCamEFEO || charType == Model.Enum.TransInrasara) && val == Model.Enum.TransKawomTT) continue;
                }

                DataRow item = dt.NewRow();
                item["value"] = (int)val;
                item["display"] = this.GetDisplayDataType(val);

                dt.Rows.Add(item);
            }

            cmbCharType.DataSource = dt;
            cmbCharType.DisplayMember = "display";
            cmbCharType.ValueMember = "value";

            //Set default selected value
            if (!isAll)
            {
                switch (charType)
                {
                    case Model.Enum.FontGilaiPraong:
                    case Model.Enum.FontYapata:
                    case Model.Enum.FontCamEFEO:
                        cmbCharType.SelectedValue = (int)Model.Enum.TransCamEFEO;
                        break;

                    default:
                        cmbCharType.SelectedValue = (int)Model.Enum.FontYapata;
                        break;
                }
            }
        }

        /// <summary>
        /// Get file from Resource
        /// </summary>
        /// <param name="FileName">FileName</param>
        public Stream GetFile(String FileName)
        {
            return this.GetType().Assembly.GetManifestResourceStream("Cam.Windows.Forms.XML." + FileName);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Get display data type
        /// </summary>
        /// <param name="value">data type</param>
        private String GetDisplayDataType(Model.Enum value)
        {
            switch (value)
            {
                case Model.Enum.FontGilaiPraong:
                    return this._resourceManager.GetString("ENUM_AKHAR_THRAH_GILAIPRAONG", this._culture);

                case Model.Enum.FontYapata:
                    return this._resourceManager.GetString("ENUM_AKHAR_THRAH_JAPATA", this._culture);

                case Model.Enum.FontCamEFEO:
                    return this._resourceManager.GetString("ENUM_AKHAR_THRAH_EFEO", this._culture);

                case Model.Enum.FontKTT:
                    return this._resourceManager.GetString("ENUM_AKHAR_THRAH_KTT", this._culture);

                case Model.Enum.FontUniCamKur:
                    return this._resourceManager.GetString("ENUM_AKHAR_THRAH_UNICAMKUR", this._culture);

                case Model.Enum.FontUniCamVN:
                    return this._resourceManager.GetString("ENUM_AKHAR_THRAH_UNICAMVN", this._culture);

                case Model.Enum.TransInrasara:
                    return this._resourceManager.GetString("ENUM_INRASARA", this._culture);

                case Model.Enum.TransKawomTT:
                    return this._resourceManager.GetString("ENUM_KTT", this._culture);
               
               
                default:
                    return this._resourceManager.GetString("ENUM_EFEO", this._culture);
            }
        }

        #endregion

    }
}

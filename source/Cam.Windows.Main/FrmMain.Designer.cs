using System.Drawing;
namespace Cam.Windows.Main
{
    partial class FrmMain
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
            this.btnClear = new System.Windows.Forms.Button();
            this.cmbSource = new System.Windows.Forms.ComboBox();
            this.cmbDestination = new System.Windows.Forms.ComboBox();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.txtDestination = new System.Windows.Forms.TextBox();
            this.btnConvert = new System.Windows.Forms.Button();
            this.lblSize1 = new System.Windows.Forms.Label();
            this.lblSize2 = new System.Windows.Forms.Label();
            this.rdoCam = new System.Windows.Forms.RadioButton();
            this.rdoEnglish = new System.Windows.Forms.RadioButton();
            this.rdoVietnamese = new System.Windows.Forms.RadioButton();
            this.bgwCheckUpdateVersion = new System.ComponentModel.BackgroundWorker();
            this.bgwDownload = new System.ComponentModel.BackgroundWorker();
            this.bgwSendLog = new System.ComponentModel.BackgroundWorker();
            this.cmbSize1 = new System.Windows.Forms.ComboBox();
            this.cmbSize2 = new System.Windows.Forms.ComboBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblMinimize = new System.Windows.Forms.Label();
            this.lblClose = new System.Windows.Forms.Label();
            this.lblFrom = new System.Windows.Forms.Label();
            this.lblTo = new System.Windows.Forms.Label();
            this.lnkWeb = new System.Windows.Forms.LinkLabel();
            this.btnSwap = new System.Windows.Forms.Button();
            this.lnkHelp = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(186)))), ((int)(((byte)(131)))));
            this.btnClear.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(539, 341);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(343, 40);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "Xóa";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // cmbSource
            // 
            this.cmbSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSource.FormattingEnabled = true;
            this.cmbSource.IntegralHeight = false;
            this.cmbSource.Location = new System.Drawing.Point(167, 305);
            this.cmbSource.Name = "cmbSource";
            this.cmbSource.Size = new System.Drawing.Size(251, 22);
            this.cmbSource.TabIndex = 1;
            this.cmbSource.SelectedValueChanged += new System.EventHandler(this.cmbSource_SelectedValueChanged);
            // 
            // cmbDestination
            // 
            this.cmbDestination.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDestination.FormattingEnabled = true;
            this.cmbDestination.Location = new System.Drawing.Point(167, 351);
            this.cmbDestination.Name = "cmbDestination";
            this.cmbDestination.Size = new System.Drawing.Size(251, 22);
            this.cmbDestination.TabIndex = 3;
            this.cmbDestination.SelectedIndexChanged += new System.EventHandler(this.cmbDestination_SelectedIndexChanged);
            // 
            // txtSource
            // 
            this.txtSource.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.txtSource.Location = new System.Drawing.Point(15, 50);
            this.txtSource.Multiline = true;
            this.txtSource.Name = "txtSource";
            this.txtSource.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSource.Size = new System.Drawing.Size(870, 230);
            this.txtSource.TabIndex = 0;
            // 
            // txtDestination
            // 
            this.txtDestination.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(248)))));
            this.txtDestination.Location = new System.Drawing.Point(15, 401);
            this.txtDestination.Multiline = true;
            this.txtDestination.Name = "txtDestination";
            this.txtDestination.ReadOnly = true;
            this.txtDestination.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtDestination.Size = new System.Drawing.Size(870, 209);
            this.txtDestination.TabIndex = 8;
            this.txtDestination.TabStop = false;
            // 
            // btnConvert
            // 
            this.btnConvert.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(169)))), ((int)(((byte)(45)))));
            this.btnConvert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConvert.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.btnConvert.ForeColor = System.Drawing.Color.White;
            this.btnConvert.Location = new System.Drawing.Point(539, 295);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(343, 40);
            this.btnConvert.TabIndex = 5;
            this.btnConvert.Text = "Chuyển Đổi";
            this.btnConvert.UseVisualStyleBackColor = false;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // lblSize1
            // 
            this.lblSize1.AutoSize = true;
            this.lblSize1.BackColor = System.Drawing.Color.Transparent;
            this.lblSize1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSize1.Location = new System.Drawing.Point(427, 306);
            this.lblSize1.Name = "lblSize1";
            this.lblSize1.Size = new System.Drawing.Size(33, 18);
            this.lblSize1.TabIndex = 7;
            this.lblSize1.Text = "Size";
            // 
            // lblSize2
            // 
            this.lblSize2.AutoSize = true;
            this.lblSize2.BackColor = System.Drawing.Color.Transparent;
            this.lblSize2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSize2.Location = new System.Drawing.Point(427, 354);
            this.lblSize2.Name = "lblSize2";
            this.lblSize2.Size = new System.Drawing.Size(33, 18);
            this.lblSize2.TabIndex = 7;
            this.lblSize2.Text = "Size";
            // 
            // rdoCam
            // 
            this.rdoCam.AutoSize = true;
            this.rdoCam.BackColor = System.Drawing.Color.Transparent;
            this.rdoCam.Checked = true;
            this.rdoCam.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoCam.ForeColor = System.Drawing.Color.White;
            this.rdoCam.Location = new System.Drawing.Point(35, 6);
            this.rdoCam.Name = "rdoCam";
            this.rdoCam.Size = new System.Drawing.Size(78, 18);
            this.rdoCam.TabIndex = 0;
            this.rdoCam.TabStop = true;
            this.rdoCam.Text = "Xap Cam";
            this.rdoCam.UseVisualStyleBackColor = false;
            this.rdoCam.CheckedChanged += new System.EventHandler(this.rdoCam_CheckedChanged);
            // 
            // rdoEnglish
            // 
            this.rdoEnglish.AutoSize = true;
            this.rdoEnglish.BackColor = System.Drawing.Color.Transparent;
            this.rdoEnglish.ForeColor = System.Drawing.Color.White;
            this.rdoEnglish.Location = new System.Drawing.Point(115, 6);
            this.rdoEnglish.Name = "rdoEnglish";
            this.rdoEnglish.Size = new System.Drawing.Size(62, 18);
            this.rdoEnglish.TabIndex = 1;
            this.rdoEnglish.Text = "English";
            this.rdoEnglish.UseVisualStyleBackColor = false;
            this.rdoEnglish.CheckedChanged += new System.EventHandler(this.rdoCam_CheckedChanged);
            // 
            // rdoVietnamese
            // 
            this.rdoVietnamese.AutoSize = true;
            this.rdoVietnamese.BackColor = System.Drawing.Color.Transparent;
            this.rdoVietnamese.ForeColor = System.Drawing.Color.White;
            this.rdoVietnamese.Location = new System.Drawing.Point(181, 6);
            this.rdoVietnamese.Name = "rdoVietnamese";
            this.rdoVietnamese.Size = new System.Drawing.Size(82, 18);
            this.rdoVietnamese.TabIndex = 2;
            this.rdoVietnamese.Text = "Tiếng Việt";
            this.rdoVietnamese.UseVisualStyleBackColor = false;
            this.rdoVietnamese.CheckedChanged += new System.EventHandler(this.rdoCam_CheckedChanged);
            // 
            // bgwCheckUpdateVersion
            // 
            this.bgwCheckUpdateVersion.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwCheckUpdateVersion_DoWork);
            this.bgwCheckUpdateVersion.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwCheckUpdateVersion_RunWorkerCompleted);
            // 
            // bgwDownload
            // 
            this.bgwDownload.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwDownload_DoWork);
            this.bgwDownload.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwDownload_RunWorkerCompleted);
            // 
            // bgwSendLog
            // 
            this.bgwSendLog.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwSendLog_DoWork);
            this.bgwSendLog.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwSendLog_RunWorkerCompleted);
            // 
            // cmbSize1
            // 
            this.cmbSize1.FormattingEnabled = true;
            this.cmbSize1.Location = new System.Drawing.Point(481, 305);
            this.cmbSize1.Name = "cmbSize1";
            this.cmbSize1.Size = new System.Drawing.Size(39, 22);
            this.cmbSize1.TabIndex = 2;
            this.cmbSize1.TextChanged += new System.EventHandler(this.txtSize1_TextChanged);
            this.cmbSize1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSize1_KeyPress);
            // 
            // cmbSize2
            // 
            this.cmbSize2.FormattingEnabled = true;
            this.cmbSize2.Location = new System.Drawing.Point(481, 351);
            this.cmbSize2.Name = "cmbSize2";
            this.cmbSize2.Size = new System.Drawing.Size(39, 22);
            this.cmbSize2.TabIndex = 4;
            this.cmbSize2.TextChanged += new System.EventHandler(this.txtSize2_TextChanged);
            this.cmbSize2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSize2_KeyPress);
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(15, 7);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(870, 17);
            this.lblTitle.TabIndex = 13;
            this.lblTitle.Text = "XALIH AKHAR CAM";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlTitle
            // 
            this.pnlTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(169)))), ((int)(((byte)(45)))));
            this.pnlTitle.Controls.Add(this.panel1);
            this.pnlTitle.Controls.Add(this.lblMinimize);
            this.pnlTitle.Controls.Add(this.lblClose);
            this.pnlTitle.Controls.Add(this.rdoVietnamese);
            this.pnlTitle.Controls.Add(this.rdoCam);
            this.pnlTitle.Controls.Add(this.rdoEnglish);
            this.pnlTitle.Controls.Add(this.lblTitle);
            this.pnlTitle.Location = new System.Drawing.Point(0, 0);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(900, 31);
            this.pnlTitle.TabIndex = 15;
            this.pnlTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlTitle_MouseDown);
            this.pnlTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlTitle_MouseMove);
            this.pnlTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlTitle_MouseUp);
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::Cam.Windows.Main.Properties.Resources.icon;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Location = new System.Drawing.Point(0, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(27, 26);
            this.panel1.TabIndex = 19;
            // 
            // lblMinimize
            // 
            this.lblMinimize.AutoSize = true;
            this.lblMinimize.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.lblMinimize.ForeColor = System.Drawing.Color.White;
            this.lblMinimize.Location = new System.Drawing.Point(843, 5);
            this.lblMinimize.Name = "lblMinimize";
            this.lblMinimize.Size = new System.Drawing.Size(18, 18);
            this.lblMinimize.TabIndex = 18;
            this.lblMinimize.Text = "_";
            this.lblMinimize.Click += new System.EventHandler(this.lblMinimize_Click);
            this.lblMinimize.MouseEnter += new System.EventHandler(this.lblMinimize_MouseEnter);
            this.lblMinimize.MouseLeave += new System.EventHandler(this.lblMinimize_MouseLeave);
            // 
            // lblClose
            // 
            this.lblClose.AutoSize = true;
            this.lblClose.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.lblClose.ForeColor = System.Drawing.Color.White;
            this.lblClose.Location = new System.Drawing.Point(869, 5);
            this.lblClose.Name = "lblClose";
            this.lblClose.Size = new System.Drawing.Size(18, 18);
            this.lblClose.TabIndex = 18;
            this.lblClose.Text = "X";
            this.lblClose.Click += new System.EventHandler(this.lblClose_Click);
            this.lblClose.MouseEnter += new System.EventHandler(this.lblMinimize_MouseEnter);
            this.lblClose.MouseLeave += new System.EventHandler(this.lblMinimize_MouseLeave);
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.BackColor = System.Drawing.Color.Transparent;
            this.lblFrom.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFrom.Location = new System.Drawing.Point(107, 309);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(46, 18);
            this.lblFrom.TabIndex = 7;
            this.lblFrom.Text = "From";
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.BackColor = System.Drawing.Color.Transparent;
            this.lblTo.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTo.Location = new System.Drawing.Point(107, 351);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(26, 18);
            this.lblTo.TabIndex = 7;
            this.lblTo.Text = "To";
            // 
            // lnkWeb
            // 
            this.lnkWeb.AutoSize = true;
            this.lnkWeb.Location = new System.Drawing.Point(732, 624);
            this.lnkWeb.Name = "lnkWeb";
            this.lnkWeb.Size = new System.Drawing.Size(156, 14);
            this.lnkWeb.TabIndex = 16;
            this.lnkWeb.TabStop = true;
            this.lnkWeb.Text = "www.tanginpantangin.com";
            this.lnkWeb.Click += new System.EventHandler(this.lnkWeb_Click);
            // 
            // btnSwap
            // 
            this.btnSwap.BackgroundImage = global::Cam.Windows.Main.Properties.Resources.Swap;
            this.btnSwap.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSwap.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnSwap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSwap.Location = new System.Drawing.Point(83, 309);
            this.btnSwap.Name = "btnSwap";
            this.btnSwap.Size = new System.Drawing.Size(18, 60);
            this.btnSwap.TabIndex = 9;
            this.btnSwap.TabStop = false;
            this.btnSwap.UseVisualStyleBackColor = false;
            this.btnSwap.Click += new System.EventHandler(this.btnSwap_Click);
            this.btnSwap.MouseEnter += new System.EventHandler(this.btnSwap_MouseEnter);
            this.btnSwap.MouseLeave += new System.EventHandler(this.btnSwap_MouseLeave);
            // 
            // lnkHelp
            // 
            this.lnkHelp.AutoSize = true;
            this.lnkHelp.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkHelp.Location = new System.Drawing.Point(15, 624);
            this.lnkHelp.Name = "lnkHelp";
            this.lnkHelp.Size = new System.Drawing.Size(31, 14);
            this.lnkHelp.TabIndex = 16;
            this.lnkHelp.TabStop = true;
            this.lnkHelp.Text = "Help";
            this.lnkHelp.Click += new System.EventHandler(this.lnkHelp_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(616, 624);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 14);
            this.label1.TabIndex = 17;
            this.label1.Text = "Copyright © 2013";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(900, 650);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lnkHelp);
            this.Controls.Add(this.lnkWeb);
            this.Controls.Add(this.cmbSize2);
            this.Controls.Add(this.cmbSize1);
            this.Controls.Add(this.lblSize2);
            this.Controls.Add(this.lblTo);
            this.Controls.Add(this.lblFrom);
            this.Controls.Add(this.lblSize1);
            this.Controls.Add(this.txtSource);
            this.Controls.Add(this.txtDestination);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.btnSwap);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.cmbDestination);
            this.Controls.Add(this.cmbSource);
            this.Controls.Add(this.pnlTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chuyen tu";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.Shown += new System.EventHandler(this.FrmMain_Shown);
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ComboBox cmbSource;
        private System.Windows.Forms.ComboBox cmbDestination;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.TextBox txtDestination;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.Label lblSize1;
        private System.Windows.Forms.Label lblSize2;
        private System.Windows.Forms.RadioButton rdoCam;
        private System.Windows.Forms.RadioButton rdoEnglish;
        private System.Windows.Forms.RadioButton rdoVietnamese;
        private System.ComponentModel.BackgroundWorker bgwCheckUpdateVersion;
        private System.ComponentModel.BackgroundWorker bgwDownload;
        private System.ComponentModel.BackgroundWorker bgwSendLog;
        private System.Windows.Forms.ComboBox cmbSize1;
        private System.Windows.Forms.ComboBox cmbSize2;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.Button btnSwap;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.LinkLabel lnkWeb;
        private System.Windows.Forms.Label lblClose;
        private System.Windows.Forms.Label lblMinimize;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel lnkHelp;
        private System.Windows.Forms.Label label1;
    }
}


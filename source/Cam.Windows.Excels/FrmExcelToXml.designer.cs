namespace Cam.Windows.Excels
{
    partial class FrmExcelToXml
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
            this.btnCamToKey = new System.Windows.Forms.Button();
            this.btnKeyToRumi = new System.Windows.Forms.Button();
            this.btnRumiToKey = new System.Windows.Forms.Button();
            this.btnValidRumi = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCamToKey
            // 
            this.btnCamToKey.Location = new System.Drawing.Point(44, 36);
            this.btnCamToKey.Name = "btnCamToKey";
            this.btnCamToKey.Size = new System.Drawing.Size(235, 35);
            this.btnCamToKey.TabIndex = 0;
            this.btnCamToKey.Text = "Akhar Cam to Key code...";
            this.btnCamToKey.UseVisualStyleBackColor = true;
            this.btnCamToKey.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // btnKeyToRumi
            // 
            this.btnKeyToRumi.Location = new System.Drawing.Point(44, 105);
            this.btnKeyToRumi.Name = "btnKeyToRumi";
            this.btnKeyToRumi.Size = new System.Drawing.Size(235, 35);
            this.btnKeyToRumi.TabIndex = 1;
            this.btnKeyToRumi.Text = "KeyCode to Chuyển tự...";
            this.btnKeyToRumi.UseVisualStyleBackColor = true;
            this.btnKeyToRumi.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // btnRumiToKey
            // 
            this.btnRumiToKey.Location = new System.Drawing.Point(44, 172);
            this.btnRumiToKey.Name = "btnRumiToKey";
            this.btnRumiToKey.Size = new System.Drawing.Size(235, 35);
            this.btnRumiToKey.TabIndex = 2;
            this.btnRumiToKey.Text = "Chuyển tự To Key Code...";
            this.btnRumiToKey.UseVisualStyleBackColor = true;
            this.btnRumiToKey.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // btnValidRumi
            // 
            this.btnValidRumi.Location = new System.Drawing.Point(44, 237);
            this.btnValidRumi.Name = "btnValidRumi";
            this.btnValidRumi.Size = new System.Drawing.Size(235, 35);
            this.btnValidRumi.TabIndex = 3;
            this.btnValidRumi.Text = "Valid Chuyển tự Char...";
            this.btnValidRumi.UseVisualStyleBackColor = true;
            this.btnValidRumi.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // FrmExcelToXml
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 333);
            this.Controls.Add(this.btnValidRumi);
            this.Controls.Add(this.btnRumiToKey);
            this.Controls.Add(this.btnKeyToRumi);
            this.Controls.Add(this.btnCamToKey);
            this.Name = "FrmExcelToXml";
            this.Text = "Convert Excel To XML";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCamToKey;
        private System.Windows.Forms.Button btnKeyToRumi;
        private System.Windows.Forms.Button btnRumiToKey;
        private System.Windows.Forms.Button btnValidRumi;
    }
}


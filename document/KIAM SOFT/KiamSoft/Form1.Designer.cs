namespace KiamSoft
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
            this.rtxtDich = new System.Windows.Forms.RichTextBox();
            this.rtxtNguon = new System.Windows.Forms.RichTextBox();
            this.cbNguon = new System.Windows.Forms.ComboBox();
            this.cbDich = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSalih = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rtxtDich
            // 
            this.rtxtDich.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtxtDich.Location = new System.Drawing.Point(470, 84);
            this.rtxtDich.Name = "rtxtDich";
            this.rtxtDich.Size = new System.Drawing.Size(366, 324);
            this.rtxtDich.TabIndex = 0;
            this.rtxtDich.Text = "fsfsfs";
            // 
            // rtxtNguon
            // 
            this.rtxtNguon.Font = new System.Drawing.Font("Akhar Thrah 1", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtxtNguon.Location = new System.Drawing.Point(12, 84);
            this.rtxtNguon.Name = "rtxtNguon";
            this.rtxtNguon.Size = new System.Drawing.Size(356, 333);
            this.rtxtNguon.TabIndex = 0;
            this.rtxtNguon.Text = "jlN -n< pl] k~w h~% tm=k b`K ymN. rY un} \\d] mrT -d\" pr%N b$x% c\'F% -A<L it%\njlN " +
                "aAR jy\' kbw";
            // 
            // cbNguon
            // 
            this.cbNguon.FormattingEnabled = true;
            this.cbNguon.Items.AddRange(new object[] {
            "(Chuyển tự) Rumi",
            "(Chuyển tự ) Aymonier",
            "(Chuyển tự ) Quang Can",
            "(Akhar Thrah) Font Đàng Quyết",
            "(Akhar Thrah) Font Gilaipraung.com",
            "(Akhar Thrah) Font EFEO",
            "(Akhar Thrah) Font BBSSCC"});
            this.cbNguon.Location = new System.Drawing.Point(65, 57);
            this.cbNguon.Name = "cbNguon";
            this.cbNguon.Size = new System.Drawing.Size(154, 21);
            this.cbNguon.TabIndex = 1;
            // 
            // cbDich
            // 
            this.cbDich.FormattingEnabled = true;
            this.cbDich.Items.AddRange(new object[] {
            "(Chuyển tự) Rumi",
            "(Chuyển tự ) Aymonier",
            "(Chuyển tự ) Quang Can",
            "(Akhar Thrah) Font Đàng Quyết",
            "(Akhar Thrah) Font Gilaipraung.com",
            "(Akhar Thrah) Font EFEO",
            "(Akhar Thrah) Font BBSSCC"});
            this.cbDich.Location = new System.Drawing.Point(515, 57);
            this.cbDich.Name = "cbDich";
            this.cbDich.Size = new System.Drawing.Size(154, 21);
            this.cbDich.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Nguồn:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(467, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Đích:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)), true);
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(245, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(332, 27);
            this.label3.TabIndex = 3;
            this.label3.Text = "SOFTWARE PASALIH INÂ AKHAR";
            // 
            // btnSalih
            // 
            this.btnSalih.Location = new System.Drawing.Point(386, 227);
            this.btnSalih.Name = "btnSalih";
            this.btnSalih.Size = new System.Drawing.Size(65, 23);
            this.btnSalih.TabIndex = 4;
            this.btnSalih.Text = ">>";
            this.btnSalih.UseVisualStyleBackColor = true;
            this.btnSalih.Click += new System.EventHandler(this.btnSalih_Click);
            this.btnSalih.Enter += new System.EventHandler(this.btnSalih_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(235, 57);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 5;
            this.btnBrowse.Text = "Browes";
            this.btnBrowse.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 449);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.btnSalih);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbDich);
            this.Controls.Add(this.cbNguon);
            this.Controls.Add(this.rtxtNguon);
            this.Controls.Add(this.rtxtDich);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtxtDich;
        private System.Windows.Forms.RichTextBox rtxtNguon;
        private System.Windows.Forms.ComboBox cbNguon;
        private System.Windows.Forms.ComboBox cbDich;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSalih;
        private System.Windows.Forms.Button btnBrowse;
    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KiamSoft.Test_Demo
{
    public partial class Test1 : Form
    {
        public Test1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string string1 = textBox1.Text;
            string temp1 = "";
            string temp2 = "";
            string1.Trim();
            //for (int i = 0; i < string1.Length; i++)
            //{
            if (string1[string1.Length - 1] == 'n' && string1[string1.Length - 2] == 'l')
                temp1 = string1.Insert(string1.Length - 1, "a");//jlan
            if (string1[0] == 'j' && string1[1] == 'l')
                temp1 = temp1.Insert(1,"a");
            //}

            //for (int i = 0; i < string1.Length; i++)
            //{
            //    if (temp1[i] == 'l')
            //        temp2 = temp1.Insert(i + 1, "a");
            //}
            textBox2.Text = temp1;
        }
    }
}

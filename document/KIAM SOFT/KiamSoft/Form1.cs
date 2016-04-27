using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;

namespace KiamSoft
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //string[] arr_langlikuk = new string[] { "k", "g", "c", "j", "t", "d", "p", "b", "m", "y", "r", "l", "x", "h", "F", "s" };
        //string[] arr_akhar_matai = new string[] { "K", "T", "P", "ñ", "H", "N", "R", "L", "C", "U", "/", "X", "w", "Y", "'" };
        //string[] arr_ina_akhar = new string[] { "k", "A","g", "G","z","Z","c","S", "j","J","v","V","W","t","E", "d", "D","n","q","Q","p","f", "b","B", "m","M", "y", "r", "l", "x", "h", "F", "s" };
        string[] arr_langlikuk = new string[] { "k", "g", "c", "j", "t", "d", "p", "b", "m", "y", "r", "l", "x", "h", "s", "u","a","F" };//"F","a",

        string[] arr_ina_akhar = new string[] { "k", "g", "c", "j", "t", "d", "n", "p", "b", "m", "y", "r", "l", "w", "x", "h", "s" };
        //Bỏ vì trùng: "kh","gh","ch","jh","th","dh","ph","bh","ng", "ny", "nj","nd","mb","pp", 
        string[] arr_akhar_matai = new string[] { "k", "t", "p", "g", "h", "n", "r", "l", "c", "x", "w", "y", "m" };//"ng",


        Hashtable H_sa_sap = new Hashtable();// tách các từ trong đoạn ra xử lý riêng
        Hashtable h = new Hashtable();//phụ âm có vần theo sau
        Hashtable h1 = new Hashtable();//phụ âm đứng 1 mình (chỉ có paluw or akhar mâtai)
        private void Form1_Load(object sender, EventArgs e)
        {       
            StreamReader sr = new StreamReader("../../Dict_data/Data_pasalih.txt");
            while (!sr.EndOfStream)
            {
                string dong = sr.ReadLine();
                string[] arr = dong.Split(':');
                string tu_tieng_Cham = arr[0];
                string tu_tieng_Viet = arr[1];
                string tu_TV_don_gian=arr[2];
                h.Add(tu_tieng_Cham, tu_tieng_Viet);
                h1.Add(tu_tieng_Cham,tu_TV_don_gian);
            }
            sr.Close();
        }

        private void btnSalih_Click(object sender, EventArgs e)
        {            
            //rtxtDich.Text = rtxtNguon.Text;
            //string akhar_Thrah = rtxtNguon.Text;
            //if (h[akhar_Thrah] != null)
            //    rtxtDich.Text = h[akhar_Thrah].ToString();
            string text_input=rtxtNguon.Text;
            string[] arr_text_input = text_input.Split(' ');//Khoảng trắng
            string kq_abih_sap = "";
            for (int i = 0; i < arr_text_input.Length; i++)//Xử lý từng từ cách nhau khoảng trắng " "
            {
                string kq_sa_sap = "";
                for (int j = 0; j < arr_text_input[i].Length; j++)
                {
                    //if (Is_Akhar_matai(arr_text_input[i][j]))// && Is_Ina_akhar(arr_text_input[i][j+1]))//jlN=jln
                    kq_sa_sap += h1[arr_text_input[i][j].ToString()];//N->n tại ni rùi
                    //kq_sa_sap.Insert(j+1,"a");
                }
                kq_abih_sap += Refix_word(kq_sa_sap) + " ";
                //kq_abih_sap += (kq_sa_sap) + " ";

            }
            rtxtDich.Text = kq_abih_sap;
        }
        
        //Phuong thuc ktra và sửa 
        private string Refix_word(string str)
        {
            string result_string = "";//nếu length!=3
            string temp_string = "";

            #region//------- Van 'O' --------
            if (Ktra_van(str)==1)
            {
                int vtri_o = str.IndexOf("-");
                string arr1 = str.Substring(0, vtri_o-0);
                string arr2=str.Substring(vtri_o,str.Length-vtri_o);
                string arr2_temp = "";
                //Split--> arr1, arr2
                //if arr1==null and !=null
                //----- Edit arr2 --------
                if (arr2.Length == 3 && arr2[arr2.Length - 1].ToString() == "%")
                    arr2_temp = arr2[1] + "o";
                if (arr2.Length == 3 && Is_Akhar_matai(arr2[arr2.Length - 1]))
                    arr2_temp = arr2[1] + "o" + arr2[arr2.Length - 1];
                if (arr2.Length == 4 && arr2[arr2.Length - 2].ToString() == "%")//co TH nay ko? t-g%K
                    arr2_temp = arr2[1] + "o" + arr2[arr2.Length - 1];
                //--------
                if (arr1 == "")//(str[0].ToString() == "-")
                {
                    result_string = arr2_temp;
                }
                else
                {
                    if (arr1.Length == 1)
                        result_string = arr1[0] + "a" + arr2_temp;
                    if (arr1.Length == 2)
                        result_string = arr1[0] + "a" + arr1[1] + "a"+arr2_temp;
                }
                return result_string;
            }
            #endregion
            //------- Van 'I', 'EI' ---------
            if (Ktra_van(str) == 2)
            {
                if (Hu_character_lei("{", str)&&str[str.Length-1].ToString()!="{")//"{" khong co nghia khi dung cuoi cung
                {
                    if (str.Length == 3)//m{K
                        result_string = str[0] + "i" + str[2];
                    if (str.Length == 4)//rm{K
                        result_string = str[0] + "a"+str[1]+"i" + str[3];
                }
                //--
                if (Hu_character_lei("}", str))
                {
                    if(str.Length==2)//n}
                        result_string=str[0]+"i";
                    if (str.Length == 3)
                    {
                        if (Is_Lang_likuk(str[0]) && Is_Ina_akhar(str[1]) && str[2].ToString() == "}")//km}
                            result_string = str[0] + "a" + str[1] + "i";
                        if(str[1].ToString()=="}")//l}K
                            result_string=str[0]+"i"+str[2];
                    }
                    if (str.Length == 4)
                    {
                        result_string = str[0] + "a" + str[1] + "i" + str[3];
                    }
                }
                //--
                if(Hu_character_lei("[",str))//Wrong with ("A["=khim)#(kh[) ???
                {
                    if(str.Length==2)
                        result_string=str[0]+"im";
                    if(str.Length==3&&Is_Lang_likuk(str[0])&&Is_Ina_akhar(str[1]))
                        result_string=str[0]+"a"+str[1]+"im";
                }
                //--
                if (Hu_character_lei("]", str))//Wrong with ("A["=khim)#(kh[) ???
                {
                    if (str.Length == 2)
                        result_string = str[0] + "ei";
                    if (str.Length == 3 && Is_Lang_likuk(str[0]) && Is_Ina_akhar(str[1]))
                        result_string = str[0] + "a" + str[1] + "ei";
                }
            }
            #region//------- Van "A" --------
            if (Ktra_van(str) == 0)
            {
                //--------------
                // Neu tu co 3 am tiet
                if (str.Length == 2)
                {
                    if (Is_Akhar_matai(str[str.Length - 1]) && Is_Ina_akhar(str[0]))//wl-->wal
                    {
                        result_string = str.Insert(1, "a");
                    }
                    if (Is_Ina_akhar(str[0]) && str[1].ToString()== "%")
                    {
                        result_string = str[0] + "a";
                    }
                }
                // Neu tu co 3 am tiet
                if (str.Length == 3)
                {
                    //3 am tiet (co 1 "baluw")
                    if (str[1].ToString() == "%")
                    {
                        result_string = str[0] + "a" + str[2];//w%r--> war
                    }
                    else if (Is_Lang_likuk(str[0]) && Is_Ina_akhar(str[1]) && str[2].ToString() == "%")//jk%
                    {
                        result_string = str[0] + "a" + str[1] + "a";
                    }
                    else//3 am tiet binh thuong
                    {
                        if (Is_Akhar_matai(str[str.Length - 1]) && Is_Ina_akhar(str[str.Length - 2]))//jln-->jlan
                        {
                            temp_string = str.Insert(str.Length - 1, "a");
                        }
                        if (Is_Lang_likuk(temp_string[0]) && Is_Ina_akhar(temp_string[1]))//jlan-->jalan
                        {
                            result_string = temp_string.Insert(1, "a");
                        }
                    }
                }
                //4 am tiet (co 1 "baluw")
                if (str.Length == 4 && str[2].ToString() == "%")
                {
                    temp_string = str.Replace('%', 'a');//jl%n --> jlan
                    result_string = temp_string.Insert(1, "a");
                }
            }
            #endregion
            return result_string;
        }
        #region//KTRA langliku; akhar matai; inâ akhar
        //Kiểm tra có phải là ký tự thuộc arr_langlikuk ko?
        private bool Is_Lang_likuk(char c)
        {
            bool result=true;
            for (int i = 0; i < arr_langlikuk.Length; i++)
            {
                if (c.ToString() == arr_langlikuk[i])
                {
                    result= true;
                    break;
                }
                else
                    result = false;
            }
            return result;
        }

        //Kiểm tra có phải là ký tự thuộc arr_akhar_matai ko?
        private bool Is_Akhar_matai(char c)
        {
            bool result = true;
            for (int i = 0; i < arr_akhar_matai.Length; i++)
            {
                if (c.ToString() == arr_akhar_matai[i])
                {
                    result = true;
                    break;
                }
                else
                    result = false;
            }
            return result;
        }

        //Kiểm tra có phải là ký tự thuộc arr_Inâ_akhar ko?
        private bool Is_Ina_akhar(char c)
        {
            bool result = true;
            for (int i = 0; i < arr_ina_akhar.Length; i++)
            {
                if (c.ToString() == arr_ina_akhar[i])
                {
                    result = true;
                    break;
                }
                else
                    result = false;
            }
            return result;
        }
        //------- Function ktra kieu van ------
        //Ktra su ton tai cua ky tu can tim trong chuoi
        private bool Hu_character_lei(string st_duah,string st_amaik)
        {
            bool kq = false;
            int count = 0;
            for (int i = 0; i < st_amaik.Length; i++)
                if (st_amaik[i].ToString() == st_duah)
                {
                    count++;
                    break;
                }
            if (count == 1)
                kq = true;
            else
                kq = false;
            return kq;
        }
        //--
        private int Ktra_van(string str)// xem lai ???
        {
            int van = 0;
            if (Hu_character_lei("-", str))//&& !Hu_character_lei("<", str)
            {
                van = 1;// Van "O"
            }
            else if (Hu_character_lei("{", str) || Hu_character_lei("}", str) 
                || Hu_character_lei("[", str) || Hu_character_lei("]",str))
            {
                van = 2;// Van "I"
            }
            else
            {
                van = 0;//Van "A"
            }
            return van;
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;

namespace Cam.Service
{

    /// <summary>
    /// Paxalih class
    /// </summary>
    public class Paxalih
    {

        #region Variables

        //Fonts to Keycode
        protected Hashtable _waYapataToKeyCode;
        protected Hashtable _gilaiPraongToKeyCode;
        protected Hashtable _camEFEOToKeyCode;
        protected Hashtable _KTTToKeyCode;
        protected Hashtable _uniCamKurToKeyCode;
        protected Hashtable _uniCamVNToKeyCode;

        //Valid chars
        protected Hashtable _validRumiChar;
        protected Hashtable _validInraChar;
        protected Hashtable _validKTTChar;

        //Keycode to Fonts
        protected Hashtable _keyCodeToWaYapata;
        protected Hashtable _keyCodeToGilaiPraong;
        protected Hashtable _keyCodeToCamEFEO;
        protected Hashtable _keyCodeToKTT;
        protected Hashtable _keyCodeToUniCamKur;
        protected Hashtable _keyCodeToUniCamVN;

        //Trans To Akhar Cam
        protected Hashtable _diipToMaTai;
        protected Hashtable _diipToTaKai;
        protected Hashtable _kanaingChars;
        protected Hashtable _kareiCrih;
        protected Hashtable _consonantKareiCrih;
        protected Hashtable _sappaohAngaok;
        protected Hashtable _sappaoh;
        protected Hashtable _takaiLikuk;
        protected Hashtable _vowelLangLikuk;
        protected Hashtable _takaiDaokLikuk;

        #endregion

        #region Public Methods

        /// <summary>
        /// Check is valid input value
        /// </summary>
        /// <param name="data">input data</param>
        /// <param name="dataType">data type</param>
        public bool ValidateInput(ref String data, Model.Enum dataType)
        {
            //Replace enter
            data = data.Replace(Model.Constant.NEW_LINE, " ");

            //Get hashtable
            Hashtable dictionary = this.GetDictionaty(dataType);

            //Split data
            string[] words = data.Split(' ');
            List<string> wordReplaced = new List<string>();

            //Check input by word
            bool ret = true;
            foreach (string word in words)
            {
                string wordCheck = word;
                // is newline character
                if (word == Model.Constant.NEW_LINE || String.IsNullOrEmpty(word))
                {
                    wordReplaced.Add(word);
                    continue;
                }
                if (!this.ValidateWord(ref wordCheck, dataType, dictionary))
                {
                    ret = false;
                }
                wordReplaced.Add(wordCheck);
            }

            data = String.Join(" ", wordReplaced.ToArray());

            return ret;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Paxalih()
        {

            Utility.SetKeyCodeData(ref this._waYapataToKeyCode, ref this._gilaiPraongToKeyCode, ref this._camEFEOToKeyCode, ref this._KTTToKeyCode, ref this._uniCamKurToKeyCode, ref this._uniCamVNToKeyCode,
                                   ref this._keyCodeToWaYapata, ref this._keyCodeToGilaiPraong, ref this._keyCodeToCamEFEO, ref this._keyCodeToKTT, ref this._keyCodeToUniCamKur, ref this._keyCodeToUniCamVN);
            Utility.SetValidTransChar(ref this._validRumiChar, ref this._validInraChar, ref this._validKTTChar);
        }

        /// <summary>
        /// Size keypress event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">KeyPressEventArgs</param>
        public void Size_KeyPress(ComboBox sender, KeyPressEventArgs e)
        {

            if (!char.IsNumber(e.KeyChar))
            {
                e.Handled = true;
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
                return;
            }

            int data;
            if (Int32.TryParse(sender.Text + e.KeyChar, out data))
            {

                if (data > Model.Constant.MAX_FONT_SIZE)
                {
                    e.Handled = true;
                }
            }
        }

        /// <summary>
        /// Size Text changed event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="textControl">text box control</param>
        public void Size_TextChanged(ComboBox sender, TextBox textControl)
        {

            if (String.IsNullOrEmpty(sender.Text)) return;

            Int32 num;
            Int32.TryParse(sender.Text, out num);

            sender.Text = num.ToString();

            if (num >= Model.Constant.MIN_FONT_SIZE && num <= Model.Constant.MAX_FONT_SIZE)
            {
                textControl.Font = new Font(textControl.Font.FontFamily, num);
            }
        }

        /// <summary>
        /// source combobox selected changed
        /// </summary>
        public void combobox_SelectedChanged(Model.Enum dataType, TextBox textControl, ComboBox sizeControl)
        {
            textControl.ForeColor = Color.Black;
            switch (dataType)
            {
                case Model.Enum.FontYapata:
                    textControl.Font = new Font(Model.Constant.FONT_YAPATA_NAME, Model.Constant.DEFAULT_FONT_CAM_SIZE);
                    sizeControl.Text = Model.Constant.DEFAULT_FONT_CAM_SIZE.ToString();
                    break;

                case Model.Enum.FontGilaiPraong:
                    textControl.Font = new Font(Model.Constant.FONT_GILAIPRAONG_NAME, Model.Constant.DEFAULT_FONT_CAM_SIZE);
                    sizeControl.Text = Model.Constant.DEFAULT_FONT_CAM_SIZE.ToString();
                    break;

                case Model.Enum.FontCamEFEO:
                    textControl.Font = new Font(Model.Constant.FONT_CAM_EFEO_NAME, Model.Constant.DEFAULT_FONT_CAM_SIZE);
                    sizeControl.Text = Model.Constant.DEFAULT_FONT_CAM_SIZE.ToString();
                    break;

                case Model.Enum.FontKTT:
                    textControl.Font = new Font(Model.Constant.FONT_KTT_NAME, Model.Constant.DEFAULT_FONT_CAM_SIZE);
                    sizeControl.Text = Model.Constant.DEFAULT_FONT_CAM_SIZE.ToString();
                    break;

                case Model.Enum.FontUniCamKur:
                    textControl.Font = new Font(Model.Constant.FONT_UNI_CAMKUR, Model.Constant.DEFAULT_FONT_CAM_SIZE);
                    sizeControl.Text = Model.Constant.DEFAULT_FONT_CAM_SIZE.ToString();
                    break;

                case Model.Enum.FontUniCamVN:
                    textControl.Font = new Font(Model.Constant.FONT_UNI_CAMVN, Model.Constant.DEFAULT_FONT_CAM_SIZE);
                    sizeControl.Text = Model.Constant.DEFAULT_FONT_CAM_SIZE.ToString();
                    break;

                default:
                    textControl.Font = new Font(Model.Constant.FONT_YUEN_NAME, Model.Constant.DEFAULT_FONT_YUEN_SIZE);
                    sizeControl.Text = Model.Constant.DEFAULT_FONT_YUEN_SIZE.ToString();
                    break;
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Init data converting
        /// </summary>
        /// <param name="sourceType">sourceType</param>
        protected void InitTransToCamData(Model.Enum sourceType)
        {
            this._diipToMaTai = Utility.InitDiipToMaTai();
            this._diipToTaKai = Utility.InitDiipToTaKai(sourceType);
            this._kanaingChars = Utility.InitKanaing();
            this._kareiCrih = Utility.InitKareiCrih(sourceType);
            this._consonantKareiCrih = Utility.InitConsonantKareiCrih();
            this._sappaohAngaok = Utility.InitSapPaohAngaok();
            this._sappaoh = Utility.InitSapPaoh();
            this._takaiLikuk = Utility.InitTakaiLikuk();
            this._vowelLangLikuk = Utility.InitVowelLangLikuk();
            this._takaiDaokLikuk = Utility.InitTakaiDaokLikuk();
        }

        /// <summary>
        /// Get index of last char
        /// </summary>
        /// <param name="list">akhar list</param>
        /// <returns>index of last char</returns>
        protected int GetIndexLastChar(List<Model.AKhar> list)
        {
            int ret = list.Count - 1;

            for (int i = ret; i >= 0; i--)
            {

                Model.AKhar akhar = list[i];
                if (!this.Check_DauCau(akhar))
                {
                    ret = i;
                    break;
                }
            }

            return ret;
        }

        /// <summary>
        /// Get index of akhar diip
        /// </summary>
        /// <param name="list">akhar list</param>
        /// <returns>index of akhar diip</returns>
        protected int GetIndexAkharDiip(List<Model.AKhar> list)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {

                Model.AKhar akhar = list[i];
                if (this.Check_InaAkhar_NguyenAm(akhar) || this.Check_InaAkhar_PhuAm(akhar))
                {
                    return i;
                }
            }

            return list.Count;
        }

        /// <summary>
        /// Get Dictionary
        /// </summary>
        /// <param name="dataType">data type</param>
        protected Hashtable GetDictionaty(Model.Enum dataType)
        {
            Hashtable ret;
            switch (dataType)
            {
                case Model.Enum.FontYapata:
                    ret = this._waYapataToKeyCode;
                    break;

                case Model.Enum.FontGilaiPraong:
                    ret = this._gilaiPraongToKeyCode;
                    break;

                case Model.Enum.FontCamEFEO:
                    ret = this._camEFEOToKeyCode;
                    break;

                case Model.Enum.FontKTT:
                    ret = this._KTTToKeyCode;
                    break;

                case Model.Enum.FontUniCamKur:
                    ret = this._uniCamKurToKeyCode;
                    break;

                case Model.Enum.FontUniCamVN:
                    ret = this._uniCamVNToKeyCode;
                    break;

                case Model.Enum.TransCamEFEO:
                    ret = this._validRumiChar;
                    break;

                case Model.Enum.TransInrasara:
                    ret = this._validInraChar;
                    break;

                default:
                    ret = this._validKTTChar;
                    break;
            }

            return ret;
        }

        /// <summary>
        /// Cut kanaing at first of akhar list
        /// </summary>
        /// <param name="list">akhar list</param>
        /// <returns>Stack of akhar</returns>
        protected Stack<Model.AKhar> CutKanaingAtFirst(ref List<Model.AKhar> list)
        {

            Stack<Model.AKhar> ret = new Stack<Model.AKhar>();

            while (list.Count != 0)
            {

                Model.AKhar akhar = list[0];

                if (this.Check_DauCau(akhar))
                {

                    ret.Push(akhar);
                    list.RemoveAt(0);
                    continue;
                }

                break;
            }

            return ret;
        }

        /// <summary>
        /// Cut kanaing at last of akhar list
        /// </summary>
        /// <param name="list">akhar list</param>
        /// <returns>Stack of akhar</returns>
        protected Stack<Model.AKhar> CutKanaingAtLast(ref List<Model.AKhar> list)
        {

            Stack<Model.AKhar> ret = new Stack<Model.AKhar>();

            while (list.Count != 0)
            {
                Model.AKhar akhar = list[list.Count - 1];
                if (this.Check_DauCau(akhar))
                {
                    ret.Push(akhar);
                    list.RemoveAt(list.Count - 1);
                    continue;
                }

                break;
            }

            return ret;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Check is valid word
        /// </summary>
        /// <param name="word">word</param>
        /// <param name="dataType">data type</param>
        ///  <param name="dictionary">dictionary</param>
        private bool ValidateWord(ref string word, Model.Enum dataType, Hashtable dictionary)
        {

            if (dataType == Model.Enum.TransCamEFEO || dataType == Model.Enum.TransInrasara || dataType == Model.Enum.TransKawomTT)
            {
                word = word.ToUpper();
            }

            bool ret = true;
            string wordReplaced = string.Empty;
            foreach (char c in word)
            {
                if (!dictionary.ContainsKey(c.ToString()))
                {
                    ret = false;
                    continue;
                }
                wordReplaced += c.ToString();
            }

            word = wordReplaced;
            return ret;
        }

        /// <summary>
        /// Set Color Error
        /// Author: Jabraok
        /// </summary>
        /// <param name="control">RichTextBox</param>
        /// <param name="dictionary">dictionary</param>
        private void SetColorError(RichTextBox control, Hashtable dictionary)
        {
            int i = 0;
            int startIndex = i;
            int length = 0;
            bool drawing = true;
            foreach (char item in control.Text)
            {
                //Draw
                if (drawing && length != 0)
                {
                    control.SelectionStart = startIndex;
                    control.SelectionLength = length;
                    control.SelectionBackColor = Color.Yellow;

                    length = 0;
                    drawing = true;
                }

                //Check invalid char
                if (item != Model.Constant.NEW_LINE_CHAR && !char.IsWhiteSpace(item) && !dictionary.ContainsKey(item.ToString().ToUpper()))
                {
                    if (length == 0)
                    {
                        startIndex = i;
                        drawing = false;
                    }

                    length++;
                }
                else
                {
                    drawing = true;
                }

                i++;
            }

            //Draw end
            if (length != 0)
            {
                control.SelectionStart = startIndex;
                control.SelectionLength = length;
                control.SelectionBackColor = Color.Yellow;
            }
        }

        #endregion

        #region Check

        /// <summary>
        /// Check is Consonant
        /// </summary>
        /// <param name="sabaoh">akhar</param>
        /// <returns>TRUE: Consonant; FALSE: Not Consonant</returns>
        protected bool Check_InaAkhar_PhuAm(Model.AKhar sabaoh)
        {
            return sabaoh <= Model.AKhar.Ak;
        }

        /// <summary>
        /// Check is wak matai
        /// </summary>
        /// <param name="sabaoh">sabaoh</param>
        /// <param name="index">index</param>
        /// <param name="count">count</param>
        protected bool IsAkharWakMaTai(Model.AKhar sabaoh, int index, int count)
        {
            return sabaoh == Model.AKhar.Wak && index == count - 1;
        }

        /// <summary>
        /// Check is Special Consonant
        /// (Mâk Nâk Nyâk Ngâk)
        /// </summary>
        /// <param name="sabaoh">akhar</param>
        protected bool Check_InaAkhar_PhuAm_Special(Model.AKhar sabaoh)
        {
            //MCMC
            Model.AKhar[] phu_am_dac_biet = new Model.AKhar[] { Model.AKhar.Mâk, Model.AKhar.Nâk, Model.AKhar.Nyâk, Model.AKhar.Ngâk };
            return Array.IndexOf(phu_am_dac_biet, sabaoh) != -1;
        }

        /// <summary>
        /// Check Is Vowel
        /// </summary>
        /// <param name="sabaoh">akhar</param>
        protected bool Check_InaAkhar_NguyenAm(Model.AKhar sabaoh)
        {
            return sabaoh >= Model.AKhar.Ik && sabaoh <= Model.AKhar.Ok;
        }

        /// <summary>
        /// Check is Lang likuk
        /// </summary>
        /// <param name="sabaoh">akhar</param>
        protected bool Check_LangLiKuk(Model.AKhar sabaoh)
        {
            //MCMC
            Model.AKhar[] arr_langlikuk = new Model.AKhar[] 
            {   Model.AKhar.Kak, Model.AKhar.Khak, Model.AKhar.Gak, Model.AKhar.Cak, Model.AKhar.Jak, Model.AKhar.Tak, Model.AKhar.Dak,Model.AKhar.Nâk,Model.AKhar.Nak,
                Model.AKhar.Pak,Model.AKhar.Bak,Model.AKhar.Mâk,Model.AKhar.Mak,Model.AKhar.Yak,Model.AKhar.Rak,Model.AKhar.Lak,
                Model.AKhar.Xak,Model.AKhar.Hak,Model.AKhar.PakPraong,Model.AKhar.SakPraong,Model.AKhar.Ak,Model.AKhar.Ik,Model.AKhar.Uk
            };

            return Array.IndexOf(arr_langlikuk, sabaoh) != -1;
        }

        /// <summary>
        /// Check is Akhar Mâtai
        /// </summary>
        /// <param name="sabaoh">akhar</param>
        protected bool Check_AkharMatai(Model.AKhar sabaoh)
        {
            return sabaoh >= Model.AKhar.KakMatai && sabaoh <= Model.AKhar.PaohDaNih || sabaoh == Model.AKhar.PaohNgâk;
        }

        protected bool CheckEndCharacter(Model.AKhar sabaoh)
        {
            Model.AKhar[] endCharacters = new Model.AKhar[] 
            { Model.AKhar.PaohNgâk, Model.AKhar.Balau, Model.AKhar.BalauTapong, Model.AKhar.TakaiKikTutTakaiMâkDalem, 
                Model.AKhar.TakaiKikTutTakaiMâkLingiw, Model.AKhar.TakaiKikTutTakaiYak, Model.AKhar.TakaiThekTutTakaiMâk, 
                Model.AKhar.TakaiThekPaohNgâk, Model.AKhar.TraohAwPaohNgâk, Model.AKhar.TraohAwTutTakaiMâk
            };

            return sabaoh >= Model.AKhar.KakMatai && sabaoh <= Model.AKhar.PaohDaNih || Array.IndexOf(endCharacters, sabaoh) != -1;
        }

        /// <summary>
        /// Check is takai akhar
        /// </summary>
        /// <param name="sabaoh">akhar</param>
        protected bool Check_TakaiAkhar(Model.AKhar sabaoh)
        {
            return sabaoh > Model.AKhar.Balau && sabaoh <= Model.AKhar.PaohNgâk;
        }

        /// <summary>
        /// Check is Takai Akhar sap paoh deng likuk
        /// </summary>
        /// <param name="sabaoh">akhar</param>
        protected bool Check_TakaiSapPaohLikuk(Model.AKhar sabaoh)
        {
            //MCMC
            Model.AKhar[] arr_TakaiAkharLikuk = new Model.AKhar[] 
            {   Model.AKhar.TakaiKik, Model.AKhar.TakaiKikTutTakaiMâkDalem, Model.AKhar.TakaiThek,
                Model.AKhar.TakaiKuk, Model.AKhar.TakaiKâk, Model.AKhar.TraohAw, Model.AKhar.BalauTapong
            };

            return Array.IndexOf(arr_TakaiAkharLikuk, sabaoh) != -1;
        }

        /// <summary>
        /// Check is Takai Akhar sap paoh deng anak
        /// </summary>
        /// <param name="sabaoh">akhar</param>
        protected bool Check_TakaiSapPaohAnak(Model.AKhar sabaoh)
        {
            return sabaoh == Model.AKhar.DarSa || sabaoh == Model.AKhar.DarDua;
        }


        /// <summary>
        /// Check is Takai Akhar sap paoh deng luic di abih
        /// </summary>
        /// <returns></returns>
        protected bool Check_TakaiSapPaohDiLuic(Model.AKhar sabaoh)
        {
            //MCMC
            Model.AKhar[] arr_TakaiAkharLikuk = new Model.AKhar[] 
            {   
                Model.AKhar.TakaiThekPaohNgâk, Model.AKhar.TakaiThekTutTakaiMâk,
                Model.AKhar.TakaiKikTutTakaiMâkLingiw, Model.AKhar.TakaiKikTutTakaiYak, 
                Model.AKhar.TraohAwTutTakaiMâk, Model.AKhar.TraohAwPaohNgâk
            };

            return Array.IndexOf(arr_TakaiAkharLikuk, sabaoh) != -1;
        }

        /// <summary>
        /// Check is Takai Akhar deng anak
        /// </summary>
        /// <param name="sabaoh">akhar</param>
        protected bool Check_TakaiAkharAnak(Model.AKhar sabaoh)
        {
            return sabaoh == Model.AKhar.TakaiKrak;
        }

        /// <summary>
        /// Check is Takai Akhar deng likuk
        /// </summary>
        /// <param name="sabaoh">akhar</param>
        protected bool Check_TakaiAkharLikuk(Model.AKhar sabaoh)
        {
            //MCMC
            Model.AKhar[] arr_TakaiAkharLikuk = new Model.AKhar[] 
            {   
                Model.AKhar.TakaiKiak, Model.AKhar.TakaiKuak,
                Model.AKhar.TakaiKlak, Model.AKhar.TakaiKlakTakaiKuak, Model.AKhar.TakaiKlakTakaiKuk                
            };
            return Array.IndexOf(arr_TakaiAkharLikuk, sabaoh) != -1;
        }

        /// <summary>
        /// Check is balau
        /// </summary>
        /// <param name="sabaoh">akhar</param>
        protected bool Check_Balau(Model.AKhar sabaoh)
        {
            return sabaoh == Model.AKhar.Balau;
        }

        /// <summary>
        /// Check is angka
        /// </summary>
        /// <param name="sabaoh">akhar</param>
        protected bool Check_Angka(Model.AKhar sabaoh)
        {
            return sabaoh >= Model.AKhar.Sa && sabaoh <= Model.AKhar.Saoh;
        }

        /// <summary>
        /// Check is sap paoh
        /// </summary>
        /// <param name="sabaoh">akhar</param>
        protected bool Check_DauCau(Model.AKhar sabaoh)
        {
            return sabaoh >= Model.AKhar.KanaingSa && sabaoh <= Model.AKhar.Square;
        }

        /// <summary>
        /// Check Is Sap Paoh
        /// </summary>
        /// <param name="akhar">akhar</param>
        protected bool IsSapPaoh(Model.AKhar akhar)
        {

            return this._sappaoh.ContainsKey(akhar);
        }

        /// <summary>
        /// Check have lang likuk
        /// </summary>
        /// <param name="list">akhar list</param>
        protected bool HuLanglikuk(List<Model.AKhar> list)
        {

            int count = list.Count;
            if (count == 1 && this.IsVowels(list[0])) return true;

            if (count < 2) return false;
            if (list[count - 2] == Model.AKhar.Ak && list[count - 1] == Model.AKhar.Ak)
            {

                return false;
            }

            if (this.IsAkharDiip(list[count - 2]) && this.IsVowelLanglikuk(list[count - 1]))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Check is akhar diip
        /// </summary>
        /// <param name="akhar">akhar</param>
        protected bool IsAkharDiip(Model.AKhar akhar)
        {
            return akhar <= Model.AKhar.Ok;
        }

        /// <summary>
        /// Check is vowels
        /// </summary>
        /// <param name="akhar">akhar</param>
        protected bool IsVowels(Model.AKhar akhar)
        {
            return akhar >= Model.AKhar.Ak && akhar <= Model.AKhar.Ok;
        }

        /// <summary>
        /// Check is consonant
        /// </summary>
        /// <param name="akhar">akhar</param>
        protected bool IsConsonant(Model.AKhar akhar)
        {
            return akhar <= Model.AKhar.SakPraong;
        }

        /// <summary>
        /// Check is Takai likuk
        /// </summary>
        /// <param name="akhar">akhar</param>
        protected bool IsTakaiLikuk(Model.AKhar akhar)
        {
            return this._takaiLikuk.ContainsKey(akhar);
        }

        /// <summary>
        /// Check is Vowel of Lang likuk
        /// </summary>
        /// <param name="akhar">khar</param>
        protected bool IsVowelLanglikuk(Model.AKhar akhar)
        {
            return this._vowelLangLikuk.ContainsKey(akhar);
        }

        /// <summary>
        /// Check Is Font Cam
        /// </summary>
        /// <param name="dataType">dataType</param>
        public bool IsFont(Model.Enum dataType)
        {
            return dataType <= Model.Enum.FontUniCamVN;
        }

        /// <summary>
        /// Check Is Transliterate
        /// </summary>
        /// <param name="dataType">dataType</param>
        public bool IsTransliterate(Model.Enum dataType)
        {
            return dataType > Model.Enum.FontKTT;
        }

        #endregion
    }

}

using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Cam.Service
{
    /// <summary>
    /// Cam To Trans Converting
    /// </summary>
    public class CamToTransPaxalih : Paxalih
    {
        #region Variables

        private Hashtable _keyCodeToTrans;
        private Hashtable _vowelKTT;

        #endregion

        #region Public Methods
       
        /// <summary>
        /// Convert To Rumi
        /// </summary>
        /// <param name="data">String data</param>
        /// <param name="sourceType">source Type</param>
        /// <param name="destinationType">destination Type</param>
        public String DoConvert(String data, Model.Enum sourceType, Model.Enum destinationType)
        {
            //Init Cam to Rumi Data
            this.InitCamToTransData(destinationType);

            //Contain converted
            List<String> converted = new List<String>();

            //Trig newline character
            data = data.Replace(Model.Constant.NEW_LINE, " " + Model.Constant.NEW_LINE + " ");

            //Plit to words array
            String[] words = data.Split(' ');

            //Convert processing
            Hashtable dictionary = this.GetDictionaty(sourceType);
            foreach (string word in words)
            {
                try
                {
                    // is newline character
                    if (word == Model.Constant.NEW_LINE || String.IsNullOrEmpty(word))
                    {
                        converted.Add(word);
                        continue;
                    }

                    List<Model.AKhar> lstAkhar = new List<Model.AKhar>();
                    foreach (char c in word)
                    {

                        Model.AKhar akhar = (Model.AKhar)dictionary[c.ToString()];
                        lstAkhar.Add(akhar);
                    }

                    Stack<Model.AKhar> kanaingAtFirst = new Stack<Model.AKhar>();
                    Stack<Model.AKhar> kanaingAtLast = new Stack<Model.AKhar>();

                    //Cut kanaing at first and last word
                    kanaingAtFirst = this.CutKanaingAtFirst(ref lstAkhar);
                    kanaingAtLast = this.CutKanaingAtLast(ref lstAkhar);

                    String ret = this.ToRumiByWord(lstAkhar, destinationType);

                    // add kanaing at first and last word
                    this.PopStackToList(ref ret, kanaingAtFirst, true);
                    this.PopStackToList(ref ret, kanaingAtLast, false);

                    converted.Add(ret);
                }
                catch (Exception ex)
                {

                    Service.Log.WriteLog(ex);
                }
            }

            String result = String.Join(" ", converted.ToArray());

            //Trig newline character
            result = result.Replace(" " + Model.Constant.NEW_LINE + " ", Model.Constant.NEW_LINE);

            return result;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Convert To Rumi by word
        /// </summary>
        /// <param name="word">word</param>
        private String ToRumiByWord(List<Model.AKhar> word, Model.Enum desType)
        {
            //List empty
            if (word.Count == 0)
            {
                return String.Empty;
            }

            string sap_Â = String.Empty;
            string sap_Ô = String.Empty;
            string sap_E = String.Empty;

            if (desType == Model.Enum.TransCamEFEO)
            {
                sap_Â = Model.Constant.Â_EFEO;
                sap_Ô = Model.Constant.Ô_EFEO;
                sap_E = Model.Constant.E_EFEO;
            }
            else if (desType == Model.Enum.TransInrasara)
            {
                sap_Â = Model.Constant.Â_SARA;
                sap_Ô = Model.Constant.Ô_SARA;
                sap_E = Model.Constant.E_SARA;
            }
            else if (desType == Model.Enum.TransKawomTT)
            {
                sap_Â = Model.Constant.Â_KawomTT;
                sap_Ô = Model.Constant.Ô_KawomTT;
                sap_E = Model.Constant.E_KawomTT;
            }

            String result = String.Empty;
            string akhar_diip = String.Empty;
            Model.AKhar akhar_diip_model = Model.AKhar.Kak;
            string takaiSappaohAnak = String.Empty;
            string takaiAkharAnak = String.Empty;
            string sapPaoh_temp = String.Empty;
            int count = word.Count;
            //int count_index = 0;
            bool akharDiipAdded = false;
            bool huTakaiKrak = false;
            bool klakAkharAk = true;
            bool huBalau = false;
            bool addedTakaiThek = false;
            bool addedTakaiKuk = false;

            List<Model.AKhar> wordKareiCrih = new List<Model.AKhar> { Model.AKhar.Ak, Model.AKhar.SakPraong, Model.AKhar.TakaiKuk, Model.AKhar.TakaiThek };
            if (count == 4 && word[0] == wordKareiCrih[0] && word[1] == wordKareiCrih[1] && word[2] == wordKareiCrih[2] && word[3] == wordKareiCrih[3])
                return result = "asau";
            //for (int i = 0; i < word.Count; i++)
            //{
            //    if (word[i] == wordKareiCrih[i])
            //        count_index++;
            //    if (count_index == word.Count)
            //        return result = "asau";
            //} 

           
            if (count == 0) return String.Empty;

            //Hu akhar mâtai di krâh, EX: "duix-xak"
            List<Model.AKhar> word1 = new List<Model.AKhar>();
            List<Model.AKhar> word2 = new List<Model.AKhar>();

            for (int i = 0; i < count; i++)
            {

                Model.AKhar item = word[i];
                if (this.Check_AkharMatai(word[i]) && i != word.Count - 1 && i != 0)
                {
                    word1 = word.GetRange(0, i + 1);
                    word2 = word.GetRange(i + 1, word.Count - (i + 1));
                    break;
                }
            }

            if (word1.Count != 0)
            {

                result = this.ToRumiByWord(word1, desType) + this.ToRumiByWord(word2, desType);
            }
            else
            {
                //Xalih bhian
                for (int i = 0; i < count; i++)
                {
                    
                    Model.AKhar item = word[i];

                    if (this.Check_Angka(item))
                    {
                        result += this._keyCodeToTrans[item].ToString();
                        continue;
                    }

                    //hu balau
                    if (this.Check_Balau(item))
                    {
                        huBalau = true;
                    }

                    //end angka
                    if ((this.Check_InaAkhar_PhuAm(item) || this.Check_InaAkhar_NguyenAm(item)) && !this.IsAkharWakMaTai(item, i, count))
                    {

                        if (!String.IsNullOrEmpty(akhar_diip))
                        {
                            result += akhar_diip;
                        }

                        akhar_diip_model = item;//??
                        akhar_diip = this._keyCodeToTrans[item].ToString().ToUpper();

                        if (!String.IsNullOrEmpty(takaiAkharAnak))//Takai krak (aiek wek ???)
                        {

                            if (this._keyCodeToTrans[akhar_diip_model].ToString().Length > 2) //or > 2 ??
                            {
                                if (this.Check_InaAkhar_PhuAm_Special(akhar_diip_model))//aiek wek hu repale("â","a")
                                {
                                    akhar_diip = akhar_diip.Substring(0, 2) + takaiAkharAnak + akhar_diip.Substring(2).Replace(sap_Â, "A");//Repalce or none???
                                }
                                else
                                {
                                    if (this._keyCodeToTrans[akhar_diip_model].ToString().Length > 3)//nhjra
                                    {
                                        akhar_diip = akhar_diip.Substring(0, 3) + takaiAkharAnak + akhar_diip.Substring(3);//mba; bba; nhjra ???
                                    }
                                    else
                                        akhar_diip = akhar_diip.Substring(0, 2) + takaiAkharAnak + akhar_diip.Substring(2);
                                        
                                    //if (desType == Model.Enum.TransCamEFEO)//nja
                                    //{
                                    //    akhar_diip = akhar_diip.Substring(0, 2) + takaiAkharAnak + akhar_diip.Substring(2);
                                    //}
                                    //else
                                    //    akhar_diip = akhar_diip.Substring(0, 3) + takaiAkharAnak + akhar_diip.Substring(3);//mba; bba; nhjra ???
                                }
                                huTakaiKrak = true;
                            }
                            else
                            {
                                if (this.Check_InaAkhar_PhuAm_Special(akhar_diip_model))//aiek wek hu repale("â","a")
                                {
                                    akhar_diip = akhar_diip.Substring(0, 1) + takaiAkharAnak + akhar_diip.Substring(1).Replace(sap_Â, "A");//Repalce or none???
                                }
                                else
                                    akhar_diip = akhar_diip.Substring(0, 1) + takaiAkharAnak + akhar_diip.Substring(1);
                                huTakaiKrak = true;
                            }

                        }

                        if (!String.IsNullOrEmpty(takaiSappaohAnak))
                        {
                            //Akhar Ak
                            if (akhar_diip_model == Model.AKhar.Ak)
                            {
                                akhar_diip = akhar_diip.Substring(0, 1) + akhar_diip.Substring(1).Replace("A", takaiSappaohAnak);
                                klakAkharAk = false;//
                            }
                            else
                            {
                                akhar_diip = akhar_diip.Replace("A", takaiSappaohAnak);
                                akhar_diip = akhar_diip.Replace(sap_Â, takaiSappaohAnak);//(â-ư)
                            }
                        }

                        continue;
                    }

                    //
                    if (this.Check_TakaiSapPaohAnak(item))
                    {
                        takaiSappaohAnak = this._keyCodeToTrans[item].ToString().ToUpper();
                        continue;
                    }
                    if (this.Check_TakaiSapPaohLikuk(item) || this.Check_TakaiSapPaohDiLuic(item))
                    {

                        string takaiAkhar = this._keyCodeToTrans[item].ToString().ToUpper();
                        if (!String.IsNullOrEmpty(akhar_diip))
                        {
                            if (!String.IsNullOrEmpty(takaiSappaohAnak))
                            {
                                if (takaiSappaohAnak == sap_Ô)
                                {

                                    Model.AKhar sappaohCombine = (Model.AKhar)Convert.ToInt32(((int)Model.AKhar.DarSa).ToString() + ((int)item).ToString());
                                    sapPaoh_temp = this._keyCodeToTrans[sappaohCombine].ToString().ToUpper();
                                }

                                akhar_diip = akhar_diip.Replace(sap_Ô, sapPaoh_temp);
                            }
                            else
                            {
                                if (akhar_diip_model == Model.AKhar.Ak)
                                {

                                    akhar_diip = akhar_diip.Substring(0, 1) + akhar_diip.Substring(1).Replace("A", takaiAkhar);
                                    klakAkharAk = false;
                                }
                                else
                                {
                                    //takai thek <+> takai kuk (ơu - au) 
                                    if (item == Model.AKhar.TakaiKuk && addedTakaiThek)
                                    {
                                        Model.AKhar sappaohCombine = (Model.AKhar)Convert.ToInt32(((int)Model.AKhar.TakaiThek).ToString() + ((int)item).ToString());
                                        sapPaoh_temp = this._keyCodeToTrans[sappaohCombine].ToString().ToUpper();

                                        akhar_diip = akhar_diip.Replace(sap_E, sapPaoh_temp);
                                    }
                                    else if (item == Model.AKhar.TakaiThek && addedTakaiKuk)
                                    {
                                        Model.AKhar sappaohCombine = (Model.AKhar)Convert.ToInt32(((int)Model.AKhar.TakaiThek).ToString() + ((int)Model.AKhar.TakaiKuk).ToString());
                                        sapPaoh_temp = this._keyCodeToTrans[sappaohCombine].ToString().ToUpper();

                                        akhar_diip = akhar_diip.Replace("U", sapPaoh_temp);
                                    }
                                    else
                                    {
                                        akhar_diip = akhar_diip.Replace("A", takaiAkhar);//???aua...
                                        akhar_diip = akhar_diip.Replace(sap_Â, takaiAkhar);

                                        if (item == Model.AKhar.TakaiThek)
                                        {
                                            addedTakaiThek = true;
                                        }
                                        if (item == Model.AKhar.TakaiKuk)
                                        {
                                            addedTakaiKuk = true;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            result += takaiAkhar;
                        }
                        continue;
                    }

                    if (this.Check_TakaiAkharAnak(item))
                    {

                        takaiAkharAnak = this._keyCodeToTrans[item].ToString().ToUpper();
                        continue;
                    }

                    if (this.Check_TakaiAkharLikuk(item))//aiek wek
                    {

                        string takaiAkhar = this._keyCodeToTrans[item].ToString().ToUpper();
                        if (!String.IsNullOrEmpty(akhar_diip))
                        {
                            if (this._keyCodeToTrans[akhar_diip_model].ToString().Length > 3)//nhja
                            {
                                //wak check -r di dahlau
                                if (huTakaiKrak)
                                {
                                    akhar_diip = akhar_diip.Substring(0, 4) + takaiAkhar + akhar_diip.Substring(4);// (Akhar Nhjak)
                                }
                                else
                                {
                                    akhar_diip = akhar_diip.Substring(0, 3) + takaiAkhar + akhar_diip.Substring(3);//aiek wek
                                }
                            }

                            else if (this._keyCodeToTrans[akhar_diip_model].ToString().Length == 3)//> 2??
                            {
                                //wak check -r di dahlau
                                if (huTakaiKrak)//Is_Hu_TakaiKrak_blaoh(akhar_diip)
                                {
                                    //if (this.Check_InaAkhar_PhuAm_Special(akhar_diip_model))
                                    //{
                                    //    akhar_diip = akhar_diip.Substring(0, 3) + takaiAkhar + akhar_diip.Substring(3);//???aiek wek
                                    //}
                                    //else
                                    if (desType == Model.Enum.TransCamEFEO)
                                    {
                                        akhar_diip = akhar_diip.Substring(0, 3) + takaiAkhar + akhar_diip.Substring(3);
                                    }
                                    else
                                    {
                                        akhar_diip = akhar_diip.Substring(0, 4) + takaiAkhar + akhar_diip.Substring(4);// (Akhar Nhjak)
                                    }

                                }
                                else
                                    //if (desType == Model.Enum.TransCamEFEO)
                                    //{
                                    akhar_diip = akhar_diip.Substring(0, 2) + takaiAkhar + akhar_diip.Substring(2);
                                //}
                                //else
                                //{
                                //    akhar_diip = akhar_diip.Substring(0, 3) + takaiAkhar + akhar_diip.Substring(3);//aiek wek
                                //}

                            }
                            else
                            {
                                if (huTakaiKrak)//Is_Hu_TakaiKrak_blaoh(akhar_diip)
                                {
                                    akhar_diip = akhar_diip.Substring(0, 2) + takaiAkhar + akhar_diip.Substring(2);
                                }
                                else
                                {
                                    if (takaiAkhar.Length == 1)
                                    {
                                        if (this.Check_InaAkhar_PhuAm_Special(akhar_diip_model))
                                            akhar_diip = akhar_diip.Substring(0, 1) + takaiAkhar + akhar_diip.Substring(1).Replace(sap_Â, "A"); //??? aiek wek
                                        else
                                            akhar_diip = akhar_diip.Substring(0, 1) + takaiAkhar + akhar_diip.Substring(1);
                                    }
                                    else
                                        akhar_diip = akhar_diip.Substring(0, 1) + akhar_diip.Substring(1).Replace(akhar_diip.Substring(1), takaiAkhar);
                                    //??? aiek hu replace ("â","a")
                                }
                            }
                        }
                        else
                        {
                            result += takaiAkhar;
                        }
                        continue;
                    }

                    if (this.Check_AkharMatai(item) || this.IsAkharWakMaTai(item, i, count))
                    {

                        string akharMatai = this._keyCodeToTrans[item].ToString();
                        if (item == Model.AKhar.Wak)
                        {
                            akharMatai = akharMatai.Substring(0, 1);
                        }

                        result += akhar_diip + akharMatai;
                        akharDiipAdded = true;
                        continue;
                    }

                    //Aiek wek????
                    if (this.Check_DauCau(item))
                    {

                        string dau_cau = this._keyCodeToTrans[item].ToString();
                        if (!akharDiipAdded)
                        {
                            akhar_diip += dau_cau;
                        }
                        else
                            result += dau_cau;

                        continue;
                    }
                }

                if (!akharDiipAdded)
                {

                    result += akhar_diip;
                }

                //Klak 'aa...'
                int libik_a = 0;
                for (int j = 0; j < result.Length - 1; j++)
                {
                    if (result[j] == result[j + 1] && result[j] == 'A' && result != "XAAI" && klakAkharAk)//if hu akhar Ak (Xaai)
                    {
                        libik_a = j + 1;
                        result = result.Substring(0, libik_a) + result.Substring(libik_a + 1);
                        break;
                    }

                    if (result.Length > 2 && (result[0] == 'A' && result[1] == 'A') || (result[0] == 'A' && result[1] == 'Â') || (result[0] == 'A' && result[1] == 'Ư'))//char vs string ???
                    {
                        result = result.Substring(1);
                    }
                }

                //Double Vowel
                if (huBalau && word[word.Count - 1] != Model.AKhar.Balau && desType == Model.Enum.TransKawomTT)
                {
                    result = DoubleVowel(result).ToUpper();
                }

                //Cut Vowel
                if (desType == Model.Enum.TransKawomTT)
                {
                    for (int i = result.Length - 1; i > 0; i--)
                    {
                        //e -> ai
                        if (result[i].ToString() == "E" && i == result.Length - 1)
                        {
                            result = result.Replace("E", "AI");
                            break;
                        }

                        //oo -> ao
                        if (result[i].ToString() + result[i - 1].ToString() == "OO" && i == result.Length - 1)
                        {
                            result = result.Replace("OO", "AO");
                            break;
                        }

                        //ii -> i
                        if (result[i].ToString() + result[i - 1].ToString() == "II" && i == result.Length - 1)
                        {
                            result = result.Replace("II", "I");
                            break;
                        }

                        //ơơ -> ơ
                        if (result[i].ToString() + result[i - 1].ToString() == "ƠƠ" && i == result.Length - 1)
                        {
                            result = result.Replace("ƠƠ", "Ơ");
                            break;
                        }
                    }
                }

                //End klak ak
                if (result.IndexOf("PPONG") != -1)
                    result = result.Replace("PPONG", "PPO");

                if (result.IndexOf("PPÔNG") != -1)
                    result = result.Replace("PPÔNG", "PPÔ");

                if (result.IndexOf("XAII") != -1)
                    result = result.Replace("XAII", "XAAI");
            }

            return result.ToLower();
        }

        private string DoubleVowel(string result)
        {

            for (int i = result.Length - 2; i > 0; i--)// not available vowel at the end!!!
            {

                string key = result[i].ToString();
                if (this._vowelKTT.ContainsKey(key))
                {
                    result = result.Remove(i, 1);
                    result = result.Insert(i, this._vowelKTT[key].ToString());
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// Add item from Stack into List
        /// </summary>
        /// <param name="list">String list</param>
        /// <param name="stack">Stack of akhar</param>
        /// <param name="addFirst">add to first flag</param>
        protected void PopStackToList(ref String list, Stack<Model.AKhar> stack, bool addFirst)
        {
            while (stack.Count != 0)
            {

                int index = addFirst ? 0 : list.Length;

                list = list.Insert(index, this._keyCodeToTrans[stack.Pop()].ToString());
            }
        }

        /// <summary>
        /// Init data converting
        /// </summary>
        protected void InitCamToTransData(Model.Enum destinationType)
        {

            this._kanaingChars = Utility.InitKanaing();
            this._vowelKTT = Utility.InitVowelKTT();
            //Set Keycode to Trans hashtable
            Utility.SetTransFromXML(ref this._keyCodeToTrans, destinationType);
        }

        #endregion
    }
}

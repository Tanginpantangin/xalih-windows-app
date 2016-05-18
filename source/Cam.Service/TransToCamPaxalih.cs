using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Cam.Service
{

    /// <summary>
    /// Cam converting
    /// </summary>
    public class TransToCamPaxalih : Paxalih
    {
        #region Constant

        private const char _DASH = '-';
        #endregion

        #region variales

        private string _trans_IM;
        private string _trans_LU;
        private string _trans_LW;
        private string _trans_AI;
        private string _trans_AO;
        private string _trans_AU = string.Empty;

        protected Hashtable _transToKeycode;
        protected Hashtable _sapAtah;
       
        #endregion

        #region Public Methods

        /// <summary>
        /// Convert To Rumi
        /// </summary>
        /// <param name="data">String data</param>
        /// <param name="sourceType">source Type</param>
        /// <param name="destitionType">destition Type</param>
        public String DoConvert(String data, Model.Enum sourceType, Model.Enum destitionType)
        {
            //Init Rumi To Cam data
            this.InitTransToCamData(sourceType, destitionType);

            //Contain converted
            List<String> converted = new List<String>();

            //Trig newline character
            data = data.Replace(Model.Constant.NEW_LINE, " " + Model.Constant.NEW_LINE + " ");

            //Plit to words array
            String[] words = data.Split(' ');

            //Convert processing
            foreach (string word in words)
            {
                // is newline character
                if (word == Model.Constant.NEW_LINE || String.IsNullOrEmpty(word))
                {
                    converted.Add(word);
                    continue;
                }

                List<Model.AKhar> lstAkhar = this.ToKeyCodeByWord(word.ToUpper(), sourceType);
                String convertedWord = String.Empty;

                foreach (Model.AKhar akhar in lstAkhar)
                {
                    switch (destitionType)
                    {
                        case Model.Enum.FontYapata:
                            convertedWord += this._keyCodeToWaYapata[(int)akhar].ToString();
                            break;

                        case Model.Enum.FontKTT:
                            convertedWord += this._keyCodeToKTT[(int)akhar].ToString();
                            break;

                        case Model.Enum.FontGilaiPraong:
                            if (akhar == Model.AKhar.GakMatai)
                            {
                                convertedWord += this._keyCodeToGilaiPraong[(int)Model.AKhar.KakMatai].ToString();
                            }
                            else
                            {
                                convertedWord += this._keyCodeToGilaiPraong[(int)akhar].ToString();
                            }
                            break;

                        case Model.Enum.FontCamEFEO:
                            if (akhar == Model.AKhar.TakaiKlakTakaiKuak)
                            {
                                convertedWord += this._keyCodeToCamEFEO[(int)Model.AKhar.TakaiKlak].ToString();
                                convertedWord += this._keyCodeToCamEFEO[(int)Model.AKhar.TakaiKuak].ToString();
                            }
                            else if (akhar == Model.AKhar.TakaiKlakTakaiKuk)
                            {
                                convertedWord += this._keyCodeToCamEFEO[(int)Model.AKhar.TakaiKlak].ToString();
                                convertedWord += this._keyCodeToCamEFEO[(int)Model.AKhar.TakaiKuk].ToString();
                            }
                            else if (akhar == Model.AKhar.GakMatai)
                            {
                                convertedWord += this._keyCodeToCamEFEO[(int)Model.AKhar.KakMatai].ToString();
                            }
                            else
                            {
                                convertedWord += this._keyCodeToCamEFEO[(int)akhar].ToString();
                            }
                            break;

                        default:
                            if (akhar == Model.AKhar.TakaiKlakTakaiKuak)
                            {
                                convertedWord += this._keyCodeToUniCamKur[(int)Model.AKhar.TakaiKlak].ToString();
                                convertedWord += this._keyCodeToUniCamKur[(int)Model.AKhar.TakaiKuak].ToString();
                            }
                            else if (akhar == Model.AKhar.TakaiKlakTakaiKuk)
                            {
                                convertedWord += this._keyCodeToUniCamKur[(int)Model.AKhar.TakaiKlak].ToString();
                                convertedWord += this._keyCodeToUniCamKur[(int)Model.AKhar.TakaiKuk].ToString();
                            }
                            else
                            {
                                convertedWord += this._keyCodeToUniCamKur[(int)akhar].ToString();
                            }
                            break;
                    }
                }

                converted.Add(convertedWord);
            }

            String result = String.Join(" ", converted.ToArray());

            //Trig newline character
            result = result.Replace(" " + Model.Constant.NEW_LINE + " ", Model.Constant.NEW_LINE);

            return result;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Covert to keycode by word
        /// </summary>
        /// <param name="word">word</param>
        private List<Model.AKhar> ToKeyCodeByWord(String word, Model.Enum sourceType)
        {
            try
            {
                List<Model.AKhar> ret = new List<Model.AKhar>();
                Stack<Model.AKhar> kanaingAtFirst = new Stack<Model.AKhar>();
                Stack<Model.AKhar> kanaingAtLast = new Stack<Model.AKhar>();

                bool addLastCharProcess = true;
                string[] wordArr = word.Split(_DASH);
                foreach (string item in wordArr)
                {
                    List<Model.AKhar> wordAkhar = new List<Model.AKhar>();
                    string wordStr = item;

                    //Cut kanaing at first and last word
                    kanaingAtFirst = this.CutKanaingAtFirst(ref wordStr);
                    kanaingAtLast = this.CutKanaingAtLast(ref wordStr);

                    //Specail word
                    if (this._kareiCrih.ContainsKey(wordStr))
                    {
                        wordAkhar = Utility.CopyListAkhar((List<Model.AKhar>)this._kareiCrih[wordStr]);

                        // add kanaing at first and last word
                        Utility.PopStackToList(wordAkhar, kanaingAtFirst, true);
                        Utility.PopStackToList(wordAkhar, kanaingAtLast, false);

                        ret.AddRange(wordAkhar);
                        addLastCharProcess = false;
                        continue;
                    }

                    addLastCharProcess = true;
                    List<Model.AKhar> retForCode = new List<Model.AKhar>();
                    Model.AKhar prevChar = Model.AKhar.Square;

                    int roundCount = 0;
                    while (!String.IsNullOrEmpty(wordStr))
                    {
                        if (roundCount == Model.Constant.MAX_ROUND)
                        {
                            break;
                        }

                        //convert to keycode
                        switch (sourceType)
                        {
                            case Model.Enum.TransCamEFEO:
                                this.ToKeycodeFromCamEFEO(ref wordStr, ref wordAkhar, ref retForCode);
                                break;

                            case Model.Enum.TransInrasara:
                                this.ToKeycodeFromInrasara(ref wordStr, ref wordAkhar, ref retForCode);
                                break;

                            default:
                                this.ToKeycodeFromKTT(ref wordStr, ref wordAkhar, ref retForCode, ref prevChar);
                                break;
                        }
                    }

                    ret.AddRange(wordAkhar);
                    roundCount++;
                }

                //add balau on the last char
                if (ret.Count != 0 && addLastCharProcess)
                {
                    int indexLastChar = this.GetIndexLastChar(ret);
                    Model.AKhar lastAkhar = ret[indexLastChar];
                    if ((this.IsAkharDiip(lastAkhar) || this.IsTakaiLikuk(lastAkhar)) && !ret.Contains(Model.AKhar.DarDua)
                        && !(lastAkhar == Model.AKhar.TakaiThek && indexLastChar > 0 && ret[indexLastChar - 1] == Model.AKhar.TakaiKuk))
                    {
                        ret.Insert(indexLastChar + 1, Model.AKhar.Balau);
                    }

                    if (ret.Count != 0 && this._takaiDaokLikuk.ContainsKey(ret[0]))
                    {
                        ret.Insert(0, Model.AKhar.Ak);
                    }
                }

                // add kanaing at first and last word
                Utility.PopStackToList(ret, kanaingAtFirst, true);
                Utility.PopStackToList(ret, kanaingAtLast, false);

                return ret;
            }
            catch (Exception ex)
            {
                Service.Log.WriteLog(ex);
                return new List<Model.AKhar>();
            }
        }

        /// <summary>
        /// To keycode processing
        /// </summary>
        /// <param name="word">rumi word</param>
        /// <param name="ret">result</param>
        /// <param name="retForCode">result for coding</param>
        private void ToKeycodeFromCamEFEO(ref String word, ref List<Model.AKhar> ret, ref List<Model.AKhar> retForCode)
        {
            List<Model.AKhar> akharList = this.ToKeyCodeByChar(ref word, retForCode);
            for (int i = 0; i < akharList.Count; i++)
            {
                Model.AKhar akhar = akharList[i];
                if (akhar == Model.AKhar.Ak && !this.HuLanglikuk(retForCode) && ret.Count != 0)
                {
                    retForCode.Add(akhar);
                    continue;
                }

                //Get the next akhar
                Model.AKhar nextAkhar = Model.AKhar.Square;
                if (!String.IsNullOrEmpty(word))
                {
                    nextAkhar = this.GetNextChar(word);
                }

                //curent char is "i" or "é" and the next char is akhar matai
                if (!String.IsNullOrEmpty(word))
                {
                    //Convert akhar diip to takai akhar
                    if (this._diipToTaKai.ContainsKey(akhar) && !this._diipToMaTai.ContainsKey(nextAkhar)
                        && !this.HuLanglikuk(retForCode) && ret.Count != 0 && !this.Check_AkharMatai(ret[ret.Count - 1]))
                    {

                        if (!((akhar == Model.AKhar.Rak && ret.Count != 0 && this.IsVowels(ret[ret.Count - 1])) ||
                            ((akhar == Model.AKhar.Ik || akhar == Model.AKhar.Uk) && this.IsConsonant(nextAkhar) && !this._diipToMaTai.ContainsKey(nextAkhar))))
                        {

                            akhar = (Model.AKhar)this._diipToTaKai[akhar];
                        }
                    }

                    //"u","o"
                    if ((akhar == Model.AKhar.Ok || akhar == Model.AKhar.Uk)
                        && !this.HuLanglikuk(retForCode) && ret.Count != 0)
                    {

                        if (akhar == Model.AKhar.Ok)
                        {
                            akhar = Model.AKhar.DarSa;
                        }
                        else
                        {
                            akhar = Model.AKhar.TakaiKuk;
                        }
                    }

                    //Convert akhar diip to sap paoh
                    if ((this._diipToMaTai.ContainsKey(nextAkhar) && !this.HuLanglikuk(retForCode) && ret.Count != 0) ||
                        (this.IsConsonant(nextAkhar) && !this._diipToMaTai.ContainsKey(nextAkhar)))
                    {
                        if (akhar == Model.AKhar.Ik && ret.Count != 0)
                        {
                            akhar = Model.AKhar.TakaiKik;
                        }
                        else if (akhar == Model.AKhar.É)
                        {
                            akhar = Model.AKhar.DarSa;
                            akharList.Add(Model.AKhar.TakaiThek);
                        }
                    }

                    //TaikaiKlakTaikaiKuak & TaikaiKlakTaikaiKuk
                    if (akhar == Model.AKhar.TakaiKlakTakaiKuak)
                    {
                        if (!this.IsSapPaoh(nextAkhar))
                        {
                            akhar = Model.AKhar.TakaiKlakTakaiKuk;
                        }
                    }

                    //Mâk, Nâk, Nyâk, Ngâk
                    if (this._consonantKareiCrih.ContainsKey(akhar) && nextAkhar == Model.AKhar.Ak)
                    {
                        akhar = (Model.AKhar)this._consonantKareiCrih[akhar];
                    }
                }

                //Sap paoh deng anak akhar
                int index = ret.Count;
                int indexForCode = retForCode.Count;
                if (akhar == Model.AKhar.DarSa || akhar == Model.AKhar.DarDua || akhar == Model.AKhar.TakaiKrak)
                {
                    index = this.GetIndexAkharDiip(ret);

                    //fix have taikai Krak
                    if (index > 0 && ret[index - 1] == Model.AKhar.TakaiKrak)
                    {
                        index--;
                    }

                    indexForCode = this.GetIndexAkharDiip(retForCode);

                    //fix have taikai Krak
                    if (indexForCode > 0 && retForCode[indexForCode - 1] == Model.AKhar.TakaiKrak)
                    {
                        indexForCode--;
                    }
                }

                //end the word
                if ((String.IsNullOrEmpty(word) || this._kanaingChars.ContainsKey(word.Substring(0, 1))) && ret.Count != 0)
                {
                    //Convert akhar diip to akhar matai
                    if (this._diipToMaTai.ContainsKey(akhar))
                    {
                        akhar = (Model.AKhar)this._diipToMaTai[akhar];
                        if (akhar == Model.AKhar.NgâkMatai)
                        {
                            if (!this._sappaohAngaok.ContainsKey(ret[ret.Count - 1]))
                            {
                                akhar = Model.AKhar.PaohNgâk;
                            }

                            int indexAkharDiip = this.GetIndexAkharDiip(ret);
                            if (indexAkharDiip - 1 >= 0)
                            {

                                Model.AKhar akharDengAnak = ret[indexAkharDiip - 1];
                                if (akharDengAnak == Model.AKhar.DarSa || akharDengAnak == Model.AKhar.DarDua)
                                {
                                    akhar = Model.AKhar.NgâkMatai;
                                }
                            }
                        }
                    }

                    //"i"
                    if (akhar == Model.AKhar.Ik)
                    {
                        akhar = Model.AKhar.TakaiKikTutTakaiMâkDalem;
                    }

                    //"é"
                    if (akhar == Model.AKhar.É)
                    {
                        akharList.Add(Model.AKhar.DarSa);
                        akharList.Add(Model.AKhar.BalauTapong);
                        continue;
                    }

                    //"o"
                    if (akhar == Model.AKhar.Ok)
                    {
                        akharList.Add(Model.AKhar.DarSa);
                        continue;
                    }

                    //"u"
                    if (akhar == Model.AKhar.Uk)
                    {
                        akharList.Add(Model.AKhar.TakaiKuk);
                        continue;
                    }

                    //"e"
                    if (akhar == Model.AKhar.TakaiThek && 
                        !(akharList.Count > 1 && akharList[akharList.Count - 2] == Model.AKhar.TakaiKuk)
                        && i == akharList.Count - 1 )
                    {
                        akharList.Add(Model.AKhar.BalauTapong);
                        continue;
                    }
                }

                //Remove takai kâkk when have consonant KareiCrih
                if (akhar == Model.AKhar.TakaiKâk && ret.Count != 0
                    && this._consonantKareiCrih.ContainsKey(ret[this.GetIndexAkharDiip(ret)]))
                {
                    retForCode.Insert(indexForCode, akhar);
                    continue;
                }

                ret.Insert(index, akhar);
                retForCode.Insert(indexForCode, akhar);
            }
        }

        /// <summary>
        /// Get akhar Cam
        /// </summary>
        /// <param name="value">String rumi value</param>
        /// <param name="retForCode">retsult for code</param>
        private List<Model.AKhar> ToKeyCodeByChar(ref String value, List<Model.AKhar> retForCode = null)
        {

            List<Model.AKhar> ret = new List<Model.AKhar>();
            int length = value.Length;
            String character = value;

            for (int i = character.Length; i > 0; i--)
            {

                //fix "IM" is takai kik & akhar nâk | fix "LU" Is Akhar Lak
                //fix "AI" Is Akhar Ak
                if ((character.CompareTo(this._trans_IM) == 0 && value.Length > 2)
                    || (character.CompareTo(this._trans_LU) == 0 && retForCode != null && (retForCode.Count == 0 || this.HuLanglikuk(retForCode)))
                    || (character.CompareTo(this._trans_LW) == 0 && retForCode != null && (retForCode.Count == 0 || this.HuLanglikuk(retForCode)))
                    || (character.CompareTo(this._trans_AI) == 0 && retForCode != null && (retForCode.Count == 0 || this.HuLanglikuk(retForCode)))
                    || (character.CompareTo(this._trans_AO) == 0 && retForCode != null && (retForCode.Count == 0 || this.HuLanglikuk(retForCode)))
                    || (character.CompareTo(this._trans_AU) == 0 && retForCode != null && (retForCode.Count == 0 || this.HuLanglikuk(retForCode))))
                {
                    character = character.Substring(0, character.Length - 1);
                    continue;
                }

                if (this._transToKeycode.ContainsKey(character))
                {
                    ret = Utility.CopyListAkhar((List<Model.AKhar>)this._transToKeycode[character]);
                    break;
                }

                character = character.Substring(0, character.Length - 1);
            }

            value = value.Substring(character.Length);
            return ret;
        }

        /// <summary>
        /// To keycode processing
        /// </summary>
        /// <param name="word">rumi word</param>
        /// <param name="ret">result</param>
        /// <param name="retForCode">result for coding</param>
        private void ToKeycodeFromInrasara(ref String word, ref List<Model.AKhar> ret, ref List<Model.AKhar> retForCode)
        {

            List<Model.AKhar> akharList = this.ToKeyCodeByChar(ref word, retForCode);
            for (int i = 0; i < akharList.Count; i++)
            {

                Model.AKhar akhar = akharList[i];
                if (akhar == Model.AKhar.Ak && !this.HuLanglikuk(retForCode) && ret.Count != 0)
                {
                    retForCode.Add(akhar);
                    continue;
                }

                //Get the next akhar
                Model.AKhar nextAkhar = Model.AKhar.Square;
                if (!String.IsNullOrEmpty(word))
                {

                    nextAkhar = this.GetNextChar(word);
                }

                //curent char is "i" or "é" and the next char is akhar matai
                if (!String.IsNullOrEmpty(word))
                {
                    //Convert akhar diip to takai akhar
                    if (this._diipToTaKai.ContainsKey(akhar) && !this._diipToMaTai.ContainsKey(nextAkhar)
                        && !this.HuLanglikuk(retForCode) && ret.Count != 0 && !this.Check_AkharMatai(ret[ret.Count - 1]))
                    {

                        if (!((akhar == Model.AKhar.Rak && ret.Count != 0 && this.IsVowels(ret[ret.Count - 1])) ||
                            ((akhar == Model.AKhar.Ik || akhar == Model.AKhar.Wak) && this.IsConsonant(nextAkhar) && !this._diipToMaTai.ContainsKey(nextAkhar))))
                        {

                            akhar = (Model.AKhar)this._diipToTaKai[akhar];
                        }
                    }

                    //"u","o"
                    if ((akhar == Model.AKhar.Ok || akhar == Model.AKhar.Uk)
                        && !this.HuLanglikuk(retForCode) && ret.Count != 0)
                    {
                        if (akhar == Model.AKhar.Ok)
                        {
                            akhar = Model.AKhar.DarSa;
                        }
                        else
                        {
                            akhar = Model.AKhar.TakaiKuk;
                        }
                    }

                    //Convert akhar diip to sap paoh
                    if ((this._diipToMaTai.ContainsKey(nextAkhar) && !this.HuLanglikuk(retForCode) && ret.Count != 0) ||
                        (this.IsConsonant(nextAkhar) && !this._diipToMaTai.ContainsKey(nextAkhar)))
                    {
                        if (akhar == Model.AKhar.Ik && ret.Count != 0)
                        {
                            akhar = Model.AKhar.TakaiKik;
                        }
                        else if (akhar == Model.AKhar.É)
                        {
                            akhar = Model.AKhar.DarSa;
                            akharList.Add(Model.AKhar.TakaiThek);
                        }
                    }

                    //TaikaiKlakTaikaiKuak & TaikaiKlakTaikaiKuk
                    if (akhar == Model.AKhar.TakaiKlakTakaiKuak)
                    {
                        if (!this.IsSapPaoh(nextAkhar))
                        {
                            akhar = Model.AKhar.TakaiKlakTakaiKuk;
                        }
                    }

                    //Mâk, Nâk, Nyâk, Ngâk
                    if (this._consonantKareiCrih.ContainsKey(akhar) && nextAkhar == Model.AKhar.Ak)
                    {
                        akhar = (Model.AKhar)this._consonantKareiCrih[akhar];
                    }
                }

                //Sap paoh deng anak akhar
                int index = ret.Count;
                int indexForCode = retForCode.Count;
                if (akhar == Model.AKhar.DarSa || akhar == Model.AKhar.DarDua || akhar == Model.AKhar.TakaiKrak)
                {

                    index = this.GetIndexAkharDiip(ret);
                    //fix have taikai Krak
                    if (index > 0 && ret[index - 1] == Model.AKhar.TakaiKrak)
                    {
                        index--;
                    }

                    indexForCode = this.GetIndexAkharDiip(retForCode);

                    //fix have taikai Krak
                    if (indexForCode > 0 && retForCode[indexForCode - 1] == Model.AKhar.TakaiKrak)
                    {
                        indexForCode--;
                    }
                }

                //end the word
                if ((String.IsNullOrEmpty(word) || this._kanaingChars.ContainsKey(word.Substring(0, 1))) && ret.Count != 0)
                {
                    //Convert akhar diip to akhar matai
                    if (this._diipToMaTai.ContainsKey(akhar))
                    {
                        akhar = (Model.AKhar)this._diipToMaTai[akhar];
                        if (akhar == Model.AKhar.NgâkMatai)
                        {
                            if (!this._sappaohAngaok.ContainsKey(ret[ret.Count - 1]))
                            {
                                akhar = Model.AKhar.PaohNgâk;
                            }

                            int indexAkharDiip = this.GetIndexAkharDiip(ret);
                            if (indexAkharDiip - 1 >= 0)
                            {
                                Model.AKhar akharDengAnak = ret[indexAkharDiip - 1];
                                if (akharDengAnak == Model.AKhar.DarSa || akharDengAnak == Model.AKhar.DarDua)
                                {

                                    akhar = Model.AKhar.NgâkMatai;
                                }
                            }
                        }
                    }

                    //"i"
                    if (akhar == Model.AKhar.Ik)
                    {
                        akhar = Model.AKhar.TakaiKikTutTakaiMâkDalem;
                    }

                    //"é"
                    if (akhar == Model.AKhar.É)
                    {
                        akharList.Add(Model.AKhar.DarSa);
                        akharList.Add(Model.AKhar.BalauTapong);
                        continue;
                    }

                    //"o"
                    if (akhar == Model.AKhar.Ok)
                    {
                        akharList.Add(Model.AKhar.DarSa);
                        continue;
                    }

                    //"u"
                    if (akhar == Model.AKhar.Uk)
                    {
                        akharList.Add(Model.AKhar.TakaiKuk);
                        continue;
                    }

                    //"e"
                    if (akhar == Model.AKhar.TakaiThek && 
                        !(akharList.Count > 1 && akharList[akharList.Count - 2] == Model.AKhar.TakaiKuk)
                        && i == akharList.Count - 1 )
                    {
                        akharList.Add(Model.AKhar.BalauTapong);
                        continue;
                    }
                }

                //Remove takai kâkk when have consonant KareiCrih
                if (akhar == Model.AKhar.TakaiKâk && ret.Count != 0
                    && this._consonantKareiCrih.ContainsKey(ret[this.GetIndexAkharDiip(ret)]))
                {
                    retForCode.Insert(indexForCode, akhar);
                    continue;
                }

                ret.Insert(index, akhar);
                retForCode.Insert(indexForCode, akhar);
            }
        }

        /// <summary>
        /// To keycode From KTT
        /// </summary>
        /// <param name="word">KTT word</param>
        /// <param name="ret">result</param>
        /// <param name="retForCode">result for coding</param>
        /// <param name="preChar">the prev char</param>
        private void ToKeycodeFromKTT(ref String word, ref List<Model.AKhar> ret, ref List<Model.AKhar> retForCode, ref Model.AKhar preChar)
        {
            List<Model.AKhar> akharList = this.ToKeyCodeByChar(ref word, retForCode);
            for (int i = 0; i < akharList.Count; i++)
            {
                Model.AKhar akhar = akharList[i];

                //Sap Atah Process
                if (retForCode.Count != 0 && this._sapAtah.ContainsKey(akhar) && akhar == preChar)
                {
                    Model.AKhar akharAtah = (Model.AKhar)this._sapAtah[akhar];
                    if (akharAtah == Model.AKhar.BalauTapong)
                    {
                        ret.RemoveAt(ret.Count - 1);
                        retForCode.RemoveAt(retForCode.Count - 1);
                    }

                    ret.Add(akharAtah);
                    retForCode.Add(akharAtah);
                    continue;
                }

                //Set prev char
                preChar = akhar;
                if (akhar == Model.AKhar.Ak && !this.HuLanglikuk(retForCode) && ret.Count != 0)
                {
                    retForCode.Add(akhar);
                    continue;
                }

                //Get the next akhar
                Model.AKhar nextAkhar = Model.AKhar.Square;
                if (!String.IsNullOrEmpty(word))
                {
                    nextAkhar = this.GetNextChar(word);
                }

                //curent char is "i" or "é" and the next char is akhar matai
                if (!String.IsNullOrEmpty(word))
                {
                    //Convert akhar diip to takai akhar
                    if (this._diipToTaKai.ContainsKey(akhar) && !this._diipToMaTai.ContainsKey(nextAkhar)
                        && !this.HuLanglikuk(retForCode) && ret.Count != 0 && !this.Check_AkharMatai(ret[ret.Count - 1]))
                    {

                        if (!((akhar == Model.AKhar.Rak && ret.Count != 0 && this.IsVowels(ret[ret.Count - 1])) ||
                            ((akhar == Model.AKhar.Ik || akhar == Model.AKhar.Uk) && this.IsConsonant(nextAkhar) && !this._diipToMaTai.ContainsKey(nextAkhar))))
                        {
                            akhar = (Model.AKhar)this._diipToTaKai[akhar];
                        }
                    }

                    //"u","o"
                    if ((akhar == Model.AKhar.Ok || akhar == Model.AKhar.Uk)
                        && !this.HuLanglikuk(retForCode) && ret.Count != 0)
                    {
                        if (akhar == Model.AKhar.Ok)
                        {
                            akhar = Model.AKhar.DarSa;
                        }
                        else
                        {
                            akhar = Model.AKhar.TakaiKuk;
                        }
                    }

                    //Convert akhar diip to sap paoh
                    if ((this._diipToMaTai.ContainsKey(nextAkhar) && !this.HuLanglikuk(retForCode) && ret.Count != 0) ||
                        (this.IsConsonant(nextAkhar) && !this._diipToMaTai.ContainsKey(nextAkhar)))
                    {
                        if (akhar == Model.AKhar.Ik && ret.Count != 0)
                        {
                            akhar = Model.AKhar.TakaiKik;
                        }
                        else if (akhar == Model.AKhar.É)
                        {
                            akhar = Model.AKhar.DarSa;
                            akharList.Add(Model.AKhar.TakaiThek);
                        }
                    }

                    //TaikaiKlakTaikaiKuak & TaikaiKlakTaikaiKuk
                    if (akhar == Model.AKhar.TakaiKlakTakaiKuak)
                    {
                        if (!this.IsSapPaoh(nextAkhar))
                        {
                            akhar = Model.AKhar.TakaiKlakTakaiKuk;
                        }
                    }

                    //Mâk, Nâk, Nyâk, Ngâk
                    if (this._consonantKareiCrih.ContainsKey(akhar) && nextAkhar == Model.AKhar.Ak)
                    {
                        akhar = (Model.AKhar)this._consonantKareiCrih[akhar];
                    }
                }

                //Convert akhar ai to DarDua
                if (akhar == Model.AKhar.Ai && ret.Count != 0 && this.IsConsonant(ret[ret.Count - 1]))
                {
                    akhar = Model.AKhar.DarDua;
                }

                //Sap paoh deng anak akhar
                int index = ret.Count;
                int indexForCode = retForCode.Count;
                if (akhar == Model.AKhar.DarSa || akhar == Model.AKhar.DarDua || akhar == Model.AKhar.TakaiKrak)
                {
                    index = this.GetIndexAkharDiip(ret);
                    //fix have taikai Krak
                    if (index > 0 && ret[index - 1] == Model.AKhar.TakaiKrak)
                    {
                        index--;
                    }

                    indexForCode = this.GetIndexAkharDiip(retForCode);
                    //fix have taikai Krak
                    if (indexForCode > 0 && retForCode[indexForCode - 1] == Model.AKhar.TakaiKrak)
                    {
                        indexForCode--;
                    }
                }

                //end the word
                if ((String.IsNullOrEmpty(word) || this._kanaingChars.ContainsKey(word.Substring(0, 1))) && ret.Count != 0)
                {
                    //Convert akhar diip to akhar matai
                    if (this._diipToMaTai.ContainsKey(akhar))
                    {
                        akhar = (Model.AKhar)this._diipToMaTai[akhar];
                        if (akhar == Model.AKhar.NgâkMatai)
                        {
                            if (!this._sappaohAngaok.ContainsKey(ret[ret.Count - 1]))
                            {
                                akhar = Model.AKhar.PaohNgâk;
                            }

                            int indexAkharDiip = this.GetIndexAkharDiip(ret);
                            if (indexAkharDiip - 1 >= 0)
                            {
                                Model.AKhar akharDengAnak = ret[indexAkharDiip - 1];
                                if (akharDengAnak == Model.AKhar.DarSa || akharDengAnak == Model.AKhar.DarDua)
                                {
                                    akhar = Model.AKhar.NgâkMatai;
                                }
                            }
                        }
                    }

                    //"i"
                    if (akhar == Model.AKhar.Ik)
                    {
                        akhar = Model.AKhar.TakaiKikTutTakaiMâkDalem;
                    }

                    //"é"
                    if (akhar == Model.AKhar.É)
                    {
                        akharList.Add(Model.AKhar.DarSa);
                        akharList.Add(Model.AKhar.BalauTapong);
                        continue;
                    }

                    //"o"
                    if (akhar == Model.AKhar.Ok)
                    {
                        akharList.Add(Model.AKhar.DarSa);
                        continue;
                    }

                    //"u"
                    if (akhar == Model.AKhar.Uk)
                    {
                        akharList.Add(Model.AKhar.TakaiKuk);
                        continue;
                    }

                    //"e"
                    if (akhar == Model.AKhar.TakaiThek && 
                        !(akharList.Count > 1 && akharList[akharList.Count - 2] == Model.AKhar.TakaiKuk)
                        && i == akharList.Count - 1 )
                    {
                        akharList.Add(Model.AKhar.BalauTapong);
                        continue;
                    }
                }

                //Remove takai kâkk when have consonant KareiCrih
                if (akhar == Model.AKhar.TakaiKâk && ret.Count != 0
                    && this._consonantKareiCrih.ContainsKey(ret[this.GetIndexAkharDiip(ret)]))
                {
                    retForCode.Insert(indexForCode, akhar);
                    continue;
                }

                ret.Insert(index, akhar);
                retForCode.Insert(indexForCode, akhar);
            }
        }

        /// <summary>
        /// Get next akhar
        /// </summary>
        /// <param name="word">word by rumi</param>
        /// <returns>akhar cam</returns>
        private Model.AKhar GetNextChar(String word)
        {
            Model.AKhar ret = Model.AKhar.Square;
            List<Model.AKhar> nextAkharList = this.ToKeyCodeByChar(ref word);
            if (nextAkharList.Count != 0)
            {
                ret = nextAkharList[0];
            }

            return ret;
        }

        /// <summary>
        /// Init trans data converting
        /// </summary>
        /// <param name="sourceType">sourceType</param>
        protected void InitTransToCamData(Model.Enum sourceType, Model.Enum destinationType)
        {

            base.InitTransToCamData(sourceType);
            Utility.SetTransToKeyCode(ref this._transToKeycode, sourceType);

            this._trans_IM = "IM";
            this._trans_AI = "AI";
            this._trans_LU = "LU";
            switch (sourceType)
            {
                case Model.Enum.TransCamEFEO:
                    this._trans_LW = "LU";
                    this._trans_AO = "AO";
                    this._trans_AU = "AU";
                    break;

                default:
                    this._trans_LW = "LW";
                    this._trans_AO = "AU";
                    break;
            }

            if (sourceType == Model.Enum.TransKawomTT)
            {

                this._sapAtah = Utility.InitSapAtah();
            }
        }

        /// <summary>
        /// Cut kanaing at first of word
        /// </summary>
        /// <param name="value">word by string</param>
        /// <returns>Stack of khar</returns>
        protected Stack<Model.AKhar> CutKanaingAtFirst(ref String value)
        {

            Stack<Model.AKhar> ret = new Stack<Model.AKhar>();

            int roundCount = 0;
            while (!String.IsNullOrEmpty(value))
            {

                if (roundCount == Model.Constant.MAX_ROUND)
                {
                    break;
                }

                String achar = value.Substring(0, 1);
                if (!this._kanaingChars.ContainsKey(achar)) break;

                List<Model.AKhar> listKanaing = (List<Model.AKhar>)this._transToKeycode[achar];
                ret.Push(listKanaing[0]);

                value = value.Substring(1);
                roundCount++;
            }

            return ret;
        }

        /// <summary>
        /// Cut kanaing at last of word
        /// </summary>
        /// <param name="value">word by string</param>
        /// <returns>Stack of khar</returns>
        protected Stack<Model.AKhar> CutKanaingAtLast(ref String value)
        {

            Stack<Model.AKhar> ret = new Stack<Model.AKhar>();

            int roundCount = 0;
            while (!String.IsNullOrEmpty(value))
            {
                if (roundCount == Model.Constant.MAX_ROUND) break;

                String achar = value.Substring(value.Length - 1, 1);
                if (!this._kanaingChars.ContainsKey(achar)) break;

                List<Model.AKhar> listKanaing = (List<Model.AKhar>)this._transToKeycode[achar];
                ret.Push(listKanaing[0]);

                value = value.Substring(0, value.Length - 1);

                roundCount++;
            }

            return ret;
        }

        #endregion
    }

}

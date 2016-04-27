using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Cam.Service
{

    /// <summary>
    /// Font converting
    /// </summary>
    public class FontToFontPaxalih : Paxalih
    {
        #region variables

        private Hashtable _srcHtb;
        private Hashtable _desHtb;

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

            //Set hastable data
            this.SetHastableData(sourceType, destitionType);

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

                List<Model.AKhar> lstAkhar = this.ToKeyCodeByWord(word, sourceType, destitionType);
                String convertedWord = String.Empty;

                foreach (Model.AKhar akhar in lstAkhar)
                {

                    if (this._desHtb.ContainsKey((int)akhar))
                    {

                        convertedWord += this._desHtb[(int)akhar].ToString();//to_unicode???
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
        /// <param name="sourceType">sourceType</param>
        /// <param name="destitionType">destitionType</param>
        private List<Model.AKhar> ToKeyCodeByWord(String word, Model.Enum sourceType, Model.Enum destitionType)
        {
            try
            {

                List<Model.AKhar> ret = new List<Model.AKhar>();

                foreach (Char chr in word)
                {

                    //Get current akhar
                    Model.AKhar saboah = (Model.AKhar)this._srcHtb[chr.ToString()];

                    //Just correct for FontUniCamKur & FontUniCamVN
                    if (destitionType == Model.Enum.FontUniCamKur || destitionType == Model.Enum.FontUniCamVN)
                    {
                        //Baluw tapong
                        if (saboah == Model.AKhar.BalauTapong)
                        {

                            ret.Add(Model.AKhar.Balau);
                            ret.Add(Model.AKhar.TakaiThek);
                            continue;
                        }

                        //Takai Thek Tut Takai Mâk
                        if (saboah == Model.AKhar.TakaiThekTutTakaiMâk)
                        {

                            ret.Add(Model.AKhar.TakaiThek);
                            ret.Add(Model.AKhar.TutTakaiMâk);
                            continue;
                        }

                        //Takai Kik Tut Takai Mâk Lingiw
                        if (saboah == Model.AKhar.TakaiKikTutTakaiMâkLingiw)
                        {

                            ret.Add(Model.AKhar.TakaiKik);
                            ret.Add(Model.AKhar.TutTakaiMâk);
                            continue;
                        }

                        //Traok Aw Paoh Ngâk
                        if (saboah == Model.AKhar.TraohAwPaohNgâk)
                        {

                            ret.Add(Model.AKhar.TraohAw);
                            ret.Add(Model.AKhar.PaohNgâk);
                            continue;
                        }

                        //Traok Aw Tut Takai Mâk
                        if (saboah == Model.AKhar.TraohAwTutTakaiMâk)
                        {

                            ret.Add(Model.AKhar.TraohAw);
                            ret.Add(Model.AKhar.TutTakaiMâk);
                            continue;
                        }
                    }

                    //Correct with FontUniCamKur and FontCamEFEO
                    if ((destitionType == Model.Enum.FontCamEFEO && sourceType != Model.Enum.FontUniCamKur) || (destitionType == Model.Enum.FontUniCamKur && sourceType != Model.Enum.FontCamEFEO)
                        || (destitionType == Model.Enum.FontCamEFEO && sourceType != Model.Enum.FontUniCamVN) || (destitionType == Model.Enum.FontUniCamVN && sourceType != Model.Enum.FontCamEFEO))
                    {

                        //Takai Klak Takai Kuak
                        if (saboah == Model.AKhar.TakaiKlakTakaiKuak)
                        {

                            ret.Add(Model.AKhar.TakaiKlak);
                            ret.Add(Model.AKhar.TakaiKuak);
                            continue;
                        }

                        //Takai Klak Takai Kuk
                        if (saboah == Model.AKhar.TakaiKlakTakaiKuk)
                        {

                            ret.Add(Model.AKhar.TakaiKlak);
                            ret.Add(Model.AKhar.TakaiKuk);
                            continue;
                        }

                        //Takai Thek Paoh Ngâk
                        if (saboah == Model.AKhar.TakaiThekPaohNgâk)
                        {

                            ret.Add(Model.AKhar.TakaiThek);
                            ret.Add(Model.AKhar.PaohNgâk);
                            continue;
                        }
                        
                    }

                    //Gak Mâtai
                    if ((destitionType == Model.Enum.FontCamEFEO || destitionType == Model.Enum.FontGilaiPraong) && saboah == Model.AKhar.GakMatai)
                    {

                        ret.Add(Model.AKhar.KakMatai);
                        continue;
                    }

                    if (sourceType == Model.Enum.FontUniCamKur || sourceType == Model.Enum.FontUniCamVN)
                    {
                        //Baluw Tapong
                        if (saboah == Model.AKhar.TakaiThek && ret.Count != 0 && ret[ret.Count - 1] == Model.AKhar.Balau)
                        {

                            ret.RemoveAt(ret.Count - 1);
                            ret.Add(Model.AKhar.BalauTapong);
                            continue;
                        }

                        //Takai Thek Tut Takai Mâk
                        if (saboah == Model.AKhar.TutTakaiMâk && ret.Count != 0 && ret[ret.Count - 1] == Model.AKhar.TakaiThek)
                        {

                            ret.RemoveAt(ret.Count - 1);
                            ret.Add(Model.AKhar.TakaiThekTutTakaiMâk);
                            continue;
                        }

                        //Takai Kik Tut Takai Mâk Lingiw
                        if (saboah == Model.AKhar.TutTakaiMâk && ret.Count != 0 && ret[ret.Count - 1] == Model.AKhar.TakaiKik)
                        {

                            ret.RemoveAt(ret.Count - 1);
                            ret.Add(Model.AKhar.TakaiKikTutTakaiMâkLingiw);
                            continue;
                        }

                        //Traok Aw Paoh Ngâk
                        if (saboah == Model.AKhar.PaohNgâk && ret.Count != 0 && ret[ret.Count - 1] == Model.AKhar.TraohAw)
                        {

                            ret.RemoveAt(ret.Count - 1);
                            ret.Add(Model.AKhar.TraohAwPaohNgâk);
                            continue;
                        }

                        //Traok Aw Tut Takai Mâk
                        if (saboah == Model.AKhar.TutTakaiMâk && ret.Count != 0 && ret[ret.Count - 1] == Model.AKhar.TraohAw)
                        {

                            ret.RemoveAt(ret.Count - 1);
                            ret.Add(Model.AKhar.TraohAwTutTakaiMâk);
                            continue;
                        }
                    }

                    //if (sourceType == Model.Enum.FontCamEFEO || sourceType == Model.Enum.FontUniCamKur)
                    if ((sourceType == Model.Enum.FontCamEFEO && destitionType != Model.Enum.FontUniCamKur) || (sourceType == Model.Enum.FontUniCamKur && destitionType != Model.Enum.FontCamEFEO)
                        || (sourceType == Model.Enum.FontCamEFEO && destitionType != Model.Enum.FontUniCamVN) || (sourceType == Model.Enum.FontUniCamVN && destitionType != Model.Enum.FontCamEFEO))
                    {

                        //Takai Klak Takai Kuak
                        if (saboah == Model.AKhar.TakaiKuak && ret.Count != 0 && ret[ret.Count - 1] == Model.AKhar.TakaiKlak)
                        {

                            ret.RemoveAt(ret.Count - 1);
                            ret.Add(Model.AKhar.TakaiKlakTakaiKuak);
                            continue;
                        }

                        //Takai Klak Takai Kuk
                        if (saboah == Model.AKhar.TakaiKuk && ret.Count != 0 && ret[ret.Count - 1] == Model.AKhar.TakaiKlak)
                        {

                            ret.RemoveAt(ret.Count - 1);
                            ret.Add(Model.AKhar.TakaiKlakTakaiKuk);
                            continue;
                        }

                        //Takai Thek Paoh Ngâk
                        if (saboah == Model.AKhar.PaohNgâk && ret.Count != 0 && ret[ret.Count - 1] == Model.AKhar.TakaiThek)
                        {

                            ret.RemoveAt(ret.Count - 1);
                            ret.Add(Model.AKhar.TakaiThekPaohNgâk);
                            continue;
                        }
                        
                    }

                    ret.Add(saboah);
                }

                return ret;
            }
            catch (Exception ex)
            {

                Service.Log.WriteLog(ex);
                return new List<Model.AKhar>();
            }
        }

        /// <summary>
        /// Set hastable datra
        /// </summary>
        /// <param name="sourceType">sourceType</param>
        /// <param name="destitionType">destitionType</param>
        private void SetHastableData(Model.Enum sourceType, Model.Enum destitionType)
        {

            //Set source data
            switch (sourceType)
            {
                case Model.Enum.FontYapata:
                    this._srcHtb = this._waYapataToKeyCode;
                    break;

                case Model.Enum.FontGilaiPraong:
                    this._srcHtb = this._gilaiPraongToKeyCode;
                    break;

                case Model.Enum.FontCamEFEO:
                    this._srcHtb = this._camEFEOToKeyCode;
                    break;

                case Model.Enum.FontUniCamKur:
                    this._srcHtb = this._uniCamKurToKeyCode;
                    break;

                case Model.Enum.FontUniCamVN:
                    this._srcHtb = this._uniCamVNToKeyCode;
                    break;

                default:
                    this._srcHtb = this._KTTToKeyCode;
                    break;
            }

            //Set destination data
            switch (destitionType)
            {
                case Model.Enum.FontYapata:
                    this._desHtb = this._keyCodeToWaYapata;
                    break;

                case Model.Enum.FontGilaiPraong:
                    this._desHtb = this._keyCodeToGilaiPraong;
                    break;

                case Model.Enum.FontCamEFEO:
                    this._desHtb = this._keyCodeToCamEFEO;
                    break;

                case Model.Enum.FontUniCamKur:
                    this._desHtb = this._keyCodeToUniCamKur;
                    break;

                case Model.Enum.FontUniCamVN:
                    this._desHtb = this._keyCodeToUniCamVN;
                    break;

                default:
                    this._desHtb = this._keyCodeToKTT;
                    break;
            }
        }

        #endregion
    }
}

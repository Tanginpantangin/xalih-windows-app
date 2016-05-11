using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Reflection;
using System.Collections;
using System.Data;
using System.IO;

namespace Cam.Service
{

    /// <summary>
    /// Utility
    /// </summary>
    public static class Utility
    {
        #region Enum

        /// <summary>
        /// Get Enum Description
        /// </summary>
        /// <param name="value">enum value</param>
        /// <returns>Enum Description</returns>
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            string result = value.ToString();
            if (attributes != null && attributes.Length > 0)
            {
                result = attributes[0].Description;
            }

            return result;
        }

        /// <summary>
        /// Convert Number to Enum
        /// </summary>
        /// <typeparam name="T">enum type</typeparam>
        /// <param name="number">number</param>
        /// <returns>enum value</returns>
        public static T NumToEnum<T>(int number)
        {

            return (T)Enum.ToObject(typeof(T), number);
        }

        #endregion

        #region XML

        /// <summary>
        /// Set Key code from XML file into hastable
        /// </summary>
        /// <param name="waQuyetKeyCode">waQuyetKeyCode</param>
        /// <param name="gilaiPraongKeyCode">gilaiPraongKeyCode</param>
        /// <param name="camEFEOKeyCode">camEFEOKeyCode</param>
        /// <param name="kawomTTKeyCode">kawomTTKeyCode</param>
        /// <param name="uniCamKurKeyCode">uniCamKurKeyCode</param>
        /// <param name="uniCamVNKeyCode">uniCamVNKeyCode</param>
        /// <param name="keyCodeWaQuyet">keyCodeWaQuyet</param>
        /// <param name="keyCodeGilaiPraong">keyCodeGilaiPraong</param>
        /// <param name="keyCodeCamEFEO">keycodeCamEFEO</param>
        /// <param name="keycodeKawomTT">keycodeKawomTT</param>
        /// <param name="keyCodeUniCamKur">keyCodeUniCamKur</param>
        /// <param name="keyCodeUniCamVN">keyCodeUniCamVN</param>
        public static void SetKeyCodeData(ref Hashtable waQuyetKeyCode, ref Hashtable gilaiPraongKeyCode, ref Hashtable camEFEOKeyCode, ref Hashtable kawomTTKeyCode, ref Hashtable uniCamKurKeyCode, ref Hashtable uniCamVNKeyCode,
                                          ref Hashtable keyCodeWaQuyet, ref Hashtable keyCodeGilaiPraong, ref Hashtable keyCodeCamEFEO, ref Hashtable keyCodeKawomTT, ref Hashtable keyCodeUniCamKur, ref Hashtable keyCodeUniCamVN) 
        {
            //Cam - Keycode
            waQuyetKeyCode = new Hashtable();
            gilaiPraongKeyCode = new Hashtable();
            camEFEOKeyCode = new Hashtable();
            kawomTTKeyCode = new Hashtable();
            uniCamKurKeyCode = new Hashtable();
            uniCamVNKeyCode = new Hashtable();

            //Keycode - Cam
            keyCodeWaQuyet = new Hashtable();
            keyCodeGilaiPraong = new Hashtable();
            keyCodeCamEFEO = new Hashtable();
            keyCodeKawomTT = new Hashtable();
            keyCodeUniCamKur = new Hashtable();
            keyCodeUniCamVN = new Hashtable();

            //Read XML file
            Stream strm = ReadXMLFile(Model.Constant.KEY_TO_FONT_FILE);
            DataSet dts = new DataSet();
            dts.ReadXml(strm);

            DataTable dtt = dts.Tables[0];

            foreach (DataRow dtr in dtt.Rows)
            {

                int keyCode = Convert.ToInt32(dtr[Convert.ToInt32(Model.XMLKeyCodeCol.KeyCode)]);
                String waQuyet = dtr[Convert.ToInt32(Model.XMLKeyCodeCol.WaQuyet)].ToString();
                String gilaiPraong = dtr[Convert.ToInt32(Model.XMLKeyCodeCol.GilaiPraong)].ToString();
                String camEFEO = dtr[Convert.ToInt32(Model.XMLKeyCodeCol.CamEFEO)].ToString();
                String kawomTuekTuah = dtr[Convert.ToInt32(Model.XMLKeyCodeCol.KawomTuekTuah)].ToString();
                String uniCamKur = dtr[Convert.ToInt32(Model.XMLKeyCodeCol.UnicodeCamKur)].ToString();
                String uniCamVN = dtr[Convert.ToInt32(Model.XMLKeyCodeCol.UnicodeCamVN)].ToString();

                //WaQuyet - Key code
                if (!waQuyetKeyCode.ContainsKey(waQuyet))
                {
                    waQuyetKeyCode.Add(waQuyet, keyCode);
                }

                //GilaiPraong - Key code
                if (!gilaiPraongKeyCode.ContainsKey(gilaiPraong))
                {
                    gilaiPraongKeyCode.Add(gilaiPraong, keyCode);
                }

                //CamEFEO - Key code
                if (!camEFEOKeyCode.ContainsKey(camEFEO))
                {
                    camEFEOKeyCode.Add(camEFEO, keyCode);
                }

                //KawomTuekTuah - Key code
                if (!kawomTTKeyCode.ContainsKey(kawomTuekTuah))
                {
                    kawomTTKeyCode.Add(kawomTuekTuah, keyCode);
                }

                //UniCamKur - Key code
                if (!uniCamKurKeyCode.ContainsKey(uniCamKur))
                {
                    uniCamKurKeyCode.Add(uniCamKur, keyCode);
                }

                // Key code - WaQuyet
                if (!keyCodeWaQuyet.ContainsKey(keyCode))
                {
                    keyCodeWaQuyet.Add(keyCode, waQuyet);
                }

                //Key code - GilaiPraong
                if (!keyCodeGilaiPraong.ContainsKey(keyCode))
                {
                    keyCodeGilaiPraong.Add(keyCode, gilaiPraong);
                }

                //Key code - CamEFEO
                if (!keyCodeCamEFEO.ContainsKey(keyCode))
                {
                    keyCodeCamEFEO.Add(keyCode, camEFEO);
                }

                //Key code - KawomTuekTuah
                if (!keyCodeKawomTT.ContainsKey(keyCode))
                {
                    keyCodeKawomTT.Add(keyCode, kawomTuekTuah);
                }

                //Key code - UniCamKur
                if (!keyCodeUniCamKur.ContainsKey(keyCode))
                {
                    keyCodeUniCamKur.Add(keyCode, uniCamKur);
                }

                //Key code - UniCamVN
                if (!keyCodeUniCamVN.ContainsKey(keyCode))
                {
                    keyCodeUniCamVN.Add(keyCode, uniCamKur);
                }
            }
        }

        /// <summary>
        /// Set Rumi chars from XML file into hastable
        /// </summary>
        /// <param name="keyCodeTrans">keyCodeRumi</param>
        public static void SetTransFromXML(ref Hashtable keyCodeTrans, Model.Enum desType)
        {
            Model.XMLRumiCol colNo = Model.XMLRumiCol.Rumi;
            if (desType == Model.Enum.TransCamEFEO)
            {
                colNo = Model.XMLRumiCol.Rumi;
            }
            else if (desType == Model.Enum.TransInrasara)
            {
                colNo = Model.XMLRumiCol.InraSara;
            }
            else if (desType == Model.Enum.TransKawomTT)
            {
                colNo = Model.XMLRumiCol.KawomTT;
            }

            keyCodeTrans = new Hashtable();

            Stream strm = ReadXMLFile(Model.Constant.KEY_TO_TRANS_FILE);
            DataSet dts = new DataSet();
            dts.ReadXml(strm);

            DataTable dtt = dts.Tables[0];

            foreach (DataRow dtr in dtt.Rows)
            {

                Model.AKhar keyCode = (Model.AKhar)Convert.ToInt32(dtr[Convert.ToInt32(Model.XMLRumiCol.KeyCode)]);
                String trans = dtr[Convert.ToInt32(colNo)].ToString();

                //Keycode - Trans
                if (!keyCodeTrans.ContainsKey(keyCode))
                {
                    keyCodeTrans.Add(keyCode, trans);
                }
            }
        }

        /// <summary>
        /// Set Rumi chars key code from XML file into hastable
        /// </summary>
        /// <param name="transKeycode">rumiKeycode</param>
        public static void SetTransToKeyCode(ref Hashtable transKeycode, Model.Enum desType)
        {

            transKeycode = new Hashtable();

            Model.XMLTransToKeyCol columnNo = Model.XMLTransToKeyCol.Rumi;
            String sapPaohAO = String.Empty;
            String sapPaohAOM = String.Empty;
            String sapPaohAONG = String.Empty;
            String sapPaohEM = String.Empty;
            String akharIÉNG = String.Empty;
            String akharIENG = String.Empty;
            String sapPaohAU = String.Empty;

            if (desType == Model.Enum.TransCamEFEO)
            {
                columnNo = Model.XMLTransToKeyCol.Rumi;
                sapPaohAO = Model.Constant.AO_EFEO;
                sapPaohAOM = Model.Constant.AOM_EFEO;
                sapPaohAONG = Model.Constant.AONG_EFEO;
                sapPaohEM = Model.Constant.EM_EFEO;
                akharIÉNG = Model.Constant.IÉNG_EFEO;
                akharIENG = Model.Constant.IENG_EFEO;
                sapPaohAU = Model.Constant.AU_EFEO;
            }

            else if (desType == Model.Enum.TransInrasara)
            {
                columnNo = Model.XMLTransToKeyCol.InraSara;
                sapPaohAO = Model.Constant.AO_SARA;
                sapPaohAOM = Model.Constant.AOM_SARA;
                sapPaohAONG = Model.Constant.AONG_SARA;
                sapPaohEM = Model.Constant.EM_SARA;
                akharIÉNG = Model.Constant.IÉNG_SARA;
                akharIENG = Model.Constant.IENG_SARA;
                sapPaohAU = Model.Constant.AU_SARA;
            }
            else if (desType == Model.Enum.TransKawomTT)
            {
                columnNo = Model.XMLTransToKeyCol.KawomTuekTuah;
                sapPaohAO = Model.Constant.AO_KawomTT;
                sapPaohAOM = Model.Constant.AOM_KawomTT;
                sapPaohAONG = Model.Constant.AONG_KawomTT;
                sapPaohEM = Model.Constant.EM_KawomTT;
                akharIÉNG = Model.Constant.IÉNG_KawomTT;
                akharIENG = Model.Constant.IENG_KawomTT;
                sapPaohAU = Model.Constant.AU_KawomTT;

                List<Model.AKhar> sappaohE = new List<Model.AKhar>();
                sappaohE.Add(Model.AKhar.DarDua);
                transKeycode.Add("E", sappaohE);

                List<Model.AKhar> iim = new List<Model.AKhar>();
                iim.Add(Model.AKhar.TakaiKiak);
                iim.Add(Model.AKhar.TakaiKikTutTakaiMâkLingiw);
                transKeycode.Add("IIM", iim);
            }

            //Add au
            List<Model.AKhar> sappaohAu = new List<Model.AKhar>();
            sappaohAu.Add(Model.AKhar.TakaiKuk);
            sappaohAu.Add(Model.AKhar.TakaiThek);
            transKeycode.Add(sapPaohAU, sappaohAu);

            //Add iéng
            List<Model.AKhar> lstTemp = new List<Model.AKhar>();
            lstTemp.Add(Model.AKhar.DarSa);
            lstTemp.Add(Model.AKhar.TakaiKiak);
            lstTemp.Add(Model.AKhar.TakaiThekPaohNgâk);
            transKeycode.Add(akharIÉNG, lstTemp);

            //Add ieng
            lstTemp = new List<Model.AKhar>();
            lstTemp.Add(Model.AKhar.TakaiKiak);
            lstTemp.Add(Model.AKhar.TakaiThekPaohNgâk);
            transKeycode.Add(akharIENG, lstTemp);

            Stream strm = ReadXMLFile(Model.Constant.TRANS_TO_KEY_FILE);
            DataSet dts = new DataSet();
            dts.ReadXml(strm);

            DataTable dtt = dts.Tables[0];

            foreach (DataRow dtr in dtt.Rows)
            {
                Model.AKhar akhar = (Model.AKhar)Convert.ToInt32(dtr[Convert.ToInt32(Model.XMLTransToKeyCol.KeyCode)]);
                String transChar = dtr[Convert.ToInt32(columnNo)].ToString().ToUpper();
                List<Model.AKhar> listAkhar = new List<Model.AKhar>();

               //Keycode - Rumi
                bool isIrregular = false;
                if (!transKeycode.ContainsKey(transChar))
                {

                    if (transChar == sapPaohAO)
                    {
                        listAkhar.Add(Model.AKhar.DarSa);
                        listAkhar.Add(Model.AKhar.TraohAw);
                        isIrregular = true;
                    }

                    if (transChar == sapPaohAOM)
                    {
                        listAkhar.Add(Model.AKhar.DarSa);
                        listAkhar.Add(Model.AKhar.TraohAwTutTakaiMâk);
                        isIrregular = true;
                    }

                    if (transChar == sapPaohAONG)
                    {
                        listAkhar.Add(Model.AKhar.DarSa);
                        listAkhar.Add(Model.AKhar.TraohAwPaohNgâk);
                        isIrregular = true;
                    }

                    if (transChar == sapPaohEM)
                    {
                        listAkhar.Add(Model.AKhar.DarSa);
                        listAkhar.Add(Model.AKhar.TakaiThek);
                        listAkhar.Add(Model.AKhar.TakaiThekTutTakaiMâk);
                        isIrregular = true;
                    }

                    if (!isIrregular)
                    {
                        listAkhar.Add(akhar);
                    }

                    transKeycode.Add(transChar, listAkhar);
                }
            }

            //add au, am dai
            if (desType == Model.Enum.TransKawomTT)
            {
                List<Model.AKhar> listAkhar = new List<Model.AKhar>();
                listAkhar.Add(Model.AKhar.DarSa);
                listAkhar.Add(Model.AKhar.TraohAw);

                transKeycode.Add(sapPaohAO, listAkhar);

                //OO
                transKeycode["OO"] = listAkhar;
                
                //AA
                listAkhar = new List<Model.AKhar>();
                listAkhar.Add(Model.AKhar.Balau);
                transKeycode.Add("AA", listAkhar);

                //UU
                listAkhar = new List<Model.AKhar>();
                listAkhar.Add(Model.AKhar.Balau);
                listAkhar.Add(Model.AKhar.TakaiKuk);
                transKeycode.Add("UU", listAkhar);

                //EE
                listAkhar = new List<Model.AKhar>();
                listAkhar.Add(Model.AKhar.DarDua);
                listAkhar.Add(Model.AKhar.Balau);
                transKeycode.Add("EE", listAkhar);

                //ƯƯ
                listAkhar = new List<Model.AKhar>();
                listAkhar.Add(Model.AKhar.TakaiKâk);
                listAkhar.Add(Model.AKhar.Balau);
                transKeycode.Add("ƯƯ", listAkhar);

                //ƠƠ
                listAkhar = new List<Model.AKhar>();
                listAkhar.Add(Model.AKhar.BalauTapong);
                transKeycode.Add("ƠƠ", listAkhar);

                //II
                listAkhar = new List<Model.AKhar>();
                listAkhar.Add(Model.AKhar.TakaiKikTutTakaiMâkDalem);
                transKeycode.Add("II", listAkhar);
            }
        }

        /// <summary>
        /// Set valid Trans chars from XML file into hastable
        /// </summary>
        /// <param name="validCamEFEO">validCamEFEO</param>
        /// <param name="validInra">validInra</param>
        /// <param name="validKTT">validKTT</param>
        public static void SetValidTransChar(ref Hashtable validCamEFEO, ref Hashtable validInra, ref Hashtable validKTT)
        {
            validCamEFEO = new Hashtable();
            validInra = new Hashtable();
            validKTT = new Hashtable();

            Stream strm = ReadXMLFile(Model.Constant.VALID_TRANS_CHAR_FILE);
            DataSet dts = new DataSet();
            dts.ReadXml(strm);

            DataTable dtt = dts.Tables[0];
            foreach (DataRow dtr in dtt.Rows)
            {

                //Valid Rumi CharWW
                String rumiChar = dtr[0].ToString().ToUpper();
                if (!String.IsNullOrEmpty(rumiChar) && !validCamEFEO.ContainsKey(rumiChar))
                {
                    validCamEFEO.Add(rumiChar, null);
                }

                //Valid Inrasara Char
                String inraChar = dtr[1].ToString().ToUpper();
                if (!String.IsNullOrEmpty(inraChar) && !validInra.ContainsKey(inraChar))
                {
                    validInra.Add(inraChar, null);
                }

                //Valid KTT Char
                String kttChar = dtr[2].ToString().ToUpper();
                if (!String.IsNullOrEmpty(kttChar) && !validKTT.ContainsKey(kttChar))
                {
                    validKTT.Add(kttChar, null);
                }
            }
        }

        /// <summary>
        /// Read XML File
        /// </summary>
        /// <param name="fileName">fileName</param>
        private static Stream ReadXMLFile(String fileName)
        {
            Windows.Forms.FrmBase frmBase = new Windows.Forms.FrmBase();
            return frmBase.GetFile(fileName);
        }

        #endregion

        #region Initation

        /// <summary>
        /// Init Diip to matai hashtable
        /// </summary>
        /// <returns>hashtable</returns>
        public static Hashtable InitDiipToMaTai()
        {
            Hashtable ret = new Hashtable();

            ret.Add(Model.AKhar.Kak, Model.AKhar.KakMatai);
            ret.Add(Model.AKhar.Gak, Model.AKhar.GakMatai);
            ret.Add(Model.AKhar.Ngâk, Model.AKhar.NgâkMatai);
            ret.Add(Model.AKhar.Cak, Model.AKhar.CakMatai);
            ret.Add(Model.AKhar.Tak, Model.AKhar.TakMatai);
            ret.Add(Model.AKhar.Nâk, Model.AKhar.NâkMatai);
            ret.Add(Model.AKhar.Pak, Model.AKhar.PakMatai);
            ret.Add(Model.AKhar.Mâk, Model.AKhar.TutTakaiMâk);
            ret.Add(Model.AKhar.Yak, Model.AKhar.YakMatai);
            ret.Add(Model.AKhar.Rak, Model.AKhar.RakMatai);
            ret.Add(Model.AKhar.Lak, Model.AKhar.LakMatai);
            ret.Add(Model.AKhar.Wak, Model.AKhar.WakMatai);
            ret.Add(Model.AKhar.Xak, Model.AKhar.XakMatai);
            ret.Add(Model.AKhar.Hak, Model.AKhar.PaohDaNih);

            return ret;
        }

        /// <summary>
        /// Init Kanaing hashtable
        /// </summary>
        /// <returns>hashtable</returns>
        public static Hashtable InitKanaing()
        {
            Hashtable ret = new Hashtable();

            ret.Add(",", null);
            ret.Add(".", null);
            ret.Add("!", null);
            ret.Add("?", null);
            ret.Add(";", null);
            ret.Add("(", null);
            ret.Add(")", null);
            ret.Add("-", null);

            return ret;
        }

        /// <summary>
        /// Init Karei Crih Char hashtable
        /// </summary>
        /// <returns>hashtable</returns>
        public static Hashtable InitKareiCrih(Model.Enum dataType)
        {
            Hashtable ret = new Hashtable();
            string xaaiTrans;
            string saaiTrans;
            string aiTrans;
            string ppoTrans;
            string liauaTrans;
            string auaTrans;
            string aiaTrans;
            string iaTrans;
            string aoTrans;

            xaaiTrans = "XAAI";
            saaiTrans = "SAAI";
            aiTrans = "AI";
            aiaTrans = "AIA";
            iaTrans = "IA";
            switch (dataType)
            {

                case Model.Enum.TransCamEFEO:
                    ppoTrans = "PPO";
                    liauaTrans = "LIAUA";
                    auaTrans = "AUA";
                    aoTrans = "AO";
                    break;

                case Model.Enum.TransInrasara:
                    ppoTrans = "PPO";
                    liauaTrans = "LIAWA";
                    auaTrans = "AWA";
                    aoTrans = "AU";
                    break;

                default:
                    ppoTrans = "PPÔ";
                    liauaTrans = "LIAWA";
                    auaTrans = "AWA";
                    aoTrans = "AU";
                    break;
            }

            //xaai
            List<Model.AKhar> xaai = new List<Model.AKhar>();
            xaai.Add(Model.AKhar.Xak);
            xaai.Add(Model.AKhar.DarDua);
            xaai.Add(Model.AKhar.Ai);
            ret.Add(xaaiTrans, xaai);

            //ai
            List<Model.AKhar> ai = new List<Model.AKhar>();
            ai.Add(Model.AKhar.DarDua);
            ai.Add(Model.AKhar.Ai);
            ret.Add(aiTrans, ai);

            //saai
            List<Model.AKhar> saai = new List<Model.AKhar>();
            saai.Add(Model.AKhar.SakPraong);
            saai.Add(Model.AKhar.DarDua);
            saai.Add(Model.AKhar.Ai);
            ret.Add(saaiTrans, saai);

            //po
            List<Model.AKhar> ppo = new List<Model.AKhar>();
            ppo.Add(Model.AKhar.DarSa);
            ppo.Add(Model.AKhar.PakPraong);
            ppo.Add(Model.AKhar.TakaiThek);
            ret.Add(ppoTrans, ppo);

            //liaua
            List<Model.AKhar> liaua = new List<Model.AKhar>();
            liaua.Add(Model.AKhar.Lak);
            liaua.Add(Model.AKhar.TakaiKik);
            liaua.Add(Model.AKhar.Ak);
            liaua.Add(Model.AKhar.TakaiKuak);
            liaua.Add(Model.AKhar.Balau);
            ret.Add(liauaTrans, liaua);

            //aua
            List<Model.AKhar> aua = new List<Model.AKhar>();
            aua.Add(Model.AKhar.Ak);
            aua.Add(Model.AKhar.TakaiKuak);
            aua.Add(Model.AKhar.Balau);
            ret.Add(auaTrans, aua);

            //aia
            List<Model.AKhar> aia = new List<Model.AKhar>();
            aia.Add(Model.AKhar.Ak);
            aia.Add(Model.AKhar.TakaiKiak);
            aia.Add(Model.AKhar.Balau);
            ret.Add(aiaTrans, aia);
            //ia
            ret.Add(iaTrans, aia);

            //ao
            List<Model.AKhar> ao = new List<Model.AKhar>();
            ao.Add(Model.AKhar.DarSa);
            ao.Add(Model.AKhar.Ak);
            ao.Add(Model.AKhar.TraohAw);
            ret.Add(aoTrans, ao);

            return ret;
        }

        /// <summary>
        /// Init Karei Diip To Takai HashTable
        /// </summary>
        /// <returns>hashtable</returns>
        public static Hashtable InitDiipToTaKai(Model.Enum sourceType)
        {
            Hashtable ret = new Hashtable();

            ret.Add(Model.AKhar.Ik, Model.AKhar.TakaiKiak);
            ret.Add(Model.AKhar.Lak, Model.AKhar.TakaiKlak);
            ret.Add(Model.AKhar.Rak, Model.AKhar.TakaiKrak);

            if (sourceType == Model.Enum.TransCamEFEO)
            {

                ret.Add(Model.AKhar.Uk, Model.AKhar.TakaiKuak);
            }
            else
            {

                ret.Add(Model.AKhar.Wak, Model.AKhar.TakaiKuak);
            }

            return ret;
        }

        /// <summary>
        /// Init Consonant Karei Crih Hashtable
        /// </summary>
        /// <returns>hashtable</returns>
        public static Hashtable InitConsonantKareiCrih()
        {
            Hashtable ret = new Hashtable();

            ret.Add(Model.AKhar.Mâk, Model.AKhar.Mak);
            ret.Add(Model.AKhar.Nâk, Model.AKhar.Nak);
            ret.Add(Model.AKhar.Nyâk, Model.AKhar.Nyak);
            ret.Add(Model.AKhar.Ngâk, Model.AKhar.Ngak);

            return ret;
        }

        /// <summary>
        /// Init Sap Poah Angaok Hashtable
        /// </summary>
        /// <returns>hashtable</returns>
        public static Hashtable InitSapPaohAngaok()
        {
            Hashtable ret = new Hashtable();

            ret.Add(Model.AKhar.Balau, null);
            ret.Add(Model.AKhar.BalauTapong, null);
            ret.Add(Model.AKhar.TakaiKik, null);
            ret.Add(Model.AKhar.TakaiKikTutTakaiMâkDalem, null);
            ret.Add(Model.AKhar.TraohAw, null);
            ret.Add(Model.AKhar.É, null);

            return ret;
        }

        /// <summary>
        /// Init Sap Poah Hashtable
        /// </summary>
        /// <returns>hashtable</returns>
        public static Hashtable InitSapPaoh()
        {
            Hashtable ret = new Hashtable();

            ret.Add(Model.AKhar.Ak, null);
            ret.Add(Model.AKhar.Ik, null);
            ret.Add(Model.AKhar.Uk, null);
            ret.Add(Model.AKhar.Ok, null);
            ret.Add(Model.AKhar.DarDua, null);
            ret.Add(Model.AKhar.É, null);
            ret.Add(Model.AKhar.TakaiThek, null);

            return ret;
        }

        /// <summary>
        /// Init Takai Likuk Hashtable
        /// </summary>
        /// <returns>hashtable</returns>
        public static Hashtable InitTakaiLikuk()
        {
            Hashtable ret = new Hashtable();

            ret.Add(Model.AKhar.TakaiKiak, null);
            ret.Add(Model.AKhar.TakaiKlak, null);
            ret.Add(Model.AKhar.TakaiKuak, null);
            ret.Add(Model.AKhar.TakaiKlakTakaiKuak, null);
            ret.Add(Model.AKhar.TakaiKlakTakaiKuk, null);
            ret.Add(Model.AKhar.TakaiKuk, null);
            ret.Add(Model.AKhar.TakaiThek, null);
            ret.Add(Model.AKhar.TakaiKâk, null);

            return ret;
        }

        /// <summary>
        /// Init Vowel Lang Likuk Hashtable
        /// </summary>
        /// <returns>hashtable</returns>
        public static Hashtable InitVowelLangLikuk()
        {
            Hashtable ret = new Hashtable();

            ret.Add(Model.AKhar.Ak, null);
            ret.Add(Model.AKhar.Ik, null);
            ret.Add(Model.AKhar.Uk, null);
            ret.Add(Model.AKhar.TakaiKâk, null);
            ret.Add(Model.AKhar.TakaiKik, null);
            ret.Add(Model.AKhar.TakaiKuk, null);

            return ret;
        }

        /// <summary>
        /// Init Takai Daok Likuk Hashtable
        /// </summary>
        /// <returns>hashtable</returns>
        public static Hashtable InitTakaiDaokLikuk()
        {
            Hashtable ret = new Hashtable();

            ret.Add(Model.AKhar.TakaiKik, null);
            ret.Add(Model.AKhar.TakaiKikTutTakaiMâkDalem, null);
            ret.Add(Model.AKhar.TakaiKikTutTakaiMâkLingiw, null);
            ret.Add(Model.AKhar.TakaiKikTutTakaiYak, null);
            ret.Add(Model.AKhar.TakaiThek, null);
            ret.Add(Model.AKhar.TakaiThekTutTakaiMâk, null);
            ret.Add(Model.AKhar.TakaiThekPaohNgâk, null);
            ret.Add(Model.AKhar.TakaiKuk, null);
            ret.Add(Model.AKhar.TakaiKâk, null);
            ret.Add(Model.AKhar.TakaiKiak, null);
            ret.Add(Model.AKhar.TakaiKuak, null);
            ret.Add(Model.AKhar.TakaiKlak, null);
            ret.Add(Model.AKhar.TakaiKlakTakaiKuak, null);
            ret.Add(Model.AKhar.TakaiKlakTakaiKuk, null);
            ret.Add(Model.AKhar.TraohAw, null);

            return ret;
        }

        /// <summary>
        /// Init Vowel Kawom Tuek Tuah
        /// </summary>
        /// <returns>hashtable</returns>
        public static Hashtable InitVowelKTT()
        {
            Hashtable vowel = new Hashtable();
            vowel.Add("a", "aa");
            vowel.Add("ô", "ôô");
            vowel.Add("u", "uu");
            vowel.Add("ư", "ưư");
            vowel.Add("ơ", "ơơ");
            vowel.Add("e", "ee");

            return vowel;
        }

        /// <summary>
        /// Init Sap Atah
        /// </summary>
        /// <returns>hashtable</returns>
        public static Hashtable InitSapAtah()
        {
            Hashtable sapAtah = new Hashtable();

            sapAtah.Add(Model.AKhar.Ak, Model.AKhar.Balau);
            sapAtah.Add(Model.AKhar.Ok, Model.AKhar.TraohAw);
            sapAtah.Add(Model.AKhar.Uk, Model.AKhar.Balau);
            sapAtah.Add(Model.AKhar.TakaiKâk, Model.AKhar.Balau);
            sapAtah.Add(Model.AKhar.TakaiThek, Model.AKhar.BalauTapong);
            sapAtah.Add(Model.AKhar.DarDua, Model.AKhar.Balau);

            return sapAtah;
        }

        #endregion

        #region Common

        /// <summary>
        /// Check is numberic
        /// </summary>
        /// <param name="val">value</param>
        public static bool IsNumeric(string val)
        {

            Int32 result;
            return Int32.TryParse(val, out result);
        }

        /// <summary>
        /// Add item to list
        /// </summary>
        /// <param name="list">akhar list</param>
        /// <param name="stack">akhar stack</param>
        /// <param name="addFirst">is add first flag</param>
        public static void PopStackToList(List<Model.AKhar> list, Stack<Model.AKhar> stack, bool addFirst)
        {
            while (stack.Count != 0)
            {

                int index = addFirst ? 0 : list.Count;
                list.Insert(index, stack.Pop());
            }
        }

        /// <summary>
        /// Copy Akhar List
        /// </summary>
        /// <param name="listSource">Akhar Source List</param>
        /// <returns>Akhar List</returns>
        public static List<Model.AKhar> CopyListAkhar(List<Model.AKhar> listSource)
        {

            List<Model.AKhar> ret = new List<Model.AKhar>();
            foreach (Model.AKhar saboah in listSource)
            {

                ret.Add(saboah);
            }

            return ret;
        }

        #endregion

        #region Files

        /// <summary>
        /// Write value to file
        /// </summary>
        /// <param name="value">value by String</param>
        /// <param name="fileName">file name</param>
        public static void WriteToFile(string value, string fileName)
        {
            StreamWriter sw = null;
            try
            {
                string sPathName = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
                sw = new StreamWriter(sPathName, false, Encoding.UTF8);

                sw.WriteLine(value);
                sw.Flush();
            }
            catch (Exception)
            {
            }
            finally
            {
                if (sw != null)
                {
                    sw.Dispose();
                    sw.Close();
                }
            }
        }

        /// <summary>
        /// read data from file
        /// </summary>
        /// <param name="fileName">file name</param>
        public static string ReadFile(string fileName)
        {
            string ret = string.Empty;
            StreamReader sr = null;
            try
            {
                string sPathName = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
                sr = new StreamReader(sPathName, Encoding.UTF8);

                ret = sr.ReadLine();
            }
            catch (Exception)
            {
            }
            finally
            {
                if (sr != null)
                {
                    sr.Dispose();
                    sr.Close();
                }
            }
            return ret;
        }

        #endregion
    }

}

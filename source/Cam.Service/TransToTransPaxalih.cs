using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Cam.Service
{

    /// <summary>
    /// Trans converting
    /// </summary>
    public class TransToTransPaxalih : Paxalih
    {
        #region Public Methods

        /// <summary>
        /// Convert To Rumi
        /// </summary>
        /// <param name="data">String data</param>
        /// <param name="sourceType">source Type</param>
        /// <param name="destitionType">destition Type</param>
        public String DoConvert(String data, Model.Enum sourceType, Model.Enum destitionType)
        {
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

                String convertedWord = String.Empty;
                switch (sourceType)
                {
                    case Model.Enum.TransInrasara:
                        convertedWord = this.InrasaraToCamEFEOByWord(word);
                        break;

                    case Model.Enum.TransCamEFEO:
                        convertedWord = this.CamEFEOToInrasaraByWord(word);
                        break;

                    default:
                        //case Model.Enum.TransKawomTT:
                        if (destitionType == Model.Enum.TransCamEFEO)
                        {
                            convertedWord = this.KTTToCamEFEOByWord(word);
                            break;
                        }
                        else //if (destitionType == Model.Enum.TransInrasara)
                        {
                            convertedWord = this.KTTToInrasaraByWord(word);
                            break;
                        }

                }

                converted.Add(convertedWord);
            }

            String result = String.Join(" ", converted.ToArray());

            //Trig newline character
            result = result.Replace(" " + Model.Constant.NEW_LINE + " ", Model.Constant.NEW_LINE);

            return result.ToLower();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Covert from Inrasara Trans To Cam EFEO Trans by word
        /// </summary>
        /// <param name="word">word</param>
        private String InrasaraToCamEFEOByWord(String word)
        {
            string result = word;
            Hashtable hashInra2EFEO = new Hashtable();
            hashInra2EFEO.Add("nhj", "nj");
            hashInra2EFEO.Add("bb", "mb");
            hashInra2EFEO.Add("ư", "â");
            hashInra2EFEO.Add("e", "é");
            hashInra2EFEO.Add("au", "ao");

            try
            {
                foreach (DictionaryEntry entry in hashInra2EFEO)
                {
                    if (!result.Contains("ei"))
                    {
                        result = result.Replace(entry.Key.ToString(), entry.Value.ToString());
                    }
                }
                result = result.Replace("ơ", "e");
                result = result.Replace("nh", "ny");
                
                //Akhar wak
                if (result.Contains("w"))
                {
                    int idex = result.IndexOf("w");
                    string str_check = "aưiu";
                    if ((idex != result.Length - 1) && (idex != 0) && !(str_check.Contains(result[idex - 1].ToString())))
                    {
                        result = result.Replace("w", "u");
                    }
                }

                return result;
            }
            catch (Exception ex)
            {

                Service.Log.WriteLog(ex);
                return String.Empty;
            }
        }

        /// <summary>
        /// Covert from Cam EFEO Trans To Inrasara Trans by word
        /// </summary>
        /// <param name="word">word</param>
        private String CamEFEOToInrasaraByWord(String word)
        {
            string result = word;
            Hashtable hashEFEO2Inra = new Hashtable();
            hashEFEO2Inra.Add("nj", "nhj");
            hashEFEO2Inra.Add("ny", "nh");
            hashEFEO2Inra.Add("mb", "bb");
            hashEFEO2Inra.Add("â", "ư");
            hashEFEO2Inra.Add("e", "ơ");

            try
            {
                foreach (DictionaryEntry entry in hashEFEO2Inra)
                {
                    if (!result.Contains("ei"))
                    {
                        result = result.Replace(entry.Key.ToString(), entry.Value.ToString());
                    }
                }
                
                //Akhar wak
                if (result.Contains("u"))
                {
                    int idex = result.IndexOf("u");
                    string str_check = "aie";
                    if ((idex != result.Length - 1) && (idex != 0) && (str_check.Contains(result[idex + 1].ToString())))
                    {
                        result = result.Replace("u", "w");
                    }                   
                }

                result = result.Replace("é", "e");
                result = result.Replace("ao", "au");
                
                return result;
            }
            catch (Exception ex)
            {

                Service.Log.WriteLog(ex);
                return String.Empty;
            }
        }

        /// <summary>
        /// Covert from KTTT Trans To EFEO Trans by word
        /// </summary>
        /// <param name="word">word</param>
        private String KTTToCamEFEOByWord(String word)
        {
            string result = word.ToLower();
            Hashtable hashKTTT2EFEO = new Hashtable();
            hashKTTT2EFEO.Add("nhj", "nj");
            hashKTTT2EFEO.Add("nh", "ny");
            hashKTTT2EFEO.Add("bb", "mb");
            hashKTTT2EFEO.Add("đ", "nd");
            hashKTTT2EFEO.Add("aa", "a");
            hashKTTT2EFEO.Add("ưư", "â");
            hashKTTT2EFEO.Add("uu", "u");
            hashKTTT2EFEO.Add("ơ", "e");
            hashKTTT2EFEO.Add("ơơ", "e");
            hashKTTT2EFEO.Add("ê", "é");
            hashKTTT2EFEO.Add("ô", "o");
            hashKTTT2EFEO.Add("oo", "ao");
            hashKTTT2EFEO.Add("ee", "ai");

            Hashtable hashKTTT2EFEO_plus = new Hashtable();
            hashKTTT2EFEO_plus.Add("e", "ai");
            hashKTTT2EFEO_plus.Add("o", "ao");
            hashKTTT2EFEO_plus.Add("ư", "â");

            try
            {

                if (!result.Contains("ei") && !result.Contains("oo") && !result.Contains("ee") && !result.Contains("ưư"))
                {

                    foreach (DictionaryEntry entry in hashKTTT2EFEO_plus)
                    {
                        result = result.Replace(entry.Key.ToString(), entry.Value.ToString());
                    }
                }

                foreach (DictionaryEntry entry in hashKTTT2EFEO)
                {
                    result = result.Replace(entry.Key.ToString(), entry.Value.ToString());
                }


                if (result.Contains("w"))
                {
                    int idex = result.IndexOf("w");
                    string str_check = "aâiu";
                    if ((idex != result.Length - 1) && (idex != 0) && !(str_check.Contains(result[idex - 1].ToString())))
                    {
                        result = result.Replace("w", "u");
                    }
                    //else

                }
                return result;
            }
            catch (Exception ex)
            {

                Service.Log.WriteLog(ex);
                return String.Empty;
            }
        }

        /// <summary>
        /// Covert from KTTT Trans To Inrasara Trans by word
        /// </summary>
        /// <param name="word">word</param>
        private String KTTToInrasaraByWord(String word)
        {
            string result = word.ToLower();
            Hashtable hashKTTT2Inra = new Hashtable();

            hashKTTT2Inra.Add("aa", "a");
            hashKTTT2Inra.Add("ưư", "ư");
            hashKTTT2Inra.Add("uu", "u");
            hashKTTT2Inra.Add("ơơ", "ơ");
            hashKTTT2Inra.Add("ê", "e");
            hashKTTT2Inra.Add("ô", "o");
            hashKTTT2Inra.Add("oo", "au");
            hashKTTT2Inra.Add("ee", "ai");

            Hashtable hashKTTT2Inra_plus = new Hashtable();
            hashKTTT2Inra_plus.Add("e", "ai");
            hashKTTT2Inra_plus.Add("o", "au");

            try
            {
                if (!result.Contains("ei") && !result.Contains("oo") && !result.Contains("ee"))
                {

                    foreach (DictionaryEntry entry in hashKTTT2Inra_plus)
                    {
                        result = result.Replace(entry.Key.ToString(), entry.Value.ToString());
                    }
                }

                foreach (DictionaryEntry entry in hashKTTT2Inra)
                {
                    result = result.Replace(entry.Key.ToString(), entry.Value.ToString());
                }
                return result;
            }
            catch (Exception ex)
            {

                Service.Log.WriteLog(ex);
                return String.Empty;
            }
        }

        #endregion
    }
}

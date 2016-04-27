using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Xml;

namespace Cam.Windows.Excels
{
    /// <summary>
    /// Converting excel to xml form
    /// </summary>
    public partial class FrmExcelToXml : Form
    {
        #region Events

        /// <summary>
        /// Contructor
        /// </summary>
        public FrmExcelToXml()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Convert To Keycode
        /// </summary>
        private void btnConvert_Click(object sender, EventArgs e)
        {
            Button btnCtrl = (Button)sender;

            //Open File dialog
            OpenFileDialog fDiag = new OpenFileDialog();
            fDiag.Filter = "Excel Files|*.xls;*.xlsx";
            fDiag.Title = "Select a Excel File";
            fDiag.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (fDiag.ShowDialog() == DialogResult.OK)
            {
                //Read Excel
                Excel.Application xlApp;
                Excel.Workbook xlWorkBook;
                Excel.Worksheet xlWorkSheet;

                xlApp = new Excel.ApplicationClass();
                xlWorkBook = xlApp.Workbooks.Open(fDiag.FileName, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                //Write XML
                if (btnCtrl.Name == this.btnCamToKey.Name)
                {
                    this.WriteCamToKey(xlWorkSheet.UsedRange);
                }
                else if (btnCtrl.Name == this.btnKeyToRumi.Name)
                {
                    this.WriteKeyToTrans(xlWorkSheet.UsedRange);
                }
                else if (btnCtrl.Name == this.btnRumiToKey.Name)
                {
                    this.WriteTransToKey(xlWorkSheet.UsedRange);
                }
                else
                {
                    this.WriteValidRumiChar(xlWorkSheet.UsedRange);
                }

                //Close and Release Excel app
                xlWorkBook.Close(true, null, null);
                xlApp.Quit();

                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlApp);

                MessageBox.Show("Convert data from excel format to XML format sucessed.");
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Release Object
        /// </summary>
        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Unable to release the Object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        /// <summary>
        /// Write data to XML
        /// </summary>
        /// <param name="range">Excel Range</param>
        private void WriteCamToKey(Excel.Range range)
        {
            try
            {
                //Create file name
                string filename = Model.Constant.KEY_TO_FONT_FILE;

                //Create XML
                XmlDocument doc = new XmlDocument();
                XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                doc.AppendChild(docNode);

                //Create root node
                XmlNode keysNode = doc.CreateElement("Keys");
                doc.AppendChild(keysNode);

                //Add Child node
                XmlNode keyNode;
                XmlAttribute keyAttribute;
                XmlNode keyCodeNode;
                XmlNode waQuyetNode;
                XmlNode gilaipraongNode;
                XmlNode camEFEONode;
                XmlNode kawomTuekTuahNode;
                XmlNode unicodeCamKurNode;

                for (int rCnt = 2; rCnt <= range.Rows.Count; rCnt++)
                {
                    if (String.IsNullOrEmpty((range.Cells[rCnt, 2] as Excel.Range).Text.ToString().Trim())) continue;

                    //Create key node
                    keyNode = doc.CreateElement("Key");
                    keyAttribute = doc.CreateAttribute("id");
                    keyAttribute.Value = (rCnt - 1).ToString();
                    keyNode.Attributes.Append(keyAttribute);
                    keysNode.AppendChild(keyNode);

                    //Key code
                    keyCodeNode = doc.CreateElement("KeyCode");
                    keyCodeNode.AppendChild(doc.CreateTextNode((range.Cells[rCnt, 2] as Excel.Range).Text.ToString().Trim()));
                    keyNode.AppendChild(keyCodeNode);

                    //WaQuyet Code
                    waQuyetNode = doc.CreateElement("WaQuyet");
                    waQuyetNode.AppendChild(doc.CreateTextNode((range.Cells[rCnt, 1] as Excel.Range).Text.ToString().Trim()));
                    
                    if (waQuyetNode.InnerText == "‘")
                    {
                        waQuyetNode.InnerText = "'";
                    }

                    if (waQuyetNode.InnerText == "“")
                    {
                        waQuyetNode.InnerText = "\"";
                    }
                    keyNode.AppendChild(waQuyetNode);

                    //Gilaipraong Code
                    gilaipraongNode = doc.CreateElement("GilaiPraong");
                    gilaipraongNode.AppendChild(doc.CreateTextNode((range.Cells[rCnt, 3] as Excel.Range).Text.ToString().Trim()));

                    if (gilaipraongNode.InnerText == "‘")
                    {
                        gilaipraongNode.InnerText = "'";
                    }

                    if (gilaipraongNode.InnerText == "“")
                    {
                        gilaipraongNode.InnerText = "\"";
                    }
                    keyNode.AppendChild(gilaipraongNode);

                    //Cam EFEO
                    camEFEONode = doc.CreateElement("CamEFEO");
                    camEFEONode.AppendChild(doc.CreateTextNode((range.Cells[rCnt, 4] as Excel.Range).Text.ToString().Trim()));
                    keyNode.AppendChild(camEFEONode);

                    //Kawom Tuek Tuah Katap Akhar Cam
                    kawomTuekTuahNode = doc.CreateElement("KawomTuekTuah");
                    kawomTuekTuahNode.AppendChild(doc.CreateTextNode((range.Cells[rCnt, 5] as Excel.Range).Text.ToString().Trim()));
                   //Fix bug Inâ akhar Lak
                    if (keyCodeNode.InnerText == "29")
                    {
                        kawomTuekTuahNode.InnerText = "'";
                    }
                    keyNode.AppendChild(kawomTuekTuahNode);

                    //Akhar Cam Unicode di Kur
                    unicodeCamKurNode = doc.CreateElement("UniCamKur");
                    unicodeCamKurNode.AppendChild(doc.CreateTextNode((range.Cells[rCnt, 6] as Excel.Range).Text.ToString().Trim()));
                    keyNode.AppendChild(unicodeCamKurNode);
                }

                //Save XML file
                doc.Save(filename);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Write data to XML
        /// </summary>
        /// <param name="range">Excel Range</param>
        private void WriteKeyToTrans(Excel.Range range)
        {
            try
            {
                //Create file name
                string filename = Model.Constant.KEY_TO_TRANS_FILE;

                //Create XML
                XmlDocument doc = new XmlDocument();
                XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                doc.AppendChild(docNode);

                //Create root node
                XmlNode keysNode = doc.CreateElement("Keys");
                doc.AppendChild(keysNode);

                //Add Child node
                XmlNode keyNode;
                XmlAttribute keyAttribute;
                XmlNode keyCodeNode;
                XmlNode TransRumi;
                XmlNode TransSara;
                XmlNode TransTuekTuah;

                for (int rCnt = 2; rCnt <= range.Rows.Count; rCnt++)
                {
                    if (String.IsNullOrEmpty((range.Cells[rCnt, 1] as Excel.Range).Text.ToString().Trim())) continue;

                    //Create key node
                    keyNode = doc.CreateElement("Key");
                    keyAttribute = doc.CreateAttribute("id");
                    keyAttribute.Value = (rCnt - 1).ToString();
                    keyNode.Attributes.Append(keyAttribute);
                    keysNode.AppendChild(keyNode);

                    //Key code
                    keyCodeNode = doc.CreateElement("KeyCode");
                    keyCodeNode.AppendChild(doc.CreateTextNode((range.Cells[rCnt, 1] as Excel.Range).Text.ToString().Trim()));
                    keyNode.AppendChild(keyCodeNode);

                    //Trans TuRumi
                    TransRumi = doc.CreateElement("Rumi");
                    TransRumi.AppendChild(doc.CreateTextNode((range.Cells[rCnt, 3] as Excel.Range).Text.ToString().Trim()));
                    keyNode.AppendChild(TransRumi);

                    //Trans InraSara
                    TransSara = doc.CreateElement("InraSara");
                    TransSara.AppendChild(doc.CreateTextNode((range.Cells[rCnt, 4] as Excel.Range).Text.ToString().Trim()));
                    keyNode.AppendChild(TransSara);

                    //Trans KawomTuekTuah
                    TransTuekTuah = doc.CreateElement("KawomTuekTuah");
                    TransTuekTuah.AppendChild(doc.CreateTextNode((range.Cells[rCnt, 5] as Excel.Range).Text.ToString().Trim()));
                    keyNode.AppendChild(TransTuekTuah);

                }

                //Save XML file
                doc.Save(filename);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Write data to XML
        /// </summary>
        /// <param name="range">Excel Range</param>
        private void WriteTransToKey(Excel.Range range)
        {
            try
            {
                //Create file name
                string filename = Model.Constant.TRANS_TO_KEY_FILE;

                //Create XML
                XmlDocument doc = new XmlDocument();
                XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                doc.AppendChild(docNode);

                //Create root node
                XmlNode keysNode = doc.CreateElement("Keys");
                doc.AppendChild(keysNode);

                //Add Child node
                XmlNode keyNode;
                XmlAttribute keyAttribute;

                XmlNode keycodeNode;
                XmlNode rumiNode;
                XmlNode ceiInraNode;
                XmlNode kttNode;

                // XmlNode gilaipraongNode;
                for (int rCnt = 2; rCnt <= range.Rows.Count; rCnt++)
                {
                    if (String.IsNullOrEmpty((range.Cells[rCnt, 1] as Excel.Range).Text.ToString().Trim())) continue;

                    //Create key node
                    keyNode = doc.CreateElement("Key");
                    keyAttribute = doc.CreateAttribute("id");
                    keyAttribute.Value = (rCnt - 1).ToString();
                    keyNode.Attributes.Append(keyAttribute);
                    keysNode.AppendChild(keyNode);

                    //Keycode
                    keycodeNode = doc.CreateElement("KeyCode");
                    keycodeNode.AppendChild(doc.CreateTextNode((range.Cells[rCnt, 4] as Excel.Range).Text.ToString().Trim()));
                    keyNode.AppendChild(keycodeNode);

                    //Rumi
                    rumiNode = doc.CreateElement("Rumi");
                    rumiNode.AppendChild(doc.CreateTextNode((range.Cells[rCnt, 1] as Excel.Range).Text.ToString().Trim()));
                    keyNode.AppendChild(rumiNode);

                    //Cei Inrasara
                    ceiInraNode = doc.CreateElement("Inrasara");
                    ceiInraNode.AppendChild(doc.CreateTextNode((range.Cells[rCnt, 2] as Excel.Range).Text.ToString().Trim()));
                    keyNode.AppendChild(ceiInraNode);

                    //KTT
                    kttNode = doc.CreateElement("KTT");
                    kttNode.AppendChild(doc.CreateTextNode((range.Cells[rCnt, 3] as Excel.Range).Text.ToString().Trim()));
                    keyNode.AppendChild(kttNode);
                }

                //Save XML file
                doc.Save(filename);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Write data to XML
        /// </summary>
        /// <param name="range">Excel Range</param>
        private void WriteValidRumiChar(Excel.Range range)
        {
            try
            {
                //Create file name
                string filename = Model.Constant.VALID_TRANS_CHAR_FILE;

                //Create XML
                XmlDocument doc = new XmlDocument();
                XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                doc.AppendChild(docNode);

                //Create root node
                XmlNode keysNode = doc.CreateElement("Keys");
                doc.AppendChild(keysNode);

                //Add Child node
                XmlNode keyNode;
                XmlAttribute keyAttribute;

                XmlNode rumiNode;
                XmlNode inraNode;
                XmlNode kttNode;
                for (int rCnt = 2; rCnt <= range.Rows.Count; rCnt++)
                {
                    if (String.IsNullOrEmpty((range.Cells[rCnt, 3] as Excel.Range).Text.ToString().Trim())) continue;

                    //Create key node
                    keyNode = doc.CreateElement("Key");
                    keyAttribute = doc.CreateAttribute("id");
                    keyAttribute.Value = (rCnt - 1).ToString();
                    keyNode.Attributes.Append(keyAttribute);
                    keysNode.AppendChild(keyNode);

                    //Rumi
                    rumiNode = doc.CreateElement("Rumi");
                    rumiNode.AppendChild(doc.CreateTextNode((range.Cells[rCnt, 1] as Excel.Range).Text.ToString().Trim()));
                    keyNode.AppendChild(rumiNode);

                    //Inrasara
                    inraNode = doc.CreateElement("Inrasara");
                    inraNode.AppendChild(doc.CreateTextNode((range.Cells[rCnt, 2] as Excel.Range).Text.ToString().Trim()));
                    keyNode.AppendChild(inraNode);

                    //Kawom Tuek Tuah
                    kttNode = doc.CreateElement("KTT");
                    kttNode.AppendChild(doc.CreateTextNode((range.Cells[rCnt, 3] as Excel.Range).Text.ToString().Trim()));
                    keyNode.AppendChild(kttNode);
                }

                //Save XML file
                doc.Save(filename);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion
    }
}
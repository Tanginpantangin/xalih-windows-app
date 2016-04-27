using System;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;

namespace Cam.Service
{
    /// <summary>
    /// Log Utility
    /// </summary>
    public class Log
    {

        #region Variables

        private static string _dataConvert = string.Empty;
        private static Model.Enum _sourceType = Model.Enum.FontYapata;
        private static Model.Enum _desinationType = Model.Enum.FontYapata;

        #endregion

        #region Methods

        /// <summary>
        /// Set Log Data
        /// </summary>
        /// <param name="dataToConvert">data to convert</param>
        /// <param name="sourceType">source Type</param>
        /// <param name="destinationType">destination type</param>
        public static void SetLogData(string dataToConvert, Model.Enum sourceType, Model.Enum destinationType)
        {
            _dataConvert = dataToConvert;
            _sourceType = sourceType;
            _desinationType = destinationType;
        }

        /// <summary>
        /// Clear log data
        /// </summary>
        public static void ClearLogData()
        {
            _dataConvert = string.Empty;
            _sourceType = Model.Enum.FontYapata;
            _desinationType = Model.Enum.FontYapata;
        }

        /// <summary>
        /// Write log
        /// </summary>
        /// <param name="e">UnhandledExceptionEventArgs</param>
        public static void WriteLog(UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            WriteLog(ex);
        }

        /// <summary>
        /// Write log
        /// </summary>
        /// <param name="e">ThreadExceptionEventArgs</param>
        public static void WriteLog(ThreadExceptionEventArgs e)
        {
            WriteLog(e.Exception);
        }

        /// <summary>
        /// Write log
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <param name="word">word String</param>
        public static void WriteLog(Exception ex)
        {

            String errorMsg = "-------------------------------------------------------------------------" + Model.Constant.NEW_LINE;

            if (!String.IsNullOrEmpty(_dataConvert))
            {
                errorMsg += "Convert Type: " + _sourceType.ToString() + " -> " + _desinationType.ToString() + Model.Constant.NEW_LINE + Model.Constant.NEW_LINE;
                errorMsg += "Data to Convert:" + Model.Constant.NEW_LINE + _dataConvert + Model.Constant.NEW_LINE;
            }

            errorMsg += DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + Model.Constant.NEW_LINE;
            errorMsg += "An application error occurred. Please contact the adminstrator " +
                "with the following information:" + Model.Constant.NEW_LINE + Model.Constant.NEW_LINE;

            // Since we can't prevent the app from terminating, log this to the event log. 
            if (!EventLog.SourceExists("ThreadException"))
            {
                EventLog.CreateEventSource("ThreadException", "Application");
            }

            errorMsg += "Message: " + Model.Constant.NEW_LINE + ex.Message + Model.Constant.NEW_LINE;

            if (ex.InnerException != null)
            {

                errorMsg += "InnerException: " + Model.Constant.NEW_LINE + ex.InnerException.Message + Model.Constant.NEW_LINE;
            }

            errorMsg += "Stack Trace: " + Model.Constant.NEW_LINE + ex.StackTrace + Model.Constant.NEW_LINE + Model.Constant.NEW_LINE;

            if (Model.Info.DevelopMode)
            {
                MessageBox.Show(errorMsg);
                Application.Exit();
                return;
            }

            WriteToFile(errorMsg);
        }

        /// <summary>
        /// Write Log to file
        /// </summary>
        /// <param name="Message">Message by String</param>
        private static void WriteToFile(string Message)
        {
            StreamWriter sw = null;

            try
            {
                string sPathName = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Model.Constant.LOG_FILE);
                sw = new StreamWriter(sPathName, true, Encoding.UTF8);

                sw.WriteLine(Message);
                sw.Flush();
            }
            catch (Exception ex)
            {
                WriteToFile(ex.ToString());
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

        #endregion

    }
}

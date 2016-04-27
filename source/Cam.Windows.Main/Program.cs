using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

namespace Cam.Windows.Main
{
    /// <summary>
    /// Application
    /// </summary>
    static class Program
    {
            /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
        }

        /// <summary>
        /// ThreadException
        /// </summary>
        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {

            try
            {
                Service.Log.WriteLog(e);
            }
            finally
            {
                Application.Exit();
            }
        }

        /// <summary>
        /// UnhandledException
        /// </summary>
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {

            try
            {
                Service.Log.WriteLog(e);
            }
            finally
            {
                Application.Exit();
            }
        }
    }
}

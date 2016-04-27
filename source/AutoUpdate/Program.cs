using System;
using System.Windows.Forms;
using System.Threading;


namespace AutoUpdate
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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmAutoUpdate());
        }

        /// <summary>
        /// ThreadException
        /// </summary>
        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// UnhandledException
        /// </summary>
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Application.Exit();
        }
    }
}

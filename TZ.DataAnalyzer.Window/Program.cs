using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TZ.DataAnalyzer.Window
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var value = Environment.GetEnvironmentVariable("DOTNETBACKEND_PORT");
            // If necessary, create it.
            if (value == null)
            {
                Environment.SetEnvironmentVariable("DOTNETBACKEND_PORT", "5567");
                                               // Now retrieve it.
                value = Environment.GetEnvironmentVariable("DOTNETBACKEND_PORT");
            }
            Environment.SetEnvironmentVariable("DOTNET_ASSEMBLY_SEARCH_PATHS", @"C:\WorkingFolder-Custom\TZ.BigDataScript\TZ.DataAnalyzer.Window\bin\Debug");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        
        }
    }
}

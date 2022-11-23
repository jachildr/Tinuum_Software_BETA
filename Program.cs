using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tinuum_Software_BETA.Icon_Masters;
using Tinuum_Software_BETA.Popups;
using Tinuum_Software_BETA.Popups.Expense;
using Tinuum_Software_BETA.Popups.Roll;

namespace Tinuum_Software_BETA
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjkwMTE2QDMxMzgyZTMyMmUzMFNzeCttcnBsMkR3NHc5TkZwd2pLZm9WVXE2WkF6ODFUQTNneDNTcDVyc3M9");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}

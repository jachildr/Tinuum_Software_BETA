using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Tinuum_Software_BETA.Popups.Expense
{
    public partial class FormSelector_PDPM : Tinuum_Software_BETA.Popups.Expense.FormSelector_Payor
    {
        public FormSelector_PDPM()
        {
            InitializeComponent();
            tbl_Input_Prefix = "dtbExpenseSelector_PDPM_Input";
            tbl_Output_Prefix = "dtbExpenseSelector_PDPM_Output";
            tbl_Main = "dtbExpenseSelector_PDPM_Main";
            tbl_Active = "dtbExpenseConfigurePDPM";
        }
        public override void Delegate()
        {
            SQLQueries.tblExpensePDPMCreate();
        }
    }
}

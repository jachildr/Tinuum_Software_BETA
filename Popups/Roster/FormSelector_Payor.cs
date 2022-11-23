using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Tinuum_Software_BETA.Popups.Roster
{
    public partial class FormSelector_Payor : Tinuum_Software_BETA.Popups.Expense.FormSelector_PDPM
    {
        public FormSelector_Payor()
        {
            InitializeComponent();
            tbl_Input_Prefix = "dtbRosterSelector_Payor_Input";
            tbl_Output_Prefix = "dtbRosterSelector_Payor_Output";
            tbl_Main = "dtbExpenseSelector_Payor_Main";
            tbl_Active = "dtbRosterConfigurePayor";
        }
        public override void Delegate()
        {
            SQLQueries.tblRosterPayorCreate();
        }
    }
}

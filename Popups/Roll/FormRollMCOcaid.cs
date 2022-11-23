using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Tinuum_Software_BETA.Popups.Roll
{
    public partial class FormRollMCOcaid : Tinuum_Software_BETA.Popups.Roll.FormRollMedicaid
    {
        public FormRollMCOcaid()
        {
            InitializeComponent();
            tbl_Prefix = "dtbRollMCOcaid";
            tbl_dtlPrefix = "dtbRollDetailDynamic_MCOcaidRate";
            tbl_Active = "dtbRollConfigureMCOcaidRate";
            tbl_DynPrefix = "dtbRollDynamic_MCOcaidRate";
            tbl_ValPrefix = "dtbRoll_MCOcaidRate";
        }
        public override void Delegate()
        {
            SQLQueries.tblRollMCOcaidRateCreate();
        }
    }
}

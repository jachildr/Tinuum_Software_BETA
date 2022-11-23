using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Tinuum_Software_BETA.Popups.Roll
{
    public partial class FormRollMCOcare : Tinuum_Software_BETA.Popups.Roll.FormRollMedicaid
    {
        public FormRollMCOcare()
        {
            InitializeComponent();
            tbl_Prefix = "dtbRollMCOcare";
            tbl_dtlPrefix = "dtbRollDetailDynamic_MCOcareRate";
            tbl_Active = "dtbRollConfigureMCOcareRate";
            tbl_DynPrefix = "dtbRollDynamic_MCOcareRate";
            tbl_ValPrefix = "dtbRoll_MCOcareRate";
        }
        public override void Delegate()
        {
            SQLQueries.tblRollMCOcareRateCreate();
        }
    }
}

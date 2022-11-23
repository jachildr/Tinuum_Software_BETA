using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Tinuum_Software_BETA.Popups.Roll
{
    public partial class FormRollOther : Tinuum_Software_BETA.Popups.Roll.FormRollMedicaid
    {
        public FormRollOther()
        {
            InitializeComponent();
            tbl_Prefix = "dtbRollOther";
            tbl_dtlPrefix = "dtbRollDetailDynamic_OtherRate";
            tbl_Active = "dtbRollConfigureOtherRate";
            tbl_DynPrefix = "dtbRollDynamic_OtherRate";
            tbl_ValPrefix = "dtbRoll_OtherRate";
        }

        public override void Delegate()
        {
            SQLQueries.tblRollOtherRateCreate();
        }
    }
}

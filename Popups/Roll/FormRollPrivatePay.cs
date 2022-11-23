using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Tinuum_Software_BETA.Popups.Roll
{
    public partial class FormRollPrivatePay : Tinuum_Software_BETA.Popups.Roll.FormRollMedicaid
    {
        public FormRollPrivatePay()
        {
            InitializeComponent();
            tbl_Prefix = "dtbRollPrivatePay";
            tbl_dtlPrefix = "dtbRollDetailDynamic_PrivatePayRate";
            tbl_Active = "dtbRollConfigurePrivatePayRate";
            tbl_DynPrefix = "dtbRollDynamic_PrivatePayRate";
            tbl_ValPrefix = "dtbRoll_PrivatePayRate";
        }

        public override void Delegate()
        {
            SQLQueries.tblRollPrivatePayRateCreate();
        }
    }
}

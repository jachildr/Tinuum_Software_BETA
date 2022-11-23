using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Tinuum_Software_BETA.Popups.Roll
{
    public partial class FormRollVets : Tinuum_Software_BETA.Popups.Roll.FormRollMedicaid
    {
        public FormRollVets()
        {
            InitializeComponent();
            tbl_Prefix = "dtbRollVets";
            tbl_dtlPrefix = "dtbRollDetailDynamic_VetsRate";
            tbl_Active = "dtbRollConfigureVetsRate";
            tbl_DynPrefix = "dtbRollDynamic_VetsRate";
            tbl_ValPrefix = "dtbRoll_VetsRate";
        }

        public override void Delegate()
        {
            SQLQueries.tblRollVetsRateCreate();
        }
    }
}

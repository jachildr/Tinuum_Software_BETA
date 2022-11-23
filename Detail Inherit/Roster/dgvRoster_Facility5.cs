using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinuum_Software_BETA.Detail_Inherit.Roster
{
    class dgvRoster_Facility5 : Detail_Inherit.Roster.dgvRoster_Facility1
    {
        public dgvRoster_Facility5()
        {
            tbl_Name = "dtbRosterVerse5";
            tbl_Dtl_Acuity = "dtbRosterDetail_Acuity5";
            tbl_Dtl_Assessment = "dtbRosterDetail_Assessment5";
            tbl_Dtl_Payor = "dtbRosterDetail_Payor5";
            tbl_Dtl_Transition = "dtbRosterDetail_Transition5";
            tbl_Dtl_Trans_RE = "dtbRosterDetail_Trans_RE5";
            gridNum = 5;
        }
    }
}

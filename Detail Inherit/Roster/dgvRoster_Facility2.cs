using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinuum_Software_BETA.Detail_Inherit.Roster
{
    class dgvRoster_Facility2 : Detail_Inherit.Roster.dgvRoster_Facility1
    {
        public dgvRoster_Facility2()
        {
            tbl_Name = "dtbRosterVerse2";
            tbl_Dtl_Acuity = "dtbRosterDetail_Acuity2";
            tbl_Dtl_Assessment = "dtbRosterDetail_Assessment2";
            tbl_Dtl_Payor = "dtbRosterDetail_Payor2";
            tbl_Dtl_Transition = "dtbRosterDetail_Transition2";
            tbl_Dtl_Trans_RE = "dtbRosterDetail_Trans_RE2";
            gridNum = 2;
        }
    }
}

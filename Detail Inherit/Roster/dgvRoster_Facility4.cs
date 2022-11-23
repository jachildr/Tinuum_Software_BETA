using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinuum_Software_BETA.Detail_Inherit.Roster
{
    class dgvRoster_Facility4 : Detail_Inherit.Roster.dgvRoster_Facility1
    {
        public dgvRoster_Facility4()
        {
            tbl_Name = "dtbRosterVerse4";
            tbl_Dtl_Acuity = "dtbRosterDetail_Acuity4";
            tbl_Dtl_Assessment = "dtbRosterDetail_Assessment4";
            tbl_Dtl_Payor = "dtbRosterDetail_Payor4";
            tbl_Dtl_Transition = "dtbRosterDetail_Transition4";
            tbl_Dtl_Trans_RE = "dtbRosterDetail_Trans_RE4";
            gridNum = 4;
        }
    }
}

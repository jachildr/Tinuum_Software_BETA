using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinuum_Software_BETA.Detail_Inherit.Roster
{
    class dgvRoster_Facility3 : Detail_Inherit.Roster.dgvRoster_Facility1
    {
        public dgvRoster_Facility3()
        {
            tbl_Name = "dtbRosterVerse3";
            tbl_Dtl_Acuity = "dtbRosterDetail_Acuity3";
            tbl_Dtl_Assessment = "dtbRosterDetail_Assessment3";
            tbl_Dtl_Payor = "dtbRosterDetail_Payor3";
            tbl_Dtl_Transition = "dtbRosterDetail_Transition3";
            tbl_Dtl_Trans_RE = "dtbRosterDetail_Trans_RE3";
            gridNum = 3;
        }
    }
}

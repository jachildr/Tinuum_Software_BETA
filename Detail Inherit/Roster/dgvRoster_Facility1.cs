using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tinuum_Software_BETA.Icon_Masters;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualBasic;
using Tinuum_Software_BETA.Popups.Roster;
using Syncfusion.Windows.Forms.Tools;

namespace Tinuum_Software_BETA.Detail_Inherit.Roster
{
    class dgvRoster_Facility1 : Detail_Inherit.Expense.dgvExpense_OPEX
    {
        protected string tbl_Dtl_Acuity = "dtbRosterDetail_Acuity";
        protected string tbl_Dtl_Assessment = "dtbRosterDetail_Assessment";
        protected new string tbl_Dtl_Payor = "dtbRosterDetail_Payor";
        protected string tbl_Dtl_Transition = "dtbRosterDetail_Transition";
        protected string tbl_Dtl_Trans_RE = "dtbRosterDetail_Trans_RE";
        protected string tbl_Inventory = "dtbInventoryVerse";
        protected string tbl_MLA = "dtbRollVerse";
        protected string tbl_MLA_RE = "dtbRollVerse_Discharge";
        protected string tbl_Payors = "dtbExpenseSelector_Payor_Main";
        protected string tbl_PT = "dtbRoster_PT";
        protected string tbl_OT = "dtbRoster_OT";
        protected string tbl_SLP = "dtbRoster_SLP";
        protected string tbl_NTA = "dtbRoster_NTA";
        protected string tbl_NRS = "dtbRoster_NRS";
        protected List<int> bedType = new List<int>();
        protected List<int> valMember = new List<int>();
        protected List<int> valMember_RE = new List<int>();
        protected List<string> Payors = new List<string>();
        protected List<string> PT = new List<string>();
        protected List<string> OT = new List<string>();
        protected List<string> SLP = new List<string>();
        protected List<string> NTA = new List<string>();
        protected List<string> NRS = new List<string>();
        protected List<string> MLA = new List<string>();
        protected List<string> MLA_RE = new List<string>();
        protected SQLControl SQL_Payors = new SQLControl();
        protected SQLControl SQL_PT = new SQLControl();
        protected SQLControl SQL_OT = new SQLControl();
        protected SQLControl SQL_SLP = new SQLControl();
        protected SQLControl SQL_NTA = new SQLControl();
        protected SQLControl SQL_NRS = new SQLControl();
        protected int gridNum = 1;
        protected int Existing;
        protected int record;
        protected int ttlBeds;
        protected int ttlUnits;
        protected ComboBox cmbo = new ComboBox();
        protected ComboBox cmboRE = new ComboBox();
        protected DialogResult prompt;
        public static int escape = 0;
        public static int _escape
        {
            get
            {
                return escape;
            }
        }

        public dgvRoster_Facility1()
        {
            int i;
            // SQL RECORDS
            SQL_Payors.ExecQuery("SELECT * FROM " + tbl_Payors + ";");
            SQL_PT.ExecQuery("SELECT * FROM " + tbl_PT + ";");
            SQL_OT.ExecQuery("SELECT * FROM " + tbl_OT + ";");
            SQL_SLP.ExecQuery("SELECT * FROM " + tbl_SLP + ";");
            SQL_NTA.ExecQuery("SELECT * FROM " + tbl_NTA + ";");
            SQL_NRS.ExecQuery("SELECT * FROM " + tbl_NRS + ";");

            
            // GET RECORDS FROM MLA TABLE
            SQL.ExecQuery("SELECT * FROM " + tbl_MLA + ";");
            for (i = 0; i <= SQL.RecordCount - 1; i++)
            {
                MLA.Add(Convert.ToString(SQL.DBDT.Rows[i][2]));
            }
            for (i = 0; i <= SQL.RecordCount - 1; i++)
            {
                valMember.Add(Convert.ToInt32(SQL.DBDT.Rows[i][0]));
            }

            cmbo.DataSource = SQL.DBDT;
            cmbo.DisplayMember = "Transition Label";
            cmbo.ValueMember = "ID_Num";

            SQL.ExecQuery("SELECT * FROM " + tbl_MLA_RE + ";");
            for (i = 0; i <= SQL.RecordCount - 1; i++)
            {
                MLA_RE.Add(Convert.ToString(SQL.DBDT.Rows[i][2]));
            }
            for (i = 0; i <= SQL.RecordCount - 1; i++)
            {
                valMember_RE.Add(Convert.ToInt32(SQL.DBDT.Rows[i][0]));
            }
            cmboRE.DataSource = SQL.DBDT;
            cmboRE.DisplayMember = "Transition Label";
            cmboRE.ValueMember = "ID_Num";

            // MAIN TABLE
            tbl_Name = "dtbRosterVerse";
            {
                // ADD SPECS FOR COMBOBOX4
                Payors.Add("");
                Payors.Add("1");
                Payors.Add("2");
                Payors.Add("3");
                Payors.Add("4");
                Payors.Add("5");
                Payors.Add("6");
                Payors.Add("7");

                // ADD SPECS FOR COMBOBOX5
                PT.Add("");
                PT.Add("1");
                PT.Add("2");
                PT.Add("3");
                PT.Add("4");
                PT.Add("5");
                PT.Add("6");
                PT.Add("7");
                PT.Add("8");
                PT.Add("9");
                PT.Add("10");
                PT.Add("11");
                PT.Add("12");
                PT.Add("13");
                PT.Add("14");
                PT.Add("15");
                PT.Add("16");

                // ADD SPECS FOR COMBOBOX6
                OT.Add("");
                OT.Add("17");
                OT.Add("18");
                OT.Add("19");
                OT.Add("20");
                OT.Add("21");
                OT.Add("22");
                OT.Add("23");
                OT.Add("24");
                OT.Add("25");
                OT.Add("26");
                OT.Add("27");
                OT.Add("28");
                OT.Add("29");
                OT.Add("30");
                OT.Add("31");
                OT.Add("32");

                // ADD SPECS FOR COMBOBOX7
                SLP.Add("");
                SLP.Add("33");
                SLP.Add("34");
                SLP.Add("35");
                SLP.Add("36");
                SLP.Add("37");
                SLP.Add("38");
                SLP.Add("39");
                SLP.Add("40");
                SLP.Add("41");
                SLP.Add("42");
                SLP.Add("43");
                SLP.Add("44");

                // ADD SPECS FOR COMBOBOX8
                NTA.Add("");
                NTA.Add("45");
                NTA.Add("46");
                NTA.Add("47");
                NTA.Add("48");
                NTA.Add("49");
                NTA.Add("50");

                // ADD SPECS FOR COMBOBOX9
                NRS.Add("");
                NRS.Add("51");
                NRS.Add("52");
                NRS.Add("53");
                NRS.Add("54");
                NRS.Add("55");
                NRS.Add("56");
                NRS.Add("57");
                NRS.Add("58");
                NRS.Add("59");
                NRS.Add("60");
                NRS.Add("61");
                NRS.Add("62");
                NRS.Add("63");
                NRS.Add("64");
                NRS.Add("65");
                NRS.Add("66");
                NRS.Add("67");
                NRS.Add("68");
                NRS.Add("69");
                NRS.Add("70");
                NRS.Add("71");
                NRS.Add("72");
                NRS.Add("73");
                NRS.Add("74");
                NRS.Add("75");
            }
        }

        public void Existing_Roster(DataGridView dataGridView1)
        {
            int i;
            int j;
            int z;
            int bedStart = 10;
            int bedShift = 4;
            int num;
            int sub;
            int count;
            int exist;

            SQL.ExecQuery("SELECT * FROM " + tbl_Inventory + ";");
            record = SQL.RecordCount;

            if (record < gridNum) return;

            ttlUnits = Convert.ToInt32(SQL.DBDT.Rows[gridNum - 1][8]);
            ttlBeds = Convert.ToInt32(SQL.DBDT.Rows[gridNum - 1][9]);

            for (i = bedStart; i <= bedStart + bedShift - 1; i++)
            {
                bedType.Add(Convert.ToInt32(SQL.DBDT.Rows[gridNum - 1][i]));
            }

            for (i = 1; i <= Convert.ToInt32(bedType.Count); i++)
            {
                for (j = 0; j <= Convert.ToInt32(bedType[i - 1] - 1); j++)
                {
                    // *** INSERT PARENT ***
                    // INSERT NEWEST VERSE COLUMN
                    SQL.ExecQuery("INSERT INTO " + tbl_Name + " DEFAULT VALUES;");

                    // GET UPDATED ROW COUNT
                    SQL_Verse.ExecQuery("SELECT * FROM " + tbl_Name + ";");
                    count = SQL_Verse.RecordCount - 1;
                    num = Convert.ToInt32(SQL_Verse.DBDT.Rows[count][0].ToString());
                    exist = 1;

                    // INSERT IDENTITY NUM INTO SIPPORTING DATABASES
                    string cmdInsert1 = "INSERT INTO " + tbl_Dtl_Acuity + " (ID_Num, Collection_Num) VALUES (" + num + ", " + num + ");";
                    string cmdInsert2 = "INSERT INTO " + tbl_Dtl_Assessment + " (ID_Num, Collection_Num) VALUES (" + num + ", " + num + ");";
                    string cmdInsert3 = "INSERT INTO " + tbl_Dtl_Payor + " (ID_Num, Collection_Num) VALUES (" + num + ", " + num + ");";
                    string cmdInsert4 = "INSERT INTO " + tbl_Dtl_Transition + " (ID_Num, Collection_Num) VALUES (" + num + ", " + num + ");";
                    string cmdInsert5 = "INSERT INTO " + tbl_Dtl_Trans_RE + " (ID_Num, Collection_Num) VALUES (" + num + ", " + num + ");";

                    SQL_Verse.ExecQuery(cmdInsert1);
                    SQL_Verse.ExecQuery(cmdInsert2);
                    SQL_Verse.ExecQuery(cmdInsert3);
                    SQL_Verse.ExecQuery(cmdInsert4);
                    SQL_Verse.ExecQuery(cmdInsert5);

                    // UPDATE VERSE COLLECTION ID
                    SQL_Verse.AddParam("@PrimKey", num);
                    SQL_Verse.AddParam("@Num", num);
                    SQL_Verse.AddParam("@Exist", exist);
                    string cmdUpdate = "UPDATE " + tbl_Name + " SET Collection_Num=@Num, Existing_Num=@Exist WHERE ID_Num=@PrimKey;";
                    SQL_Verse.ExecQuery(cmdUpdate);

                    // *** INSERT SUB ***

                    for (z = 0; z <= i - 1; z++)
                    {
                        switch (i)
                        {
                            case 1:
                                continue;
                            default:
                                {
                                    // INSERT NEWEST VERSE COLUMN
                                    SQL.ExecQuery("INSERT INTO " + tbl_Name + " DEFAULT VALUES;");

                                    // GET UPDATED ROW COUNT
                                    SQL_Verse.ExecQuery("SELECT * FROM " + tbl_Name + ";");
                                    count = SQL_Verse.RecordCount - 1;
                                    num = Convert.ToInt32(SQL_Verse.DBDT.Rows[count][0].ToString());
                                    sub = Convert.ToInt32(SQL_Verse.DBDT.Rows[count - 1][1].ToString()); // CHECK

                                    // INSERT IDENTITY NUM INTO SIPPORTING DATABASES
                                    string Insert1 = "INSERT INTO " + tbl_Dtl_Acuity + " (ID_Num, Collection_Num) VALUES (" + num + ", " + sub + ");";
                                    string Insert2 = "INSERT INTO " + tbl_Dtl_Assessment + " (ID_Num, Collection_Num) VALUES (" + num + ", " + sub + ");";
                                    string Insert3 = "INSERT INTO " + tbl_Dtl_Payor + " (ID_Num, Collection_Num) VALUES (" + num + ", " + sub + ");";
                                    string Insert4 = "INSERT INTO " + tbl_Dtl_Transition + " (ID_Num, Collection_Num) VALUES (" + num + ", " + sub + ");";
                                    string Insert5 = "INSERT INTO " + tbl_Dtl_Trans_RE + " (ID_Num, Collection_Num) VALUES (" + num + ", " + sub + ");";

                                    SQL_Verse.ExecQuery(Insert1);
                                    SQL_Verse.ExecQuery(Insert2);
                                    SQL_Verse.ExecQuery(Insert3);
                                    SQL_Verse.ExecQuery(Insert4);
                                    SQL_Verse.ExecQuery(Insert5);

                                    // UPDATE VERSE COLLECTION ID
                                    SQL_Verse.AddParam("@PrimKey", num);
                                    SQL_Verse.AddParam("@Num", sub);
                                    SQL_Verse.AddParam("@Exist", exist);
                                    string Update = "UPDATE " + tbl_Name + " SET Collection_Num=@Num, Existing_Num=@Exist WHERE ID_Num=@PrimKey;";
                                    SQL_Verse.ExecQuery(Update);
                                }
                                break;
                        }
                        
                    }  
                }
            }
            Existing = 1;
        }

        public void Existing_Fill(DataGridView dataGridView1)
        {
            int sqft;
            int avgBed_SF;
            int avgUnit_SF;
            string tbl_SF = "dtbInventoryDetail_SF";
            
            int i;
            int j;
            int z;
            int bedCount = 0;
            int unitCount = 0;
            Random rand = new Random();

            if (Existing == 0) return;


            // GET BED AND UNIT TOTALS FROM INVENTORY DB
            SQL.ExecQuery("SELECT * FROM " + tbl_SF + ";");
            sqft = Convert.ToInt32(SQL.DBDT.Rows[gridNum - 1][2]);
            avgBed_SF = sqft / ttlBeds;
            avgUnit_SF = sqft / ttlUnits;

            // GET DATE OPENED
            SQL.ExecQuery("SELECT * FROM " + tbl_Inventory + ";");

            DateTime date1 = Convert.ToDateTime(SQL.DBDT.Rows[gridNum - 1][4]);
            DateTime date2 = DateTime.Now;
            int result = DateTime.Compare(date1, date2);

            // IF DATE OPENED IS LATER THAN TODAY THEN SET STATUS TO VACANT
            if (result > 0)
            {
                for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                {
                    Parent = 0;
                    if (Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value) == Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value))
                    {
                        try
                        {
                            for (z = i + 1; z <= dataGridView1.RowCount - 1; z++)
                            {
                                if (Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value) == Convert.ToInt32(dataGridView1.Rows[z].Cells[1].Value))
                                {
                                    Parent = 1;
                                }
                            }
                        }
                        catch
                        {
                            Parent = 0;
                        }
                    }
                    for (j = 0; j <= dataGridView1.ColumnCount - 1; j++)
                    {
                        switch (j)
                        {
                            case 4:
                                {
                                    if (Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value) == Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value))
                                    {
                                        unitCount += 1;
                                        dataGridView1.Rows[i].Cells[j].Value = "Unit " + unitCount;
                                    }
                                    else
                                    {
                                        dataGridView1.Rows[i].Cells[j].Value = "Unit " + unitCount;
                                    }
                                }
                                break;
                            case 5:
                                {
                                    if (Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value) != Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value))
                                    {
                                        bedCount += 1;
                                        dataGridView1.Rows[i].Cells[j].Value = "U-" + unitCount + " : Bed " + bedCount;
                                    }
                                    else
                                    {
                                        int beds_per_room = 0;
                                        for (z = 0; z <= dataGridView1.RowCount - 1; z++)
                                        {
                                            if (Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value) == Convert.ToInt32(dataGridView1.Rows[z].Cells[1].Value))
                                            {
                                                beds_per_room += 1;
                                            }
                                        }
                                        if (beds_per_room == 1)
                                        {
                                            bedCount += 1;
                                            dataGridView1.Rows[i].Cells[j].Value = "U-" + unitCount + " : Bed " + bedCount;
                                        }
                                    }
                                }
                                break;
                            case 6:
                                {
                                    if (Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value) != Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value))
                                    {
                                        dataGridView1.Rows[i].Cells[j].Value = avgBed_SF;
                                    }
                                    else
                                    {
                                        int beds_per_room = 0;
                                        for (z = 0; z <= dataGridView1.RowCount - 1; z++)
                                        {
                                            if (Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value) == Convert.ToInt32(dataGridView1.Rows[z].Cells[1].Value))
                                            {
                                                beds_per_room += 1;
                                            }
                                        }

                                        switch (beds_per_room)
                                        {
                                            case 1:
                                                {
                                                    dataGridView1.Rows[i].Cells[j].Value = avgBed_SF * 1;
                                                }
                                                break;
                                            case 2:
                                                {
                                                    dataGridView1.Rows[i].Cells[j].Value = avgBed_SF * 1;
                                                }
                                                break;
                                            case 3:
                                                {
                                                    dataGridView1.Rows[i].Cells[j].Value = avgBed_SF * 2;
                                                }
                                                break;
                                            case 4:
                                                {
                                                    dataGridView1.Rows[i].Cells[j].Value = avgBed_SF * 3;
                                                }
                                                break;
                                            case 5:
                                                {
                                                    dataGridView1.Rows[i].Cells[j].Value = avgBed_SF * 4;
                                                }
                                                break;
                                        }
                                    }
                                }
                                break;
                            case 7:
                                {
                                    if (Parent > 0) continue;
                                    int beds_per_room = 0;
                                    for (z = 0; z <= dataGridView1.RowCount - 1; z++)
                                    {
                                        if (Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value) == Convert.ToInt32(dataGridView1.Rows[z].Cells[1].Value))
                                        {
                                            beds_per_room += 1;
                                        }
                                    }

                                    switch (beds_per_room)
                                    {
                                        case 1:
                                            {
                                                dataGridView1.Rows[i].Cells[j].Value = "1";
                                            }
                                            break;
                                        case 2:
                                            {
                                                dataGridView1.Rows[i].Cells[j].Value = "1";
                                            }
                                            break;
                                        case 3:
                                            {
                                                dataGridView1.Rows[i].Cells[j].Value = "2";
                                            }
                                            break;
                                        case 4:
                                            {
                                                if (dataGridView1.Rows[i - 1].Cells[j].Value == null || dataGridView1.Rows[i - 1].Cells[j].Value.ToString() == "")
                                                {
                                                    dataGridView1.Rows[i].Cells[j].Value = "1";
                                                }
                                                else
                                                {
                                                    dataGridView1.Rows[i].Cells[j].Value = "2";
                                                }
                                            }
                                            break;
                                        case 5:
                                            {
                                                dataGridView1.Rows[i].Cells[j].Value = "2";
                                            }
                                            break;
                                    }
                                }
                                break;
                            case 8:
                                {
                                    if (Parent > 0) continue;
                                    int beds_per_room = 0;
                                    for (z = 0; z <= dataGridView1.RowCount - 1; z++)
                                    {
                                        if (Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value) == Convert.ToInt32(dataGridView1.Rows[z].Cells[1].Value))
                                        {
                                            beds_per_room += 1;
                                        }
                                    }

                                    switch (beds_per_room)
                                    {
                                        case 1:
                                            {
                                                dataGridView1.Rows[i].Cells[j].Value = "Private";
                                            }
                                            break;
                                        case 2:
                                            {
                                                dataGridView1.Rows[i].Cells[j].Value = "Private";
                                            }
                                            break;
                                        case 3:
                                            {
                                                dataGridView1.Rows[i].Cells[j].Value = "Shared";
                                            }
                                            break;
                                        case 4:
                                            {
                                                dataGridView1.Rows[i].Cells[j].Value = "Non-adjoined";
                                            }
                                            break;
                                        case 5:
                                            {
                                                dataGridView1.Rows[i].Cells[j].Value = "Non-adjoined";
                                            }
                                            break;
                                    }
                                }
                                break;
                            case 9:
                                {
                                    if (Parent > 0) continue;
                                    dataGridView1.Rows[i].Cells[j].Value = "Vacant";
                                }
                                break;
                            case 10:
                                {
                                    // DATE OPENED
                                    if (Parent > 0) continue;
                                    TimeSpan time = new TimeSpan(0, 0, 0, 0);
                                    DateTime combined = date1.Add(time);

                                    dataGridView1.Rows[i].Cells[j].Value = combined.ToString("MM/dd/yyyy");
                                }
                                break;
                            case 19:
                            case 21:
                            case 23:
                                {
                                    if (Parent > 0) continue;
                                    dataGridView1.Rows[i].Cells[j].Value = "Default";
                                }
                                break;
                            case 25:
                                {
                                    if (Parent > 0) continue;
                                    dataGridView1.Rows[i].Cells[j].Value = "Market";
                                }
                                break;
                            case 27:
                                {
                                    if (Parent > 0) continue;
                                    dataGridView1.Rows[i].Cells[j].Value = valMember[0];
                                }
                                break;
                            case 29:
                                {
                                    if (Parent > 0) continue;
                                    dataGridView1.Rows[i].Cells[j].Value = valMember_RE[0];
                                }
                                break;
                        }
                    }
                }
            }
            else
            {
                for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                {
                    Parent = 0;
                    if (Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value) == Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value))
                    {
                        try
                        {
                            for (z = i + 1; z <= dataGridView1.RowCount - 1; z++)
                            {
                                if (Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value) == Convert.ToInt32(dataGridView1.Rows[z].Cells[1].Value))
                                {
                                    Parent = 1;
                                }
                            }
                        }
                        catch
                        {
                            Parent = 0;
                        }
                    }
                    for (j = 0; j <= dataGridView1.ColumnCount - 1; j++)
                    {
                        switch (j)
                        {
                            case 4:
                                {
                                    if (Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value) == Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value))
                                    {
                                        unitCount += 1;
                                        dataGridView1.Rows[i].Cells[j].Value = "Unit " + unitCount;
                                    }
                                    else
                                    {
                                        dataGridView1.Rows[i].Cells[j].Value = "Unit " + unitCount;
                                    }
                                }
                                break;
                            case 5:
                                {
                                    if (Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value) != Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value))
                                    {
                                        bedCount += 1;
                                        dataGridView1.Rows[i].Cells[j].Value = "U-" + unitCount + " : Bed " + bedCount;
                                    }
                                    else
                                    {
                                        int beds_per_room = 0;
                                        for (z = 0; z <= dataGridView1.RowCount - 1; z++)
                                        {
                                            if (Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value) == Convert.ToInt32(dataGridView1.Rows[z].Cells[1].Value))
                                            {
                                                beds_per_room += 1;
                                            }
                                        }
                                        if (beds_per_room == 1)
                                        {
                                            bedCount += 1;
                                            dataGridView1.Rows[i].Cells[j].Value = "U-" + unitCount + " : Bed " + bedCount;
                                        }
                                    }
                                }
                                break;
                            case 6:
                                {
                                    if (Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value) != Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value))
                                    {
                                        dataGridView1.Rows[i].Cells[j].Value = avgBed_SF;
                                    }
                                    else
                                    {
                                        int beds_per_room = 0;
                                        for (z = 0; z <= dataGridView1.RowCount - 1; z++)
                                        {
                                            if (Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value) == Convert.ToInt32(dataGridView1.Rows[z].Cells[1].Value))
                                            {
                                                beds_per_room += 1;
                                            }
                                        }

                                        switch (beds_per_room)
                                        {
                                            case 1:
                                                {
                                                    dataGridView1.Rows[i].Cells[j].Value = avgBed_SF * 1;
                                                }
                                                break;
                                            case 2:
                                                {
                                                    dataGridView1.Rows[i].Cells[j].Value = avgBed_SF * 1;
                                                }
                                                break;
                                            case 3:
                                                {
                                                    dataGridView1.Rows[i].Cells[j].Value = avgBed_SF * 2;
                                                }
                                                break;
                                            case 4:
                                                {
                                                    dataGridView1.Rows[i].Cells[j].Value = avgBed_SF * 3;
                                                }
                                                break;
                                            case 5:
                                                {
                                                    dataGridView1.Rows[i].Cells[j].Value = avgBed_SF * 4;
                                                }
                                                break;
                                        }
                                    }
                                }
                                break;
                            case 7:
                                {
                                    if (Parent > 0) continue;
                                    int beds_per_room = 0;
                                    for (z = 0; z <= dataGridView1.RowCount - 1; z++)
                                    {
                                        if (Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value) == Convert.ToInt32(dataGridView1.Rows[z].Cells[1].Value))
                                        {
                                            beds_per_room += 1;
                                        }
                                    }

                                    switch (beds_per_room)
                                    {
                                        case 1:
                                            {
                                                dataGridView1.Rows[i].Cells[j].Value = "1";
                                            }
                                            break;
                                        case 2:
                                            {
                                                dataGridView1.Rows[i].Cells[j].Value = "1";
                                            }
                                            break;
                                        case 3:
                                            {
                                                dataGridView1.Rows[i].Cells[j].Value = "2";
                                            }
                                            break;
                                        case 4:
                                            {
                                                if (dataGridView1.Rows[i - 1].Cells[j].Value == null || dataGridView1.Rows[i - 1].Cells[j].Value.ToString() == "")
                                                {
                                                    dataGridView1.Rows[i].Cells[j].Value = "1";
                                                }
                                                else
                                                {
                                                    dataGridView1.Rows[i].Cells[j].Value = "2";
                                                }
                                            }
                                            break;
                                        case 5:
                                            {
                                                dataGridView1.Rows[i].Cells[j].Value = "2";
                                            }
                                            break;
                                    }
                                }
                                break;
                            case 8:
                                {
                                    if (Parent > 0) continue;
                                    int beds_per_room = 0;
                                    for (z = 0; z <= dataGridView1.RowCount - 1; z++)
                                    {
                                        if (Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value) == Convert.ToInt32(dataGridView1.Rows[z].Cells[1].Value))
                                        {
                                            beds_per_room += 1;
                                        }
                                    }

                                    switch (beds_per_room)
                                    {
                                        case 1:
                                            {
                                                dataGridView1.Rows[i].Cells[j].Value = "Private";
                                            }
                                            break;
                                        case 2:
                                            {
                                                dataGridView1.Rows[i].Cells[j].Value = "Private";
                                            }
                                            break;
                                        case 3:
                                            {
                                                dataGridView1.Rows[i].Cells[j].Value = "Shared";
                                            }
                                            break;
                                        case 4:
                                            {
                                                dataGridView1.Rows[i].Cells[j].Value = "Non-adjoined";
                                            }
                                            break;
                                        case 5:
                                            {
                                                dataGridView1.Rows[i].Cells[j].Value = "Non-adjoined";
                                            }
                                            break;
                                    }
                                }
                                break;
                            case 9:
                                {
                                    if (Parent > 0) continue;
                                    dataGridView1.Rows[i].Cells[j].Value = "Occupied";
                                }
                                break;
                            case 11:
                                {
                                    if (Parent > 0) continue;
                                    dataGridView1.Rows[i].Cells[j].Value = DateTime.Now.ToString("MM/dd/yyyy");
                                }
                                break;
                            case 12:
                                {
                                    if (Parent > 0) continue;
                                    int rand_num = rand.Next(1, Payors.Count - 1);

                                    dataGridView1.Rows[i].Cells[j].Value = Convert.ToInt32(Payors[rand_num]);
                                }
                                break;
                            case 13:
                                {
                                    if (Parent > 0) continue;
                                    if(Information.IsNumeric(dataGridView1.Rows[i].Cells[j - 1].Value))
                                    {
                                        switch (dataGridView1.Rows[i].Cells[j - 1].Value)
                                        {
                                            case 1:
                                            case 4:
                                                {
                                                    int rand_num = rand.Next(1, 100);

                                                    dataGridView1.Rows[i].Cells[j].Value = rand_num;
                                                }
                                                break;
                                        }
                                    }  
                                }
                                break;
                            case 14:
                                {
                                    if (Parent > 0) continue;
                                    int rand_num = rand.Next(1, PT.Count - 1);

                                    dataGridView1.Rows[i].Cells[j].Value = Convert.ToInt32(PT[rand_num]);
                                }
                                break;
                            case 15:
                                {
                                    if (Parent > 0) continue;
                                    int rand_num = rand.Next(1, OT.Count - 1);

                                    dataGridView1.Rows[i].Cells[j].Value = Convert.ToInt32(OT[rand_num]);
                                }
                                break;
                            case 16:
                                {
                                    if (Parent > 0) continue;
                                    int rand_num = rand.Next(1, SLP.Count - 1);

                                    dataGridView1.Rows[i].Cells[j].Value = Convert.ToInt32(SLP[rand_num]);
                                }
                                break;
                            case 17:
                                {
                                    if (Parent > 0) continue;
                                    int rand_num = rand.Next(1, NTA.Count - 1);

                                    dataGridView1.Rows[i].Cells[j].Value = Convert.ToInt32(NTA[rand_num]);
                                }
                                break;
                            case 18:
                                {
                                    if (Parent > 0) continue;
                                    int rand_num = rand.Next(1, NRS.Count - 1);

                                    dataGridView1.Rows[i].Cells[j].Value = Convert.ToInt32(NRS[rand_num]);
                                }
                                break;
                            case 19:
                            case 21:
                            case 23:
                                {
                                    if (Parent > 0) continue;
                                    dataGridView1.Rows[i].Cells[j].Value = "Default";
                                }
                                break;
                            case 25:
                                {
                                    if (Parent > 0) continue;
                                    dataGridView1.Rows[i].Cells[j].Value = "Market";
                                }
                                break;
                            case 27:
                                {
                                    if (Parent > 0) continue;
                                    dataGridView1.Rows[i].Cells[j].Value = valMember[0];
                                }
                                break;
                            case 29:
                                {
                                    if (Parent > 0) continue;
                                    dataGridView1.Rows[i].Cells[j].Value = valMember_RE[0];
                                }
                                break;
                        }
                    }
                }
            }
        }

        public override void ClinicLoad(DataGridView dataGridView1)
        {
            int i;
            int r;
            int j;
            string Title = "TINUUM SOFTWARE";
            // DGV CTRLS 
            var cmbo1 = new DataGridViewComboBoxColumn();
            var cmbo2 = new DataGridViewComboBoxColumn();
            var cmbo3 = new DataGridViewComboBoxColumn();
            var cmbo4 = new DataGridViewComboBoxColumn();
            var cmbo5 = new DataGridViewComboBoxColumn();
            var cmbo6 = new DataGridViewComboBoxColumn();
            var cmbo7 = new DataGridViewComboBoxColumn();
            var cmbo8 = new DataGridViewComboBoxColumn();
            var cmbo9 = new DataGridViewComboBoxColumn();
            var cmbo10 = new DataGridViewComboBoxColumn();
            var cmbo11 = new DataGridViewComboBoxColumn();
            var cmbo12 = new DataGridViewComboBoxColumn();
            var cmbo13 = new DataGridViewComboBoxColumn();
            var cmbo14 = new DataGridViewComboBoxColumn();
            var cmbo15 = new DataGridViewComboBoxColumn();

            var btn1 = new DataGridViewButtonColumn();
            var btn2 = new DataGridViewButtonColumn();
            var btn3 = new DataGridViewButtonColumn();
            var btn4 = new DataGridViewButtonColumn();
            var btn5 = new DataGridViewButtonColumn();

            if (MLA.Count == 0)
            {
                MessageBox.Show("You must complete an entry for clinical turnover before continuing.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            terminate = 1;
            // SORT TABLES
            {
                SQL_Verse.ExecQuery("SELECT * FROM " + tbl_Name + " ORDER BY Collection_Num ASC;");
                SQL_Verse.ExecQuery("SELECT * FROM " + tbl_Dtl_Acuity + " ORDER BY Collection_Num ASC;");
                SQL_Verse.ExecQuery("SELECT * FROM " + tbl_Dtl_Assessment + " ORDER BY Collection_Num ASC;");
                SQL_Verse.ExecQuery("SELECT * FROM " + tbl_Dtl_Payor + " ORDER BY Collection_Num ASC;");
                SQL_Verse.ExecQuery("SELECT * FROM " + tbl_Dtl_Transition + " ORDER BY Collection_Num ASC;");
                SQL_Verse.ExecQuery("SELECT * FROM " + tbl_Dtl_Trans_RE + " ORDER BY Collection_Num ASC;");

                // COLUMN CONTROLS
                {
                    // ADD SPECS FOR COMBOBOX1
                    cmbo1.Items.Add("");
                    cmbo1.Items.Add("1");
                    cmbo1.Items.Add("2");
                    cmbo1.Items.Add("3");
                    cmbo1.Items.Add("4");
                    cmbo1.FlatStyle = FlatStyle.Popup;
                    cmbo1.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                    cmbo1.DisplayStyleForCurrentCellOnly = false;

                    // ADD SPECS FOR COMBOBOX2
                    cmbo2.Items.Add("");
                    cmbo2.Items.Add("Private");
                    cmbo2.Items.Add("Shared");
                    cmbo2.Items.Add("Non-adjoined");
                    cmbo2.FlatStyle = FlatStyle.Popup;
                    cmbo2.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                    cmbo2.DisplayStyleForCurrentCellOnly = false;

                    // ADD SPECS FOR COMBOBOX3
                    cmbo3.Items.Add("");
                    cmbo3.Items.Add("Occupied");
                    cmbo3.Items.Add("Vacant");
                    cmbo3.FlatStyle = FlatStyle.Popup;
                    cmbo3.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                    cmbo3.DisplayStyleForCurrentCellOnly = false;
                    
                    // ADD SPECS FOR COMBOBOX4
                    cmbo4.DataSource = SQL_Payors.DBDT;
                    cmbo4.DisplayMember = "Item1";
                    cmbo4.ValueMember = "Prime";
                    cmbo4.FlatStyle = FlatStyle.Popup;
                    cmbo4.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                    cmbo4.DisplayStyleForCurrentCellOnly = false;

                    // ADD SPECS FOR COMBOBOX5
                    cmbo5.DataSource = SQL_PT.DBDT;
                    cmbo5.DisplayMember = "Item1";
                    cmbo5.ValueMember = "Prime";
                    cmbo5.FlatStyle = FlatStyle.Popup;
                    cmbo5.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                    cmbo5.DisplayStyleForCurrentCellOnly = false;

                    // ADD SPECS FOR COMBOBOX6
                    cmbo6.DataSource = SQL_OT.DBDT;
                    cmbo6.DisplayMember = "Item1";
                    cmbo6.ValueMember = "Prime";
                    cmbo6.FlatStyle = FlatStyle.Popup;
                    cmbo6.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                    cmbo6.DisplayStyleForCurrentCellOnly = false;

                    // ADD SPECS FOR COMBOBOX7
                    cmbo7.DataSource = SQL_SLP.DBDT;
                    cmbo7.DisplayMember = "Item1";
                    cmbo7.ValueMember = "Prime";
                    cmbo7.FlatStyle = FlatStyle.Popup;
                    cmbo7.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                    cmbo7.DisplayStyleForCurrentCellOnly = false;

                    // ADD SPECS FOR COMBOBOX8
                    cmbo8.DataSource = SQL_NTA.DBDT;
                    cmbo8.DisplayMember = "Item1";
                    cmbo8.ValueMember = "Prime";
                    cmbo8.FlatStyle = FlatStyle.Popup;
                    cmbo8.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                    cmbo8.DisplayStyleForCurrentCellOnly = false;

                    // ADD SPECS FOR COMBOBOX9
                    cmbo9.DataSource = SQL_NRS.DBDT;
                    cmbo9.DisplayMember = "Item1";
                    cmbo9.ValueMember = "Prime";
                    cmbo9.FlatStyle = FlatStyle.Popup;
                    cmbo9.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                    cmbo9.DisplayStyleForCurrentCellOnly = false;

                    // ADD SPECS FOR COMBOBOX10
                    cmbo10.Items.Add(""); //DYNAMIC
                    cmbo10.Items.Add("Configure");
                    cmbo10.Items.Add("Default");
                    cmbo10.Items.Add("Detail");
                    cmbo10.FlatStyle = FlatStyle.Popup;
                    cmbo10.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                    cmbo10.DisplayStyleForCurrentCellOnly = false;

                    // ADD SPECS FOR COMBOBOX11
                    cmbo11.Items.Add(""); //DYNAMIC
                    cmbo11.Items.Add("Configure");
                    cmbo11.Items.Add("Default");
                    cmbo11.Items.Add("Detail");
                    cmbo11.FlatStyle = FlatStyle.Popup;
                    cmbo11.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                    cmbo11.DisplayStyleForCurrentCellOnly = false;

                    // ADD SPECS FOR COMBOBOX12
                    cmbo12.Items.Add(""); //DYNAMIC
                    cmbo12.Items.Add("Configure");
                    cmbo12.Items.Add("Default");
                    cmbo12.Items.Add("Detail");
                    cmbo12.FlatStyle = FlatStyle.Popup;
                    cmbo12.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                    cmbo12.DisplayStyleForCurrentCellOnly = false;

                    // ADD SPECS FOR COMBOBOX13
                    cmbo13.Items.Add(""); //DYNAMIC
                    cmbo13.Items.Add("Market");
                    cmbo13.Items.Add("Reabsorb");
                    cmbo13.FlatStyle = FlatStyle.Popup;
                    cmbo13.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                    cmbo13.DisplayStyleForCurrentCellOnly = false;

                    // ADD SPECS FOR COMBOBOX14
                    cmbo14.DataSource = cmbo.DataSource;
                    cmbo14.DisplayMember = "Transition Label";
                    cmbo14.ValueMember = "ID_Num";
                    cmbo14.FlatStyle = FlatStyle.Popup;
                    cmbo14.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                    cmbo14.DisplayStyleForCurrentCellOnly = false;

                    // ADD SPECS FOR COMBOBOX15
                    cmbo15.DataSource = cmboRE.DataSource;
                    cmbo15.DisplayMember = "Transition Label";
                    cmbo15.ValueMember = "ID_Num";
                    cmbo15.FlatStyle = FlatStyle.Popup;
                    cmbo15.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                    cmbo15.DisplayStyleForCurrentCellOnly = false;

                    // ADD SPECS FOR BUTTON1
                    btn1.UseColumnTextForButtonValue = true;
                    btn1.Text = "_";
                    btn1.FlatStyle = FlatStyle.System;
                    btn1.DefaultCellStyle.Alignment = (DataGridViewContentAlignment)ContentAlignment.BottomRight;
                    btn1.DefaultCellStyle.Font = new Font("Arial", 6, FontStyle.Bold);

                    // ADD SPECS FOR BUTTON2
                    btn2.UseColumnTextForButtonValue = true;
                    btn2.Text = "_";
                    btn2.FlatStyle = FlatStyle.System;
                    btn2.DefaultCellStyle.Alignment = (DataGridViewContentAlignment)ContentAlignment.BottomRight;
                    btn2.DefaultCellStyle.Font = new Font("Arial", 6, FontStyle.Bold);

                    // ADD SPECS FOR BUTTON3
                    btn3.UseColumnTextForButtonValue = true;
                    btn3.Text = "_";
                    btn3.FlatStyle = FlatStyle.System;
                    btn3.DefaultCellStyle.Alignment = (DataGridViewContentAlignment)ContentAlignment.BottomRight;
                    btn3.DefaultCellStyle.Font = new Font("Arial", 6, FontStyle.Bold);

                    // ADD SPECS FOR BUTTON4
                    btn4.UseColumnTextForButtonValue = true;
                    btn4.Text = "_";
                    btn4.FlatStyle = FlatStyle.System;
                    btn4.DefaultCellStyle.Alignment = (DataGridViewContentAlignment)ContentAlignment.BottomRight;
                    btn4.DefaultCellStyle.Font = new Font("Arial", 6, FontStyle.Bold);

                    // ADD SPECS FOR BUTTON5
                    btn5.UseColumnTextForButtonValue = true;
                    btn5.Text = "_";
                    btn5.FlatStyle = FlatStyle.System;
                    btn5.DefaultCellStyle.Alignment = (DataGridViewContentAlignment)ContentAlignment.BottomRight;
                    btn5.DefaultCellStyle.Font = new Font("Arial", 6, FontStyle.Bold);
                }

                // REFRESH ROWS & COLUMNS
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();

                // CREATE GRIDVIEW COLUMNS
                for (i = 0; i <= Col_Count - 1; i++)
                {
                    var switchExpr = Header_Name[i].Substring(Header_Name[i].Length - 3, 3);
                    switch (switchExpr)
                    {
                        case "(b)":
                            {
                                switch (i)
                                {
                                    case 20:
                                        {
                                            dataGridView1.Columns.Add(btn1);
                                        }
                                        break;
                                    case 22:
                                        {
                                            dataGridView1.Columns.Add(btn2);
                                        }
                                        break;
                                    case 24:
                                        {
                                            dataGridView1.Columns.Add(btn3);
                                        }
                                        break;
                                    case 28:
                                        {
                                            dataGridView1.Columns.Add(btn4);
                                        }
                                        break;
                                    case 30:
                                        {
                                            dataGridView1.Columns.Add(btn5);
                                        }
                                        break;
                                    default:
                                        break;
                                }
                            }
                            break;
                        case "(c)":
                            {
                                switch (i)
                                {
                                    case 7:
                                        {
                                            dataGridView1.Columns.Add(cmbo1);
                                        }
                                        break;
                                    case 8:
                                        {
                                            dataGridView1.Columns.Add(cmbo2);
                                        }
                                        break;
                                    case 9:
                                        {
                                            dataGridView1.Columns.Add(cmbo3);
                                        }
                                        break;
                                    case 12:
                                        {
                                            dataGridView1.Columns.Add(cmbo4);
                                        }
                                        break;
                                    case 14:
                                        {
                                            dataGridView1.Columns.Add(cmbo5);
                                        }
                                        break;
                                    case 15:
                                        {
                                            dataGridView1.Columns.Add(cmbo6);
                                        }
                                        break;
                                    case 16:
                                        {
                                            dataGridView1.Columns.Add(cmbo7);
                                        }
                                        break;
                                    case 17:
                                        {
                                            dataGridView1.Columns.Add(cmbo8);
                                        }
                                        break;
                                    case 18:
                                        {
                                            dataGridView1.Columns.Add(cmbo9);
                                        }
                                        break;
                                    case 19:
                                        {
                                            dataGridView1.Columns.Add(cmbo10);
                                        }
                                        break;
                                    case 21:
                                        {
                                            dataGridView1.Columns.Add(cmbo11);
                                        }
                                        break;
                                    case 23:
                                        {
                                            dataGridView1.Columns.Add(cmbo12);
                                        }
                                        break;
                                    case 25:
                                        {
                                            dataGridView1.Columns.Add(cmbo13);
                                        }
                                        break;
                                    case 27:
                                        {
                                            dataGridView1.Columns.Add(cmbo14);
                                        }
                                        break;
                                    case 29:
                                        {
                                            dataGridView1.Columns.Add(cmbo15);
                                        }
                                        break;
                                    default:
                                        break;
                                }
                            }
                            break;
                        default:
                            {
                                dataGridView1.Columns.Add("txt", "New Text");
                            }
                            break;
                    }
                }

                // SET HEADERS AND NON SORT
                for (i = 0; i <= dataGridView1.ColumnCount - 1; i++)
                {
                    dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    dataGridView1.Columns[i].HeaderText = Header_Rename[i];
                }

                // CALL EXISTING INSERT IF RECORD COUNT IS EMPTY
                SQL.ExecQuery("SELECT * FROM " + tbl_Name + ";");
                if (SQL.RecordCount == 0)
                {
                    this.Existing_Roster(dataGridView1);
                }
                
                // SET DGV NUMBER OF ROWS BY REFRESHING TBL ROWCOUNT 
                SQL_Verse.ExecQuery("SELECT * FROM " + tbl_Name + " ORDER BY Collection_Num ASC;");
                dataGridView1.RowCount = SQL_Verse.RecordCount;

                // FILL DATAGRID FROM DATA TABLE
                for (r = 0; r <= SQL_Verse.RecordCount - 1; r++)
                {
                    for (i = 0; i <= Col_Count - 1; i++)
                    {
                        switch (i)
                        {
                            case 12:
                            case 14:
                            case 15:
                            case 16:
                            case 17:
                            case 18:
                            case 27:
                            case 29:
                                {
                                    if (Information.IsNumeric(SQL_Verse.DBDT.Rows[r][i]))
                                    {
                                        dataGridView1.Rows[r].Cells[i].Value = Convert.ToInt32(SQL_Verse.DBDT.Rows[r][i]);
                                    }
                                    else
                                    {
                                        dataGridView1.Rows[r].Cells[i].Value = SQL_Verse.DBDT.Rows[r][i];
                                    }   
                                }
                                break;
                            default:
                                {
                                    dataGridView1.Rows[r].Cells[i].Value = SQL_Verse.DBDT.Rows[r][i];
                                }
                                break;
                        }
                    }
                }

                // MAKE ROWS IN COLUMN 1 READ ONLY AND NUMBER IN ORDER
                for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                {
                    try
                    {
                        dataGridView1.Rows[i].Cells[3].Value = i + 1;
                    }
                    catch (Exception ex)
                    {

                    }
                }

                // SET COLUMN SPECS
                for (i = 0; i <= dataGridView1.Columns.Count - 1; i++)
                {
                    var switchExpr1 = Header_Name[i].Substring(Header_Name[i].Length - 3, 3);
                    switch (switchExpr1)
                    {
                        case "(b)":
                            {
                                dataGridView1.Columns[i].Width = 20;
                                break;
                            }
                    }
                }

                // FREEZE COLUMNS & VISIBILITY
                dataGridView1.Columns[0].Frozen = true;
                dataGridView1.Columns[1].Frozen = true;
                dataGridView1.Columns[2].Frozen = true;
                dataGridView1.Columns[3].Frozen = true;
                dataGridView1.Columns[4].Frozen = true;
                dataGridView1.Columns[5].Frozen = true;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Visible = false;
                dataGridView1.Columns[2].Visible = false;

                // DATE TIME CELL STYLE
                dataGridView1.Columns[10].DefaultCellStyle.Format = "MM/dd/yyyy";
                dataGridView1.Columns[11].DefaultCellStyle.Format = "MM/dd/yyyy";
                dataGridView1.Columns[26].DefaultCellStyle.Format = "MM/dd/yyyy";

                // CALL EXISTING FILL
                this.Existing_Fill(dataGridView1);

                // DISABLE CELLS AS DEFAULT - ENABLE LATER
                for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                {
                    for (j = 4; j <= 8; j++)
                    {
                        dataGridView1.Rows[i].Cells[j].ReadOnly = true;
                        dataGridView1.Rows[i].Cells[j].Style.SelectionBackColor = SystemColors.Control;
                        dataGridView1.Rows[i].Cells[j].Style.SelectionForeColor = SystemColors.ControlDark;
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = SystemColors.Control;
                        dataGridView1.Rows[i].Cells[j].Style.ForeColor = SystemColors.ControlDark;
                    }

                    for (j = 10; j <= 18; j++)
                    {
                        dataGridView1.Rows[i].Cells[j].ReadOnly = true;
                        dataGridView1.Rows[i].Cells[j].Style.SelectionBackColor = SystemColors.Control;
                        dataGridView1.Rows[i].Cells[j].Style.SelectionForeColor = SystemColors.ControlDark;
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = SystemColors.Control;
                        dataGridView1.Rows[i].Cells[j].Style.ForeColor = SystemColors.ControlDark;
                    }

                    for (j = 26; j <= 30; j++)
                    {
                        dataGridView1.Rows[i].Cells[j].ReadOnly = true;
                        dataGridView1.Rows[i].Cells[j].Style.SelectionBackColor = SystemColors.Control;
                        dataGridView1.Rows[i].Cells[j].Style.SelectionForeColor = SystemColors.ControlDark;
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = SystemColors.Control;
                        dataGridView1.Rows[i].Cells[j].Style.ForeColor = SystemColors.ControlDark;
                    }
                    
                }

                // DYNAMC CTRLS HERE BECAUSE NEED TO ENABLE DATETIME
                //this.Dynamic_CTRLs(dataGridView1);

                // NESTED LOOP DID NOT WORK, SKIPPING ITERATION
                // HIGHLIGHT BORDER OF ROW INDEX FOR SUBLINES - COLUMN 3
                for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                {
                    if (Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value) != Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value))
                    {
                        dataGridView1.Rows[i].Cells[3].Style.SelectionBackColor = SystemColors.Control;
                        dataGridView1.Rows[i].Cells[3].Style.SelectionForeColor = SystemColors.ControlDark;
                        dataGridView1.Rows[i].Cells[3].Style.BackColor = SystemColors.Control;
                        dataGridView1.Rows[i].Cells[3].Style.ForeColor = SystemColors.ControlDark;
                    }
                }


                // ENABLE SELECT CELLS - COLUMN 9
                for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                {
                    switch (dataGridView1.Rows[i].Cells[9].Value.ToString())
                    {
                        case "Occupied":
                            {
                                for (j = 11; j <= 18; j++)
                                {
                                    if (new int[] { 11, 12, 14, 15, 16, 17, 18 }.Contains(j))
                                    {
                                        dataGridView1.Rows[i].Cells[j].ReadOnly = false;
                                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                                        dataGridView1.Rows[i].Cells[j].Style.ForeColor = Color.Black;
                                        dataGridView1.Rows[i].Cells[j].Style.SelectionBackColor = SystemColors.Highlight;
                                        dataGridView1.Rows[i].Cells[j].Style.SelectionForeColor = Color.White;
                                    }
                                }
                            }
                            // ENABLE DATE CONTROLS
                            foreach (Control ctrl in dataGridView1.Controls)
                            {
                                int Diff;
                                int rowNum;
                                string name = "dtr";

                                if (ctrl is DateTimePicker)
                                {
                                    Diff = ctrl.Name.Length - name.Trim().Length;
                                    rowNum = Convert.ToInt32(ctrl.Name.Substring(ctrl.Name.Length - Diff, Diff));

                                    if (rowNum == i && ctrl.Name.Substring(0, 3) == name)
                                    {
                                        ctrl.Enabled = true;
                                    }
                                }

                            }
                            break;
                        case "Vacant":
                            {
                                dataGridView1.Rows[i].Cells[10].ReadOnly = false;
                                dataGridView1.Rows[i].Cells[10].Style.BackColor = Color.White;
                                dataGridView1.Rows[i].Cells[10].Style.ForeColor = Color.Black;
                                dataGridView1.Rows[i].Cells[10].Style.SelectionBackColor = SystemColors.Highlight;
                                dataGridView1.Rows[i].Cells[10].Style.SelectionForeColor = Color.White;
                            }
                            // ENABLE DATE CONTROLS
                            foreach (Control ctrl in dataGridView1.Controls)
                            {
                                int Diff;
                                int rowNum;
                                string name = "dta";

                                if (ctrl is DateTimePicker)
                                {
                                    Diff = ctrl.Name.Length - name.Trim().Length;
                                    rowNum = Convert.ToInt32(ctrl.Name.Substring(ctrl.Name.Length - Diff, Diff));

                                    if (rowNum == i && ctrl.Name.Substring(0, 3) == name)
                                    {
                                        ctrl.Enabled = true;
                                    }
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }


                // ENABLE SELECT CELLS - COLUMN 12
                for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                {
                    if (Information.IsNumeric(dataGridView1.Rows[i].Cells[12].Value))
                    {
                        switch (dataGridView1.Rows[i].Cells[12].Value)
                        {
                            case 1:
                            case 4:
                                {
                                    dataGridView1.Rows[i].Cells[13].ReadOnly = false;
                                    dataGridView1.Rows[i].Cells[13].Style.BackColor = Color.White;
                                    dataGridView1.Rows[i].Cells[13].Style.ForeColor = Color.Black;
                                    dataGridView1.Rows[i].Cells[13].Style.SelectionBackColor = SystemColors.Highlight;
                                    dataGridView1.Rows[i].Cells[13].Style.SelectionForeColor = Color.White;
                                }
                                break;
                        }
                    }
                }


                // ENABLE SELECT CELLS - COLUMN 25
                for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                {
                    switch (dataGridView1.Rows[i].Cells[25].Value.ToString())
                    {
                        case "Market":
                            {
                                for (j = 27; j <= 30; j++)
                                {
                                    dataGridView1.Rows[i].Cells[j].ReadOnly = false;
                                    dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                                    dataGridView1.Rows[i].Cells[j].Style.ForeColor = Color.Black;
                                    dataGridView1.Rows[i].Cells[j].Style.SelectionBackColor = SystemColors.Highlight;
                                    dataGridView1.Rows[i].Cells[j].Style.SelectionForeColor = Color.White;
                                }
                            }
                            break;
                        case "Reabsorb":
                            {
                                dataGridView1.Rows[i].Cells[26].ReadOnly = false;
                                dataGridView1.Rows[i].Cells[26].Style.BackColor = Color.White;
                                dataGridView1.Rows[i].Cells[26].Style.ForeColor = Color.Black;
                                dataGridView1.Rows[i].Cells[26].Style.SelectionBackColor = SystemColors.Highlight;
                                dataGridView1.Rows[i].Cells[26].Style.SelectionForeColor = Color.White;
                                
                                // ENABLE DATE CONTROLS
                                foreach (Control ctrl in dataGridView1.Controls)
                                {
                                    int Diff;
                                    int rowNum;
                                    string name = "dtb";

                                    if (ctrl is DateTimePicker)
                                    {
                                        Diff = ctrl.Name.Length - name.Trim().Length;
                                        rowNum = Convert.ToInt32(ctrl.Name.Substring(ctrl.Name.Length - Diff, Diff));

                                        if (rowNum == i && ctrl.Name.Substring(0, 3) == name)
                                        {
                                            ctrl.Enabled = true;
                                        }
                                    }
                                }
                            }
                            break;
                    }
                }

                // DISABLE PARENT CELLS
                for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                {
                    int num;
                    int count = 0;
                    
                    num = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value);

                    for (j = 0; j <= dataGridView1.RowCount - 1; j++)
                    {
                        if (Convert.ToInt32(dataGridView1.Rows[j].Cells[1].Value) == num)
                        {
                            count += 1;
                        }
                    }

                    if (count > 1)
                    {
                        for (j = 4; j <= dataGridView1.ColumnCount - 1; j++)
                        {
                            dataGridView1.Rows[i].Cells[j].ReadOnly = true;
                            dataGridView1.Rows[i].Cells[j].Style.SelectionBackColor = SystemColors.Control;
                            dataGridView1.Rows[i].Cells[j].Style.SelectionForeColor = SystemColors.ControlDark;
                            dataGridView1.Rows[i].Cells[j].Style.BackColor = SystemColors.Control;
                            dataGridView1.Rows[i].Cells[j].Style.ForeColor = SystemColors.ControlDark;
                        }
                        for (j = 4; j <= 6; j++)
                        {
                            if (new int[] { 4, 6 }.Contains(j))
                            {
                                dataGridView1.Rows[i].Cells[j].ReadOnly = false;
                                dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                                dataGridView1.Rows[i].Cells[j].Style.ForeColor = Color.Black;
                                dataGridView1.Rows[i].Cells[j].Style.SelectionBackColor = SystemColors.Highlight;
                                dataGridView1.Rows[i].Cells[j].Style.SelectionForeColor = Color.White;
                            }
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value) == Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value))
                        {
                            for (j = 4; j <= 8; j++)
                            {
                                dataGridView1.Rows[i].Cells[j].ReadOnly = false;
                                dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                                dataGridView1.Rows[i].Cells[j].Style.ForeColor = Color.Black;
                                dataGridView1.Rows[i].Cells[j].Style.SelectionBackColor = SystemColors.Highlight;
                                dataGridView1.Rows[i].Cells[j].Style.SelectionForeColor = Color.White;
                            }
                        }
                        else
                        {
                            for (j = 5; j <= 8; j++)
                            {
                                dataGridView1.Rows[i].Cells[j].ReadOnly = false;
                                dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                                dataGridView1.Rows[i].Cells[j].Style.ForeColor = Color.Black;
                                dataGridView1.Rows[i].Cells[j].Style.SelectionBackColor = SystemColors.Highlight;
                                dataGridView1.Rows[i].Cells[j].Style.SelectionForeColor = Color.White;    
                            }
                        }   
                    }
                }

                // MAKE 1ST COLUMN READ ONLY
                for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                {
                    dataGridView1.Rows[i].Cells[0].ReadOnly = true;
                    dataGridView1.Rows[i].Cells[1].ReadOnly = true;
                    dataGridView1.Rows[i].Cells[2].ReadOnly = true;
                    dataGridView1.Rows[i].Cells[3].ReadOnly = true;
                }

                // MAKE 1ST COLUMN STATIC WHITE
                dataGridView1.Columns[3].DefaultCellStyle.SelectionBackColor = Color.White;
                dataGridView1.Columns[3].DefaultCellStyle.SelectionForeColor = Color.Black;

                // COLUMN ALIGNMENT & WIDTH
                dataGridView1.Columns[3].Width = 50;
                //dataGridView1.Columns[4].Width = 150;

                for (i = 3; i <= dataGridView1.ColumnCount - 1; i++)
                {
                    if (new int[] { 3, 6, 7, 10, 11, 13, 26 }.Contains(i))
                    {
                        dataGridView1.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dataGridView1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                    else
                    {
                        dataGridView1.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dataGridView1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    }
                }

                // CALL PROCEDURES
                terminate = 0;
            }
        }

        public override void Insert_Sub(DataGridView dataGridView1)
        {
            int i;
            int y;
            string Title = "TINUUM SOFTWARE";
            int num;
            int sub;
            int count;
            int j;
            string val;
            int exist = 0;
            int subCount = 0;
            int sublineMax = 4;
            Parent = 0;
            add = 1;

            if (dataGridView1.RowCount == 0) return;
            // CHECK THAT CURRENT CELL SELECTED
            if (dataGridView1.CurrentCell == null)
            {
                MessageBox.Show("Select row before adding subline.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // CHECK IF CURRENT IS EXISTING UNIT COUNT
            if (Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value) > 0)
            {
                MessageBox.Show("You cannot add to existing roster. Add new unit before continuiing.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // DETERMINE IF PARENT ALREADY HAS SUBLINES
            for (i = dataGridView1.CurrentCell.RowIndex; i <= dataGridView1.ColumnCount - 1; i++)
            {
                if (Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[1].Value) == Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value))
                {
                    subCount += 1;
                }
            }
            // IF ENTRY HAS 4 BEDS THEN EXIT
            if (subCount > sublineMax)
            {
                MessageBox.Show("Maximum of four beds per unit.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // QUERY
            this.Query_Header(dataGridView1);

            // IF CURRENT CELL IS PARENT THEN FORMAT CELLS FOR PROCESS 
            if (Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value) == Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[1].Value))
            {
                Parent = 1;
                for (j = 7; j <= dataGridView1.ColumnCount - 1; j++)
                {
                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[j].Value = null;
                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[j].ReadOnly = true;
                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[j].Style.SelectionBackColor = SystemColors.Control;
                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[j].Style.SelectionForeColor = SystemColors.ControlDark;
                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[j].Style.BackColor = SystemColors.Control;
                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[j].Style.ForeColor = SystemColors.ControlDark;
                }
                dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[5].Value = null;
                dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[5].ReadOnly = true;
                dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[5].Style.SelectionBackColor = SystemColors.Control;
                dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[5].Style.SelectionForeColor = SystemColors.ControlDark;
                dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[5].Style.BackColor = SystemColors.Control;
                dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[5].Style.ForeColor = SystemColors.ControlDark;
            }

            for (y = 0; y <= dataGridView1.ColumnCount - 1; y++)
            {
                for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                {
                    if (dataGridView1.Rows[i].Cells[y].ReadOnly == false)
                    {
                        if (Header_Name[y] == "")
                        {
                            // Do Nothing
                        }
                        else if (dataGridView1.Rows[i].Cells[y].Value == DBNull.Value || dataGridView1.Rows[i].Cells[y].Value == null || Convert.ToString(dataGridView1.Rows[i].Cells[y].Value) == "Configure")
                        {
                            MessageBox.Show("You must enter values for all fields before adding a new entry.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);

                            if (Parent > 0)
                            {
                                if (subCount <= 1)
                                {
                                    for (j = 4; j <= dataGridView1.ColumnCount - 1; j++)
                                    {
                                        dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[j].Value = null;
                                        dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[j].ReadOnly = true;
                                        dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[j].Style.SelectionBackColor = SystemColors.Control;
                                        dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[j].Style.SelectionForeColor = SystemColors.ControlDark;
                                        dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[j].Style.BackColor = SystemColors.Control;
                                        dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[j].Style.ForeColor = SystemColors.ControlDark;
                                    }
                                    for (j = 4; j <= 9; j++)
                                    {
                                        dataGridView1.Rows[i].Cells[j].ReadOnly = false;
                                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                                        dataGridView1.Rows[i].Cells[j].Style.ForeColor = Color.Black;
                                        dataGridView1.Rows[i].Cells[j].Style.SelectionBackColor = SystemColors.Highlight;
                                        dataGridView1.Rows[i].Cells[j].Style.SelectionForeColor = Color.White;
                                    }
                                    for (j = 19; j <= 25; j++)
                                    {
                                        dataGridView1.Rows[i].Cells[j].ReadOnly = false;
                                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                                        dataGridView1.Rows[i].Cells[j].Style.ForeColor = Color.Black;
                                        dataGridView1.Rows[i].Cells[j].Style.SelectionBackColor = SystemColors.Highlight;
                                        dataGridView1.Rows[i].Cells[j].Style.SelectionForeColor = Color.White;
                                    }
                                }
                            }

                            dataGridView1.CurrentCell = dataGridView1.Rows[i].Cells[y];
                            return;
                        }
                    }
                }
            }

            // CALL UPDATE
            this.UpdateSQL(dataGridView1);

            // INSERT NEWEST VERSE COLUMN
            SQL.ExecQuery("INSERT INTO " + tbl_Name + " DEFAULT VALUES;");

            // GET UPDATED ROW COUNT
            SQL_Verse.ExecQuery("SELECT * FROM " + tbl_Name + ";");
            count = SQL_Verse.RecordCount - 1;
            num = Convert.ToInt32(SQL_Verse.DBDT.Rows[count][0].ToString());
            sub = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[1].Value);

            // INSERT IDENTITY NUM INTO SUPPORTING DATABASES
            string Insert1 = "INSERT INTO " + tbl_Dtl_Acuity + " (ID_Num, Collection_Num) VALUES (" + num + ", " + sub + ");";
            string Insert2 = "INSERT INTO " + tbl_Dtl_Assessment + " (ID_Num, Collection_Num) VALUES (" + num + ", " + sub + ");";
            string Insert3 = "INSERT INTO " + tbl_Dtl_Payor + " (ID_Num, Collection_Num) VALUES (" + num + ", " + sub + ");";
            string Insert4 = "INSERT INTO " + tbl_Dtl_Transition + " (ID_Num, Collection_Num) VALUES (" + num + ", " + sub + ");";
            string Insert5 = "INSERT INTO " + tbl_Dtl_Trans_RE + " (ID_Num, Collection_Num) VALUES (" + num + ", " + sub + ");";

            SQL_Verse.ExecQuery(Insert1);
            SQL_Verse.ExecQuery(Insert2);
            SQL_Verse.ExecQuery(Insert3);
            SQL_Verse.ExecQuery(Insert4);
            SQL_Verse.ExecQuery(Insert5);

            // UPDATE VERSE COLLECTION ID
            SQL_Verse.AddParam("@PrimKey", num);
            SQL_Verse.AddParam("@Num", sub);
            SQL_Verse.AddParam("@Exist", exist);
            string Update = "UPDATE " + tbl_Name + " SET Collection_Num=@Num, Existing_Num=@Exist WHERE ID_Num=@PrimKey;";
            SQL_Verse.ExecQuery(Update);
            // CALL METHODS
            this.Add_Source(dataGridView1);
            this.ClinicLoad(dataGridView1);

            Parent = 0;
            add = 0;
        }

        public override void InsertUser(DataGridView dataGridView1)
        {
            int i;
            int y;
            string Title = "TINUUM SOFTWARE";
            int num;
            int count;
            int exist = 0;

            add = 1;
            Parent = 0;

            this.Query_Header(dataGridView1);

            for (y = 0; y <= dataGridView1.ColumnCount - 1; y++)
            {
                for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                {
                    if (dataGridView1.Rows[i].Cells[y].ReadOnly == false)
                    {
                        if (Header_Name[y] == "")
                        {
                            // Do Nothing
                        }
                        else if (dataGridView1.Rows[i].Cells[y].Value == DBNull.Value || Convert.ToString(dataGridView1.Rows[i].Cells[y].Value) == "Configure")
                        {
                            MessageBox.Show("You must enter values for all fields before adding a new entry.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            dataGridView1.CurrentCell = dataGridView1.Rows[i].Cells[y];
                            return;
                        }
                    }

                }
            }

            // CALL UPDATE
            this.UpdateSQL(dataGridView1);

            // INSERT NEWEST VERSE COLUMN
            SQL.ExecQuery("INSERT INTO " + tbl_Name + " DEFAULT VALUES;");

            // GET UPDATED ROW COUNT
            SQL_Verse.ExecQuery("SELECT * FROM " + tbl_Name + ";");
            count = SQL_Verse.RecordCount - 1;
            num = Convert.ToInt32(SQL_Verse.DBDT.Rows[count][0].ToString());

            // INSERT IDENTITY NUM INTO SIPPORTING DATABASES
            string cmdInsert1 = "INSERT INTO " + tbl_Dtl_Acuity + " (ID_Num, Collection_Num) VALUES (" + num + ", " + num + ");";
            string cmdInsert2 = "INSERT INTO " + tbl_Dtl_Assessment + " (ID_Num, Collection_Num) VALUES (" + num + ", " + num + ");";
            string cmdInsert3 = "INSERT INTO " + tbl_Dtl_Payor + " (ID_Num, Collection_Num) VALUES (" + num + ", " + num + ");";
            string cmdInsert4 = "INSERT INTO " + tbl_Dtl_Transition + " (ID_Num, Collection_Num) VALUES (" + num + ", " + num + ");";
            string cmdInsert5 = "INSERT INTO " + tbl_Dtl_Trans_RE + " (ID_Num, Collection_Num) VALUES (" + num + ", " + num + ");";

            SQL_Verse.ExecQuery(cmdInsert1);
            SQL_Verse.ExecQuery(cmdInsert2);
            SQL_Verse.ExecQuery(cmdInsert3);
            SQL_Verse.ExecQuery(cmdInsert4);
            SQL_Verse.ExecQuery(cmdInsert5);

            // UPDATE VERSE COLLECTION ID
            SQL_Verse.AddParam("@PrimKey", num);
            SQL_Verse.AddParam("@Num", num);
            SQL_Verse.AddParam("@Exist", exist);
            string cmdUpdate = "UPDATE " + tbl_Name + " SET Collection_Num=@Num, Existing_Num=@Exist WHERE ID_Num=@PrimKey;";
            SQL_Verse.ExecQuery(cmdUpdate);
            // CALL METHODS
            this.Add_Source(dataGridView1);
            this.ClinicLoad(dataGridView1);

            add = 0;
        }

        public override void UpdateSQL(DataGridView dataGridView1)
        {
            int i;
            int y;
            int j;
            string cmdUpdate;
            string title = "TINUUM SOFTWARE";

            dataGridView1.Tag = dataGridView1.Parent.Parent.Parent;
            Form frm = (Form)dataGridView1.Tag;
            TabControl tab = (TabControl)dataGridView1.Parent.Parent;

            SQL.ExecQuery("SELECT * FROM " + tbl_Inventory + ";");
            record = SQL.RecordCount;

            tab.TabPages[gridNum - 1].Show();

            if (add == 0)
            {
                this.Query_Header(dataGridView1);
            }

            if (dataGridView1.RowCount == 0)
            {
                // Nothing
            }
            else
            {
                for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                {
                    for (y = 0; y <= dataGridView1.ColumnCount - 1; y++)
                    {
                        if (dataGridView1.Rows[i].Cells[y].ReadOnly == false)
                        {
                            if (Header_Name[y] == "")
                            {
                                // Do Nothing
                            }
                            else if (dataGridView1.Rows[i].Cells[y].Value == DBNull.Value || dataGridView1.Rows[i].Cells[y].Value == null || Convert.ToString(dataGridView1.Rows[i].Cells[y].Value) == "Configure")
                            {
                                MessageBox.Show("You must enter relevant values for all fields before continuing.", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                dataGridView1.CurrentCell = dataGridView1.Rows[i].Cells[y];
                                escape = 1;
                                return;
                            }
                        }

                    }
                }
            }

            for (y = 0; y <= dataGridView1.RowCount - 1; y++)
            {
                for (i = 1; i <= dataGridView1.ColumnCount - 1; i++)
                {
                    if (new int[] { 27, 29 }.Contains(i))
                    {
                        // SUBMIT TO MAJOR DATA TABLE
                        if (dataGridView1.Rows[y].Cells[i].ReadOnly == false)
                        {
                            // ADD PARAMS
                            switch (i)
                            {
                                default:
                                    {
                                        SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                        SQL_Verse.AddParam("@vals", dataGridView1.Rows[y].Cells[i].Value);
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                            SQL_Verse.AddParam("@vals", DBNull.Value);
                        }
                        cmdUpdate = "UPDATE " + tbl_Name + " SET " + Headers_Submit[i] + "=@vals WHERE ID_Num=@PrimKey;";
                        SQL_Verse.ExecQuery(cmdUpdate);

                        if (frm.ActiveControl.Name == "btnSubmit")
                        {
                            // UPDATE STATEMENT FOR DETAIL IF NUMERIC
                            if (dataGridView1.Rows[y].Cells[i].ReadOnly == false)
                            {
                                if (new int[] { 27, 29 }.Contains(i))
                                {
                                    switch (i)
                                    {
                                        case 27:
                                            {
                                                if (Information.IsNumeric(dataGridView1.Rows[y].Cells[i].Value))
                                                {
                                                    for (j = 1; j <= myMethods.Period * Mos_Const; j++)
                                                    {
                                                        SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                        SQL_Verse.AddParam("@vals", dataGridView1.Rows[y].Cells[i].Value);
                                                        string header = "month" + j;
                                                        string cmdUpdate1 = "UPDATE " + tbl_Dtl_Transition + " SET " + header + "=@vals WHERE ID_Num=@PrimKey;";
                                                        SQL_Verse.ExecQuery(cmdUpdate1);
                                                    }
                                                }
                                            }
                                            break;
                                        case 29:
                                            {
                                                if (Information.IsNumeric(dataGridView1.Rows[y].Cells[i].Value))
                                                {
                                                    for (j = 1; j <= myMethods.Period * Mos_Const; j++)
                                                    {
                                                        SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                        SQL_Verse.AddParam("@vals", dataGridView1.Rows[y].Cells[i].Value);
                                                        string header = "month" + j;
                                                        string cmdUpdate1 = "UPDATE " + tbl_Dtl_Trans_RE + " SET " + header + "=@vals WHERE ID_Num=@PrimKey;";
                                                        SQL_Verse.ExecQuery(cmdUpdate1);
                                                    }
                                                }
                                            }
                                            break;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                        if (Header_Name[i] == "")
                        {
                            SQL_Verse.AddParam("@vals", DBNull.Value);
                        }
                        else if (dataGridView1.Rows[y].Cells[i].ReadOnly == false)
                        {
                            SQL_Verse.AddParam("@vals", dataGridView1.Rows[y].Cells[i].Value);
                        }
                        else
                        {
                            if (i > 2) // SUBMIT NULL FOR READ ONLY AFTER INDEX VALUE
                            {
                                switch (i)
                                {
                                    case 4: // CONTROL FOR UNIT VALUE
                                        {
                                            if (dataGridView1.Rows[y].Cells[i].Value == null)
                                            {
                                                SQL_Verse.AddParam("@vals", DBNull.Value);
                                            }
                                            else
                                            {
                                                SQL_Verse.AddParam("@vals", dataGridView1.Rows[y].Cells[i].Value);
                                            }
                                        }
                                        break;
                                    default:
                                        {
                                            SQL_Verse.AddParam("@vals", DBNull.Value);
                                        }
                                        break;
                                }
                            }
                            else
                            {
                                SQL_Verse.AddParam("@vals", dataGridView1.Rows[y].Cells[i].Value);
                            }
                        }
                        cmdUpdate = "UPDATE " + tbl_Name + " SET " + Headers_Submit[i] + "=@vals WHERE ID_Num=@PrimKey;";
                        SQL_Verse.ExecQuery(cmdUpdate);
                    }
                }
            }
            if (frm.ActiveControl.Name == "btnSubmit")
            {
                if (gridNum == record)
                {
                    frm.Dispose();
                }
            }
        }

        public override void Cancel(DataGridView dataGridView1)
        {
            int i;
            int y;
            int j;
            string Title = "TINUUM SOFTWARE";
            
            terminate = 1;

            dataGridView1.Tag = dataGridView1.Parent.Parent.Parent;
            Form frm = (Form)dataGridView1.Tag;
            TabControl tab = (TabControl)dataGridView1.Parent.Parent;

            SQL.ExecQuery("SELECT * FROM " + tbl_Inventory + ";");
            record = SQL.RecordCount;

            tab.TabPages[gridNum - 1].Show();

            this.Query_Header(dataGridView1);
            
            if (gridNum == 1)
            {
                prompt = MessageBox.Show("Are you sure? Any unsubmitted data will be lost.", Title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                Rslt_Cncl = prompt.ToString();
            }

            if (Rslt_Cncl == "No") return;
            if (gridNum > 1) prompt = DialogResult.Yes;

            if (prompt == DialogResult.Yes)
            {
                if (dataGridView1.RowCount != 0)
                {
                    // CLEAR GRID AND RESET WITH ORIGINAL TABLE
                    // CALL METHODS
                    this.Add_Source(dataGridView1);
                    this.ClinicLoad(dataGridView1);

                    // DELETE ROWS FROM RELEVANT TABLES
                    for (y = 0; y <= dataGridView1.ColumnCount - 1; y++)
                    {
                        for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                        {
                            if (Header_Name[y] == "")
                            {
                                // Do Nothing
                            }
                            else if (dataGridView1.Rows[i].Cells[y].ReadOnly == false && string.IsNullOrEmpty(dataGridView1.Rows[i].Cells[y].Value.ToString()))
                            {
                                //DELETE SELECTED ROWS FROM TABLE
                                if (Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value) == Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value))
                                {
                                    SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[i].Cells[0].Value.ToString());
                                    SQL_Verse.ExecQuery("DELETE FROM " + tbl_Name + " WHERE Collection_Num=@PrimKey;");
                                }
                                else
                                {
                                    SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[i].Cells[0].Value.ToString());
                                    SQL_Verse.ExecQuery("DELETE FROM " + tbl_Name + " WHERE ID_Num=@PrimKey;");
                                }
                            }
                        }
                    }
                    for (y = 0; y <= dataGridView1.RowCount - 1; y++)
                    {
                        for (i = 1; i <= dataGridView1.ColumnCount - 1; i++)
                        {
                            if (new int[] { 27, 29 }.Contains(i))
                            {
                                // UPDATE STATEMENT FOR DETAIL IF NUMERIC
                                if (dataGridView1.Rows[y].Cells[i].ReadOnly == false)
                                {
                                    switch (i)
                                    {
                                        case 27:
                                            {
                                                if (Information.IsNumeric(dataGridView1.Rows[y].Cells[i].Value))
                                                {
                                                    for (j = 1; j <= myMethods.Period * Mos_Const; j++)
                                                    {
                                                        SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                        SQL_Verse.AddParam("@vals", dataGridView1.Rows[y].Cells[i].Value);
                                                        string header = "month" + j;
                                                        string cmdUpdate1 = "UPDATE " + tbl_Dtl_Transition + " SET " + header + "=@vals WHERE ID_Num=@PrimKey;";
                                                        SQL_Verse.ExecQuery(cmdUpdate1);
                                                    }
                                                }
                                            }
                                            break;
                                        case 29:
                                            {
                                                if (Information.IsNumeric(dataGridView1.Rows[y].Cells[i].Value))
                                                {
                                                    for (j = 1; j <= myMethods.Period * Mos_Const; j++)
                                                    {
                                                        SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                        SQL_Verse.AddParam("@vals", dataGridView1.Rows[y].Cells[i].Value);
                                                        string header = "month" + j;
                                                        string cmdUpdate1 = "UPDATE " + tbl_Dtl_Trans_RE + " SET " + header + "=@vals WHERE ID_Num=@PrimKey;";
                                                        SQL_Verse.ExecQuery(cmdUpdate1);
                                                    }
                                                }
                                            }
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
                // CLOSE FOR BOTH CASES
                if (gridNum == record)
                {
                    frm.Close();
                    return;
                }  
            }
            else
            {
                return;
            }
            terminate = 0;
        }

        public override void Delete_Command(DataGridView dataGridView1)
        {
            int r;
            string Title = "TINUUM SOFTWARE";
            dataGridView1.Tag = dataGridView1.Parent.Parent.Parent;

            // CHECK IF CURRENT IS EXISTING UNIT COUNT
            if (Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value) > 0)
            {
                MessageBox.Show("You cannot Delete existing roster. Retry.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (dataGridView1.RowCount == 0) return;
            // CHECK THAT CURRENT CELL SELECTED
            if (dataGridView1.CurrentCell == null)
            {
                MessageBox.Show("Select row before Deleting.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult promptDlt = MessageBox.Show("Are you sure you want to permanently delete?", Title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            try
            {
                if (promptDlt == DialogResult.Yes)
                {
                    r = dataGridView1.CurrentCell.RowIndex;

                    //DELETE SELECTED ROWS FROM TABLE
                    if (Convert.ToInt32(dataGridView1.Rows[r].Cells[0].Value) == Convert.ToInt32(dataGridView1.Rows[r].Cells[1].Value))
                    {
                        SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[r].Cells[0].Value.ToString());
                        SQL_Verse.ExecQuery("DELETE FROM " + tbl_Name + " WHERE Collection_Num=@PrimKey;");
                    }
                    else
                    {
                        SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[r].Cells[0].Value.ToString());
                        SQL_Verse.ExecQuery("DELETE FROM " + tbl_Name + " WHERE ID_Num=@PrimKey;");
                    }

                    // CALL METHODS
                    this.Add_Source(dataGridView1);
                    this.ClinicLoad(dataGridView1);
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
            }
        }
        public override void CellEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView1 = (DataGridView)sender;

            int i;
            int j;
            string title = "TINUUM SOFTWARE";

            switch (e.ColumnIndex)
            {
                case 9:
                    {
                        switch (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)
                        {
                            case null:
                                {
                                    // CLEAR CONTENTS OF IRRELEVANT CELLS
                                    for (j = 10; j <= 18; j++)
                                    {
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Value = null;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].ReadOnly = true;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionBackColor = SystemColors.Control;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionForeColor = SystemColors.ControlDark;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.BackColor = SystemColors.Control;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.ForeColor = SystemColors.ControlDark;
                                    }
                                    // DISABLE DATE CONTROLS
                                    foreach (Control ctrl in dataGridView1.Controls)
                                    {
                                        int Diff;
                                        int rowNum;
                                        string name = "dta";

                                        if (ctrl is DateTimePicker)
                                        {
                                            Diff = ctrl.Name.Length - name.Trim().Length;
                                            rowNum = Convert.ToInt32(ctrl.Name.Substring(ctrl.Name.Length - Diff, Diff));

                                            if (rowNum == e.RowIndex && ctrl.Name.Substring(0, 3) == name)
                                            {
                                                ctrl.Enabled = false;
                                            }
                                        }

                                    }
                                    foreach (Control ctrl in dataGridView1.Controls)
                                    {
                                        int Diff;
                                        int rowNum;
                                        string name = "dtr";

                                        if (ctrl is DateTimePicker)
                                        {
                                            Diff = ctrl.Name.Length - name.Trim().Length;
                                            rowNum = Convert.ToInt32(ctrl.Name.Substring(ctrl.Name.Length - Diff, Diff));

                                            if (rowNum == e.RowIndex && ctrl.Name.Substring(0, 3) == name)
                                            {
                                                ctrl.Enabled = false;
                                            }
                                        }

                                    }
                                }
                                break;
                            case "Vacant":
                                {
                                    // CLEAR CONTENTS OF IRRELEVANT CELLS
                                    for (j = 10; j <= 18; j++)
                                    {
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Value = null;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].ReadOnly = true;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionBackColor = SystemColors.Control;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionForeColor = SystemColors.ControlDark;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.BackColor = SystemColors.Control;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.ForeColor = SystemColors.ControlDark;
                                    }
                                    // DISABLE DATE CONTROLS
                                    foreach (Control ctrl in dataGridView1.Controls)
                                    {
                                        int Diff;
                                        int rowNum;
                                        string name = "dta";

                                        if (ctrl is DateTimePicker)
                                        {
                                            Diff = ctrl.Name.Length - name.Trim().Length;
                                            rowNum = Convert.ToInt32(ctrl.Name.Substring(ctrl.Name.Length - Diff, Diff));

                                            if (rowNum == e.RowIndex && ctrl.Name.Substring(0, 3) == name)
                                            {
                                                ctrl.Enabled = false;
                                            }
                                        }

                                    }
                                    foreach (Control ctrl in dataGridView1.Controls)
                                    {
                                        int Diff;
                                        int rowNum;
                                        string name = "dtr";

                                        if (ctrl is DateTimePicker)
                                        {
                                            Diff = ctrl.Name.Length - name.Trim().Length;
                                            rowNum = Convert.ToInt32(ctrl.Name.Substring(ctrl.Name.Length - Diff, Diff));

                                            if (rowNum == e.RowIndex && ctrl.Name.Substring(0, 3) == name)
                                            {
                                                ctrl.Enabled = false;
                                            }
                                        }

                                    }

                                    // ENABLE RELEVANT
                                    dataGridView1.Rows[e.RowIndex].Cells[10].ReadOnly = false;
                                    dataGridView1.Rows[e.RowIndex].Cells[10].Style.BackColor = Color.White;
                                    dataGridView1.Rows[e.RowIndex].Cells[10].Style.ForeColor = Color.Black;
                                    dataGridView1.Rows[e.RowIndex].Cells[10].Style.SelectionBackColor = SystemColors.Highlight;
                                    dataGridView1.Rows[e.RowIndex].Cells[10].Style.SelectionForeColor = Color.White;
                                    // ENABLE DATE CONTROLS
                                    foreach (Control ctrl in dataGridView1.Controls)
                                    {
                                        int Diff;
                                        int rowNum;
                                        string name = "dta";

                                        if (ctrl is DateTimePicker)
                                        {
                                            Diff = ctrl.Name.Length - name.Trim().Length;
                                            rowNum = Convert.ToInt32(ctrl.Name.Substring(ctrl.Name.Length - Diff, Diff));

                                            if (rowNum == e.RowIndex && ctrl.Name.Substring(0, 3) == name)
                                            {
                                                ctrl.Enabled = true;
                                            }
                                        }

                                    }
                                }
                                break;
                            case "Occupied":
                                {
                                    // CLEAR CONTENTS OF IRRELEVANT CELLS
                                    for (j = 10; j <= 18; j++)
                                    {
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Value = null;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].ReadOnly = true;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionBackColor = SystemColors.Control;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionForeColor = SystemColors.ControlDark;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.BackColor = SystemColors.Control;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.ForeColor = SystemColors.ControlDark;
                                    }
                                    // DISABLE DATE CONTROLS
                                    foreach (Control ctrl in dataGridView1.Controls)
                                    {
                                        int Diff;
                                        int rowNum;
                                        string name = "dta";

                                        if (ctrl is DateTimePicker)
                                        {
                                            Diff = ctrl.Name.Length - name.Trim().Length;
                                            rowNum = Convert.ToInt32(ctrl.Name.Substring(ctrl.Name.Length - Diff, Diff));

                                            if (rowNum == e.RowIndex && ctrl.Name.Substring(0, 3) == name)
                                            {
                                                ctrl.Enabled = false;
                                            }
                                        }

                                    }
                                    foreach (Control ctrl in dataGridView1.Controls)
                                    {
                                        int Diff;
                                        int rowNum;
                                        string name = "dtr";

                                        if (ctrl is DateTimePicker)
                                        {
                                            Diff = ctrl.Name.Length - name.Trim().Length;
                                            rowNum = Convert.ToInt32(ctrl.Name.Substring(ctrl.Name.Length - Diff, Diff));

                                            if (rowNum == e.RowIndex && ctrl.Name.Substring(0, 3) == name)
                                            {
                                                ctrl.Enabled = false;
                                            }
                                        }

                                    }
                                    // UNLOCK
                                    for (j = 11; j <= 12; j++)
                                    {
                                        dataGridView1.Rows[e.RowIndex].Cells[j].ReadOnly = false;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.BackColor = Color.White;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.ForeColor = Color.Black;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionBackColor = SystemColors.Highlight;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionForeColor = Color.White;
                                    }
                                    for (j = 14; j <= 18; j++)
                                    {
                                        dataGridView1.Rows[e.RowIndex].Cells[j].ReadOnly = false;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.BackColor = Color.White;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.ForeColor = Color.Black;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionBackColor = SystemColors.Highlight;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionForeColor = Color.White;
                                    }
                                    // ENABLE DTR CONTROL
                                    foreach (Control ctrl in dataGridView1.Controls)
                                    {
                                        int Diff;
                                        int rowNum;
                                        string name = "dtr";

                                        if (ctrl is DateTimePicker)
                                        {
                                            Diff = ctrl.Name.Length - name.Trim().Length;
                                            rowNum = Convert.ToInt32(ctrl.Name.Substring(ctrl.Name.Length - Diff, Diff));

                                            if (rowNum == e.RowIndex && ctrl.Name.Substring(0, 3) == name)
                                            {
                                                ctrl.Enabled = true;
                                            }
                                        }

                                    }
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case 12:
                    {
                        switch (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)
                        {
                            case 1:
                            case 4:
                                {
                                    dataGridView1.Rows[e.RowIndex].Cells[13].ReadOnly = false;
                                    dataGridView1.Rows[e.RowIndex].Cells[13].Style.BackColor = Color.White;
                                    dataGridView1.Rows[e.RowIndex].Cells[13].Style.ForeColor = Color.Black;
                                    dataGridView1.Rows[e.RowIndex].Cells[13].Style.SelectionBackColor = SystemColors.Highlight;
                                    dataGridView1.Rows[e.RowIndex].Cells[13].Style.SelectionForeColor = Color.White;
                                }
                                break;
                            default:
                                {
                                    // CLEAR CONTENTS OF IRRELEVANT CELLS
                                    dataGridView1.Rows[e.RowIndex].Cells[13].Value = null;
                                    dataGridView1.Rows[e.RowIndex].Cells[13].ReadOnly = true;
                                    dataGridView1.Rows[e.RowIndex].Cells[13].Style.SelectionBackColor = SystemColors.Control;
                                    dataGridView1.Rows[e.RowIndex].Cells[13].Style.SelectionForeColor = SystemColors.ControlDark;
                                    dataGridView1.Rows[e.RowIndex].Cells[13].Style.BackColor = SystemColors.Control;
                                    dataGridView1.Rows[e.RowIndex].Cells[13].Style.ForeColor = SystemColors.ControlDark;
                                }
                                break;
                        }
                    }
                    break;
                case 25:
                    {
                        switch (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)
                        {
                            case null:
                                {
                                    // CLEAR CONTENTS OF IRRELEVANT CELLS
                                    for (j = 26; j <= 30; j++)
                                    {
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Value = null;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].ReadOnly = true;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionBackColor = SystemColors.Control;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionForeColor = SystemColors.ControlDark;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.BackColor = SystemColors.Control;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.ForeColor = SystemColors.ControlDark;
                                    }
                                    // DISABLE DATE CONTROLS
                                    foreach (Control ctrl in dataGridView1.Controls)
                                    {
                                        int Diff;
                                        int rowNum;
                                        string name = "dtb";

                                        if (ctrl is DateTimePicker)
                                        {
                                            Diff = ctrl.Name.Length - name.Trim().Length;
                                            rowNum = Convert.ToInt32(ctrl.Name.Substring(ctrl.Name.Length - Diff, Diff));

                                            if (rowNum == e.RowIndex && ctrl.Name.Substring(0, 3) == name)
                                            {
                                                ctrl.Enabled = false;
                                            }
                                        }
                                    }
                                }
                                break;
                            case "Reabsorb":
                                {
                                    // CLEAR CONTENTS OF IRRELEVANT CELLS
                                    for (j = 26; j <= 30; j++)
                                    {
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Value = null;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].ReadOnly = true;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionBackColor = SystemColors.Control;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionForeColor = SystemColors.ControlDark;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.BackColor = SystemColors.Control;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.ForeColor = SystemColors.ControlDark;
                                    }

                                    dataGridView1.Rows[e.RowIndex].Cells[26].ReadOnly = false;
                                    dataGridView1.Rows[e.RowIndex].Cells[26].Style.BackColor = Color.White;
                                    dataGridView1.Rows[e.RowIndex].Cells[26].Style.ForeColor = Color.Black;
                                    dataGridView1.Rows[e.RowIndex].Cells[26].Style.SelectionBackColor = SystemColors.Highlight;
                                    dataGridView1.Rows[e.RowIndex].Cells[26].Style.SelectionForeColor = Color.White;

                                    dataGridView1.Rows[e.RowIndex].Cells[26].Value = DateTime.Today;

                                    // ENABLE DATE CONTROLS
                                    foreach (Control ctrl in dataGridView1.Controls)
                                    {
                                        int Diff;
                                        int rowNum;
                                        string name = "dtb";

                                        if (ctrl is DateTimePicker)
                                        {
                                            Diff = ctrl.Name.Length - name.Trim().Length;
                                            rowNum = Convert.ToInt32(ctrl.Name.Substring(ctrl.Name.Length - Diff, Diff));

                                            if (rowNum == e.RowIndex && ctrl.Name.Substring(0, 3) == name)
                                            {
                                                ctrl.Enabled = true;
                                            }
                                        }

                                    }
                                }
                                break;
                            case "Market":
                                {
                                    // CLEAR CONTENTS OF IRRELEVANT CELLS
                                    for (j = 26; j <= 30; j++)
                                    {
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Value = null;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].ReadOnly = true;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionBackColor = SystemColors.Control;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionForeColor = SystemColors.ControlDark;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.BackColor = SystemColors.Control;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.ForeColor = SystemColors.ControlDark;
                                    }
                                    // DISABLE DATE CONTROLS
                                    foreach (Control ctrl in dataGridView1.Controls)
                                    {
                                        int Diff;
                                        int rowNum;
                                        string name = "dtb";

                                        if (ctrl is DateTimePicker)
                                        {
                                            Diff = ctrl.Name.Length - name.Trim().Length;
                                            rowNum = Convert.ToInt32(ctrl.Name.Substring(ctrl.Name.Length - Diff, Diff));

                                            if (rowNum == e.RowIndex && ctrl.Name.Substring(0, 3) == name)
                                            {
                                                ctrl.Enabled = false;
                                            }
                                        }

                                    }

                                    for (j = 27; j <= 30; j++)
                                    {
                                        dataGridView1.Rows[e.RowIndex].Cells[j].ReadOnly = false;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.BackColor = Color.White;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.ForeColor = Color.Black;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionBackColor = SystemColors.Highlight;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionForeColor = Color.White;
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case 10:
                case 11:
                case 26:
                    {
                        try
                        {
                            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null) return;
                            DateTime value;
                            if (!DateTime.TryParse(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out value))
                            {
                                MessageBox.Show("You must enter a relevant date before continuing.", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;
                            }
                        }
                        catch
                        {

                        }
                    }
                    break;
                case 4:
                    {
                        // COPY UNIT NUMBER DOWN TO REFERENCING ROWS
                        for (j = e.RowIndex + 1; j <= dataGridView1.RowCount - 1; j++)
                        {
                            if (Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value) == Convert.ToInt32(dataGridView1.Rows[j].Cells[1].Value))
                            {
                                dataGridView1.Rows[j].Cells[e.ColumnIndex].Value = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                            }
                        }
                    }
                    break;
                case 6:
                case 13:
                    {
                        if (Information.IsNumeric(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value))
                        {
                            // NOTHING
                        }
                        else
                        {
                            MessageBox.Show("You must enter relevant values for all fields before continuing.", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        public override void Dynamic_CTRLs(DataGridView dataGridView1)
        {
            int x;
            int y;
            int i;
            int j;
            int Width;
            int Height;

            Rectangle rect; // STORES A SET OF FOUR INTEGERS
            TabControl tab = (TabControl)dataGridView1.Parent.Parent;
            tab.TabPages[gridNum - 1].Show();

            for (j = 0; j <= dataGridView1.ColumnCount - 1; j++)
            {
                switch (j)
                {
                    case 10:
                        {
                            dataGridView1.HorizontalScrollingOffset = 0;
                            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                            {
                                var gridDte = new DateTimePicker();
                                gridDte.Name = "dta" + i;
                                try
                                {
                                    gridDte.Value = Convert.ToDateTime(dataGridView1.Rows[i].Cells[j].Value.ToString());   // SET EQUAL TO CORRESPONDING DTGV CELL TEXT (CHECK)
                                }
                                catch (Exception ex)
                                {
                                    gridDte.Value = DateTime.Today;
                                }
                                gridDte.Tag = dataGridView1;
                                gridDte.Format = DateTimePickerFormat.Custom;
                                gridDte.CustomFormat = "MMM yyyy";
                                dataGridView1.Controls.Add(gridDte);
                                // POSITION
                                rect = dataGridView1.GetCellDisplayRectangle(j, i, false);
                                x = rect.X;
                                y = rect.Y;
                                Width = rect.Width;
                                Height = rect.Height;
                                // BIND TO CELL
                                gridDte.SetBounds(x, y, Width, Height);
                                gridDte.Visible = true;
                                gridDte.Enabled = false;
                                // ADD HANDLER
                                gridDte.Enter += new EventHandler(HandleDynamicDate_Enter);
                                gridDte.Leave += new EventHandler(HandleDynamicDate_Leave);
                            }
                        }
                        break;
                    case 11:
                        {
                            dataGridView1.HorizontalScrollingOffset = 500;
                            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                            {
                                var gridDte = new DateTimePicker();
                                gridDte.Name = "dtr" + i;
                                try
                                {
                                    gridDte.Value = Convert.ToDateTime(dataGridView1.Rows[i].Cells[j].Value.ToString());   // SET EQUAL TO CORRESPONDING DTGV CELL TEXT (CHECK)
                                }
                                catch (Exception ex)
                                {
                                    gridDte.Value = DateTime.Today;
                                }
                                gridDte.Tag = dataGridView1;
                                gridDte.Format = DateTimePickerFormat.Custom;
                                gridDte.CustomFormat = "MMM yyyy";
                                dataGridView1.Controls.Add(gridDte);
                                // POSITION
                                rect = dataGridView1.GetCellDisplayRectangle(j, i, false);
                                x = rect.X;
                                y = rect.Y;
                                Width = rect.Width;
                                Height = rect.Height;
                                // BIND TO CELL
                                gridDte.SetBounds(x, y, Width, Height);
                                gridDte.Visible = true;
                                gridDte.Enabled = false;
                                // ADD HANDLER
                                gridDte.Enter += new EventHandler(HandleDynamicDate_Enter);
                                gridDte.Leave += new EventHandler(HandleDynamicDate_Leave);
                            }
                        }
                        break;
                    case 26:
                        {
                            dataGridView1.HorizontalScrollingOffset = 500;
                            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                            {
                                var gridDte = new DateTimePicker();
                                gridDte.Name = "dtb" + i;
                                try
                                {
                                    gridDte.Value = Convert.ToDateTime(dataGridView1.Rows[i].Cells[j].Value.ToString());   // SET EQUAL TO CORRESPONDING DTGV CELL TEXT (CHECK)
                                }
                                catch (Exception ex)
                                {
                                    gridDte.Value = DateTime.Today;
                                }
                                gridDte.Tag = dataGridView1;
                                gridDte.Format = DateTimePickerFormat.Custom;
                                gridDte.CustomFormat = "MMM yyyy";
                                dataGridView1.Controls.Add(gridDte);
                                // POSITION
                                rect = dataGridView1.GetCellDisplayRectangle(j, i, false);
                                x = rect.X;
                                y = rect.Y;
                                Width = rect.Width;
                                Height = rect.Height;
                                // BIND TO CELL
                                gridDte.SetBounds(x, y, Width, Height);
                                gridDte.Visible = true;
                                gridDte.Enabled = false;
                                // ADD HANDLER
                                gridDte.Enter += new EventHandler(HandleDynamicDate_Enter);
                                gridDte.Leave += new EventHandler(HandleDynamicDate_Leave);
                            }
                        }
                        break;
                }
            }
            dataGridView1.HorizontalScrollingOffset = 0;
        }

        public override void Move_CTRLs(DataGridView dataGridView1)
        {
            int n;
            int c;
            int x;
            int y;
            int z;
            int width;
            int height;
            Rectangle rect;

            if (dataGridView1.RowCount == 0) return;

            for (n = 0; n <= dataGridView1.RowCount - 1; n++)
            {
                for (c = 0; c <= dataGridView1.ColumnCount - 1; c++)
                {
                    //FIND & MOVE ALL DYNAMIC CONTROLS
                    foreach (Control ctrl in dataGridView1.Controls)
                    {
                        if (ctrl.Name == "dta" + n || ctrl.Name == "dtr" + n || ctrl.Name == "dtb" + n)
                        {
                            switch (ctrl.Name.Substring(0, 3))
                            {
                                case "dta":
                                    {
                                        rect = dataGridView1.GetCellDisplayRectangle(10, n, false);
                                        x = rect.X;
                                        y = rect.Y;
                                        width = rect.Width;
                                        height = rect.Height;

                                        ctrl.SetBounds(x, y, width, height);
                                        ctrl.Visible = true;
                                    }
                                    break;
                                case "dtb":
                                    {
                                        rect = dataGridView1.GetCellDisplayRectangle(26, n, false);
                                        x = rect.X;
                                        y = rect.Y;
                                        width = rect.Width;
                                        height = rect.Height;

                                        ctrl.SetBounds(x, y, width, height);
                                        ctrl.Visible = true;
                                    }
                                    break;
                                case "dtr":
                                    {
                                        rect = dataGridView1.GetCellDisplayRectangle(11, n, false);
                                        x = rect.X;
                                        y = rect.Y;
                                        width = rect.Width;
                                        height = rect.Height;

                                        ctrl.SetBounds(x, y, width, height);
                                        ctrl.Visible = true;
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
        }
        public override void HandleDynamicDate_Enter(object sender, EventArgs e)
        {
            DateTimePicker dtePck = (DateTimePicker)sender;
            DataGridView dataGridView1 = (DataGridView)dtePck.Tag;
            int Diff;
            int rowNum = 0;
            string name = "xxx";

            try
            {
                Diff = dtePck.Name.Length - name.Trim().Length;
                rowNum = Convert.ToInt32(dtePck.Name.Substring(dtePck.Name.Length - Diff, Diff));
            }
            catch (Exception ex)
            {
            }
            switch (dtePck.Name.Substring(0, 3))
            {
                case "dta":
                    {
                        dataGridView1.CurrentCell = dataGridView1.Rows[rowNum].Cells[10];
                    }
                    break;
                case "dtr":
                    {
                        dataGridView1.CurrentCell = dataGridView1.Rows[rowNum].Cells[11];
                    }
                    break;
                case "dtb":
                    {
                        dataGridView1.CurrentCell = dataGridView1.Rows[rowNum].Cells[26];
                    }
                    break;
            }
        }
        public override void CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (terminate > 0) return;

            string title = "TINUUM SOFTWARE";
            DataGridView dataGridView1 = (DataGridView)sender;
            int j;

            dataGridView1.Tag = dataGridView1.Parent.Parent.Parent;

            Form frm = (Form)dataGridView1.Tag;
            if (frm.ActiveControl.Name == "btnAdd" || frm.ActiveControl.Name == "btnDelete" || frm.ActiveControl.Name == "btnCancel" || frm.ActiveControl.Name == "btnSub") return;

            // GET AGE OF BUILDNIG
            try
            {
                switch (e.ColumnIndex)
                {
                    case 19:
                        {
                            var switchExpr = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                            switch (switchExpr)
                            {
                                case "Configure":
                                    {
                                        FormConfigure_Payor frmDetail = new FormConfigure_Payor();
                                        frmDetail.Show(dataGridView1);
                                    }
                                    break;
                                case "Detail":
                                    {
                                        dtlRoster_Collection frmDetail = new dtlRoster_Collection();
                                        frmDetail.Show(dataGridView1);
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    case 21:
                        {
                            var switchExpr = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                            switch (switchExpr)
                            {
                                case "Configure":
                                    {
                                        FormConfigure_PDPM frmDetail = new FormConfigure_PDPM();
                                        frmDetail.Show(dataGridView1);
                                    }
                                    break;
                                case "Detail":
                                    {
                                        dtlRoster_Collection frmDetail = new dtlRoster_Collection();
                                        frmDetail.Show(dataGridView1);
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    case 23:
                        {
                            var switchExpr = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                            switch (switchExpr)
                            {
                                case "Configure":
                                    {
                                        FormConfigure_Assessment frmDetail = new FormConfigure_Assessment();
                                        frmDetail.Show(dataGridView1);
                                    }
                                    break;
                                case "Detail":
                                    {
                                        dtlRoster_Collection frmDetail = new dtlRoster_Collection();
                                        frmDetail.Show(dataGridView1);
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    case 11:
                        {
                            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null) return;

                            DateTime date1 = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[11].Value);
                            DateTime date2 = DateTime.Today;
                            int result = DateTime.Compare(date1, date2);

                            if (result > 0)
                            {
                                MessageBox.Show("Retry. Available date must be before today's date.", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                dataGridView1.Rows[e.RowIndex].Cells[11].Value = null;

                                dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[11];
                            }
                        }
                        break;
                    case 26:
                        {
                            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null) return;

                            DateTime date1 = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[11].Value);
                            DateTime date2 = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[26].Value);
                            int result = DateTime.Compare(date1, date2);

                            if (result > 0)
                            {
                                MessageBox.Show("Retry. Reabsorb date must be greater than available date.", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                dataGridView1.Rows[e.RowIndex].Cells[26].Value = null;

                                dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[26];
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {

            }
        }
        public override void CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dataGridView1 = (DataGridView)sender;

            if (e.RowIndex == -1) return;
            if (e.ColumnIndex == -1) return;

            int Slct = dataGridView1.CurrentCell.RowIndex;
            int col = dataGridView1.CurrentCell.ColumnIndex;

            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].ReadOnly == true) return;

            DataGridView senderGrid = (DataGridView)sender;
            try
            {
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
                {
                    switch (e.ColumnIndex)
                    {
                        case 28:
                            {
                                dataGridView1.CurrentCell = dataGridView1.Rows[Slct].Cells[e.ColumnIndex - 1];
                                index = 27;
                                dataGridView1.Rows[Slct].Cells[col - 1].Value = "Detail";
                                dtlRoster_Collection_Figureless frmDetail = new dtlRoster_Collection_Figureless();
                                frmDetail.Show(dataGridView1);
                            }
                            break;
                        case 30:
                            {
                                dataGridView1.CurrentCell = dataGridView1.Rows[Slct].Cells[e.ColumnIndex - 1];
                                index = 29;
                                dataGridView1.Rows[Slct].Cells[col - 1].Value = "Detail";
                                dtlRoster_Collection_Figureless frmDetail = new dtlRoster_Collection_Figureless();
                                frmDetail.Show(dataGridView1);
                            }
                            break;
                        default:
                            {
                                {
                                    dataGridView1.CurrentCell = dataGridView1.Rows[Slct].Cells[e.ColumnIndex - 1];
                                    dataGridView1.Rows[Slct].Cells[col - 1].Value = "";
                                    dataGridView1.Rows[Slct].Cells[col - 1].Value = "Detail";
                                }
                            }
                            break;
                    }

                }
            }
            catch (Exception ex)
            {
            }
        }
        public void DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            DataGridView dataGridView1 = (DataGridView)sender;

            switch (e.ColumnIndex)
            {
                case 27:
                case 29:
                    {
                        if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Detail") return;
                    }
                    break;
            }
        }
    }
}
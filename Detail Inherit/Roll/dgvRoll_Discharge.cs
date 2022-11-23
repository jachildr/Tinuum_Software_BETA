using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tinuum_Software_BETA.Icon_Masters;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualBasic;
using Tinuum_Software_BETA.Popups.Roll;

namespace Tinuum_Software_BETA.Detail_Inherit.Roll
{
    class dgvRoll_Discharge : Detail_Inherit.Roll.dgvRoll_Clinical
    {
        protected string tbl_Detail_DT = "dtbRollDetail_Downtime";
        protected string tbl_Dynamic_DT = "dtbRollDynamic_Downtime";
        protected string tbl_dtlDyna_DT = "dtbRollDetailDynamic_Downtime";
        protected string tbl_Detail_PC = "dtbRollDetail_PLacement";
        protected string tbl_Dynamic_PC = "dtbRollDynamic_Placement";
        protected string tbl_dtlDyna_PC = "dtbRollDetailDynamic_Placement";
        protected string tbl_Detail_RM = "dtbRollDetail_Maintenance";
        protected string tbl_Dynamic_RM = "dtbRollDynamic_Maintenance";
        protected string tbl_dtlDyna_RM = "dtbRollDetailDynamic_Maintenance";
        protected string tbl_pctDetail_DT = "dtbRollDetailPct_Downtime";
        protected string tbl_pctDetail_PC = "dtbRollDetailPct_Placement";
        protected string tbl_pctDetail_RM = "dtbRollDetailPct_Maintenance";

        public dgvRoll_Discharge()
        {
            tbl_Name = "dtbRollVerse_Discharge";
        }

        public virtual void ClinicLoad(DataGridView dataGridView1)
        {
            int i;
            int r;
            int j;
            // DGV CTRLS 
            var cmbo1 = new DataGridViewComboBoxColumn();
            var cmbo2 = new DataGridViewComboBoxColumn();
            var cmbo3 = new DataGridViewComboBoxColumn();
            
            var btn1 = new DataGridViewButtonColumn();
            var btn2 = new DataGridViewButtonColumn();
            var btn3 = new DataGridViewButtonColumn();
            var btn4 = new DataGridViewButtonColumn();
            var btn5 = new DataGridViewButtonColumn();
            var btn6 = new DataGridViewButtonColumn();
            
            terminate = 1;

            SQL_Verse.ExecQuery("SELECT * FROM " + tbl_Name + ";");

            // COLUMN CONTROLS
            {
                // ADD SPECS FOR COMBOBOX1
                cmbo1.Items.Add("");
                cmbo1.Items.Add("Days");
                cmbo1.Items.Add("% ALOS");
                cmbo1.FlatStyle = FlatStyle.Popup;
                cmbo1.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                cmbo1.DisplayStyleForCurrentCellOnly = false;

                // ADD SPECS FOR COMBOBOX2
                cmbo2.Items.Add("");
                cmbo2.Items.Add("$ Amount");
                cmbo2.Items.Add("$/SqFt");
                cmbo2.Items.Add("% ALOS Income");
                cmbo2.FlatStyle = FlatStyle.Popup;
                cmbo2.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                cmbo2.DisplayStyleForCurrentCellOnly = false;

                // ADD SPECS FOR COMBOBOX3
                cmbo3.Items.Add("");
                cmbo3.Items.Add("$ Amount");
                cmbo3.Items.Add("$/SqFt");
                cmbo3.Items.Add("% ALOS Income");
                cmbo3.FlatStyle = FlatStyle.Popup;
                cmbo3.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                cmbo3.DisplayStyleForCurrentCellOnly = false;

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

                // ADD SPECS FOR BUTTON6
                btn6.UseColumnTextForButtonValue = true;
                btn6.Text = "_";
                btn6.FlatStyle = FlatStyle.System;
                btn6.DefaultCellStyle.Alignment = (DataGridViewContentAlignment)ContentAlignment.BottomRight;
                btn6.DefaultCellStyle.Font = new Font("Arial", 6, FontStyle.Bold);
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
                                case 5:
                                    {
                                        dataGridView1.Columns.Add(btn1);
                                    }
                                    break;
                                case 7:
                                    {
                                        dataGridView1.Columns.Add(btn2);
                                    }
                                    break;
                                case 10:
                                    {
                                        dataGridView1.Columns.Add(btn3);
                                    }
                                    break;
                                case 12:
                                    {
                                        dataGridView1.Columns.Add(btn4);
                                    }
                                    break;
                                case 15:
                                    {
                                        dataGridView1.Columns.Add(btn5);
                                    }
                                    break;
                                case 17:
                                    {
                                        dataGridView1.Columns.Add(btn6);
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
                                case 3:
                                    {
                                        dataGridView1.Columns.Add(cmbo1);
                                    }
                                    break;
                                case 8:
                                    {
                                        dataGridView1.Columns.Add(cmbo2);
                                    }
                                    break;
                                case 13:
                                    {
                                        dataGridView1.Columns.Add(cmbo3);
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

            // SET DGV NUMBER OF ROWS BY REFRESHING TBL ROWCOUNT 
            SQL_Verse.ExecQuery("SELECT * FROM " + tbl_Name + ";");
            dataGridView1.RowCount = SQL_Verse.RecordCount;

            // FILL DATAGRID FROM DATA TABLE
            for (r = 0; r <= SQL_Verse.RecordCount - 1; r++)
            {
                for (i = 0; i <= Col_Count - 1; i++)
                {
                    dataGridView1.Rows[r].Cells[i].Value = SQL_Verse.DBDT.Rows[r][i];
                }
            }

            // MAKE ROWS IN COLUMN 1 READ ONLY AND NUMBER IN ORDER
            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                try
                {
                    dataGridView1.Rows[i].Cells[1].Value = i + 1;
                    dataGridView1.Rows[i].Cells[1].ReadOnly = true;
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
            dataGridView1.Columns[0].Visible = false;

            // ENABLE / DISABLE CELLS
            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                for (j = 0; j <= dataGridView1.ColumnCount - 1; j++)
                {
                    if (new int[] { 3, 8, 13 }.Contains(j))
                    {
                        switch (dataGridView1.Rows[i].Cells[j].Value.ToString())
                        {
                            case "":
                                {
                                    for (r = 1; r <= 4; r++)
                                    {
                                        dataGridView1.Rows[i].Cells[j + r].ReadOnly = true;
                                        dataGridView1.Rows[i].Cells[j + r].Style.SelectionBackColor = SystemColors.Control;
                                        dataGridView1.Rows[i].Cells[j + r].Style.SelectionForeColor = SystemColors.ControlDark;
                                        dataGridView1.Rows[i].Cells[j + r].Style.BackColor = SystemColors.Control;
                                        dataGridView1.Rows[i].Cells[j + r].Style.ForeColor = SystemColors.ControlDark;
                                    }
                                }
                                break;
                            case "% ALOS":
                            case "% ALOS Income":
                                {
                                    for (r = 3; r <= 4; r++)
                                    {
                                        dataGridView1.Rows[i].Cells[j + r].ReadOnly = true;
                                        dataGridView1.Rows[i].Cells[j + r].Style.SelectionBackColor = SystemColors.Control;
                                        dataGridView1.Rows[i].Cells[j + r].Style.SelectionForeColor = SystemColors.ControlDark;
                                        dataGridView1.Rows[i].Cells[j + r].Style.BackColor = SystemColors.Control;
                                        dataGridView1.Rows[i].Cells[j + r].Style.ForeColor = SystemColors.ControlDark;
                                    }
                                }
                                break;
                            default:
                                {
                                    for (r = 1; r <= 2; r++)
                                    {
                                        dataGridView1.Rows[i].Cells[j + r].ReadOnly = true;
                                        dataGridView1.Rows[i].Cells[j + r].Style.SelectionBackColor = SystemColors.Control;
                                        dataGridView1.Rows[i].Cells[j + r].Style.SelectionForeColor = SystemColors.ControlDark;
                                        dataGridView1.Rows[i].Cells[j + r].Style.BackColor = SystemColors.Control;
                                        dataGridView1.Rows[i].Cells[j + r].Style.ForeColor = SystemColors.ControlDark;
                                    }
                                }
                                break;
                        }
                    }
                }
            }

            // MAKE 1ST COLUMN READ ONLY
            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                dataGridView1.Rows[i].Cells[1].ReadOnly = true;
            }

            // MAKE 1ST COLUMN STATIC WHITE
            dataGridView1.Columns[1].DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView1.Columns[1].DefaultCellStyle.SelectionForeColor = Color.Black;

            // COLUMN ALIGNMENT & WIDTH
            dataGridView1.Columns[1].Width = 50;
            dataGridView1.Columns[2].Width = 150;

            for (i = 3; i <= dataGridView1.ColumnCount - 1; i++)
            {
                dataGridView1.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridView1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            // CALL PROCEDURES
            this.Percent_Change(dataGridView1);

            terminate = 0;
        }

        public virtual void Percent_Change(DataGridView dataGridView1)
        {
            int j;
            int i;
            string strNum;
            double intNum;

            // FORMAT FILLED DB DATA
            try
            {
                for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                {
                    for (j = 0; j <= dataGridView1.ColumnCount - 1; j++)
                    {
                        if (new int[] { 4, 9, 14 }.Contains(j))
                        {
                            if (dataGridView1.Rows[i].Cells[j].ReadOnly == false)
                            {
                                strNum = dataGridView1.Rows[i].Cells[j].Value.ToString();
                                if (Information.IsNumeric(strNum) == true)
                                {
                                    intNum = Convert.ToDouble(strNum);
                                    dataGridView1.Rows[i].Cells[j].Value = String.Format("{0:p}", intNum);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public virtual void InsertUser(DataGridView dataGridView1)
        {
            int i;
            int y;
            string Title = "TINUUM SOFTWARE";
            int cRight = 3;
            string btnString = "(b)";
            int num;
            int count;

            add = 1;
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
            string cmdInsert1 = "INSERT INTO " + tbl_Detail_DT + " (ID_Num) VALUES (" + num + ");";
            string cmdInsert2 = "INSERT INTO " + tbl_Dynamic_DT + " (ID_Num) VALUES (" + num + ");";
            string cmdInsert3 = "INSERT INTO " + tbl_dtlDyna_DT + " (ID_Num) VALUES (" + num + ");";
            string cmdInsert4 = "INSERT INTO " + tbl_Detail_PC + " (ID_Num) VALUES (" + num + ");";
            string cmdInsert5 = "INSERT INTO " + tbl_Dynamic_PC + " (ID_Num) VALUES (" + num + ");";
            string cmdInsert6 = "INSERT INTO " + tbl_dtlDyna_PC + " (ID_Num) VALUES (" + num + ");";
            string cmdInsert7 = "INSERT INTO " + tbl_Detail_RM + " (ID_Num) VALUES (" + num + ");";
            string cmdInsert8 = "INSERT INTO " + tbl_Dynamic_RM + " (ID_Num) VALUES (" + num + ");";
            string cmdInsert9 = "INSERT INTO " + tbl_dtlDyna_RM + " (ID_Num) VALUES (" + num + ");";
            string cmdInsert10 = "INSERT INTO " + tbl_pctDetail_DT + " (ID_Num) VALUES (" + num + ");";
            string cmdInsert11 = "INSERT INTO " + tbl_pctDetail_PC + " (ID_Num) VALUES (" + num + ");";
            string cmdInsert12 = "INSERT INTO " + tbl_pctDetail_RM + " (ID_Num) VALUES (" + num + ");";
            SQL_Verse.ExecQuery(cmdInsert1);
            SQL_Verse.ExecQuery(cmdInsert2);
            SQL_Verse.ExecQuery(cmdInsert3);
            SQL_Verse.ExecQuery(cmdInsert4);
            SQL_Verse.ExecQuery(cmdInsert5);
            SQL_Verse.ExecQuery(cmdInsert6);
            SQL_Verse.ExecQuery(cmdInsert7);
            SQL_Verse.ExecQuery(cmdInsert8);
            SQL_Verse.ExecQuery(cmdInsert9);
            SQL_Verse.ExecQuery(cmdInsert10);
            SQL_Verse.ExecQuery(cmdInsert11);
            SQL_Verse.ExecQuery(cmdInsert12);

            // CALL METHODS
            this.Add_Source(dataGridView1);
            this.ClinicLoad(dataGridView1);

            add = 0;
        }

        public override void Delete_Command(DataGridView dataGridView1)
        {
            int r;
            string Title = "TINUUM SOFTWARE";

            DialogResult prompt = MessageBox.Show("Are you sure you want to permanently delete?", Title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            try
            {
                if (prompt == DialogResult.Yes)
                {
                    r = dataGridView1.CurrentCell.RowIndex;

                    //DELETE SELECTED ROWS FROM TABLE
                    SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[r].Cells[0].Value.ToString());
                    SQL_Verse.ExecQuery("DELETE FROM " + tbl_Name + " WHERE ID_Num=@PrimKey;");

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
        public override void UpdateSQL(DataGridView dataGridView1)
        {
            int i;
            int y;
            int j;
            string cmdUpdate;
            string title = "TINUUM SOFTWARE";
            TabControl tab;

            dataGridView1.Tag = dataGridView1.Parent.Parent.Parent;
            Form frm = (Form)dataGridView1.Tag;
            tab = frm.Controls["tabCtrl"] as TabControl;
            //tab.TabPages[1].Show();

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
                                escapeNum = 1;
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
                    if (new int[] { 4, 6, 9, 11, 14, 16 }.Contains(i))
                    {
                        // SUBMIT TO MAJOR DATA TABLE
                        if (dataGridView1.Rows[y].Cells[i].ReadOnly == false)
                        {
                            // ADD PARAMS
                            switch (i)
                            {
                                case 4:
                                case 9:
                                case 14:
                                    {
                                        string percent = dataGridView1.Rows[y].Cells[i].Value.ToString();
                                        if (Information.IsNumeric(percent.Substring(0, percent.Length - 1)))
                                        {
                                            SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                            SQL_Verse.AddParam("@vals", myMethods.ToDecimal(dataGridView1.Rows[y].Cells[i].Value.ToString()));
                                        }
                                    }
                                    break;
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
                                if (new int[] { 4, 9, 14 }.Contains(i))
                                {
                                    string percent = dataGridView1.Rows[y].Cells[i].Value.ToString(); // SUBMIT VALUES OF NEXT CELL OVER
                                    if (Information.IsNumeric(percent.Substring(0, percent.Length - 1)))
                                    {
                                        for (j = 1; j <= myMethods.Period * Mos_Const; j++)
                                        {
                                            switch (i)
                                            {
                                                case 4:
                                                    {
                                                        SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                        SQL_Verse.AddParam("@vals", myMethods.ToDecimal(dataGridView1.Rows[y].Cells[i].Value.ToString()));
                                                        string header = "month" + j;
                                                        string cmdUpdate1 = "UPDATE " + tbl_pctDetail_DT + " SET " + header + "=@vals WHERE ID_Num=@PrimKey;";
                                                        SQL_Verse.ExecQuery(cmdUpdate1);
                                                    }
                                                    break;
                                                case 9:
                                                    {
                                                        SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                        SQL_Verse.AddParam("@vals", myMethods.ToDecimal(dataGridView1.Rows[y].Cells[i].Value.ToString()));
                                                        string header = "month" + j;
                                                        string cmdUpdate2 = "UPDATE " + tbl_pctDetail_RM + " SET " + header + "=@vals WHERE ID_Num=@PrimKey;";
                                                        SQL_Verse.ExecQuery(cmdUpdate2);
                                                    }
                                                    break;
                                                case 14:
                                                    {
                                                        SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                        SQL_Verse.AddParam("@vals", myMethods.ToDecimal(dataGridView1.Rows[y].Cells[i].Value.ToString()));
                                                        string header = "month" + j;
                                                        string cmdUpdate3 = "UPDATE " + tbl_pctDetail_PC + " SET " + header + "=@vals WHERE ID_Num=@PrimKey;";
                                                        SQL_Verse.ExecQuery(cmdUpdate3);
                                                    }
                                                    break;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    switch (i)
                                    {
                                        case 6:
                                            {
                                                if (Information.IsNumeric(dataGridView1.Rows[y].Cells[i].Value))
                                                {
                                                    for (j = 1; j <= myMethods.Period * Mos_Const; j++)
                                                    {
                                                        SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                        SQL_Verse.AddParam("@vals", dataGridView1.Rows[y].Cells[i].Value);
                                                        string header = "month" + j;
                                                        string cmdUpdate1 = "UPDATE " + tbl_Detail_DT + " SET " + header + "=@vals WHERE ID_Num=@PrimKey;";
                                                        SQL_Verse.ExecQuery(cmdUpdate1);
                                                    }
                                                    // DYNAMIC DEFAULT CHANGE
                                                    SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                    SQL_Verse.AddParam("@val1", 1);
                                                    SQL_Verse.AddParam("@val2", DBNull.Value);
                                                    string colName1 = "Choose";
                                                    string colName2 = "Selection";
                                                    string cmdUpdate2 = "UPDATE " + tbl_Dynamic_DT + " SET " + colName1 + "=@val1, " + colName2 + "=@val2 WHERE ID_Num=@PrimKey;";
                                                    SQL_Verse.ExecQuery(cmdUpdate2);
                                                }
                                            }
                                            break;
                                        case 11:
                                            {
                                                if (Information.IsNumeric(dataGridView1.Rows[y].Cells[i].Value))
                                                {
                                                    for (j = 1; j <= myMethods.Period * Mos_Const; j++)
                                                    {
                                                        SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                        SQL_Verse.AddParam("@vals", dataGridView1.Rows[y].Cells[i].Value);
                                                        string header = "month" + j;
                                                        string cmdUpdate1 = "UPDATE " + tbl_Detail_RM + " SET " + header + "=@vals WHERE ID_Num=@PrimKey;";
                                                        SQL_Verse.ExecQuery(cmdUpdate1);
                                                    }
                                                    // DYNAMIC DEFAULT CHANGE
                                                    SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                    SQL_Verse.AddParam("@val1", 1);
                                                    SQL_Verse.AddParam("@val2", DBNull.Value);
                                                    string colName1 = "Choose";
                                                    string colName2 = "Selection";
                                                    string cmdUpdate2 = "UPDATE " + tbl_Dynamic_RM + " SET " + colName1 + "=@val1, " + colName2 + "=@val2 WHERE ID_Num=@PrimKey;";
                                                    SQL_Verse.ExecQuery(cmdUpdate2);
                                                }
                                            }
                                            break;
                                        case 16:
                                            {
                                                if (Information.IsNumeric(dataGridView1.Rows[y].Cells[i].Value))
                                                {
                                                    for (j = 1; j <= myMethods.Period * Mos_Const; j++)
                                                    {
                                                        SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                        SQL_Verse.AddParam("@vals", dataGridView1.Rows[y].Cells[i].Value);
                                                        string header = "month" + j;
                                                        string cmdUpdate1 = "UPDATE " + tbl_Detail_PC + " SET " + header + "=@vals WHERE ID_Num=@PrimKey;";
                                                        SQL_Verse.ExecQuery(cmdUpdate1);
                                                    }
                                                    // DYNAMIC DEFAULT CHANGE
                                                    SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                    SQL_Verse.AddParam("@val1", 1);
                                                    SQL_Verse.AddParam("@val2", DBNull.Value);
                                                    string colName1 = "Choose";
                                                    string colName2 = "Selection";
                                                    string cmdUpdate2 = "UPDATE " + tbl_Dynamic_PC + " SET " + colName1 + "=@val1, " + colName2 + "=@val2 WHERE ID_Num=@PrimKey;";
                                                    SQL_Verse.ExecQuery(cmdUpdate2);
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
                if (tab.SelectedIndex == 1)
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
            int cRight = 3;
            string btnString = "(b)";
            string cmdUpdate;

            grid = 2;

            dataGridView1.Tag = dataGridView1.Parent.Parent.Parent;
            Form frm = (Form)dataGridView1.Tag;

            this.Query_Header(dataGridView1);

            if (grid == 1)
            {
                cnclPrompt = MessageBox.Show("Are you sure? Any unsubmitted data will be lost.", Title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                Rslt_Cncl = cnclPrompt.ToString();
            }

            if (Rslt_Cncl == "No") return;
            if (grid > 1) cnclPrompt = DialogResult.Yes;

            if (cnclPrompt == DialogResult.Yes)
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
                                SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[i].Cells[0].Value.ToString());
                                SQL_Verse.ExecQuery("DELETE FROM " + tbl_Name + " WHERE ID_Num=@PrimKey;");
                            }
                        }
                    }

                    for (y = 0; y <= dataGridView1.RowCount - 1; y++)
                    {
                        for (i = 1; i <= dataGridView1.ColumnCount - 1; i++)
                        {
                            if (new int[] { 4, 6, 9, 11, 14, 16 }.Contains(i))
                            {
                                // UPDATE STATEMENT FOR DETAIL IF NUMERIC
                                if (dataGridView1.Rows[y].Cells[i].ReadOnly == false)
                                {
                                    if (new int[] { 4, 9, 14 }.Contains(i))
                                    {
                                        string percent = dataGridView1.Rows[y].Cells[i].Value.ToString(); // SUBMIT VALUES OF NEXT CELL OVER
                                        if (Information.IsNumeric(percent.Substring(0, percent.Length - 1)))
                                        {
                                            for (j = 1; j <= myMethods.Period * Mos_Const; j++)
                                            {
                                                switch (i)
                                                {
                                                    case 4:
                                                        {
                                                            SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                            SQL_Verse.AddParam("@vals", myMethods.ToDecimal(dataGridView1.Rows[y].Cells[i].Value.ToString()));
                                                            string header = "month" + j;
                                                            string cmdUpdate1 = "UPDATE " + tbl_pctDetail_DT + " SET " + header + "=@vals WHERE ID_Num=@PrimKey;";
                                                            SQL_Verse.ExecQuery(cmdUpdate1);
                                                        }
                                                        break;
                                                    case 9:
                                                        {
                                                            SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                            SQL_Verse.AddParam("@vals", myMethods.ToDecimal(dataGridView1.Rows[y].Cells[i].Value.ToString()));
                                                            string header = "month" + j;
                                                            string cmdUpdate2 = "UPDATE " + tbl_pctDetail_RM + " SET " + header + "=@vals WHERE ID_Num=@PrimKey;";
                                                            SQL_Verse.ExecQuery(cmdUpdate2);
                                                        }
                                                        break;
                                                    case 14:
                                                        {
                                                            SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                            SQL_Verse.AddParam("@vals", myMethods.ToDecimal(dataGridView1.Rows[y].Cells[i].Value.ToString()));
                                                            string header = "month" + j;
                                                            string cmdUpdate3 = "UPDATE " + tbl_pctDetail_PC + " SET " + header + "=@vals WHERE ID_Num=@PrimKey;";
                                                            SQL_Verse.ExecQuery(cmdUpdate3);
                                                        }
                                                        break;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        switch (i)
                                        {
                                            case 6:
                                                {
                                                    if (Information.IsNumeric(dataGridView1.Rows[y].Cells[i].Value))
                                                    {
                                                        for (j = 1; j <= myMethods.Period * Mos_Const; j++)
                                                        {
                                                            SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                            SQL_Verse.AddParam("@vals", dataGridView1.Rows[y].Cells[i].Value);
                                                            string header = "month" + j;
                                                            string cmdUpdate1 = "UPDATE " + tbl_Detail_DT + " SET " + header + "=@vals WHERE ID_Num=@PrimKey;";
                                                            SQL_Verse.ExecQuery(cmdUpdate1);
                                                        }
                                                        // DYNAMIC DEFAULT CHANGE
                                                        SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                        SQL_Verse.AddParam("@val1", 1);
                                                        SQL_Verse.AddParam("@val2", DBNull.Value);
                                                        string colName1 = "Choose";
                                                        string colName2 = "Selection";
                                                        string cmdUpdate2 = "UPDATE " + tbl_Dynamic_DT + " SET " + colName1 + "=@val1, " + colName2 + "=@val2 WHERE ID_Num=@PrimKey;";
                                                        SQL_Verse.ExecQuery(cmdUpdate2);
                                                    }
                                                }
                                                break;
                                            case 11:
                                                {
                                                    if (Information.IsNumeric(dataGridView1.Rows[y].Cells[i].Value))
                                                    {
                                                        for (j = 1; j <= myMethods.Period * Mos_Const; j++)
                                                        {
                                                            SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                            SQL_Verse.AddParam("@vals", dataGridView1.Rows[y].Cells[i].Value);
                                                            string header = "month" + j;
                                                            string cmdUpdate1 = "UPDATE " + tbl_Detail_RM + " SET " + header + "=@vals WHERE ID_Num=@PrimKey;";
                                                            SQL_Verse.ExecQuery(cmdUpdate1);
                                                        }
                                                        // DYNAMIC DEFAULT CHANGE
                                                        SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                        SQL_Verse.AddParam("@val1", 1);
                                                        SQL_Verse.AddParam("@val2", DBNull.Value);
                                                        string colName1 = "Choose";
                                                        string colName2 = "Selection";
                                                        string cmdUpdate2 = "UPDATE " + tbl_Dynamic_RM + " SET " + colName1 + "=@val1, " + colName2 + "=@val2 WHERE ID_Num=@PrimKey;";
                                                        SQL_Verse.ExecQuery(cmdUpdate2);
                                                    }
                                                }
                                                break;
                                            case 16:
                                                {
                                                    if (Information.IsNumeric(dataGridView1.Rows[y].Cells[i].Value))
                                                    {
                                                        for (j = 1; j <= myMethods.Period * Mos_Const; j++)
                                                        {
                                                            SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                            SQL_Verse.AddParam("@vals", dataGridView1.Rows[y].Cells[i].Value);
                                                            string header = "month" + j;
                                                            string cmdUpdate1 = "UPDATE " + tbl_Detail_PC + " SET " + header + "=@vals WHERE ID_Num=@PrimKey;";
                                                            SQL_Verse.ExecQuery(cmdUpdate1);
                                                        }
                                                        // DYNAMIC DEFAULT CHANGE
                                                        SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                        SQL_Verse.AddParam("@val1", 1);
                                                        SQL_Verse.AddParam("@val2", DBNull.Value);
                                                        string colName1 = "Choose";
                                                        string colName2 = "Selection";
                                                        string cmdUpdate2 = "UPDATE " + tbl_Dynamic_PC + " SET " + colName1 + "=@val1, " + colName2 + "=@val2 WHERE ID_Num=@PrimKey;";
                                                        SQL_Verse.ExecQuery(cmdUpdate2);
                                                    }
                                                }
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                // CLOSE FOR BOTH CASES
                if (grid == 2)
                {
                    frm.Close();
                    return;
                }
            }
            else
            {
                return;
            }
        }

        public virtual void CellEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView1 = (DataGridView)sender;

            int i;
            int j;

            switch (e.ColumnIndex)
            {
                case 3:
                case 8:
                case 13:
                    {
                        switch (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)
                        {
                            case null:
                                {
                                    // CLEAR CONTENTS OF IRRELEVANT CELLS
                                    for (j = 0; j <= dataGridView1.ColumnCount - 1; j++)
                                    {
                                        if (new int[] { e.ColumnIndex + 1, e.ColumnIndex + 2, e.ColumnIndex + 3, e.ColumnIndex + 4 }.Contains(j))
                                        {
                                            dataGridView1.Rows[e.RowIndex].Cells[j].Value = null;
                                            dataGridView1.Rows[e.RowIndex].Cells[j].ReadOnly = true;
                                            dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionBackColor = SystemColors.Control;
                                            dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionForeColor = SystemColors.ControlDark;
                                            dataGridView1.Rows[e.RowIndex].Cells[j].Style.BackColor = SystemColors.Control;
                                            dataGridView1.Rows[e.RowIndex].Cells[j].Style.ForeColor = SystemColors.ControlDark;
                                        }
                                    }
                                }
                                break;
                            case "% ALOS":
                            case "% ALOS Income":
                                {
                                    for (j = 0; j <= dataGridView1.ColumnCount - 1; j++)
                                    {
                                        if (new int[] { e.ColumnIndex + 1, e.ColumnIndex + 2 }.Contains(j))
                                        {
                                            dataGridView1.Rows[e.RowIndex].Cells[j].ReadOnly = false;
                                            dataGridView1.Rows[e.RowIndex].Cells[j].Style.BackColor = Color.White;
                                            dataGridView1.Rows[e.RowIndex].Cells[j].Style.ForeColor = Color.Black;
                                            dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionBackColor = SystemColors.Highlight;
                                            dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionForeColor = Color.White;
                                        }
                                    }
                                    // CLEAR CONTENTS OF IRRELEVANT CELLS
                                    for (j = 0; j <= dataGridView1.ColumnCount - 1; j++)
                                    {
                                        if (new int[] { e.ColumnIndex + 3, e.ColumnIndex + 4 }.Contains(j))
                                        {
                                            dataGridView1.Rows[e.RowIndex].Cells[j].Value = null;
                                            dataGridView1.Rows[e.RowIndex].Cells[j].ReadOnly = true;
                                            dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionBackColor = SystemColors.Control;
                                            dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionForeColor = SystemColors.ControlDark;
                                            dataGridView1.Rows[e.RowIndex].Cells[j].Style.BackColor = SystemColors.Control;
                                            dataGridView1.Rows[e.RowIndex].Cells[j].Style.ForeColor = SystemColors.ControlDark;
                                        }
                                    }
                                }
                                break;
                            case "Days":
                            case "$ Amount":
                            case "$/SqFt":
                                {
                                    for (j = 0; j <= dataGridView1.ColumnCount - 1; j++)
                                    {
                                        if (new int[] { e.ColumnIndex + 3, e.ColumnIndex + 4 }.Contains(j))
                                        {
                                            dataGridView1.Rows[e.RowIndex].Cells[j].ReadOnly = false;
                                            dataGridView1.Rows[e.RowIndex].Cells[j].Style.BackColor = Color.White;
                                            dataGridView1.Rows[e.RowIndex].Cells[j].Style.ForeColor = Color.Black;
                                            dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionBackColor = SystemColors.Highlight;
                                            dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionForeColor = Color.White;
                                        }
                                    }
                                    // CLEAR CONTENTS OF IRRELEVANT CELLS
                                    for (j = 0; j <= dataGridView1.ColumnCount - 1; j++)
                                    {
                                        if (new int[] { e.ColumnIndex + 1, e.ColumnIndex + 2 }.Contains(j))
                                        {
                                            dataGridView1.Rows[e.RowIndex].Cells[j].Value = null;
                                            dataGridView1.Rows[e.RowIndex].Cells[j].ReadOnly = true;
                                            dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionBackColor = SystemColors.Control;
                                            dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionForeColor = SystemColors.ControlDark;
                                            dataGridView1.Rows[e.RowIndex].Cells[j].Style.BackColor = SystemColors.Control;
                                            dataGridView1.Rows[e.RowIndex].Cells[j].Style.ForeColor = SystemColors.ControlDark;
                                        }
                                    }
                                }
                                break;
                            default:
                                break;
                        }

                    }
                    break;
                case 4:
                case 9:
                case 14:
                    {
                        if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == DBNull.Value || dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null) return;
                        var val = myMethods.ToPercent(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = val;
                    }
                    break;
                case 6:
                case 11:
                case 16:
                    {
                        if (!Information.IsNumeric(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value))
                        {
                            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        public virtual void CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dataGridView1 = (DataGridView)sender;

            int Slct = dataGridView1.CurrentCell.RowIndex;
            int col = dataGridView1.CurrentCell.ColumnIndex;

            DataGridView senderGrid = (DataGridView)sender;
            try
            {
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
                {
                    if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].ReadOnly == true) return;

                    switch (e.ColumnIndex)
                    {
                        case 5:
                        case 10:
                        case 15:
                            {        
                                dataGridView1.CurrentCell = dataGridView1.Rows[Slct].Cells[e.ColumnIndex - 1];
                                dtlRoll_Percent_Discharge frmDetail = new dtlRoll_Percent_Discharge();
                                frmDetail.Show(dataGridView1);   
                            }
                            break;
                        case 7:
                            {
                                dataGridView1.CurrentCell = dataGridView1.Rows[Slct].Cells[e.ColumnIndex - 1];
                                index = 6;
                                dtlRoll_Dynamic_Discharges frmDetail = new dtlRoll_Dynamic_Discharges();
                                frmDetail.Show(dataGridView1);
                            }
                            break;
                        case 12:
                            {
                                dataGridView1.CurrentCell = dataGridView1.Rows[Slct].Cells[e.ColumnIndex - 1];
                                index = 11;
                                dtlRoll_Dynamic_Discharges frmDetail = new dtlRoll_Dynamic_Discharges();
                                frmDetail.Show(dataGridView1);
                            }
                            break;
                        case 17:
                            {
                                dataGridView1.CurrentCell = dataGridView1.Rows[Slct].Cells[e.ColumnIndex - 1];
                                index = 16;
                                dtlRoll_Dynamic_Discharges frmDetail = new dtlRoll_Dynamic_Discharges();
                                frmDetail.Show(dataGridView1);
                            }
                            break;
                    }

                }
            }
            catch (Exception ex)
            {
            }
        }

        public override void CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //base.CellValueChanged(sender, e);
        }
        
    }
}

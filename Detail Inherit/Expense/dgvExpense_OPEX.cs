using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tinuum_Software_BETA.Icon_Masters;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualBasic;
using Tinuum_Software_BETA.Popups.Expense;
using Syncfusion.Windows.Forms.Tools;

namespace Tinuum_Software_BETA.Detail_Inherit.Expense
{
    class dgvExpense_OPEX : Detail_Inherit.Roll.dgvRoll_Clinical
    {
        public SQLControl SQL_Query = new SQLControl();
        public SQLControl SQL_Groups = new SQLControl();
        protected string tbl_dtlDyn_genRate = "dtbExpenseDetail_GeneralRate";
        protected string tbl_Dtl_genRate = "dtbExpenseDetailDynamic_GeneralRate";
        protected string tbl_Dyn_genRate = "dtbExpenseDynamic_GeneralRate";
        protected string tbl_dtlDyn_lRatio = "dtbExpenseDetail_LeftRatio";
        protected string tbl_Dtl_lRatio = "dtbExpenseDetailDynamic_LeftRatio";
        protected string tbl_Dyn_lRatio = "dtbExpenseDynamic_LeftRatio";
        protected string tbl_dtlDyn_rRatio = "dtbExpenseDetail_RightRatio";
        protected string tbl_Dtl_rRatio = "dtbExpenseDetailDynamic_RightRatio";
        protected string tbl_Dyn_rRatio = "dtbExpenseDynamic_RightRatio";
        protected string tbl_dtlDyn_Shift = "dtbExpenseDetail_Shift";
        protected string tbl_Dtl_Shift = "dtbExpenseDetailDynamic_Shift";
        protected string tbl_Dyn_Shift = "dtbExpenseDynamic_Shift";
        protected string tbl_dtlDyn_Staff = "dtbExpenseDetail_StaffQuantity";
        protected string tbl_Dtl_Staff = "dtbExpenseDetailDynamic_StaffQuantity";
        protected string tbl_Dyn_Staff = "dtbExpenseDynamic_StaffQuantity";
        protected string tbl_dtlDyn_Wage = "dtbExpenseDetail_Wage";
        protected string tbl_Dtl_Wage = "dtbExpenseDetailDynamic_Wage";
        protected string tbl_Dyn_Wage = "dtbExpenseDynamic_Wage";
        protected string tbl_Dtl_Payor = "dtbExpenseDetail_Payor"; //COLLECTIONS
        protected string tbl_Dtl_PDPM = "dtbExpenseDetail_PDPM"; //COLLECTIONS
        protected string tbl_dtlPct_Fixed = "dtbExpenseDetailPct_Fixed";
        protected string tbl_dtlPct_genRate = "dtbExpenseDetailPct_GeneralRate";
        protected string tbl_dtlPct_PPS = "dtbExpenseDetailPct_PPSRate";
        protected string tbl_Exp_Group = "dtbExpenseDetail_Groups";
        protected int Parent;
        protected int msg;
        public static int escapeEXP = 0;
        public static int _escapeEXP
        {
            get
            {
                return escapeEXP;
            }
        }
        public dgvExpense_OPEX()
        {
            tbl_Name = "dtbExpenseVerse";
        }
        public override void ClinicLoad(DataGridView dataGridView1)
        {
            int i;
            int r;
            int j;
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
           
            var btn1 = new DataGridViewButtonColumn();
            var btn2 = new DataGridViewButtonColumn();
            var btn3 = new DataGridViewButtonColumn();
            var btn4 = new DataGridViewButtonColumn();
            var btn5 = new DataGridViewButtonColumn();
            var btn6 = new DataGridViewButtonColumn();
            var btn7 = new DataGridViewButtonColumn();
            var btn8 = new DataGridViewButtonColumn();
            var btn9 = new DataGridViewButtonColumn();
            var btn10 = new DataGridViewButtonColumn();
            var btn11 = new DataGridViewButtonColumn();
            var btn12 = new DataGridViewButtonColumn();

            terminate = 1;
            // SORT TABLES
            {
                SQL_Verse.ExecQuery("SELECT * FROM " + tbl_Name + " ORDER BY Collection_Num ASC;");
                SQL_Verse.ExecQuery("SELECT * FROM " + tbl_dtlDyn_genRate + " ORDER BY Collection_Num ASC;");
                SQL_Verse.ExecQuery("SELECT * FROM " + tbl_Dtl_genRate + " ORDER BY Collection_Num ASC;");
                SQL_Verse.ExecQuery("SELECT * FROM " + tbl_Dyn_genRate + " ORDER BY Collection_Num ASC;");
                SQL_Verse.ExecQuery("SELECT * FROM " + tbl_dtlDyn_lRatio + " ORDER BY Collection_Num ASC;");
                SQL_Verse.ExecQuery("SELECT * FROM " + tbl_Dtl_lRatio + " ORDER BY Collection_Num ASC;");
                SQL_Verse.ExecQuery("SELECT * FROM " + tbl_Dyn_lRatio + " ORDER BY Collection_Num ASC;");
                SQL_Verse.ExecQuery("SELECT * FROM " + tbl_dtlDyn_rRatio + " ORDER BY Collection_Num ASC;");
                SQL_Verse.ExecQuery("SELECT * FROM " + tbl_Dtl_rRatio + " ORDER BY Collection_Num ASC;");
                SQL_Verse.ExecQuery("SELECT * FROM " + tbl_Dyn_rRatio + " ORDER BY Collection_Num ASC;");
                SQL_Verse.ExecQuery("SELECT * FROM " + tbl_dtlDyn_Shift + " ORDER BY Collection_Num ASC;");
                SQL_Verse.ExecQuery("SELECT * FROM " + tbl_Dtl_Shift + " ORDER BY Collection_Num ASC;");
                SQL_Verse.ExecQuery("SELECT * FROM " + tbl_Dyn_Shift + " ORDER BY Collection_Num ASC;");
                SQL_Verse.ExecQuery("SELECT * FROM " + tbl_dtlDyn_Staff + " ORDER BY Collection_Num ASC;");
                SQL_Verse.ExecQuery("SELECT * FROM " + tbl_Dtl_Staff + " ORDER BY Collection_Num ASC;");
                SQL_Verse.ExecQuery("SELECT * FROM " + tbl_Dyn_Staff + " ORDER BY Collection_Num ASC;");
                SQL_Verse.ExecQuery("SELECT * FROM " + tbl_dtlDyn_Wage + " ORDER BY Collection_Num ASC;");
                SQL_Verse.ExecQuery("SELECT * FROM " + tbl_Dtl_Wage + " ORDER BY Collection_Num ASC;");
                SQL_Verse.ExecQuery("SELECT * FROM " + tbl_Dyn_Wage + " ORDER BY Collection_Num ASC;");
                SQL_Verse.ExecQuery("SELECT * FROM " + tbl_Dtl_Payor + " ORDER BY Collection_Num ASC;");
                SQL_Verse.ExecQuery("SELECT * FROM " + tbl_Dtl_PDPM + " ORDER BY Collection_Num ASC;");
                SQL_Verse.ExecQuery("SELECT * FROM " + tbl_dtlPct_Fixed + " ORDER BY Collection_Num ASC;");
                SQL_Verse.ExecQuery("SELECT * FROM " + tbl_dtlPct_genRate + " ORDER BY Collection_Num ASC;");
                SQL_Verse.ExecQuery("SELECT * FROM " + tbl_dtlPct_PPS + " ORDER BY Collection_Num ASC;");
            }

            // COLUMN CONTROLS
            {
                // ADD SPECS FOR COMBOBOX1
                cmbo1.Items.Add("");
                cmbo1.Items.Add("General");
                cmbo1.Items.Add("Staffing");
                cmbo1.Items.Add("PPS Function");
                cmbo1.Items.Add("Distribution");
                cmbo1.FlatStyle = FlatStyle.Popup;
                cmbo1.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                cmbo1.DisplayStyleForCurrentCellOnly = false;

                // ADD SPECS FOR COMBOBOX2
                cmbo2.Items.Add("");
                cmbo2.Items.Add("$ Amount");
                cmbo2.Items.Add("$/PPD");
                cmbo2.Items.Add("$/Bed/Yr");
                cmbo2.Items.Add("$/SF/Yr");
                cmbo2.Items.Add("$/Medicare Days");
                cmbo2.Items.Add("$/Medicaid Days");
                cmbo2.Items.Add("$/Private Pay Days");
                cmbo2.Items.Add("$/Medicare MCO Days");
                cmbo2.Items.Add("$/Medicaid MCO Days");
                cmbo2.Items.Add("$/VA Days");
                cmbo2.Items.Add("$/Other Payor Days");
                cmbo2.Items.Add("% Net Revenue");
                cmbo2.Items.Add("% Medicare Revenue");
                cmbo2.Items.Add("% Medicaid Revenue");
                cmbo2.Items.Add("% Private Pay Revenue");
                cmbo2.Items.Add("% Medicare MCO Revenue");
                cmbo2.Items.Add("% Medicaid MCO Revenue");
                cmbo2.Items.Add("% VA Revenue");
                cmbo2.Items.Add("% Other Payor Revenue");
                cmbo2.FlatStyle = FlatStyle.Popup;
                cmbo2.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                cmbo2.DisplayStyleForCurrentCellOnly = false;

                // ADD SPECS FOR COMBOBOX3
                SQL_Query.ExecQuery("SELECT * FROM dtbExpenseSOC_Category;");
                cmbo3.DataSource = SQL_Query.DBDT;
                cmbo3.DisplayMember = "SOC Category";
                cmbo3.FlatStyle = FlatStyle.Popup;
                cmbo3.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                cmbo3.DisplayStyleForCurrentCellOnly = false;

                // ADD SPECS FOR COMBOBOX4
                cmbo4.Items.Add(""); //DYNAMIC
                cmbo4.FlatStyle = FlatStyle.Popup;
                cmbo4.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                cmbo4.DisplayStyleForCurrentCellOnly = false;

                // ADD SPECS FOR COMBOBOX5
                cmbo5.Items.Add("");
                cmbo5.Items.Add("Fixed");
                cmbo5.Items.Add("Variable");
                cmbo5.FlatStyle = FlatStyle.Popup;
                cmbo5.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                cmbo5.DisplayStyleForCurrentCellOnly = false;

                // ADD SPECS FOR COMBOBOX6
                cmbo6.Items.Add("");
                cmbo6.Items.Add("FTE");
                cmbo6.Items.Add("Minutes/Day");
                cmbo6.Items.Add("Hours/Day");
                cmbo6.FlatStyle = FlatStyle.Popup;
                cmbo6.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                cmbo6.DisplayStyleForCurrentCellOnly = false;

                // ADD SPECS FOR COMBOBOX7
                cmbo7.Items.Add("");
                cmbo7.Items.Add("ADC");
                cmbo7.Items.Add("Beds");
                cmbo7.Items.Add("Units");
                cmbo7.FlatStyle = FlatStyle.Popup;
                cmbo7.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                cmbo7.DisplayStyleForCurrentCellOnly = false;

                // ADD SPECS FOR COMBOBOX8
                cmbo8.Items.Add("");
                cmbo8.Items.Add("Configure");
                cmbo8.Items.Add("Detail");
                cmbo8.FlatStyle = FlatStyle.Popup;
                cmbo8.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                cmbo8.DisplayStyleForCurrentCellOnly = false;

                // ADD SPECS FOR COMBOBOX9
                cmbo9.Items.Add("");
                cmbo9.Items.Add("Configure");
                cmbo9.Items.Add("Detail");
                cmbo9.FlatStyle = FlatStyle.Popup;
                cmbo9.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                cmbo9.DisplayStyleForCurrentCellOnly = false;

                // ADD SPECS FOR COMBOBOX10
                SQL_Groups.ExecQuery("SELECT * FROM " + tbl_Exp_Group + ";");

                cmbo10.DataSource = SQL_Groups.DBDT;
                cmbo10.DisplayMember = "Expense Group";
                cmbo10.ValueMember = "ID_Num";
                cmbo10.FlatStyle = FlatStyle.Popup;
                cmbo10.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                cmbo10.DisplayStyleForCurrentCellOnly = false;

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

                // ADD SPECS FOR BUTTON7
                btn7.UseColumnTextForButtonValue = true;
                btn7.Text = "_";
                btn7.FlatStyle = FlatStyle.System;
                btn7.DefaultCellStyle.Alignment = (DataGridViewContentAlignment)ContentAlignment.BottomRight;
                btn7.DefaultCellStyle.Font = new Font("Arial", 6, FontStyle.Bold);

                // ADD SPECS FOR BUTTON8
                btn8.UseColumnTextForButtonValue = true;
                btn8.Text = "_";
                btn8.FlatStyle = FlatStyle.System;
                btn8.DefaultCellStyle.Alignment = (DataGridViewContentAlignment)ContentAlignment.BottomRight;
                btn8.DefaultCellStyle.Font = new Font("Arial", 6, FontStyle.Bold);

                // ADD SPECS FOR BUTTON9
                btn9.UseColumnTextForButtonValue = true;
                btn9.Text = "_";
                btn9.FlatStyle = FlatStyle.System;
                btn9.DefaultCellStyle.Alignment = (DataGridViewContentAlignment)ContentAlignment.BottomRight;
                btn9.DefaultCellStyle.Font = new Font("Arial", 6, FontStyle.Bold);

                // ADD SPECS FOR BUTTON10
                btn10.UseColumnTextForButtonValue = true;
                btn10.Text = "_";
                btn10.FlatStyle = FlatStyle.System;
                btn10.DefaultCellStyle.Alignment = (DataGridViewContentAlignment)ContentAlignment.BottomRight;
                btn10.DefaultCellStyle.Font = new Font("Arial", 6, FontStyle.Bold);

                // ADD SPECS FOR BUTTON11
                btn11.UseColumnTextForButtonValue = true;
                btn11.Text = "_";
                btn11.FlatStyle = FlatStyle.System;
                btn11.DefaultCellStyle.Alignment = (DataGridViewContentAlignment)ContentAlignment.BottomRight;
                btn11.DefaultCellStyle.Font = new Font("Arial", 6, FontStyle.Bold);

                // ADD SPECS FOR BUTTON12
                btn12.UseColumnTextForButtonValue = true;
                btn12.Text = "_";
                btn12.FlatStyle = FlatStyle.System;
                btn12.DefaultCellStyle.Alignment = (DataGridViewContentAlignment)ContentAlignment.BottomRight;
                btn12.DefaultCellStyle.Font = new Font("Arial", 6, FontStyle.Bold);
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
                                case 7:
                                    {
                                        dataGridView1.Columns.Add(btn1);
                                    }
                                    break;
                                case 9:
                                    {
                                        dataGridView1.Columns.Add(btn2);
                                    }
                                    break;
                                case 15:
                                    {
                                        dataGridView1.Columns.Add(btn3);
                                    }
                                    break;
                                case 18:
                                    {
                                        dataGridView1.Columns.Add(btn4);
                                    }
                                    break;
                                case 20:
                                    {
                                        dataGridView1.Columns.Add(btn5);
                                    }
                                    break;
                                case 23:
                                    {
                                        dataGridView1.Columns.Add(btn6);
                                    }
                                    break;
                                case 25:
                                    {
                                        dataGridView1.Columns.Add(btn7);
                                    }
                                    break;
                                case 27:
                                    {
                                        dataGridView1.Columns.Add(btn8);
                                    }
                                    break;
                                case 29:
                                    {
                                        dataGridView1.Columns.Add(btn9);
                                    }
                                    break;
                                case 31:
                                    {
                                        dataGridView1.Columns.Add(btn10);
                                    }
                                    break;
                                case 36:
                                    {
                                        dataGridView1.Columns.Add(btn11);
                                    }
                                    break;
                                case 38:
                                    {
                                        dataGridView1.Columns.Add(btn12);
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
                                case 4:
                                    {
                                        dataGridView1.Columns.Add(cmbo1);
                                    }
                                    break;
                                case 5:
                                    {
                                        dataGridView1.Columns.Add(cmbo2);
                                    }
                                    break;
                                case 10:
                                    {
                                        dataGridView1.Columns.Add(cmbo3);
                                    }
                                    break;
                                case 11:
                                    {
                                        dataGridView1.Columns.Add(cmbo4);
                                    }
                                    break;
                                case 13:
                                    {
                                        dataGridView1.Columns.Add(cmbo5);
                                    }
                                    break;
                                case 16:
                                    {
                                        dataGridView1.Columns.Add(cmbo6);
                                    }
                                    break;
                                case 21:
                                    {
                                        dataGridView1.Columns.Add(cmbo7);
                                    }
                                    break;
                                case 26:
                                    {
                                        dataGridView1.Columns.Add(cmbo8);
                                    }
                                    break;
                                case 28:
                                    {
                                        dataGridView1.Columns.Add(cmbo9);
                                    }
                                    break;
                                case 35:
                                    {
                                        dataGridView1.Columns.Add(cmbo10);
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
            SQL_Verse.ExecQuery("SELECT * FROM " + tbl_Name + " ORDER BY Collection_Num ASC;");
            dataGridView1.RowCount = SQL_Verse.RecordCount;

            // FILL DATAGRID FROM DATA TABLE
            for (r = 0; r <= SQL_Verse.RecordCount - 1; r++)
            {
                for (i = 0; i <= Col_Count - 1; i++)
                {
                    switch (i)
                    {
                        case 35:
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
                                switch (i)
                                {
                                    // ACCOUNT FOR SOC OCCUPATION
                                    case 10:
                                        {
                                            if (dataGridView1.Rows[r].Cells[i].Value == null || dataGridView1.Rows[r].Cells[i].Value.ToString() == "")
                                            {
                                                // NOTHING
                                            }
                                            else
                                            {
                                                string category = "'" + dataGridView1.Rows[r].Cells[i].Value + "'";
                                                SQL_Query.ExecQuery("SELECT * FROM dtbExpenseSOC_Codes WHERE [SOC Category] = " + category + ";");
                                                DataGridViewComboBoxColumn col = (DataGridViewComboBoxColumn)dataGridView1.Columns[11];
                                                col.DataSource = SQL_Query.DBDT;
                                                col.DisplayMember = "Occupation";
                                                col.ValueMember = "Occupation";
                                            }
                                        }
                                        break;
                                }
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
                    dataGridView1.Rows[i].Cells[2].Value = i + 1;
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
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;

            // DISABLE CELLS AS DEFAULT - ENABLE LATER
            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                for (j = 5; j <= dataGridView1.ColumnCount - 1; j++)
                {
                    dataGridView1.Rows[i].Cells[j].ReadOnly = true;
                    dataGridView1.Rows[i].Cells[j].Style.SelectionBackColor = SystemColors.Control;
                    dataGridView1.Rows[i].Cells[j].Style.SelectionForeColor = SystemColors.ControlDark;
                    dataGridView1.Rows[i].Cells[j].Style.BackColor = SystemColors.Control;
                    dataGridView1.Rows[i].Cells[j].Style.ForeColor = SystemColors.ControlDark;
                }
            }

            // DYNAMC CTRLS HERE BECAUSE NEED TO ENABLE DATETIME
            // this.Dynamic_CTRLs(dataGridView1);

            // NESTED LOOP DID NOT WORK, SKIPPING ITERATION
            // HIGHLIGHT BORDER OF ROW INDEX FOR SUBLINES - COLUMN 2
            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                if (Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value) != Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value))
                {
                    dataGridView1.Rows[i].Cells[2].Style.SelectionBackColor = SystemColors.Control;
                    dataGridView1.Rows[i].Cells[2].Style.SelectionForeColor = SystemColors.ControlDark;
                    dataGridView1.Rows[i].Cells[2].Style.BackColor = SystemColors.Control;
                    dataGridView1.Rows[i].Cells[2].Style.ForeColor = SystemColors.ControlDark;
                }
            }


            // ENABLE SELECT CELLS - COLUMN 4
            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                switch (dataGridView1.Rows[i].Cells[4].Value.ToString())
                {
                    case "General":
                        {
                            dataGridView1.Rows[i].Cells[5].ReadOnly = false;
                            dataGridView1.Rows[i].Cells[5].Style.BackColor = Color.White;
                            dataGridView1.Rows[i].Cells[5].Style.ForeColor = Color.Black;
                            dataGridView1.Rows[i].Cells[5].Style.SelectionBackColor = SystemColors.Highlight;
                            dataGridView1.Rows[i].Cells[5].Style.SelectionForeColor = Color.White;
                        }
                        break;
                    case "Staffing":
                        {
                            for (j = 10; j <= 15; j++)
                            {
                                if (new int[] { 10, 11, 13 }.Contains(j))
                                {
                                    dataGridView1.Rows[i].Cells[j].ReadOnly = false;
                                    dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                                    dataGridView1.Rows[i].Cells[j].Style.ForeColor = Color.Black;
                                    dataGridView1.Rows[i].Cells[j].Style.SelectionBackColor = SystemColors.Highlight;
                                    dataGridView1.Rows[i].Cells[j].Style.SelectionForeColor = Color.White;
                                }
                            }

                        }
                        break;
                    case "PPS Function":
                        {
                            for (j = 26; j <= 31; j++)
                            {
                                dataGridView1.Rows[i].Cells[j].ReadOnly = false;
                                dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                                dataGridView1.Rows[i].Cells[j].Style.ForeColor = Color.Black;
                                dataGridView1.Rows[i].Cells[j].Style.SelectionBackColor = SystemColors.Highlight;
                                dataGridView1.Rows[i].Cells[j].Style.SelectionForeColor = Color.White;
                            }

                            for (j = 35; j <= 38; j++)
                            {
                                dataGridView1.Rows[i].Cells[j].ReadOnly = false;
                                dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                                dataGridView1.Rows[i].Cells[j].Style.ForeColor = Color.Black;
                                dataGridView1.Rows[i].Cells[j].Style.SelectionBackColor = SystemColors.Highlight;
                                dataGridView1.Rows[i].Cells[j].Style.SelectionForeColor = Color.White;
                            }

                            if (Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value) != Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value))
                            {
                                dataGridView1.Rows[i].Cells[35].ReadOnly = true;
                                dataGridView1.Rows[i].Cells[35].Style.SelectionBackColor = SystemColors.Control;
                                dataGridView1.Rows[i].Cells[35].Style.SelectionForeColor = SystemColors.ControlDark;
                                dataGridView1.Rows[i].Cells[35].Style.BackColor = SystemColors.Control;
                                dataGridView1.Rows[i].Cells[35].Style.ForeColor = SystemColors.ControlDark;
                            }
                        }
                        break;
                    case "Distribution":
                        {
                            for (j = 32; j <= 34; j++)
                            {
                                dataGridView1.Rows[i].Cells[j].ReadOnly = false;
                                dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                                dataGridView1.Rows[i].Cells[j].Style.ForeColor = Color.Black;
                                dataGridView1.Rows[i].Cells[j].Style.SelectionBackColor = SystemColors.Highlight;
                                dataGridView1.Rows[i].Cells[j].Style.SelectionForeColor = Color.White;
                            }

                            for (j = 35; j <= 38; j++)
                            {
                                dataGridView1.Rows[i].Cells[j].ReadOnly = false;
                                dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                                dataGridView1.Rows[i].Cells[j].Style.ForeColor = Color.Black;
                                dataGridView1.Rows[i].Cells[j].Style.SelectionBackColor = SystemColors.Highlight;
                                dataGridView1.Rows[i].Cells[j].Style.SelectionForeColor = Color.White;
                            }

                            if (Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value) != Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value))
                            {
                                dataGridView1.Rows[i].Cells[35].ReadOnly = true;
                                dataGridView1.Rows[i].Cells[35].Style.SelectionBackColor = SystemColors.Control;
                                dataGridView1.Rows[i].Cells[35].Style.SelectionForeColor = SystemColors.ControlDark;
                                dataGridView1.Rows[i].Cells[35].Style.BackColor = SystemColors.Control;
                                dataGridView1.Rows[i].Cells[35].Style.ForeColor = SystemColors.ControlDark;
                            }

                            // ENABLE DATE CONTROLS
                            foreach (Control ctrl in dataGridView1.Controls)
                            {
                                int Diff;
                                int rowNum;
                                string name = "xxx";

                                if (ctrl is DateTimePicker)
                                {
                                    Diff = ctrl.Name.Length - name.Trim().Length;
                                    rowNum = Convert.ToInt32(ctrl.Name.Substring(ctrl.Name.Length - Diff, Diff));

                                    if (rowNum == i)
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


            // ENABLE SELECT CELLS - COLUMN 5
            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                switch (dataGridView1.Rows[i].Cells[5].Value.ToString())
                {
                    case "":
                        break;
                    case "% Net Revenue":
                    case "% Medicare Revenue":
                    case "% Medicaid Revenue":
                    case "% Private Pay Revenue":
                    case "% Medicare MCO Revenue":
                    case "% Medicaid MCO Revenue":
                    case "% VA Revenue":
                    case "% Other Payor Revenue":
                        {
                            for (j = 6; j <= 7; j++)
                            {
                                dataGridView1.Rows[i].Cells[j].ReadOnly = false;
                                dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                                dataGridView1.Rows[i].Cells[j].Style.ForeColor = Color.Black;
                                dataGridView1.Rows[i].Cells[j].Style.SelectionBackColor = SystemColors.Highlight;
                                dataGridView1.Rows[i].Cells[j].Style.SelectionForeColor = Color.White;
                            }
                        }
                        break;
                    case "$ Amount":
                        {
                            for (j = 8; j <= 9; j++)
                            {
                                dataGridView1.Rows[i].Cells[j].ReadOnly = false;
                                dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                                dataGridView1.Rows[i].Cells[j].Style.ForeColor = Color.Black;
                                dataGridView1.Rows[i].Cells[j].Style.SelectionBackColor = SystemColors.Highlight;
                                dataGridView1.Rows[i].Cells[j].Style.SelectionForeColor = Color.White;
                            }
                            for (j = 35; j <= 38; j++)
                            {
                                dataGridView1.Rows[i].Cells[j].ReadOnly = false;
                                dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                                dataGridView1.Rows[i].Cells[j].Style.ForeColor = Color.Black;
                                dataGridView1.Rows[i].Cells[j].Style.SelectionBackColor = SystemColors.Highlight;
                                dataGridView1.Rows[i].Cells[j].Style.SelectionForeColor = Color.White;
                            }
                            if (Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value) != Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value))
                            {
                                dataGridView1.Rows[i].Cells[35].ReadOnly = true;
                                dataGridView1.Rows[i].Cells[35].Style.SelectionBackColor = SystemColors.Control;
                                dataGridView1.Rows[i].Cells[35].Style.SelectionForeColor = SystemColors.ControlDark;
                                dataGridView1.Rows[i].Cells[35].Style.BackColor = SystemColors.Control;
                                dataGridView1.Rows[i].Cells[35].Style.ForeColor = SystemColors.ControlDark;
                            }
                        }
                        break;
                    default:
                        {
                            for (j = 8; j <= 9; j++)
                            {
                                dataGridView1.Rows[i].Cells[j].ReadOnly = false;
                                dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                                dataGridView1.Rows[i].Cells[j].Style.ForeColor = Color.Black;
                                dataGridView1.Rows[i].Cells[j].Style.SelectionBackColor = SystemColors.Highlight;
                                dataGridView1.Rows[i].Cells[j].Style.SelectionForeColor = Color.White;
                            }
                        }
                        break;
                }
            }


            // ENABLE SELECT CELLS - COLUMN 13
            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                switch (dataGridView1.Rows[i].Cells[13].Value.ToString())
                {
                    case "":
                        break;
                    case "Fixed":
                        {
                            for (j = 14; j <= 18; j++)
                            {
                                dataGridView1.Rows[i].Cells[j].ReadOnly = false;
                                dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                                dataGridView1.Rows[i].Cells[j].Style.ForeColor = Color.Black;
                                dataGridView1.Rows[i].Cells[j].Style.SelectionBackColor = SystemColors.Highlight;
                                dataGridView1.Rows[i].Cells[j].Style.SelectionForeColor = Color.White;
                            }
                            for (j = 35; j <= 38; j++)
                            {
                                dataGridView1.Rows[i].Cells[j].ReadOnly = false;
                                dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                                dataGridView1.Rows[i].Cells[j].Style.ForeColor = Color.Black;
                                dataGridView1.Rows[i].Cells[j].Style.SelectionBackColor = SystemColors.Highlight;
                                dataGridView1.Rows[i].Cells[j].Style.SelectionForeColor = Color.White;
                            }
                            if (Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value) != Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value))
                            {
                                dataGridView1.Rows[i].Cells[35].ReadOnly = true;
                                dataGridView1.Rows[i].Cells[35].Style.SelectionBackColor = SystemColors.Control;
                                dataGridView1.Rows[i].Cells[35].Style.SelectionForeColor = SystemColors.ControlDark;
                                dataGridView1.Rows[i].Cells[35].Style.BackColor = SystemColors.Control;
                                dataGridView1.Rows[i].Cells[35].Style.ForeColor = SystemColors.ControlDark;
                            }
                        }
                        break;
                    default:
                        {
                            for (j = 19; j <= 25; j++)
                            {
                                dataGridView1.Rows[i].Cells[j].ReadOnly = false;
                                dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                                dataGridView1.Rows[i].Cells[j].Style.ForeColor = Color.Black;
                                dataGridView1.Rows[i].Cells[j].Style.SelectionBackColor = SystemColors.Highlight;
                                dataGridView1.Rows[i].Cells[j].Style.SelectionForeColor = Color.White;
                            }
                            for (j = 14; j <= 15; j++)
                            {
                                dataGridView1.Rows[i].Cells[j].ReadOnly = false;
                                dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                                dataGridView1.Rows[i].Cells[j].Style.ForeColor = Color.Black;
                                dataGridView1.Rows[i].Cells[j].Style.SelectionBackColor = SystemColors.Highlight;
                                dataGridView1.Rows[i].Cells[j].Style.SelectionForeColor = Color.White;
                            }
                            for (j = 35; j <= 38; j++)
                            {
                                dataGridView1.Rows[i].Cells[j].ReadOnly = false;
                                dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                                dataGridView1.Rows[i].Cells[j].Style.ForeColor = Color.Black;
                                dataGridView1.Rows[i].Cells[j].Style.SelectionBackColor = SystemColors.Highlight;
                                dataGridView1.Rows[i].Cells[j].Style.SelectionForeColor = Color.White;
                            }
                            if (Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value) != Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value))
                            {
                                dataGridView1.Rows[i].Cells[35].ReadOnly = true;
                                dataGridView1.Rows[i].Cells[35].Style.SelectionBackColor = SystemColors.Control;
                                dataGridView1.Rows[i].Cells[35].Style.SelectionForeColor = SystemColors.ControlDark;
                                dataGridView1.Rows[i].Cells[35].Style.BackColor = SystemColors.Control;
                                dataGridView1.Rows[i].Cells[35].Style.ForeColor = SystemColors.ControlDark;
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
                    dataGridView1.Rows[i].Cells[4].ReadOnly = true;
                    dataGridView1.Rows[i].Cells[4].Style.SelectionBackColor = SystemColors.Control;
                    dataGridView1.Rows[i].Cells[4].Style.SelectionForeColor = SystemColors.ControlDark;
                    dataGridView1.Rows[i].Cells[4].Style.BackColor = SystemColors.Control;
                    dataGridView1.Rows[i].Cells[4].Style.ForeColor = SystemColors.ControlDark;
                    
                    // ENABLE GROUPS
                    dataGridView1.Rows[i].Cells[35].ReadOnly = false;
                    dataGridView1.Rows[i].Cells[35].Style.BackColor = Color.White;
                    dataGridView1.Rows[i].Cells[35].Style.ForeColor = Color.Black;
                    dataGridView1.Rows[i].Cells[35].Style.SelectionBackColor = SystemColors.Highlight;
                    dataGridView1.Rows[i].Cells[35].Style.SelectionForeColor = Color.White;
                }
            }

            // MAKE 1ST COLUMN READ ONLY
            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                dataGridView1.Rows[i].Cells[0].ReadOnly = true;
                dataGridView1.Rows[i].Cells[1].ReadOnly = true;
                dataGridView1.Rows[i].Cells[2].ReadOnly = true;
            }

            // MAKE 1ST COLUMN STATIC WHITE
            dataGridView1.Columns[2].DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView1.Columns[2].DefaultCellStyle.SelectionForeColor = Color.Black;

            // COLUMN ALIGNMENT & WIDTH
            dataGridView1.Columns[2].Width = 50;
            dataGridView1.Columns[3].Width = 150;

            for (i = 4; i <= dataGridView1.ColumnCount - 1; i++)
            {
                dataGridView1.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridView1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            //CHECK TIME PICKER COUNT
            {
                //foreach (Control ctrl in dataGridView1.Controls)
                //{

                //    if (ctrl is DateTimePicker)
                //    {
                //        msg += 1;
                //    }

                //}

                //MessageBox.Show(msg.ToString());
            }

            // CALL PROCEDURES
            this.Percent_Change(dataGridView1);
            terminate = 0;
        }

        public override void Percent_Change(DataGridView dataGridView1)
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
                        if (new int[] { 6, 30, 37 }.Contains(j))
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
        public virtual void Dynamic_CTRLs(DataGridView dataGridView1)
        {
            int x;
            int y;
            int i;
            int j;
            int Width;
            int Height;

            Rectangle rect; // STORES A SET OF FOUR INTEGERS
            dataGridView1.HorizontalScrollingOffset = 2000;

            int n;
            int c;

            try
            {
                for (n = 0; n <= dataGridView1.Rows.Count - 1; n++)
                {
                    for (c = 0; c <= dataGridView1.Columns.Count - 1; c++)
                    {
                        // FIND AND DELETE ALL DYNAMIC CONTROLS
                        foreach (Control ctrl in dataGridView1.Controls)
                        {
                            if (ctrl.Name.Substring(0, 3) == "beg" || ctrl.Name.Substring(0, 3) == "end")
                            {
                                dataGridView1.Controls.Remove(ctrl);
                                ctrl.Dispose();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }

            //foreach (Control ctrl in dataGridView1.Controls)
            //{

            //    if (ctrl is DateTimePicker)
            //    {
            //        dataGridView1.Controls.Remove(ctrl);
            //        ctrl.Dispose();
            //    }

            //}

            //CHECK TIME PICKER COUNT
            {
                foreach (Control ctrl in dataGridView1.Controls)
                {

                    if (ctrl is DateTimePicker)
                    {
                        msg += 1;
                    }

                }

                MessageBox.Show(msg.ToString());
            }

            for (j = 0; j <= dataGridView1.ColumnCount - 1; j++)
            {
                switch (j)
                {
                    case 32:
                        {
                            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                            {
                                var gridDte = new DateTimePicker();
                                gridDte.Name = "beg" + i;
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
                    case 33:
                        {
                            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                            {
                                var gridDte = new DateTimePicker();
                                gridDte.Name = "end" + i;
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
        public virtual void Move_CTRLs(DataGridView dataGridView1)
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
                        if (ctrl.Name == "beg" + n || ctrl.Name == "end" + n)
                        {
                            switch (ctrl.Name.Substring(0, 3))
                            {
                                case "beg":
                                    {
                                        rect = dataGridView1.GetCellDisplayRectangle(32, n, false);
                                        x = rect.X;
                                        y = rect.Y;
                                        width = rect.Width;
                                        height = rect.Height;

                                        ctrl.SetBounds(x, y, width, height);
                                        ctrl.Visible = true;
                                    }
                                    break;
                                case "end":
                                    {
                                        rect = dataGridView1.GetCellDisplayRectangle(33, n, false);
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
        public void HandleDynamicDate_Leave(object sender, EventArgs e)
        {
            DateTimePicker dtePck = (DateTimePicker)sender;
            DataGridView dataGridView1 = (DataGridView)dtePck.Tag;
            dataGridView1.CurrentCell.Value = dtePck.Value;
        }

        public virtual void HandleDynamicDate_Enter(object sender, EventArgs e)
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
                case "beg":
                    {
                        dataGridView1.CurrentCell = dataGridView1.Rows[rowNum].Cells[32];
                    }
                    break;
                case "end":
                    {
                        dataGridView1.CurrentCell = dataGridView1.Rows[rowNum].Cells[33];
                    }
                    break;
            }
        }

        public override void InsertUser(DataGridView dataGridView1)
        {
            int i;
            int y;
            string Title = "TINUUM SOFTWARE";
            int num;
            int count;

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
            string cmdInsert1 = "INSERT INTO " + tbl_dtlDyn_genRate + " (ID_Num, Collection_Num) VALUES (" + num + ", " + num +");";
            string cmdInsert2 = "INSERT INTO " + tbl_Dtl_genRate + " (ID_Num, Collection_Num) VALUES (" + num + ", " + num + ");";
            string cmdInsert3 = "INSERT INTO " + tbl_Dyn_genRate + " (ID_Num, Collection_Num) VALUES (" + num + ", " + num + ");";
            string cmdInsert4 = "INSERT INTO " + tbl_dtlDyn_lRatio + " (ID_Num, Collection_Num) VALUES (" + num + ", " + num + ");";
            string cmdInsert5 = "INSERT INTO " + tbl_Dtl_lRatio + " (ID_Num, Collection_Num) VALUES (" + num + ", " + num + ");";
            string cmdInsert6 = "INSERT INTO " + tbl_Dyn_lRatio + " (ID_Num, Collection_Num) VALUES (" + num + ", " + num + ");";
            string cmdInsert7 = "INSERT INTO " + tbl_dtlDyn_rRatio + " (ID_Num, Collection_Num) VALUES (" + num + ", " + num + ");";
            string cmdInsert8 = "INSERT INTO " + tbl_Dtl_rRatio + " (ID_Num, Collection_Num) VALUES (" + num + ", " + num + ");";
            string cmdInsert9 = "INSERT INTO " + tbl_Dyn_rRatio + " (ID_Num, Collection_Num) VALUES (" + num + ", " + num + ");";
            string cmdInsert10 = "INSERT INTO " + tbl_dtlDyn_Shift + " (ID_Num, Collection_Num) VALUES (" + num + ", " + num + ");";
            string cmdInsert11 = "INSERT INTO " + tbl_Dtl_Shift + " (ID_Num, Collection_Num) VALUES (" + num + ", " + num + ");";
            string cmdInsert12 = "INSERT INTO " + tbl_Dyn_Shift + " (ID_Num, Collection_Num) VALUES (" + num + ", " + num + ");";
            string cmdInsert13 = "INSERT INTO " + tbl_dtlDyn_Staff + " (ID_Num, Collection_Num) VALUES (" + num + ", " + num + ");";
            string cmdInsert14 = "INSERT INTO " + tbl_Dtl_Staff + " (ID_Num, Collection_Num) VALUES (" + num + ", " + num + ");";
            string cmdInsert15 = "INSERT INTO " + tbl_Dyn_Staff + " (ID_Num, Collection_Num) VALUES (" + num + ", " + num + ");";
            string cmdInsert16 = "INSERT INTO " + tbl_dtlDyn_Wage + " (ID_Num, Collection_Num) VALUES (" + num + ", " + num + ");";
            string cmdInsert17 = "INSERT INTO " + tbl_Dtl_Wage + " (ID_Num, Collection_Num) VALUES (" + num + ", " + num + ");";
            string cmdInsert18 = "INSERT INTO " + tbl_Dyn_Wage + " (ID_Num, Collection_Num) VALUES (" + num + ", " + num + ");";
            string cmdInsert19 = "INSERT INTO " + tbl_Dtl_Payor + " (ID_Num, Collection_Num) VALUES (" + num + ", " + num + ");";
            string cmdInsert20 = "INSERT INTO " + tbl_Dtl_PDPM + " (ID_Num, Collection_Num) VALUES (" + num + ", " + num + ");";
            string cmdInsert21 = "INSERT INTO " + tbl_dtlPct_Fixed + " (ID_Num, Collection_Num) VALUES (" + num + ", " + num + ");";
            string cmdInsert22 = "INSERT INTO " + tbl_dtlPct_genRate + " (ID_Num, Collection_Num) VALUES (" + num + ", " + num + ");";
            string cmdInsert23 = "INSERT INTO " + tbl_dtlPct_PPS + " (ID_Num, Collection_Num) VALUES (" + num + ", " + num + ");";

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
            SQL_Verse.ExecQuery(cmdInsert13);
            SQL_Verse.ExecQuery(cmdInsert14);
            SQL_Verse.ExecQuery(cmdInsert15);
            SQL_Verse.ExecQuery(cmdInsert16);
            SQL_Verse.ExecQuery(cmdInsert17);
            SQL_Verse.ExecQuery(cmdInsert18);
            SQL_Verse.ExecQuery(cmdInsert19);
            SQL_Verse.ExecQuery(cmdInsert20);
            SQL_Verse.ExecQuery(cmdInsert21);
            SQL_Verse.ExecQuery(cmdInsert22);
            SQL_Verse.ExecQuery(cmdInsert23);

            // UPDATE VERSE COLLECTION ID
            SQL_Verse.AddParam("@PrimKey", num);
            SQL_Verse.AddParam("@Num", num);
            string cmdUpdate = "UPDATE " + tbl_Name + " SET Collection_Num=@Num WHERE ID_Num=@PrimKey;";
            SQL_Verse.ExecQuery(cmdUpdate);
            // CALL METHODS
            this.Add_Source(dataGridView1);
            this.ClinicLoad(dataGridView1);

            add = 0;
        }

        public virtual void Insert_Sub(DataGridView dataGridView1)
        {
            int i;
            int y;
            string Title = "TINUUM SOFTWARE";
            int num;
            int sub;
            int count;
            int j;
            string val;

            if (dataGridView1.RowCount == 0) return;
            // CHECK THAT CURRENT CELL SELECTED
            if (dataGridView1.CurrentCell == null)
            {
                MessageBox.Show("Select row before adding subline.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Parent = 0;
            add = 1;
            this.Query_Header(dataGridView1);

            // IF CURRENT CELL IS PARENT THEN FORMAT CELLS FOR PROCESS 
            if (Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value) == Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[1].Value))
            {
                Parent = 1;
                for (j = 4; j <= 34; j++)
                {
                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[j].Value = null;
                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[j].ReadOnly = true;
                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[j].Style.SelectionBackColor = SystemColors.Control;
                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[j].Style.SelectionForeColor = SystemColors.ControlDark;
                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[j].Style.BackColor = SystemColors.Control;
                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[j].Style.ForeColor = SystemColors.ControlDark;
                }
                for (j = 37; j <= 38; j++)
                {
                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[j].Value = null;
                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[j].ReadOnly = true;
                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[j].Style.SelectionBackColor = SystemColors.Control;
                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[j].Style.SelectionForeColor = SystemColors.ControlDark;
                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[j].Style.BackColor = SystemColors.Control;
                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[j].Style.ForeColor = SystemColors.ControlDark;
                }
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
                                for (j = 4; j <= 38; j++)
                                {
                                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[j].Value = null;
                                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[j].ReadOnly = true;
                                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[j].Style.SelectionBackColor = SystemColors.Control;
                                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[j].Style.SelectionForeColor = SystemColors.ControlDark;
                                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[j].Style.BackColor = SystemColors.Control;
                                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[j].Style.ForeColor = SystemColors.ControlDark;
                                }
                                dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[4].ReadOnly = false;
                                dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[4].Style.BackColor = Color.White;
                                dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[4].Style.ForeColor = Color.Black;
                                dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[4].Style.SelectionBackColor = SystemColors.Highlight;
                                dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[4].Style.SelectionForeColor = Color.White;
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

            if (dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[35].Value == null)
            {
                val = DBNull.Value.ToString();
            }
            else
            {
                val = "'" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[35].Value.ToString() + "'";
            }
            
            // INSERT IDENTITY NUM INTO SUPPORTING DATABASES
            string cmdInsert1 = "INSERT INTO " + tbl_dtlDyn_genRate + " (ID_Num, Collection_Num) VALUES (" + num + ", " + sub + ");";
            string cmdInsert2 = "INSERT INTO " + tbl_Dtl_genRate + " (ID_Num, Collection_Num) VALUES (" + num + ", " + sub + ");";
            string cmdInsert3 = "INSERT INTO " + tbl_Dyn_genRate + " (ID_Num, Collection_Num) VALUES (" + num + ", " + sub + ");";
            string cmdInsert4 = "INSERT INTO " + tbl_dtlDyn_lRatio + " (ID_Num, Collection_Num) VALUES (" + num + ", " + sub + ");";
            string cmdInsert5 = "INSERT INTO " + tbl_Dtl_lRatio + " (ID_Num, Collection_Num) VALUES (" + num + ", " + sub + ");";
            string cmdInsert6 = "INSERT INTO " + tbl_Dyn_lRatio + " (ID_Num, Collection_Num) VALUES (" + num + ", " + sub + ");";
            string cmdInsert7 = "INSERT INTO " + tbl_dtlDyn_rRatio + " (ID_Num, Collection_Num) VALUES (" + num + ", " + sub + ");";
            string cmdInsert8 = "INSERT INTO " + tbl_Dtl_rRatio + " (ID_Num, Collection_Num) VALUES (" + num + ", " + sub + ");";
            string cmdInsert9 = "INSERT INTO " + tbl_Dyn_rRatio + " (ID_Num, Collection_Num) VALUES (" + num + ", " + sub + ");";
            string cmdInsert10 = "INSERT INTO " + tbl_dtlDyn_Shift + " (ID_Num, Collection_Num) VALUES (" + num + ", " + sub + ");";
            string cmdInsert11 = "INSERT INTO " + tbl_Dtl_Shift + " (ID_Num, Collection_Num) VALUES (" + num + ", " + sub + ");";
            string cmdInsert12 = "INSERT INTO " + tbl_Dyn_Shift + " (ID_Num, Collection_Num) VALUES (" + num + ", " + sub + ");";
            string cmdInsert13 = "INSERT INTO " + tbl_dtlDyn_Staff + " (ID_Num, Collection_Num) VALUES (" + num + ", " + sub + ");";
            string cmdInsert14 = "INSERT INTO " + tbl_Dtl_Staff + " (ID_Num, Collection_Num) VALUES (" + num + ", " + sub + ");";
            string cmdInsert15 = "INSERT INTO " + tbl_Dyn_Staff + " (ID_Num, Collection_Num) VALUES (" + num + ", " + sub + ");";
            string cmdInsert16 = "INSERT INTO " + tbl_dtlDyn_Wage + " (ID_Num, Collection_Num) VALUES (" + num + ", " + sub + ");";
            string cmdInsert17 = "INSERT INTO " + tbl_Dtl_Wage + " (ID_Num, Collection_Num) VALUES (" + num + ", " + sub + ");";
            string cmdInsert18 = "INSERT INTO " + tbl_Dyn_Wage + " (ID_Num, Collection_Num) VALUES (" + num + ", " + sub + ");";
            string cmdInsert19 = "INSERT INTO " + tbl_Dtl_Payor + " (ID_Num, Collection_Num) VALUES (" + num + ", " + sub + ");";
            string cmdInsert20 = "INSERT INTO " + tbl_Dtl_PDPM + " (ID_Num, Collection_Num) VALUES (" + num + ", " + sub + ");";
            string cmdInsert21 = "INSERT INTO " + tbl_dtlPct_Fixed + " (ID_Num, Collection_Num) VALUES (" + num + ", " + sub + ");";
            string cmdInsert22 = "INSERT INTO " + tbl_dtlPct_genRate + " (ID_Num, Collection_Num) VALUES (" + num + ", " + sub + ");";
            string cmdInsert23 = "INSERT INTO " + tbl_dtlPct_PPS + " (ID_Num, Collection_Num) VALUES (" + num + ", " + sub + ");";

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
            SQL_Verse.ExecQuery(cmdInsert13);
            SQL_Verse.ExecQuery(cmdInsert14);
            SQL_Verse.ExecQuery(cmdInsert15);
            SQL_Verse.ExecQuery(cmdInsert16);
            SQL_Verse.ExecQuery(cmdInsert17);
            SQL_Verse.ExecQuery(cmdInsert18);
            SQL_Verse.ExecQuery(cmdInsert19);
            SQL_Verse.ExecQuery(cmdInsert20);
            SQL_Verse.ExecQuery(cmdInsert21);
            SQL_Verse.ExecQuery(cmdInsert22);
            SQL_Verse.ExecQuery(cmdInsert23);

            // UPDATE VERSE COLLECTION ID
            SQL_Verse.AddParam("@PrimKey", num);
            SQL_Verse.AddParam("@Num", sub);
            string cmdUpdate = "UPDATE " + tbl_Name + " SET Collection_Num=@Num WHERE ID_Num=@PrimKey;";
            SQL_Verse.ExecQuery(cmdUpdate);
            // CALL METHODS
            this.Add_Source(dataGridView1);
            this.ClinicLoad(dataGridView1);

            Parent = 0;
            add = 0;
        }

        public override void Delete_Command(DataGridView dataGridView1)
        {
            int r;
            string Title = "TINUUM SOFTWARE";
            dataGridView1.Tag = dataGridView1.Parent.Parent.Parent;
            Form frm = (Form)dataGridView1.Tag;

            if (dataGridView1.RowCount == 0) return;
            // CHECK THAT CURRENT CELL SELECTED
            if (dataGridView1.CurrentCell == null)
            {
                MessageBox.Show("Select row before Deleting.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult prompt = MessageBox.Show("Are you sure you want to permanently delete?", Title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            try
            {
                if (prompt == DialogResult.Yes)
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
                                escapeEXP = 1;
                                return;
                            }
                        }
                        else
                        {
                            switch (y)
                            {
                                case 35:
                                    {
                                        if (dataGridView1.Rows[i].Cells[y].Value == DBNull.Value || dataGridView1.Rows[i].Cells[y].Value == null)
                                        {
                                            MessageBox.Show("You must enter relevant values for all fields before continuing.", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            dataGridView1.CurrentCell = dataGridView1.Rows[i].Cells[y];
                                            return;
                                        }
                                    }
                                    break;
                            }
                        }
                    }
                }
            }

            for (y = 0; y <= dataGridView1.RowCount - 1; y++)
            {
                for (i = 1; i <= dataGridView1.ColumnCount - 1; i++)
                {
                    if (new int[] { 6, 8, 14, 17, 19, 22, 24, 30, 37 }.Contains(i))
                    {
                        // SUBMIT TO MAJOR DATA TABLE
                        if (dataGridView1.Rows[y].Cells[i].ReadOnly == false)
                        {
                            // ADD PARAMS
                            switch (i)
                            {
                                case 6:
                                case 30:
                                case 37:
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
                                if (new int[] { 6, 30, 37 }.Contains(i))
                                {
                                    string percent = dataGridView1.Rows[y].Cells[i].Value.ToString(); // SUBMIT VALUES OF NEXT CELL OVER
                                    if (Information.IsNumeric(percent.Substring(0, percent.Length - 1)))
                                    {
                                        for (j = 1; j <= myMethods.Period * Mos_Const; j++)
                                        {
                                            switch (i)
                                            {
                                                case 6:
                                                    {
                                                        SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                        SQL_Verse.AddParam("@vals", myMethods.ToDecimal(dataGridView1.Rows[y].Cells[i].Value.ToString()));
                                                        string header = "month" + j;
                                                        string cmdUpdate1 = "UPDATE " + tbl_dtlPct_genRate + " SET " + header + "=@vals WHERE ID_Num=@PrimKey;";
                                                        SQL_Verse.ExecQuery(cmdUpdate1);
                                                    }
                                                    break;
                                                case 30:
                                                    {
                                                        SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                        SQL_Verse.AddParam("@vals", myMethods.ToDecimal(dataGridView1.Rows[y].Cells[i].Value.ToString()));
                                                        string header = "month" + j;
                                                        string cmdUpdate2 = "UPDATE " + tbl_dtlPct_PPS + " SET " + header + "=@vals WHERE ID_Num=@PrimKey;";
                                                        SQL_Verse.ExecQuery(cmdUpdate2);
                                                    }
                                                    break;
                                                case 37:
                                                    {
                                                        SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                        SQL_Verse.AddParam("@vals", myMethods.ToDecimal(dataGridView1.Rows[y].Cells[i].Value.ToString()));
                                                        string header = "month" + j;
                                                        string cmdUpdate3 = "UPDATE " + tbl_dtlPct_Fixed + " SET " + header + "=@vals WHERE ID_Num=@PrimKey;";
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
                                        case 8:
                                            {
                                                if (Information.IsNumeric(dataGridView1.Rows[y].Cells[i].Value))
                                                {
                                                    for (j = 1; j <= myMethods.Period * Mos_Const; j++)
                                                    {
                                                        SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                        SQL_Verse.AddParam("@vals", dataGridView1.Rows[y].Cells[i].Value);
                                                        string header = "month" + j;
                                                        string cmdUpdate1 = "UPDATE " + tbl_Dtl_genRate + " SET " + header + "=@vals WHERE ID_Num=@PrimKey;";
                                                        SQL_Verse.ExecQuery(cmdUpdate1);   
                                                    }
                                                    // DYNAMIC DEFAULT CHANGE
                                                    SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                    SQL_Verse.AddParam("@val1", 1);
                                                    SQL_Verse.AddParam("@val2", DBNull.Value);
                                                    string colName1 = "Choose";
                                                    string colName2 = "Selection";
                                                    string cmdUpdate2 = "UPDATE " + tbl_Dyn_genRate + " SET " + colName1 + "=@val1, " + colName2 + "=@val2 WHERE ID_Num=@PrimKey;";
                                                    SQL_Verse.ExecQuery(cmdUpdate2);
                                                }
                                            }
                                            break;
                                        case 14:
                                            {
                                                if (Information.IsNumeric(dataGridView1.Rows[y].Cells[i].Value))
                                                {
                                                    for (j = 1; j <= myMethods.Period * Mos_Const; j++)
                                                    {
                                                        SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                        SQL_Verse.AddParam("@vals", dataGridView1.Rows[y].Cells[i].Value);
                                                        string header = "month" + j;
                                                        string cmdUpdate1 = "UPDATE " + tbl_Dtl_Wage + " SET " + header + "=@vals WHERE ID_Num=@PrimKey;";
                                                        SQL_Verse.ExecQuery(cmdUpdate1);
                                                    }
                                                    // DYNAMIC DEFAULT CHANGE
                                                    SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                    SQL_Verse.AddParam("@val1", 1);
                                                    SQL_Verse.AddParam("@val2", DBNull.Value);
                                                    string colName1 = "Choose";
                                                    string colName2 = "Selection";
                                                    string cmdUpdate2 = "UPDATE " + tbl_Dyn_Wage + " SET " + colName1 + "=@val1, " + colName2 + "=@val2 WHERE ID_Num=@PrimKey;";
                                                    SQL_Verse.ExecQuery(cmdUpdate2);
                                                }
                                            }
                                            break;
                                        case 17:
                                            {
                                                if (Information.IsNumeric(dataGridView1.Rows[y].Cells[i].Value))
                                                {
                                                    for (j = 1; j <= myMethods.Period * Mos_Const; j++)
                                                    {
                                                        SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                        SQL_Verse.AddParam("@vals", dataGridView1.Rows[y].Cells[i].Value);
                                                        string header = "month" + j;
                                                        string cmdUpdate1 = "UPDATE " + tbl_Dtl_Staff + " SET " + header + "=@vals WHERE ID_Num=@PrimKey;";
                                                        SQL_Verse.ExecQuery(cmdUpdate1);
                                                    }
                                                    // DYNAMIC DEFAULT CHANGE
                                                    SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                    SQL_Verse.AddParam("@val1", 1);
                                                    SQL_Verse.AddParam("@val2", DBNull.Value);
                                                    string colName1 = "Choose";
                                                    string colName2 = "Selection";
                                                    string cmdUpdate2 = "UPDATE " + tbl_Dyn_Staff + " SET " + colName1 + "=@val1, " + colName2 + "=@val2 WHERE ID_Num=@PrimKey;";
                                                    SQL_Verse.ExecQuery(cmdUpdate2);
                                                }
                                            }
                                            break;
                                        case 19:
                                            {
                                                if (Information.IsNumeric(dataGridView1.Rows[y].Cells[i].Value))
                                                {
                                                    for (j = 1; j <= myMethods.Period * Mos_Const; j++)
                                                    {
                                                        SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                        SQL_Verse.AddParam("@vals", dataGridView1.Rows[y].Cells[i].Value);
                                                        string header = "month" + j;
                                                        string cmdUpdate1 = "UPDATE " + tbl_Dtl_Shift + " SET " + header + "=@vals WHERE ID_Num=@PrimKey;";
                                                        SQL_Verse.ExecQuery(cmdUpdate1);
                                                    }
                                                    // DYNAMIC DEFAULT CHANGE
                                                    SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                    SQL_Verse.AddParam("@val1", 1);
                                                    SQL_Verse.AddParam("@val2", DBNull.Value);
                                                    string colName1 = "Choose";
                                                    string colName2 = "Selection";
                                                    string cmdUpdate2 = "UPDATE " + tbl_Dyn_Shift + " SET " + colName1 + "=@val1, " + colName2 + "=@val2 WHERE ID_Num=@PrimKey;";
                                                    SQL_Verse.ExecQuery(cmdUpdate2);
                                                }
                                            }
                                            break;
                                        case 22:
                                            {
                                                if (Information.IsNumeric(dataGridView1.Rows[y].Cells[i].Value))
                                                {
                                                    for (j = 1; j <= myMethods.Period * Mos_Const; j++)
                                                    {
                                                        SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                        SQL_Verse.AddParam("@vals", dataGridView1.Rows[y].Cells[i].Value);
                                                        string header = "month" + j;
                                                        string cmdUpdate1 = "UPDATE " + tbl_Dtl_lRatio + " SET " + header + "=@vals WHERE ID_Num=@PrimKey;";
                                                        SQL_Verse.ExecQuery(cmdUpdate1);
                                                    }
                                                    // DYNAMIC DEFAULT CHANGE
                                                    SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                    SQL_Verse.AddParam("@val1", 1);
                                                    SQL_Verse.AddParam("@val2", DBNull.Value);
                                                    string colName1 = "Choose";
                                                    string colName2 = "Selection";
                                                    string cmdUpdate2 = "UPDATE " + tbl_Dyn_lRatio + " SET " + colName1 + "=@val1, " + colName2 + "=@val2 WHERE ID_Num=@PrimKey;";
                                                    SQL_Verse.ExecQuery(cmdUpdate2);
                                                }
                                            }
                                            break;
                                        case 24:
                                            {
                                                if (Information.IsNumeric(dataGridView1.Rows[y].Cells[i].Value))
                                                {
                                                    for (j = 1; j <= myMethods.Period * Mos_Const; j++)
                                                    {
                                                        SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                        SQL_Verse.AddParam("@vals", dataGridView1.Rows[y].Cells[i].Value);
                                                        string header = "month" + j;
                                                        string cmdUpdate1 = "UPDATE " + tbl_Dtl_rRatio + " SET " + header + "=@vals WHERE ID_Num=@PrimKey;";
                                                        SQL_Verse.ExecQuery(cmdUpdate1);
                                                    }
                                                    // DYNAMIC DEFAULT CHANGE
                                                    SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                    SQL_Verse.AddParam("@val1", 1);
                                                    SQL_Verse.AddParam("@val2", DBNull.Value);
                                                    string colName1 = "Choose";
                                                    string colName2 = "Selection";
                                                    string cmdUpdate2 = "UPDATE " + tbl_Dyn_rRatio + " SET " + colName1 + "=@val1, " + colName2 + "=@val2 WHERE ID_Num=@PrimKey;";
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
                                    case 12: // CONTROL FOR SOC CODE
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
                                    case 35: // CONTROL FOR EXPENSE GROUPS
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

            grid = 1;

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
                            if (new int[] { 6, 8, 14, 17, 19, 22, 24, 30, 37 }.Contains(i))
                            {
                                // UPDATE STATEMENT FOR DETAIL IF NUMERIC
                                if (dataGridView1.Rows[y].Cells[i].ReadOnly == false)
                                {
                                    if (new int[] { 6, 30, 37 }.Contains(i))
                                    {
                                        string percent = dataGridView1.Rows[y].Cells[i].Value.ToString(); // SUBMIT VALUES OF NEXT CELL OVER
                                        if (Information.IsNumeric(percent.Substring(0, percent.Length - 1)))
                                        {
                                            for (j = 1; j <= myMethods.Period * Mos_Const; j++)
                                            {
                                                switch (i)
                                                {
                                                    case 6:
                                                        {
                                                            SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                            SQL_Verse.AddParam("@vals", myMethods.ToDecimal(dataGridView1.Rows[y].Cells[i].Value.ToString()));
                                                            string header = "month" + j;
                                                            string cmdUpdate1 = "UPDATE " + tbl_dtlPct_genRate + " SET " + header + "=@vals WHERE ID_Num=@PrimKey;";
                                                            SQL_Verse.ExecQuery(cmdUpdate1);
                                                        }
                                                        break;
                                                    case 30:
                                                        {
                                                            SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                            SQL_Verse.AddParam("@vals", myMethods.ToDecimal(dataGridView1.Rows[y].Cells[i].Value.ToString()));
                                                            string header = "month" + j;
                                                            string cmdUpdate2 = "UPDATE " + tbl_dtlPct_PPS + " SET " + header + "=@vals WHERE ID_Num=@PrimKey;";
                                                            SQL_Verse.ExecQuery(cmdUpdate2);
                                                        }
                                                        break;
                                                    case 37:
                                                        {
                                                            SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                            SQL_Verse.AddParam("@vals", myMethods.ToDecimal(dataGridView1.Rows[y].Cells[i].Value.ToString()));
                                                            string header = "month" + j;
                                                            string cmdUpdate3 = "UPDATE " + tbl_dtlPct_Fixed + " SET " + header + "=@vals WHERE ID_Num=@PrimKey;";
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
                                            case 8:
                                                {
                                                    if (Information.IsNumeric(dataGridView1.Rows[y].Cells[i].Value))
                                                    {
                                                        for (j = 1; j <= myMethods.Period * Mos_Const; j++)
                                                        {
                                                            SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                            SQL_Verse.AddParam("@vals", dataGridView1.Rows[y].Cells[i].Value);
                                                            string header = "month" + j;
                                                            string cmdUpdate1 = "UPDATE " + tbl_Dtl_genRate + " SET " + header + "=@vals WHERE ID_Num=@PrimKey;";
                                                            SQL_Verse.ExecQuery(cmdUpdate1);
                                                        }
                                                        // DYNAMIC DEFAULT CHANGE
                                                        SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                        SQL_Verse.AddParam("@val1", 1);
                                                        SQL_Verse.AddParam("@val2", DBNull.Value);
                                                        string colName1 = "Choose";
                                                        string colName2 = "Selection";
                                                        string cmdUpdate2 = "UPDATE " + tbl_Dyn_genRate + " SET " + colName1 + "=@val1, " + colName2 + "=@val2 WHERE ID_Num=@PrimKey;";
                                                        SQL_Verse.ExecQuery(cmdUpdate2);
                                                    }
                                                }
                                                break;
                                            case 14:
                                                {
                                                    if (Information.IsNumeric(dataGridView1.Rows[y].Cells[i].Value))
                                                    {
                                                        for (j = 1; j <= myMethods.Period * Mos_Const; j++)
                                                        {
                                                            SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                            SQL_Verse.AddParam("@vals", dataGridView1.Rows[y].Cells[i].Value);
                                                            string header = "month" + j;
                                                            string cmdUpdate1 = "UPDATE " + tbl_Dtl_Wage + " SET " + header + "=@vals WHERE ID_Num=@PrimKey;";
                                                            SQL_Verse.ExecQuery(cmdUpdate1);
                                                        }
                                                        // DYNAMIC DEFAULT CHANGE
                                                        SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                        SQL_Verse.AddParam("@val1", 1);
                                                        SQL_Verse.AddParam("@val2", DBNull.Value);
                                                        string colName1 = "Choose";
                                                        string colName2 = "Selection";
                                                        string cmdUpdate2 = "UPDATE " + tbl_Dyn_Wage + " SET " + colName1 + "=@val1, " + colName2 + "=@val2 WHERE ID_Num=@PrimKey;";
                                                        SQL_Verse.ExecQuery(cmdUpdate2);
                                                    }
                                                }
                                                break;
                                            case 17:
                                                {
                                                    if (Information.IsNumeric(dataGridView1.Rows[y].Cells[i].Value))
                                                    {
                                                        for (j = 1; j <= myMethods.Period * Mos_Const; j++)
                                                        {
                                                            SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                            SQL_Verse.AddParam("@vals", dataGridView1.Rows[y].Cells[i].Value);
                                                            string header = "month" + j;
                                                            string cmdUpdate1 = "UPDATE " + tbl_Dtl_Staff + " SET " + header + "=@vals WHERE ID_Num=@PrimKey;";
                                                            SQL_Verse.ExecQuery(cmdUpdate1);
                                                        }
                                                        // DYNAMIC DEFAULT CHANGE
                                                        SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                        SQL_Verse.AddParam("@val1", 1);
                                                        SQL_Verse.AddParam("@val2", DBNull.Value);
                                                        string colName1 = "Choose";
                                                        string colName2 = "Selection";
                                                        string cmdUpdate2 = "UPDATE " + tbl_Dyn_Staff + " SET " + colName1 + "=@val1, " + colName2 + "=@val2 WHERE ID_Num=@PrimKey;";
                                                        SQL_Verse.ExecQuery(cmdUpdate2);
                                                    }
                                                }
                                                break;
                                            case 19:
                                                {
                                                    if (Information.IsNumeric(dataGridView1.Rows[y].Cells[i].Value))
                                                    {
                                                        for (j = 1; j <= myMethods.Period * Mos_Const; j++)
                                                        {
                                                            SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                            SQL_Verse.AddParam("@vals", dataGridView1.Rows[y].Cells[i].Value);
                                                            string header = "month" + j;
                                                            string cmdUpdate1 = "UPDATE " + tbl_Dtl_Shift + " SET " + header + "=@vals WHERE ID_Num=@PrimKey;";
                                                            SQL_Verse.ExecQuery(cmdUpdate1);
                                                        }
                                                        // DYNAMIC DEFAULT CHANGE
                                                        SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                        SQL_Verse.AddParam("@val1", 1);
                                                        SQL_Verse.AddParam("@val2", DBNull.Value);
                                                        string colName1 = "Choose";
                                                        string colName2 = "Selection";
                                                        string cmdUpdate2 = "UPDATE " + tbl_Dyn_Shift + " SET " + colName1 + "=@val1, " + colName2 + "=@val2 WHERE ID_Num=@PrimKey;";
                                                        SQL_Verse.ExecQuery(cmdUpdate2);
                                                    }
                                                }
                                                break;
                                            case 22:
                                                {
                                                    if (Information.IsNumeric(dataGridView1.Rows[y].Cells[i].Value))
                                                    {
                                                        for (j = 1; j <= myMethods.Period * Mos_Const; j++)
                                                        {
                                                            SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                            SQL_Verse.AddParam("@vals", dataGridView1.Rows[y].Cells[i].Value);
                                                            string header = "month" + j;
                                                            string cmdUpdate1 = "UPDATE " + tbl_Dtl_lRatio + " SET " + header + "=@vals WHERE ID_Num=@PrimKey;";
                                                            SQL_Verse.ExecQuery(cmdUpdate1);
                                                        }
                                                        // DYNAMIC DEFAULT CHANGE
                                                        SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                        SQL_Verse.AddParam("@val1", 1);
                                                        SQL_Verse.AddParam("@val2", DBNull.Value);
                                                        string colName1 = "Choose";
                                                        string colName2 = "Selection";
                                                        string cmdUpdate2 = "UPDATE " + tbl_Dyn_lRatio + " SET " + colName1 + "=@val1, " + colName2 + "=@val2 WHERE ID_Num=@PrimKey;";
                                                        SQL_Verse.ExecQuery(cmdUpdate2);
                                                    }
                                                }
                                                break;
                                            case 24:
                                                {
                                                    if (Information.IsNumeric(dataGridView1.Rows[y].Cells[i].Value))
                                                    {
                                                        for (j = 1; j <= myMethods.Period * Mos_Const; j++)
                                                        {
                                                            SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                            SQL_Verse.AddParam("@vals", dataGridView1.Rows[y].Cells[i].Value);
                                                            string header = "month" + j;
                                                            string cmdUpdate1 = "UPDATE " + tbl_Dtl_rRatio + " SET " + header + "=@vals WHERE ID_Num=@PrimKey;";
                                                            SQL_Verse.ExecQuery(cmdUpdate1);
                                                        }
                                                        // DYNAMIC DEFAULT CHANGE
                                                        SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                        SQL_Verse.AddParam("@val1", 1);
                                                        SQL_Verse.AddParam("@val2", DBNull.Value);
                                                        string colName1 = "Choose";
                                                        string colName2 = "Selection";
                                                        string cmdUpdate2 = "UPDATE " + tbl_Dyn_rRatio + " SET " + colName1 + "=@val1, " + colName2 + "=@val2 WHERE ID_Num=@PrimKey;";
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

        public override void CellEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView1 = (DataGridView)sender;

            int i;
            int j;
            string title = "TINUUM SOFTWARE";

            switch (e.ColumnIndex)
            {
                case 4:
                    {
                        switch (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)
                        {
                            case null:
                                {
                                    // CLEAR CONTENTS OF IRRELEVANT CELLS
                                    for (j = 5; j <= dataGridView1.ColumnCount - 1; j++)
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
                                        string name = "xxx";

                                        if (ctrl is DateTimePicker)
                                        {
                                            Diff = ctrl.Name.Length - name.Trim().Length;
                                            rowNum = Convert.ToInt32(ctrl.Name.Substring(ctrl.Name.Length - Diff, Diff));

                                            if (rowNum == e.RowIndex)
                                            {
                                                ctrl.Enabled = false;
                                            }
                                        }
                                    }
                                }
                                break;
                            case "General":
                                {
                                    // CLEAR CONTENTS OF IRRELEVANT CELLS
                                    for (j = 5; j <= dataGridView1.ColumnCount - 1; j++)
                                    {
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Value = null;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].ReadOnly = true;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionBackColor = SystemColors.Control;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionForeColor = SystemColors.ControlDark;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.BackColor = SystemColors.Control;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.ForeColor = SystemColors.ControlDark;   
                                    }

                                    dataGridView1.Rows[e.RowIndex].Cells[5].ReadOnly = false;
                                    dataGridView1.Rows[e.RowIndex].Cells[5].Style.BackColor = Color.White;
                                    dataGridView1.Rows[e.RowIndex].Cells[5].Style.ForeColor = Color.Black;
                                    dataGridView1.Rows[e.RowIndex].Cells[5].Style.SelectionBackColor = SystemColors.Highlight;
                                    dataGridView1.Rows[e.RowIndex].Cells[5].Style.SelectionForeColor = Color.White;
                                    
                                    // DISABLE DATE CONTROLS
                                    foreach (Control ctrl in dataGridView1.Controls)
                                    {
                                        int Diff;
                                        int rowNum;
                                        string name = "xxx";

                                        if (ctrl is DateTimePicker)
                                        {
                                            Diff = ctrl.Name.Length - name.Trim().Length;
                                            rowNum = Convert.ToInt32(ctrl.Name.Substring(ctrl.Name.Length - Diff, Diff));

                                            if (rowNum == e.RowIndex)
                                            {
                                                ctrl.Enabled = false;
                                            }
                                        }

                                    }
                                }
                                break;
                            case "Staffing":
                                {
                                    // CLEAR CONTENTS OF IRRELEVANT CELLS
                                    for (j = 5; j <= dataGridView1.ColumnCount - 1; j++)
                                    {
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Value = null;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].ReadOnly = true;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionBackColor = SystemColors.Control;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionForeColor = SystemColors.ControlDark;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.BackColor = SystemColors.Control;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.ForeColor = SystemColors.ControlDark;
                                    }

                                    for (j = 10; j <= 15; j++)
                                    {
                                        if (new int[] { 10, 11, 13 }.Contains(j))
                                        {
                                            dataGridView1.Rows[e.RowIndex].Cells[j].ReadOnly = false;
                                            dataGridView1.Rows[e.RowIndex].Cells[j].Style.BackColor = Color.White;
                                            dataGridView1.Rows[e.RowIndex].Cells[j].Style.ForeColor = Color.Black;
                                            dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionBackColor = SystemColors.Highlight;
                                            dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionForeColor = Color.White;
                                        }
                                    }

                                    // DISABLE DATE CONTROLS
                                    foreach (Control ctrl in dataGridView1.Controls)
                                    {
                                        int Diff;
                                        int rowNum;
                                        string name = "xxx";

                                        if (ctrl is DateTimePicker)
                                        {
                                            Diff = ctrl.Name.Length - name.Trim().Length;
                                            rowNum = Convert.ToInt32(ctrl.Name.Substring(ctrl.Name.Length - Diff, Diff));

                                            if (rowNum == e.RowIndex)
                                            {
                                                ctrl.Enabled = false;
                                            }
                                        }

                                    }

                                }
                                break;
                            case "PPS Function":
                                {
                                    // CLEAR CONTENTS OF IRRELEVANT CELLS
                                    for (j = 5; j <= dataGridView1.ColumnCount - 1; j++)
                                    {
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Value = null;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].ReadOnly = true;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionBackColor = SystemColors.Control;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionForeColor = SystemColors.ControlDark;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.BackColor = SystemColors.Control;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.ForeColor = SystemColors.ControlDark;
                                    }

                                    for (j = 26; j <= 31; j++)
                                    {
                                        dataGridView1.Rows[e.RowIndex].Cells[j].ReadOnly = false;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.BackColor = Color.White;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.ForeColor = Color.Black;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionBackColor = SystemColors.Highlight;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionForeColor = Color.White;   
                                    }

                                    for (j = 35; j <= 38; j++)
                                    {
                                        dataGridView1.Rows[e.RowIndex].Cells[j].ReadOnly = false;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.BackColor = Color.White;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.ForeColor = Color.Black;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionBackColor = SystemColors.Highlight;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionForeColor = Color.White;
                                    }

                                    if (Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value) != Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value))
                                    {
                                        dataGridView1.Rows[e.RowIndex].Cells[35].ReadOnly = true;
                                        dataGridView1.Rows[e.RowIndex].Cells[35].Style.SelectionBackColor = SystemColors.Control;
                                        dataGridView1.Rows[e.RowIndex].Cells[35].Style.SelectionForeColor = SystemColors.ControlDark;
                                        dataGridView1.Rows[e.RowIndex].Cells[35].Style.BackColor = SystemColors.Control;
                                        dataGridView1.Rows[e.RowIndex].Cells[35].Style.ForeColor = SystemColors.ControlDark;
                                    }

                                    // DISABLE DATE CONTROLS
                                    foreach (Control ctrl in dataGridView1.Controls)
                                    {
                                        int Diff;
                                        int rowNum;
                                        string name = "xxx";

                                        if (ctrl is DateTimePicker)
                                        {
                                            Diff = ctrl.Name.Length - name.Trim().Length;
                                            rowNum = Convert.ToInt32(ctrl.Name.Substring(ctrl.Name.Length - Diff, Diff));

                                            if (rowNum == e.RowIndex)
                                            {
                                                ctrl.Enabled = false;
                                            }
                                        }

                                    }
                                }
                                break;
                            case "Distribution":
                                {
                                    // CLEAR CONTENTS OF IRRELEVANT CELLS
                                    for (j = 5; j <= dataGridView1.ColumnCount - 1; j++)
                                    {
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Value = null;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].ReadOnly = true;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionBackColor = SystemColors.Control;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionForeColor = SystemColors.ControlDark;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.BackColor = SystemColors.Control;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.ForeColor = SystemColors.ControlDark;
                                    }

                                    for (j = 32; j <= 34; j++)
                                    {
                                        dataGridView1.Rows[e.RowIndex].Cells[j].ReadOnly = false;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.BackColor = Color.White;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.ForeColor = Color.Black;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionBackColor = SystemColors.Highlight;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionForeColor = Color.White;
                                    }

                                    for (j = 35; j <= 38; j++)
                                    {
                                        dataGridView1.Rows[e.RowIndex].Cells[j].ReadOnly = false;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.BackColor = Color.White;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.ForeColor = Color.Black;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionBackColor = SystemColors.Highlight;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionForeColor = Color.White;
                                    }

                                    if (Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value) != Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value))
                                    {
                                        dataGridView1.Rows[e.RowIndex].Cells[35].ReadOnly = true;
                                        dataGridView1.Rows[e.RowIndex].Cells[35].Style.SelectionBackColor = SystemColors.Control;
                                        dataGridView1.Rows[e.RowIndex].Cells[35].Style.SelectionForeColor = SystemColors.ControlDark;
                                        dataGridView1.Rows[e.RowIndex].Cells[35].Style.BackColor = SystemColors.Control;
                                        dataGridView1.Rows[e.RowIndex].Cells[35].Style.ForeColor = SystemColors.ControlDark;
                                    }
                                    // ENABLE DATE CONTROLS
                                    foreach (Control ctrl in dataGridView1.Controls)
                                    {
                                        int Diff;
                                        int rowNum;
                                        string name = "xxx";

                                        if (ctrl is DateTimePicker)
                                        {
                                            Diff = ctrl.Name.Length - name.Trim().Length;
                                            rowNum = Convert.ToInt32(ctrl.Name.Substring(ctrl.Name.Length - Diff, Diff));

                                            if (rowNum == e.RowIndex)
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
                case 5:
                    {
                        switch (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)
                        {
                            case null:
                                {
                                    // CLEAR CONTENTS OF IRRELEVANT CELLS
                                    for (j = 6; j <= 9; j++)
                                    {
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Value = null;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].ReadOnly = true;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionBackColor = SystemColors.Control;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionForeColor = SystemColors.ControlDark;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.BackColor = SystemColors.Control;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.ForeColor = SystemColors.ControlDark;
                                    }
                                }
                                break;
                            case "% Net Revenue":
                            case "% Medicare Revenue":
                            case "% Medicaid Revenue":
                            case "% Private Pay Revenue":
                            case "% Medicare MCO Revenue":
                            case "% Medicaid MCO Revenue":
                            case "% VA Revenue":
                            case "% Other Payor Revenue":
                                {
                                    // CLEAR CONTENTS OF IRRELEVANT CELLS
                                    for (j = 6; j <= 9; j++)
                                    {
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Value = null;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].ReadOnly = true;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionBackColor = SystemColors.Control;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionForeColor = SystemColors.ControlDark;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.BackColor = SystemColors.Control;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.ForeColor = SystemColors.ControlDark;
                                    }
                                    for (j = 6; j <= 7; j++)
                                    {
                                        dataGridView1.Rows[e.RowIndex].Cells[j].ReadOnly = false;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.BackColor = Color.White;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.ForeColor = Color.Black;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionBackColor = SystemColors.Highlight;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionForeColor = Color.White;
                                    }
                                    for (j = 35; j <= 38; j++)
                                    {
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Value = null;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].ReadOnly = true;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionBackColor = SystemColors.Control;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionForeColor = SystemColors.ControlDark;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.BackColor = SystemColors.Control;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.ForeColor = SystemColors.ControlDark;
                                    }
                                }
                                break;
                            case "$ Amount":
                                {
                                    for (j = 6; j <= 9; j++)
                                    {
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Value = null;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].ReadOnly = true;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionBackColor = SystemColors.Control;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionForeColor = SystemColors.ControlDark;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.BackColor = SystemColors.Control;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.ForeColor = SystemColors.ControlDark;
                                    }
                                    for (j = 8; j <= 9; j++)
                                    {
                                        dataGridView1.Rows[e.RowIndex].Cells[j].ReadOnly = false;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.BackColor = Color.White;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.ForeColor = Color.Black;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionBackColor = SystemColors.Highlight;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionForeColor = Color.White;
                                    }
                                    for (j = 35; j <= 38; j++)
                                    {
                                        dataGridView1.Rows[e.RowIndex].Cells[j].ReadOnly = false;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.BackColor = Color.White;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.ForeColor = Color.Black;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionBackColor = SystemColors.Highlight;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionForeColor = Color.White;
                                    }
                                    if (Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value) != Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value))
                                    {
                                        dataGridView1.Rows[e.RowIndex].Cells[35].ReadOnly = true;
                                        dataGridView1.Rows[e.RowIndex].Cells[35].Style.SelectionBackColor = SystemColors.Control;
                                        dataGridView1.Rows[e.RowIndex].Cells[35].Style.SelectionForeColor = SystemColors.ControlDark;
                                        dataGridView1.Rows[e.RowIndex].Cells[35].Style.BackColor = SystemColors.Control;
                                        dataGridView1.Rows[e.RowIndex].Cells[35].Style.ForeColor = SystemColors.ControlDark;
                                    }
                                }
                                break;
                            default:
                                {
                                    // CLEAR CONTENTS OF IRRELEVANT CELLS
                                    for (j = 6; j <= 9; j++)
                                    {
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Value = null;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].ReadOnly = true;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionBackColor = SystemColors.Control;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionForeColor = SystemColors.ControlDark;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.BackColor = SystemColors.Control;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.ForeColor = SystemColors.ControlDark;
                                    }
                                    for (j = 8; j <= 9; j++)
                                    {
                                        dataGridView1.Rows[e.RowIndex].Cells[j].ReadOnly = false;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.BackColor = Color.White;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.ForeColor = Color.Black;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionBackColor = SystemColors.Highlight;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionForeColor = Color.White;
                                    }
                                    for (j = 35; j <= 38; j++)
                                    {
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Value = null;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].ReadOnly = true;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionBackColor = SystemColors.Control;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionForeColor = SystemColors.ControlDark;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.BackColor = SystemColors.Control;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.ForeColor = SystemColors.ControlDark;
                                    }
                                }
                                break;
                        }
                    }
                    break;
                case 13:
                    {
                        switch (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)
                        {
                            case null:
                                {
                                    // CLEAR CONTENTS OF IRRELEVANT CELLS
                                    for (j = 16; j <= 25; j++)
                                    {
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Value = null;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].ReadOnly = true;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionBackColor = SystemColors.Control;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionForeColor = SystemColors.ControlDark;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.BackColor = SystemColors.Control;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.ForeColor = SystemColors.ControlDark;
                                    }
                                }
                                break;
                            case "Fixed":
                                {
                                    // CLEAR CONTENTS OF IRRELEVANT CELLS
                                    for (j = 16; j <= 25; j++)
                                    {
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Value = null;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].ReadOnly = true;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionBackColor = SystemColors.Control;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionForeColor = SystemColors.ControlDark;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.BackColor = SystemColors.Control;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.ForeColor = SystemColors.ControlDark;
                                    }
                                    for (j = 14; j <= 18; j++)
                                    {
                                        dataGridView1.Rows[e.RowIndex].Cells[j].ReadOnly = false;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.BackColor = Color.White;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.ForeColor = Color.Black;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionBackColor = SystemColors.Highlight;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionForeColor = Color.White;
                                    }
                                    for (j = 35; j <= 38; j++)
                                    {
                                        dataGridView1.Rows[e.RowIndex].Cells[j].ReadOnly = false;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.BackColor = Color.White;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.ForeColor = Color.Black;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionBackColor = SystemColors.Highlight;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionForeColor = Color.White;
                                    }
                                    if (Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value) != Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value))
                                    {
                                        dataGridView1.Rows[e.RowIndex].Cells[35].ReadOnly = true;
                                        dataGridView1.Rows[e.RowIndex].Cells[35].Style.SelectionBackColor = SystemColors.Control;
                                        dataGridView1.Rows[e.RowIndex].Cells[35].Style.SelectionForeColor = SystemColors.ControlDark;
                                        dataGridView1.Rows[e.RowIndex].Cells[35].Style.BackColor = SystemColors.Control;
                                        dataGridView1.Rows[e.RowIndex].Cells[35].Style.ForeColor = SystemColors.ControlDark;
                                    }
                                }
                                break;
                            default:
                                {
                                    // CLEAR CONTENTS OF IRRELEVANT CELLS
                                    for (j = 16; j <= 25; j++)
                                    {
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Value = null;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].ReadOnly = true;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionBackColor = SystemColors.Control;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionForeColor = SystemColors.ControlDark;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.BackColor = SystemColors.Control;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.ForeColor = SystemColors.ControlDark;
                                    }
                                    for (j = 19; j <= 25; j++)
                                    {
                                        dataGridView1.Rows[e.RowIndex].Cells[j].ReadOnly = false;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.BackColor = Color.White;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.ForeColor = Color.Black;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionBackColor = SystemColors.Highlight;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionForeColor = Color.White;
                                    }
                                    for (j = 14; j <= 15; j++)
                                    {
                                        dataGridView1.Rows[e.RowIndex].Cells[j].ReadOnly = false;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.BackColor = Color.White;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.ForeColor = Color.Black;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionBackColor = SystemColors.Highlight;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionForeColor = Color.White;
                                    }
                                    for (j = 35; j <= 38; j++)
                                    {
                                        dataGridView1.Rows[e.RowIndex].Cells[j].ReadOnly = false;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.BackColor = Color.White;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.ForeColor = Color.Black;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionBackColor = SystemColors.Highlight;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionForeColor = Color.White;
                                    }
                                    if (Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value) != Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value))
                                    {
                                        dataGridView1.Rows[e.RowIndex].Cells[35].ReadOnly = true;
                                        dataGridView1.Rows[e.RowIndex].Cells[35].Style.SelectionBackColor = SystemColors.Control;
                                        dataGridView1.Rows[e.RowIndex].Cells[35].Style.SelectionForeColor = SystemColors.ControlDark;
                                        dataGridView1.Rows[e.RowIndex].Cells[35].Style.BackColor = SystemColors.Control;
                                        dataGridView1.Rows[e.RowIndex].Cells[35].Style.ForeColor = SystemColors.ControlDark;
                                    }
                                }
                                break;
                        }
                    }
                    break;
                case 6:
                case 30:
                case 37:
                    {
                        if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == DBNull.Value || dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null) return;
                        var val = myMethods.ToPercent(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = val;
                    }
                    break;
                case 35:
                    {
                        for (j = e.RowIndex + 1; j <= dataGridView1.RowCount - 1; j++)
                        {
                            if (Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value) == Convert.ToInt32(dataGridView1.Rows[j].Cells[1].Value))
                            {
                                dataGridView1.Rows[j].Cells[35].Value = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                            }
                        }
                    }
                    break;
                case 32:
                case 33:
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
                case 8:
                case 14:
                case 17:
                case 19:
                case 22:
                case 24:
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
                    case 26:
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
                                        dtlExpense_Collection frmDetail = new dtlExpense_Collection();
                                        frmDetail.Show(dataGridView1);
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    case 28:
                        {
                            var switchExpr = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                            switch (switchExpr)
                            {
                                case "Configure":
                                    {
                                        FormConfigurePDPM frmDetail = new FormConfigurePDPM();
                                        frmDetail.Show(dataGridView1);
                                    }
                                    break;
                                case "Detail":
                                    {
                                        dtlExpense_Collection frmDetail = new dtlExpense_Collection();
                                        frmDetail.Show(dataGridView1);
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    case 10:
                        {
                            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null) return;

                            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value = null;
                            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 2].Value = null;
                            string category = "'" + dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value + "'";
                            SQL_Query.ExecQuery("SELECT * FROM dtbExpenseSOC_Codes WHERE [SOC Category] = " + category + ";");
                            DataGridViewComboBoxColumn col = (DataGridViewComboBoxColumn)dataGridView1.Columns[11];
                            col.DataSource = SQL_Query.DBDT;
                            col.DisplayMember = "Occupation";
                            col.ValueMember = "Occupation";
                        }
                        break;
                    case 11:
                        {
                            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null) return;

                            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value = null;
                            string category = "'" + dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value + "'";
                            string occupation = "'" + dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value + "'";
                            SQL_Query.ExecQuery("SELECT * FROM dtbExpenseSOC_Codes WHERE [SOC Category] = " + category + " AND Occupation = " + occupation + ";");
                            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value = SQL_Query.DBDT.Rows[0][2].ToString();
                        }
                        break;
                    case 32:
                    case 33:
                        {
                            DateTime date1 = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[32].Value);
                            DateTime date2 = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[33].Value);
                            int result = DateTime.Compare(date1, date2);

                            if (result > 0)
                            {
                                MessageBox.Show("Retry. End date must be greater than start date.", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                dataGridView1.Rows[e.RowIndex].Cells[32].Value = null;
                                dataGridView1.Rows[e.RowIndex].Cells[33].Value = null;

                                dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[32];
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

            Form frm = senderGrid.Parent.Parent.Parent as Form;
            try
            {
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
                {
                    switch (e.ColumnIndex)
                    {
                        case 7:
                        case 31:
                        case 38:
                            {
                                dataGridView1.CurrentCell = dataGridView1.Rows[Slct].Cells[e.ColumnIndex - 1];
                                dtlExpense_Percent frmDetail = new dtlExpense_Percent();
                                frmDetail.Show(dataGridView1);           
                            }
                            break;
                        case 9:
                            {
                                dataGridView1.CurrentCell = dataGridView1.Rows[Slct].Cells[e.ColumnIndex - 1];
                                index = 8;
                                dtlExpense_Dynamic frmDetail = new dtlExpense_Dynamic();
                                frmDetail.Show(dataGridView1);
                            }
                            break;
                        case 15:
                            {
                                dataGridView1.CurrentCell = dataGridView1.Rows[Slct].Cells[e.ColumnIndex - 1];
                                index = 14;
                                dtlExpense_Dynamic frmDetail = new dtlExpense_Dynamic();
                                frmDetail.Show(dataGridView1);
                            }
                            break;
                        case 18:
                            {
                                dataGridView1.CurrentCell = dataGridView1.Rows[Slct].Cells[e.ColumnIndex - 1];
                                index = 17;
                                dtlExpense_Dynamic frmDetail = new dtlExpense_Dynamic();
                                frmDetail.Show(dataGridView1);
                            }
                            break;
                        case 20:
                            {
                                dataGridView1.CurrentCell = dataGridView1.Rows[Slct].Cells[e.ColumnIndex - 1];
                                index = 19;
                                dtlExpense_Dynamic frmDetail = new dtlExpense_Dynamic();
                                frmDetail.Show(dataGridView1);
                            }
                            break;
                        case 23:
                            {
                                dataGridView1.CurrentCell = dataGridView1.Rows[Slct].Cells[e.ColumnIndex - 1];
                                index = 22;
                                dtlExpense_Dynamic frmDetail = new dtlExpense_Dynamic();
                                frmDetail.Show(dataGridView1);
                            }
                            break;
                        case 25:
                            {
                                dataGridView1.CurrentCell = dataGridView1.Rows[Slct].Cells[e.ColumnIndex - 1];
                                index = 24;
                                dtlExpense_Dynamic frmDetail = new dtlExpense_Dynamic();
                                frmDetail.Show(dataGridView1);
                            }
                            break;
                        case 36:
                            {
                                dataGridView1.CurrentCell = dataGridView1.Rows[Slct].Cells[e.ColumnIndex - 1];
                                FormGroups_Expenses frmDetail = new FormGroups_Expenses();
                                frm.Enabled = false;
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
        public virtual void DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            DataGridView dataGridView1 = (DataGridView)sender;

            switch (e.ColumnIndex)
            {
                case 35:
                    {
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;
                    }
                    break;
            }
        }
    }
}

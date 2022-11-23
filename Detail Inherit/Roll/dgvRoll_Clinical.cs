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
    [CLSCompliant(true)]
    class dgvRoll_Clinical
    {
        protected SQLControl SQL_Verse = new SQLControl();
        protected SQLControl SQL_Name = new SQLControl();
        protected SQLControl SQL_Source = new SQLControl();
        protected string tbl_Medicaid = "dtbRollDetailPct_Medicaid";
        protected string tbl_MCOMedicaid = "dtbRollDetailPct_MCOMedicaid";
        protected string tbl_MCOMedicare = "dtbRollDetailPct_MCOMedicare";
        protected string tbl_Other = "dtbRollDetailPct_Other";
        protected string tbl_PrivatePay = "dtbRollDetailPct_PrivatePay";
        protected string tbl_VA = "dtbRollDetailPct_VA";
        protected string tbl_Weight = "dtbRollDetail_Weight";
        protected string tbl_PPS = "dtbRollDetail_PPS";
        protected int terminate;
        protected int exit;
        protected int add;
        protected int grid;
        protected DialogResult cnclPrompt;
        // FROM MASTER
        public static string Rslt_Cncl = null;
        public static string _Rslt_Cncl
        {
            get
            {
                return Rslt_Cncl;
            }
        }
        public static int escapeNum = 0;
        public static int _escapeNum
        {
            get
            {
                return escapeNum;
            }
        }
        protected SQLControl SQL = new SQLControl(); // CREATE NEW INSTANCE OF SQLCONTROL CLASS
        protected List<string> Headers_Submit = new List<string>();
        protected List<string> Header_Name = new List<string>();
        protected List<string> Header_Rename = new List<string>();
        protected int Col_Count;
        protected string tbl_Name = "dtbRollVerse";
        protected string tbl_Change = "Default";
        protected int Mos_Const = 12;
        protected static int index;
        public static int _index
        {
            get
            {
                return index;
            }
        }


        public dgvRoll_Clinical()
        {
               
        }
        public virtual void Add_Source(DataGridView dataGridView1)
        {
            string btnString = "(b)";
            string cmbString = "(c)";
            string dteString = "(d)";
            int i;
            myMethods.SQL_Grab();

            // UNBIND DATA SOURCE AT BEGINNING FOR UPDATE COMMAND
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ColumnCount = 0;
            dataGridView1.RowCount = 0;
            dataGridView1.Refresh();

            Header_Name.Clear();
            Headers_Submit.Clear();

            // LINK DATA SOURCE TO GET COL NAMES
            SQL_Source.ExecQuery("SELECT * FROM " + tbl_Name + ";");
            if (SQL_Source.HasException(true))
                return;

            dataGridView1.DataSource = SQL_Source.DBDT;

            // FILL LIST FROM COLUMN HEADERS
            Col_Count = dataGridView1.ColumnCount; //CHECK

            for (i = 0; i <= Col_Count - 1; i++)
            {
                Headers_Submit.Add("[" + dataGridView1.Columns[i].HeaderText + "]");
            }

            for (i = 0; i <= Col_Count - 1; i++)
            {
                Header_Name.Add(dataGridView1.Columns[i].HeaderText);
            }


            for (i = 0; i <= Col_Count - 1; i++)
            {
                var switchExpr = Header_Name[i].Substring(Header_Name[i].Length - 3, 3);
                switch (switchExpr)
                {
                    case var @case when @case == btnString:
                        {
                            Header_Rename.Add("");
                            break;
                        }

                    case var case1 when case1 == cmbString:
                    case var case2 when case2 == dteString:
                        {
                            Header_Rename.Add(Header_Name[i].Substring(0, Header_Name[i].Length - 3));
                            break;
                        }

                    default:
                        {
                            Header_Rename.Add(Header_Name[i]);
                            break;
                        }
                }
            }
            // CLEAR DATA SOURCE 
            dataGridView1.DataSource = null;
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
            var btn13 = new DataGridViewButtonColumn();
            var btn14 = new DataGridViewButtonColumn();
            
            terminate = 1;

            SQL_Verse.ExecQuery("SELECT * FROM " + tbl_Name + ";");

            // COLUMN CONTROLS
            {
                // ADD SPECS FOR COMBOBOX1
                cmbo1.Items.Add("");
                cmbo1.Items.Add("Configure");
                cmbo1.Items.Add("Detail");
                cmbo1.FlatStyle = FlatStyle.Popup;
                cmbo1.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                cmbo1.DisplayStyleForCurrentCellOnly = false;

                // ADD SPECS FOR COMBOBOX2
                cmbo2.Items.Add("");
                cmbo2.Items.Add("Configure");
                cmbo2.Items.Add("Detail");
                cmbo2.FlatStyle = FlatStyle.Popup;
                cmbo2.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                cmbo2.DisplayStyleForCurrentCellOnly = false;

                // ADD SPECS FOR COMBOBOX3
                cmbo3.Items.Add("");
                cmbo3.Items.Add("% Medicare");
                cmbo3.Items.Add("% Private Pay");
                cmbo3.Items.Add("Flat Rate");
                cmbo3.Items.Add("State Basis");
                cmbo3.FlatStyle = FlatStyle.Popup;
                cmbo3.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                cmbo3.DisplayStyleForCurrentCellOnly = false;

                // ADD SPECS FOR COMBOBOX4
                cmbo4.Items.Add("");
                cmbo4.Items.Add("Configure");
                cmbo4.Items.Add("Detail");
                cmbo4.FlatStyle = FlatStyle.Popup;
                cmbo4.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                cmbo4.DisplayStyleForCurrentCellOnly = false;

                // ADD SPECS FOR COMBOBOX5
                cmbo5.Items.Add("");
                cmbo5.Items.Add("% Medicare");
                cmbo5.Items.Add("% Medicaid");
                cmbo5.Items.Add("Flat Rate");
                cmbo5.FlatStyle = FlatStyle.Popup;
                cmbo5.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                cmbo5.DisplayStyleForCurrentCellOnly = false;

                // ADD SPECS FOR COMBOBOX6
                cmbo6.Items.Add("");
                cmbo6.Items.Add("Configure");
                cmbo6.Items.Add("Detail");
                cmbo6.FlatStyle = FlatStyle.Popup;
                cmbo6.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                cmbo6.DisplayStyleForCurrentCellOnly = false;

                // ADD SPECS FOR COMBOBOX7
                cmbo7.Items.Add("");
                cmbo7.Items.Add("% Medicare");
                cmbo7.Items.Add("% Medicaid");
                cmbo7.Items.Add("% Private Pay");
                cmbo7.Items.Add("Flat Rate");
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
                cmbo9.Items.Add("% Medicare");
                cmbo9.Items.Add("% Medicaid");
                cmbo9.Items.Add("% Private Pay");
                cmbo9.Items.Add("Flat Rate");
                cmbo9.FlatStyle = FlatStyle.Popup;
                cmbo9.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                cmbo9.DisplayStyleForCurrentCellOnly = false;

                // ADD SPECS FOR COMBOBOX10
                cmbo10.Items.Add("");
                cmbo10.Items.Add("Configure");
                cmbo10.Items.Add("Detail");
                cmbo10.FlatStyle = FlatStyle.Popup;
                cmbo10.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                cmbo10.DisplayStyleForCurrentCellOnly = false;

                // ADD SPECS FOR COMBOBOX11
                cmbo11.Items.Add("");
                cmbo11.Items.Add("% Medicare");
                cmbo11.Items.Add("% Medicaid");
                cmbo11.Items.Add("% Private Pay");
                cmbo11.Items.Add("Flat Rate");
                cmbo11.FlatStyle = FlatStyle.Popup;
                cmbo11.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                cmbo11.DisplayStyleForCurrentCellOnly = false;

                // ADD SPECS FOR COMBOBOX12
                cmbo12.Items.Add("");
                cmbo12.Items.Add("Configure");
                cmbo12.Items.Add("Detail");
                cmbo12.FlatStyle = FlatStyle.Popup;
                cmbo12.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                cmbo12.DisplayStyleForCurrentCellOnly = false;


                // ADD SPECS FOR COMBOBOX13
                cmbo13.Items.Add("");
                cmbo13.Items.Add("% Medicare");
                cmbo13.Items.Add("% Medicaid");
                cmbo13.Items.Add("% Private Pay");
                cmbo13.Items.Add("Flat Rate");
                cmbo13.FlatStyle = FlatStyle.Popup;
                cmbo13.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                cmbo13.DisplayStyleForCurrentCellOnly = false;

                // ADD SPECS FOR COMBOBOX14
                cmbo14.Items.Add("");
                cmbo14.Items.Add("Configure");
                cmbo14.Items.Add("Detail");
                cmbo14.FlatStyle = FlatStyle.Popup;
                cmbo14.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                cmbo14.DisplayStyleForCurrentCellOnly = false;
                
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

                // ADD SPECS FOR BUTTON13
                btn13.UseColumnTextForButtonValue = true;
                btn13.Text = "_";
                btn13.FlatStyle = FlatStyle.System;
                btn13.DefaultCellStyle.Alignment = (DataGridViewContentAlignment)ContentAlignment.BottomRight;
                btn13.DefaultCellStyle.Font = new Font("Arial", 6, FontStyle.Bold);

                // ADD SPECS FOR BUTTON14
                btn14.UseColumnTextForButtonValue = true;
                btn14.Text = "_";
                btn14.FlatStyle = FlatStyle.System;
                btn14.DefaultCellStyle.Alignment = (DataGridViewContentAlignment)ContentAlignment.BottomRight;
                btn14.DefaultCellStyle.Font = new Font("Arial", 6, FontStyle.Bold);
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
                                case 4:
                                    {
                                        dataGridView1.Columns.Add(btn1);
                                    }
                                    break;
                                case 6:
                                    {
                                        dataGridView1.Columns.Add(btn2);
                                    }
                                    break;
                                case 9:
                                    {
                                        dataGridView1.Columns.Add(btn3);
                                    }
                                    break;
                                case 11:
                                    {
                                        dataGridView1.Columns.Add(btn4);
                                    }
                                    break;
                                case 14:
                                    {
                                        dataGridView1.Columns.Add(btn5);
                                    }
                                    break;
                                case 16:
                                    {
                                        dataGridView1.Columns.Add(btn6);
                                    }
                                    break;
                                case 19:
                                    {
                                        dataGridView1.Columns.Add(btn7);
                                    }
                                    break;
                                case 21:
                                    {
                                        dataGridView1.Columns.Add(btn8);
                                    }
                                    break;
                                case 24:
                                    {
                                        dataGridView1.Columns.Add(btn9);
                                    }
                                    break;
                                case 26:
                                    {
                                        dataGridView1.Columns.Add(btn10);
                                    }
                                    break;
                                case 29:
                                    {
                                        dataGridView1.Columns.Add(btn11);
                                    }
                                    break;
                                case 31:
                                    {
                                        dataGridView1.Columns.Add(btn12);
                                    }
                                    break;
                                case 34:
                                    {
                                        dataGridView1.Columns.Add(btn13);
                                    }
                                    break;
                                case 36:
                                    {
                                        dataGridView1.Columns.Add(btn14);
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
                                case 5:
                                    {
                                        dataGridView1.Columns.Add(cmbo2);
                                    }
                                    break;
                                case 7:
                                    {
                                        dataGridView1.Columns.Add(cmbo3);
                                    }
                                    break;
                                case 10:
                                    {
                                        dataGridView1.Columns.Add(cmbo4);
                                    }
                                    break;
                                case 12:
                                    {
                                        dataGridView1.Columns.Add(cmbo5);
                                    }
                                    break;
                                case 15:
                                    {
                                        dataGridView1.Columns.Add(cmbo6);
                                    }
                                    break;
                                case 17:
                                    {
                                        dataGridView1.Columns.Add(cmbo7);
                                    }
                                    break;
                                case 20:
                                    {
                                        dataGridView1.Columns.Add(cmbo8);
                                    }
                                    break;
                                case 22:
                                    {
                                        dataGridView1.Columns.Add(cmbo9);
                                    }
                                    break;
                                case 25:
                                    {
                                        dataGridView1.Columns.Add(cmbo10);
                                    }
                                    break;
                                case 27:
                                    {
                                        dataGridView1.Columns.Add(cmbo11);
                                    }
                                    break;
                                case 30:
                                    {
                                        dataGridView1.Columns.Add(cmbo12);
                                    }
                                    break;
                                case 32:
                                    {
                                        dataGridView1.Columns.Add(cmbo13);
                                    }
                                    break;
                                case 35:
                                    {
                                        dataGridView1.Columns.Add(cmbo14);
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
            for (i = 0; i <= dataGridView1.RowCount -1; i++)
            {
                for (j = 0; j <= dataGridView1.ColumnCount -1; j++)
                {
                    if (new int[] { 7, 12, 17, 22, 27, 32 }.Contains(j))
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
                            case "% Medicare":
                            case "% Medicaid":
                            case "% Private Pay":
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
                            case "Flat Rate":
                            case "State Basis":
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
                            default:
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

            for (i = 4; i <= dataGridView1.ColumnCount - 1; i++)
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
                        if (new int[] { 8, 13, 18, 23, 28, 33 }.Contains(j))
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

        public void Query_Header(DataGridView dataGridView1)
        {
            int i;

            // LINK DATA SOURCE TO GET COL NAMES
            SQL.ExecQuery("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME= '" + tbl_Name + "'" + "ORDER BY ORDINAL_POSITION" + ";");
            if (SQL.HasException(true))
                return;

            // FILL LIST FROM COLUMN HEADERS
            Col_Count = dataGridView1.ColumnCount;

            for (i = 0; i <= Col_Count - 1; i++)
            {
                Headers_Submit.Add("[" + SQL.DBDT.Rows[i][0] + "]");
            }

            for (i = 0; i <= Col_Count - 1; i++)
            {
                Header_Name.Add(dataGridView1.Columns[i].HeaderText);
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
            string cmdInsert1 = "INSERT INTO " + tbl_MCOMedicaid + " (ID_Num) VALUES (" + num + ");";
            string cmdInsert2 = "INSERT INTO " + tbl_MCOMedicare + " (ID_Num) VALUES (" + num + ");";
            string cmdInsert3 = "INSERT INTO " + tbl_Medicaid + " (ID_Num) VALUES (" + num + ");";
            string cmdInsert4 = "INSERT INTO " + tbl_PrivatePay + " (ID_Num) VALUES (" + num + ");";
            string cmdInsert5 = "INSERT INTO " + tbl_Other + " (ID_Num) VALUES (" + num + ");";
            string cmdInsert6 = "INSERT INTO " + tbl_VA + " (ID_Num) VALUES (" + num + ");";
            string cmdInsert7 = "INSERT INTO " + tbl_Weight + " (ID_Num) VALUES (" + num + ");";
            string cmdInsert8 = "INSERT INTO " + tbl_PPS + " (ID_Num) VALUES (" + num + ");";
            SQL_Verse.ExecQuery(cmdInsert1);
            SQL_Verse.ExecQuery(cmdInsert2);
            SQL_Verse.ExecQuery(cmdInsert3);
            SQL_Verse.ExecQuery(cmdInsert4);
            SQL_Verse.ExecQuery(cmdInsert5);
            SQL_Verse.ExecQuery(cmdInsert6);
            SQL_Verse.ExecQuery(cmdInsert7);
            SQL_Verse.ExecQuery(cmdInsert8);

            // CALL METHODS
            this.Add_Source(dataGridView1);
            this.ClinicLoad(dataGridView1);

            add = 0;
        }

        public virtual void UpdateSQL(DataGridView dataGridView1)
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
            //tab.TabPages[0].Show();

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
                    if (new int[] { 8, 13, 18, 23, 28, 33 }.Contains(i))
                    {
                        // SUBMIT TO MAJOR DATA TABLE
                        if (dataGridView1.Rows[y].Cells[i].ReadOnly == false)
                        {
                            // ADD PARAMS
                            string percent = dataGridView1.Rows[y].Cells[i].Value.ToString();
                            if (Information.IsNumeric(percent.Substring(0, percent.Length - 1)))
                            {
                                SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                SQL_Verse.AddParam("@vals", myMethods.ToDecimal(dataGridView1.Rows[y].Cells[i].Value.ToString()));
                            }
                            else
                            {
                                SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                SQL_Verse.AddParam("@vals", dataGridView1.Rows[y].Cells[i].Value);
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
                                string percent = dataGridView1.Rows[y].Cells[i].Value.ToString(); // SUBMIT VALUES OF NEXT CELL OVER
                                if (Information.IsNumeric(percent.Substring(0, percent.Length - 1)))
                                {
                                    for (j = 1; j <= myMethods.Period * Mos_Const; j++)
                                    {
                                        switch (i)
                                        {
                                            case 8:
                                                {
                                                    SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                    SQL_Verse.AddParam("@vals", myMethods.ToDecimal(dataGridView1.Rows[y].Cells[i].Value.ToString()));
                                                    string header = "month" + j;
                                                    string cmdUpdate1 = "UPDATE " + tbl_Medicaid + " SET " + header + "=@vals WHERE ID_Num=@PrimKey;";
                                                    SQL_Verse.ExecQuery(cmdUpdate1);
                                                }
                                                break;
                                            case 13:
                                                {
                                                    SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                    SQL_Verse.AddParam("@vals", myMethods.ToDecimal(dataGridView1.Rows[y].Cells[i].Value.ToString()));
                                                    string header = "month" + j;
                                                    string cmdUpdate2 = "UPDATE " + tbl_PrivatePay + " SET " + header + "=@vals WHERE ID_Num=@PrimKey;";
                                                    SQL_Verse.ExecQuery(cmdUpdate2);
                                                }
                                                break;
                                            case 18:
                                                {
                                                    SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                    SQL_Verse.AddParam("@vals", myMethods.ToDecimal(dataGridView1.Rows[y].Cells[i].Value.ToString()));
                                                    string header = "month" + j;
                                                    string cmdUpdate3 = "UPDATE " + tbl_MCOMedicare + " SET " + header + "=@vals WHERE ID_Num=@PrimKey;";
                                                    SQL_Verse.ExecQuery(cmdUpdate3);
                                                }
                                                break;
                                            case 23:
                                                {
                                                    SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                    SQL_Verse.AddParam("@vals", myMethods.ToDecimal(dataGridView1.Rows[y].Cells[i].Value.ToString()));
                                                    string header = "month" + j;
                                                    string cmdUpdate4 = "UPDATE " + tbl_MCOMedicaid + " SET " + header + "=@vals WHERE ID_Num=@PrimKey;";
                                                    SQL_Verse.ExecQuery(cmdUpdate4);
                                                }
                                                break;
                                            case 28:
                                                {
                                                    SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                    SQL_Verse.AddParam("@vals", myMethods.ToDecimal(dataGridView1.Rows[y].Cells[i].Value.ToString()));
                                                    string header = "month" + j;
                                                    string cmdUpdate5 = "UPDATE " + tbl_VA + " SET " + header + "=@vals WHERE ID_Num=@PrimKey;";
                                                    SQL_Verse.ExecQuery(cmdUpdate5);
                                                }
                                                break;
                                            case 33:
                                                {
                                                    SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                    SQL_Verse.AddParam("@vals", myMethods.ToDecimal(dataGridView1.Rows[y].Cells[i].Value.ToString()));
                                                    string header = "month" + j;
                                                    string cmdUpdate6 = "UPDATE " + tbl_Other + " SET " + header + "=@vals WHERE ID_Num=@PrimKey;";
                                                    SQL_Verse.ExecQuery(cmdUpdate6);
                                                }
                                                break;
                                        }
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
                            if (i > 1)
                            {
                                SQL_Verse.AddParam("@vals", DBNull.Value);
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

        public virtual void Delete_Command(DataGridView dataGridView1)
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
                    {
                        //SQL_Verse.ExecQuery("DELETE FROM " + tbl_Medicaid + " WHERE ID_Num=@PrimKey;");
                        //SQL_Verse.ExecQuery("DELETE FROM " + tbl_PrivatePay + " WHERE ID_Num=@PrimKey;");
                        //SQL_Verse.ExecQuery("DELETE FROM " + tbl_MCOMedicare + " WHERE ID_Num=@PrimKey;");
                        //SQL_Verse.ExecQuery("DELETE FROM " + tbl_MCOMedicaid + " WHERE ID_Num=@PrimKey;");
                        //SQL_Verse.ExecQuery("DELETE FROM " + tbl_VA + " WHERE ID_Num=@PrimKey;");
                        //SQL_Verse.ExecQuery("DELETE FROM " + tbl_Other + " WHERE ID_Num=@PrimKey;");
                        //SQL_Verse.ExecQuery("DELETE FROM " + tbl_Weight + " WHERE ID_Num=@PrimKey;");
                        //SQL_Verse.ExecQuery("DELETE FROM " + tbl_PPS + " WHERE ID_Num=@PrimKey;");
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

        public virtual void Cancel(DataGridView dataGridView1)
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
                                SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[i].Cells[0].Value.ToString());
                                SQL_Verse.ExecQuery("DELETE FROM " + tbl_Name + " WHERE ID_Num=@PrimKey;");
                                {
                                    //IRRELEVANT - SQL CASCADE
                                    //SQL_Verse.ExecQuery("DELETE FROM " + tbl_Medicaid + " WHERE ID_Num=@PrimKey;");
                                    //SQL_Verse.ExecQuery("DELETE FROM " + tbl_PrivatePay + " WHERE ID_Num=@PrimKey;");
                                    //SQL_Verse.ExecQuery("DELETE FROM " + tbl_MCOMedicare + " WHERE ID_Num=@PrimKey;");
                                    //SQL_Verse.ExecQuery("DELETE FROM " + tbl_MCOMedicaid + " WHERE ID_Num=@PrimKey;");
                                    //SQL_Verse.ExecQuery("DELETE FROM " + tbl_VA + " WHERE ID_Num=@PrimKey;");
                                    //SQL_Verse.ExecQuery("DELETE FROM " + tbl_Other + " WHERE ID_Num=@PrimKey;");
                                    //SQL_Verse.ExecQuery("DELETE FROM " + tbl_Weight + " WHERE ID_Num=@PrimKey;");
                                    //SQL_Verse.ExecQuery("DELETE FROM " + tbl_PPS + " WHERE ID_Num=@PrimKey;");
                                }
                            }
                        }
                    }
                    for (y = 0; y <= dataGridView1.RowCount - 1; y++)
                    {
                        for (i = 1; i <= dataGridView1.ColumnCount - 1; i++)
                        {
                            if (new int[] { 8, 13, 18, 23, 28, 33 }.Contains(i))
                            {
                                // UPDATE STATEMENT FOR DETAIL IF NUMERIC
                                if (dataGridView1.Rows[y].Cells[i].ReadOnly == false)
                                {
                                    string percent = dataGridView1.Rows[y].Cells[i].Value.ToString(); // SUBMIT VALUES OF NEXT CELL OVER
                                    if (Information.IsNumeric(percent.Substring(0, percent.Length - 1)))
                                    {
                                        for (j = 1; j <= myMethods.Period * Mos_Const; j++)
                                        {
                                            switch (i)
                                            {
                                                case 8:
                                                    {
                                                        SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                        SQL_Verse.AddParam("@vals", myMethods.ToDecimal(dataGridView1.Rows[y].Cells[i].Value.ToString()));
                                                        string header = "month" + j;
                                                        string cmdUpdate1 = "UPDATE " + tbl_Medicaid + " SET " + header + "=@vals WHERE ID_Num=@PrimKey;";
                                                        SQL_Verse.ExecQuery(cmdUpdate1);
                                                    }
                                                    break;
                                                case 13:
                                                    {
                                                        SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                        SQL_Verse.AddParam("@vals", myMethods.ToDecimal(dataGridView1.Rows[y].Cells[i].Value.ToString()));
                                                        string header = "month" + j;
                                                        string cmdUpdate2 = "UPDATE " + tbl_PrivatePay + " SET " + header + "=@vals WHERE ID_Num=@PrimKey;";
                                                        SQL_Verse.ExecQuery(cmdUpdate2);
                                                    }
                                                    break;
                                                case 18:
                                                    {
                                                        SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                        SQL_Verse.AddParam("@vals", myMethods.ToDecimal(dataGridView1.Rows[y].Cells[i].Value.ToString()));
                                                        string header = "month" + j;
                                                        string cmdUpdate3 = "UPDATE " + tbl_MCOMedicare + " SET " + header + "=@vals WHERE ID_Num=@PrimKey;";
                                                        SQL_Verse.ExecQuery(cmdUpdate3);
                                                    }
                                                    break;
                                                case 23:
                                                    {
                                                        SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                        SQL_Verse.AddParam("@vals", myMethods.ToDecimal(dataGridView1.Rows[y].Cells[i].Value.ToString()));
                                                        string header = "month" + j;
                                                        string cmdUpdate4 = "UPDATE " + tbl_MCOMedicaid + " SET " + header + "=@vals WHERE ID_Num=@PrimKey;";
                                                        SQL_Verse.ExecQuery(cmdUpdate4);
                                                    }
                                                    break;
                                                case 28:
                                                    {
                                                        SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                        SQL_Verse.AddParam("@vals", myMethods.ToDecimal(dataGridView1.Rows[y].Cells[i].Value.ToString()));
                                                        string header = "month" + j;
                                                        string cmdUpdate5 = "UPDATE " + tbl_VA + " SET " + header + "=@vals WHERE ID_Num=@PrimKey;";
                                                        SQL_Verse.ExecQuery(cmdUpdate5);
                                                    }
                                                    break;
                                                case 33:
                                                    {
                                                        SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                                        SQL_Verse.AddParam("@vals", myMethods.ToDecimal(dataGridView1.Rows[y].Cells[i].Value.ToString()));
                                                        string header = "month" + j;
                                                        string cmdUpdate6 = "UPDATE " + tbl_Other + " SET " + header + "=@vals WHERE ID_Num=@PrimKey;";
                                                        SQL_Verse.ExecQuery(cmdUpdate6);
                                                    }
                                                    break;
                                            }
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
                    case 7:
                    case 12:
                    case 17:
                    case 22:
                    case 27:
                    case 32:
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
                            case "% Medicare":
                            case "% Medicaid":
                            case "% Private Pay":
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
                            case "Flat Rate":
                            case "State Basis":
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
                case 8:
                case 13:
                case 18:
                case 23:
                case 28:
                case 33:
                    {
                        if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == DBNull.Value || dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null) return;
                        var val = myMethods.ToPercent(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = val;
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
                        case 9:
                        case 14:
                        case 19:
                        case 24:
                        case 29:
                        case 34:
                            {
                                switch (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex - 2].Value.ToString())
                                {
                                    case "":
                                    case "Flat Rate":
                                    case "State Basis":
                                        {
                                            return;
                                        }
                                    default:
                                        {
                                            dataGridView1.CurrentCell = dataGridView1.Rows[Slct].Cells[e.ColumnIndex - 1];
                                            dtlRoll_Percent_Major frmDetail = new dtlRoll_Percent_Major();
                                            frmDetail.Show(dataGridView1);
                                        }
                                        break;
                                }
                            }
                            break;
                        case 11:
                        case 16:
                        case 21:
                        case 26:
                        case 31:
                        case 36:
                            {
                                switch (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex - 4].Value.ToString())
                                {
                                    case "":
                                    case "% Medicare":
                                    case "% Medicaid":
                                    case "% Private Pay":
                                        {
                                            return;
                                        }
                                    default:
                                        {
                                            dataGridView1.CurrentCell = dataGridView1.Rows[Slct].Cells[e.ColumnIndex - 1];
                                            dataGridView1.Rows[Slct].Cells[col - 1].Value = "";
                                            dataGridView1.Rows[Slct].Cells[col - 1].Value = "Detail";
                                        }
                                        break;
                                }
                            }
                            break;

                        default:
                            {
                                if (dataGridView1.CurrentCell == null)
                                {
                                    return;
                                }
                                else
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

        public virtual void CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView1 = (DataGridView)sender;
            int j;

            dataGridView1.Tag = dataGridView1.Parent.Parent.Parent;

            Form frm = (Form)dataGridView1.Tag;
            if (frm.ActiveControl.Name == "btnAdd" || frm.ActiveControl.Name == "btnDelete" || frm.ActiveControl.Name == "btnCancel") return;

            // GET AGE OF BUILDNIG
            try
            {
                switch (e.ColumnIndex)
                {
                    case 3:
                        {   
                            var switchExpr = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                            switch (switchExpr)
                            {
                                case "Configure":
                                    {
                                        FormConfigure_Weight frmDetail = new FormConfigure_Weight();
                                        frmDetail.Show(dataGridView1);
                                    }
                                    break;
                                case "Detail":
                                    {
                                        dtlRoll_Collection_Major frmDetail = new dtlRoll_Collection_Major();
                                        frmDetail.Show(dataGridView1);
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;

                    case 5:
                        {
                            var switchExpr = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                            switch (switchExpr)
                            {
                                case "Configure":
                                    {
                                        FormConfigure_PPS frmDetail = new FormConfigure_PPS();
                                        frmDetail.Show(dataGridView1);
                                        index = 5;
                                    }
                                    break;
                                case "Detail":
                                    {
                                        dtlRoll_Collection_Major frmDetail = new dtlRoll_Collection_Major();
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
                            var switchExpr = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                            switch (switchExpr)
                            {
                                case "Configure":
                                    {
                                        if (Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex - 3].Value) == "Flat Rate")
                                        {
                                            FormConfigure_Medicaid frmDetail = new FormConfigure_Medicaid();
                                            frmDetail.Show(dataGridView1);
                                            index = 10;
                                        }
                                    }
                                    break;
                                case "Detail":
                                    {
                                        dtlRoll_Collection_Major frmDetail = new dtlRoll_Collection_Major();
                                        frmDetail.Show(dataGridView1);
                                    }
                                    break;
                                default:
                                    break;
                            }

                        }
                        break;
                    case 15:
                        {
                            var switchExpr = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                            switch (switchExpr)
                            {
                                case "Configure":
                                    {
                                        if (Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex - 3].Value) == "Flat Rate")
                                        {
                                            FormConfigure_PrivatePay frmDetail = new FormConfigure_PrivatePay();
                                            frmDetail.Show(dataGridView1);
                                            index = 15;
                                        }
                                    }
                                    break;
                                case "Detail":
                                    {
                                        dtlRoll_Collection_Major frmDetail = new dtlRoll_Collection_Major();
                                        frmDetail.Show(dataGridView1);
                                    }
                                    break;
                                default:
                                    break;
                            }

                        }
                        break;
                    case 20:
                        {
                            var switchExpr = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                            switch (switchExpr)
                            {
                                case "Configure":
                                    {
                                        if (Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex - 3].Value) == "Flat Rate")
                                        {
                                            FormConfigure_MCOcare frmDetail = new FormConfigure_MCOcare();
                                            frmDetail.Show(dataGridView1);
                                            index = 20;
                                        }
                                    }
                                    break;
                                case "Detail":
                                    {
                                        dtlRoll_Collection_Major frmDetail = new dtlRoll_Collection_Major();
                                        frmDetail.Show(dataGridView1);
                                    }
                                    break;
                                default:
                                    break;
                            }

                        }
                        break;
                    case 25:
                        {
                            var switchExpr = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                            switch (switchExpr)
                            {
                                case "Configure":
                                    {
                                        if (Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex - 3].Value) == "Flat Rate")
                                        {
                                            FormConfigure_MCOcaid frmDetail = new FormConfigure_MCOcaid();
                                            frmDetail.Show(dataGridView1);
                                            index = 25;
                                        }
                                    }
                                    break;
                                case "Detail":
                                    {
                                        dtlRoll_Collection_Major frmDetail = new dtlRoll_Collection_Major();
                                        frmDetail.Show(dataGridView1);
                                    }
                                    break;
                                default:
                                    break;
                            }

                        }
                        break;
                    case 30:
                        {
                            var switchExpr = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                            switch (switchExpr)
                            {
                                case "Configure":
                                    {
                                        if (Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex - 3].Value) == "Flat Rate")
                                        {
                                            FormConfigure_Vets frmDetail = new FormConfigure_Vets();
                                            frmDetail.Show(dataGridView1);
                                            index = 30;
                                        }
                                    }
                                    break;
                                case "Detail":
                                    {
                                        dtlRoll_Collection_Major frmDetail = new dtlRoll_Collection_Major();
                                        frmDetail.Show(dataGridView1);
                                    }
                                    break;
                                default:
                                    break;
                            }

                        }
                        break;
                    case 35:
                        {
                            var switchExpr = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                            switch (switchExpr)
                            {
                                case "Configure":
                                    {
                                        if (Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex - 3].Value) == "Flat Rate")
                                        {
                                            FormConfigure_Other frmDetail = new FormConfigure_Other();
                                            frmDetail.Show(dataGridView1);
                                            index = 35;
                                        }
                                    }
                                    break;
                                case "Detail":
                                    {
                                        dtlRoll_Collection_Major frmDetail = new dtlRoll_Collection_Major();
                                        frmDetail.Show(dataGridView1);
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    case 7:
                        {
                            tbl_Change = "dtbRollDetailPct_Medicaid";
                        }
                        break;
                    case 12:
                        {
                            tbl_Change = "dtbRollDetailPct_PrivatePay";
                        }
                        break;
                    case 17:
                        {
                            tbl_Change = "dtbRollDetailPct_MCOMedicare";
                        }
                        break;
                    case 22:
                        {
                            tbl_Change = "dtbRollDetailPct_MCOMedicaid";
                        }
                        break;
                    case 27:
                        {
                            tbl_Change = "dtbRollDetailPct_VA";
                        }
                        break;
                    case 32:
                        {
                            tbl_Change = "dtbRollDetailPct_Other";
                        }
                        break;
                    default:
                        {
                            return;
                        }
                }

                switch (e.ColumnIndex)
                {
                    case 7:
                    case 12:
                    case 17:
                    case 22:
                    case 27:
                    case 32:
                        {
                            // CLEAR SQL & DATA GRID VALUES ON CHANGE
                            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value = "";
                            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 3].Value = "";
                            for (j = 1; j <= myMethods.Period * Mos_Const; j++)
                            {
                                SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                                SQL_Verse.AddParam("@vals", DBNull.Value); // SUBMIT VALUES OF NEXT CELL OVER
                                string header = "month" + j;
                                string cmdUpdate = "UPDATE " + tbl_Change + " SET " + header + "=@vals WHERE ID_Num=@PrimKey;";
                                SQL_Verse.ExecQuery(cmdUpdate);
                            }
                        }
                        break;
                    default:
                        {
                            return;   
                        }
                }  
            }
            catch (Exception ex)
            {

            }
        }
    }
}

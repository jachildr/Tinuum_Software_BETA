using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using Microsoft.VisualBasic;
using Tinuum_Software_BETA.Popups.Inventory;
using Tinuum_Software_BETA.Detail_Inherit.Inventory;
using System.Runtime.Remoting.Messaging;

namespace Tinuum_Software_BETA.Icon_Masters
{
    [CLSCompliant(true)]
    public partial class FormInventory : Tinuum_Software_BETA.FormMaster
    {
        protected SQLControl SQL_Verse = new SQLControl(); 
        protected SQLControl SQL_Name = new SQLControl();
        protected SQLControl SQL_Query = new SQLControl();
        protected string tbl_Star = "dtbInventoryDetail_Star";
        protected string tbl_SF = "dtbInventoryDetail_SF";
        protected string tbl_dtlDynSF = "dtbInventoryDetailDynamic_SF";
        protected string tbl_dtlSF = "dtbInventoryDynamic_SF";
        protected string tbl_Vacant = "dtbInventoryDetail_Vacancy";
        protected string tbl_Roster1 = "dtbRosterVerse";
        protected string tbl_Roster2 = "dtbRosterVerse2";
        protected string tbl_Roster3 = "dtbRosterVerse3";
        protected string tbl_Roster4 = "dtbRosterVerse4";
        protected string tbl_Roster5 = "dtbRosterVerse5";
        protected int terminate;
        protected int exit;
        protected int priorValue;
        public FormInventory()
        {
            InitializeComponent();
            tbl_Name = "dtbInventoryVerse";
        }

        public override void LoadGrid()
        {
            int i;
            int r;
            int j;
            var cmbo1 = new DataGridViewComboBoxColumn();
            var cmbo2 = new DataGridViewComboBoxColumn();
            var btn1 = new DataGridViewButtonColumn();
            var btn2 = new DataGridViewButtonColumn();
            var btn3 = new DataGridViewButtonColumn();
            var btn4 = new DataGridViewButtonColumn();

            string tbl_Facility = "dtbHome";
            string slctCol = "[Facility Name]";
            int prime;

            terminate = 1;

            SQL_Verse.ExecQuery("SELECT * FROM " + tbl_Name + ";");
            // ADD SUBJECT IF NO RECORD
            if (SQL_Verse.RecordCount > 0)
            {
                SQL_Name.ExecQuery("SELECT * FROM " + tbl_Facility + ";");
                string facility = SQL_Name.DBDT.Rows[0][1].ToString();
                string cmdUpdate;

                // UPDATE FACILITY NAME
                prime = Convert.ToInt32(SQL_Verse.DBDT.Rows[0][0]);
                SQL_Verse.AddParam("@PrimeKey", prime);
                SQL_Verse.AddParam("@CaseName", facility);
                cmdUpdate = "UPDATE " + tbl_Name + " SET " + slctCol + "=@CaseName WHERE ID_Num=@PrimeKey;";
                SQL_Verse.ExecQuery(cmdUpdate);
            }
            else
            {
                SQL_Name.ExecQuery("SELECT * FROM " + tbl_Facility + ";");
                string facility = SQL_Name.DBDT.Rows[0][1].ToString();
                
                // INSERT NEW ROW INTO MAIN TABLE
                string cmdInsert1 = "INSERT INTO " + tbl_Name + " ([Facility Name]) VALUES ('" + facility + "');";
                
                // SELECT TABLE TO GET NEW ROWS
                SQL_Verse.ExecQuery("SELECT * FROM " + tbl_Name + ";");
                
                // INSERT NEW ROW INTO SUPPORTING TABLES
                prime = Convert.ToInt32(SQL_Verse.DBDT.Rows[0][0]);
                string cmdInsert2 = "INSERT INTO " + tbl_SF + " (ID_Num) VALUES ("+ prime +");";
                string cmdInsert3 = "INSERT INTO " + tbl_Star + " (ID_Num) VALUES (" + prime + ");";
                string cmdInsert4 = "INSERT INTO " + tbl_dtlSF + " (ID_Num) VALUES (" + prime + ");";
                string cmdInsert5 = "INSERT INTO " + tbl_dtlDynSF + " (ID_Num) VALUES (" + prime + ");";
                string cmdInsert6 = "INSERT INTO " + tbl_Vacant + " (ID_Num) VALUES (" + prime + ");";
                SQL_Verse.ExecQuery(cmdInsert1);
                SQL_Verse.ExecQuery(cmdInsert2);
                SQL_Verse.ExecQuery(cmdInsert3);
                SQL_Verse.ExecQuery(cmdInsert4);
                SQL_Verse.ExecQuery(cmdInsert5);
                SQL_Verse.ExecQuery(cmdInsert6);

                // CALL QUERY TO CREATE LIFE DATABASE
                SQLQueries.tblInventoryLifeCreate();
            }

            // COLUMN CONTROLS
            {
                // ADD SPECS FOR COMBOBOX1
                cmbo1.Items.Add("");
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
            }

            // REFRESH ROWS & COLUMNS
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
                                case 15:
                                    {
                                        dataGridView1.Columns.Add(btn2);
                                    }
                                    break;
                                case 17:
                                    {
                                        dataGridView1.Columns.Add(btn3);
                                    }
                                    break;
                                case 19:
                                    {
                                        dataGridView1.Columns.Add(btn4);
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
                                case 14:
                                    {
                                        dataGridView1.Columns.Add(cmbo1);
                                    }
                                    break;
                                case 16:
                                    {
                                        dataGridView1.Columns.Add(cmbo2);
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
            dataGridView1.RowCount = SQL.RecordCount;

            // FILL DATAGRID FROM DATA TABLE
            for (r = 0; r <= SQL.RecordCount - 1; r++)
            {
                for (i = 0; i <= Col_Count - 1; i++)
                {
                    dataGridView1.Rows[r].Cells[i].Value = SQL.DBDT.Rows[r][i];
                }
            }

            // MAKE ROWS IN COLUMN 1 READ ONLY AND NUMBER IN ORDER
            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                dataGridView1.Rows[i].Cells[1].Value = i + 1;
                dataGridView1.Rows[i].Cells[1].ReadOnly = true;
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
            dataGridView1.Columns[11].Width = 120;

            // DISABLE COLUMNS
            for (i = 0; i <= dataGridView1.ColumnCount - 1; i++)
            {
                if (new int[] { 3, 5, }.Contains(i))
                {
                    dataGridView1.Columns[i].ReadOnly = true;
                    dataGridView1.Columns[i].DefaultCellStyle.SelectionBackColor = SystemColors.Control;
                    dataGridView1.Columns[i].DefaultCellStyle.SelectionForeColor = SystemColors.ControlDark;
                    dataGridView1.Columns[i].DefaultCellStyle.BackColor = SystemColors.Control;
                    dataGridView1.Columns[i].DefaultCellStyle.ForeColor = SystemColors.ControlDark;
                }
            }

            dataGridView1.Rows[0].Cells[2].ReadOnly = true;
            dataGridView1.Rows[0].Cells[2].Style.SelectionBackColor = SystemColors.Control;
            dataGridView1.Rows[0].Cells[2].Style.SelectionForeColor = SystemColors.ControlDark;
            dataGridView1.Rows[0].Cells[2].Style.BackColor = SystemColors.Control;
            dataGridView1.Rows[0].Cells[2].Style.ForeColor = SystemColors.ControlDark;

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
                    if (new int[] { 10, 11, 12, 13 }.Contains(j))
                    {
                        if (dataGridView1.Rows[i].Cells[8].Value != DBNull.Value && dataGridView1.Rows[i].Cells[9].Value != DBNull.Value)
                        {
                            dataGridView1.Rows[i].Cells[j].ReadOnly = false;
                            dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                            dataGridView1.Rows[i].Cells[j].Style.ForeColor = Color.Black;
                            dataGridView1.Rows[i].Cells[j].Style.SelectionBackColor = SystemColors.Highlight;
                            dataGridView1.Rows[i].Cells[j].Style.SelectionForeColor = Color.White;
                        }
                        else
                        {
                            dataGridView1.Rows[i].Cells[j].ReadOnly = true;
                            dataGridView1.Rows[i].Cells[j].Style.SelectionBackColor = SystemColors.Control;
                            dataGridView1.Rows[i].Cells[j].Style.SelectionForeColor = SystemColors.ControlDark;
                            dataGridView1.Rows[i].Cells[j].Style.BackColor = SystemColors.Control;
                            dataGridView1.Rows[i].Cells[j].Style.ForeColor = SystemColors.ControlDark;
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

            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                if (i > 0)
                {
                    dataGridView1.Rows[i].Cells[3].Value = "Competitor";
                }
                else
                {
                    dataGridView1.Rows[i].Cells[3].Value = "Subject";
                }
            }

            // CALL METHOD
            Percent_Change();

            terminate = 0;
        }

        public void Percent_Change()
        {
            int i;
            string strNum;
            double intNum;

            // FORMAT FILLED DB DATA
            try
            {
                for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                {
                    if (dataGridView1.Rows[i].Cells[18].ReadOnly == false)
                    {
                        strNum = dataGridView1.Rows[i].Cells[18].Value.ToString();
                        if (Information.IsNumeric(strNum) == true)
                        {
                            intNum = Convert.ToDouble(strNum);
                            dataGridView1.Rows[i].Cells[18].Value = String.Format("{0:p}", intNum);
                        }
                    }  
                }
            }
            catch (Exception ex)
            {
            }
        }

        public override void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (terminate > 0) return;
            string title = "TINUUM SOFTWARE";
            // GET AGE OF BUILDNIG
            try
            {
                switch (e.ColumnIndex)
                {
                    case 4:
                        {
                            if (Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) > DateTime.Today)
                            {
                                dataGridView1.Rows[e.RowIndex].Cells[5].Value = "Development";
                            }
                            else
                            {
                                TimeSpan duration = DateTime.Today.Subtract(Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value));
                                dataGridView1.Rows[e.RowIndex].Cells[5].Value = Convert.ToDouble(duration.Days / 365);
                            }
                        }
                        break;
                    case 14:
                        {
                            if (Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) == "Detail")
                            {
                                FormInventoryLifeCycle frmDetail = new FormInventoryLifeCycle();
                                frmDetail.Show(this);
                            }
                            
                        }
                        break;
                    case 16:
                        {
                            var switchExpr = dataGridView1.Rows[e.RowIndex].Cells[16].Value;
                            switch (switchExpr)
                            {
                                case "Configure":
                                    {
                                        FormConfigure_Star frmDetail = new FormConfigure_Star();
                                        frmDetail.Show(this);
                                    }
                                    break;
                                case "Detail":
                                    {
                                        dtlInventory_Collection frmDetail = new dtlInventory_Collection();
                                        frmDetail.Show(this);
                                        this.Enabled = false;
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    case 8:
                    case 9:
                    case 10:
                    case 11:
                    case 12:
                    case 13:
                        {
                            switch (e.RowIndex)
                            {
                                case 0:
                                    {
                                        if (Information.IsNumeric(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value))
                                        {
                                            if (Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) == priorValue) return;
                                        }
                                        
                                        SQL_Query.ExecQuery("SELECT * FROM " + tbl_Roster1 + ";");
                                        if (SQL_Query.RecordCount > 0)
                                        {
                                            DialogResult prompt = MessageBox.Show("Are you sure? Changes made will reset any saved records in the Roster.", title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                                            if (prompt == DialogResult.Yes)
                                            {
                                                SQL_Query.ExecQuery("DELETE FROM " + tbl_Roster1 + ";");
                                            }
                                            else
                                            {
                                                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = priorValue;
                                            }
                                        }
                                    }
                                    break;
                                case 1:
                                    {
                                        if (Information.IsNumeric(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value))
                                        {
                                            if (Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) == priorValue) return;
                                        }

                                        SQL_Query.ExecQuery("SELECT * FROM " + tbl_Roster2 + ";");
                                        if (SQL_Query.RecordCount > 0)
                                        {
                                            DialogResult prompt = MessageBox.Show("Are you sure? Changes made will reset any saved records in the Roster.", title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                                            if (prompt == DialogResult.Yes)
                                            {
                                                SQL_Query.ExecQuery("DELETE FROM " + tbl_Roster2 + ";");
                                            }
                                            else
                                            {
                                                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = priorValue;
                                            }
                                        }
                                    }
                                    break;
                                case 2:
                                    {
                                        if (Information.IsNumeric(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value))
                                        {
                                            if (Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) == priorValue) return;
                                        }

                                        SQL_Query.ExecQuery("SELECT * FROM " + tbl_Roster3 + ";");
                                        if (SQL_Query.RecordCount > 0)
                                        {
                                            DialogResult prompt = MessageBox.Show("Are you sure? Changes made will reset any saved records in the Roster.", title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                                            if (prompt == DialogResult.Yes)
                                            {
                                                SQL_Query.ExecQuery("DELETE FROM " + tbl_Roster3 + ";");
                                            }
                                            else
                                            {
                                                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = priorValue;
                                            }
                                        }
                                    }
                                    break;
                                case 3:
                                    {
                                        if (Information.IsNumeric(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value))
                                        {
                                            if (Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) == priorValue) return;
                                        }

                                        SQL_Query.ExecQuery("SELECT * FROM " + tbl_Roster4 + ";");
                                        if (SQL_Query.RecordCount > 0)
                                        {
                                            DialogResult prompt = MessageBox.Show("Are you sure? Changes made will reset any saved records in the Roster.", title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                                            if (prompt == DialogResult.Yes)
                                            {
                                                SQL_Query.ExecQuery("DELETE FROM " + tbl_Roster4 + ";");
                                            }
                                            else
                                            {
                                                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = priorValue;
                                            }
                                        }
                                    }
                                    break;
                                case 4:
                                    {
                                        if (Information.IsNumeric(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value))
                                        {
                                            if (Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) == priorValue) return;
                                        }

                                        SQL_Query.ExecQuery("SELECT * FROM " + tbl_Roster5 + ";");
                                        if (SQL_Query.RecordCount > 0)
                                        {
                                            DialogResult prompt = MessageBox.Show("Are you sure? Changes made will reset any saved records in the Roster.", title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                                            if (prompt == DialogResult.Yes)
                                            {
                                                SQL_Query.ExecQuery("DELETE FROM " + tbl_Roster5 + ";");
                                            }
                                            else
                                            {
                                                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = priorValue;
                                            }
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                    default:
                        break;
                }
                
            }
            catch (Exception ex)
            {

            }
        }

        public override void DynamicCTLRs()
        {
            int Counter = dataGridView1.RowCount - 1;
            int c;
            int x = 0;
            int y = 0;
            int z = 0;
            int Width = 0;
            int Height = 0;
            int cRight = 3;
            Rectangle rect; // STORES A SET OF FOUR INTEGERS
            string dteStr = "(d)";

            for (c = 0; c <= dataGridView1.ColumnCount - 1; c++)
            {
                var switchExpr = Header_Name[c].Substring(Header_Name[c].Length - cRight, cRight);
                switch (switchExpr)
                {
                    case var case2 when case2 == dteStr:
                        {
                            for (z = 0; z <= Counter; z++)
                            {
                                var gridDte = new DateTimePicker();
                                gridDte.Name = (Header_Rename[c]).ToString().Trim() + z;
                                try
                                {
                                    gridDte.Value = Convert.ToDateTime(dataGridView1.Rows[z].Cells[c].Value.ToString());   // SET EQUAL TO CORRESPONDING DTGV CELL TEXT (CHECK)
                                }
                                catch (Exception ex)
                                {
                                    gridDte.Value = DateTime.Today;
                                }

                                gridDte.Format = DateTimePickerFormat.Custom;
                                gridDte.CustomFormat = "MMM yyyy";
                                dataGridView1.Controls.Add(gridDte);
                                // POSITION
                                rect = dataGridView1.GetCellDisplayRectangle(c, z, false);
                                x = rect.X;
                                y = rect.Y;
                                Width = rect.Width;
                                Height = rect.Height;
                                // BIND TO CELL
                                gridDte.SetBounds(x, y, Width, Height);
                                gridDte.Visible = true;
                                // ADD HANDLER
                                gridDte.Enter += new EventHandler(HandleDynamicDate_Enter);
                                gridDte.Leave += new EventHandler(HandleDynamicDate_Leave);
                            }
                            break;
                        }
                }
            }
        }

        public override void frmAltVerse_Master_Load(object sender, EventArgs e)
        {
            base.frmAltVerse_Master_Load(sender, e);
            DynamicCTLRs();
        }

        private void check_Fill()
        {
            int i;
            int j;
            int sumBeds = default(int);
            int sumUnits = default(int);
            int beds = default(int);
            int units = default(int);
            string title = "TINUUM SOFTWARE";

            for (i = 0; i <= dataGridView1.Rows.Count - 1; i++)
            {
                if (dataGridView1.Rows[i].Cells[8].Value == DBNull.Value || dataGridView1.Rows[i].Cells[8].Value == DBNull.Value)
                {
                    MessageBox.Show("You have not entered values for units and / or beds. Retry.", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dataGridView1.CurrentCell = dataGridView1.Rows[i].Cells[8];
                    dataGridView1.Rows[i].Cells[9].Selected = true;
                    exit = 1;
                    return;
                }
                // ASSIGN AND CLEAR UNITS/ BEDS
                sumUnits = 0;
                sumBeds = 0;
                units = Convert.ToInt32(dataGridView1.Rows[i].Cells[8].Value);
                beds = Convert.ToInt32(dataGridView1.Rows[i].Cells[9].Value);
                for (j = 10; j <= 13; j++)
                {
                    if (dataGridView1.Rows[i].Cells[j].Value == DBNull.Value)
                    {
                        dataGridView1.Rows[i].Cells[j].Value = 0;
                    }
                }
                for (j = 10; j <= 13; j++)
                {
                    sumUnits += Convert.ToInt32(dataGridView1.Rows[i].Cells[j].Value);
                }
                if (units != sumUnits)
                {
                    MessageBox.Show("Units must sum to total. Retry.", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dataGridView1.CurrentCell = dataGridView1.Rows[i].Cells[8];
                    exit = 1;
                    return;
                }
                for (j = 10; j <= 13; j++)
                {
                    sumBeds += Convert.ToInt32(dataGridView1.Rows[i].Cells[j].Value) * (j-9);
                }
                if (beds != sumBeds)
                {
                    MessageBox.Show("Beds must sum to total. Retry.", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dataGridView1.CurrentCell = dataGridView1.Rows[i].Cells[9];
                    exit = 1;
                    return;
                }
            }
            exit = 0;
        }

        public override void btnSubmit_Click(object sender, EventArgs e)
        {
            UpdateSQL();
            if (exit > 0) return;
            this.Dispose();
        }
        public override void UpdateSQL()
        {
            int i;
            int y;
            int j;
            int cRight = 3;
            string btnString = "(b)";
            var commandBuilder = new System.Data.SqlClient.SqlCommandBuilder(SQL.DBDA);
            string cmdUpdate;
            string title = "TINUUM SOFTWARE";

            check_Fill();

            if (exit > 0) return;

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
                        if (Header_Name[y].Substring(Header_Name[y].Length - cRight, cRight).Equals(btnString))
                        {
                            // Do Nothing
                        }
                        else if (dataGridView1.Rows[i].Cells[y].Value == DBNull.Value || Convert.ToString(dataGridView1.Rows[i].Cells[y].Value) == "Configure")
                        {
                            MessageBox.Show("You must enter relevant values for all fields before continuing.", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            dataGridView1.CurrentCell = dataGridView1.Rows[i].Cells[y];
                            exit = 1;
                            return;
                        }
                    }
                }
            }

            for (y = 0; y <= dataGridView1.RowCount - 1; y++)
            {
                for (i = 1; i <= dataGridView1.ColumnCount - 1; i++)
                {
                    switch (i)
                    {
                        case 6:
                            {
                                // ADD PARAMS
                                SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                SQL_Verse.AddParam("@vals", dataGridView1.Rows[y].Cells[i].Value);
                                // UPDATE STATEMENT FOR MAINVERSE
                                cmdUpdate = "UPDATE " + tbl_Name + " SET " + Headers_Submit[i] + "=@vals WHERE ID_Num=@PrimKey;";
                                SQL_Verse.ExecQuery(cmdUpdate);

                                // UPDATE STATEMENT FOR DYNAMIC IF NUMERIC
                                if (Information.IsNumeric(dataGridView1.Rows[y].Cells[i].Value))
                                {
                                    for (j = 1; j <= myMethods.Period * Mos_Const; j++)
                                    {
                                        SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                        SQL_Verse.AddParam("@vals", dataGridView1.Rows[y].Cells[i].Value);
                                        string header = "month" + j;
                                        cmdUpdate = "UPDATE " + tbl_SF + " SET " + header +"=@vals WHERE ID_Num=@PrimKey;";
                                        SQL_Verse.ExecQuery(cmdUpdate);
                                    }
                                    // SET DYNAMIC YEARLY TO NULL
                                    SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                    SQL_Verse.AddParam("@val1", 1);
                                    SQL_Verse.AddParam("@val2", DBNull.Value);
                                    string colName1 = "Choose";
                                    string colName2 = "Selection";
                                    cmdUpdate = "UPDATE " + tbl_dtlSF + " SET " + colName1 + "=@val1, " + colName2 + "=@val2 WHERE ID_Num=@PrimKey;";
                                    SQL_Verse.ExecQuery(cmdUpdate);
                                }
                                
                            }
                            break;
                        case 18:
                            {
                                // ADD PARAMS
                                SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                SQL_Verse.AddParam("@vals", dataGridView1.Rows[y].Cells[i].Value);
                                // UPDATE STATEMENT FOR MAINVERSE
                                cmdUpdate = "UPDATE " + tbl_Name + " SET " + Headers_Submit[i] + "=@vals WHERE ID_Num=@PrimKey;";
                                SQL_Verse.ExecQuery(cmdUpdate);

                                // UPDATE STATEMENT FOR PERCENT
                                string percent = dataGridView1.Rows[y].Cells[i].Value.ToString(); // SUBMIT VALUES OF NEXT CELL OVER
                                if (Information.IsNumeric(percent.Substring(0, percent.Length - 1)))
                                {
                                    for (j = 1; j <= myMethods.Period * Mos_Const; j++)
                                    {
                                        
                                        SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                        SQL_Verse.AddParam("@vals", myMethods.ToDecimal(dataGridView1.Rows[y].Cells[i].Value.ToString()));
                                        string header = "month" + j;
                                        cmdUpdate = "UPDATE " + tbl_Vacant + " SET " + header + "=@vals WHERE ID_Num=@PrimKey;";
                                        SQL_Verse.ExecQuery(cmdUpdate);
                                    }
                                }
                            }
                            break;
                        default:
                            {
                                SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                if (Header_Name[i].Substring(Header_Name[i].Length - cRight, cRight).Equals(btnString))
                                {
                                    SQL_Verse.AddParam("@vals", null);
                                }
                                else
                                {
                                    SQL_Verse.AddParam("@vals", dataGridView1.Rows[y].Cells[i].Value);
                                }

                                cmdUpdate = "UPDATE " + tbl_Name + " SET " + Headers_Submit[i] + "=@vals WHERE ID_Num=@PrimKey;";
                                SQL_Verse.ExecQuery(cmdUpdate);
                            }
                            break;
                    }
                }
            }
        }
    
        public override void InsertUser()
        {
            int i;
            int y;
            string Title = "TINUUM SOFTWARE";
            int cRight = 3;
            string btnString = "(b)";
            int num;
            int count;

            if (dataGridView1.RowCount >= 5)
            {
                MessageBox.Show("Maximum of five entries.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            check_Fill();

            if (exit > 0) return;

            for (y = 0; y <= dataGridView1.ColumnCount - 1; y++)
            {
                for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                {
                    if (Header_Name[y].Substring(Header_Name[y].Length - cRight, cRight).Equals(btnString))
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

            base.UpdateSQL();
            // INSERT NEWEST VERSE COLUMN
            SQL.ExecQuery("INSERT INTO " + tbl_Name + " DEFAULT VALUES;");
            
            // GET UPDATED ROW COUNT
            SQL_Verse.ExecQuery("SELECT * FROM " + tbl_Name + ";");
            count = SQL_Verse.RecordCount - 1;
            num = Convert.ToInt32(SQL_Verse.DBDT.Rows[count][0].ToString());
            
            // INSERT IDENTITY NUM INTO SIPPORTING DATABASES
            string cmdInsert1 = "INSERT INTO " + tbl_SF + " (ID_Num) VALUES ("+ num +");";
            string cmdInsert2 = "INSERT INTO " + tbl_Star + " (ID_Num) VALUES (" + num + ");";
            string cmdInsert3 = "INSERT INTO " + tbl_dtlSF + " (ID_Num) VALUES (" + num + ");";
            string cmdInsert4 = "INSERT INTO " + tbl_dtlDynSF + " (ID_Num) VALUES (" + num + ");";
            string cmdInsert5 = "INSERT INTO " + tbl_Vacant + " (ID_Num) VALUES (" + num + ");";
            SQL_Verse.ExecQuery(cmdInsert1);
            SQL_Verse.ExecQuery(cmdInsert2);
            SQL_Verse.ExecQuery(cmdInsert3);
            SQL_Verse.ExecQuery(cmdInsert4);
            SQL_Verse.ExecQuery(cmdInsert5);

            // CALL QUERY TO CREATE LIFE DATABASE
            SQLQueries.tblInventoryLifeCreate();
            
            // CALL METHODS
            Add_Source();
            LoadGrid();
            
            // REPLACE DYNAMIC CONTROLS
            base.DeleteCTRLs();
            DynamicCTLRs();
        }

        public override void Cancel()
        {
            int i;
            int y;
            int j;
            string Title = "TINUUM SOFTWARE";
            int cRight = 3;
            string btnString = "(b)";
            string cmdUpdate;
            string prefix_Life = "dtbInventoryLife";
            string dltTable;

            DialogResult prompt = MessageBox.Show("Are you sure? Any unsubmitted data will be lost.", Title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            Rslt_Cncl = prompt.ToString();

            if (prompt == DialogResult.Yes)
            {
                if (dataGridView1.RowCount != 0)
                {
                    // CLEAR GRID AND RESET WITH ORIGINAL TABLE
                    SQL_Verse.ExecQuery("SELECT * FROM " + tbl_Name + ";");
                    dataGridView1.Rows.Clear();
                    dataGridView1.Columns.Clear();
                    dataGridView1.DataSource = SQL_Verse.DBDT;

                    // DELETE ROWS FROM RELEVANT TABLES
                    for (y = 0; y <= dataGridView1.ColumnCount - 1; y++)
                    {
                        for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                        {
                            if (i == 0) continue;
                            if (Header_Name[y].Substring(Header_Name[y].Length - cRight, cRight).Equals(btnString))
                            {
                                // Do Nothing
                            }
                            else if (string.IsNullOrEmpty(dataGridView1.Rows[i].Cells[y].Value.ToString()))
                            {
                                SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[i].Cells[0].Value.ToString());
                                SQL_Verse.ExecQuery("DELETE FROM " + tbl_Name + " WHERE ID_Num=@PrimKey;");
                                SQL_Verse.ExecQuery("DELETE FROM " + tbl_SF + " WHERE ID_Num=@PrimKey;");
                                SQL_Verse.ExecQuery("DELETE FROM " + tbl_Star + " WHERE ID_Num=@PrimKey;");
                                SQL_Verse.ExecQuery("DELETE FROM " + tbl_dtlSF + " WHERE ID_Num=@PrimKey;");
                                SQL_Verse.ExecQuery("DELETE FROM " + tbl_dtlDynSF + " WHERE ID_Num=@PrimKey;");
                                SQL_Verse.ExecQuery("DELETE FROM " + tbl_Vacant + " WHERE ID_Num=@PrimKey;");
                                // DROP TABLE
                                dltTable = prefix_Life + dataGridView1.Rows[i].Cells[0].Value;
                                SQL_Verse.ExecQuery("DROP TABLE " + dltTable + ";");
                            }
                        }
                    }
                }
                else
                {
                    this.Close();
                    return;
                }

                // RESET DYNAMIC CELLS WITH ORIGINAL VALUES
                for (y = 0; y <= dataGridView1.RowCount - 1; y++)
                {
                    for (i = 1; i <= dataGridView1.ColumnCount - 1; i++)
                    {
                        switch (i)
                        {
                            case 6:
                                {
                                    // UPDATE STATEMENT FOR DYNAMIC IF NUMERIC
                                    if (Information.IsNumeric(dataGridView1.Rows[y].Cells[i].Value))
                                    {
                                        for (j = 1; j <= myMethods.Period * Mos_Const; j++)
                                        {
                                            SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                            SQL_Verse.AddParam("@vals", dataGridView1.Rows[y].Cells[i].Value);
                                            string header = "month" + j;
                                            cmdUpdate = "UPDATE " + tbl_dtlDynSF + " SET " + header + "=@vals WHERE ID_Num=@PrimKey;";
                                            SQL_Verse.ExecQuery(cmdUpdate);
                                        }
                                        // SET DYNAMIC YEARLY TO NULL
                                        SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                        SQL_Verse.AddParam("@val1", 1);
                                        SQL_Verse.AddParam("@val2", DBNull.Value);
                                        string colName1 = "Choose";
                                        string colName2 = "Selection";
                                        cmdUpdate = "UPDATE " + tbl_dtlSF + " SET " + colName1 + "=@val1, " + colName2 + "=@val2 WHERE ID_Num=@PrimKey;";
                                        SQL_Verse.ExecQuery(cmdUpdate);
                                    }
                                }
                                break;
                            case 18:
                                {
                                    string percent = dataGridView1.Rows[y].Cells[i].Value.ToString(); // SUBMIT VALUES OF NEXT CELL OVER
                                    if (Information.IsNumeric(percent.Substring(0, percent.Length - 1)))
                                    {
                                        for (j = 1; j <= myMethods.Period * Mos_Const; j++)
                                        {

                                            SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                                            SQL_Verse.AddParam("@vals", myMethods.ToDecimal(dataGridView1.Rows[y].Cells[i].Value.ToString()));
                                            string header = "month" + j;
                                            cmdUpdate = "UPDATE " + tbl_Vacant + " SET " + header + "=@vals WHERE ID_Num=@PrimKey;";
                                            SQL_Verse.ExecQuery(cmdUpdate);
                                        }
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }

            }
            else
            {
                return;
            }
            Close();
        }
        public override void btnDelete_Click(object sender, EventArgs e)
        {
            int r;
            string Title = "TINUUM SOFTWARE";
            string prefix_Life = "dtbInventoryLife";
            string dltTable;

            if (dataGridView1.CurrentRow.Index > 0)
            {
                DialogResult prompt = MessageBox.Show("Are you sure you want to permanently delete?", Title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                try
                {
                    if (prompt == DialogResult.Yes)
                    {
                        r = dataGridView1.CurrentCell.RowIndex;
                        base.DeleteCTRLs();

                        //DELETE SELECTED ROWS FROM TABLE
                        SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[r].Cells[0].Value.ToString());
                        SQL_Verse.ExecQuery("DELETE FROM " + tbl_Name + " WHERE ID_Num=@PrimKey;");
                        SQL_Verse.ExecQuery("DELETE FROM " + tbl_SF + " WHERE ID_Num=@PrimKey;");
                        SQL_Verse.ExecQuery("DELETE FROM " + tbl_Star + " WHERE ID_Num=@PrimKey;");
                        SQL_Verse.ExecQuery("DELETE FROM " + tbl_dtlSF + " WHERE ID_Num=@PrimKey;");
                        SQL_Verse.ExecQuery("DELETE FROM " + tbl_dtlDynSF + " WHERE ID_Num=@PrimKey;");
                        SQL_Verse.ExecQuery("DELETE FROM " + tbl_Vacant + " WHERE ID_Num=@PrimKey;");

                        //DROP TABLE
                        dltTable = prefix_Life + dataGridView1.Rows[r].Cells[0].Value;
                        SQL_Verse.ExecQuery("DROP TABLE " + dltTable + ";");

                        // CALL METHODS
                        Add_Source();
                        LoadGrid();
                        DynamicCTLRs();
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
            else
            {
                MessageBox.Show("You cannot delete the subject facility. Retry.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
        }

        public override void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            int j;
            string title = "TINUUM SOFTWARE";
            int beds = 0;
            int units = 0;
            int ttlBeds = 0;
            int ttlUnits = 0;

            // CONTROL FOR MY VALUES IN BEDS & UNITS
            {
                switch (e.ColumnIndex)
                {
                    case 6:
                        {
                            if (Information.IsNumeric(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value))
                            {
                                // Nothing
                            }
                            else
                            {
                                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = DBNull.Value;
                                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
                                MessageBox.Show("You must enter a numeric value", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        break;
                    case 8:
                    case 9:
                        {
                            if (Information.IsNumeric(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value))
                            {
                                if (Information.IsNumeric(dataGridView1.Rows[e.RowIndex].Cells[8].Value) && Information.IsNumeric(dataGridView1.Rows[e.RowIndex].Cells[9].Value))
                                {

                                    for (j = 0; j <= dataGridView1.ColumnCount - 1; j++)
                                    {
                                        if (new int[] { 10, 11, 12, 13 }.Contains(j))
                                        {
                                            dataGridView1.Rows[e.RowIndex].Cells[j].ReadOnly = false;
                                            dataGridView1.Rows[e.RowIndex].Cells[j].Style.BackColor = Color.White;
                                            dataGridView1.Rows[e.RowIndex].Cells[j].Style.ForeColor = Color.Black;
                                            dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionBackColor = SystemColors.Highlight;
                                            dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionForeColor = Color.White;
                                        }
                                    }

                                }
                                else
                                {
                                    for (j = 0; j <= dataGridView1.ColumnCount - 1; j++)
                                    {
                                        if (new int[] { 10, 11, 12, 13 }.Contains(j))
                                        {
                                            dataGridView1.Rows[e.RowIndex].Cells[j].ReadOnly = true;
                                            dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionBackColor = SystemColors.Control;
                                            dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionForeColor = SystemColors.ControlDark;
                                            dataGridView1.Rows[e.RowIndex].Cells[j].Style.BackColor = SystemColors.Control;
                                            dataGridView1.Rows[e.RowIndex].Cells[j].Style.ForeColor = SystemColors.ControlDark;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = DBNull.Value;
                                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;

                                for (j = 0; j <= dataGridView1.ColumnCount - 1; j++)
                                {
                                    if (new int[] { 10, 11, 12, 13 }.Contains(j))
                                    {
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Value = DBNull.Value;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].ReadOnly = true;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionBackColor = SystemColors.Control;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.SelectionForeColor = SystemColors.ControlDark;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.BackColor = SystemColors.Control;
                                        dataGridView1.Rows[e.RowIndex].Cells[j].Style.ForeColor = SystemColors.ControlDark;
                                    }
                                }
                                MessageBox.Show("You must enter a numeric value", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        break;
                    case 10:
                    case 11:
                    case 12:
                    case 13:
                        {
                            if (Information.IsNumeric(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value))
                            {
                                for (i = 10; i <= 13; i++)
                                {
                                    if (dataGridView1.Rows[e.RowIndex].Cells[i].Value == DBNull.Value) continue;
                                    switch (i)
                                    {
                                        case 10:
                                            {
                                                units = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[i].Value);
                                                beds = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[i].Value) * 1;
                                            }
                                            break;
                                        case 11:
                                            {
                                                units = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[i].Value);
                                                beds = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[i].Value) * 2;
                                            }
                                            break;
                                        case 12:
                                            {
                                                units = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[i].Value);
                                                beds = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[i].Value) * 3;
                                            }
                                            break;
                                        case 13:
                                            {
                                                units = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[i].Value);
                                                beds = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[i].Value) * 4;
                                            }
                                            break;
                                        default:
                                            break;
                                    }
                                    ttlBeds += beds;
                                    ttlUnits += units;
                                }
                                if (ttlUnits > Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[8].Value) || ttlBeds > Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[9].Value))
                                {
                                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = DBNull.Value;
                                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
                                    MessageBox.Show("Entries must sum to total. Retry.", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                            else
                            {
                                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = DBNull.Value;
                                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
                                MessageBox.Show("You must enter a numeric value", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        break;
                    case 18:
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
        }
        public override void DataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int Slct = dataGridView1.CurrentCell.RowIndex;
            int col = dataGridView1.CurrentCell.ColumnIndex;

            DataGridView senderGrid = (DataGridView)sender;
            try
            {
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
                {
                    switch (e.ColumnIndex)
                    {
                        case 7:
                            {
                                dataGridView1.CurrentCell = dataGridView1.Rows[Slct].Cells[e.ColumnIndex - 1];
                                dtlInventory_Dynamic frmDetail = new dtlInventory_Dynamic();
                                frmDetail.Show(this);
                            }
                            break;
                        case 19:
                            {
                                dataGridView1.CurrentCell = dataGridView1.Rows[Slct].Cells[e.ColumnIndex - 1];
                                dtlInventory_Percent frmDetail = new dtlInventory_Percent();
                                frmDetail.Show(this);
                            }
                            break;
                        default: 
                            {
                                if (dataGridView1.CurrentCell == null)
                                {
                                    return;
                                }

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
        public override void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                    {
                        priorValue = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                    }
                    break;
            }
        }
    }
}

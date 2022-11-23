using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Tinuum_Software_BETA.Detail_Classes.Market;
using Tinuum_Software_BETA.Detail_Masters;
using Tinuum_Software_BETA.Popups;
using Tinuum_Software_BETA.Popups.Market;
using System.Linq;


namespace Tinuum_Software_BETA
{
    [CLSCompliant(true)]
    public partial class FormMarket : Tinuum_Software_BETA.FormMaster
    {

        public SQLControl SQL_ADD = new SQLControl();
        public SQLControl SQL_Source = new SQLControl();
        private int Counts;
        private int Mos_Const = 12;
        private List<string> Headers_Submit = new List<string>();
        private List<string> Header_Name = new List<string>();
        private List<string> Header_Rename = new List<string>();
        private int Col_Count;
        private string tbl_Source;
        private string tbl_Name = "dtbMarketVerse";
        private string tbl_Detail = "dtbMarketDetail";
        private int frmLoading;

        public FormMarket()
        {
            InitializeComponent();
        }

        public override void Add_Source()
        {
            string btnString = "(b)";
            string cmbString = "(c)";
            string dteString = "(d)";
            int colStart = 5;
            int colEnd;
            string Header;
            var colString = new List<string>();
            string strExec;
            int n;
            int i;
            string tbl_Source;

            // UNBIND DATA SOURCE AT BEGINNING FOR UPDATE COMMAND
            dataGridView1.ColumnCount = 0;
            dataGridView1.RowCount = 0;
            dataGridView1.Refresh();

            // LINK DATA SOURCE TO GET COL NAMES
            SQL.ExecQuery("SELECT * FROM " + tbl_Name + ";");
            if (SQL.HasException(true))
                return;
            dataGridView1.DataSource = SQL.DBDT;

            // SET VARIABLES
            colEnd = colStart + myMethods.Period - 1;
            for (n = colStart; n <= colEnd; n++)
            {
                colString.Add(dataGridView1.Columns[n].HeaderText);
            }

            // CHANGE HEADER COLUMNS IN SQL BASED ON DATE CHANGE
            try
            {
                for (i = 1; i <= myMethods.Period; i++)
                {
                    var headDate = myMethods.dteStart.AddMonths((i - 1) * Mos_Const - 1);
                    Header = headDate.ToString("MMM yyyy");
                    strExec = "sp_rename '" + tbl_Name + "." + colString[i - 1] + "', '" + Header + "', 'COLUMN';";
                    SQL.ExecQuery(strExec);
                }
            }
            catch (Exception ex)
            {
            }

            // FILL LIST FROM COLUMN HEADERS
            Col_Count = dataGridView1.ColumnCount;

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

        public override void LoadGrid()
        {
            int i;
            int r;
            var cmboDetail = new DataGridViewComboBoxColumn(); //DUMMY
            var btn = new DataGridViewButtonColumn();

            int colStart = 5;
            // SET FORM LOADING VARIABLE TO STOP EVENT DGV COMBOBOX EVENTS
            frmLoading = 1;

            // HIDE CONTROLS NOT APPLICABLE
            btnAdd.Visible = false;
            btnDelete.Visible = false;

            // SET DATA SOURCE AND RESET DGV
            SQL.ExecQuery("SELECT * FROM " + tbl_Name + ";");
            if (SQL.HasException(true))
                return;

            dataGridView1.ColumnCount = 0;
            dataGridView1.RowCount = 0;
            dataGridView1.Refresh();


            // ADD SPECS FOR BUTTON
            btn.UseColumnTextForButtonValue = true;
            btn.Text = "_";
            btn.FlatStyle = FlatStyle.System;
            btn.DefaultCellStyle.Alignment = (DataGridViewContentAlignment)ContentAlignment.BottomRight;
            btn.DefaultCellStyle.Font = new Font("Arial", 6, FontStyle.Bold);
            dataGridView1.Rows.Clear();

            // CREATE GRIDVIEW COLUMNS
            for (i = 0; i <= Col_Count - 1; i++)
            {
                var switchExpr = Header_Name[i].Substring(Header_Name[i].Length - 3, 3);
                switch (switchExpr)
                {
                    case "(b)":
                        {
                            dataGridView1.Columns.Add(btn);
                            break;
                        }

                    case "(c)":
                        {
                            dataGridView1.Columns.Add(cmboDetail);
                            break;
                        }

                    default:
                        {
                            dataGridView1.Columns.Add("txt", "New Text");
                            break;
                        }
                }
            }

            // SET HEADERS AND NON SORT
            for (i = 0; i <= dataGridView1.ColumnCount - 1; i++)
            {
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView1.Columns[i].HeaderText = Header_Rename[i];
            }

            // SET NUMBER OF ROWS
            dataGridView1.RowCount = SQL.RecordCount;

            // CHANGE TXT GRIDVIEW CELLS TO COMBO CELLS
            for (r = 0; r <= SQL.RecordCount - 1; r++)
            {
                for (i = 0; i <= Col_Count - 1; i++)
                {
                    var switchExpr = i;
                    switch (switchExpr)
                    {
                        case 3:
                            {
                                if (r >= 5)
                                {
                                    var newCell = new DataGridViewComboBoxCell();
                                    // ADD SPECS FOR COMBOBOX
                                    newCell.Items.Add("");
                                    newCell.Items.Add("Configure");
                                    newCell.Items.Add("Detail");
                                    newCell.FlatStyle = FlatStyle.Popup;
                                    newCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                                    newCell.DisplayStyleForCurrentCellOnly = false;
                                    dataGridView1.Rows[r].Cells[i] = newCell;
                                }
                                else
                                {
                                    var newCell = new DataGridViewComboBoxCell();
                                    // ADD SPECS FOR 
                                    newCell.Items.Add("");
                                    newCell.Items.Add("Detail");
                                    newCell.FlatStyle = FlatStyle.Popup;
                                    newCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                                    newCell.DisplayStyleForCurrentCellOnly = false;
                                    dataGridView1.Rows[r].Cells[i] = newCell;
                                }
                                break;
                            }
                        case object _ when switchExpr >= colStart:
                            {
                                var switchExpr1 = r;
                                switch (switchExpr1)
                                {
                                    case 5:
                                        {
                                            tbl_Source = "dtbMarketConfigurePayors";
                                            SQL_Source.ExecQuery("SELECT * FROM " + tbl_Source + ";");
                                            var newCell = new DataGridViewComboBoxCell();
                                            // ADD SPECS FOR COMBOBOX
                                            newCell.DataSource = SQL_Source.DBDT;
                                            newCell.DisplayMember = "collection_groups";
                                            newCell.ValueMember = "Prime";
                                            newCell.FlatStyle = FlatStyle.Popup;
                                            newCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                                            newCell.DisplayStyleForCurrentCellOnly = false;
                                            dataGridView1.Rows[r].Cells[i] = newCell;
                                            break;
                                        }
                                    case 6:
                                        {
                                            tbl_Source = "dtbMarketConfigurePDPM";
                                            SQL_Source.ExecQuery("SELECT * FROM " + tbl_Source + ";");
                                            var newCell = new DataGridViewComboBoxCell();
                                            // ADD SPECS FOR COMBOBOX
                                            newCell.DataSource = SQL_Source.DBDT;
                                            newCell.DisplayMember = "collection_groups";
                                            newCell.ValueMember = "Prime";
                                            newCell.FlatStyle = FlatStyle.Popup;
                                            newCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                                            newCell.DisplayStyleForCurrentCellOnly = false;
                                            dataGridView1.Rows[r].Cells[i] = newCell;
                                            break;
                                        }
                                    case 7:
                                        {
                                            tbl_Source = "dtbMarketConfigureIncome";
                                            SQL_Source.ExecQuery("SELECT * FROM " + tbl_Source + ";");
                                            var newCell = new DataGridViewComboBoxCell();
                                            // ADD SPECS FOR COMBOBOX
                                            newCell.DataSource = SQL_Source.DBDT;
                                            newCell.DisplayMember = "collection_groups";
                                            newCell.ValueMember = "Prime";
                                            newCell.FlatStyle = FlatStyle.Popup;
                                            newCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                                            newCell.DisplayStyleForCurrentCellOnly = false;
                                            dataGridView1.Rows[r].Cells[i] = newCell;
                                            break;
                                        }
                                    case 8:
                                        {
                                            tbl_Source = "dtbMarketConfigureAsset";
                                            SQL_Source.ExecQuery("SELECT * FROM " + tbl_Source + ";");
                                            var newCell = new DataGridViewComboBoxCell();
                                            // ADD SPECS FOR COMBOBOX
                                            newCell.DataSource = SQL_Source.DBDT;
                                            newCell.DisplayMember = "collection_groups";
                                            newCell.ValueMember = "Prime";
                                            newCell.FlatStyle = FlatStyle.Popup;
                                            newCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                                            newCell.DisplayStyleForCurrentCellOnly = false;
                                            dataGridView1.Rows[r].Cells[i] = newCell;
                                            break;
                                        }
                                    case 9:
                                        {
                                            tbl_Source = "dtbMarketConfigureAge";
                                            SQL_Source.ExecQuery("SELECT * FROM " + tbl_Source + ";");
                                            var newCell = new DataGridViewComboBoxCell();
                                            // ADD SPECS FOR COMBOBOX
                                            newCell.DataSource = SQL_Source.DBDT;
                                            newCell.DisplayMember = "collection_groups";
                                            newCell.ValueMember = "Prime";
                                            newCell.FlatStyle = FlatStyle.Popup;
                                            newCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                                            newCell.DisplayStyleForCurrentCellOnly = false;
                                            dataGridView1.Rows[r].Cells[i] = newCell;
                                            break;
                                        }
                                }
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                    
                }
            }

            // FILL DATAGRID FROM DATA TABLE
            for (r = 0; r <= SQL.RecordCount - 1; r++)
            {
                for (i = 0; i <= Col_Count - 1; i++)
                {
                    if (new int[] { 5, 6, 7, 8, 9 }.Contains(r))
                    {
                        if (Information.IsNumeric(SQL.DBDT.Rows[r][i].ToString().Trim()))
                        {
                            dataGridView1.Rows[r].Cells[i].Value = Convert.ToInt32(SQL.DBDT.Rows[r][i].ToString().Trim());
                        }
                        else
                        {
                            dataGridView1.Rows[r].Cells[i].Value = SQL.DBDT.Rows[r][i];
                        }
                    }
                    else
                    {
                        dataGridView1.Rows[r].Cells[i].Value = SQL.DBDT.Rows[r][i];
                    }
                        
                }
            }

            // MAKE ROWS IN COLUMN 1 READ ONLY AND NUMBER IN ORDER
            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                dataGridView1.Rows[i].Cells[1].Value = i + 1;
                dataGridView1.Rows[i].Cells[1].ReadOnly = true;
                dataGridView1.Rows[i].Cells[2].ReadOnly = true;
            }

            // SET COLUMN SPECS
            try
            {
                for (i = 1; i <= dataGridView1.Columns.Count - 1; i++)
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
            }
            catch (Exception ex)
            {
            }

            // FREEZE COLUMNS & VISIBILITY
            dataGridView1.Columns[0].Frozen = true;
            dataGridView1.Columns[1].Frozen = true;
            dataGridView1.Columns[2].Frozen = true;
            dataGridView1.Columns[3].Frozen = true;
            dataGridView1.Columns[4].Frozen = true;
            dataGridView1.Columns[0].Visible = false;

            // MAKE 1ST COLUMN READ ONLY
            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                dataGridView1.Rows[i].Cells[1].ReadOnly = true;
            }

            // MAKE 1ST COLUMN STATIC WHITE
            dataGridView1.Columns[1].DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView1.Columns[1].DefaultCellStyle.SelectionForeColor = Color.Black;

            // COLUMN ALIGNMENT & WIDTH
            dataGridView1.Width = 785;
            dataGridView1.Height = 392;
            dataGridView1.Columns[1].Width = 50;
            dataGridView1.Columns[2].Width = 220;

            for (i = colStart; i <= dataGridView1.ColumnCount - 1; i++)
            {
                dataGridView1.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridView1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            // HIDE COLUMNS OF DGV THAT ARE NOT NEEDED FOR SELECTED PEROID
            for (i = colStart + myMethods.Period; i <= dataGridView1.ColumnCount - 1; i++)
            {
                dataGridView1.Columns[i].Visible = false;
            }

            // RESET VARIABLE
            frmLoading = 0;
        }

        private void Percent_Change()
        {
            int r;
            int n;
            string strNum;
            double intNum;
            int colStart = 5;
            int colEnd;
            colEnd = colStart + myMethods.Period - 1;
            int pctStart = 2; // PERCENT ITEMS IN GRID
            int pctEnd = 4;

            // FORMAT FILLED DB DATA
            try
            {
                for (r = pctStart; r <= pctEnd; r++)
                {
                    for (n = colStart; n <= colEnd; n++)
                    {
                        {
                            strNum = dataGridView1.Rows[r].Cells[n].Value.ToString();
                            if (Information.IsNumeric(strNum) == true) //KEEP ISNUMERIC AS VB METHOD
                            {
                                intNum = Convert.ToDouble(strNum);
                                dataGridView1.Rows[r].Cells[n].Value = String.Format("{0:p}", intNum);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public override void frmAltVerse_Master_Load(object sender, EventArgs e)
        {
            myMethods.SQL_Grab();
            Add_Source();
            LoadGrid();
            UpdateSQL();
            Percent_Change();
        }

        public override void UpdateSQL()
        {
            int i;
            int y;
            int cRight = 3;
            string btnString = "(b)";
            int colStart = 5;
            int colEnd;
            var commandBuilder = new System.Data.SqlClient.SqlCommandBuilder(SQL.DBDA);
            string cmdUpdate;
            colEnd = colStart + myMethods.Period - 1;

            if (dataGridView1.RowCount == 0)
            {
                // Nothing
            }
            else
            {
                for (y = 0; y <= colEnd; y++)
                {
                    for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                    {
                        if (Header_Name[y].Substring(Header_Name[y].Length - cRight, cRight).Equals(btnString))
                        {
                            // Do Nothing
                        }
                        else if (dataGridView1.Rows[i].Cells[y].Value == null || dataGridView1.Rows[i].Cells[y].Value == DBNull.Value)
                        {
                            return;
                        }
                    }
                }
            }

            dataGridView1.CurrentCell = null;
            for (y = 0; y <= dataGridView1.RowCount - 1; y++)
            {
                for (i = 1; i <= dataGridView1.ColumnCount - 1; i++)
                {
                    SQL.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                    if (Header_Name[i].Substring(Header_Name[i].Length - cRight, cRight).Equals(btnString))
                    {
                        SQL.AddParam("@vals", null);
                    }
                    else
                    {
                        SQL.AddParam("@vals", dataGridView1.Rows[y].Cells[i].Value);
                    }

                    cmdUpdate = "UPDATE " + tbl_Name + " SET " + Headers_Submit[i] + "=@vals WHERE ID_Num=@PrimKey;";
                    SQL.ExecQuery(cmdUpdate);
                }
            }

            LoadGrid();
        }

        private void Change_To_Decimal()
        {
            int i;
            int y;
            string Title = "TINUUM SOFTWARE";
            int cRight = 3;
            string btnString = "(b)";
            int colStart = 5;
            int colEnd;
            double num;
            var Values = new List<double>();
            int pctStart = 2; // PERCENT ITEMS IN GRID
            int pctEnd = 4;

            colEnd = colStart + myMethods.Period - 1;
            // CONTROL FOR BLANKS

            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                for (y = 0; y <= colEnd; y++)
                {
                    if (Header_Name[y].Substring(Header_Name[y].Length - cRight, cRight).Equals(btnString) || dataGridView1.Columns[y].HeaderText == "Detail Select")
                    {
                        // Do Nothing
                    }
                    else if (dataGridView1.Rows[i].Cells[y].Value == null || dataGridView1.Rows[i].Cells[y].Value == DBNull.Value)
                    {
                        Counts += 1;
                        MessageBox.Show("You must enter values for all fields before continuing.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dataGridView1.CurrentCell = dataGridView1.Rows[i].Cells[y];
                        return;
                    }
                }
            }

            // CHANNGE TO DECIMAL TO PASS TO DATABASE
            for (i = pctStart; i <= pctEnd; i++)
            {
                for (y = colStart; y <= colEnd; y++)
                {
                    if (dataGridView1.Rows[i].Cells[y].Value.ToString() == "Detail")
                    {
                        num = 0;
                    }
                    else
                    {
                        num = myMethods.ToDecimal(Convert.ToString(dataGridView1.Rows[i].Cells[y].Value));
                    }

                    Values.Add(num);
                }
            }

            // UPDATE VALUE IN DGV
            for (i = pctStart; i <= pctEnd; i++)
            {
                for (y = colStart; y <= colEnd; y++)
                {
                    if (dataGridView1.Rows[i].Cells[y].Value.ToString() == "Detail")
                    {
                        continue;
                    }
                    dataGridView1.Rows[i].Cells[y].Value = Values[(i-2) * myMethods.Period + (y - colStart)]; // MINUS 2 GIVEN PCTSTART AT ROW 2
                }
            }

            UpdateSQL();
        }

        public override void btnSubmit_Click(object sender, EventArgs e)
        {
            Change_To_Decimal();
            if (Counts > 0)
            {
                Counts = 0;
                return;
            }
            else
            {
                Dispose();
            }
        }

        public override void Cancel()
        {
            string Title = "TINUUM SOFTWARE";

            DialogResult prompt = MessageBox.Show("Are you sure? Any unsubmitted data will be lost.", Title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            Rslt_Cncl = prompt.ToString();

            if (prompt == DialogResult.Yes)
            {
                LoadGrid();
                Rate_To_CnclBtn_DB();
                Close();
            }
            else
            {
                return;
            }
        }

        public override void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string strNum;
            double intNum;
            int r;
            int n;
            int x;
            int colStart = 5;
            int colEnd;
            int Slct;
            DataGridViewComboBoxCell cmbo;
            string title = "TINUUM SOFTWARE";
            colEnd = colStart + myMethods.Period - 1;
            Slct = dataGridView1.CurrentCell.RowIndex;
            cmbo = (DataGridViewComboBoxCell)dataGridView1.Rows[Slct].Cells[3];

            // ONLY APPLIES TO RATE FIELDS
            if (dataGridView1.CurrentCell.ColumnIndex < colStart)
            {
                return;
            }

            // INDEX OF COMBOBOX IF EQUAL TO DETAIL OR NULL
            try
            {
                if (cmbo.Value == null || cmbo.Value == DBNull.Value || cmbo.Value.ToString() == "Configure")
                {
                    cmbo.Value = "";
                }
                else if (cmbo.Value.ToString() == "Detail")
                {
                    cmbo.Value = "";
                    MessageBox.Show("Edit field before continuing. Retry.", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dataGridView1.CurrentCell.Value = "";
                    dataGridView1.CurrentCell = cmbo;
                    return;
                }
            }
            catch (Exception ex)
            {
            }

            // FORMAT CELL
            try
            {
                if (cmbo.Value == null || cmbo.Value == DBNull.Value)
                {
                    MessageBox.Show("You must enter a value for rate selection.", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dataGridView1.CurrentCell.Value = "";
                    dataGridView1.CurrentCell.Selected = false;
                    dataGridView1.Rows[Slct].Cells[3].Selected = true;

                    return;
                }
                else
                {
                    if (dataGridView1.CurrentCell.ColumnIndex >= colStart)
                    {
                        var switchExpr = dataGridView1.CurrentCell.RowIndex;
                        switch (switchExpr)
                        {
                            case object _ when 0 <= switchExpr && switchExpr <= 1:
                                {
                                    strNum = Convert.ToString(dataGridView1.CurrentCell.Value);
                                    if (Information.IsNumeric(strNum) == true)
                                    {
                                        intNum = Convert.ToInt32(strNum);
                                        dataGridView1.CurrentCell.Value = intNum;
                                    }
                                    else
                                    {
                                        MessageBox.Show("You must enter a numeric value.", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        dataGridView1.CurrentCell.Value = "";
                                        dataGridView1.CurrentCell.Selected = true;
                                    }
                                    break;
                                }

                            case object _ when 2 <= switchExpr && switchExpr <= 4:
                                {
                                    strNum = Convert.ToString(dataGridView1.CurrentCell.Value);
                                    if (Information.IsNumeric(strNum) == true)
                                    {
                                        intNum = Convert.ToDouble(strNum);
                                        dataGridView1.CurrentCell.Value = String.Format("{0:p}", intNum);
                                    }
                                    else
                                    {
                                        MessageBox.Show("You must enter a numeric value.", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        dataGridView1.CurrentCell.Value = "";
                                        dataGridView1.CurrentCell.Selected = true;
                                    }
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }        
                    }
                    
                }
            }
            catch (Exception ex)
            {
            }

            // AUTO EXTEND COLUMN DATA
            r = dataGridView1.CurrentCell.RowIndex; // STATIC
            n = dataGridView1.CurrentCell.ColumnIndex; // STATIC
            try
            {
                var switchExpr1 = dataGridView1.CurrentCell.ColumnIndex;
                switch (switchExpr1)
                {
                    case object _ when 0 <= switchExpr1 && switchExpr1 <= 4: //CHECK
                        {
                            return;
                        }

                    default:
                        {
                            for (x = n + 1; x <= colEnd; x++)
                            {
                                dataGridView1.Rows[r].Cells[x].Value = dataGridView1.CurrentCell.Value;
                                dataGridView1.Rows[r].Cells[x].Selected = true;
                            }
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
            }

            Percent_Change();
            Rate_To_DB();
        }

        private void Rate_To_DB()
        {
            int Row_Num;
            int i;
            int r;
            int colStart = 5;
            int colEnd;
            var Rates = new List<string>();
            string cmdUpdate;
            string colName;

            if (Information.IsNothing(dataGridView1.CurrentCell))
                return;
            Row_Num = dataGridView1.CurrentCell.RowIndex;
            colEnd = colStart + myMethods.Period - 1;

            // FIND RATES FROM DGV AND COLLECT IN LIST TO ADJUST MONTHLY
            try
            {
                for (i = colStart; i <= colEnd; i++)
                {
                    for (r = 1; r <= Mos_Const; r++) // MONTH CONTANT TO EXPAND YEARS TO MONTHS AND FILL SQL TABLE
                    {
                        var switchExpr = Row_Num;
                        switch (switchExpr)
                        {
                            case 0:
                                {
                                    var Val = Convert.ToString(Math.Round(Convert.ToDouble(dataGridView1.Rows[Row_Num].Cells[i].Value) / Mos_Const));
                                    Rates.Add(Val);
                                    break;
                                }
                            case 1:
                                {
                                    var Val = Convert.ToString(Math.Round(Convert.ToDouble(dataGridView1.Rows[Row_Num].Cells[i].Value)));
                                    Rates.Add(Val);
                                    break;
                                }

                            case object _ when 2 <= switchExpr && switchExpr <= 4:
                                {
                                    var Val = Convert.ToString(myMethods.ToDecimal(Convert.ToString(dataGridView1.Rows[Row_Num].Cells[i].Value)));
                                    Rates.Add(Val);
                                    break;
                                }

                            default:
                                {
                                    var Val = Convert.ToString(dataGridView1.Rows[Row_Num].Cells[i].Value);
                                    Rates.Add(Val);
                                    break;
                                }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }

            SQL_ADD.ExecQuery("SELECT * FROM " + tbl_Detail + ";");
            try
            {
                // SEND RATES TO DATABASE
                for (i = 1; i <= myMethods.Period * Mos_Const; i++)
                {
                    //VARIALES & PARAMS
                    SQL_ADD.AddParam("@PrimKey", dataGridView1.Rows[Row_Num].Cells[0].Value);
                    colName = "month" + i;
                    // UPDATE COMMAND
                    cmdUpdate = "UPDATE " + tbl_Detail + " SET " + colName + "=" + Rates[i - 1] + " WHERE ID_Num=@PrimKey;";
                    SQL_ADD.ExecQuery(cmdUpdate);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void Rate_To_CnclBtn_DB()
        {
            int Row_Num;
            int i;
            int r;
            int colStart = 5;
            int colEnd;
            string cmdUpdate;
            string colName;
            var Rates = new List<string>();
            colEnd = colStart + myMethods.Period - 1;

            // FIND RATES FROM DB AND COLLECT IN LIST TO ADJUST 
            for (Row_Num = 0; Row_Num <= dataGridView1.RowCount - 1; Row_Num++)
            {
                if (dataGridView1.Rows[Row_Num].Cells[3].Value.ToString() == "Detail")
                {
                    continue;
                }
                Rates.Clear();
                try
                {
                    for (i = colStart; i <= colEnd; i++)
                    {
                        for (r = 1; r <= Mos_Const; r++)
                        {
                            var switchExpr = Row_Num;
                            switch (switchExpr)
                            {
                                case 0:
                                    {
                                        var Val = Convert.ToString(Math.Round(Convert.ToDouble(dataGridView1.Rows[Row_Num].Cells[i].Value) / Mos_Const));
                                        Rates.Add(Val);
                                        break;
                                    }
                                case 1:
                                    {
                                        var Val = Convert.ToString(Math.Round(Convert.ToDouble(dataGridView1.Rows[Row_Num].Cells[i].Value)));
                                        Rates.Add(Val);
                                        break;
                                    }

                                case object _ when 2 <= switchExpr && switchExpr <= 4:
                                    {
                                        var Val = Convert.ToString(dataGridView1.Rows[Row_Num].Cells[i].Value);
                                        Rates.Add(Val);
                                        break;
                                    }

                                default:
                                    {
                                        var Val = Convert.ToString(dataGridView1.Rows[Row_Num].Cells[i].Value);
                                        Rates.Add(Val);
                                        break;
                                    }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                }

                SQL_ADD.ExecQuery("SELECT * FROM " + tbl_Detail + ";");
                try
                {
                    // SEND RATES TO DATABASE
                    for (i = 1; i <= myMethods.Period * Mos_Const; i++)
                    {
                        SQL_ADD.AddParam("@PrimKey", dataGridView1.Rows[Row_Num].Cells[0].Value);
                        colName = "month" + i;
                        cmdUpdate = "UPDATE " + tbl_Detail + " SET " + colName + "=" + Rates[i - 1] + " WHERE ID_Num=@PrimKey;";
                        SQL_ADD.ExecQuery(cmdUpdate);
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void Detail_Check()
        {
            var Counter = default(int);
            int colStart = 5;
            var colEnd = default(int);
            int i;
            var Slct = default(int);

            try
            {
                if (dataGridView1.CurrentCell == null || dataGridView1.CurrentCell.Value == null)
                {
                    return;
                }

                Slct = dataGridView1.CurrentCell.RowIndex;
                colEnd = colStart + myMethods.Period - 1;

                for (i = colStart; i <= colEnd; i++)
                {
                    if (dataGridView1.Rows[Slct].Cells[i].Value == null) continue;
                    if (dataGridView1.Rows[Slct].Cells[i].Value.ToString() == "Detail")
                    {
                        Counter += 1;
                    }
                }
            }
            catch (Exception ex)
            {
            }

            if (Counter > 0)
            {
                for (i = colStart; i <= colEnd; i++)
                {
                    dataGridView1.Rows[Slct].Cells[i].Value = "";
                }
            }
        }

        public override void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView senderGrid = (DataGridView)sender;
            int i;
            int start = 5;

            try
            {
                if (frmLoading > 0)
                {
                    return;
                }
                if (dataGridView1.CurrentCell == null)
                {
                    return;
                }
                if (e.ColumnIndex == 3)
                {
                    Detail_Check();
                    
                    if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                    {
                        for (i = start; i <= dataGridView1.ColumnCount - 1; i++)
                        {
                            dataGridView1.Rows[e.RowIndex].Cells[i].Value = null;
                        }
                        return;
                    }
                    else if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Detail")
                    {
                        int switchExpr = e.RowIndex;
                        
                        switch (switchExpr)
                        {
                            case 0:
                            case 1:
                                {
                                    FormDetail_Dynamic frmDetail = new FormDetail_Dynamic();
                                    frmDetail.Show(this);
                                    break;
                                }
                            case 2:
                            case 3:
                            case 4:
                                {
                                    dtlMarket_Percent frmDetail = new dtlMarket_Percent();
                                    frmDetail.Show(this);
                                    break;
                                }
                            case 5:
                            case 6:
                            case 7:
                            case 8:
                            case 9:
                                {
                                    dtlMarket_Collection frmDetail = new dtlMarket_Collection();
                                    frmDetail.Show(this);
                                }
                                break;
                            default:
                                {
                                    break;
                                }  
                        }

                    }
                    else if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Configure")
                    {
                        for (i = start; i <= dataGridView1.ColumnCount - 1; i++)
                        {
                            dataGridView1.Rows[e.RowIndex].Cells[i].Value = null;
                        }

                        int switchExpr = e.RowIndex;
                        switch (switchExpr)
                        {
                            case 5:
                                {
                                    FormConfigure_Payors frmConfigure = new FormConfigure_Payors();
                                    frmConfigure.Show(this);
                                    break;
                                }
                            case 6:
                                {
                                    FormConfigure frmConfigure = new FormConfigure();
                                    frmConfigure.Show(this);
                                    break;
                                }
                            case 7:
                                {
                                    FormConfigure_Income frmConfigure = new FormConfigure_Income();
                                    frmConfigure.Show(this);
                                    break;
                                }
                            case 8:
                                {
                                    FormConfigure_Asset frmConfigure = new FormConfigure_Asset();
                                    frmConfigure.Show(this);
                                    break;
                                }
                            case 9:
                                {
                                    FormConfigure_Age frmConfigure = new FormConfigure_Age();
                                    frmConfigure.Show(this);
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
            }

        }

        public override void DataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int Slct;
            DataGridView senderGrid = (DataGridView)sender;
            try
            {
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
                {
                    if (dataGridView1.CurrentCell == null)
                    {
                        return;
                    }
                    Slct = dataGridView1.CurrentCell.RowIndex;
                    {
                        dataGridView1.CurrentCell = dataGridView1.Rows[Slct].Cells[3];
                        dataGridView1.Rows[Slct].Cells[3].Value = "";
                        dataGridView1.Rows[Slct].Cells[3].Value = "Detail";
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}

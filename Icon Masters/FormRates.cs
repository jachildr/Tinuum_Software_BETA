using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Tinuum_Software_BETA
{
    [CLSCompliant(true)]
    public partial class FormRates : Tinuum_Software_BETA.FormMaster
    {
        public FormRates()
        {
            InitializeComponent();
        }

        public SQLControl SQL_ADD = new SQLControl();
        private int Counts;
        private int Mos_Const = 12;
        private List<string> Headers_Submit = new List<string>();
        private List<string> Header_Name = new List<string>();
        private List<string> Header_Rename = new List<string>();
        private int Col_Count;
        private string tbl_Name = "dtbRateVerse";
        private string tbl_Detail = "dtbRateDetail";
        private int frmLoading;

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
                colString.Add(dataGridView1.Columns[n].HeaderText);

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
            var cmbo = new DataGridViewComboBoxColumn();
            var btn = new DataGridViewButtonColumn();
            int colStart = 5;
            // SET FORM LOADING VARIABLE TO STOP EVENT DGV COMBOBOX EVENTS
            frmLoading = 1;

            // SET DATA SOURCE AND RESET DGV
            SQL.ExecQuery("SELECT * FROM " + tbl_Name + ";");
            if (SQL.HasException(true))
                return;

            dataGridView1.ColumnCount = 0;
            dataGridView1.RowCount = 0;
            dataGridView1.Refresh();

            // ADD SPECS FOR COMBOBOX
            cmbo.Items.Add("");
            cmbo.Items.Add("Annually");
            cmbo.Items.Add("Semi-Annually");
            cmbo.Items.Add("Quarterly");
            cmbo.Items.Add("Monthly");
            cmbo.Items.Add("Detail");
            cmbo.FlatStyle = FlatStyle.Popup;
            cmbo.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
            cmbo.DisplayStyleForCurrentCellOnly = false;

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
                            dataGridView1.Columns.Add(cmbo);
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
            dataGridView1.Columns[1].Width = 50;
            dataGridView1.Columns[2].Width = 150;

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

            // FORMAT FILLED DB DATA
            try
            {
                for (r = 0; r <= dataGridView1.RowCount - 1; r++)
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

        public override void InsertUser()
        {
            int i;
            int y;
            string Title = "TINUUM SOFTWARE";
            int cRight = 3;
            string btnString = "(b)";
            int colStart = 5;
            int colEnd;
            colEnd = colStart + myMethods.Period - 1;

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
                        MessageBox.Show("You must enter values for all fields before adding a new entry.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dataGridView1.CurrentCell = dataGridView1.Rows[i].Cells[y];
                        return;
                    }
                }
            }

            Change_To_Decimal(); // INCLUDES UPDATE CALL
            SQL.ExecQuery("INSERT INTO " + tbl_Name + " DEFAULT VALUES;");
            LoadGrid();
            base.DeleteCTRLs();
            DynamicCTLRs();
            Percent_Change();

            // INSERT ID INTO TBL RATE DETAIL
            SQL_Foreign_Insert();
        }

        private void SQL_Foreign_Insert()
        {
            int rNum = SQL.RecordCount - 1;
            SQL_ADD.AddParam("@param", SQL.DBDT.Rows[rNum][0]);
            SQL_ADD.ExecQuery("INSERT INTO " + tbl_Detail + " (ID_Num)" + "VALUES (@param);");
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

            colEnd = colStart + myMethods.Period - 1;
            // CONTROL FOR BLANKS

            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                for (y = 0; y <= colEnd; y++)
                {
                    if (Header_Name[y].Substring(Header_Name[y].Length - cRight, cRight).Equals(btnString))
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
            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
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
            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                for (y = colStart; y <= colEnd; y++)
                {
                    if (dataGridView1.Rows[i].Cells[y].Value.ToString() == "Detail")
                    {
                        continue;
                    }
                    dataGridView1.Rows[i].Cells[y].Value = Values[i * myMethods.Period + (y - colStart)];
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

        public override void btnDelete_Click(object sender, EventArgs e)
        {
            int r;
            string Title = "TINUUM SOFTWARE";
            DialogResult prompt = MessageBox.Show("Are you sure you want to permanently delete?", Title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            try
            {
                if (prompt == DialogResult.Yes)
                {
                    r = dataGridView1.CurrentCell.RowIndex;
                    base.DeleteCTRLs();
                    SQL.AddParam("@PrimKey", dataGridView1.Rows[r].Cells[0].Value.ToString());
                    SQL.ExecQuery("DELETE FROM " + tbl_Name + " WHERE ID_Num=@PrimKey");
                    LoadGrid();
                    DynamicCTLRs();
                    Percent_Change();
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
            string cboStr = "(x)"; // ADJUSTED TO DISABLE
            string btnStr = "(y)"; // ADJUSTED TO DISABLE
            string dteStr = "(d)";
            int colStart = 5;
            int colEnd;
            colEnd = colStart + myMethods.Period - 1;

            for (c = 0; c <= colEnd; c++)
            {
                var switchExpr = Header_Name[c].Substring(Header_Name[c].Length - cRight, cRight);
                switch (switchExpr)
                {
                    case var @case when @case == cboStr:
                        {
                            for (z = 0; z <= Counter; z++)
                            {
                                var cmboBox = new ComboBox();
                                {
                                    cmboBox.Items.Add("");
                                    cmboBox.Items.Add("Annually");
                                    cmboBox.Items.Add("Semi-Annually");
                                    cmboBox.Items.Add("Quarterly");
                                    cmboBox.Items.Add("Monthly");
                                    cmboBox.Items.Add("Detail");
                                }

                                cmboBox.Name = Header_Name[c].Trim() + z;
                                try
                                {
                                    cmboBox.SelectedIndex = Convert.ToInt32(dataGridView1.Rows[z].Cells[c].Value.ToString());   // SET EQUAL TO CORRESPONDING DTGV CELL TEXT
                                }
                                catch (Exception ex)
                                {
                                }

                                cmboBox.FlatStyle = FlatStyle.Flat;
                                cmboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                                dataGridView1.Controls.Add(cmboBox);

                                // SET POSITION VARIABLES
                                rect = dataGridView1.GetCellDisplayRectangle(c, z, false);
                                x = rect.X;
                                y = rect.Y;
                                Width = rect.Width;
                                Height = rect.Height;

                                // SET POSITION
                                cmboBox.SetBounds(x, y, Width, Height);
                                cmboBox.Visible = true;

                                // ADD HANDLER
                                cmboBox.Click += new EventHandler(base.HandleDynamicCombo_Click);
                                cmboBox.SelectedIndexChanged += new EventHandler(base.HandleDynamicCombo_SelectedIndexChanged);
                            }

                            break;
                        }

                    case var case1 when case1 == btnStr:
                        {
                            for (z = 0; z <= Counter; z++)
                            {
                                var gridBtn = new Button();
                                gridBtn.Name = Header_Name[c].Trim() + z;
                                gridBtn.Text = "...";
                                gridBtn.FlatStyle = FlatStyle.System;
                                gridBtn.TextAlign = ContentAlignment.BottomCenter;
                                gridBtn.Font = new Font("Calibri", 6, FontStyle.Bold);
                                gridBtn.BackColor = SystemColors.Control;

                                dataGridView1.Controls.Add(gridBtn);

                                // SET POSITION
                                rect = dataGridView1.GetCellDisplayRectangle(c, z, false);
                                x = rect.X;
                                y = rect.Y;
                                Width = rect.Width;
                                Height = rect.Height;

                                gridBtn.SetBounds(x, y, Width, Height);
                                gridBtn.Visible = true;

                                // ADD EVENT HANDLER
                                gridBtn.Click += new EventHandler(base.HandleDynamicButton_Click);
                            }

                            break;
                        }
                }
            }
        }

        public override void Cancel()
        {
            int i;
            int y;
            string Title = "TINUUM SOFTWARE";
            int cRight = 3;
            string btnString = "(b)";
            int colStart = 5;
            int colEnd;
            colEnd = colStart + myMethods.Period - 1;

            DialogResult prompt = MessageBox.Show("Are you sure? Any unsubmitted data will be lost.", Title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            Rslt_Cncl = prompt.ToString();

            if (prompt == DialogResult.Yes)
            {
                LoadGrid();
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
                            else if (string.IsNullOrEmpty(dataGridView1.Rows[i].Cells[y].Value.ToString()))
                            {
                                SQL.AddParam("@PrimKey", dataGridView1.Rows[i].Cells[0].Value.ToString());
                                SQL.ExecQuery("DELETE FROM " + tbl_Name + " WHERE ID_Num=@PrimKey");
                            }
                        }
                    }
                }
            }
            else
            {
                return;
            }

            Rate_To_CnclBtn_DB();
            Close();
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
            int ent_name = 2;
            int Counter = 0;
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
                if (cmbo.Value == null || cmbo.Value == DBNull.Value)
                {
                cmbo.Value = "";
                MessageBox.Show("Choose valid rate selection before continuing. Retry.", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataGridView1.CurrentCell.Value = "";
                dataGridView1.CurrentCell = cmbo;
                return;
                }
                else if (cmbo.Value.ToString() == "Detail")
                {
                    cmbo.Value = "";
                    MessageBox.Show("Choose valid rate selection before continuing. Retry.", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dataGridView1.CurrentCell.Value = "";
                    dataGridView1.CurrentCell = cmbo;
                    return;
                }
            }
            catch (Exception ex)
            {
            }

            // ENSURE NO DUPLICATE ENTRIES
            if (dataGridView1.CurrentCell.ColumnIndex == ent_name)
            {
                for (int i = 0; i <= dataGridView1.RowCount - 1; i++)
                {
                    if (dataGridView1.Rows[i].Cells[2].Value.ToString().ToLower() == dataGridView1.CurrentCell.Value.ToString().ToLower())
                    {
                        Counter += 1;
                    }
                }

                if (Counter > 1)
                {
                    dataGridView1.CurrentCell.Value = "";
                    MessageBox.Show("You cannot enter duplicate values in this field. Retry.", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
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
                    var switchExpr = dataGridView1.CurrentCell.ColumnIndex;
                    switch (switchExpr)
                    {
                        case object _ when 0 <= switchExpr && switchExpr <= 4:
                            {
                                return;
                            }

                        default:
                            {
                                strNum = Convert.ToString(dataGridView1.CurrentCell.Value);
                                if (Information.IsNumeric(strNum) == true) //KEEP VB METHOD
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
                    case object _ when 0 <= switchExpr1 && switchExpr1 <= 4:
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

        public override void btnAdd_Click(object sender, EventArgs e)
        {
            InsertUser();
        }

        private void Rate_To_DB()
        {
            int Row_Num;
            int i;
            int r;
            double Val;
            int Ann = 1;
            int Semi = 2;
            int Quart = 4;
            int colStart = 5;
            int colEnd;
            var Rates = new List<double>();
            string cmdUpdate;
            string colName;

            if (Information.IsNothing(dataGridView1.CurrentCell)) // KEEP VB METHOD
                return;
            Row_Num = dataGridView1.CurrentCell.RowIndex;
            colEnd = colStart + myMethods.Period - 1;

            // FIND RATES FROM DGV AND COLLECT IN LIST TO ADJUST MONTHLY
            try
            {
                for (i = colStart; i <= colEnd; i++)
                {
                    for (r = 1; r <= Mos_Const; r++)
                    {
                        var switchExpr = dataGridView1.Rows[Row_Num].Cells[3].Value;
                        switch (switchExpr)
                        {
                            case "Annually":
                                {
                                    if (r % (Mos_Const / (double)Ann) == 1)
                                    {
                                        Val = myMethods.ToDecimal(Convert.ToString(dataGridView1.Rows[Row_Num].Cells[i].Value)) / Ann;
                                    }
                                    else
                                    {
                                        Val = 0;
                                    }

                                    Rates.Add(Val);
                                    break;
                                }

                            case "Semi-Annually":
                                {
                                    if (r % (Mos_Const / (double)Semi) == 1)
                                    {
                                        Val = myMethods.ToDecimal(Convert.ToString(dataGridView1.Rows[Row_Num].Cells[i].Value)) / Semi;
                                    }
                                    else
                                    {
                                        Val = 0;
                                    }

                                    Rates.Add(Val);
                                    break;
                                }

                            case "Quarterly":
                                {
                                    if (r % (Mos_Const / (double)Quart) == 1)
                                    {
                                        Val = myMethods.ToDecimal(Convert.ToString(dataGridView1.Rows[Row_Num].Cells[i].Value)) / Quart;
                                    }
                                    else
                                    {
                                        Val = 0;
                                    }

                                    Rates.Add(Val);
                                    break;
                                }

                            case "Monthly":
                                {
                                    Val = myMethods.ToDecimal(Convert.ToString(dataGridView1.Rows[Row_Num].Cells[i].Value)) / Mos_Const;
                                    Rates.Add(Val);
                                    break;
                                }

                            default:
                                {
                                    return;
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
            double Val;
            int Ann = 1;
            int Semi = 2;
            int Quart = 4;
            int colStart = 5;
            int colEnd;
            string cmdUpdate;
            string colName;
            var Rates = new List<double>();
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
                            var switchExpr = dataGridView1.Rows[Row_Num].Cells[3].Value;
                            switch (switchExpr)
                            {
                                case "Annually":
                                    {
                                        if (r % (Mos_Const / (double)Ann) == 1)
                                        {
                                            Val = Convert.ToDouble(dataGridView1.Rows[Row_Num].Cells[i].Value) / Ann;
                                        }
                                        else
                                        {
                                            Val = 0;
                                        }

                                        Rates.Add(Val);
                                        break;
                                    }

                                case "Semi-Annually":
                                    {
                                        if (r % (Mos_Const / (double)Semi) == 1)
                                        {
                                            Val = Convert.ToDouble(dataGridView1.Rows[Row_Num].Cells[i].Value) / Semi;
                                        }
                                        else
                                        {
                                            Val = 0;
                                        }

                                        Rates.Add(Val);
                                        break;
                                    }

                                case "Quarterly":
                                    {
                                        if (r % (Mos_Const / (double)Quart) == 1)
                                        {
                                            Val = Convert.ToDouble(dataGridView1.Rows[Row_Num].Cells[i].Value) / Quart;
                                        }
                                        else
                                        {
                                            Val = 0;
                                        }

                                        Rates.Add(Val);
                                        break;
                                    }

                                case "Monthly":
                                    {
                                        Val = Convert.ToDouble(dataGridView1.Rows[Row_Num].Cells[i].Value) / Mos_Const;
                                        Rates.Add(Val);
                                        break;
                                    }

                                default:
                                    {
                                        return;
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
                if (dataGridView1.CurrentCell == null)
                {
                    return;
                }

                Slct = dataGridView1.CurrentCell.RowIndex;
                colEnd = colStart + myMethods.Period - 1;

                for (i = colStart; i <= colEnd; i++)
                {
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
        
        private void blankCheck()
        {
            int row;
            row = dataGridView1.CurrentCell.RowIndex;
            int colStart = 5;
            int colEnd;
            int i;

            colEnd = colStart + myMethods.Period - 1;

            if (dataGridView1.Rows[row].Cells[3].Value == null || dataGridView1.Rows[row].Cells[3].Value == DBNull.Value)
            {
                for (i = colStart; i <= colEnd; i++)
                {
                    dataGridView1.Rows[row].Cells[i].Value = "";
                }
            }
        }
        public override void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView senderGrid = (DataGridView)sender;
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
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn)
                {
                    blankCheck();
                    Detail_Check();
                    Rate_To_DB();
                    if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null || dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == DBNull.Value)
                    {
                        return;
                    }
                    else if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Detail")
                    {
                        FormDetail_Rate frmRate = new FormDetail_Rate();
                        frmRate.Show(this);
                        //MessageBox.Show(Application.OpenForms[3].Name.ToString());
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

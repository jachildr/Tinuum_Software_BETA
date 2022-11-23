using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace Tinuum_Software_BETA
{
    [CLSCompliant(true)]
    public partial class FormMaster : Form
    {
        public FormMaster()
        {
            InitializeComponent();
        }

        protected string Rslt_Cncl = null;
        public SQLControl SQL = new SQLControl(); // CREATE NEW INSTANCE OF SQLCONTROL CLASS
        protected List<string> Headers_Submit = new List<string>();
        protected List<string> Header_Name = new List<string>();
        protected List<string> Header_Rename = new List<string>();
        protected int Col_Count;
        protected string tbl_Name = "tblCreate";
        protected int Mos_Const = 12;

        public virtual void Add_Source()
        {
            string btnString = "(b)";
            string cmbString = "(c)";
            string dteString = "(d)";
            int i;

            // UNBIND DATA SOURCE AT BEGINNING FOR UPDATE COMMAND

            dataGridView1.ColumnCount = 0;
            dataGridView1.RowCount = 0;
            //dataGridView1.Refresh();


            // LINK DATA SOURCE TO GET COL NAMES
            SQL.ExecQuery("SELECT * FROM " + tbl_Name + ";");
            if (SQL.HasException(true))
                return;
            dataGridView1.DataSource = SQL.DBDT;

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

        public virtual void LoadGrid()
        {
            int i;
            int r;
            var cmbo = new DataGridViewComboBoxColumn();
            var btn = new DataGridViewButtonColumn();

            // ADD SPECS FOR COMBOBOX
            cmbo.Items.Add("First");
            cmbo.Items.Add("Second");
            cmbo.Items.Add("Third");
            cmbo.FlatStyle = FlatStyle.Popup;
            cmbo.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
            cmbo.DisplayStyleForCurrentCellOnly = false;

            // ADD SPECS FOR BUTTON1
            btn.UseColumnTextForButtonValue = true;
            btn.Text = "_";
            btn.FlatStyle = FlatStyle.System;
            btn.DefaultCellStyle.Alignment = (DataGridViewContentAlignment)ContentAlignment.BottomRight;
            btn.DefaultCellStyle.Font = new Font("Arial", 6, FontStyle.Bold);
            dataGridView1.Rows.Clear();

            //MessageBox.Show(dataGridView1.ColumnCount.ToString());
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

        }

        public virtual void frmAltVerse_Master_Load(object sender, EventArgs e)
        {
            myMethods.SQL_Grab();
            Add_Source();
            LoadGrid();
            //DynamicCTLRs();
        }

        public virtual void Cancel()
        {
            int i;
            int y;
            string Title = "TINUUM SOFTWARE";
            int cRight = 3;
            string btnString = "(b)";

            DialogResult prompt = MessageBox.Show("Are you sure? Any unsubmitted data will be lost.", Title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            Rslt_Cncl = prompt.ToString();

            if (prompt == DialogResult.Yes)
            {
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
                            else if (string.IsNullOrEmpty(dataGridView1.Rows[i].Cells[y].Value.ToString()))
                            {
                                SQL.AddParam("@PrimKey", dataGridView1.Rows[i].Cells[0].Value.ToString());
                                SQL.ExecQuery("DELETE FROM " + tbl_Name + " WHERE ID_Num=@PrimKey;");
                            }
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

        public virtual void InsertUser()
        {
            int i;
            int y;
            string Title = "TINUUM SOFTWARE";
            int cRight = 3;
            string btnString = "(b)";

            for (y = 0; y <= dataGridView1.ColumnCount - 1; y++)
            {
                for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                {
                    if (Header_Name[y].Substring(Header_Name[y].Length - cRight, cRight).Equals(btnString))
                    {
                        // Do Nothing
                    }
                    else if (dataGridView1.Rows[i].Cells[y].Value == DBNull.Value)
                    {
                        MessageBox.Show("You must enter values for all fields before adding a new entry.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            UpdateSQL();
            SQL.ExecQuery("INSERT INTO " + tbl_Name + " DEFAULT VALUES;");
            Add_Source();
            LoadGrid();
            //DeleteCTRLs();
            //DynamicCTLRs();
        }

        public virtual void btnAdd_Click(object sender, EventArgs e)
        {
            InsertUser();
        }

        public virtual void UpdateSQL()
        {
            int i;
            int y;
            int cRight = 3;
            string btnString = "(b)";
            var commandBuilder = new System.Data.SqlClient.SqlCommandBuilder(SQL.DBDA);
            string cmdUpdate;

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
                            return;
                        }
                    }
                }
            }

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
        }

        public virtual void btnSubmit_Click(object sender, EventArgs e)
        {
            int i;
            int y;
            string Title = "TINUUM SOFTWARE";
            int cRight = 3;
            string btnString = "(b)";

            for (y = 0; y <= dataGridView1.ColumnCount - 1; y++)
            {
                for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                {
                    if (Header_Name[y].Substring(Header_Name[y].Length - cRight, cRight).Equals(btnString))
                    {
                        // Do Nothing
                    }
                    else if (dataGridView1.Rows[i].Cells[y].Value == null || Convert.ToString(dataGridView1.Rows[i].Cells[y].Value) == "Configure")
                    {
                        MessageBox.Show("You must enter relevant values for all fields before continuing.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dataGridView1.CurrentCell = dataGridView1.Rows[i].Cells[y];
                        return;
                    }
                }
            }

            UpdateSQL();
            Dispose();
        }

        public virtual void DeleteCTRLs()
        {
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
                            if (ctrl.Name == Header_Rename[c].Trim() + n)
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
        }

        public virtual void btnDelete_Click(object sender, EventArgs e)
        {
            int r;
            string Title = "TINUUM SOFTWARE";

            DialogResult prompt = MessageBox.Show("Are you sure you want to permanently delete?", Title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            try
            {
                if (prompt == DialogResult.Yes)
                {
                    r = dataGridView1.CurrentCell.RowIndex;
                    DeleteCTRLs();
                    SQL.AddParam("@PrimKey", dataGridView1.Rows[r].Cells[0].Value.ToString());
                    SQL.ExecQuery("DELETE FROM " + tbl_Name + " WHERE ID_Num=@PrimKey;");
                    Add_Source();
                    LoadGrid();
                    //DynamicCTLRs();
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

        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // KEEP'DataGridView1.Rows(e.RowIndex).HeaderCell.Value = CStr(e.RowIndex + 1)
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Cancel();
        }

        public virtual void DynamicCTLRs()
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
            string cboStr = "(c)"; // CHANGED TO DISABLE
            string btnStr = "(b)"; // CHANGED TO DISABLE
            string dteStr = "(d)";

            for (c = 0; c <= dataGridView1.ColumnCount - 1; c++)
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
                                    cmboBox.Items.Add("First");
                                    cmboBox.Items.Add("Second");
                                    cmboBox.Items.Add("Third");
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
                                cmboBox.Click += new EventHandler(HandleDynamicCombo_Click);
                                cmboBox.SelectedIndexChanged += new EventHandler(HandleDynamicCombo_SelectedIndexChanged);
                            }

                            break;
                        }

                    case var case1 when case1 == btnStr:
                        {
                            for (z = 0; z <= Counter; z++)
                            {
                                var gridBtn = new Button();
                                gridBtn.Name = (Header_Name[c]).Trim() + z;
                                gridBtn.Text = "...";
                                gridBtn.FlatStyle = FlatStyle.System;
                                gridBtn.TextAlign = ContentAlignment.BottomCenter;
                                gridBtn.Font = new Font("Calibri", 6, FontStyle.Bold);

                                dataGridView1.Controls.Add(gridBtn);

                                // SET POSITION
                                rect = dataGridView1.GetCellDisplayRectangle(c, z, false);
                                x = rect.X;
                                y = rect.Y;
                                Width = rect.Width;
                                Height = rect.Height;

                                gridBtn.SetBounds(x, y, Width, Height);
                                gridBtn.Visible = true;

                                // EVENT HANDLER
                                gridBtn.Click += new EventHandler(HandleDynamicButton_Click);
                            }

                            break;
                        }

                    case var case2 when case2 == dteStr:
                        {
                            for (z = 0; z <= Counter; z++)
                            {
                                var gridDte = new DateTimePicker();
                                gridDte.Name = (Header_Name[c]).Trim() + z;
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

        private void frmAltVerse_Master_Shown(object sender, EventArgs e)
        {
            DynamicCTLRs();
        }

        private void Move_CTRLs()
        {
            int n;
            int c;
            int x;
            int y;
            int z;
            int width;
            int height;
            Rectangle rect;

            for (n=0; n <= dataGridView1.RowCount - 1; n++)
            {
                for (c = 0; c <= dataGridView1.ColumnCount - 1; c++)
                {
                    //FIND & MOVE ALL DYNAMIC CONTROLS
                    foreach (Control ctrl in dataGridView1.Controls)
                    {
                        if (ctrl.Name == dataGridView1.Columns[c].HeaderText.Trim() + n)
                        {
                            rect = dataGridView1.GetCellDisplayRectangle(c, n, false);
                            x = rect.X;
                            y = rect.Y;
                            width = rect.Width;
                            height = rect.Height;

                            ctrl.SetBounds(x,y,width,height);
                            ctrl.Visible = true;
                        } 
                    }
                }
            }

        }

        private void DataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            Move_CTRLs();
        }

        public virtual void HandleDynamicCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmboBox = (ComboBox)sender;
            // FIX BELOW CODE TO BE DYNAMIC
            dataGridView1.CurrentCell.Value = cmboBox.SelectedIndex;
        }

        public virtual void HandleDynamicCombo_Click(object sender, EventArgs e)
        {
            ComboBox cmboBox = (ComboBox)sender;
            var i = default(int);
            int Diff;
            var rowNum = default(int);

            try
            {
                for (i = 0; i <= dataGridView1.Columns.Count - 1; i++)
                {
                    if (cmboBox.Name.Substring(0, Header_Name[i].Trim().Length) == (Header_Name[i]).Trim())
                    {
                        Diff = cmboBox.Name.Length - Header_Name[i].Trim().Length;
                        rowNum = Convert.ToInt32(cmboBox.Name.Substring(cmboBox.Name.Length - Diff, Diff));
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
            }

            dataGridView1.CurrentCell = dataGridView1.Rows[rowNum].Cells[i];
        }

        private void DataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //string Title = "TINUUM SOFTWARE";
            //MessageBox.Show("Data entry is invalid. Retry", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public virtual void HandleDynamicButton_Click(object sender, EventArgs e)
        {
            Button Btn = (Button)sender;
            var i = default(int);
            int Diff;
            var rowNum = default(int);

            try
            {
                for (i = 0; i <= dataGridView1.Columns.Count - 1; i++)
                {
                    if (Btn.Name.Substring(0, Header_Name[i].Trim().Length) == (Header_Name[i]).Trim())
                    {
                        Diff = Btn.Name.Length - Header_Name[i].Trim().Length;
                        rowNum = Convert.ToInt32(Btn.Name.Substring(Btn.Name.Length - Diff, Diff));
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
            }

            dataGridView1.CurrentCell = dataGridView1.Rows[rowNum].Cells[i];
        }

        public void HandleDynamicDate_Leave(object sender, EventArgs e)
        {
            DateTimePicker dtePck = (DateTimePicker)sender;
            dataGridView1.CurrentCell.Value = dtePck.Value;
        }

        public void HandleDynamicDate_Enter(object sender, EventArgs e)
        {
            DateTimePicker dtePck = (DateTimePicker)sender;
            var i = default(int);
            int Diff;
            var rowNum = default(int);

            try
            {
                for (i = 0; i <= dataGridView1.Columns.Count - 1; i++)
                {
                    if (dtePck.Name.Trim().Length < Header_Rename[i].Trim().Length) continue;
                    if (dtePck.Name.Trim().Substring(0, Header_Rename[i].Trim().Length) == (Header_Rename[i]).Trim())
                    {
                        Diff = dtePck.Name.Length - Header_Rename[i].Trim().Length;
                        rowNum = Convert.ToInt32(dtePck.Name.Substring(dtePck.Name.Length - Diff, Diff));
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
            }

            dataGridView1.CurrentCell = dataGridView1.Rows[rowNum].Cells[i];
        }

        public virtual void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // PLACE HOLDER
        }

        private void frmMultiverse_Master_Resize(object sender, EventArgs e)
        {
            Move_CTRLs();
        }

        private void DataGridView1_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            dataGridView1.ShowCellToolTips = false;
        }

        public virtual void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // PLACE HOLDER
        }

        public virtual void DataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // PLACE HOLDER
        }

        public virtual void HandleDynamicDate_MouseUp(object sender, MouseEventArgs e)
        {
            // PLACE HOLDER
        }

        private void frmAltVerse_Master_FormClosing(object sender, FormClosingEventArgs e)
        {
            //e.Cancel = true;
            var switchExpr = Rslt_Cncl;
            switch (switchExpr)
            {
                case null:
                    {
                        Cancel();
                        e.Cancel = true;
                        break;
                    }

                case "No":
                    {
                        e.Cancel = true;
                        break;
                    }

                case "Yes":
                    {
                        e.Cancel = false;
                        Dispose();
                        break;
                    }
            }

            Rslt_Cncl = null;
        }

        public virtual void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {

        }
    }
}

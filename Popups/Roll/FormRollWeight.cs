using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Tinuum_Software_BETA.Detail_Inherit.Roll;

namespace Tinuum_Software_BETA.Popups.Roll
{
    public partial class FormRollWeight : Tinuum_Software_BETA.FormMaster
    {
        protected string tbl_Prefix = "dtbRollWeight";
        protected string tbl_dtlPrefix = "dtbRollWeight_Detail";
        protected string tbl_Active = "dtbRollConfigureWeight";
        protected string tbl_Variable;
        protected string slctCol = "collection_groups";
        protected string keyCol = "Prime";
        protected string Cncl = null;
        protected SQLControl SQL_Variable = new SQLControl();
        protected SQLControl SQL_Active = new SQLControl();
        protected List<double> cumulative = new List<double>();
        protected Control actCtrl;
        protected ListBox lstBox; // CHANGE FORM NUM
        protected Form frm;
        protected DataRowView drv;
        protected static int primeKey;
        public static int _primeKey 
        {
            get 
            {
                return primeKey;
            }
        }
        protected int lstIndex;
        protected int terminate = 0;
        protected string tbl_Detail;
        public FormRollWeight()
        {
            InitializeComponent();
        }
        public override void Add_Source()
        {
            if (DesignMode) return;
            int count;
            string btnString = "(b)";
            string cmbString = "(c)";
            string dteString = "(d)";
            int i;

            actCtrl = Application.OpenForms[2].ActiveControl; // CHANGE FORM NUM
            lstBox = Application.OpenForms[2].Controls["listBox1"] as ListBox;
            frm = Application.OpenForms[2];

            // CREATE NEW TABLE IF ADD
            if (actCtrl.Name == "btnAdd")
            {
                Delegate();
            }

            // QUERY TO GET TABLE FOR LATER METHODS
            SQL_Active.ExecQuery("SELECT * FROM " + tbl_Active + ";");
            // FIND PRIME KEY TO SELECTT TABLE
            lstIndex = lstBox.SelectedIndex;
            count = lstBox.Items.Count - 1;

            if (lstBox.SelectedIndex < 0)
            {
                drv = (DataRowView)lstBox.Items[count];
                primeKey = Convert.ToInt32(drv[keyCol]);
            }
            else
            {
                drv = (DataRowView)lstBox.Items[lstIndex];
                primeKey = Convert.ToInt32(drv[keyCol]);
            }

            // GET TABLE AND SELECT
            tbl_Variable = tbl_Prefix + primeKey;
            tbl_Detail = tbl_dtlPrefix + primeKey;
            SQL.ExecQuery("SELECT * FROM " + tbl_Variable + ";");

            // UNBIND DATA SOURCE AT BEGINNING FOR UPDATE COMMAND
            dataGridView1.ColumnCount = 0;
            dataGridView1.RowCount = 0;
            dataGridView1.Refresh();

            // LINK DATA SOURCE TO GET COL NAMES
            SQL.ExecQuery("SELECT * FROM " + tbl_Variable + ";");
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
        public override void LoadGrid()
        {
            int i;
            int r;
            var cmbo = new DataGridViewComboBoxColumn();
            var btn = new DataGridViewButtonColumn();

            terminate = 1;

            btnAdd.Visible = false;
            btnDelete.Visible = false;

            if (DesignMode) return;

            // ASSIGN TABLE
            SQL.ExecQuery("SELECT * FROM " + tbl_Variable + ";");
            dataGridView1.DataSource = SQL.DBDT;

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

            // RESET DGV ROWS
            dataGridView1.DataSource = null;

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

            // ADDED TO LOAD
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[2].DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView1.Columns[2].DefaultCellStyle.SelectionForeColor = Color.Black;

            // PERCENT CHANGE
            percent_change();

            // SET GRID AND FORM DIMENSIONS
            this.Width = 410;
            dataGridView1.Width = 365;
            dataGridView1.Height = 390;

            // ADD SUBMIT NAME
            if (actCtrl.Name != "btnAdd")
            {
                configName.Text = lstBox.Text;
            }

            terminate = 0;
        }

        public virtual void percent_change()
        {
            // PERCENT CHANGE
            int i;

            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                if (Information.IsNumeric(dataGridView1.Rows[i].Cells[3].Value.ToString()))
                {
                    var val = myMethods.ToPercent(dataGridView1.Rows[i].Cells[3].Value.ToString());
                    dataGridView1.Rows[i].Cells[3].Value = val;
                }
                else
                {
                    continue;
                }
            }
        }
        public override void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (terminate > 0) return;
            try
            {
                switch (e.ColumnIndex)
                {
                    case 3:
                        {
                            var switchExpr = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                            switch (switchExpr)
                            {
                                case "Detail":
                                    {
                                        dtlRoll_Percent frmDetail = new dtlRoll_Percent();
                                        frmDetail.Show(this);
                                        this.Enabled = false;
                                    }
                                    break;
                                default:
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

        public override void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 3:
                    {
                        if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                        {
                            return;
                        }
                        else
                        {
                            var val = myMethods.ToPercent(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = val;
                        }  
                    }
                    break;
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
                        case 4:
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
                        default:
                            break;
                    }

                }
            }
            catch (Exception ex)
            {
            }
        }
        public override void UpdateSQL()
        {
            int i;
            int y;
            int cRight = 3;
            string btnString = "(b)";
            var commandBuilder = new System.Data.SqlClient.SqlCommandBuilder(SQL.DBDA);
            string cmdUpdate;
            int counter = default(int);
            string title = "TINUUM SOFTWARE";
            int j;

            // ENSURE NAME FIELD NOT BLANK
            if (configName.Text == null)
            {
                MessageBox.Show("You must enter a name for the collection. Retry.", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.ActiveControl = configName;
                return;
            }

            // ENSURE NO DUPLICATE ENTRIES
            if (lstBox.Items.Count > 0)
            {
                for (i = 0; i <= lstBox.Items.Count - 1; i++)
                {
                    if (i == lstIndex) continue;

                    drv = (DataRowView)lstBox.Items[i];
                    if (drv[slctCol].ToString().ToLower() == configName.Text.ToString().ToLower())
                    {
                        counter += 1;
                    }
                }

                if (counter > 0)
                {
                    configName.Text = "";
                    this.ActiveControl = configName;
                    MessageBox.Show("You cannot enter duplicate values in this field. Retry.", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
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
                        if (Header_Name[y].Substring(Header_Name[y].Length - cRight, cRight).Equals(btnString))
                        {
                            // Do Nothing
                        }
                        else if (dataGridView1.Rows[i].Cells[y].Value == null || dataGridView1.Rows[i].Cells[y].Value == DBNull.Value)
                        {
                            MessageBox.Show("You must enter relevant values. Retry.", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            dataGridView1.CurrentCell = dataGridView1.Rows[i].Cells[y];
                            return;
                        }
                    }
                }
            }
            // FILL MAJOR TABLE WITH GRID
            for (y = 0; y <= dataGridView1.RowCount - 1; y++)
            {
                for (i = 1; i <= dataGridView1.ColumnCount - 1; i++)
                {
                    SQL.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                    if (Header_Name[i].Substring(Header_Name[i].Length - cRight, cRight).Equals(btnString))
                    {
                        SQL.AddParam("@vals", null);
                    }
                    else if (i == 3)
                    {
                        if (dataGridView1.Rows[y].Cells[i].Value.ToString() != "Detail")
                        {
                            SQL.AddParam("@vals", myMethods.ToDecimal(dataGridView1.Rows[y].Cells[i].Value.ToString()));
                        }
                        else
                        {
                            SQL.AddParam("@vals", dataGridView1.Rows[y].Cells[i].Value);
                        }
                    }
                    else
                    {
                        SQL.AddParam("@vals", dataGridView1.Rows[y].Cells[i].Value);
                    }

                    cmdUpdate = "UPDATE " + tbl_Variable + " SET " + Headers_Submit[i] + "=@vals WHERE ID_Num=@PrimKey;";
                    SQL.ExecQuery(cmdUpdate);
                }
            }

            // FILL DETAIL TABLES FROM GRID
            for (y = 0; y <= dataGridView1.ColumnCount - 1; y++)
            {
                for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                {
                    if (dataGridView1.Rows[i].Cells[3].Value.ToString() == "Detail")
                    {
                        continue;
                    }
                    else
                    {
                        for (j = 1; j <= myMethods.Period * Mos_Const; j++)
                        {
                            string tbl_Col = "month" + j;
                            var dec_Val = dataGridView1.Rows[i].Cells[3].Value;
                            SQL.AddParam("@PrimKey", dataGridView1.Rows[i].Cells[0].Value.ToString());
                            SQL.AddParam("@months_data", dec_Val);
                            cmdUpdate = "UPDATE " + tbl_Detail + " SET " + tbl_Col + "=@months_data WHERE ID_Num=@PrimKey;";
                            SQL.ExecQuery(cmdUpdate);
                        }
                    }

                }
            }

            // UPDATE ACTIVE TABLE
            SQL_Active.AddParam("@PrimeKey", primeKey);
            SQL_Active.AddParam("@CaseName", configName.Text);
            cmdUpdate = "UPDATE " + tbl_Active + " SET " + slctCol + "=@CaseName WHERE Prime=@PrimeKey;";
            SQL_Active.ExecQuery(cmdUpdate);
            frm.Enabled = true;

            this.Dispose();
        }

        public override void btnSubmit_Click(object sender, EventArgs e)
        {
            UpdateSQL();
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

            DialogResult prompt = MessageBox.Show("Are you sure? Any unsubmitted data will be lost.", Title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            Rslt_Cncl = prompt.ToString();

            if (prompt == DialogResult.Yes)
            {
                if (actCtrl.Name == "btnAdd")
                {
                    // DROP TABLE
                    SQL_Variable.ExecQuery("DROP TABLE " + tbl_Detail + ";");
                    SQL_Variable.ExecQuery("DROP TABLE " + tbl_Variable + ";");
                    // DELETE ENTRY FROM TABLE
                    SQL_Variable.AddParam("@PrimeKey", primeKey);
                    SQL_Variable.ExecQuery("DELETE FROM " + tbl_Active + " WHERE Prime=@PrimeKey;");

                    // clean up
                    frm.Enabled = true;
                    this.Close();
                }
                else if (dataGridView1.RowCount != 0)
                {
                    // CLEAR GRID AND RESET WITH ORIGINAL TABLE
                    SQL.ExecQuery("SELECT * FROM " + tbl_Variable + ";");
                    dataGridView1.Rows.Clear();
                    dataGridView1.Columns.Clear();
                    dataGridView1.DataSource = SQL.DBDT;

                    // FILL TABLES FROM GRID
                    for (y = 0; y <= dataGridView1.ColumnCount - 1; y++)
                    {
                        for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                        {
                            if (dataGridView1.Rows[i].Cells[3].Value.ToString() == "Detail")
                            {
                                continue;
                            }
                            else
                            {
                                for (j = 1; j <= myMethods.Period * Mos_Const; j++)
                                {
                                    string tbl_Col = "month" + j;
                                    var dec_Val = dataGridView1.Rows[i].Cells[3].Value;
                                    SQL.AddParam("@PrimKey", dataGridView1.Rows[i].Cells[0].Value.ToString());
                                    SQL.AddParam("@months_data", dec_Val);
                                    cmdUpdate = "UPDATE " + tbl_Detail + " SET " + tbl_Col + "=@months_data WHERE ID_Num=@PrimKey;";
                                    SQL.ExecQuery(cmdUpdate);
                                }
                            }
                            
                        }
                    }
                }
                else
                {
                    this.Close();
                    frm.Enabled = true;
                    return;
                }

            }
            else
            {
                return;
            }
            frm.Enabled = true;
            Close();
        }
        public virtual void Delegate()
        {
            SQLQueries.tblRollWeightCreate();
        }
    }
}

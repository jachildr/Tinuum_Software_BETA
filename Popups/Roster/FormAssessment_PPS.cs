using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace Tinuum_Software_BETA.Popups.Roster
{
    public partial class FormAssessment_PPS : Form
    {
        protected string tbl_Prefix = "dtbRoster_Assess";
        protected string tbl_Active = "dtbRosterConfigureAssess";
        protected string tbl_Variable;
        protected string slctCol = "collection_groups";
        protected string keyCol = "Prime";
        protected string Cncl = null;
        protected SQLControl SQL_Variable = new SQLControl();
        protected SQLControl SQL_Active = new SQLControl();
        protected Control actCtrl;
        protected ListBox lstBox; // CHANGE FORM NUM
        protected Form frm;
        protected DataRowView drv;
        protected int loading;
        protected int primeKey;
        protected int lstIndex;
        protected int terminate = 0;
        public FormAssessment_PPS()
        {
            InitializeComponent();
        }
        public virtual void Loader()
        {
            if (DesignMode) return;

            loading = 1;
            int i;
            int count;

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
            SQL_Variable.ExecQuery("SELECT * FROM " + tbl_Variable + ";");

            dataGridView1.DataSource = SQL_Variable.DBDT;

            // ROW HEADER DISABLE
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            // SET COLUMNS
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView1.Columns[1].DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridView1.Columns[1].DefaultCellStyle.ForeColor = Color.Black;
            dataGridView1.Columns[2].Width = 50;
            dataGridView1.Columns[2].Width = 150;
            dataGridView1.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            foreach (DataGridViewColumn Col in dataGridView1.Columns)
            {
                Col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            // MAKE ROWS IN COLUMN 1 READ ONLY AND NUMBER IN ORDER
            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                dataGridView1.Rows[i].Cells[1].Value = i + 1;
                dataGridView1.Rows[i].Cells[1].ReadOnly = true;
            }

            // ADD SUBMIT NAME
            if (actCtrl.Name != "btnAdd")
            {
                configName.Text = lstBox.Text;
            }

            loading = 0;
        }

        public virtual void InsertUser()
        {
            int i;
            int y;
            string Title = "TINUUM SOFTWARE";

            for (y = 0; y <= dataGridView1.ColumnCount - 1; y++)
            {
                for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                {
                    if (dataGridView1.Rows[i].Cells[y].Value == DBNull.Value)
                    {
                        MessageBox.Show("You must enter values for all fields before adding a new entry.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            SQL_Variable.DBDA.Update(SQL_Variable.DBDT);
            SQL_Variable.ExecQuery("INSERT INTO " + tbl_Variable + " DEFAULT VALUES;");
            
            this.Loader();
        }
        public virtual void process_Submit()
        {
            int i;
            int j;
            int counter = 0;
            string title = "TINUUM SOFTWARE";

            // ENSURE NAME FIELD NOT BLANK
            if (configName.Text == null || configName.Text == "")
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

            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                for (j = 0; j <= dataGridView1.ColumnCount - 1; j++)
                {
                    if (dataGridView1.Rows[i].Cells[j].Value == DBNull.Value || dataGridView1.Rows[i].Cells[j].Value == null)
                    {
                        MessageBox.Show("You must enter a value before continuing. Retry.", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dataGridView1.CurrentCell = dataGridView1.Rows[i].Cells[j];
                        return;
                    }
                }
            }

            // CALL METHODS
            SQL_Variable.DBDA.Update(SQL_Variable.DBDT);
            update_active();

            frm.Enabled = true;
            this.Dispose();
        }

        public virtual void update_active()
        {
            string cmdUpdate;

            // UPDATE ACTIVE TABLE WITH
            SQL_Active.AddParam("@PrimeKey", primeKey);
            SQL_Active.AddParam("@CaseName", configName.Text);
            cmdUpdate = "UPDATE " + tbl_Active + " SET " + slctCol + "=@CaseName WHERE Prime=@PrimeKey;";
            SQL_Active.ExecQuery(cmdUpdate);
        }
        public void Delegate()
        {
            SQLQueries.tblRosterAssessCreate();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int r;
            string Title = "TINUUM SOFTWARE";

            DialogResult prompt = MessageBox.Show("Are you sure you want to permanently delete?", Title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            try
            {
                if (prompt == DialogResult.Yes)
                {
                    r = dataGridView1.CurrentCell.RowIndex;
                    SQL_Variable.AddParam("@PrimKey", dataGridView1.Rows[r].Cells[0].Value.ToString());
                    SQL_Variable.ExecQuery("DELETE FROM " + tbl_Variable + " WHERE ID_Num=@PrimKey;");
                    Loader();
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

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string Title = "TINUUM SOFTWARE";

            switch (e.ColumnIndex)
            {
                case 3:
                    {
                        if (!Information.IsNumeric(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value))
                        {
                            MessageBox.Show("You must enter relevant values for all fields before continuing.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        public virtual void call_cancel()
        {
            int i;
            int y;
            int j;
            string Title = "TINUUM SOFTWARE";
            string cmdUpdate;

            DialogResult prompt = MessageBox.Show("Are you sure? Any unsubmitted data will be lost.", Title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            Cncl = prompt.ToString();

            if (prompt == DialogResult.Yes)
            {
                    if (actCtrl.Name == "btnAdd")
                    {
                        // DROP TABLE
                        SQL_Variable.ExecQuery("DROP TABLE " + tbl_Variable + ";");

                        // DELETE ENTRY FROM TABLE
                        SQL_Variable.AddParam("@PrimeKey", primeKey);
                        SQL_Variable.ExecQuery("DELETE FROM " + tbl_Active + " WHERE Prime=@PrimeKey;");
                    }
                    else
                    {
                    // CLEAR GRID AND RESET WITH ORIGINAL TABLE
                    SQL_Variable.ExecQuery("SELECT * FROM " + tbl_Variable + ";");
                    //dataGridView1.Rows.Clear();
                    //dataGridView1.Columns.Clear();
                    dataGridView1.DataSource = SQL_Variable.DBDT;

                    // DELETE ROWS FROM RELEVANT TABLES
                    for (y = 0; y <= dataGridView1.ColumnCount - 1; y++)
                        {
                            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                            {
                                if (string.IsNullOrEmpty(dataGridView1.Rows[i].Cells[y].Value.ToString()))
                                {
                                    SQL_Variable.AddParam("@PrimKey", dataGridView1.Rows[i].Cells[0].Value.ToString());
                                    SQL_Variable.ExecQuery("DELETE FROM " + tbl_Variable + " WHERE ID_Num=@PrimKey;");
                                }
                            }
                        }
                    }
                    frm.Enabled = true;
                    this.Close();
            }
            else
            {
                return;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            call_cancel();
        }

        private void FormAssessment_PPS_FormClosing(object sender, FormClosingEventArgs e)
        {
            var switchExpr = Cncl;
            switch (switchExpr)
            {
                case null:
                    {
                        call_cancel();
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

            Cncl = null;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            process_Submit();
        }

        private void FormAssessment_PPS_Load(object sender, EventArgs e)
        {
            this.Loader();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.InsertUser();
        }
    }
}

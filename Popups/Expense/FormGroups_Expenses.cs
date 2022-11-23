using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Tinuum_Software_BETA.Popups.Expense
{
    public partial class FormGroups_Expenses : Tinuum_Software_BETA.Popups.Roster.FormAssessment_PPS
    {
        public SQLControl SQL_Update = new SQLControl();
        public DataGridView dgv;
        protected TabControl tab;
        public FormGroups_Expenses()
        {
            InitializeComponent();
            tbl_Prefix = "dtbExpenseDetail_Groups";
        }

        public override void Loader()
        {
            if (DesignMode) return;

            loading = 1;
            int i;
            int count;

            frm = Application.OpenForms[1];
            tab = frm.Controls["tabCtrl"] as TabControl;
            dgv = tab.TabPages[0].Controls["dataGridView1"] as DataGridView;

            tbl_Variable = tbl_Prefix;
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
            dataGridView1.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

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
            
            configName.Visible = false;

            loading = 0;
        }

        public override void process_Submit()
        {
            int i;
            int j;
            string title = "TINUUM SOFTWARE";
            int counter = 0;

            // ENSURE NO DUPLICATE ENTRIES
            
                for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                {
                    for (j = 0; j <= dataGridView1.RowCount - 1; j++)
                    {
                        if (i == j) continue;
                        if (dataGridView1.Rows[i].Cells[2].Value.ToString().ToLower() == dataGridView1.Rows[j].Cells[2].Value.ToString().ToLower())
                        {
                            counter += 1;
                        }
                    }
                }

                if (counter > 0)
                {
                    MessageBox.Show("You cannot enter duplicate values in this field. Retry.", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            

            // ENSURE NO BLANKS
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

        public override void call_cancel()
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
                // CLEAR GRID AND RESET WITH ORIGINAL TABLE
                SQL_Variable.ExecQuery("SELECT * FROM " + tbl_Variable + ";");
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
                
                frm.Enabled = true;
                this.Close();
            }
            else
            {
                return;
            }
        }

        public override void update_active()
        {
            SQL_Update.ExecQuery("SELECT * FROM " + tbl_Variable + ";");
            DataGridViewComboBoxColumn col = (DataGridViewComboBoxColumn)dgv.Columns[35];
            col.DataSource = SQL_Update.DBDT;
            col.DisplayMember = "Expense Group";
            col.ValueMember = "ID_Num";
        }
    }
}

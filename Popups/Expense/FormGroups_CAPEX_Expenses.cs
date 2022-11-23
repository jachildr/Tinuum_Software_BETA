using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Tinuum_Software_BETA.Popups.Expense
{
    public partial class FormGroups_CAPEX_Expenses : Tinuum_Software_BETA.Popups.Expense.FormGroups_Expenses
    {
        public FormGroups_CAPEX_Expenses()
        {
            InitializeComponent();
            tbl_Prefix = "dtbExpenseCAPEX_Detail_Groups";
        }

        public override void Loader()
        {
            if (DesignMode) return;

            loading = 1;
            int i;
            int count;

            frm = Application.OpenForms[1];
            tab = frm.Controls["tabCtrl"] as TabControl;
            dgv = tab.TabPages[1].Controls["dataGridView2"] as DataGridView;

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
        public override void update_active()
        {
            SQL_Update.ExecQuery("SELECT * FROM " + tbl_Variable + ";");
            DataGridViewComboBoxColumn col = (DataGridViewComboBoxColumn)dgv.Columns[18];
            col.DataSource = SQL_Update.DBDT;
            col.DisplayMember = "Expense Group";
            col.ValueMember = "ID_Num";
        }
    }
}

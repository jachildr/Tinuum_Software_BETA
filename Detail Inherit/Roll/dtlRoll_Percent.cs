using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Tinuum_Software_BETA.Popups.Roll;

namespace Tinuum_Software_BETA.Detail_Inherit.Roll
{
    public partial class dtlRoll_Percent : Tinuum_Software_BETA.FormDetail_Percent
    {
        protected DataGridView dgv = Application.OpenForms[3].Controls["dataGridView1"] as DataGridView;
        protected Form frm = Application.OpenForms[3] as Form;
        protected string tbl_dtlPrefix = "dtbRollWeight_Detail";
        protected int frmCol;

        public dtlRoll_Percent()
        {
            InitializeComponent();
            
            tbl_Main = tbl_dtlPrefix + FormRollWeight._primeKey;
        }

        public override void Form_Loader()
        {
            myMethods.SQL_Grab();
            int i;
            int r;
            int n;
            int c;
            string strNum;
            double intNum;
            //string txt = ((frmGeneral)this.Owner).txtVacant.Text;
            frmRow = dgv.CurrentCell.RowIndex;
            frmCol = dgv.CurrentCell.ColumnIndex;

            dataGridView1.ColumnCount = myMethods.Period + 1;
            dataGridView1.RowCount = Mos_Const + 1;
            dataGridView1.Columns[0].HeaderText = "For The Year Ending:";
            dataGridView1.Columns[0].Frozen = true;
            dataGridView1.Columns[0].Width = 140;

            for (i = 1; i <= myMethods.Period; i++)
            {
                var headerNme = myMethods.dteStart.AddMonths((i - 1) * Mos_Const - 1);
                dataGridView1.Columns[i].HeaderText = headerNme.ToString("MMM yyyy");
                dataGridView1.Columns[i].Width = 80;
            }

            // FILL MONTH OF YEAR VALUES
            for (i = 0; i <= Mos_Const - 1; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = myMethods.dteStart.AddMonths(i).ToString("MMMM");
            }

            // AVERAGE ANNUAL RATE VALUE & SET ROW READ ONLY
            dataGridView1.Rows[Mos_Const].Cells[0].Value = "Average Annual Rate";

            for (i = 1; i <= dataGridView1.ColumnCount - 1; i++)
            {
                dataGridView1.Rows[Mos_Const].Cells[i].ReadOnly = true;
            }

            // FILL DATAGRIDVIEW WITH DT VALUES 
            SQL_DETAIL.ExecQuery("SELECT * FROM " + tbl_Main + ";");
            try
            {
                for (r = 0; r <= Mos_Const - 1; r++)
                {
                    for (n = 1; n <= myMethods.Period; n++)
                    {
                        c = r + (n - 1) * Mos_Const + 1 + 1; // PLUS 2 EFFECTIVELY BECAUSE CELL FILL DATA STARTS ON COL 2 IN DATABASE
                        dataGridView1.Rows[r].Cells[n].Value = SQL_DETAIL.DBDT.Rows[frmRow][c];
                    }
                }
            }
            catch (Exception ex)
            {
            }

            // FORMAT FILLED DB DATA
            try
            {
                for (n = 1; n <= myMethods.Period; n++)
                {
                    for (r = 0; r <= Mos_Const - 1; r++)
                    {
                        {
                            strNum = Convert.ToString(dataGridView1.Rows[r].Cells[n].Value);
                            if (Information.IsNumeric(strNum) == true)
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
            // MAKE 1ST COLUMN READ ONLY
            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                dataGridView1.Rows[i].Cells[0].ReadOnly = true;
            }

            dataGridView1.Columns[0].DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView1.Columns[0].DefaultCellStyle.SelectionForeColor = Color.Black;

            // MAKE DGV COLUMNS NOT SORTABLE
            foreach (DataGridViewColumn Col in dataGridView1.Columns)
            {
                Col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            // COLUMN TEXT ALIGNMENT

            dataGridView1.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            for (i = 1; i <= dataGridView1.ColumnCount - 1; i++)
            {
                dataGridView1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            // MAKE LAST ROW READ ONLY
            dataGridView1.Rows[Mos_Const].DefaultCellStyle.SelectionBackColor = SystemColors.Control;
            dataGridView1.Rows[Mos_Const].DefaultCellStyle.SelectionForeColor = SystemColors.ControlDark;
            dataGridView1.Rows[Mos_Const].DefaultCellStyle.BackColor = SystemColors.Control;
            dataGridView1.Rows[Mos_Const].DefaultCellStyle.ForeColor = SystemColors.ControlDark;
            dataGridView1.Rows[Mos_Const].DefaultCellStyle.Font = new Font("Sans Serif", 8.25F, FontStyle.Italic);
        }

        public virtual void Write_Detail()
        {
            dgv.Rows[frmRow].Cells[frmCol].Value = "Detail";
            dgv.Rows[frmRow].Cells[frmCol].Selected = true;
        }

        private void Form_Cancel()
        {
            dgv.CurrentCell = dgv.Rows[frmRow].Cells[frmCol];
            dgv.CurrentCell.Value = null;
            frm.ActiveControl = null;
        }

        public override void btnCancel_Click(object sender, EventArgs e)
        {
            Form_Cancel();
            base.btnCancel_Click(sender, e);
            frm.Enabled = true;
        }

        public override void btnSubmit_Click(object sender, EventArgs e)
        {
            int r;
            int n;
            int mos_Num;
            string sel_cell;
            string tbl_Col;
            double dec_Val;
            string cmdUpdate;
            int num = Convert.ToInt32(dgv.Rows[frmRow].Cells[0].Value);

            for (r = 0; r <= Mos_Const - 1; r++)
            {
                for (n = 1; n <= myMethods.Period; n++)
                {
                    if (dataGridView1.Rows[r].Cells[n].Value == DBNull.Value)
                    {
                        MessageBox.Show("You must enter valid data before continuing.", "TINUUM SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            for (r = 0; r <= Mos_Const - 1; r++)
            {
                for (n = 1; n <= myMethods.Period; n++)
                {
                    sel_cell = Convert.ToString(dataGridView1.Rows[r].Cells[n].Value.ToString());
                    mos_Num = r + (n - 1) * Mos_Const + 1;
                    tbl_Col = "month" + mos_Num;
                    dec_Val = myMethods.ToDecimal(sel_cell); //CHECK
                    SQL_DETAIL.AddParam("@PrimKey", num);
                    //SQL_DETAIL.AddParam("@period", myMethods.Period);
                    SQL_DETAIL.AddParam("@months_data", dec_Val);
                    cmdUpdate = "UPDATE " + tbl_Main + " SET " + tbl_Col + "=@months_data WHERE ID_Num=@PrimKey;";
                    SQL_DETAIL.ExecQuery(cmdUpdate);
                }
            }

            if (SQL_DETAIL.HasException(true))
            {
                return;
            }
            Write_Detail();
            frm.ActiveControl = null;
            frm.Enabled = true;
            Dispose();
        }

        private void dtlRoll_Percent_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form_Cancel();
            Dispose();
            frm.Enabled = true;
        }
    }
}

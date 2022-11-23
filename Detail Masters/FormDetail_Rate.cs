using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Tinuum_Software_BETA
{
    [CLSCompliant(true)]
    public partial class FormDetail_Rate : Tinuum_Software_BETA.FormDetail_Percent
    {
        private int Mos_Const = 12;
        protected int frmRow;
        protected int frmCol;
        protected DataGridView dgv;
        protected Form frm;
        protected Form prntFrm;
        protected Panel pane;
        public FormDetail_Rate()
        {
            InitializeComponent();
            prntFrm = Application.OpenForms[0];
            pane = prntFrm.Controls["panel3"] as Panel;
            frm = Application.OpenForms[1];
            string name = frm.Name;
            dgv = frm.Controls["dataGridView1"] as DataGridView;
            frmRow = dgv.CurrentCell.RowIndex;
            frmCol = dgv.CurrentCell.ColumnIndex;
        }

        public override void frmDetail_Percent_Load(object sender, EventArgs e)
        {
            Form_Loader();
            Rate_Growth();
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
            //MessageBox.Show(Application.OpenForms[4].Name.ToString());

            // SET DGV SPECS    
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
            dataGridView1.Rows[Mos_Const].Cells[0].Value = "Effective Annual Rate";
            for (i = 1; i <= dataGridView1.ColumnCount - 1; i++)
            {
                dataGridView1.Rows[Mos_Const].Cells[i].ReadOnly = true;
            }

            // FILL DATAGRIDVIEW WITH DT VALUES 
            SQL_DETAIL.ExecQuery("SELECT * FROM dtbRateDetail;");
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

            // FIRST COLUMN SPECS    
            dataGridView1.Columns[0].DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView1.Columns[0].DefaultCellStyle.SelectionForeColor = Color.Black;


            // MAKE DGV COLUMNS NOT SORTABLE
            foreach (DataGridViewColumn Col in dataGridView1.Columns)
            {
                Col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            // COLUMN TEXT ALIGNMENT
            {
                dataGridView1.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                for (i = 1; i <= dataGridView1.ColumnCount - 1; i++)
                {
                    dataGridView1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }

            }

            // MAKE LAST ROW READ ONLY
            dataGridView1.Rows[Mos_Const].DefaultCellStyle.SelectionBackColor = SystemColors.Control;
            dataGridView1.Rows[Mos_Const].DefaultCellStyle.SelectionForeColor = SystemColors.ControlDark;
            dataGridView1.Rows[Mos_Const].DefaultCellStyle.BackColor = SystemColors.Control;
            dataGridView1.Rows[Mos_Const].DefaultCellStyle.ForeColor = SystemColors.ControlDark;
            dataGridView1.Rows[Mos_Const].DefaultCellStyle.Font = new Font("Sans Serif", 8.25F, FontStyle.Italic);
        }

        public override void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string strNum;
            double intNum;

            // FORMAT CELL
            if (dataGridView1.CurrentCell.ColumnIndex == 0)
            {
                return;
            }
            else
            {
                {
                    strNum = Convert.ToString(dataGridView1.CurrentCell.Value);
                    if (Information.IsNumeric(strNum) == true)
                    {
                        intNum = Convert.ToDouble(strNum);
                        dataGridView1.CurrentCell.Value = String.Format("{0:p}", intNum);
                    }
                    else
                    {
                        MessageBox.Show("You must enter a numeric value.", "TINUUM SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dataGridView1.CurrentCell.Value = "";
                        dataGridView1.CurrentCell.Selected = true;
                    }
                }
            }

            // CALCULATE EFFECTIVE ANNUAL RATE
            Rate_Growth();
        }

        public override void btnSubmit_Click(object sender, EventArgs e)
        {
            int num = Convert.ToInt32(dgv.Rows[frmRow].Cells[0].Value);
            int cent = 100;
            int r;
            int n;
            int mos_Num;
            string sel_cell;
            string tbl_Col;
            double dec_Val;
            string tbl_Name;
            string cmdUpdate;
            tbl_Name = "dtbRateDetail";

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
                    sel_cell = Convert.ToString(dataGridView1.Rows[r].Cells[n].Value);
                    mos_Num = r + (n - 1) * Mos_Const + 1;
                    tbl_Col = "month" + mos_Num;
                    dec_Val = Convert.ToDouble(sel_cell.Substring(0, sel_cell.Length - 1)) / cent; //CHECK
                    SQL_DETAIL.AddParam("@PrimKey", num);
                    SQL_DETAIL.AddParam("@months_data", dec_Val);
                    SQL_DETAIL.AddParam("@column_name", tbl_Col);
                    cmdUpdate = "UPDATE " + tbl_Name + " SET " + tbl_Col + "=@months_data WHERE ID_Num=@PrimKey;";
                    SQL_DETAIL.ExecQuery(cmdUpdate);
                }
            }

            if (SQL_DETAIL.HasException(true))
                return;
            Write_Detail();
            frm.ActiveControl = null;
            Dispose();
        }

        public void Rate_Growth()
        {
            int cnst = 1;
            var Rates = new List<double>();
            var Growth = new List<double>();
            var Difference = new List<double>();
            var Summations = new List<double>();
            int i;
            int r;
            double Val;
            int cent = 100;
            int Period = myMethods.Period;

            try
            {
                for (i = 1; i <= Period; i++)
                {
                    for (r = 0; r <= Mos_Const - 1; r++) // CHECK ROW NUMBER WHEN MAKING DYNAMIC VARIATION
                    {
                        if (Information.IsNumeric((Convert.ToString(dataGridView1.Rows[r].Cells[i].Value).Substring(0, Convert.ToString(dataGridView1.Rows[r].Cells[i].Value).Length - 1)))) //CHECK
                        {
                            Val = cnst + Convert.ToDouble(Convert.ToString(dataGridView1.Rows[r].Cells[i].Value).Substring(0, Convert.ToString(dataGridView1.Rows[r].Cells[i].Value).Length - 1)) / cent;
                        }
                        else
                        {
                            Val = cnst;
                        }

                        Rates.Add(Val);
                    }
                }

                Val = 1;
                for (i = 1; i <= Period * Mos_Const; i++)
                {
                    if (i % Mos_Const == 1)
                    {
                        Val = 1;
                    }
                    Val *= cnst * Rates[i - 1];
                    Growth.Add(Val - 1);
                }

                Val = 0;
                for (i = 1; i <= Period * Mos_Const; i++)
                {
                    if (i % Mos_Const == 1)
                    {
                        Val = Growth[i - 1];
                    }
                    else
                    {
                        Val = Growth[i - 1] - Growth[i - 2];
                    } // DIFFERENCE BETWEEN NEXT IN SEQUENCE LESS PREVIOUS

                    Difference.Add(Val);
                }

                for (i = 1; i <= Period; i++)
                {
                    Val = 0;
                    for (r = 0; r <= Mos_Const - 1; r++)
                    {
                        Val += Difference[(i - 1) * Mos_Const + r];
                    }
                    Summations.Add(Val);
                }

                for (i = 1; i <= Period; i++)
                {
                    dataGridView1.Rows[Mos_Const].Cells[i].Value = String.Format("{0:p}", Summations[i - 1]);
                }

            }
            catch (Exception ex)
            {
            }
        }

        private void Write_Detail()
        {
            int colStart = 5;
            int colEnd;
            int i;

            colEnd = colStart + myMethods.Period - 1;

            for (i = colStart; i <= colEnd; i++)
            {
                dgv.Rows[frmRow].Cells[i].Value = "Detail";
                dgv.Rows[frmRow].Cells[i].Selected = true;
            }
        }

        public override void btnRow_Click(object sender, EventArgs e)
        {
            int r;
            int n;
            int x;
            // Dim y As Integer

            r = dataGridView1.CurrentCell.RowIndex; // STATIC
            n = dataGridView1.CurrentCell.ColumnIndex; // STATIC
            try
            {
                for (x = n + 1; x <= dataGridView1.ColumnCount - 1; x++)
                {
                    dataGridView1.Rows[r].Cells[x].Value = dataGridView1.CurrentCell.Value;
                    dataGridView1.Rows[r].Cells[x].Selected = true;
                }
            }
            catch (Exception ex)
            {
            }

            Rate_Growth();
        }

        public override void btnCol_Click(object sender, EventArgs e)
        {
            int r;
            int n;
            int y;
            
            r = dataGridView1.CurrentCell.RowIndex; // STATIC
            n = dataGridView1.CurrentCell.ColumnIndex; // STATIC
            
            try
            {
                for (y = r + 1; y <= Mos_Const - 1; y++)
                {
                    dataGridView1.Rows[y].Cells[n].Value = dataGridView1.CurrentCell.Value;
                    dataGridView1.Rows[y].Cells[n].Selected = true;
                }
            }
            catch (Exception ex)
            {
            }

            Rate_Growth();
        }

        public override void btnAll_Click(object sender, EventArgs e)
        {
            int r;
            int n;
            int x;
            int y;
            
            r = dataGridView1.CurrentCell.RowIndex; // STATIC
            n = dataGridView1.CurrentCell.ColumnIndex; // STATIC
            
            try
            {
                for (y = r + 1; y <= Mos_Const - 1; y++)
                {
                    dataGridView1.Rows[y].Cells[n].Value = dataGridView1.CurrentCell.Value;
                    dataGridView1.Rows[y].Cells[n].Selected = true;
                }

                for (x = n + 1; x <= dataGridView1.ColumnCount - 1; x++)
                {
                    for (y = 0; y <= Mos_Const - 1; y++)
                    {
                        dataGridView1.Rows[y].Cells[x].Value = dataGridView1.CurrentCell.Value;
                        dataGridView1.Rows[y].Cells[x].Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
            }

            Rate_Growth();
        }

        private void Form_Cancel()
        {
            dgv.CurrentCell = dgv.Rows[frmRow].Cells[3];
            dgv.CurrentCell.Value = "Annually";
            frm.ActiveControl = null;
        }

        public override void btnCancel_Click(object sender, EventArgs e)
        {
            Form_Cancel();
            base.btnCancel_Click(sender, e);
        }

        private void frmDetail_Rate_Closing(object sender, CancelEventArgs e)
        {
            Form_Cancel();
            Dispose();
        }
    }
}

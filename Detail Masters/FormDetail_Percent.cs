using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic;
namespace Tinuum_Software_BETA
{
    [CLSCompliant(true)]
    public partial class FormDetail_Percent : Form
    {
        protected SQLControl SQL_DETAIL = new SQLControl();
        protected int Mos_Const = 12;
        protected string tbl_Main = "dtbHomeVacant";
        protected int frmRow;

        public FormDetail_Percent()
        {
            InitializeComponent();
            frmRow = 0;
        }
        
        public virtual void frmDetail_Percent_Load(object sender, EventArgs e)
        {
            
            Form_Loader();
            Percent_Calculate();
        }

        public virtual void Form_Loader()
        {
            if (DesignMode) return;
            myMethods.SQL_Grab();
            int i;
            int r;
            int n;
            int c;
            string prntRef;
            string strNum;
            double intNum;
            TextBox vcnt = Application.OpenForms[0].Controls["txtVacant"] as TextBox;
            //string txt = ((frmGeneral)this.Owner).txtVacant.Text;

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
                prntRef = vcnt.Text.Trim();
                if (Information.IsNumeric(prntRef.Substring(0, prntRef.Length - 1)))
                {
                    for (r = 0; r <= Mos_Const - 1; r++)
                    {
                        for (n = 1; n <= myMethods.Period; n++)
                        {
                            dataGridView1.Rows[r].Cells[n].Value = vcnt.Text;
                        }
                    }
                }
                else
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
                                if (Convert.ToDouble(strNum) <= 1)
                                {
                                    intNum = Convert.ToDouble(strNum);
                                    dataGridView1.Rows[r].Cells[n].Value = String.Format("{0:p}", intNum);
                                }
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

        public virtual void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string strNum;
            double intNum;
            var List = new List<double>();
            string pctVal;
            int cent = 100;
            double sumVal;

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
                        if (Convert.ToDouble(strNum) <= 1)
                        {
                            intNum = Convert.ToDouble(strNum);
                            dataGridView1.CurrentCell.Value = String.Format("{0:p}", intNum);
                        }
                        else
                        {
                            MessageBox.Show("Your entry must be less than 100%.", "TINUUM SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            dataGridView1.CurrentCell.Value = "";
                            dataGridView1.CurrentCell.Selected = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("You must enter a numeric value.", "TINUUM SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dataGridView1.CurrentCell.Value = "";
                        dataGridView1.CurrentCell.Selected = true;
                    }
                }
            }

            // CALCULATE AVAERAGE ANNUAL RATE
            try
            {
                for (int i = 0; i <= Mos_Const - 1; i++)
                {
                    pctVal = Convert.ToString(dataGridView1.Rows[i].Cells[dataGridView1.CurrentCell.ColumnIndex].Value);
                    List.Add(Convert.ToDouble(pctVal.Substring(0, pctVal.Length - 1)) / cent);
                }

                sumVal = 0;
                for (int i = 0; i <= List.Count - 1; i++)
                {
                    sumVal += List[i];
                }

                dataGridView1.Rows[Mos_Const].Cells[dataGridView1.CurrentCell.ColumnIndex].Value = String.Format("{0:p}", sumVal / Mos_Const);
            }
            catch (Exception ex)
            {
            }
        }

        private void Percent_Calculate()
        {
            var List = new List<double>();
            string pctVal;
            int cent = 100;
            double sumVal;
            int i;
            int n;
            int itmStart;

            try
            {
                for (n = 1; n <= dataGridView1.ColumnCount - 1; n++)
                {
                    for (i = 0; i <= Mos_Const - 1; i++)
                    {
                        pctVal = Convert.ToString(dataGridView1.Rows[i].Cells[n].Value);
                        List.Add(Convert.ToDouble(pctVal.Substring(0, pctVal.Length - 1)) / cent);
                    }
                }

                for (n = 1; n <= dataGridView1.ColumnCount - 1; n++)
                {
                    sumVal = 0;
                    itmStart = (n - 1) * Mos_Const;

                    for (i = itmStart; i <= itmStart + Mos_Const - 1; i++)
                    {
                        sumVal += List[i];
                    }

                    dataGridView1.Rows[Mos_Const].Cells[n].Value = String.Format("{0:p}", sumVal / Mos_Const);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public virtual void btnSubmit_Click(object sender, EventArgs e)
        {
            int cent = 100;
            int r;
            int n;
            int mos_Num;
            string sel_cell;
            string tbl_Col;
            double dec_Val;
            string cmdUpdate;

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
                    SQL_DETAIL.AddParam("@PrimKey", frmRow);
                    SQL_DETAIL.AddParam("@period", myMethods.Period);
                    SQL_DETAIL.AddParam("@months_data", dec_Val);
                    cmdUpdate = "UPDATE " + tbl_Main + " SET period=@period, " + tbl_Col + "=@months_data WHERE ID_Num=@PrimKey;";
                    SQL_DETAIL.ExecQuery(cmdUpdate);
                }
            }

            if (SQL_DETAIL.HasException(true))
            {
                return;
            }
        }

        public virtual void btnRow_Click(object sender, EventArgs e)
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
            //CALL CALC FOR SELECT COMPONENTS -- ADJUST WITH SWITCH
            Percent_Calculate();
        }

        public virtual void btnCol_Click(object sender, EventArgs e)
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
            //CALL CALC FOR SELECT COMPONENTS -- ADJUST WITH SWITCH
            Percent_Calculate();
        }

        public virtual void btnAll_Click(object sender, EventArgs e)
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
            //CALL CALC FOR SELECT COMPONENTS -- ADJUST WITH SWITCH
            Percent_Calculate();
        }

        public virtual void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
            Dispose();
        }

        public virtual void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // DUMMY
        }

        public virtual void DataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            // PLACE HOLDER
        }

        public virtual void DataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            // PLACE HOLDER
        }

        public virtual void DataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            // PLACE HOLDER
        }

        public virtual void DataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // PLACE HOLDER
        }

        public virtual void FormDetail_Percent_Shown(object sender, EventArgs e)
        {
            // PLACE HOLDER
        }

        public virtual void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // PLACE HOLDER
        }
    }
}

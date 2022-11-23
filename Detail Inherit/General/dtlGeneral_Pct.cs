using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;

namespace Tinuum_Software_BETA
{
    [CLSCompliant(true)]

    public partial class dtlGeneral : FormDetail_Percent
    {
        public SQLControl SQL_DETAIL = new SQLControl();
        private int Mos_Const = 12;

        public void dummyLoad(DataGridView dataGridView1)
        {
            myMethods.SQL_Grab();
            int i;
            int r;
            int n;
            int c;
            string prntRef;
            string strNum;
            double intNum;
            //string txt = ((frmGeneral)this.Owner).txtVacant.Text;
            TextBox vcnt = Application.OpenForms[0].Controls["txtVacant"] as TextBox;

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
            SQL_DETAIL.ExecQuery("SELECT * FROM dtbHomeVacant;");
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
                            dataGridView1.Rows[r].Cells[n].Value = SQL_DETAIL.DBDT.Rows[0][c];
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

        public void dummyCellEdit(DataGridView dataGridView1)
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

        public void dummyPctCalc(DataGridView dataGridView1)
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

        public void dummySubmit(DataGridView dataGridView1)
        {
            int num = 1;
            int cent = 100;
            int r;
            int n;
            int mos_Num;
            string sel_cell;
            string tbl_Col;
            double dec_Val;
            string tbl_Name;
            string cmdUpdate;
            tbl_Name = "dtbHomeVacant";

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
                    SQL_DETAIL.AddParam("@period", myMethods.Period);
                    SQL_DETAIL.AddParam("@months_data", dec_Val);
                    SQL_DETAIL.AddParam("@column_name", tbl_Col);
                    cmdUpdate = "UPDATE " + tbl_Name + " SET period=@period, " + tbl_Col + "=@months_data WHERE ID_Num=@PrimKey;";
                    SQL_DETAIL.ExecQuery(cmdUpdate);
                }
            }

            if (SQL_DETAIL.HasException(true))
            {
                return;
            }

        }
    }
}

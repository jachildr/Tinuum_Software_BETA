using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace Tinuum_Software_BETA.Detail_Inherit.Expense
{
    public partial class dtlExpense_Dynamic : Tinuum_Software_BETA.Detail_Inherit.Roll.dtlRoll_Dynamic
    {
        protected TabControl tab;
        public dtlExpense_Dynamic()
        {
            InitializeComponent();
            switch (dgvExpense_OPEX._index)
            {
                case 8:
                    {
                        tbl_Name = "dtbExpenseDetail_GeneralRate"; //VALUES VIEW
                        tbl_Dynamic = "dtbExpenseDetailDynamic_GeneralRate"; //RATES MINOR
                        tbl_MajorDyna = "dtbExpenseDynamic_GeneralRate"; //RATES MAJOR
                        frm = Application.OpenForms[1] as Form;
                        tab = frm.Controls["tabCtrl"] as TabControl;
                        dgv = tab.TabPages[0].Controls["dataGridView1"] as DataGridView;
                    }
                    break;
                case 14:
                    {
                        tbl_Name = "dtbExpenseDetail_Wage"; //VALUES VIEW
                        tbl_Dynamic = "dtbExpenseDetailDynamic_Wage"; //RATES MINOR
                        tbl_MajorDyna = "dtbExpenseDynamic_Wage"; //RATES MAJOR
                        frm = Application.OpenForms[1] as Form;
                        tab = frm.Controls["tabCtrl"] as TabControl;
                        dgv = tab.TabPages[0].Controls["dataGridView1"] as DataGridView;
                    }
                    break;
                case 17:
                    {
                        tbl_Name = "dtbExpenseDetail_StaffQuantity"; //VALUES VIEW
                        tbl_Dynamic = "dtbExpenseDetailDynamic_StaffQuantity"; //RATES MINOR
                        tbl_MajorDyna = "dtbExpenseDynamic_StaffQuantity"; //RATES MAJOR
                        frm = Application.OpenForms[1] as Form;
                        tab = frm.Controls["tabCtrl"] as TabControl;
                        dgv = tab.TabPages[0].Controls["dataGridView1"] as DataGridView;
                    }
                    break;
                case 19:
                    {
                        tbl_Name = "dtbExpenseDetail_Shift"; //VALUES VIEW
                        tbl_Dynamic = "dtbExpenseDetailDynamic_Shift"; //RATES MINOR
                        tbl_MajorDyna = "dtbExpenseDynamic_Shift"; //RATES MAJOR
                        frm = Application.OpenForms[1] as Form;
                        tab = frm.Controls["tabCtrl"] as TabControl;
                        dgv = tab.TabPages[0].Controls["dataGridView1"] as DataGridView;
                    }
                    break;
                case 22:
                    {
                        tbl_Name = "dtbExpenseDetail_LeftRatio"; //VALUES VIEW
                        tbl_Dynamic = "dtbExpenseDetailDynamic_LeftRatio"; //RATES MINOR
                        tbl_MajorDyna = "dtbExpenseDynamic_LeftRatio"; //RATES MAJOR
                        frm = Application.OpenForms[1] as Form;
                        tab = frm.Controls["tabCtrl"] as TabControl;
                        dgv = tab.TabPages[0].Controls["dataGridView1"] as DataGridView;
                    }
                    break;
                case 24:
                    {
                        tbl_Name = "dtbExpenseDetail_RightRatio"; //VALUES VIEW
                        tbl_Dynamic = "dtbExpenseDetailDynamic_RightRatio"; //RATES MINOR
                        tbl_MajorDyna = "dtbExpenseDynamic_RightRatio"; //RATES MAJOR
                        frm = Application.OpenForms[1] as Form;
                        tab = frm.Controls["tabCtrl"] as TabControl;
                        dgv = tab.TabPages[0].Controls["dataGridView1"] as DataGridView;
                    }
                    break;
            }
        }
        public override void fill_DataTable()
        {
            int r;
            int n;
            int c;

            // FILL DATAGRIDVIEW WITH DT VALUES
            if (Information.IsNumeric(dgv.CurrentCell.Value))
            {
                for (r = 0; r <= Mos_Const - 1; r++)
                {
                    for (n = 1; n <= myMethods.Period; n++)
                    {
                        dataGridView1.Rows[r].Cells[n].Value = dgv.CurrentCell.Value;
                    }
                }
            }
            else
            {
                try
                {
                    for (r = 0; r <= Mos_Const - 1; r++)
                    {
                        for (n = 1; n <= myMethods.Period; n++)
                        {
                            c = r + (n - 1) * Mos_Const + 1 + 1; // PLUS 2 EFFECTIVELY BECAUSE CELL FILL DATA STARTS ON COL 2 IN DATABASE
                            dataGridView1.Rows[r].Cells[n].Value = SQL_DETAIL.DBDT.Rows[frmRow][c + 1];
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }
        public override void Form_Loader()
        {
            if (DesignMode) return;

            myMethods.SQL_Grab();
            int i;
            int r;
            int n;
            int c;
            int j;
            string strNum;
            double intNum;

            frmRow = dgv.CurrentCell.RowIndex;
            frmCol = dgv.CurrentCell.ColumnIndex;

            List<string> fillRates = new List<string>();
            int count = 0;

            // QUERY DATA TABLES
            SQL_Rates.ExecQuery("SELECT * FROM " + tbl_Rates + ";");
            SQL_DETAIL.ExecQuery("SELECT * FROM " + tbl_Name + ";");
            SQL_Main.ExecQuery("SELECT * FROM " + tbl_Categories + ";");
            SQL_DB.ExecQuery("SELECT * FROM " + tbl_Dynamic + ";");
            SQL_Major.ExecQuery("SELECT * FROM " + tbl_MajorDyna + ";");

            // SET DGV SPECS    
            dataGridView1.ColumnCount = myMethods.Period + 1;
            dataGridView1.RowCount = Mos_Const + 3; //NEED RATE OPTION; RATE SELECT IF MANUAL; AND VALUE ADJUTED WITH GROWTH
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
            dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[0].Value = "Effective Annual Rate";
            dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[0].ReadOnly = true;

            for (i = 1; i <= dataGridView1.ColumnCount - 1; i++)
            {
                dataGridView1.Rows[Mos_Const].Cells[i].ReadOnly = true;
            }

            // FILL DATAGRIDVIEW WITH DT VALUES 
            fill_DataTable();

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
                                dataGridView1.Rows[r].Cells[n].Value = intNum;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            // MAKE 1ST COLUMN TO 11 READ ONLY
            for (i = 0; i <= Mos_Const - 1; i++)
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
            dataGridView1.Rows[dataGridView1.RowCount - 1].ReadOnly = true;
            dataGridView1.Rows[dataGridView1.RowCount - 1].DefaultCellStyle.SelectionBackColor = SystemColors.Control;
            dataGridView1.Rows[dataGridView1.RowCount - 1].DefaultCellStyle.SelectionForeColor = SystemColors.ControlDark;
            dataGridView1.Rows[dataGridView1.RowCount - 1].DefaultCellStyle.BackColor = SystemColors.Control;
            dataGridView1.Rows[dataGridView1.RowCount - 1].DefaultCellStyle.ForeColor = SystemColors.ControlDark;
            //DataGridView1.Rows[DataGridView1.RowCount - 1].DefaultCellStyle.Font = new Font("Sans Serif", 8.25F, FontStyle.Italic);

            for (i = 1; i <= dataGridView1.ColumnCount - 1; i++)
            {
                dataGridView1.Rows[Mos_Const].Cells[i].ReadOnly = true;
                dataGridView1.Rows[Mos_Const].Cells[i].Value = "";
                dataGridView1.Rows[Mos_Const].Cells[i].Style.BackColor = SystemColors.Control;
                dataGridView1.Rows[Mos_Const].Cells[i].Style.ForeColor = SystemColors.ControlDark;
                dataGridView1.Rows[Mos_Const].Cells[i].Style.SelectionBackColor = SystemColors.Control;
                dataGridView1.Rows[Mos_Const].Cells[i].Style.SelectionForeColor = SystemColors.ControlDark;
            }

            // FILL LIST OF STRINGS

            records = SQL_Main.RecordCount;
            strRates.Add("");
            for (i = 0; i <= records - 1; i++)
            {
                strRates.Add(Convert.ToString(SQL_Main.DBDT.Rows[i][2]));
            }
            strRates.Add("Detail");

            // CHANGE TXT GRIDVIEW CELLS TO COMBO CELLS
            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                var switchExpr = i;
                switch (switchExpr)
                {
                    case 12:
                        {
                            var newCell = new DataGridViewComboBoxCell();
                            // ADD SPECS FOR 
                            newCell.DataSource = strRates;
                            newCell.FlatStyle = FlatStyle.Popup;
                            newCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                            newCell.DisplayStyleForCurrentCellOnly = false;
                            dataGridView1.Rows[i].Cells[0] = newCell;

                            break;
                        }
                    case 13:
                        {
                            var newCell = new DataGridViewComboBoxCell();
                            // ADD SPECS FOR 
                            newCell.Items.Add("");
                            newCell.Items.Add("Annually");
                            newCell.Items.Add("Semi-Annually");
                            newCell.Items.Add("Quarterly");
                            newCell.Items.Add("Monthly");
                            newCell.FlatStyle = FlatStyle.Popup;
                            newCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                            newCell.DisplayStyleForCurrentCellOnly = false;
                            dataGridView1.Rows[i].Cells[0] = newCell;

                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }

            // FILL RATES FROM MAJOR DYNAMIC DT 
            for (i = 1; i <= myMethods.Period + 2; i++)
            {
                fillRates.Add(Convert.ToString(SQL_Major.DBDT.Rows[frmRow][i + 2]));
            }

            //SWITCH EXPR TO FILL RATE DATA
            var switchExprRate = fillRates[0];
            switch (switchExprRate)
            {
                case "0":
                    {
                        // SET MAJOR RATE SELECTION TO DETAIL
                        dataGridView1.Rows[Mos_Const].Cells[0].Value = "Detail";

                        // FILL RATES FROM DATATABLE
                        for (j = 0; j <= myMethods.Period * Mos_Const - 1; j++)
                        {
                            Rates.Add(Convert.ToDouble(SQL_DB.DBDT.Rows[frmRow][j + 3]));
                        }
                        for (i = 0; i <= dataGridView1.ColumnCount - 1; i++)
                        {
                            dataGridView1.Rows[Mos_Const + 1].Cells[i].Value = fillRates[i + 1];
                        }

                        // RUN METHODS & TERMINATE CALL TO VALUE CHANGE EVENT
                        terminate = 1;
                        {
                            Percent_Change();
                            Rate_Growth();
                            effective_Value();
                        }
                        terminate = 0;
                        break;
                    }
                case "1":
                case "":
                    {
                        // CHECK IF RATE SELECTION EXISTS
                        for (i = 0; i <= strRates.Count - 1; i++)
                        {
                            if (fillRates[1] == strRates[i])
                            {
                                count += 1;
                            }
                        }
                        // IF SO FILL IF NOT SET TO NULL
                        if (count > 0)
                        {
                            dataGridView1.Rows[Mos_Const].Cells[0].Value = fillRates[1];
                        }
                        else
                        {
                            dataGridView1.Rows[Mos_Const].Cells[0].Value = null;
                        }
                        // EITHER WAY SET MANUAL RATES ROW TO INVISIBLE
                        DataGridViewBand band = dataGridView1.Rows[Mos_Const + 1];
                        band.Visible = false;
                        break;

                    }
                default:
                    {
                        break;
                    }
            }
        }
    }
}

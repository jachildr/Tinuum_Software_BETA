using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Syncfusion.Windows.Forms.Tools;

namespace Tinuum_Software_BETA
{
    [CLSCompliant(true)]
    public partial class FormDetail_Dynamic : Tinuum_Software_BETA.FormDetail_Percent
    {

        protected Form frm = Application.OpenForms[1] as Form;
        protected DataGridView dgv = Application.OpenForms[1].Controls["dataGridView1"] as DataGridView;
        protected int Mos_Const = 12;
        protected string tbl_Name = "dtbMarketDetail"; //MAIN VIEW
        protected string tbl_MajorDyna = "dtbMarketDynamic"; //RATES MAJOR
        protected string tbl_Dynamic = "dtbMarketDetailDynamic"; //RATES MINOR
        protected string tbl_Categories = "dtbRateVerse"; //STATIC
        protected string tbl_Rates = "dtbRateDetail"; //STATIC
        protected List<string> strRates = new List<string>();
        protected int records;
        protected SQLControl SQL_Rates = new SQLControl();
        protected SQLControl SQL_Main = new SQLControl();
        protected SQLControl SQL_DB = new SQLControl();
        protected SQLControl SQL_Major = new SQLControl();
        protected List<double> rateRange = new List<double>();
        protected List<double> Rates = new List<double>();
        protected List<double> Summations = new List<double>();
        protected int terminate = 0;
        protected int frmRow;
        protected int frmCol;

        public FormDetail_Dynamic()
        {
            InitializeComponent();
        }

        public override void frmDetail_Percent_Load(object sender, EventArgs e)
        {
            Form_Loader();
        }

        public virtual void fill_DataTable()
        {
            // FILL DATAGRIDVIEW WITH DT VALUES 
            int r;
            int n;
            int c;

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
            for (i = 0; i <= records -1; i++)
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
                fillRates.Add(Convert.ToString(SQL_Major.DBDT.Rows[frmRow][i + 1]));
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
                            Rates.Add(Convert.ToDouble(SQL_DB.DBDT.Rows[frmRow][j + 2]));
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
                            if(fillRates[1] == strRates[i])
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

        public override void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string strNum;
            double intNum;
            int r;
            int n;
            int j;

            // FORMAT CELL
            if (dataGridView1.CurrentCell.ColumnIndex == 0)
            {
                // Nothing
            }
            else
            {
                {
                    strNum = Convert.ToString(dataGridView1.CurrentCell.Value);
                    if (Information.IsNumeric(strNum) == true)
                    {
                        intNum = Convert.ToDouble(strNum);
                        dataGridView1.CurrentCell.Value = intNum;
                        effective_Value();
                    }
                    else
                    {
                        MessageBox.Show("You must enter a numeric value.", "TINUUM SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dataGridView1.CurrentCell.Value = "";
                        dataGridView1.CurrentCell.Selected = true;
                    }
                }
            }

            // AUTO EXTEND COLUMN DATA
            r = dataGridView1.CurrentCell.RowIndex; // STATIC
            n = dataGridView1.CurrentCell.ColumnIndex; // STATIC
            try
            {
                var switchExpr = dataGridView1.CurrentCell.RowIndex;
                switch (switchExpr)
                {
                    case 12:
                        {
                            if (dataGridView1.CurrentCell.Value == null)
                            {
                                zero_to_DB(); //EFFECTIVE VALUE CALLED IN CHANGE EVENT
                            }
                            break;
                        }
                    case 13:
                        {
                            if (e.ColumnIndex == 0 && dataGridView1.CurrentCell.Value == null) return;
                            for (j = n + 1; j <= dataGridView1.ColumnCount - 1; j++)
                            {
                                if (dataGridView1.CurrentCell.ColumnIndex == 0) break;
                                dataGridView1.Rows[r].Cells[j].Value = dataGridView1.CurrentCell.Value;
                                dataGridView1.Rows[r].Cells[j].Selected = true;
                            }
                            Percent_Change();
                            if (dataGridView1.Rows[Mos_Const + 1].Cells[0].Value == null)
                            {
                                MessageBox.Show("You must select a valid rate index before continuing.", "TINUUM SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                dataGridView1.Rows[Mos_Const + 1].Cells[0].Selected = true;
                                return;
                            }
                            Rate_To_DB();
                            Rate_Growth();
                            effective_Value();
                            break;
                        }

                    default:
                        {
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
            }
            // CALCULATE EFFECTIVE ANNUAL RATE
        }

        public void Percent_Change()
        {
            int r;
            int i;
            string strNum;
            double intNum;

            // FORMAT FILLED DB DATA
            try
            {
                for (i = 1; i <= dataGridView1.ColumnCount - 1; i++)
                {
                    {
                        if (dataGridView1.Rows[Mos_Const + 1].Cells[i].Value == null) return;
                        strNum = dataGridView1.Rows[Mos_Const + 1].Cells[i].Value.ToString();
                        if (Information.IsNumeric(strNum) == true) //KEEP ISNUMERIC AS VB METHOD
                        {
                            intNum = Convert.ToDouble(strNum);
                            dataGridView1.Rows[Mos_Const + 1].Cells[i].Value = String.Format("{0:p}", intNum);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        } 
        
        public override void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            dynamic_custom();
            dynamic_format();
        }

        public void dynamic_format()
        {
            int i;

            if (dataGridView1.CurrentCell == null || dataGridView1.CurrentCell.ColumnIndex != 0 || terminate > 0) return;

            var switchExpr = dataGridView1.Rows[Mos_Const].Cells[0].Value;
            switch (switchExpr)
            {
                case "Detail":
                    {
                        for(i = 0; i <= dataGridView1.ColumnCount-1; i++)
                        {
                            if (dataGridView1.CurrentCell.RowIndex == Mos_Const + 1) break;
                            if (i > 0)
                            {
                                dataGridView1.Rows[Mos_Const].Cells[i].Value = null;
                            }
                            dataGridView1.Rows[Mos_Const + 1].Cells[i].ReadOnly = false;
                            dataGridView1.Rows[Mos_Const + 1].Cells[i].Style.BackColor = Color.White;
                            dataGridView1.Rows[Mos_Const + 1].Cells[i].Style.ForeColor = Color.Black;
                            dataGridView1.Rows[Mos_Const + 1].Cells[i].Style.SelectionBackColor = SystemColors.Highlight;
                            dataGridView1.Rows[Mos_Const + 1].Cells[i].Style.SelectionForeColor = Color.White;
                        }
                        dataGridView1.Rows[Mos_Const + 1].Visible = true;
                        effective_Value();
                        break;
                    }
                case null:
                    {
                        if (dataGridView1.Rows[Mos_Const + 1].Cells[0].Value != null) dataGridView1.Rows[Mos_Const + 1].Cells[0].Value = null;
                        for (i = 0; i <= dataGridView1.ColumnCount - 1; i++)
                        {
                            if (i > 0)
                            {
                                double zero = 0;
                                dataGridView1.Rows[Mos_Const].Cells[i].Value = String.Format("{0:p}", zero);
                                dataGridView1.Rows[Mos_Const + 1].Cells[i].Value = null;
                            }
                            
                            dataGridView1.Rows[Mos_Const + 1].Cells[i].ReadOnly = true;
                            dataGridView1.Rows[Mos_Const + 1].Cells[i].Style.BackColor = SystemColors.Control;
                            dataGridView1.Rows[Mos_Const + 1].Cells[i].Style.ForeColor = SystemColors.ControlDark;
                            dataGridView1.Rows[Mos_Const + 1].Cells[i].Style.SelectionBackColor = SystemColors.Control;
                        }
                        effective_Value();
                        break;
                    }
                    
                default:
                    {
                        for (i = 0; i <= dataGridView1.ColumnCount - 1; i++)
                        {
                            dataGridView1.Rows[Mos_Const + 1].Cells[i].ReadOnly = true;
                            dataGridView1.Rows[Mos_Const + 1].Cells[i].Value = null;
                            dataGridView1.Rows[Mos_Const + 1].Cells[i].Style.BackColor = SystemColors.Control;
                            dataGridView1.Rows[Mos_Const + 1].Cells[i].Style.ForeColor = SystemColors.ControlDark;
                            dataGridView1.Rows[Mos_Const + 1].Cells[i].Style.SelectionBackColor = SystemColors.Control;
                        }
                        dataGridView1.Rows[Mos_Const + 1].Visible = false;
                        original_rates();
                        Rate_Growth();
                        effective_Value();
                        break;
                    }    
            }
            
        }

        public void effective_Value()
        {
            int i;
            int j;
            int z;
            List<double> compound = new List<double>();
            List<double> compounded = new List<double>();
            List<double> effective = new List<double>();

            if (Summations.Count > 0)
            {
                for (i = 0; i <= myMethods.Period - 1; i++)
                {
                    compound.Add(1 + Summations[i]);
                }
                for (i = 0; i <= myMethods.Period - 1; i++)
                {
                    if (i - 1 >= 0)
                    {
                        compounded.Add(compound[i] *= compounded[i - 1]);
                    }
                    else
                    {
                        compounded.Add(compound[i]);
                    }
                    
                }
            }
                

            try
            {
                for (i = 1; i <= myMethods.Period; i++)
                {
                    effective.Clear();
                    for (j = 0; j <= Mos_Const - 1; j++)
                    {
                        effective.Add(Convert.ToDouble(dataGridView1.Rows[j].Cells[i].Value));
                    }
                    for (z = 1; z <= dataGridView1.RowCount - 1; z++)
                    {
                        if (dataGridView1.Rows[Mos_Const].Cells[0].Value == null || dataGridView1.Rows[Mos_Const].Cells[0].Value.ToString() == "Detail" && dataGridView1.Rows[Mos_Const + 1].Cells[0].Value == null)
                        {
                            dataGridView1.Rows[Mos_Const + 2].Cells[i].Value = string.Format("{0:0.00}", effective.Average());
                        }
                        else if (Summations.Count > 0)
                        {
                            dataGridView1.Rows[Mos_Const + 2].Cells[i].Value = string.Format("{0:0.00}", (compounded[i - 1]) * effective.Average());
                        }
                        else
                        {
                            dataGridView1.Rows[Mos_Const + 2].Cells[i].Value = string.Format("{0:0.00}", effective.Average());
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        
        private void zero_to_DB()
        {
            try
            {
                int i;
                string colName;
                string cmdUpdate;
                double zero = 0;

                // MAKE CALL TO DATABASE
                SQL_DB.ExecQuery("SELECT * FROM " + tbl_Dynamic + ";"); // DB DOES NOT REGISTER FIRST COLUMN UNLESS SELECTED AGAIN
                for (i = 1; i <= myMethods.Period * Mos_Const; i++)
                {
                    //VARIALES & PARAMS
                    SQL_DB.AddParam("@PrimKey", dgv.Rows[dgv.CurrentCell.RowIndex].Cells[0].Value);
                    colName = "month" + i;
                    // UPDATE COMMAND
                    cmdUpdate = "UPDATE " + tbl_Dynamic + " SET " + colName + "=" + zero + " WHERE ID_Num=@PrimKey;";
                    SQL_DB.ExecQuery(cmdUpdate);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void dynamic_custom()
        {
            if (dataGridView1.CurrentCell.ColumnIndex > 0 || dataGridView1.CurrentCell.RowIndex == Mos_Const)
                return;
            if (dataGridView1.CurrentCell.RowIndex == Mos_Const + 1 && dataGridView1.CurrentCell.Value == null)
            {
                dataGridView1.Rows[Mos_Const].Cells[0].Value = null;
                dataGridView1.CurrentCell = dataGridView1.Rows[Mos_Const].Cells[0];
                dataGridView1.Rows[Mos_Const].Cells[0].Selected = true;
            }
        }

        public override void DataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewBand band = dataGridView1.Rows[Mos_Const + 1];

            if (dataGridView1.CurrentCell.RowIndex == Mos_Const + 1) return;

            if (band.Visible == true && dataGridView1.Rows[Mos_Const].Cells[0].Value == null)
            {
                band.Visible = false;
            }
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
            string cmdUpdate;
            string cols;
            int i;
            List<string> range = new List<string>();
            string title = "TINUUM SOFWARE";
            var val = dataGridView1.Rows[Mos_Const].Cells[0].Value;
            string newVal = Convert.ToString(val);
            int j;
            List<double> getRates = new List<double>();

            // COUNT TOTAL OF VALUES IN DYNAMIC ROW
            for (i = 0; i <= dataGridView1.ColumnCount - 1; i++)
            {
                if(dataGridView1.Rows[Mos_Const + 1].Cells[i].Value == null)
                {
                    break;
                }
                else
                {
                    range.Add(dataGridView1.Rows[Mos_Const + 1].Cells[i].Value.ToString());
                }
                
            }

            if (range.Count < dataGridView1.ColumnCount && newVal == "Detail")
            {
                MessageBox.Show("Complete empty fields in detailed rates before continuing.", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                try
                {
                    for (r = 0; r <= Mos_Const - 1; r++)
                    {
                        for (n = 1; n <= myMethods.Period; n++)
                        {
                            if (dataGridView1.Rows[r].Cells[n].Value == null)
                            {
                                MessageBox.Show("Entries must be valid before continuing.", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }

                    for (r = 0; r <= Mos_Const - 1; r++)
                    {
                        for (n = 1; n <= myMethods.Period; n++)
                        {
                            mos_Num = r + (n - 1) * Mos_Const + 1;
                            tbl_Col = "month" + mos_Num;
                            dec_Val = Convert.ToDouble(dataGridView1.Rows[r].Cells[n].Value);
                            SQL_DETAIL.AddParam("@PrimKey", num);
                            SQL_DETAIL.AddParam("@months_data", dec_Val);
                            cmdUpdate = "UPDATE " + tbl_Name + " SET " + tbl_Col + "=@months_data WHERE ID_Num=@PrimKey;";
                            SQL_DETAIL.ExecQuery(cmdUpdate);
                        }
                    }
                    //DYNAMIC MAJOR SUBMIT FOR YEARLY RATES DATA
                    if (range.Count < dataGridView1.ColumnCount)
                    {
                        SQL_DETAIL.AddParam("@PrimKey", num);
                        SQL_DETAIL.AddParam("@choose", 1);
                        if (dataGridView1.Rows[Mos_Const].Cells[i].Value == null)
                        {
                            SQL_DETAIL.AddParam("@select", DBNull.Value);
                        }
                        else
                        {
                            SQL_DETAIL.AddParam("@select", dataGridView1.Rows[Mos_Const].Cells[0].Value);
                        }    
                        cmdUpdate = "UPDATE " + tbl_MajorDyna + " SET Choose=@choose, Selection=@select WHERE ID_Num=@PrimKey;";
                        SQL_DETAIL.ExecQuery(cmdUpdate);
                    }
                    else
                    {
                        for (j = 1; j <= dataGridView1.ColumnCount - 1; ++j)
                        {
                            SQL_DETAIL.AddParam("@PrimKey", num);
                            cols = "year" + j;
                            SQL_DETAIL.AddParam("@choose", 0);
                            SQL_DETAIL.AddParam("@select", dataGridView1.Rows[Mos_Const + 1].Cells[0].Value);
                            SQL_DETAIL.AddParam("@decs", myMethods.ToDecimal(dataGridView1.Rows[Mos_Const + 1].Cells[j].Value.ToString()));
                            cmdUpdate = "UPDATE " + tbl_MajorDyna + " SET " + cols + "=@decs, Choose=@choose, Selection=@select WHERE ID_Num=@PrimKey;";
                            SQL_DETAIL.ExecQuery(cmdUpdate);
                        }
                    }
                }
                catch (Exception ex)
                {
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
            var inputRates = new List<double>();
            var Growth = new List<double>();
            var Difference = new List<double>();
            int i;
            int r;
            double Val;
            int Period = myMethods.Period;
            
            // REFRESH LIST
            Summations.Clear();

            if (dataGridView1.Rows[Mos_Const].Cells[0].Value == null) return;
            try
            {
                for (i = 0; i <= Period * Mos_Const -1 ; i++)
                {
                    if (dataGridView1.Rows[Mos_Const].Cells[0].Value.ToString() == "Detail")
                    {
                        inputRates.Add(cnst + Rates[i]);
                    }
                    else
                    {
                        inputRates.Add(cnst + rateRange[i]);
                    }
                }

                Val = 1;
                for (i = 1; i <= Period * Mos_Const; i++)
                {
                    if (i % Mos_Const == 1)
                    {
                        Val = 1;
                    }
                    Val *= cnst * inputRates[i - 1];
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

        private void original_rates()
        {
            string slct = dataGridView1.Rows[Mos_Const].Cells[0].Value.ToString();
            int i;
            int j;
            int count = 0;
            string colNames;
            string cmdUpdate;

            if (dataGridView1.Rows[Mos_Const].Cells[0].Value.ToString() == "") return;

            for (i = 0; i <= strRates.Count - 1; i++)
            {
                if(strRates[i] == slct)
                {
                    break;
                }
                count += 1;
            }

            // FILL LIST WITH COMPREHENSIVE RATES
            rateRange.Clear();
            SQL_Rates.ExecQuery("SELECT * FROM " + tbl_Rates + ";");
            for (j = 0; j <= myMethods.Period * Mos_Const - 1; j++)
            {
                rateRange.Add(Convert.ToDouble(SQL_Rates.DBDT.Rows[count - 1][j + 2]));
            }

            // UPDATE COMPREHENSIVE RATE DATABASE
            for (j = 0; j <= myMethods.Period * Mos_Const - 1; j++)
            {
                SQL_Rates.AddParam("@vals", rateRange[j]);
                colNames = "month" + (j + 1);
                SQL_Rates.AddParam("@PrimKey", dgv.Rows[dgv.CurrentCell.RowIndex].Cells[0].Value);
                cmdUpdate = "UPDATE " + tbl_Dynamic + " SET " + colNames +"=@vals WHERE ID_Num=@PrimKey;";
                SQL_Rates.ExecQuery(cmdUpdate);
            }

        }

        private void Rate_To_DB()
        {
            int Row_Num;
            int i;
            int r;
            double Val;
            int Ann = 1;
            int Semi = 2;
            int Quart = 4;
            string cmdUpdate;
            string colName;

            if (Information.IsNothing(dataGridView1.CurrentCell)) // KEEP VB METHOD
                return;
            Row_Num = dataGridView1.CurrentCell.RowIndex;
            Rates.Clear();
            // FIND RATES FROM DGV AND COLLECT IN LIST TO ADJUST MONTHLY
            try
            {
                for (i = 1; i <= myMethods.Period; i++)
                {
                    for (r = 1; r <= Mos_Const; r++)
                    {
                        var switchExpr = dataGridView1.Rows[Row_Num].Cells[0].Value;
                        switch (switchExpr)
                        {
                            case "Annually":
                                {
                                    if (r % (Mos_Const / (double)Ann) == 1)
                                    {
                                        Val = myMethods.ToDecimal(Convert.ToString(dataGridView1.Rows[Row_Num].Cells[i].Value)) / Ann;
                                    }
                                    else
                                    {
                                        Val = 0;
                                    }

                                    Rates.Add(Val);
                                    break;
                                }

                            case "Semi-Annually":
                                {
                                    if (r % (Mos_Const / (double)Semi) == 1)
                                    {
                                        Val = myMethods.ToDecimal(Convert.ToString(dataGridView1.Rows[Row_Num].Cells[i].Value)) / Semi;
                                    }
                                    else
                                    {
                                        Val = 0;
                                    }

                                    Rates.Add(Val);
                                    break;
                                }

                            case "Quarterly":
                                {
                                    if (r % (Mos_Const / (double)Quart) == 1)
                                    {
                                        Val = myMethods.ToDecimal(Convert.ToString(dataGridView1.Rows[Row_Num].Cells[i].Value)) / Quart;
                                    }
                                    else
                                    {
                                        Val = 0;
                                    }

                                    Rates.Add(Val);
                                    break;
                                }

                            case "Monthly":
                                {
                                    Val = myMethods.ToDecimal(Convert.ToString(dataGridView1.Rows[Row_Num].Cells[i].Value)) / Mos_Const;
                                    Rates.Add(Val);
                                    break;
                                }

                            default:
                                {
                                    return;
                                }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }

            try
            {
                // SEND RATES TO DATABASE
                SQL_DB.ExecQuery("SELECT * FROM " + tbl_Dynamic + ";"); // DB DOES NOT REGISTER FIRST COLUMN UNLESS SELECTED AGAIN
                for (i = 1; i <= myMethods.Period * Mos_Const; i++)
                {
                    //VARIALES & PARAMS
                    SQL_DB.AddParam("@PrimKey", dgv.Rows[dgv.CurrentCell.RowIndex].Cells[0].Value);
                    colName = "month" + i;
                    // UPDATE COMMAND
                    cmdUpdate = "UPDATE " + tbl_Dynamic + " SET " + colName + "=" + Rates[i - 1] + " WHERE ID_Num=@PrimKey;";
                    SQL_DB.ExecQuery(cmdUpdate);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public virtual void Write_Detail()
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

            effective_Value();
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

            effective_Value();
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

            effective_Value();
        }

        public void Form_Cancel()
        {
            int num = Convert.ToInt32(dgv.Rows[frmRow].Cells[0].Value);
            string title = "TINUUM SOFTWARE";
            string cmdUpdate;

            DialogResult prompt = MessageBox.Show("Are you sure? Any unsubmitted data will be lost.", title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (prompt == DialogResult.Yes)
            {
                // EXTENDED DB CLEAR
                dataGridView1.Rows[Mos_Const].Cells[0].Value = null;
                zero_to_DB();

                // SUBMIT TO MAJOR
                SQL_DB.AddParam("@PrimKey", num);
                SQL_DB.AddParam("@choose", 1);
                SQL_DB.AddParam("@select", DBNull.Value);
                cmdUpdate = "UPDATE " + tbl_MajorDyna + " SET Choose=@choose, Selection=@select WHERE ID_Num=@PrimKey;";
                SQL_DB.ExecQuery(cmdUpdate);
            }
            current_Cell();
        }

        public override void btnCancel_Click(object sender, EventArgs e)
        {
            Form_Cancel();
            this.Dispose();
        }

        public virtual void current_Cell()
        {
            dgv.CurrentCell = dgv.Rows[frmRow].Cells[3];
            dgv.Rows[frmRow].Cells[3].Value = null;
            frm.ActiveControl = null;
        }

        public virtual void FormDetail_Dynamic_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form_Cancel();
            this.Dispose();
        }
    }
}

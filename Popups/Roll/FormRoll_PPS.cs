using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using Microsoft.VisualBasic;
using Syncfusion.Windows.Forms.Tools;
using Tinuum_Software_BETA.Detail_Inherit.Roll;

namespace Tinuum_Software_BETA.Popups.Roll
{
    public partial class FormRoll_PPS : Tinuum_Software_BETA.Popups.FormMarketPDPM
    {
        public SQLControl SQL = new SQLControl(); // CREATE NEW INSTANCE OF SQLCONTROL CLASS
        public SQLControl SQL_Diem = new SQLControl();
        protected List<string> PPD_Rates = new List<string>();
        protected List<string> Headers_Submit = new List<string>();
        protected List<string> Header_Name = new List<string>();
        protected List<string> Header_Rename = new List<string>();
        protected int Col_Count;
        protected string tbl_Name;
        protected int Mos_Const = 12;
        protected string Rslt_Cncl;
        protected string tbl_Rural = "dtbHomePPSRates_Rural";
        protected string tbl_Urban = "dtbHomePPSRates_Urban";
        protected string tbl_Detail = "dtbRoll_PPSRate";
        protected string tbl_Rate;
        protected string tbl_Dyn = "dtbRollDynamic_PPSRate";
        protected string tbl_Dynamic;
        protected string tbl_ValDyn = "dtbRollDetailDynamic_PPSRate";
        protected string tbl_ValDynamic;
        protected string tbl_CollectPrefix = "dtbRollDetail_Assess";
        protected string tbl_CollectDelete;
        protected string tbl_DiemRural = "dtbHomeFedPerDiem_Rural";
        protected string tbl_DiemUrban = "dtbHomeFedPerDiem_Urban";


        protected static int primeKey;
        public static int _primeKey
        {
            get
            {
                return primeKey;
            }
        }
        
        protected static int keyNum;
        public static int _keyNum
        {
            get
            {
                return keyNum;
            }
        }
        protected int load;

        public FormRoll_PPS()
        {
            tbl_Prefix = "dtbRollPPS";
            tbl_Active = "dtbRollConfigurePPS";
            InitializeComponent();
        }

        public override void FormMarketPDPM_Load(object sender, EventArgs e)
        {

            load = 1;

            Add_Source();
            Load_Grid();
            
            load = 0;
        }

        public virtual void Add_Source()
        {
            string btnString = "(b)";
            string cmbString = "(c)";
            string dteString = "(d)";
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

            tbl_Name = tbl_Prefix + primeKey;
            tbl_Rate = tbl_Detail + primeKey;
            tbl_Dynamic = tbl_Dyn + primeKey;
            tbl_ValDynamic = tbl_ValDyn + primeKey;
            tbl_CollectDelete = tbl_CollectPrefix + primeKey;

            // UNBIND DATA SOURCE AT BEGINNING FOR UPDATE COMMAND
            dataGridView1.ColumnCount = 0;
            dataGridView1.RowCount = 0;
            dataGridView1.Refresh();

            // LINK DATA SOURCE TO GET COL NAMES
            SQL.ExecQuery("SELECT * FROM " + tbl_Name + ";");
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

        public virtual void Load_Grid()
        {
            myMethods.SQL_Grab();
            if (DesignMode) return;

            var cmbo1 = new DataGridViewComboBoxColumn();
            var btn1 = new DataGridViewButtonColumn();
            var btn2 = new DataGridViewButtonColumn();
            loading = 1;
            List<double> myList = new List<double>();
            int i;
            int r;

            // SIZING
            //this.Width = 1200;
            //dataGridView1.Width = 1160;
            //btnSubmit.Location = new Point(1050, 22);
            //btnCancel.Location = new Point(922, 22);

            // GET TABLE AND SELECT
            tbl_Variable = tbl_Prefix + primeKey;
            SQL_Variable.ExecQuery("SELECT * FROM " + tbl_Variable + ";");

            // REFRESH ROWS & COLUMNS
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            // COLUMN CONTROLS
            {
                // ADD SPECS FOR COMBOBOX1
                cmbo1.Items.Add("");
                cmbo1.Items.Add("Standard");
                cmbo1.Items.Add("Configure");
                cmbo1.Items.Add("Detail");
                cmbo1.FlatStyle = FlatStyle.Popup;
                cmbo1.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                cmbo1.DisplayStyleForCurrentCellOnly = false;

                // ADD SPECS FOR BUTTON1
                btn1.UseColumnTextForButtonValue = true;
                btn1.Text = "_";
                btn1.FlatStyle = FlatStyle.System;
                btn1.DefaultCellStyle.Alignment = (DataGridViewContentAlignment)ContentAlignment.BottomRight;
                btn1.DefaultCellStyle.Font = new Font("Arial", 6, FontStyle.Bold);

                // ADD SPECS FOR BUTTON2
                btn2.UseColumnTextForButtonValue = true;
                btn2.Text = "_";
                btn2.FlatStyle = FlatStyle.System;
                btn2.DefaultCellStyle.Alignment = (DataGridViewContentAlignment)ContentAlignment.BottomRight;
                btn2.DefaultCellStyle.Font = new Font("Arial", 6, FontStyle.Bold);
            }

            // CREATE GRIDVIEW COLUMNS
            for (i = 0; i <= Col_Count - 1; i++)
            {
                var switchExpr = Header_Name[i].Substring(Header_Name[i].Length - 3, 3);
                switch (switchExpr)
                {
                    case "(b)":
                        {
                            switch (i)
                            {
                                case 19:
                                    {
                                        dataGridView1.Columns.Add(btn1);
                                    }
                                    break;
                                case 21:
                                    {
                                        dataGridView1.Columns.Add(btn2);
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    case "(c)":
                        {
                            switch (i)
                            {
                                case 18:
                                    {
                                        dataGridView1.Columns.Add(cmbo1);
                                    }
                                    break;
                            }  

                        }
                        break;
                    default:
                        {
                            dataGridView1.Columns.Add("txt", "New Text");
                        }
                        break;
                }
            }

            // SET HEADERS AND NON SORT
            for (i = 0; i <= dataGridView1.ColumnCount - 1; i++)
            {
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView1.Columns[i].HeaderText = Header_Rename[i];
            }

            // SET DGV NUMBER OF ROWS BY REFRESHING TBL ROWCOUNT
            dataGridView1.RowCount = SQL_Variable.RecordCount;

            // FILL DATAGRID FROM DATA TABLE
            for (r = 0; r <= SQL_Variable.RecordCount - 1; r++)
            {
                for (i = 0; i <= Col_Count - 1; i++)
                {
                    dataGridView1.Rows[r].Cells[i].Value = SQL_Variable.DBDT.Rows[r][i];
                }
            }

            // MAKE ROWS IN COLUMN 1 READ ONLY AND NUMBER IN ORDER
            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = i + 1;
                dataGridView1.Rows[i].Cells[0].ReadOnly = true;
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

            // ROW HEADER DISABLE
            foreach (DataGridViewColumn Col in dataGridView1.Columns)
            {
                Col.SortMode = DataGridViewColumnSortMode.NotSortable;
                Col.ReadOnly = true;
            }
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            // FREEZE COLUMNS & VISIBILITY & OTHER SPECS
            dataGridView1.Columns[0].Frozen = true;
            dataGridView1.Columns[1].Frozen = true;
            dataGridView1.Columns[1].Width = 150;

            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                if (new int[] { 0, 5, 6, 10, 11 }.Contains(i))
                {
                    dataGridView1.Columns[i].Visible = false;
                }
            }

            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                if (new int[] { 2, 7, 12, 15 }.Contains(i))
                {
                    dataGridView1.Columns[i].Width = 300;
                    dataGridView1.Columns[i].DefaultCellStyle.SelectionBackColor = Color.White;
                    dataGridView1.Columns[i].DefaultCellStyle.ForeColor = Color.White;
                }
            }

            for (i = 0; i <= dataGridView1.ColumnCount - 1; i++)
            {
                if (new int[] { 1, 2, 7, 12, 15, }.Contains(i))
                {
                    if ( i > 1)
                    {
                        dataGridView1.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridView1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                    else
                    {
                        dataGridView1.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dataGridView1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    }
                }
                else
                {
                    dataGridView1.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }

            // MAKE ROW READ ONLY
            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                if (new int[] { 0, 17, 34, 47, 54 }.Contains(i))
                {
                    dataGridView1.Rows[i].ReadOnly = true;
                    dataGridView1.Rows[i].DefaultCellStyle.SelectionBackColor = SystemColors.Control;
                    dataGridView1.Rows[i].DefaultCellStyle.SelectionForeColor = SystemColors.ControlDark;
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = SystemColors.Control;
                    dataGridView1.Rows[i].DefaultCellStyle.ForeColor = SystemColors.ControlDark;
                }
            }

            // FILL LISTS FOR CHART
            SQL_Norm.ExecQuery("SELECT * FROM " + tbl_New + ";");

            for (i = 0; i <= SQL_Norm.RecordCount - 1; i++)
            {
                myList.Add(Convert.ToDouble(SQL_Norm.DBDT.Rows[i][0]));
            }

            for (i = 0; i <= SQL_Norm.RecordCount - 1; i++)
            {
                NormDist.Series["Distribution"].Points.AddXY(i + 1, myList[i]);
            }

            //FILL LIST FOR CUMULATIVE CELL VALUES
            SQL_Norm.ExecQuery("SELECT * FROM " + tbl_Cumulative + ";");
            cumulative.Add(0);
            for (i = 1; i <= SQL_Norm.RecordCount - 2; i++)
            {
                cumulative.Add(Convert.ToDouble(SQL_Norm.DBDT.Rows[i][0]));
            }
            cumulative.Add(1);

            // CHART CHARACTERISTICS
            {
                NormDist.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                NormDist.ChartAreas[0].AxisX.LabelStyle.Enabled = false;
                NormDist.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
                NormDist.ChartAreas[0].AxisY.MinorGrid.Enabled = false;
                NormDist.ChartAreas[0].AxisY.LabelStyle.Enabled = false;
                NormDist.ChartAreas[0].AxisY.MajorTickMark.Enabled = false;
                NormDist.ChartAreas[0].AxisY.LineColor = NormDist.BackColor;
                NormDist.ChartAreas[0].AxisX.LineColor = SystemColors.ControlDarkDark;
                NormDist.Series["Distribution"].BorderWidth = 2;
                NormDist.Series["Distribution"].BorderColor = SystemColors.Highlight;
                NormDist.Series["Distribution"].Color = SystemColors.Highlight;
                NormDist.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
                NormDist.BorderlineColor = SystemColors.ControlDark;
            }

            // ADD SUBMIT NAME
            if (actCtrl.Name != "btnAdd")
            {
                configName.Text = lstBox.Text;
            }
            else
            {
                // ADD DEFAULT VALUES
                number_load();
            }
            // FILL DGV FOR PPS RATES
            for (i = 0; i <= dataGridView1.RowCount - 2; i++) // MINUS 2 BECAUSE ADDED NON CASE MIX GROUP
            {
                if (dataGridView1.Rows[i].Cells[20].Value == DBNull.Value)
                {
                    if (myMethods.geo_area == "Urban")
                    {
                        SQL.ExecQuery("SELECT * FROM " + tbl_Urban + ";");
                    }
                    else
                    {
                        SQL.ExecQuery("SELECT * FROM " + tbl_Rural + ";");
                    }

                    if (new int[] { 0, 17, 34, 47, 54 }.Contains(i))
                    {
                        continue;
                    }
                    else
                    {
                        dataGridView1.Rows[i].Cells[20].Value = SQL.DBDT.Rows[i][2];
                    }    
                }
            }
            
            // LAST ROW RATE - NON CASEMIX
            if (dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[20].Value == DBNull.Value)
            {
                if (myMethods.geo_area == "Urban")
                {
                    SQL_Diem.ExecQuery("SELECT * FROM " + tbl_DiemUrban + ";");
                }
                else
                {
                    SQL_Diem.ExecQuery("SELECT * FROM " + tbl_DiemRural + ";");
                }
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[20].Value = SQL_Diem.DBDT.Rows[0][5];
            }

            // ADD RATES TO ARRAY
            if (myMethods.geo_area == "Urban")
            {
                SQL.ExecQuery("SELECT * FROM " + tbl_Urban + ";");
                SQL_Diem.ExecQuery("SELECT * FROM " + tbl_DiemUrban + ";");
            }
            else
            {
                SQL.ExecQuery("SELECT * FROM " + tbl_Rural + ";");
                SQL_Diem.ExecQuery("SELECT * FROM " + tbl_DiemRural + ";");
            }

            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                switch (PPD_Rates.Count)
                {
                    case 80:
                        {
                            PPD_Rates.Add(SQL_Diem.DBDT.Rows[0][5].ToString());
                        }
                        break;
                    default:
                        {
                            PPD_Rates.Add(SQL.DBDT.Rows[i][2].ToString());
                        }
                        break;
                }
            }

            // SET COMBO COLUMN TO ACCESSIBLE
            dataGridView1.Columns[18].ReadOnly = false;
            dataGridView1.Columns[20].ReadOnly = false;
            // CALL METHODS
            DynamicCTLRs();
            sldr_Fill(); // CORRECT
            Percent_Change();

            dataGridView1.FirstDisplayedScrollingRowIndex = 0;
            Move_CTRLs();

            // MAKE LAST ROW READ ONLY
            for (i = 2; i <= dataGridView1.ColumnCount - 3; i++)
            {
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[i].Value = null;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[i].ReadOnly = true;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[i].Style.SelectionBackColor = SystemColors.Control;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[i].Style.SelectionForeColor = SystemColors.ControlDark;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[i].Style.BackColor = SystemColors.Control;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[i].Style.ForeColor = SystemColors.ControlDark;
            }

            loading = 0;
        }
        public override void sldr_Fill()
        {
            string name = "sldr";
            int i;
            int j;
            int varCol;
            int cols = 4;
            int colNum;
            int rowNum;

            for (i = 0; i <= dataGridView1.RowCount * cols; i++)
            {
                varCol = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(i) / Convert.ToDouble(dataGridView1.RowCount)));
                
                switch (varCol)
                {
                    case 1:
                        {
                            colNum = 2;
                            rowNum = Convert.ToInt32(i - ((varCol - 1) * dataGridView1.RowCount));


                            if (new int[] { 0, 17, 34, 47, 54, 80, 81 }.Contains(rowNum)) continue;
                            RangeSlider sldrCtrl = (RangeSlider)dataGridView1.Controls[name + i];

                            // MIN VALS
                            if (dataGridView1.Rows[rowNum].Cells[colNum + 1].Value != DBNull.Value)
                            {
                                sldrCtrl.SliderMax = Convert.ToInt32(dataGridView1.Rows[rowNum].Cells[colNum + 2].Value);
                            }
                            // MAX VALS
                            if (dataGridView1.Rows[rowNum].Cells[colNum + 1].Value != DBNull.Value)
                            {
                                sldrCtrl.SliderMin = Convert.ToInt32(dataGridView1.Rows[rowNum].Cells[colNum + 1].Value);
                            }
                        }
                        break;
                    case 2:
                        {
                            colNum = 7;
                            rowNum = Convert.ToInt32(i - ((varCol - 1) * dataGridView1.RowCount));


                            if (new int[] { 0, 17, 34, 47, 54, 80, 81 }.Contains(rowNum)) continue;
                            RangeSlider sldrCtrl = (RangeSlider)dataGridView1.Controls[name + i];

                            // MIN VALS
                            if (dataGridView1.Rows[rowNum].Cells[colNum + 1].Value != DBNull.Value)
                            {
                                sldrCtrl.SliderMax = Convert.ToInt32(dataGridView1.Rows[rowNum].Cells[colNum - 1].Value);
                            }
                            // MAX VALS
                            if (dataGridView1.Rows[rowNum].Cells[colNum + 1].Value != DBNull.Value)
                            {
                                sldrCtrl.SliderMin = Convert.ToInt32(dataGridView1.Rows[rowNum].Cells[colNum - 2].Value);
                            }
                        }
                        break;
                    case 3:
                        {
                            colNum = 12;
                            rowNum = Convert.ToInt32(i - ((varCol - 1) * dataGridView1.RowCount));


                            if (new int[] { 0, 17, 34, 47, 54, 80, 81 }.Contains(rowNum)) continue;
                            RangeSlider sldrCtrl = (RangeSlider)dataGridView1.Controls[name + i];

                            // MIN VALS
                            if (dataGridView1.Rows[rowNum].Cells[colNum + 1].Value != DBNull.Value)
                            {
                                sldrCtrl.SliderMax = Convert.ToInt32(dataGridView1.Rows[rowNum].Cells[colNum - 1].Value);
                            }
                            // MAX VALS
                            if (dataGridView1.Rows[rowNum].Cells[colNum + 1].Value != DBNull.Value)
                            {
                                sldrCtrl.SliderMin = Convert.ToInt32(dataGridView1.Rows[rowNum].Cells[colNum - 2].Value);
                            }
                        }
                        break;
                    case 4:
                        {
                            colNum = 15;
                            rowNum = Convert.ToInt32(i - ((varCol - 1) * dataGridView1.RowCount));


                            if (new int[] { 0, 17, 34, 47, 54, 80, 81 }.Contains(rowNum)) continue;
                            RangeSlider sldrCtrl = (RangeSlider)dataGridView1.Controls[name + i];

                            // MIN VALS
                            if (dataGridView1.Rows[rowNum].Cells[colNum + 1].Value != DBNull.Value)
                            {
                                sldrCtrl.SliderMax = Convert.ToInt32(dataGridView1.Rows[rowNum].Cells[colNum + 2].Value);
                            }
                            // MAX VALS
                            if (dataGridView1.Rows[rowNum].Cells[colNum + 1].Value != DBNull.Value)
                            {
                                sldrCtrl.SliderMin = Convert.ToInt32(dataGridView1.Rows[rowNum].Cells[colNum + 1].Value);
                            }
                        }
                        break;        
                }     
            }
        }
        public void number_load()
        {
            int i;
            int j;

            int Mortality = 30;
            int Readmission = 40;

            for (j = 0; j <= dataGridView1.ColumnCount -1; j++)
            {
                switch (j)
                {
                    case 3:
                        {
                            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                            {
                                if (new int[] { 0, 17, 34, 47, 54, 80, 81 }.Contains(i))
                                {
                                    continue;
                                }
                                else
                                {
                                    dataGridView1.Rows[i].Cells[j].Value = 15;
                                }
                            }
                        }
                        break;
                    case 4:
                        {
                            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                            {
                                if (new int[] { 0, 17, 34, 47, 54, 80, 81 }.Contains(i))
                                {
                                    continue;
                                }
                                else
                                {
                                    dataGridView1.Rows[i].Cells[j].Value = 60;
                                }
                            }
                        }
                        break;
                    case 5:
                    case 6:
                        {
                            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                            {
                                if (new int[] { 0, 17, 34, 47, 54, 80, 81 }.Contains(i))
                                {
                                    continue;
                                }
                                else
                                {
                                    dataGridView1.Rows[i].Cells[j].Value = Mortality;
                                }
                            }
                        }
                        break;
                    case 8:
                    case 9:
                        {
                            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                            {
                                if (new int[] { 0, 17, 34, 47, 54, 80, 81 }.Contains(i))
                                {
                                    continue;
                                }
                                else
                                {
                                    dataGridView1.Rows[i].Cells[j].Value = cumulative[Mortality];
                                }
                            }
                        }
                        break;
                    case 10:
                    case 11:
                        {
                            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                            {
                                if (new int[] { 0, 17, 34, 47, 54, 80, 81 }.Contains(i))
                                {
                                    continue;
                                }
                                else
                                {
                                    dataGridView1.Rows[i].Cells[j].Value = Readmission;
                                }
                            }
                        }
                        break;
                    case 13:
                    case 14:
                        {
                            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                            {
                                if (new int[] { 0, 17, 34, 47, 54, 80, 81 }.Contains(i))
                                {
                                    continue;
                                }
                                else
                                {
                                    dataGridView1.Rows[i].Cells[j].Value = cumulative[Readmission];
                                }
                            }
                        }
                        break;
                    case 16:
                        {
                            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                            {
                                if (new int[] { 0, 17, 34, 47, 54, 80, 81 }.Contains(i))
                                {
                                    continue;
                                }
                                else
                                {
                                    dataGridView1.Rows[i].Cells[j].Value = 90;
                                }
                            }
                        }
                        break;
                    case 17:
                        {
                            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                            {
                                if (new int[] { 0, 17, 34, 47, 54, 80, 81 }.Contains(i))
                                {
                                    continue;
                                }
                                else
                                {
                                    dataGridView1.Rows[i].Cells[j].Value = 180;
                                }
                            }
                        }
                        break;
                    case 18:
                        {
                            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                            {
                                if (new int[] { 0, 17, 34, 47, 54, 80, 81 }.Contains(i))
                                {
                                    continue;
                                }
                                else
                                {
                                    dataGridView1.Rows[i].Cells[j].Value = "Standard";
                                }
                            }
                        }
                        break;
                }
                
            }
            
        }

        public override void Percent_Change()
        {
            int j;
            int i;
            string strNum;
            double intNum;

            // FORMAT FILLED DB DATA
            try
            {
                for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                {
                    if (new int[] { 0, 17, 34, 47, 54 }.Contains(i)) continue;
                    for (j = 0; j <= dataGridView1.ColumnCount - 1; j++)
                    {
                        
                        switch (j)
                        {
                            case 8:
                            case 9:
                            case 13:
                            case 14:
                                {
                                    if (dataGridView1.Rows[i].Cells[j].Value == DBNull.Value) return;
                                    strNum = dataGridView1.Rows[i].Cells[j].Value.ToString();
                                    if (Information.IsNumeric(strNum) == true)
                                    {
                                        intNum = Convert.ToDouble(strNum);
                                        dataGridView1.Rows[i].Cells[j].Value = String.Format("{0:p}", intNum);
                                    }
                                }
                                break;
                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public override void Move_CTRLs()
        {
            int i;
            int c;
            int x;
            int y;
            int z;
            int width;
            int height;
            Rectangle rect;
            int Diff;
            int rowVal;
            int rowNum = default;
            int colNum = default;
            int varCol;
            string name = "sldr";

            //FIND & MOVE ALL DYNAMIC CONTROLS
            foreach (Control ctrl in dataGridView1.Controls)
            {
                if (ctrl is RangeSlider)
                {
                    try
                    {
                        Diff = ctrl.Name.Length - name.Trim().Length;
                        rowVal = Convert.ToInt32(ctrl.Name.Substring(ctrl.Name.Length - Diff, Diff));
                        varCol = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(rowVal) / Convert.ToDouble(dataGridView1.RowCount)));

                        switch (varCol)
                        {
                            case 1:
                                {
                                    colNum = 2;
                                }
                                break;
                            case 2:
                                {
                                    colNum = 7;
                                }
                                break;
                            case 3:
                                {
                                    colNum = 12;
                                }
                                break;
                            case 4:
                                {
                                    colNum = 15;
                                }
                                break;
                        }

                        rowNum = Convert.ToInt32(rowVal - ((varCol - 1) * dataGridView1.RowCount));
                    }
                    catch (Exception ex)
                    {
                    }

                    rect = dataGridView1.GetCellDisplayRectangle(colNum, rowNum, false);
                    x = rect.X;
                    y = rect.Y;
                    width = rect.Width;
                    height = rect.Height;

                    ctrl.SetBounds(x, y, width, height);

                    if (rowNum % dataGridView1.RowCount == 1) ctrl.Visible = true;
                }
            }
        }

        public override void DynamicCTLRs()
        {
            int Counter = -1; //STARTING FROM ZERO
            int x;
            int y;
            int i;
            int j;
            int Width;
            int Height;

            Rectangle rect; // STORES A SET OF FOUR INTEGERS

            for (j = 0; j <= dataGridView1.ColumnCount - 1; j++)
            {
                switch (j)
                {
                    case 2:
                    case 7:
                    case 12:
                    case 15:
                        {
                            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                            {
                                Counter += 1;
                                int switchExpr = i;
                                switch (switchExpr)
                                {
                                    case 0:
                                    case 17:
                                    case 34:
                                    case 47:
                                    case 54:
                                    case 80:
                                        {
                                            break;
                                        }

                                    default:
                                        {
                                            var sldrCtrl = new RangeSlider();
                                            sldrCtrl.Name = "sldr" + Counter;
                                            sldrCtrl.VisualStyle = RangeSlider.RangeSliderStyle.Metro;
                                            sldrCtrl.RangeColor = SystemColors.Highlight;
                                            sldrCtrl.HighlightedThumbColor = SystemColors.Highlight;
                                            sldrCtrl.ThumbColor = SystemColors.Highlight;
                                            sldrCtrl.PushedThumbColor = SystemColors.Highlight;
                                            sldrCtrl.ChannelColor = SystemColors.ControlDark;
                                            sldrCtrl.BackColor = Color.White;
                                            switch (j)
                                            {
                                                case 2:
                                                    {
                                                        sldrCtrl.Minimum = 0;
                                                        sldrCtrl.Maximum = 365;
                                                    }
                                                    break;
                                                case 7:
                                                    {
                                                        sldrCtrl.Minimum = 0;
                                                        sldrCtrl.Maximum = 99;
                                                    }
                                                    break;
                                                case 12:
                                                    {
                                                        sldrCtrl.Minimum = 0;
                                                        sldrCtrl.Maximum = 99;
                                                    }
                                                        break;
                                                case 15:
                                                    {
                                                        sldrCtrl.Minimum = 0;
                                                        sldrCtrl.Maximum = 2000;
                                                    }
                                                    break;
                                            }
                                            sldrCtrl.SliderMin = 0;
                                            sldrCtrl.SliderMax = 0;
                                            sldrCtrl.Enabled = false;

                                            dataGridView1.Controls.Add(sldrCtrl);

                                            // SET POSITION
                                            rect = dataGridView1.GetCellDisplayRectangle(j, i, false);
                                            x = rect.X;
                                            y = rect.Y;
                                            Width = rect.Width;
                                            Height = rect.Height;

                                            sldrCtrl.SetBounds(x, y, Width, Height);
                                            sldrCtrl.Visible = true;

                                            // ADD EVENT HANDLER
                                            sldrCtrl.Click += new EventHandler(sldrCrtl_Click);
                                            sldrCtrl.ValueChanged += new EventHandler(sldrCrtl_ValueChanged);
                                            sldrCtrl.MouseDoubleClick += new MouseEventHandler(sldrCtrl_DoubleClick);
                                            sldrCtrl.LostFocus += new EventHandler(sldrCtrl_Leave);
                                        }
                                        break;
                                }

                            }
                        }
                        break;
                }
            }
            
        }

        public override void process_Submit()
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

            // ENSURE NO BLANKS OR CONFIGURE

            if (dataGridView1.RowCount == 0)
            {
                // Nothing
            }
            else
            {
                for (y = 0; y <= dataGridView1.ColumnCount - 1; y++)
                {
                    if (new int[] { 2, 7, 12, 15 }.Contains(y))
                    {
                        continue;
                    }
                    for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                    {
                        if (new int[] { 0, 17, 34, 47, 54 }.Contains(i))
                        {
                            continue;
                        }
                        else
                        {
                            if (i == dataGridView1.RowCount - 1 && dataGridView1.Rows[dataGridView1.ColumnCount - 1].Cells[20].Value != null) continue;
                            if (Header_Name[y].Substring(Header_Name[y].Length - cRight, cRight).Equals(btnString))
                            {
                                // Do Nothing
                            }
                            else if (dataGridView1.Rows[i].Cells[y].Value == null || dataGridView1.Rows[i].Cells[y].Value == DBNull.Value || dataGridView1.Rows[i].Cells[y].Value.ToString() == "Configure")
                            {
                                MessageBox.Show("You must enter relevant values. Retry.", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                dataGridView1.CurrentCell = dataGridView1.Rows[i].Cells[y];
                                return;
                            }
                        }

                    }
                }
            }

            // FILL MAJOR TABLE WITH GRID
            for (y = 0; y <= dataGridView1.RowCount - 1; y++)
            {
                if (new int[] { 0, 17, 34, 47, 54 }.Contains(y)) continue;
                    for (i = 1; i <= dataGridView1.ColumnCount - 1; i++)
                {
                    SQL.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                    if (Header_Name[i].Substring(Header_Name[i].Length - cRight, cRight).Equals(btnString))
                    {
                        SQL.AddParam("@vals", null);
                    }
                    else
                    {
                        switch (i)
                        {
                            case 8:
                            case 9:
                            case 13:
                            case 14:
                                {
                                    if (dataGridView1.Rows[y].Cells[i].Value == DBNull.Value || dataGridView1.Rows[y].Cells[i].Value == null)
                                    {
                                        SQL.AddParam("@vals", null);
                                    }
                                    else
                                    {
                                        SQL.AddParam("@vals", myMethods.ToDecimal(dataGridView1.Rows[y].Cells[i].Value.ToString()));
                                    }                                        
                                }
                                break;
                            default:
                                {
                                    SQL.AddParam("@vals", dataGridView1.Rows[y].Cells[i].Value);
                                }
                                break;
                        }
                    }

                    cmdUpdate = "UPDATE " + tbl_Variable + " SET " + Headers_Submit[i] + "=@vals WHERE ID_Num=@PrimKey;";
                    SQL.ExecQuery(cmdUpdate);
                }
            }

            // FILL DETAIL TABLES FROM GRID
            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                if (dataGridView1.Rows[i].Cells[20].Value.ToString() == "Detail")
                {
                    continue;
                }
                else
                {
                    for (j = 1; j <= myMethods.Period * Mos_Const; j++)
                    {
                        string tbl_Col = "month" + j;
                        var dec_Val = dataGridView1.Rows[i].Cells[20].Value;
                        SQL.AddParam("@PrimKey", dataGridView1.Rows[i].Cells[0].Value.ToString());
                        SQL.AddParam("@months_data", dec_Val);
                        cmdUpdate = "UPDATE " + tbl_Rate + " SET " + tbl_Col + "=@months_data WHERE ID_Num=@PrimKey;";
                        SQL.ExecQuery(cmdUpdate);
                    }
                }

            }
            

            // SET DYNAMIC YEARLY TO NULL
            for (y = 0; y <= dataGridView1.RowCount - 1; y++)
            {
                if (Information.IsNumeric(dataGridView1.Rows[y].Cells[dataGridView1.ColumnCount - 2].Value))
                {
                    SQL.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                    SQL.AddParam("@val1", 1);
                    SQL.AddParam("@val2", DBNull.Value);
                    string colName1 = "Choose";
                    string colName2 = "Selection";
                    cmdUpdate = "UPDATE " + tbl_Dynamic + " SET " + colName1 + "=@val1, " + colName2 + "=@val2 WHERE ID_Num=@PrimKey;";
                    SQL.ExecQuery(cmdUpdate);
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

        public override void call_cancel()
        {
            int i;
            int y;
            int j;
            int r;
            string Title = "TINUUM SOFTWARE";
            int cRight = 3;
            string btnString = "(b)";
            string cmdUpdate;

            load = 1;

            DialogResult prompt = MessageBox.Show("Are you sure? Any unsubmitted data will be lost.", Title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            Rslt_Cncl = prompt.ToString();

            if (prompt == DialogResult.Yes)
            {
                if (actCtrl.Name == "btnAdd")
                {
                    // DROP TABLES
                    SQL_Variable.ExecQuery("DROP TABLE " + tbl_Rate + ";");
                    SQL_Variable.ExecQuery("DROP TABLE " + tbl_Dynamic + ";");
                    SQL_Variable.ExecQuery("DROP TABLE " + tbl_ValDynamic + ";");
                    SQL_Variable.ExecQuery("DROP TABLE " + tbl_CollectDelete + ";");
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
                    // FILL DATAGRID FROM DATA TABLE
                    for (r = 0; r <= SQL_Variable.RecordCount - 1; r++)
                    {
                        for (i = 0; i <= Col_Count - 1; i++)
                        {
                            dataGridView1.Rows[r].Cells[i].Value = SQL_Variable.DBDT.Rows[r][i];
                        }
                    }

                    // FILL TABLES FROM GRID
                    for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                    {
                        if (dataGridView1.Rows[i].Cells[20].Value.ToString() == "Detail")
                        {
                            continue;
                        }
                        else
                        {
                            for (j = 1; j <= myMethods.Period * Mos_Const; j++)
                            {
                                string tbl_Col = "month" + j;
                                var dec_Val = dataGridView1.Rows[i].Cells[20].Value;
                                SQL.AddParam("@PrimKey", dataGridView1.Rows[i].Cells[0].Value.ToString());
                                SQL.AddParam("@months_data", dec_Val);
                                cmdUpdate = "UPDATE " + tbl_Rate + " SET " + tbl_Col + "=@months_data WHERE ID_Num=@PrimKey;";
                                SQL.ExecQuery(cmdUpdate);
                            }
                        }

                        // SET DYNAMIC YEARLY TO NULL
                        SQL.AddParam("@PrimKey", dataGridView1.Rows[i].Cells[0].Value);
                        SQL.AddParam("@val1", 1);
                        SQL.AddParam("@val2", DBNull.Value);
                        string colName1 = "Choose";
                        string colName2 = "Selection";
                        cmdUpdate = "UPDATE " + tbl_Dynamic + " SET " + colName1 + "=@val1, " + colName2 + "=@val2 WHERE ID_Num=@PrimKey;";
                        SQL.ExecQuery(cmdUpdate);
                    }
                    this.Close();
                    load = 0;
                    frm.Enabled = true;
                }
                else
                {
                    this.Close();
                    frm.Enabled = true;
                    load = 0;
                    return;
                }

            }
            else
            {
                return;
            }
            load = 0;
        }

        public override void sldrCrtl_Click(object sender, EventArgs e)
        {
            RangeSlider sldrCtrl = (RangeSlider)sender;
            int Diff;
            var rowNum = default(int);
            string name = "sldr";
            int x;
            int y;
            int i;
            int Width;
            Rectangle rect;
            int rowVal;
            int colNum = default;
            int varCol;

            sldrCtrl.Enabled = true;

            try
            {
                Diff = sldrCtrl.Name.Length - name.Trim().Length;
                rowVal = Convert.ToInt32(sldrCtrl.Name.Substring(sldrCtrl.Name.Length - Diff, Diff));
                varCol = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(rowVal) / Convert.ToDouble(dataGridView1.RowCount)));

                switch (varCol)
                {
                    case 1:
                        {
                            colNum = 2;
                        }
                        break;
                    case 2:
                        {
                            colNum = 7;
                        }
                        break;
                    case 3:
                        {
                            colNum = 12;
                        }
                        break;
                    case 4:
                        {
                            colNum = 15;
                        }
                        break;
                }

                rowNum = Convert.ToInt32(rowVal - ((varCol - 1) * dataGridView1.RowCount));
            }
            catch (Exception ex)
            {
            }

            dataGridView1.CurrentCell = dataGridView1.Rows[rowNum].Cells[colNum];

            // SHOW CHART AT POSITION
            switch (colNum)
            {
                case 7:
                case 12:
                    {
                        rect = dataGridView1.GetCellDisplayRectangle(colNum, rowNum, false);
                        x = rect.X;
                        y = rect.Y;
                        Width = rect.Width;

                        NormDist.SetBounds(x + 11, y + 10, Width + 1, 110);
                        NormDist.Visible = true;

                        // HANDLE CHART MARKER OBJECT
                        NormDist.Series[0].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.None;
                        for (i = 0; i <= cumulative.Count - 1; i++)
                        {
                            if (i == sldrCtrl.SliderMin || i == sldrCtrl.SliderMax)
                            {
                                NormDist.Series[0].Points[i].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
                                NormDist.Series[0].Points[i].MarkerSize = 7;
                                NormDist.Series[0].Points[i].MarkerColor = Color.White;
                            }
                            else
                            {
                                NormDist.Series[0].Points[i].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.None;
                            }
                        }
                    }
                    break;
            }
            
        }

        public override void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            int i;
            int j;
            string name = "sldr";
            int sldrCols = 4;
            int colNum = default;

            if (e.Button == MouseButtons.Left)
            {
                for (j = 1; j <= sldrCols; j++)
                {
                    for (i = 0; i <= dataGridView1.RowCount - 1 ; i++)
                    {
                        if (new int[] { 0, 17, 34, 47, 54, 80, 81 }.Contains(i)) continue;
                        RangeSlider sldrCtrl = (RangeSlider)dataGridView1.Controls[name + (i + ((j - 1) * dataGridView1.RowCount))];

                        switch (j)
                        {
                            case 1:
                                {
                                    colNum = 2;
                                }
                                break;
                            case 2:
                                {
                                    colNum = 7;
                                }
                                break;
                            case 3:
                                {
                                    colNum = 12;
                                }
                                break;
                            case 4:
                                {
                                    colNum = 15;
                                }
                                break;
                        }
                        if (dataGridView1.GetCellDisplayRectangle(colNum, i, false).Contains(e.Location))
                        {
                            sldrCtrl.Enabled = true;
                        }
                    }
                }
                
            }
        }

        public override void sldrCrtl_ValueChanged(object sender, EventArgs e)
        {
            RangeSlider sldrCtrl = (RangeSlider)sender;
            int Diff;
            int rowNum = 0;
            double low;
            double high;
            string name = "sldr";
            int i;
            int x;
            int y;
            int Width;
            Rectangle rect;
            int rowVal;
            int colNum = default;
            int varCol = default;

            if (loading > 0) return;

            try
            {
                Diff = sldrCtrl.Name.Length - name.Trim().Length;
                rowVal = Convert.ToInt32(sldrCtrl.Name.Substring(sldrCtrl.Name.Length - Diff, Diff));
                varCol = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(rowVal) / Convert.ToDouble(dataGridView1.RowCount)));

                switch (varCol)
                {
                    case 1:
                        {
                            colNum = 2;
                        }
                        break;
                    case 2:
                        {
                            colNum = 7;
                        }
                        break;
                    case 3:
                        {
                            colNum = 12;
                        }
                        break;
                    case 4:
                        {
                            colNum = 15;
                        }
                        break;
                }

                rowNum = Convert.ToInt32(rowVal - ((varCol - 1) * dataGridView1.RowCount));
            }
            catch (Exception ex)
            {
            }

            if (dataGridView1.CurrentCell.RowIndex != rowNum)
            {
                return;
            }


            // DYNAMIC CHANGE CELL VALUE
            switch (varCol)
            {
                case 1:
                case 4:
                    {
                        // SET MIN & MAX TO CELL
                        dataGridView1.Rows[rowNum].Cells[colNum + 1].Value = string.Format("{0:N0}", sldrCtrl.SliderMin);
                        dataGridView1.Rows[rowNum].Cells[colNum + 2].Value = string.Format("{0:N0}", sldrCtrl.SliderMax);

                        //AVERAGE
                        dataGridView1.Rows[rowNum].Cells[colNum].Value = (sldrCtrl.SliderMin + sldrCtrl.SliderMax) / 2;
                    }
                    break;
                case 2:
                case 3:
                    {
                        // SHOW CHART AT POSITION
                        rect = dataGridView1.GetCellDisplayRectangle(colNum, rowNum, false);
                        x = rect.X;
                        y = rect.Y;
                        Width = rect.Width;

                        NormDist.SetBounds(x + 11, y + 10, Width + 1, 110);
                        NormDist.Visible = true;

                        // SET MIN & MAX TO CELL
                        dataGridView1.Rows[rowNum].Cells[colNum - 2].Value = sldrCtrl.SliderMin;
                        dataGridView1.Rows[rowNum].Cells[colNum - 1].Value = sldrCtrl.SliderMax;

                        //HIGH & LOW VALUES
                        low = cumulative[sldrCtrl.SliderMin];
                        high = cumulative[sldrCtrl.SliderMax];


                        dataGridView1.Rows[rowNum].Cells[colNum + 1].Value = string.Format("{0:p}", low);
                        dataGridView1.Rows[rowNum].Cells[colNum + 2].Value = string.Format("{0:p}", high);

                        //AVERAGE
                        dataGridView1.Rows[rowNum].Cells[colNum].Value = (low + high) / 2;

                        // HANDLE CHART OBJECT
                        NormDist.Series[0].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.None;
                        for (i = 0; i <= cumulative.Count - 1; i++)
                        {
                            if (i == sldrCtrl.SliderMin || i == sldrCtrl.SliderMax)
                            {
                                NormDist.Series[0].Points[i].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
                                NormDist.Series[0].Points[i].MarkerSize = 7;
                                NormDist.Series[0].Points[i].MarkerColor = Color.White;
                            }
                            else
                            {
                                NormDist.Series[0].Points[i].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.None;
                            }

                        }
                    }
                    break;
            }    
        }

        public override void Delegate()
        {
            SQLQueries.tblRollPPSCreate();
        }

        public override void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (load > 0) return;
            keyNum = e.RowIndex;

            DataGridView senderGrid = (DataGridView)sender;
            try
            {
                switch (e.ColumnIndex)
                {
                    case 18:
                        {
                            switch (Convert.ToString(dataGridView1.Rows[keyNum].Cells[18].Value))
                            {
                                case "Configure":
                                    {
                                        if (new int[] { 0, 17, 34, 47, 54, 80, 81 }.Contains(e.RowIndex))
                                        {
                                            return;
                                        }
                                        else
                                        {
                                            FormConfigure_MDS frmDetail = new FormConfigure_MDS();
                                            frmDetail.Show(this);
                                            this.Enabled = false;
                                        }
                                    }
                                    break;
                                case "Detail":
                                    {
                                        if (new int[] { 0, 17, 34, 47, 54, 80, 81 }.Contains(e.RowIndex))
                                        {
                                            return;
                                        }
                                        else
                                        {
                                            dtlRoll_PPS_Collection frmDetail = new dtlRoll_PPS_Collection();
                                            frmDetail.Show(this);
                                            this.Enabled = false;
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                    case 20:
                        {
                            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null || dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "")
                            {
                                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = PPD_Rates[e.RowIndex];
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

        public override void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 20:
                    {
                        if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                        {
                            return;
                        }
                        else if (!Information.IsNumeric(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()))
                        {
                            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;
                            return;
                        }
                    }
                    break;
            }
        }

        public override void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
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
                        case 19:
                            {
                                if (e.RowIndex == dataGridView1.RowCount - 1) return;
                                if (dataGridView1.CurrentCell == null)
                                {
                                    return;
                                }

                                {
                                    dataGridView1.CurrentCell = dataGridView1.Rows[Slct].Cells[e.ColumnIndex - 1];
                                    dataGridView1.Rows[Slct].Cells[col - 1].Value = "";
                                    dataGridView1.Rows[Slct].Cells[col - 1].Value = "Detail";
                                }
                            }
                            break;
                        case 21:
                            {
                                if (new int[] { 0, 17, 34, 47, 54 }.Contains(e.RowIndex))
                                {
                                    return;
                                }
                                else
                                {
                                    dataGridView1.CurrentCell = dataGridView1.Rows[Slct].Cells[e.ColumnIndex - 1];
                                    dtlRoll_Dynamic frmDetail = new dtlRoll_Dynamic();
                                    frmDetail.Show(this);
                                    this.Enabled = false;
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

        public override void FormMarketPDPM_FormClosing(object sender, FormClosingEventArgs e)
        {
            var switchExpr = Rslt_Cncl;
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

            Rslt_Cncl = null;
        }
        public override void FormMarketPDPM_SizeChanged(object sender, EventArgs e)
        {
            Move_CTRLs();
        }
    }
}

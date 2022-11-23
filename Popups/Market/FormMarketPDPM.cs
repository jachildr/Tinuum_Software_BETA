using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Syncfusion.Windows.Forms.Tools;

namespace Tinuum_Software_BETA.Popups
{
    [CLSCompliant(true)]
    public partial class FormMarketPDPM : Form
    {
        protected string tbl_New = "[dtbMarket.Norm.Dist]";
        protected string tbl_Cumulative = "[dtbMarket.Cumulative]";
        protected string tbl_Prefix = "dtbMarketPDPM";
        protected string tbl_Active = "dtbMarketConfigurePDPM";
        protected string tbl_Variable;
        protected string slctCol = "collection_groups";
        protected string keyCol = "Prime";
        protected string Cncl = null;
        protected SQLControl SQL_Norm = new SQLControl();
        protected SQLControl SQL_Variable = new SQLControl();
        protected SQLControl SQL_Active = new SQLControl();
        protected List<double> cumulative = new List<double>();
        protected Control actCtrl;
        protected ListBox lstBox; // CHANGE FORM NUM
        protected Form frm;
        protected DataRowView drv;
        protected int loading;
        protected int primeKey;
        protected int lstIndex;
        protected int terminate = 0;
        
        public FormMarketPDPM()
        {
            InitializeComponent();
            
        }

        public virtual void FormMarketPDPM_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            actCtrl = Application.OpenForms[2].ActiveControl; // CHANGE FORM NUM
            lstBox = Application.OpenForms[2].Controls["listBox1"] as ListBox;
            frm = Application.OpenForms[2];

            loading = 1;
            List<double> myList = new List<double>();
            int i;
            int count;
            

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
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[4].Width = 300;
            dataGridView1.Columns[4].DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView1.Columns[4].DefaultCellStyle.ForeColor = Color.White;
            dataGridView1.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            foreach (DataGridViewColumn Col in dataGridView1.Columns)
            {
                Col.SortMode = DataGridViewColumnSortMode.NotSortable;
                Col.ReadOnly = true;
            }

            // MAKE LAST ROW READ ONLY
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
            
            // CALL METHODS
            DynamicCTLRs();
            sldr_Fill();
            Percent_Change();

            dataGridView1.FirstDisplayedScrollingRowIndex = 0;
            Move_CTRLs();

            loading = 0;
        }

        public virtual void Percent_Change()
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
                    for (j = 5; j <= dataGridView1.ColumnCount - 1; j++)
                    {
                        if (dataGridView1.Rows[i].Cells[j].Value == DBNull.Value) return;
                        strNum = dataGridView1.Rows[i].Cells[j].Value.ToString();
                        if (Information.IsNumeric(strNum) == true)
                        {
                            intNum = Convert.ToDouble(strNum);
                            dataGridView1.Rows[i].Cells[j].Value = String.Format("{0:p}", intNum);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        public virtual void sldr_Fill()
        {
            string name = "sldr";
            int i;

            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                if (new int[] { 0, 17, 34, 47, 54 }.Contains(i)) continue;
                RangeSlider sldrCtrl = (RangeSlider)dataGridView1.Controls[name + i];

                // MIN VALS
                if (dataGridView1.Rows[i].Cells[3].Value != DBNull.Value)
                {
                    sldrCtrl.SliderMax = Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value);
                }
                // MAX VALS
                if (dataGridView1.Rows[i].Cells[2].Value != DBNull.Value)
                {
                    sldrCtrl.SliderMin = Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value);
                }
            }
        }

        public virtual void DynamicCTLRs()
        {
            int Counter = dataGridView1.RowCount - 1;
            int x;
            int y;
            int i;
            int Width;
            int Height;
            Rectangle rect; // STORES A SET OF FOUR INTEGERS
            
            for (i = 0; i <= Counter; i++)
            {
                int switchExpr = i;
                switch (switchExpr)
                {
                    case 0:
                    case 17:
                    case 34:
                    case 47:
                    case 54:
                        {
                            break;
                        }

                    default:
                        {
                            var sldrCtrl = new RangeSlider();
                            sldrCtrl.Name = "sldr" + i;
                            sldrCtrl.VisualStyle = RangeSlider.RangeSliderStyle.Metro;
                            sldrCtrl.RangeColor = SystemColors.Highlight;
                            sldrCtrl.HighlightedThumbColor = SystemColors.Highlight;
                            sldrCtrl.ThumbColor = SystemColors.Highlight;
                            sldrCtrl.PushedThumbColor = SystemColors.Highlight;
                            sldrCtrl.ChannelColor = SystemColors.ControlDark;
                            sldrCtrl.BackColor = Color.White;
                            sldrCtrl.Minimum = 0;
                            sldrCtrl.Maximum = 99;
                            sldrCtrl.SliderMin = 0;
                            sldrCtrl.SliderMax = 0;
                            sldrCtrl.Enabled = false;

                            dataGridView1.Controls.Add(sldrCtrl);

                            // SET POSITION
                            rect = dataGridView1.GetCellDisplayRectangle(4, i, false);
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
                            break;
                        }                      
                }
                
            }
            
        }

        public virtual void sldrCrtl_Click(object sender, EventArgs e)
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

            sldrCtrl.Enabled = true;

            try
            {
                Diff = sldrCtrl.Name.Length - name.Trim().Length;
                rowNum = Convert.ToInt32(sldrCtrl.Name.Substring(sldrCtrl.Name.Length - Diff, Diff));
            }
            catch (Exception ex)
            {
            }

            dataGridView1.CurrentCell = dataGridView1.Rows[rowNum].Cells[4];

            // SHOW CHART AT POSITION
            rect = dataGridView1.GetCellDisplayRectangle(4, rowNum, false);
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

        public void sldrCtrl_DoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            RangeSlider sldrCtrl = (RangeSlider)sender;
            if (e.Button == MouseButtons.Left)
            {
                // RESET
                sldrCtrl.SliderMin = 0;
                sldrCtrl.SliderMax = 0;
            }
            
        }

        public void sldrCtrl_Leave(object sender, EventArgs e)
        {
            RangeSlider sldrCtrl = (RangeSlider)sender;
            sldrCtrl.Enabled = false;

        }
        public virtual void sldrCrtl_ValueChanged(object sender, EventArgs e)
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
            double avg = 0;
            int ticker = 0;
            double dontExceed;
            double avgRange;

            if (loading > 0) return;
            
            try
            {
                Diff = sldrCtrl.Name.Length - name.Trim().Length;
                rowNum = Convert.ToInt32(sldrCtrl.Name.Substring(sldrCtrl.Name.Length - Diff, Diff));
            }
            catch (Exception ex)
            {
            }

            if (dataGridView1.CurrentCell.RowIndex != rowNum)
            {
                return;
            }

            // SHOW CHART AT POSITION
            rect = dataGridView1.GetCellDisplayRectangle(4, rowNum, false);
            x = rect.X;
            y = rect.Y;
            Width = rect.Width;

            NormDist.SetBounds(x + 11, y + 10, Width + 1, 110);
            NormDist.Visible = true;

            // SET MIN & MAX TO CELL
            dataGridView1.Rows[rowNum].Cells[2].Value = sldrCtrl.SliderMin;
            dataGridView1.Rows[rowNum].Cells[3].Value = sldrCtrl.SliderMax;

            //HIGH & LOW VALUES
            low = cumulative[sldrCtrl.SliderMin];
            high = cumulative[sldrCtrl.SliderMax];
            

            dataGridView1.Rows[rowNum].Cells[5].Value = string.Format("{0:p}", low);
            dataGridView1.Rows[rowNum].Cells[6].Value = string.Format("{0:p}", high);
            
            //AVERAGE
            dataGridView1.Rows[rowNum].Cells[4].Value = (low + high) / 2;

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

            // RUN THROUGH MAX SLIDER VALUES FOR EACH GROUP
            int switchExpr = rowNum;
            switch (switchExpr)
            {
                case object _ when 1 <= switchExpr && switchExpr <= 16:
                    {
                        // CALCULATE CUMULATIVE AVG
                        for (i = 1; i <= 16; i++)
                        {
                            if (i == rowNum)
                            {
                                continue;
                            }
                            else
                            {
                                if (dataGridView1.Rows[i].Cells[4].Value != DBNull.Value)
                                {
                                    avg += Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value);
                                }
                                else
                                {
                                    avg += 0;
                                }
                            }
                        }
                    }
                    break;

                case object _ when 18 <= switchExpr && switchExpr <= 33:
                    {
                        // CALCULATE CUMULATIVE AVG
                        for (i = 18; i <= 33; i++)
                        {
                            if (i == rowNum)
                            {
                                continue;
                            }
                            else
                            {
                                if (dataGridView1.Rows[i].Cells[4].Value != DBNull.Value)
                                {
                                    avg += Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value);
                                }
                                else
                                {
                                    avg += 0;
                                }
                            }
                        }
                    }
                    break;
                case object _ when 35 <= switchExpr && switchExpr <= 46:
                    {
                        // CALCULATE CUMULATIVE AVG
                        for (i = 35; i <= 46; i++)
                        {
                            if (i == rowNum)
                            {
                                continue;
                            }
                            else
                            {
                                if (dataGridView1.Rows[i].Cells[4].Value != DBNull.Value)
                                {
                                    avg += Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value);
                                }
                                else
                                {
                                    avg += 0;
                                }
                            }
                        }
                    }
                    break;
                case object _ when 48 <= switchExpr && switchExpr <= 53:
                    {
                        // CALCULATE CUMULATIVE AVG
                        for (i = 48; i <= 53; i++)
                        {
                            if (i == rowNum)
                            {
                                continue;
                            }
                            else
                            {
                                if (dataGridView1.Rows[i].Cells[4].Value != DBNull.Value)
                                {
                                    avg += Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value);
                                }
                                else
                                {
                                    avg += 0;
                                }
                            }
                        }
                    }
                    break;
                case object _ when 55 <= switchExpr && switchExpr <= 79:
                    {
                        // CALCULATE CUMULATIVE AVG
                        for (i = 55; i <= 79; i++)
                        {
                            if (i == rowNum)
                            {
                                continue;
                            }
                            else
                            {
                                if (dataGridView1.Rows[i].Cells[4].Value != DBNull.Value)
                                {
                                    avg += Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value);
                                }
                                else
                                {
                                    avg += 0;
                                }
                            }
                        }
                    }
                    break;

                default:
                    {
                        break;
                    }
            }

            if (avg > 1) avg = 1;
            dontExceed = 1 - avg;
            avgRange = (cumulative[sldrCtrl.SliderMin] + cumulative[sldrCtrl.SliderMax]) / 2;
            // CONTROL FOR SLIDERMAX VALUES EXCEEDING AVG
            try
            {
                if (avgRange > dontExceed)
                {
                    for (i = 0; i <= cumulative.Count - 1; i++)
                    {
                        if (cumulative[i] + cumulative[sldrCtrl.SliderMin] > dontExceed * 2)
                        {
                            if (i - 1 >= 0)
                            {
                                ticker = i - 1;
                                break;
                            }
                            else
                            {
                                ticker = 0;
                                break;
                            }
                        }
                    }
                    if (ticker >= 0 && cumulative[ticker] >= cumulative[sldrCtrl.SliderMin])
                    { 
                        sldrCtrl.SliderMax = ticker;
                    }
                }
            }
            catch (Exception ex)
            {

            }

            // CONTROL FOR SLIDERMIN VALUES EXCEEDING AVG
            try
            {
                if (avgRange > dontExceed)
                {
                    for (i = 0; i <= cumulative.Count - 1; i++)
                    {
                        if (cumulative[i] + cumulative[sldrCtrl.SliderMax] > dontExceed * 2)
                        {
                            if (i - 1 >= 0)
                            {
                                ticker = i - 1;
                                break;
                            }
                            else
                            {
                                ticker = 0;
                                break;
                            }
                        }
                    }
                    if (ticker >= 0 && cumulative[ticker] <= cumulative[sldrCtrl.SliderMax])
                    {
                        sldrCtrl.SliderMin = ticker;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public virtual void Move_CTRLs()
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
            var rowNum = default(int);
            string name = "sldr";

           

            //FIND & MOVE ALL DYNAMIC CONTROLS
            foreach (Control ctrl in dataGridView1.Controls)
            {
                if (ctrl is RangeSlider)
                {
                try
                {
                    Diff = ctrl.Name.Length - name.Trim().Length;
                    rowNum = Convert.ToInt32(ctrl.Name.Substring(ctrl.Name.Length - Diff, Diff));
                }
                catch (Exception ex)
                {
                }

                rect = dataGridView1.GetCellDisplayRectangle(4, rowNum, false);
                x = rect.X;
                y = rect.Y;
                width = rect.Width;
                height = rect.Height;

                ctrl.SetBounds(x, y, width, height);
                 
                if (ctrl.Name == name + "1") ctrl.Visible = true;
                }
            }

        }

        public void dataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            NormDist.Visible = false;
            Move_CTRLs();
            
        }

        public void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            NormDist.Series[0].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.None;
            NormDist.Visible = false;
        }

        public virtual void submit_fill()
        {
            int i;
            // FILL LOW RANGE COLUMN
            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                if (new int[] { 0, 17, 34, 47, 54 }.Contains(i)) continue;
                if (dataGridView1.Rows[i].Cells[2].Value != DBNull.Value)
                {
                    dataGridView1.Rows[i].Cells[5].Value = cumulative[Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value)];
                }
                else
                {
                    dataGridView1.Rows[i].Cells[5].Value = 0;
                }
            }
            // FILL HIGH RANGE COLUMN
            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                if (new int[] { 0, 17, 34, 47, 54 }.Contains(i)) continue;
                if (dataGridView1.Rows[i].Cells[3].Value != DBNull.Value)
                {
                    dataGridView1.Rows[i].Cells[6].Value = cumulative[Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value)];
                }
                else
                {
                    dataGridView1.Rows[i].Cells[6].Value = 0;
                }
            }
        }

        public virtual void process_Submit()
        {
            int rowNum;
            int i;
            double avg1 = 0;
            double avg2 = 0;
            double avg3 = 0;
            double avg4 = 0;
            double avg5 = 0;
            int counter = 0;
            string title = "TINUUM SOFTWARE";
            
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
   
            // RUN THROUGH MAX SLIDER VALUES FOR EACH GROUP

            for (rowNum = 0; rowNum <= dataGridView1.RowCount - 1; rowNum++)
            {
                int switchExpr = rowNum;
                switch (switchExpr)
                {
                    case object _ when 1 <= switchExpr && switchExpr <= 16:
                        {
                            // CALCULATE CUMULATIVE AVG
                            for (i = 1; i <= 16; i++)
                            {
                                if (dataGridView1.Rows[i].Cells[4].Value != DBNull.Value)
                                {
                                    avg1 += Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value);
                                }
                                else
                                {
                                    avg1 += 0;
                                }
                            }
                            if (avg1 < .01)
                            {
                                MessageBox.Show("Retry. You must enter at least one signifcant value within each case-mix group.", "TINUUM SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        break;

                    case object _ when 18 <= switchExpr && switchExpr <= 33:
                        {
                            // CALCULATE CUMULATIVE AVG
                            for (i = 18; i <= 33; i++)
                            {
                                if (dataGridView1.Rows[i].Cells[4].Value != DBNull.Value)
                                {
                                    avg2 += Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value);
                                }
                                else
                                {
                                    avg2 += 0;
                                }
                            }
                            if (avg2 < .01)
                            {
                                MessageBox.Show("Retry. You must enter at least one signifcant value within each case-mix group.", "TINUUM SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        break;
                    case object _ when 35 <= switchExpr && switchExpr <= 46:
                        {
                            // CALCULATE CUMULATIVE AVG
                            for (i = 35; i <= 46; i++)
                            {
                                if (dataGridView1.Rows[i].Cells[4].Value != DBNull.Value)
                                {
                                    avg3 += Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value);
                                }
                                else
                                {
                                    avg3 += 0;
                                }
                            }
                            if (avg3 < .01)
                            {
                                MessageBox.Show("Retry. You must enter at least one signifcant value within each case-mix group.", "TINUUM SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        break;
                    case object _ when 48 <= switchExpr && switchExpr <= 53:
                        {
                            // CALCULATE CUMULATIVE AVG
                            for (i = 48; i <= 53; i++)
                            {
                                if (dataGridView1.Rows[i].Cells[4].Value != DBNull.Value)
                                {
                                    avg4 += Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value);
                                }
                                else
                                {
                                    avg4 += 0;
                                }
                            }
                            if (avg4 < .01)
                            {
                                MessageBox.Show("Retry. You must enter at least one signifcant value within each case-mix group.", "TINUUM SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        break;
                    case object _ when 55 <= switchExpr && switchExpr <= 79:
                        {
                            // CALCULATE CUMULATIVE AVG
                            for (i = 55; i <= 79; i++)
                            {
                                if (dataGridView1.Rows[i].Cells[4].Value != DBNull.Value)
                                {
                                    avg5 += Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value);
                                }
                                else
                                {
                                    avg5 += 0;
                                }
                            }
                            if (avg5 < .01)
                            {
                                MessageBox.Show("Retry. You must enter at least one signifcant value within each case-mix group.", "TINUUM SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        break;

                    default:
                        {
                            break;
                        }
                }
            }
            // CALL METHODS
            submit_fill();
            SQL_Variable.DBDA.Update(SQL_Variable.DBDT);
            update_active();

            frm.Enabled = true;
            this.Dispose();
        }

        protected void update_active()
        {
            string cmdUpdate;
            
            // UPDATE ACTIVE TABLE WITH
            SQL_Active.AddParam("@PrimeKey", primeKey);
            SQL_Active.AddParam("@CaseName", configName.Text);
            cmdUpdate = "UPDATE " + tbl_Active + " SET " + slctCol +"=@CaseName WHERE Prime=@PrimeKey;";
            SQL_Active.ExecQuery(cmdUpdate);
        }
        
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            process_Submit();
        }

        public virtual void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            int i;
            string name = "sldr";

            if (e.Button == MouseButtons.Left)
            {
                for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                {
                    if (new int[] { 0, 17, 34, 47, 54 }.Contains(i)) continue;
                    RangeSlider sldrCtrl = (RangeSlider)dataGridView1.Controls[name + i];
                    if (dataGridView1.GetCellDisplayRectangle(4, i, false).Contains(e.Location))
                    {
                        sldrCtrl.Enabled = true;
                    }
                }
            }
        }

        public virtual void call_cancel()
        {
            string Title = "TINUUM SOFTWARE";

            DialogResult prompt = MessageBox.Show("Are you sure? Any unsaved data will be lost", Title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            Cncl = prompt.ToString();
            try
            {
                if (prompt == DialogResult.Yes)
                {
                    if (actCtrl.Name == "btnAdd")
                    {
                        // DROP TABLE
                        SQL_Variable.ExecQuery("DROP TABLE " + tbl_Variable + ";");

                        // DELETE ENTRY FROM TABLE
                        SQL_Variable.AddParam("@PrimeKey", primeKey);
                        SQL_Variable.ExecQuery("DELETE FROM " + tbl_Active + " WHERE Prime=@PrimeKey;");

                        // clean up
                        frm.Enabled = true;
                        this.Close();
                    }
                    else
                    {
                        frm.Enabled = true;
                        this.Close();
                    }
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            call_cancel();    
        }

        public virtual void FormMarketPDPM_FormClosing(object sender, FormClosingEventArgs e)
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

        public virtual void Delegate()
        {
            SQLQueries.tblMarketPDPMCreate();
        }

        public virtual void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // HOLDER
        }

        public virtual void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        public virtual void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

        }

        public virtual void FormMarketPDPM_SizeChanged(object sender, EventArgs e)
        {

        }
    }
}

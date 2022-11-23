using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using Syncfusion.Windows.Forms.Tools;

namespace Tinuum_Software_BETA.Popups.Inventory
{
    [CLSCompliant(true)]
    public partial class FormInventoryLifeCycle : Tinuum_Software_BETA.Popups.FormMarketPDPM
    {
        protected DataGridView dgv = Application.OpenForms[1].Controls["dataGridView1"] as DataGridView;
        public FormInventoryLifeCycle()
        {
            InitializeComponent();
            tbl_New = "dtbInventoryNormDist";
            tbl_Prefix = "dtbInventoryLife";
            frm = Application.OpenForms[1] as Form;
            actCtrl = null;
            lstBox = null;
            InitializeComponent();
        }
        public override void FormMarketPDPM_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            loading = 1;
            List<double> myList = new List<double>();
            int i;

            // DATAGRIDVIEW SPECS
            //dataGridView1.Width = 700;
            //dataGridView1.Height = 150;
            //btnSubmit.Location = new Point(585, 22);
            //btnCancel.Location = new Point(457, 22);
            //this.Width = 800;
            //this.Height = 500;
            //dataGridView1.BackgroundColor = Color.White;

            // QUERY TO GET TABLE FOR LATER METHODS
            SQL_Active.ExecQuery("SELECT * FROM " + tbl_Active + ";");

            // FIND PRIME KEY TO SELECTT TABLE
            primeKey = Convert.ToInt32(dgv.Rows[dgv.CurrentCell.RowIndex].Cells[0].Value);

            // GET TABLE AND SELECT
            tbl_Variable = tbl_Prefix + primeKey;
            SQL_Variable.ExecQuery("SELECT * FROM " + tbl_Variable + ";");

            dataGridView1.DataSource = SQL_Variable.DBDT;

            //int you = SQL_Variable.RecordCount;

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
                NormDist.Series["Distribution"].BorderWidth = 0;
                NormDist.Series["Distribution"].BorderColor = SystemColors.Highlight;
                NormDist.Series["Distribution"].Color = SystemColors.Highlight;
                NormDist.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
                NormDist.BorderlineColor = SystemColors.ControlDark;
            }

            // ADD SUBMIT NAME
            configName.Visible = false;

            // CALL METHODS
            DynamicCTLRs();
            sldr_Fill();
            Percent_Change();

            // dataGridView1.FirstDisplayedScrollingRowIndex = 0;

            Move_CTRLs();

            loading = 0;
            dataGridView1.Visible = true;
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
            double dontExceed;
            double avgRange;
            int distance;


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

            int switchExpr = sldrCtrl.SliderMin;
            switch (switchExpr)
            {
                case object _ when 0 <= switchExpr && switchExpr <= 24:
                    {
                        dataGridView1.Rows[rowNum].Cells[5].Value = "Launch";
                    }
                    break;
                case object _ when 25 <= switchExpr && switchExpr <= 49:
                    {
                        dataGridView1.Rows[rowNum].Cells[5].Value = "Growth";
                    }
                    break;
                case object _ when 50 <= switchExpr && switchExpr <= 74:
                    {
                        dataGridView1.Rows[rowNum].Cells[5].Value = "Mature";
                    }
                    break;
                case object _ when 75 <= switchExpr && switchExpr <= 100:
                    {
                        dataGridView1.Rows[rowNum].Cells[5].Value = "Decline";
                    }
                    break;
                default:
                    break;
            }

            int switchExpr1 = sldrCtrl.SliderMax;
            switch (switchExpr1)
            {
                case object _ when 0 <= switchExpr1 && switchExpr1 <= 24:
                    {
                        dataGridView1.Rows[rowNum].Cells[6].Value = "Launch";
                    }
                    break;
                case object _ when 25 <= switchExpr1 && switchExpr1 <= 49:
                    {
                        dataGridView1.Rows[rowNum].Cells[6].Value = "Growth";
                    }
                    break;
                case object _ when 50 <= switchExpr1 && switchExpr1 <= 74:
                    {
                        dataGridView1.Rows[rowNum].Cells[6].Value = "Mature";
                    }
                    break;
                case object _ when 75 <= switchExpr1 && switchExpr1 <= 100:
                    {
                        dataGridView1.Rows[rowNum].Cells[6].Value = "Decline";
                    }
                    break;
                default:
                    break;
            }

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


        }
        public override void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int distance;
            string name = "sldr";
            RangeSlider rngSlde = dataGridView1.Controls[name + e.RowIndex] as RangeSlider;

            //FIND DYNAMIC CONTROL           
            distance = Math.Abs(rngSlde.SliderMax - rngSlde.SliderMin);

            // CONTROL FOR MAX DISTANCE BETWEEN TICKERs
            switch (e.ColumnIndex)
            {
                case 2:
                    {
                        if (distance > 20)
                        {
                            rngSlde.SliderMax = rngSlde.SliderMin + 20;
                        }
                    }
                    break;
                case 3:
                    {
                        if (distance > 20)
                        {
                            rngSlde.SliderMin = rngSlde.SliderMax - 20;
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        public override void process_Submit()
        {
            int rowNum;
            int i;
            double avg1 = 0;
            string title = "TINUUM SOFTWARE";

            // RUN THROUGH MAX SLIDER VALUES FOR EACH GROUP

            for (rowNum = 1; rowNum <= dataGridView1.RowCount - 1; rowNum++)
            {
                int switchExpr = rowNum;
                switch (switchExpr)
                {
                    case object _ when 1 <= switchExpr && switchExpr <= 1:
                        {
                            // CALCULATE CUMULATIVE AVG
                            for (i = 1; i <= 1; i++)
                            {
                                if (dataGridView1.Rows[i].Cells[5].Value == DBNull.Value || dataGridView1.Rows[i].Cells[6].Value == DBNull.Value)
                                {
                                    MessageBox.Show("Retry. You must enter a value before submitting.", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                        }
                        break;

                    default:
                        break;
                }
            }
            // CALL METHODS
            // base.submit_fill();
            SQL_Variable.DBDA.Update(SQL_Variable.DBDT);
            base.update_active();

            frm.Enabled = true;
            this.Dispose();
        }
        public override void call_cancel()
        {
            string Title = "TINUUM SOFTWARE";

            DialogResult prompt = MessageBox.Show("Are you sure? Any unsaved data will be lost", Title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            Cncl = prompt.ToString();

            if (prompt == DialogResult.Yes)
            {
                this.Close();
                dgv.Rows[dgv.CurrentCell.RowIndex].Cells[14].Value = DBNull.Value;
            }
            else
            {
                return;
            }
        }
    }
}

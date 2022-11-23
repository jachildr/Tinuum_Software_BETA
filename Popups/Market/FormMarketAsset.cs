using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Syncfusion.Windows.Forms.Tools;

namespace Tinuum_Software_BETA.Popups.Market
{
    public partial class FormMarketAsset : Tinuum_Software_BETA.Popups.Market.FormMarketIncome
    {
        [CLSCompliant(true)]
        public FormMarketAsset()
        {
            InitializeComponent();
            tbl_Prefix = "dtbMarketAsset";
            tbl_Active = "dtbMarketConfigureAsset";
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
                case object _ when 1 <= switchExpr && switchExpr <= 11:
                    {
                        // CALCULATE CUMULATIVE AVG
                        for (i = 1; i <= 11; i++)
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
                    break;
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

        public override void process_Submit()
        {
            int rowNum;
            int i;
            double avg1 = 0;
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
                    case object _ when 1 <= switchExpr && switchExpr <= 11:
                        {
                            // CALCULATE CUMULATIVE AVG
                            for (i = 1; i <= 11; i++)
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

                    default:
                        break;
                }
            }
            // CALL METHODS
            base.submit_fill();
            SQL_Variable.DBDA.Update(SQL_Variable.DBDT);
            base.update_active();

            frm.Enabled = true;
            this.Dispose();
        }

        public override void Delegate()
        {
            SQLQueries.tblMarketAssetCreate();
        }
    }
}

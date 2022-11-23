using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Syncfusion.Windows.Forms.Tools;

namespace Tinuum_Software_BETA
{
    public partial class Form1 : Form
    { 
        public Form1()
        {
            InitializeComponent();
        }
        private void  rngMore()
        {
            
            //RangeSlider rangeSlider1 = new RangeSlider();
            //rangeSlider1.ShowLabels = true;
            //this.Controls.Add(rangeSlider1);

            rangeSlider1.RangeColor = SystemColors.Highlight;
            rangeSlider1.HighlightedThumbColor = SystemColors.Highlight;
            rangeSlider1.ThumbColor = SystemColors.Highlight;
            rangeSlider1.PushedThumbColor = SystemColors.Highlight;
            rangeSlider1.ChannelColor = SystemColors.ControlDark;
            rangeSlider1.BackColor = Color.White;
            //trackBar1.BackColor = Color.Transparent;
            trackBar2.Value = 5;
            trackBar2.Height = 20;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            rngMore();
            
            SQLControl SQL_DB = new SQLControl();
            string tbl_PDPM = "dtbMarketPDPM1";
            SQL_DB.ExecQuery("SELECT * FROM " + tbl_PDPM + ";");
            listBox1.DataSource = SQL_DB.DBDT;
            listBox1.DisplayMember = "Case Mix Components";
            listBox1.SelectionMode = SelectionMode.MultiExtended;

            SQLQueries.tbl_CAPEX_ExpenseGroups();
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            //frmGeneral frmPct = new frmGeneral();
            //frmPct.Show(this);
            //MessageBox.Show(Application.OpenForms[0].Name.ToString());
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            //trackBar1.BackColor = Color.Transparent;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            //trackBar2.Value = 5;
        }

        private void rangeSlider1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void toolStripComboBoxEx1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

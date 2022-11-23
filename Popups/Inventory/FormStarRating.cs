using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Tinuum_Software_BETA.Popups.Inventory
{
    public partial class FormStarRating : Form
    {
        protected string tbl_Prefix = "dtbInventoryStar";
        protected string tbl_Active = "dtbInventoryConfigureStar";
        protected string tbl_Variable;
        protected string slctCol = "collection_groups";
        protected string keyCol = "Prime";
        protected string Cncl = null;
        protected SQLControl SQL_Variable = new SQLControl();
        protected SQLControl SQL_Active = new SQLControl();
        protected Control actCtrl;
        protected ListBox lstBox; // CHANGE FORM NUM
        protected Form frm;
        protected DataRowView drv;
        protected int loading;
        protected int primeKey;
        protected int lstIndex;
        protected int terminate = 0;
        public FormStarRating()
        {
            InitializeComponent();
            actCtrl = Application.OpenForms[2].ActiveControl; // CHANGE FORM NUM
            lstBox = Application.OpenForms[2].Controls["listBox1"] as ListBox;
            frm = Application.OpenForms[2];
        }

        private void FormStarRating_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            loading = 1;
            int i;
            int count;
            string name = "bar";


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
            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[2].Width = 300;
            dataGridView1.Columns[2].DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView1.Columns[2].DefaultCellStyle.ForeColor = Color.White;
            dataGridView1.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            foreach (DataGridViewColumn Col in dataGridView1.Columns)
            {
                Col.SortMode = DataGridViewColumnSortMode.NotSortable;
                Col.ReadOnly = true;
            }

            // ADD SUBMIT NAME
            if (actCtrl.Name != "btnAdd")
            {
                configName.Text = lstBox.Text;
            }

            // CALL METHODS
            DynamicCTLRs();

            dataGridView1.FirstDisplayedScrollingRowIndex = 0;

            loading = 0;

            // FILL TRACKER VALUES
            if (actCtrl.Name == "btnEdit")
            {
                for (i = 0; i <= 3; i++)
                {
                    TrackBar bar = dataGridView1.Controls[name + i] as TrackBar;
                    bar.Value = Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value);
                }
            }
            

        }
        protected void DynamicCTLRs()
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
                var bar = new TrackBar();
                bar.Name = "bar" + i;
                bar.Minimum = 1;
                bar.Maximum = 5;
                bar.TickStyle = TickStyle.None;
                bar.SmallChange = 1;
                bar.LargeChange = 1;
                bar.BackColor = SystemColors.Window;
                bar.AutoSize = false;

                dataGridView1.Controls.Add(bar);

                // SET POSITION
                rect = dataGridView1.GetCellDisplayRectangle(2, i, false); // CHANGE COLUMN WHEN NEW
                x = rect.X;
                y = rect.Y;
                Width = rect.Width;
                Height = rect.Height;

                bar.SetBounds(x, y, Width - 2, Height - 2);
                bar.Visible = true;
                
                // ADD EVENT HANDLER
                bar.Scroll += new EventHandler(trackBar_Scroll);
            }

        }

        private void scroll_Changed()
        {
            string name = "bar";
            TrackBar bar1 = dataGridView1.Controls[name + 0] as TrackBar;
            TrackBar bar2 = dataGridView1.Controls[name + 1] as TrackBar;
            TrackBar bar3 = dataGridView1.Controls[name + 2] as TrackBar;
            TrackBar bar4 = dataGridView1.Controls[name + 3] as TrackBar;
            int val1;
            int val2;
            int val3;
            int val4;
            int diff1;
            int diff2;

            val1 = bar1.Value; 
            switch (val1)
            {
                case object _ when val1 <= bar2.Value:
                    {
                        switch (bar2.Value)
                        {
                            case object _ when bar2.Value >= 4:
                                {
                                    val2 = val1 + 1;
                                }
                                break;
                            default:
                                {
                                    val2 = val1;
                                }
                                break;
                        }
                    }
                    break;
                default:
                    {
                        switch (bar2.Value)
                        {
                            case 1:
                                {
                                    val2 = val1 - 1;
                                }
                                break;
                            default:
                                {
                                    val2 = val1;
                                }
                                break;
                        }
                    }
                    break;
            }

            switch (bar3.Value)
            {
                case 1:
                    {
                        val3 = val2 - 1;
                    }
                    break;
                case 5:
                    {
                        val3 = val2 + 1;
                    }
                    break;
                default:
                    {
                        val3 = val2;
                    }
                    break;
            }
            
            switch (val3)
            {
                case object _ when val3 > 5:
                    {
                        val3 = 5;
                    }
                    break;
                case object _ when val3 < 1:
                    {
                        val3 = 1;
                    }
                    break;
            }

            diff1 = val2 - val1;
            diff2 = val3 - val2;

            switch (val1)
            {
                case 1:
                    {
                        if (diff1 + diff2 > 1)
                        {
                            val4 = 2;
                        }
                        else
                        {
                            val4 = val3;
                        }
                    }
                    break;
                default:
                    {
                        val4 = val3;
                    }
                    break;
            }

            bar4.Value = val4;
            dataGridView1.Rows[0].Cells[3].Value = bar1.Value;
            dataGridView1.Rows[1].Cells[3].Value = bar2.Value;
            dataGridView1.Rows[2].Cells[3].Value = bar3.Value;
            dataGridView1.Rows[3].Cells[3].Value = bar4.Value;

        }

        public virtual void process_Submit()
        {
            int i;
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

            for (i = 0; i <= 3; i++)
            {
                if (dataGridView1.Rows[i].Cells[3].Value == DBNull.Value)
                {
                    MessageBox.Show("You must enter a rating before continuing. Retry.", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // CALL METHODS
            SQL_Variable.DBDA.Update(SQL_Variable.DBDT);
            update_active();

            frm.Enabled = true;
            this.Dispose();
        }

        private void trackBar_Scroll(object sender, EventArgs e)
        {
            TrackBar bar = (TrackBar)sender;
            scroll_Changed();
        }
        
        protected void update_active()
        {
            string cmdUpdate;

            // UPDATE ACTIVE TABLE WITH
            SQL_Active.AddParam("@PrimeKey", primeKey);
            SQL_Active.AddParam("@CaseName", configName.Text);
            cmdUpdate = "UPDATE " + tbl_Active + " SET " + slctCol + "=@CaseName WHERE Prime=@PrimeKey;";
            SQL_Active.ExecQuery(cmdUpdate);
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            process_Submit();
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

        public virtual void Delegate()
        {
            SQLQueries.tblInventoryStarCreate();
        }

        private void FormStarRating_FormClosing(object sender, FormClosingEventArgs e)
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
    }
}

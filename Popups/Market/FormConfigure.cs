using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tinuum_Software_BETA.Popups
{
    [CLSCompliant(true)]
    public partial class FormConfigure : Form
    {
        protected SQLControl SQL_VarConfig = new SQLControl();
        protected string tbl_Prefix = "dtbMarketPDPM";
        protected string tbl_Variant = "dtbMarketConfigurePDPM";
        protected string displayStr = "collection_groups";
        protected string tbl_Delete;
        protected Form frm = Application.OpenForms[1];
        protected int frmRow;
        protected DataGridView dgv = Application.OpenForms[1].Controls["dataGridView1"] as DataGridView;

        public FormConfigure()
        {
            InitializeComponent();
        }

        private void FormConfigure_Load(object sender, EventArgs e)
        {
            SQL_VarConfig.ExecQuery("SELECT * FROM " + tbl_Variant + ";");
            listBox1.DataSource = SQL_VarConfig.DBDT;
            listBox1.DisplayMember = displayStr;
            listBox1.SelectionMode = SelectionMode.One;
            this.StartPosition = FormStartPosition.CenterScreen;
            frm.Enabled = false;
        }

        public virtual void btnEdit_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0)
            {
                MessageBox.Show("You must add a record or select a valid entry", "TINUUM SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FormMarketPDPM frmCollection = new FormMarketPDPM();
            frmCollection.Show(this);

            this.Enabled = false;
        }

        public virtual void btnDelete_Click(object sender, EventArgs e)
        {
            int lstIndex;
            int primeKey;
            string Title = "TINUUM SOFTWARE";

            // FIND PRIME KEY TO SELECTT TABLE
            lstIndex = listBox1.SelectedIndex;

            // REFRESH TABLE
            SQL_VarConfig.ExecQuery("SELECT * FROM " + tbl_Variant + ";");

            // GET PRIME KEY
            if (listBox1.SelectedIndex < 0)
            {
                MessageBox.Show("You must select a valid record before continuing.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                primeKey = Convert.ToInt32(SQL_VarConfig.DBDT.Rows[lstIndex][0]);
            }

            // GET TABLE AND SELECT
            tbl_Delete = tbl_Prefix + primeKey;

            // CALL DIALOUGUE AND EXECUTE
            DialogResult prompt = MessageBox.Show("Are you sure? Any unsaved data will be lost", Title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            try
            {
                if (prompt == DialogResult.Yes)
                {
                    SQL_VarConfig.ExecQuery("DROP TABLE " + tbl_Delete + ";");
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {

            }

            // DELETE ENTRY FROM TABLE
            SQL_VarConfig.AddParam("@PrimeKey", primeKey);
            SQL_VarConfig.ExecQuery("DELETE FROM " + tbl_Variant + " WHERE Prime=@PrimeKey;");

            // UPDATE LIST BOX
            SQL_VarConfig.ExecQuery("SELECT * FROM " + tbl_Variant + ";");
            listBox1.DataSource = SQL_VarConfig.DBDT;
            listBox1.DisplayMember = displayStr;
        }

        public virtual void btnAdd_Click(object sender, EventArgs e)
        {
            // INSERT NEW RECORD IN DATA TABLE
            SQL_VarConfig.ExecQuery("INSERT INTO " + tbl_Variant + " DEFAULT VALUES;");

            // UPDATE LISTBOX
            SQL_VarConfig.ExecQuery("SELECT * FROM " + tbl_Variant + ";");
            listBox1.DataSource = SQL_VarConfig.DBDT;
            listBox1.DisplayMember = displayStr;

            // SHOW FORM
            listBox1.SelectedIndex = -1;
            FormMarketPDPM frmCollection = new FormMarketPDPM();
            frmCollection.Show(this);

            this.Enabled = false;
        }

        private void FormConfigure_EnabledChanged(object sender, EventArgs e)
        {
            if (this.Enabled == true)
            {
                SQL_VarConfig.ExecQuery("SELECT * FROM " + tbl_Variant + ";");
                listBox1.DataSource = null;
                listBox1.DataSource = SQL_VarConfig.DBDT;
                listBox1.DisplayMember = displayStr;
            }
        }

        public virtual void combo_Update()
        {
            frmRow = dgv.CurrentCell.RowIndex;
            int i;
            int start = 5;
            
            for (i = start; i <= dgv.ColumnCount - 1; i++)
            {
                var cell = dgv.Rows[frmRow].Cells[i] as DataGridViewComboBoxCell;
                cell.DataSource = SQL_VarConfig.DBDT;
            }
            
        }

        public virtual void FormConfigure_FormClosing(object sender, FormClosingEventArgs e)
        {
            combo_Update();
            frm.Enabled = true;
        }
    }
}

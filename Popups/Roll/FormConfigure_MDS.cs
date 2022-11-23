using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Tinuum_Software_BETA.Popups.Roll
{
    public partial class FormConfigure_MDS : Tinuum_Software_BETA.Popups.FormConfigure
    {
        protected string tbl_BIMS = "dtbRollMDS_BIMS";
        protected string tbl_Function = "dtbRollMDS_FunctionScore";
        protected string tbl_Clinical = "dtbRollMDS_Clinical";
        protected string tbl_Morbid = "dtbRollMDS_SLPMorbid";
        protected string tbl_Disorder = "dtbRollMDS_SLPDisorders";
        protected string tbl_NTA = "dtbRollMDS_NTA";
        protected string tbl_Extensive = "dtbRollMDS_Extensive";
        protected string tbl_Depression = "dtbRollMDS_Depression";
        protected string tbl_SCH = "dtbRollMDS_SCH";
        protected string tbl_SCL = "dtbRollMDS_SCL";
        protected string tbl_Complex = "dtbRollMDS_Complex";
        protected string tbl_Behavioral = "dtbRollMDS_Behavioral";
        protected string tbl_Restorative = "dtbRollMDS_Restorative";
        protected string dlt_tbl_BIMS;
        protected string dlt_tbl_FunctionScore;
        protected string dlt_tbl_Clinical;
        protected string dlt_tbl_Morbid;
        protected string dlt_tbl_Disorder;
        protected string dlt_tbl_NTA;
        protected string dlt_tbl_Extensive;
        protected string dlt_tbl_Depression;
        protected string dlt_tbl_SCH;
        protected string dlt_tbl_SCL;
        protected string dlt_tbl_Complex;
        protected string dlt_tbl_Behavioral;
        protected string dlt_tbl_Restorative;

        public FormConfigure_MDS()
        {
            InitializeComponent();
            tbl_Variant = "dtbRollConfigureMDS";
            frm = Application.OpenForms[3];
        }

        public override void btnEdit_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0)
            {
                MessageBox.Show("You must add a record or select a valid entry", "TINUUM SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FormRollMDS frmCollection = new FormRollMDS();
            frmCollection.Show(this);

            this.Enabled = false;
        }
        public override void btnAdd_Click(object sender, EventArgs e)
        {
            // INSERT NEW RECORD IN DATA TABLE
            SQL_VarConfig.ExecQuery("INSERT INTO " + tbl_Variant + " DEFAULT VALUES;");

            // UPDATE LISTBOX
            SQL_VarConfig.ExecQuery("SELECT * FROM " + tbl_Variant + ";");
            listBox1.DataSource = SQL_VarConfig.DBDT;
            listBox1.DisplayMember = displayStr;

            // SHOW FORM
            listBox1.SelectedIndex = -1;
            FormRollMDS frmCollection = new FormRollMDS();
            frmCollection.Show(this);

            this.Enabled = false;
        }

        public override void btnDelete_Click(object sender, EventArgs e)
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
            dlt_tbl_BIMS = tbl_BIMS + primeKey;
            dlt_tbl_FunctionScore = tbl_Function + primeKey;
            dlt_tbl_Clinical = tbl_Clinical + primeKey;
            dlt_tbl_Morbid = tbl_Morbid + primeKey;
            dlt_tbl_Disorder = tbl_Disorder + primeKey;
            dlt_tbl_NTA = tbl_NTA + primeKey;
            dlt_tbl_Extensive = tbl_Extensive + primeKey;
            dlt_tbl_Depression = tbl_Depression + primeKey;
            dlt_tbl_SCH = tbl_SCH + primeKey;
            dlt_tbl_SCL = tbl_SCL + primeKey;
            dlt_tbl_Complex = tbl_Complex + primeKey;
            dlt_tbl_Behavioral = tbl_Behavioral + primeKey;
            dlt_tbl_Restorative = tbl_Restorative + primeKey;

            // CALL DIALOUGUE AND EXECUTE
            DialogResult prompt = MessageBox.Show("Are you sure? Any unsaved data will be lost", Title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            try
            {
                if (prompt == DialogResult.Yes)
                {
                    SQL_VarConfig.ExecQuery("DROP TABLE " + dlt_tbl_BIMS + ";");
                    SQL_VarConfig.ExecQuery("DROP TABLE " + dlt_tbl_FunctionScore + ";");
                    SQL_VarConfig.ExecQuery("DROP TABLE " + dlt_tbl_Clinical + ";");
                    SQL_VarConfig.ExecQuery("DROP TABLE " + dlt_tbl_Morbid + ";");
                    SQL_VarConfig.ExecQuery("DROP TABLE " + dlt_tbl_Disorder + ";");
                    SQL_VarConfig.ExecQuery("DROP TABLE " + dlt_tbl_NTA + ";");
                    SQL_VarConfig.ExecQuery("DROP TABLE " + dlt_tbl_Extensive + ";");
                    SQL_VarConfig.ExecQuery("DROP TABLE " + dlt_tbl_Depression + ";");
                    SQL_VarConfig.ExecQuery("DROP TABLE " + dlt_tbl_SCH + ";");
                    SQL_VarConfig.ExecQuery("DROP TABLE " + dlt_tbl_SCL + ";");
                    SQL_VarConfig.ExecQuery("DROP TABLE " + dlt_tbl_Complex + ";");
                    SQL_VarConfig.ExecQuery("DROP TABLE " + dlt_tbl_Behavioral + ";");
                    SQL_VarConfig.ExecQuery("DROP TABLE " + dlt_tbl_Restorative + ";");
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

        public override void combo_Update()
        {
            // NOTHING
        }
    }
}

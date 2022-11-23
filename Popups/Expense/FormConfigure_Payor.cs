using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Tinuum_Software_BETA.Popups.Expense
{
    public partial class FormConfigure_Payor : Tinuum_Software_BETA.Popups.FormConfigure
    {
        protected string tbl_Prefix1 = "dtbExpenseSelector_Payor_Output";
        protected string tbl_Delete1;
        public FormConfigure_Payor()
        {
            InitializeComponent();
            tbl_Prefix = "dtbExpenseSelector_Payor_Input";
            tbl_Variant = "dtbExpenseConfigurePayor";
        }

        public override void btnEdit_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0)
            {
                MessageBox.Show("You must add a record or select a valid entry", "TINUUM SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FormSelector_Payor frmCollection = new FormSelector_Payor();
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
            FormSelector_Payor frmCollection = new FormSelector_Payor();
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
            tbl_Delete = tbl_Prefix + primeKey;
            tbl_Delete1 = tbl_Prefix1 + primeKey;

            // CALL DIALOUGUE AND EXECUTE
            DialogResult prompt = MessageBox.Show("Are you sure? Any unsaved data will be lost", Title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            try
            {
                if (prompt == DialogResult.Yes)
                {
                    SQL_VarConfig.ExecQuery("DROP TABLE " + tbl_Delete + ";");
                    SQL_VarConfig.ExecQuery("DROP TABLE " + tbl_Delete1 + ";");
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
            // DO NOTHING
        }
    }
}

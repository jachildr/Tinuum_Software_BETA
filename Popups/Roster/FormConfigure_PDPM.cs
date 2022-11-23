using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Tinuum_Software_BETA.Popups.Roster
{
    public partial class FormConfigure_PDPM : Tinuum_Software_BETA.Popups.Expense.FormConfigurePDPM
    {
        public FormConfigure_PDPM()
        {
            InitializeComponent();
            tbl_Prefix = "dtbRosterSelector_PDPM_Input";
            tbl_Variant = "dtbRosterConfigurePDPM";
            tbl_Prefix1 = "dtbRosterSelector_PDPM_Output";
        }
        public override void btnEdit_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0)
            {
                MessageBox.Show("You must add a record or select a valid entry", "TINUUM SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FormSelector_PDPM frmCollection = new FormSelector_PDPM();
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
            FormSelector_PDPM frmCollection = new FormSelector_PDPM();
            frmCollection.Show(this);

            this.Enabled = false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Tinuum_Software_BETA.Popups.Roster
{
    public partial class FormConfigure_Payor : Tinuum_Software_BETA.Popups.Expense.FormConfigure_Payor
    {
        public FormConfigure_Payor()
        {
            InitializeComponent();
            tbl_Prefix = "dtbRosterSelector_Payor_Input";
            tbl_Variant = "dtbRosterConfigurePayor";
            tbl_Prefix1 = "dtbRosterSelector_Payor_Output";
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
    }
}

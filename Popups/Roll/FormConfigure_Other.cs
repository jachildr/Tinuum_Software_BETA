using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Tinuum_Software_BETA.Popups.Roll
{
    public partial class FormConfigure_Other : Tinuum_Software_BETA.Popups.Roll.FormConfigure_Medicaid
    {
        public FormConfigure_Other()
        {
            InitializeComponent();
            tbl_Prefix = "dtbRollOther";
            tbl_Variant = "dtbRollConfigureOtherRate";
            tbl_DtlPrefix = "dtbRollDetailDynamic_OtherRate";
            tbl_DynPrefix = "dtbRollDynamic_OtherRate";
            tbl_ValPrefix = "dtbRoll_OtherRate";
        }
        public override void btnEdit_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0)
            {
                MessageBox.Show("You must add a record or select a valid entry", "TINUUM SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FormRollOther frmCollection = new FormRollOther();
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
            FormRollOther frmCollection = new FormRollOther();
            frmCollection.Show(this);

            this.Enabled = false;
        }
    }
}

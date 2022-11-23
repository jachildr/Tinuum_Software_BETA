using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Tinuum_Software_BETA.Popups.Market
{
    public partial class FormConfigure_Income : Tinuum_Software_BETA.Popups.FormConfigure
    {
        public FormConfigure_Income()
        {
            InitializeComponent();
            tbl_Prefix = "dtbMarketIncome";
            tbl_Variant = "dtbMarketConfigureIncome";
        }

        public override void btnEdit_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0)
            {
                MessageBox.Show("You must add a record or select a valid entry", "TINUUM SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FormMarketIncome frmCollection = new FormMarketIncome();
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
            FormMarketIncome frmCollection = new FormMarketIncome();
            frmCollection.Show(this);

            this.Enabled = false;
        }
    }
}

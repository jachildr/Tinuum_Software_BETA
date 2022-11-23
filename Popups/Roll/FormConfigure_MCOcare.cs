﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Tinuum_Software_BETA.Popups.Roll
{
    public partial class FormConfigure_MCOcare : Tinuum_Software_BETA.Popups.Roll.FormConfigure_Medicaid
    {
        public FormConfigure_MCOcare()
        {
            InitializeComponent();
            tbl_Prefix = "dtbRollMCOcare";
            tbl_Variant = "dtbRollConfigureMCOcareRate";
            tbl_DtlPrefix = "dtbRollDetailDynamic_MCOcareRate";
            tbl_DynPrefix = "dtbRollDynamic_MCOcareRate";
            tbl_ValPrefix = "dtbRoll_MCOcareRate";
        }
        public override void btnEdit_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0)
            {
                MessageBox.Show("You must add a record or select a valid entry", "TINUUM SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FormRollMCOcare frmCollection = new FormRollMCOcare();
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
            FormRollMCOcare frmCollection = new FormRollMCOcare();
            frmCollection.Show(this);

            this.Enabled = false;
        }
    }
}

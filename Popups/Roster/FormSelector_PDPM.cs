using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Tinuum_Software_BETA.Popups.Roster
{
    public partial class FormSelector_PDPM : Tinuum_Software_BETA.Popups.Expense.FormSelector_PDPM
    {
        public FormSelector_PDPM()
        {
            InitializeComponent();
            tbl_Input_Prefix = "dtbRosterSelector_PDPM_Input";
            tbl_Output_Prefix = "dtbRosterSelector_PDPM_Output";
            tbl_Main = "dtbRosterSelector_PDPM_Main";
            tbl_Active = "dtbRosterConfigurePDPM";
        }
        public override void Delegate()
        {
            SQLQueries.tblRosterPDPMCreate();
        }

        public override void btnSubmit_Click(object sender, EventArgs e)
        {
            int rowNum;
            int i;
            int counter = 0;
            string title = "TINUUM SOFTWARE";
            int components = 0;
            int hit1 = 0;
            int hit2 = 0;
            int hit3 = 0;
            int hit4 = 0;
            int hit5 = 0;

            // ENSURE NAME FIELD NOT BLANK
            if (configName.Text == null || configName.Text == "")
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
            // ENSURE AT LEAST ONE ENTRY
            if (listBox_Output.Items.Count == 0)
            {
                MessageBox.Show("You must select at least one output item. Retry.", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // SELECTIONS ALREADY IN OUTPUT TABLE
            List<int> arrOutput = new List<int>();
            foreach (var item in listBox_Output.Items)
            {
                var val = (item as DataRowView)["Prime"].ToString();
                arrOutput.Add(Convert.ToInt32(val));
            }

            for (i = 0; i <= listBox_Output.Items.Count - 1; i++)
            {
                int Expression = arrOutput[i];
                switch (Expression)
                {
                    case object _ when 1 <= Expression && Expression <= 16:
                        {
                            
                            if (hit1 > 0) continue;
                            hit1 += 1;
                            components += 1;
                            continue;
                        }
                    case object _ when 17 <= Expression && Expression <= 32:
                        {
                            if (hit2 > 0) continue;
                            hit2 += 1;
                            components += 1;
                            continue;
                        }
                    case object _ when 33 <= Expression && Expression <= 44:
                        {
                            if (hit3 > 0) continue;
                            hit3 += 1;
                            components += 1;
                            continue;
                        }
                    case object _ when 45 <= Expression && Expression <= 50:
                        {
                            if (hit4 > 0) continue;
                            hit4 += 1;
                            components += 1;
                            continue;
                        }
                    case object _ when 51 <= Expression && Expression <= 75:
                        {
                            if (hit5 > 0) continue;
                            hit5 += 1;
                            components += 1;
                            continue;
                        }
                }
            }
            // ENSURE AT LEAST ONE ENTRY FOR EACH COMPONENT
            if (components < 5)
            {
                MessageBox.Show("You must select at least one output item from each case-mix component. Retry.", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // CALL UPDATE
            update_active();

            frm.Enabled = true;
            this.Dispose();
        }
    }
}

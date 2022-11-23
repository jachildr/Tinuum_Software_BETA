using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace Tinuum_Software_BETA
{
    public partial class FormGeneral : Form
    {
        int terminate = 0;
        public FormGeneral()
        {
            InitializeComponent();
        }

        public SQLControl SQL = new SQLControl();

        [CLSCompliant(true)]
        private void frmGeneral_Load(object sender, EventArgs e)
        {
            
            
            string strNum;
            double intNum;

            terminate = 1;

            ctrl3.Items.Add("RIDEA");
            ctrl3.Items.Add("Operation");
            ctrl3.Items.Add("Real Estate");

            ctrl6.Items.Add("Urban");
            ctrl6.Items.Add("Rural");

            ctrl14.Items.Add("Analysis End");
            ctrl14.Items.Add("Custom");

            ctrl12.Items.Add("Analysis Start");
            ctrl12.Items.Add("Custom");

            SQL.ExecQuery("SELECT * FROM dtbHomeStates;");
            ctrl7.DataSource = SQL.DBDT;
            ctrl7.DisplayMember = "States";

            SQL.ExecQuery("SELECT * FROM dtbHome;");

            try
            {
                ctrl1.Text = Convert.ToString(SQL.DBDT.Rows[0][1]);
                ctrl2.Text = Convert.ToString(SQL.DBDT.Rows[0][2]);
                ctrl3.Text = Convert.ToString(SQL.DBDT.Rows[0][3]);
                ctrl4.Text = Convert.ToString(SQL.DBDT.Rows[0][4]);
                ctrl5.Text = Convert.ToString(SQL.DBDT.Rows[0][5]);
                ctrl6.Text = Convert.ToString(SQL.DBDT.Rows[0][6]);
                ctrl7.Text = Convert.ToString(SQL.DBDT.Rows[0][7]);
                ctrl8.Text = Convert.ToString(SQL.DBDT.Rows[0][8]);
                ctrl9.Text = Convert.ToString(SQL.DBDT.Rows[0][9]);
                ctrl10.Value = Convert.ToDateTime(SQL.DBDT.Rows[0][10]);
                ctrl11.Value = Convert.ToDecimal(SQL.DBDT.Rows[0][11]);
                ctrl12.Text = Convert.ToString(SQL.DBDT.Rows[0][12]);
                ctrl13.Value = Convert.ToDateTime(SQL.DBDT.Rows[0][13]);
                ctrl14.Text = Convert.ToString(SQL.DBDT.Rows[0][14]);
                ctrl15.Value = Convert.ToDateTime(SQL.DBDT.Rows[0][15]);
                ctrl16.Text = Convert.ToString(SQL.DBDT.Rows[0][16]);
            }
            catch (Exception ex)
            {
            }

            strNum = ctrl4.Text;
            if (Information.IsNumeric(strNum) == true)
            {
                intNum = Convert.ToDouble(strNum);
                ctrl4.Text = String.Format("{0:p}", intNum);
            }

            ctrl10.Format = DateTimePickerFormat.Custom;
            ctrl10.CustomFormat = "MMM yyyy";

            ctrl13.Format = DateTimePickerFormat.Custom;
            ctrl13.CustomFormat = "MMM yyyy";

            ctrl15.Format = DateTimePickerFormat.Custom;
            ctrl15.CustomFormat = "MMM yyyy";

            if (SQL.DBDT.Rows[0][12].ToString() == "Analysis Start") ctrl13.Value = ctrl10.Value;
            if (SQL.DBDT.Rows[0][14].ToString() == "Analysis End") ctrl15.Value = ctrl10.Value.AddYears(Convert.ToInt32(ctrl11.Value));

            if (Information.IsNumeric(SQL.DBDT.Rows[0][11]))
            {
                ctrl11.Enabled = false;
            }
            else
            {
                ctrl11.Enabled = true;
            }

            if (Information.IsDate(SQL.DBDT.Rows[0][13]))
            {
                ctrl13.Enabled = true;
            }
            else
            {
                ctrl13.Enabled = false;
            }

            if (Information.IsDate(SQL.DBDT.Rows[0][15]))
            {
                ctrl15.Enabled = true;
            }
            else
            {
                ctrl15.Enabled = false;
            }
            if (Information.IsDBNull(SQL.DBDT.Rows[0][6]))
            {
                ctrl6.Enabled = true;
            }
            else
            {
                ctrl6.Enabled = false;
            }
            if (Information.IsDBNull(SQL.DBDT.Rows[0][16]))
            {
                ctrl16.ReadOnly = true;
            }
            else
            {
                ctrl16.ReadOnly = false;
            }

            ctrl14.Text = Convert.ToString(SQL.DBDT.Rows[0][14]);
            ctrl16.Text = Convert.ToString(SQL.DBDT.Rows[0][16]);
            terminate = 0;
        }
        
        private void cmbState_SelectedIndexChanged(object sender, EventArgs e)
        {   
            string state = "'" + ctrl7.Text.Trim() + "'";

            SQL.ExecQuery("SELECT * FROM dtbHomeWageIndex_Urban WHERE State = "+ state +";");
            ctrl8.DataSource = SQL.DBDT;
            ctrl8.DisplayMember = "Constituent Counties";

            if (terminate > 0)
            {
                SQL.ExecQuery("SELECT * FROM dtbHome;");
                ctrl7.Text = Convert.ToString(SQL.DBDT.Rows[0][7]);
            }
            else
            {
                return;
            }
        }

        private void cmbCounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            string state = "'" + ctrl7.Text.Trim() + "'";
            string county = "'" + ctrl8.Text.Trim() + "'";

            SQL.ExecQuery("SELECT * FROM dtbHomeWageIndex_Urban WHERE State = " + state + " AND [Constituent Counties] = " + county + ";");
            
            ctrl9.Text = Convert.ToString(SQL.DBDT.Rows[0][3]);

            if (terminate > 0)
            {
                SQL.ExecQuery("SELECT * FROM dtbHome;");
                ctrl8.Text = Convert.ToString(SQL.DBDT.Rows[0][8]);
            }
            else
            {
                return;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ctrl12_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (terminate > 0) return;
            switch (ctrl12.SelectedIndex)
            {
                case 0:
                    {
                        ctrl13.Enabled = false;
                        ctrl13.Value = ctrl10.Value;
                    }
                    break;
                case 1:
                    {
                        ctrl13.Enabled = true;
                        ctrl13.Value = ctrl10.Value;
                    }
                    break;
            }
        }

        private void ctrl14_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (terminate > 0) return;
            switch (ctrl14.SelectedIndex)
            {
                case 0:
                    {
                        ctrl15.Enabled = false;
                        ctrl15.Value = ctrl10.Value.AddYears(Convert.ToInt32(ctrl11.Value));
                    }
                    break;
                case 1:
                    {
                        ctrl15.Enabled = true;
                        ctrl15.Value = ctrl10.Value.AddYears(Convert.ToInt32(ctrl11.Value));
                    }
                    break;
            }
        }

        private void ctrl13_ValueChanged(object sender, EventArgs e)
        {
            string title = "TINUUM SOFTWARE";

            DateTime date1 = Convert.ToDateTime(ctrl10.Value);
            DateTime date2 = Convert.ToDateTime(ctrl13.Value);
            int result = DateTime.Compare(date1, date2);

            if (result > 0)
            {
                MessageBox.Show("Retry. Report start must be greater than or equal to analysis start.", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrl13.Value = ctrl10.Value;
            }
        }

        private void ctrl15_ValueChanged(object sender, EventArgs e)
        {
            string title = "TINUUM SOFTWARE";

            DateTime date1 = Convert.ToDateTime(ctrl13.Value);
            DateTime date2 = Convert.ToDateTime(ctrl15.Value);
            int result = DateTime.Compare(date1, date2);

            if (result > 0)
            {
                MessageBox.Show("Retry. Report start must be greater than or equal to analysis start.", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrl15.Value = ctrl10.Value.AddYears(Convert.ToInt32(ctrl11.Value));
            }
        }

        private void ctrl4_Leave(object sender, EventArgs e)
        {
            string strNum;
            double intNum;
            
            strNum = ctrl4.Text;
            if (Information.IsNumeric(strNum) == true)
            {
                if (Convert.ToDouble(strNum) <= 1)
                {
                    intNum = Convert.ToDouble(strNum);
                    ctrl4.Text = String.Format("{0:p}", intNum);
                }
                else
                {
                    MessageBox.Show("Your entry must be less than 100%.", "TINUUM SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ctrl4.Text = "";
                    ctrl4.Select();
                }
            }
            else
            {
                MessageBox.Show("You must enter a numeric value.", "TINUUM SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrl4.Text = "";
                ctrl4.Select();
            }
            
        }

        private void ctrl16_Click(object sender, EventArgs e)
        {
            ctrl16.ReadOnly = false;
        }

        private void ctrl16_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ctrl16.Text))
            {
                ctrl16.ReadOnly = true;
            }
        }

        private void btnSubmit_Click_1(object sender, EventArgs e)
        {
            int num = 1;
            int count = 16;
            int i;
            List<string> Items = new List<string>();

            foreach (Control ctrl in panel1.Controls)
            {
                int Expression = ctrl.TabIndex;
                switch (Expression)
                {
                    case object _ when 1 <= Expression && Expression <= 16:
                        {
                            if (ctrl.TabIndex == 16) continue;
                            if (ctrl.Enabled == false) continue;
                            if (string.IsNullOrEmpty(ctrl.Text))
                            {
                                MessageBox.Show("You must enter valid data before continuing.", "TINUUM SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                ctrl.Focus();
                                return;
                            }
                        }
                        break;
                }
            }

            foreach (Control ctrl in panel1.Controls)
            {
                //int Expression = ctrl.TabIndex;
                //switch (Expression)
                //{
                //    case object _ when 1 <= Expression && Expression <= 16:
                //        {
                //            switch (ctrl.TabIndex)
                //            {
                //                case 13:
                //                    {
                //                        if (ctrl12.Text == "Custom")
                //                        {
                //                            Items.Add(ctrl.Text);
                //                        }
                //                        else
                //                        {
                //                            Items.Add(DBNull.Value.ToString());
                //                        }
                                        
                //                    }
                //                    break;
                //                case 15:
                //                    {
                //                        if (ctrl14.Text == "Custom")
                //                        {
                //                            Items.Add(ctrl.Text);
                //                        }
                //                        else
                //                        {
                //                            Items.Add(DBNull.Value.ToString());
                //                        }
                //                    }
                //                    break;
                //                case 4:
                //                    {
                //                        Items.Add(myMethods.ToDecimal(ctrl.Text).ToString());   
                //                    }
                //                    break;
                //                default:
                //                    {
                //                        Items.Add(ctrl.Text);
                //                    }
                //                    break;
                //            }
                //        }
                //        break;
                //}
            }

            Items.Add(ctrl1.Text);
            Items.Add(ctrl2.Text);
            Items.Add(ctrl3.Text);

            Items.Add(myMethods.ToDecimal(ctrl4.Text).ToString());

            Items.Add(ctrl5.Text);
            Items.Add(ctrl6.Text);
            Items.Add(ctrl7.Text);
            Items.Add(ctrl8.Text);
            Items.Add(ctrl9.Text);
            Items.Add(Convert.ToDateTime(ctrl10.Text).ToString());
            Items.Add(ctrl11.Text);
            Items.Add(ctrl12.Text);

            if (ctrl12.Text == "Custom")
            {
                Items.Add(Convert.ToDateTime(ctrl13.Text).ToString());
            }
            else
            {
                Items.Add(DBNull.Value.ToString());
            }

            Items.Add(ctrl14.Text);

            if (ctrl14.Text == "Custom")
            {
                Items.Add(Convert.ToDateTime(ctrl15.Text).ToString());
            }
            else
            {
                Items.Add(DBNull.Value.ToString());
            }

            Items.Add(ctrl16.Text);

            for (i = 1; i <= count; i++)
            {
                string col = "ctrl" + i;
                SQL.AddParam("@PrimKey", num);
                SQL.AddParam("@val", Items[i - 1]);
                string cmdUpdate = "UPDATE dtbHome SET " + col + "=@val WHERE num_key=@PrimKey;";
                SQL.ExecQuery(cmdUpdate);
            }

            if (SQL.HasException(true))
            {
                return;
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

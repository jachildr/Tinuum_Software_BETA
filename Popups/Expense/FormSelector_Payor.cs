using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tinuum_Software_BETA.Popups.Expense
{
    public partial class FormSelector_Payor : Form
    {
        protected SQLControl SQL_Main = new SQLControl();
        protected SQLControl SQL_Input = new SQLControl();
        protected SQLControl SQL_Output = new SQLControl();
        protected SQLControl SQL_Variable = new SQLControl();
        protected SQLControl SQL_Active = new SQLControl();
        protected string tbl_Input_Prefix = "dtbExpenseSelector_Payor_Input";
        protected string tbl_Output_Prefix = "dtbExpenseSelector_Payor_Output";
        protected string tbl_Main = "dtbExpenseSelector_Payor_Main";
        protected string tbl_Input;
        protected string tbl_Output;
        protected List<string> items_first = new List<string>();
        protected List<int> prime = new List<int>();
        protected string Cncl = null;
        protected string tbl_Active = "dtbExpenseConfigurePayor";
        protected string slctCol = "collection_groups";
        protected string keyCol = "Prime";
        protected Control actCtrl;
        protected ListBox lstBox; // CHANGE FORM NUM
        protected Form frm;
        protected DataRowView drv;
        protected int primeKey;
        protected int lstIndex;

        public FormSelector_Payor()
        {
            InitializeComponent();
        }
        public void FormSelector_Payor_Load(object sender, EventArgs e)
        {
            Loader();
        }

        public void Loader()
        {
            int i;
            int count;

            actCtrl = Application.OpenForms[2].ActiveControl; // CHANGE FORM NUM
            lstBox = Application.OpenForms[2].Controls["listBox1"] as ListBox;
            frm = Application.OpenForms[2];

            // CREATE NEW TABLE IF ADD
            if (actCtrl.Name == "btnAdd")
            {
                Delegate();
            }

            // QUERY TO GET TABLE FOR LATER METHODS
            SQL_Active.ExecQuery("SELECT * FROM " + tbl_Active + ";");
            // FIND PRIME KEY TO SELECTT TABLE
            lstIndex = lstBox.SelectedIndex;
            count = lstBox.Items.Count - 1;

            if (lstBox.SelectedIndex < 0)
            {
                drv = (DataRowView)lstBox.Items[count];
                primeKey = Convert.ToInt32(drv[keyCol]);
            }
            else
            {
                drv = (DataRowView)lstBox.Items[lstIndex];
                primeKey = Convert.ToInt32(drv[keyCol]);
            }

            // GET TABLE AND SELECT, ADD PRIMEKEY
            tbl_Input = tbl_Input_Prefix + primeKey;
            tbl_Output = tbl_Output_Prefix + primeKey;

            // CLEAR FIELDS
            listBox_Input.DataSource = null;
            listBox_Output.DataSource = null;
            items_first.Clear();
            prime.Clear();

            // LOAD TABLES
            SQL_Main.ExecQuery("SELECT * FROM " + tbl_Main + ";");
            SQL_Input.ExecQuery("SELECT * FROM " + tbl_Input + ";");
            SQL_Output.ExecQuery("SELECT * FROM " + tbl_Output + ";");

            // LOAD ARRAYS
            for (i = 0; i <= SQL_Main.RecordCount - 1; i++)
            {
                items_first.Add("'" + SQL_Main.DBDT.Rows[i][1].ToString() + "'");
            }
            for (i = 0; i <= SQL_Main.RecordCount - 1; i++)
            {
                prime.Add(Convert.ToInt32(SQL_Main.DBDT.Rows[i][0]));
            }

            // ADD DATA SOURCE
            if (SQL_Input.RecordCount == 0 && SQL_Output.RecordCount == 0)
            {
                listBox_Input.DataSource = SQL_Main.DBDT;
            }
            else
            {
                if (configName.Text == "")
                {
                    configName.Text = lstBox.Text;
                }
                listBox_Input.DataSource = SQL_Input.DBDT;
                listBox_Output.DataSource = SQL_Output.DBDT;
            }
            
            listBox_Input.DisplayMember = "Item1";
            listBox_Output.DisplayMember = "Item1";
            {
                listBox_Input.ValueMember = "Prime";
                listBox_Output.ValueMember = "Prime";
            }

            listBox_Input.SelectionMode = SelectionMode.MultiSimple;
            listBox_Output.SelectionMode = SelectionMode.MultiSimple;
            listBox_Input.SelectedIndex = -1;
            listBox_Output.SelectedIndex = -1;
        }

        private void btnAll_Right_Click(object sender, EventArgs e)
        {
            int i;
            SQL_Output.ExecQuery("DELETE FROM " + tbl_Input + ";");
            SQL_Output.ExecQuery("DELETE FROM " + tbl_Output + ";");
            for (i = 0; i <= items_first.Count - 1; i++)
            {
                string cmdInsert = "INSERT INTO " + tbl_Output + " (Prime, Item1) VALUES (" + prime[i] + ", " + items_first[i] + ");";
                SQL_Output.ExecQuery(cmdInsert);
            }
            Loader();
        }

        private void btnAll_Left_Click(object sender, EventArgs e)
        {
            int i;
            SQL_Input.ExecQuery("DELETE FROM " + tbl_Input + ";");
            SQL_Input.ExecQuery("DELETE FROM " + tbl_Output + ";");
            for (i = 0; i <= items_first.Count - 1; i++)
            {
                string cmdInsert = "INSERT INTO " + tbl_Input + " (Prime, Item1) VALUES (" + prime[i] + ", " + items_first[i] + ");";
                SQL_Input.ExecQuery(cmdInsert);
            }
            Loader();
        }

        private void output_submit()
        {
            int i;
            int j;
            if (listBox_Input.SelectedIndex == -1) return;

            SQL_Output.ExecQuery("DELETE FROM " + tbl_Input + ";");
            SQL_Output.ExecQuery("DELETE FROM " + tbl_Output + ";");
            // INPUT SELECTION ARRAY
            List<int> arrInput = new List<int>();
            foreach (var item in listBox_Input.SelectedItems)
            {
                var val = (item as DataRowView)["Prime"].ToString();
                arrInput.Add(Convert.ToInt32(val));  
            }
            // SELECTIONS ALREADY IN OUTPUT TABLE
            List<int> arrOutput = new List<int>();
            foreach (var item in listBox_Output.Items)
            {
                var val = (item as DataRowView)["Prime"].ToString();
                arrOutput.Add(Convert.ToInt32(val));
            }

            for (i = 0; i <= prime.Count - 1; i++)
            {
                // IF "i" IS CONTAINED IN SELECTED ARRAY THEN SUBMIT TO OUTPUT DBDT
                if (arrInput.Contains(i + 1))
                {
                    string cmdInsert = "INSERT INTO " + tbl_Output + " (Prime, Item1) VALUES (" + prime[i] + ", " + items_first[i] + ");";
                    SQL_Output.ExecQuery(cmdInsert);
                }
                // IF "i" IS CONTAINED IN EXISTING OUTPUT ARRAY THEN SUBMIT TO OUTPUT DBDT
                else if (arrOutput.Contains(i + 1))
                {
                    string cmdInsert = "INSERT INTO " + tbl_Output + " (Prime, Item1) VALUES (" + prime[i] + ", " + items_first[i] + ");";
                    SQL_Output.ExecQuery(cmdInsert);
                }
                // OTHERWISE IT IS HELD IN THE INPUT DBDT
                else
                {
                    string cmdInsert = "INSERT INTO " + tbl_Input + " (Prime, Item1) VALUES (" + prime[i] + ", " + items_first[i] + ");";
                    SQL_Input.ExecQuery(cmdInsert);
                }
            }
            Loader();
        }
        private void Input_submit()
        {
            int i;
            int j;
            if (listBox_Output.SelectedIndex == -1) return;

            SQL_Output.ExecQuery("DELETE FROM " + tbl_Input + ";");
            SQL_Output.ExecQuery("DELETE FROM " + tbl_Output + ";");
            // OUTPUT SELECTION ARRAY
            List<int> arrOutput = new List<int>();
            foreach (var item in listBox_Output.SelectedItems)
            {
                var val = (item as DataRowView)["Prime"].ToString();
                arrOutput.Add(Convert.ToInt32(val));
            }
            // SELECTIONS ALREADY IN INPUT TABLE
            List<int> arrInput = new List<int>();
            foreach (var item in listBox_Input.Items)
            {
                var val = (item as DataRowView)["Prime"].ToString();
                arrInput.Add(Convert.ToInt32(val));
            }

            for (i = 0; i <= prime.Count - 1; i++)
            {
                // IF "i" IS CONTAINED IN SELECTED ARRAY THEN SUBMIT TO INPUT DBDT
                if (arrOutput.Contains(i + 1))
                {
                    string cmdInsert = "INSERT INTO " + tbl_Input + " (Prime, Item1) VALUES (" + prime[i] + ", " + items_first[i] + ");";
                    SQL_Input.ExecQuery(cmdInsert);
                }
                // IF "i" IS CONTAINED IN EXISTING INPUT ARRAY THEN SUBMIT TO INPUT DBDT
                else if (arrInput.Contains(i + 1))
                {
                    string cmdInsert = "INSERT INTO " + tbl_Input + " (Prime, Item1) VALUES (" + prime[i] + ", " + items_first[i] + ");";
                    SQL_Input.ExecQuery(cmdInsert);
                }
                // OTHERWISE IT IS HELD IN THE OUTPUT DBDT
                else
                {
                    string cmdInsert = "INSERT INTO " + tbl_Output + " (Prime, Item1) VALUES (" + prime[i] + ", " + items_first[i] + ");";
                    SQL_Output.ExecQuery(cmdInsert);
                }
            }
            Loader();
        }

        public virtual void call_cancel()
        {
            string Title = "TINUUM SOFTWARE";

            DialogResult prompt = MessageBox.Show("Are you sure? Any unsaved data will be lost", Title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            Cncl = prompt.ToString();
            try
            {
                if (prompt == DialogResult.Yes)
                {
                    if (actCtrl.Name == "btnAdd")
                    {
                        // DROP TABLE
                        SQL_Variable.ExecQuery("DROP TABLE " + tbl_Input + ";");
                        SQL_Variable.ExecQuery("DROP TABLE " + tbl_Output + ";");

                        // DELETE ENTRY FROM TABLE
                        SQL_Variable.AddParam("@PrimeKey", primeKey);
                        SQL_Variable.ExecQuery("DELETE FROM " + tbl_Active + " WHERE Prime=@PrimeKey;");

                        // clean up
                        frm.Enabled = true;
                        this.Close();
                    }
                    else
                    {
                        frm.Enabled = true;
                        this.Close();
                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void btnSlct_Right_Click(object sender, EventArgs e)
        {
            output_submit();
        }

        private void btnSlct_Left_Click(object sender, EventArgs e)
        {
            Input_submit();
        }

        private void listBox_Input_Click(object sender, EventArgs e)
        {
            listBox_Output.SelectedIndex = -1;
        }

        private void listBox_Output_Click(object sender, EventArgs e)
        {
            listBox_Input.SelectedIndex = -1;
        }

        public virtual void btnSubmit_Click(object sender, EventArgs e)
        {
            int rowNum;
            int i;
            int counter = 0;
            string title = "TINUUM SOFTWARE";

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


            update_active();

            frm.Enabled = true;
            this.Dispose();
        }

        protected void update_active()
        {
            string cmdUpdate;

            // UPDATE ACTIVE TABLE WITH
            SQL_Active.AddParam("@PrimeKey", primeKey);
            SQL_Active.AddParam("@CaseName", configName.Text);
            cmdUpdate = "UPDATE " + tbl_Active + " SET " + slctCol + "=@CaseName WHERE Prime=@PrimeKey;";
            SQL_Active.ExecQuery(cmdUpdate);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            call_cancel();
        }
        public virtual void Delegate()
        {
            SQLQueries.tblExpensePayorCreate();
        }

        private void FormSelector_Payor_FormClosing(object sender, FormClosingEventArgs e)
        {
            var switchExpr = Cncl;
            switch (switchExpr)
            {
                case null:
                    {
                        call_cancel();
                        e.Cancel = true;
                        break;
                    }

                case "No":
                    {
                        e.Cancel = true;
                        break;
                    }

                case "Yes":
                    {
                        e.Cancel = false;
                        Dispose();
                        break;
                    }
            }

            Cncl = null;
        }
    }
}

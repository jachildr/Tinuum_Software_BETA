using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using Microsoft.VisualBasic;
using Tinuum_Software_BETA.Detail_Inherit.Roll;

namespace Tinuum_Software_BETA.Popups.Roll
{
    public partial class FormRollMedicaid : Tinuum_Software_BETA.Popups.Roll.FormRollWeight
    {
        protected string tbl_DynPrefix = "dtbRollDynamic_MedicaidRate";
        protected string tbl_DynDelete;
        protected string tbl_ValPrefix = "dtbRoll_MedicaidRate";
        protected string tbl_ValDelete;
        protected string tbl_Rural = "dtbHomePPSRates_Rural";
        protected string tbl_Urban = "dtbHomePPSRates_Urban";
        protected string tbl_DiemRural = "dtbHomeFedPerDiem_Rural";
        protected string tbl_DiemUrban = "dtbHomeFedPerDiem_Urban";
        public SQLControl SQL_DiemRates = new SQLControl();
        protected List<string> PPD_Rates = new List<string>();

        public FormRollMedicaid()
        {
            InitializeComponent();
            tbl_Prefix = "dtbRollMedicaid";
            tbl_dtlPrefix = "dtbRollDetailDynamic_MedicaidRate";
            tbl_Active = "dtbRollConfigureMedicaidRate";
        }

        public override void Add_Source()
        {
            base.Add_Source();
        }

        public override void LoadGrid()
        {
            myMethods.SQL_Grab();
            base.LoadGrid();
            int i;
            // MAKE LAST ROW READ ONLY

            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                if (new int[] { 0, 17, 34, 47, 54 }.Contains(i))
                {
                    dataGridView1.Rows[i].ReadOnly = true;
                    dataGridView1.Rows[i].DefaultCellStyle.SelectionBackColor = SystemColors.Control;
                    dataGridView1.Rows[i].DefaultCellStyle.SelectionForeColor = SystemColors.ControlDark;
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = SystemColors.Control;
                    dataGridView1.Rows[i].DefaultCellStyle.ForeColor = SystemColors.ControlDark;
                }
            }

            tbl_DynDelete = tbl_DynPrefix + primeKey;
            tbl_ValDelete = tbl_ValPrefix + primeKey;

            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].Width = 182;

            // FILL DGV FOR PPS RATES
            for (i = 0; i <= dataGridView1.RowCount - 2; i++) // MINUS 2 BECAUSE ADDED NON CASE MIX GROUP
            {
                if (dataGridView1.Rows[i].Cells[3].Value == DBNull.Value)
                {
                    if (myMethods.geo_area == "Urban")
                    {
                        SQL.ExecQuery("SELECT * FROM " + tbl_Urban + ";");
                    }
                    else
                    {
                        SQL.ExecQuery("SELECT * FROM " + tbl_Rural + ";");
                    }

                    if (new int[] { 0, 17, 34, 47, 54 }.Contains(i))
                    {
                        continue;
                    }
                    else
                    {
                        dataGridView1.Rows[i].Cells[3].Value = SQL.DBDT.Rows[i][2];
                    }
                }
            }

            // LAST ROW RATE - NON CASEMIX
            if (dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[3].Value == DBNull.Value)
            {
                if (myMethods.geo_area == "Urban")
                {
                    SQL_DiemRates.ExecQuery("SELECT * FROM " + tbl_DiemUrban + ";");
                }
                else
                {
                    SQL_DiemRates.ExecQuery("SELECT * FROM " + tbl_DiemRural + ";");
                }
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[3].Value = SQL_DiemRates.DBDT.Rows[0][5];
            }

            // ADD RATES TO ARRAY
            if (myMethods.geo_area == "Urban")
            {
                SQL.ExecQuery("SELECT * FROM " + tbl_Urban + ";");
                SQL_DiemRates.ExecQuery("SELECT * FROM " + tbl_DiemUrban + ";");
            }
            else
            {
                SQL.ExecQuery("SELECT * FROM " + tbl_Rural + ";");
                SQL_DiemRates.ExecQuery("SELECT * FROM " + tbl_DiemRural + ";");
            }

            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                switch (PPD_Rates.Count)
                {
                    case 80:
                        {
                            PPD_Rates.Add(SQL_DiemRates.DBDT.Rows[0][5].ToString());
                        }
                        break;
                    default:
                        {
                            PPD_Rates.Add(SQL.DBDT.Rows[i][2].ToString());
                        }
                        break;
                }
            }
        }
        public override void Delegate()
        {
            SQLQueries.tblRollMedicaidRateCreate();
        }

        public override void percent_change()
        {
            // NOTHING
        }
        public override void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 3:
                    {
                        if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                        {
                            return;
                        }
                        else if (!Information.IsNumeric(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()))
                        {
                            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;
                            return;
                        }
                    }
                    break;
            }
        }

        public override void UpdateSQL()
        {
            int i;
            int y;
            int cRight = 3;
            string btnString = "(b)";
            var commandBuilder = new System.Data.SqlClient.SqlCommandBuilder(SQL.DBDA);
            string cmdUpdate;
            int counter = default(int);
            string title = "TINUUM SOFTWARE";
            int j;

            // ENSURE NAME FIELD NOT BLANK
            if (configName.Text == null)
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

            if (dataGridView1.RowCount == 0)
            {
                // Nothing
            }
            else
            {
                for (y = 0; y <= dataGridView1.ColumnCount - 1; y++)
                {
                    for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                    {
                        if (new int[] { 0, 17, 34, 47, 54 }.Contains(i))
                        {
                            continue;
                        }
                        else
                        {
                            if (Header_Name[y].Substring(Header_Name[y].Length - cRight, cRight).Equals(btnString))
                            {
                                // Do Nothing
                            }
                            else if (dataGridView1.Rows[i].Cells[y].Value == null || dataGridView1.Rows[i].Cells[y].Value == DBNull.Value)
                            {
                                MessageBox.Show("You must enter relevant values. Retry.", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                dataGridView1.CurrentCell = dataGridView1.Rows[i].Cells[y];
                                return;
                            }
                        }
                        
                    }
                }
            }
            // FILL MAJOR TABLE WITH GRID
            for (y = 0; y <= dataGridView1.RowCount - 1; y++)
            {
                for (i = 1; i <= dataGridView1.ColumnCount - 1; i++)
                {
                    SQL.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                    if (Header_Name[i].Substring(Header_Name[i].Length - cRight, cRight).Equals(btnString))
                    {
                        SQL.AddParam("@vals", null);
                    }
                    else if (i == 3)
                    {
                        SQL.AddParam("@vals", dataGridView1.Rows[y].Cells[i].Value);   
                    }
                    else
                    {
                        SQL.AddParam("@vals", dataGridView1.Rows[y].Cells[i].Value);
                    }

                    cmdUpdate = "UPDATE " + tbl_Variable + " SET " + Headers_Submit[i] + "=@vals WHERE ID_Num=@PrimKey;";
                    SQL.ExecQuery(cmdUpdate);
                }
            }

            // FILL DETAIL TABLES FROM GRID
            for (y = 0; y <= dataGridView1.ColumnCount - 1; y++)
            {
                for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                {
                    if (dataGridView1.Rows[i].Cells[3].Value.ToString() == "Detail")
                    {
                        continue;
                    }
                    else
                    {
                        for (j = 1; j <= myMethods.Period * Mos_Const; j++)
                        {
                            string tbl_Col = "month" + j;
                            var dec_Val = dataGridView1.Rows[i].Cells[3].Value;
                            SQL.AddParam("@PrimKey", dataGridView1.Rows[i].Cells[0].Value.ToString());
                            SQL.AddParam("@months_data", dec_Val);
                            cmdUpdate = "UPDATE " + tbl_ValDelete + " SET " + tbl_Col + "=@months_data WHERE ID_Num=@PrimKey;";
                            SQL.ExecQuery(cmdUpdate);
                        }
                    }

                }
            }

            // UPDATE ACTIVE TABLE
            SQL_Active.AddParam("@PrimeKey", primeKey);
            SQL_Active.AddParam("@CaseName", configName.Text);
            cmdUpdate = "UPDATE " + tbl_Active + " SET " + slctCol + "=@CaseName WHERE Prime=@PrimeKey;";
            SQL_Active.ExecQuery(cmdUpdate);
            frm.Enabled = true;

            // SET DYNAMIC YEARLY TO NULL
            for (y = 0; y <= dataGridView1.RowCount - 1; y++)
            {
                SQL.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                SQL.AddParam("@val1", 1);
                SQL.AddParam("@val2", DBNull.Value);
                string colName1 = "Choose";
                string colName2 = "Selection";
                cmdUpdate = "UPDATE " + tbl_DynDelete + " SET " + colName1 + "=@val1, " + colName2 + "=@val2 WHERE ID_Num=@PrimKey;";
                SQL.ExecQuery(cmdUpdate);
            }

            this.Dispose();
        }

        public override void Cancel()
        {
            int i;
            int y;
            int j;
            string Title = "TINUUM SOFTWARE";
            int cRight = 3;
            string btnString = "(b)";
            string cmdUpdate;

            DialogResult prompt = MessageBox.Show("Are you sure? Any unsubmitted data will be lost.", Title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            Rslt_Cncl = prompt.ToString();

            if (prompt == DialogResult.Yes)
            {
                if (actCtrl.Name == "btnAdd")
                {
                    // DROP TABLES
                    SQL_Variable.ExecQuery("DROP TABLE " + tbl_Detail + ";");
                    SQL_Variable.ExecQuery("DROP TABLE " + tbl_DynDelete + ";");
                    SQL_Variable.ExecQuery("DROP TABLE " + tbl_ValDelete + ";");
                    SQL_Variable.ExecQuery("DROP TABLE " + tbl_Variable + ";"); // LAST BECAUSE FOREIGN KEYS ATTACHED 

                    // DELETE ENTRY FROM TABLE
                    SQL_Variable.AddParam("@PrimeKey", primeKey);
                    SQL_Variable.ExecQuery("DELETE FROM " + tbl_Active + " WHERE Prime=@PrimeKey;");

                    // clean up
                    frm.Enabled = true;
                    this.Close();
                }
                else if (dataGridView1.RowCount != 0)
                {
                    // CLEAR GRID AND RESET WITH ORIGINAL TABLE
                    SQL.ExecQuery("SELECT * FROM " + tbl_Variable + ";");
                    dataGridView1.Rows.Clear();
                    dataGridView1.Columns.Clear();
                    dataGridView1.DataSource = SQL.DBDT;

                    // FILL TABLES FROM GRID
                    for (y = 0; y <= dataGridView1.ColumnCount - 1; y++)
                    {
                        for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                        {
                            if (dataGridView1.Rows[i].Cells[3].Value.ToString() == "Detail")
                            {
                                continue;
                            }
                            else
                            {
                                for (j = 1; j <= myMethods.Period * Mos_Const; j++)
                                {
                                    string tbl_Col = "month" + j;
                                    var dec_Val = dataGridView1.Rows[i].Cells[3].Value;
                                    SQL.AddParam("@PrimKey", dataGridView1.Rows[i].Cells[0].Value.ToString());
                                    SQL.AddParam("@months_data", dec_Val);
                                    cmdUpdate = "UPDATE " + tbl_ValDelete + " SET " + tbl_Col + "=@months_data WHERE ID_Num=@PrimKey;";
                                    SQL.ExecQuery(cmdUpdate);
                                }
                            }

                        }
                        // SET DYNAMIC YEARLY TO NULL
                        SQL.AddParam("@PrimKey", dataGridView1.Rows[y].Cells[0].Value);
                        SQL.AddParam("@val1", 1);
                        SQL.AddParam("@val2", DBNull.Value);
                        string colName1 = "Choose";
                        string colName2 = "Selection";
                        cmdUpdate = "UPDATE " + tbl_DynDelete + " SET " + colName1 + "=@val1, " + colName2 + "=@val2 WHERE ID_Num=@PrimKey;";
                        SQL.ExecQuery(cmdUpdate);
                    }
                }
                else
                {
                    this.Close();
                    frm.Enabled = true;
                    return;
                }

            }
            else
            {
                return;
            }
            frm.Enabled = true;
            Close();
        }

        public override void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (terminate > 0) return;

            switch (e.ColumnIndex)
            {
                case 3:
                    {
                        if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value  == null || dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "")
                        {
                            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = PPD_Rates[e.RowIndex];
                        }
                    }
                    break;
            }
        }

        public override void DataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int Slct = dataGridView1.CurrentCell.RowIndex;

            DataGridView senderGrid = (DataGridView)sender;
            try
            {
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
                {
                    switch (e.ColumnIndex)
                    {
                        case 4:
                            {
                                if (new int[] { 0, 17, 34, 47, 54 }.Contains(e.RowIndex))
                                {
                                    return;
                                }
                                else
                                {
                                    dataGridView1.CurrentCell = dataGridView1.Rows[Slct].Cells[e.ColumnIndex - 1];
                                    dtlRoll_Dynamic frmDetail = new dtlRoll_Dynamic();
                                    frmDetail.Show(this);
                                    this.Enabled = false;
                                }
                                
                            }
                            break;
                        default:
                            break;
                    }

                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}

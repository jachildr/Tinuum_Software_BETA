using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tinuum_Software_BETA.Detail_Inherit.Roll;
using Syncfusion.Windows.Forms.Tools;

namespace Tinuum_Software_BETA.Popups.Roll
{
    public partial class FormRollMDS : Form
    {
        protected string tbl_Active = "dtbRollConfigureMDS";
        protected string tbl_Variable;
        protected string slctCol = "collection_groups";
        protected string keyCol = "Prime";
        protected string Cncl = null;
        
        protected SQLControl SQL_Variable = new SQLControl();
        protected SQLControl SQL_Active = new SQLControl();
        
        protected Control actCtrl;
        protected ListBox lstBox; // CHANGE FORM NUM
        protected DataGridView mainDGV; // CHANGE FORM NUM
        protected Form frm;
        protected DataRowView drv;
        protected int primeKey;
        protected int lstIndex;

        protected SQLControl SQL_Verse = new SQLControl();
        protected SQLControl SQL_Name = new SQLControl();
        protected int terminate;
        protected int exit;
        protected int chckCol;

        // FROM MASTER
        protected string Rslt_Cncl = null;
        protected SQLControl SQL = new SQLControl(); // CREATE NEW INSTANCE OF SQLCONTROL CLASS
        protected List<string> Headers_Submit = new List<string>();
        protected List<string> Header_Name = new List<string>();
        protected List<string> Header_Rename = new List<string>();
        protected int Col_Count;
        protected string tbl_Name;
        protected int Mos_Const = 12;
        protected int sldrCol = 1;
        protected int sldrMax;
        protected int loading;
        protected DataGridView dgv;
        protected int Counter;
        protected int sldrNum;
        protected int err = 0;

        protected static int index;
        public static int _index
        {
            get
            {
                return index;
            }
        }

        // TABLE VARIABLES
        protected string tbl_BIMS = "dtbRollMDS_BIMS";
        protected string tbl_Function = "dtbRollMDS_FunctionScore";
        protected string tbl_Clinical = "dtbRollMDS_Clinical";
        protected string tbl_Morbid = "dtbRollMDS_SLPMorbid";
        protected string tbl_Disorder = "dtbRollMDS_SLPDisorders";
        protected string tbl_NTA = "dtbRollMDS_NTA";
        protected string tbl_Extensive = "dtbRollMDS_Extensive";
        protected string tbl_Depression = "dtbRollMDS_Depression";
        protected string tbl_SCH = "dtbRollMDS_SCH";
        protected string tbl_SCL = "dtbRollMDS_SCL";
        protected string tbl_Complex = "dtbRollMDS_Complex";
        protected string tbl_Behavioral = "dtbRollMDS_Behavioral";
        protected string tbl_Restorative = "dtbRollMDS_Restorative";
        protected string dlt_tbl_BIMS;
        protected string dlt_tbl_FunctionScore;
        protected string dlt_tbl_Clinical;
        protected string dlt_tbl_Morbid;
        protected string dlt_tbl_Disorder;
        protected string dlt_tbl_NTA;
        protected string dlt_tbl_Extensive;
        protected string dlt_tbl_Depression;
        protected string dlt_tbl_SCH;
        protected string dlt_tbl_SCL;
        protected string dlt_tbl_Complex;
        protected string dlt_tbl_Behavioral;
        protected string dlt_tbl_Restorative;

        public FormRollMDS()
        {
            InitializeComponent();
        }

        public virtual void Load_Process()
        {
            int z;
            int tabs = 15;
            string name = "sldr";

            for (z = 1; z <= tabs; z++)
            {
                switch (z)
                {
                    case 1:
                        {
                            tbl_Name = tbl_BIMS + primeKey;
                            dgv = dataGridView1;
                            Counter = z;
                            Add_Source();
                            Loader();
                            Dynamic_CTRLs();
                            RangeSlider sldrCtrl = (RangeSlider)dgv.Controls[name + z];
                            try
                            {
                                sldrCtrl.SliderMax = Convert.ToInt32(dgv.Rows[0].Cells[dgv.ColumnCount - 2].Value);
                                sldrCtrl.SliderMin = Convert.ToInt32(dgv.Rows[0].Cells[dgv.ColumnCount - 3].Value);
                            }
                            catch (Exception ex)
                            {
                                sldrCtrl.SliderMax = 0;
                                sldrCtrl.SliderMin = 0;
                            }
                            // SLIDER RANGE TEXT
                            if (sldrCtrl.SliderMin == 0 && sldrCtrl.SliderMax == 0)
                            {
                                dgv.Rows[0].Cells[dgv.ColumnCount - 1].Value = null;
                            }
                            else
                            {
                                dgv.Rows[0].Cells[dgv.ColumnCount - 1].Value = string.Format("{0:N0}", sldrCtrl.SliderMin) + " - " + string.Format("{0:N0}", sldrCtrl.SliderMax);
                            }
                        }
                        break;
                    case 2: // DIFFERENT CASE
                        {
                            tbl_Name = tbl_Function + primeKey;
                            dgv = dataGridView2;
                            Counter = z;
                            FuntionScore_Loader();
                            Score_Dynamic_CTRLs();
                            // RANGE SLIDER FILL
                            Function_sldr_Fill();
                        }
                        break;
                    case 3:
                        {
                            tbl_Name = tbl_Clinical + primeKey;
                            dgv = dataGridView3;
                            Counter = z;
                            Add_Source();
                            Loader();
                            Dynamic_CTRLs();
                            RangeSlider sldrCtrl = (RangeSlider)dgv.Controls[name + z];
                            try
                            {
                                sldrCtrl.SliderMax = Convert.ToInt32(dgv.Rows[0].Cells[dgv.ColumnCount - 2].Value);
                                sldrCtrl.SliderMin = Convert.ToInt32(dgv.Rows[0].Cells[dgv.ColumnCount - 3].Value);
                            }
                            catch (Exception ex)
                            {
                                sldrCtrl.SliderMax = 0;
                                sldrCtrl.SliderMin = 0;
                            }
                            // SLIDER RANGE TEXT
                            if (sldrCtrl.SliderMin == 0 && sldrCtrl.SliderMax == 0)
                            {
                                dgv.Rows[0].Cells[dgv.ColumnCount - 1].Value = null;
                            }
                            else
                            {
                                dgv.Rows[0].Cells[dgv.ColumnCount - 1].Value = string.Format("{0:N0}", sldrCtrl.SliderMin) + " - " + string.Format("{0:N0}", sldrCtrl.SliderMax);
                            }
                        }
                        break;
                    case 12:
                        {
                            tbl_Name = tbl_Morbid + primeKey;
                            dgv = dataGridView12;
                            Counter = z;
                            Add_Source();
                            Loader();
                            Dynamic_CTRLs();
                            RangeSlider sldrCtrl = (RangeSlider)dgv.Controls[name + z];
                            try
                            {
                                sldrCtrl.SliderMax = Convert.ToInt32(dgv.Rows[0].Cells[dgv.ColumnCount - 2].Value);
                                sldrCtrl.SliderMin = Convert.ToInt32(dgv.Rows[0].Cells[dgv.ColumnCount - 3].Value);
                            }
                            catch (Exception ex)
                            {
                                sldrCtrl.SliderMax = 0;
                                sldrCtrl.SliderMin = 0;
                            }
                            // SLIDER RANGE TEXT
                            if (sldrCtrl.SliderMin == 0 && sldrCtrl.SliderMax == 0)
                            {
                                dgv.Rows[0].Cells[dgv.ColumnCount - 1].Value = null;
                            }
                            else
                            {
                                dgv.Rows[0].Cells[dgv.ColumnCount - 1].Value = string.Format("{0:N0}", sldrCtrl.SliderMin) + " - " + string.Format("{0:N0}", sldrCtrl.SliderMax);
                            }
                        }
                        break;
                    case 13:
                        {
                            tbl_Name = tbl_Disorder + primeKey;
                            dgv = dataGridView13;
                            Counter = z;
                            Add_Source();
                            Loader();
                            Dynamic_CTRLs();
                            RangeSlider sldrCtrl = (RangeSlider)dgv.Controls[name + z];
                            try
                            {
                                sldrCtrl.SliderMax = Convert.ToInt32(dgv.Rows[0].Cells[dgv.ColumnCount - 2].Value);
                                sldrCtrl.SliderMin = Convert.ToInt32(dgv.Rows[0].Cells[dgv.ColumnCount - 3].Value);
                            }
                            catch (Exception ex)
                            {
                                sldrCtrl.SliderMax = 0;
                                sldrCtrl.SliderMin = 0;
                            }
                            // SLIDER RANGE TEXT
                            if (sldrCtrl.SliderMin == 0 && sldrCtrl.SliderMax == 0)
                            {
                                dgv.Rows[0].Cells[dgv.ColumnCount - 1].Value = null;
                            }
                            else
                            {
                                dgv.Rows[0].Cells[dgv.ColumnCount - 1].Value = string.Format("{0:N0}", sldrCtrl.SliderMin) + " - " + string.Format("{0:N0}", sldrCtrl.SliderMax);
                            }
                        }
                        break;
                    case 5:
                        {
                            tbl_Name = tbl_NTA + primeKey;
                            dgv = dataGridView5;
                            Counter = z;
                            Add_Source();
                            Loader();
                            Dynamic_CTRLs();
                            RangeSlider sldrCtrl = (RangeSlider)dgv.Controls[name + z];
                            try
                            {
                                sldrCtrl.SliderMax = Convert.ToInt32(dgv.Rows[0].Cells[dgv.ColumnCount - 2].Value);
                                sldrCtrl.SliderMin = Convert.ToInt32(dgv.Rows[0].Cells[dgv.ColumnCount - 3].Value);
                            }
                            catch (Exception ex)
                            {
                                sldrCtrl.SliderMax = 0;
                                sldrCtrl.SliderMin = 0;
                            }
                            // SLIDER RANGE TEXT
                            if (sldrCtrl.SliderMin == 0 && sldrCtrl.SliderMax == 0)
                            {
                                dgv.Rows[0].Cells[dgv.ColumnCount - 1].Value = null;
                            }
                            else
                            {
                                dgv.Rows[0].Cells[dgv.ColumnCount - 1].Value = string.Format("{0:N0}", sldrCtrl.SliderMin) + " - " + string.Format("{0:N0}", sldrCtrl.SliderMax);
                            }
                        }
                        break;
                    case 6:
                        {
                            tbl_Name = tbl_Extensive + primeKey;
                            dgv = dataGridView6;
                            Counter = z;
                            Add_Source();
                            Loader();
                            Dynamic_CTRLs();
                            RangeSlider sldrCtrl = (RangeSlider)dgv.Controls[name + z];
                            try
                            {
                                sldrCtrl.SliderMax = Convert.ToInt32(dgv.Rows[0].Cells[dgv.ColumnCount - 2].Value);
                                sldrCtrl.SliderMin = Convert.ToInt32(dgv.Rows[0].Cells[dgv.ColumnCount - 3].Value);
                            }
                            catch (Exception ex)
                            {
                                sldrCtrl.SliderMax = 0;
                                sldrCtrl.SliderMin = 0;
                            }
                            // SLIDER RANGE TEXT
                            if (sldrCtrl.SliderMin == 0 && sldrCtrl.SliderMax == 0)
                            {
                                dgv.Rows[0].Cells[dgv.ColumnCount - 1].Value = null;
                            }
                            else
                            {
                                dgv.Rows[0].Cells[dgv.ColumnCount - 1].Value = string.Format("{0:N0}", sldrCtrl.SliderMin) + " - " + string.Format("{0:N0}", sldrCtrl.SliderMax);
                            }
                        }
                        break;
                    case 7:
                        {
                            tbl_Name = tbl_Depression + primeKey;
                            dgv = dataGridView7;
                            Counter = z;
                            Add_Source();
                            Loader();
                            Dynamic_CTRLs();
                            RangeSlider sldrCtrl = (RangeSlider)dgv.Controls[name + z];
                            try
                            {
                                sldrCtrl.SliderMax = Convert.ToInt32(dgv.Rows[0].Cells[dgv.ColumnCount - 2].Value);
                                sldrCtrl.SliderMin = Convert.ToInt32(dgv.Rows[0].Cells[dgv.ColumnCount - 3].Value);
                            }
                            catch (Exception ex)
                            {
                                sldrCtrl.SliderMax = 0;
                                sldrCtrl.SliderMin = 0;
                            }
                            // SLIDER RANGE TEXT
                            if (sldrCtrl.SliderMin == 0 && sldrCtrl.SliderMax == 0)
                            {
                                dgv.Rows[0].Cells[dgv.ColumnCount - 1].Value = null;
                            }
                            else
                            {
                                dgv.Rows[0].Cells[dgv.ColumnCount - 1].Value = string.Format("{0:N0}", sldrCtrl.SliderMin) + " - " + string.Format("{0:N0}", sldrCtrl.SliderMax);
                            }
                        }
                        break;
                    case 8:
                        {
                            tbl_Name = tbl_SCH + primeKey;
                            dgv = dataGridView8;
                            Counter = z;
                            Add_Source();
                            Loader();
                            Dynamic_CTRLs();
                            RangeSlider sldrCtrl = (RangeSlider)dgv.Controls[name + z];
                            try
                            {
                                sldrCtrl.SliderMax = Convert.ToInt32(dgv.Rows[0].Cells[dgv.ColumnCount - 2].Value);
                                sldrCtrl.SliderMin = Convert.ToInt32(dgv.Rows[0].Cells[dgv.ColumnCount - 3].Value);
                            }
                            catch (Exception ex)
                            {
                                sldrCtrl.SliderMax = 0;
                                sldrCtrl.SliderMin = 0;
                            }
                            // SLIDER RANGE TEXT
                            if (sldrCtrl.SliderMin == 0 && sldrCtrl.SliderMax == 0)
                            {
                                dgv.Rows[0].Cells[dgv.ColumnCount - 1].Value = null;
                            }
                            else
                            {
                                dgv.Rows[0].Cells[dgv.ColumnCount - 1].Value = string.Format("{0:N0}", sldrCtrl.SliderMin) + " - " + string.Format("{0:N0}", sldrCtrl.SliderMax);
                            }
                        }
                        break;
                    case 9:
                        {
                            tbl_Name = tbl_SCL + primeKey;
                            dgv = dataGridView9;
                            Counter = z;
                            Add_Source();
                            Loader();
                            Dynamic_CTRLs();
                            RangeSlider sldrCtrl = (RangeSlider)dgv.Controls[name + z];
                            try
                            {
                                sldrCtrl.SliderMax = Convert.ToInt32(dgv.Rows[0].Cells[dgv.ColumnCount - 2].Value);
                                sldrCtrl.SliderMin = Convert.ToInt32(dgv.Rows[0].Cells[dgv.ColumnCount - 3].Value);
                            }
                            catch (Exception ex)
                            {
                                sldrCtrl.SliderMax = 0;
                                sldrCtrl.SliderMin = 0;
                            }
                            // SLIDER RANGE TEXT
                            if (sldrCtrl.SliderMin == 0 && sldrCtrl.SliderMax == 0)
                            {
                                dgv.Rows[0].Cells[dgv.ColumnCount - 1].Value = null;
                            }
                            else
                            {
                                dgv.Rows[0].Cells[dgv.ColumnCount - 1].Value = string.Format("{0:N0}", sldrCtrl.SliderMin) + " - " + string.Format("{0:N0}", sldrCtrl.SliderMax);
                            }
                        }
                        break;
                    case 10:
                        {
                            tbl_Name = tbl_Complex + primeKey;
                            dgv = dataGridView10;
                            Counter = z;
                            Add_Source();
                            Loader();
                            Dynamic_CTRLs();
                            RangeSlider sldrCtrl = (RangeSlider)dgv.Controls[name + z];
                            try
                            {
                                sldrCtrl.SliderMax = Convert.ToInt32(dgv.Rows[0].Cells[dgv.ColumnCount - 2].Value);
                                sldrCtrl.SliderMin = Convert.ToInt32(dgv.Rows[0].Cells[dgv.ColumnCount - 3].Value);
                            }
                            catch (Exception ex)
                            {
                                sldrCtrl.SliderMax = 0;
                                sldrCtrl.SliderMin = 0;
                            }
                            // SLIDER RANGE TEXT
                            if (sldrCtrl.SliderMin == 0 && sldrCtrl.SliderMax == 0)
                            {
                                dgv.Rows[0].Cells[dgv.ColumnCount - 1].Value = null;
                            }
                            else
                            {
                                dgv.Rows[0].Cells[dgv.ColumnCount - 1].Value = string.Format("{0:N0}", sldrCtrl.SliderMin) + " - " + string.Format("{0:N0}", sldrCtrl.SliderMax);
                            }
                        }
                        break;
                    case 14:
                        {
                            tbl_Name = tbl_Behavioral + primeKey;
                            dgv = dataGridView14;
                            Counter = z;
                            Add_Source();
                            Loader();
                            Dynamic_CTRLs();
                            RangeSlider sldrCtrl = (RangeSlider)dgv.Controls[name + z];
                            try
                            {
                                sldrCtrl.SliderMax = Convert.ToInt32(dgv.Rows[0].Cells[dgv.ColumnCount - 2].Value);
                                sldrCtrl.SliderMin = Convert.ToInt32(dgv.Rows[0].Cells[dgv.ColumnCount - 3].Value);
                            }
                            catch (Exception ex)
                            {
                                sldrCtrl.SliderMax = 0;
                                sldrCtrl.SliderMin = 0;
                            }
                            // SLIDER RANGE TEXT
                            if (sldrCtrl.SliderMin == 0 && sldrCtrl.SliderMax == 0)
                            {
                                dgv.Rows[0].Cells[dgv.ColumnCount - 1].Value = null;
                            }
                            else
                            {
                                dgv.Rows[0].Cells[dgv.ColumnCount - 1].Value = string.Format("{0:N0}", sldrCtrl.SliderMin) + " - " + string.Format("{0:N0}", sldrCtrl.SliderMax);
                            }
                        }
                        break;
                    case 15:
                        {
                            tbl_Name = tbl_Restorative + primeKey;
                            dgv = dataGridView15;
                            Counter = z;
                            Add_Source();
                            Loader();
                            Dynamic_CTRLs();
                            RangeSlider sldrCtrl = (RangeSlider)dgv.Controls[name + z];
                            try
                            {
                                sldrCtrl.SliderMax = Convert.ToInt32(dgv.Rows[0].Cells[dgv.ColumnCount - 2].Value);
                                sldrCtrl.SliderMin = Convert.ToInt32(dgv.Rows[0].Cells[dgv.ColumnCount - 3].Value);
                            }
                            catch (Exception ex)
                            {
                                sldrCtrl.SliderMax = 0;
                                sldrCtrl.SliderMin = 0;
                            }
                            // SLIDER RANGE TEXT
                            if (sldrCtrl.SliderMin == 0 && sldrCtrl.SliderMax == 0)
                            {
                                dgv.Rows[0].Cells[dgv.ColumnCount - 1].Value = null;
                            }
                            else
                            {
                                dgv.Rows[0].Cells[dgv.ColumnCount - 1].Value = string.Format("{0:N0}", sldrCtrl.SliderMin) + " - " + string.Format("{0:N0}", sldrCtrl.SliderMax);
                            }
                        }
                        break;
                    default:
                        {
                            continue;
                        }
                }
            }   
        }

        public virtual void Add_Source()
        {
            string btnString = "(b)";
            string cmbString = "(c)";
            string dteString = "(d)";
            int i;
            int z;

            // UNBIND DATA SOURCE AT BEGINNING FOR UPDATE COMMAND
            dgv.AllowUserToAddRows = false;
            dgv.Rows.Clear();
            dgv.Columns.Clear();
            dgv.ColumnCount = 0;
            dgv.RowCount = 0;
            dgv.Refresh();
            Headers_Submit.Clear();
            Header_Rename.Clear();
            Header_Name.Clear();

            // LINK DATA SOURCE TO GET COL NAMES
            SQL.ExecQuery("SELECT * FROM " + tbl_Name + ";");
            if (SQL.HasException(true))
                return;
            dgv.DataSource = SQL.DBDT;

            // FILL LIST FROM COLUMN HEADERS
            Col_Count = dgv.ColumnCount;

            for (i = 0; i <= Col_Count - 1; i++)
            {
                Headers_Submit.Add("[" + dgv.Columns[i].HeaderText + "]");
            }

            for (i = 0; i <= Col_Count - 1; i++)
            {
                Header_Name.Add(dgv.Columns[i].HeaderText);
            }


            for (i = 0; i <= Col_Count - 1; i++)
            {
                var switchExpr = Header_Name[i].Substring(Header_Name[i].Length - 3, 3);
                switch (switchExpr)
                {
                    case var @case when @case == btnString:
                        {
                            Header_Rename.Add("");
                            break;
                        }

                    case var case1 when case1 == cmbString:
                    case var case2 when case2 == dteString:
                        {
                            Header_Rename.Add(Header_Name[i].Substring(0, Header_Name[i].Length - 3));
                            break;
                        }

                    default:
                        {
                            Header_Rename.Add(Header_Name[i]);
                            break;
                        }
                }
            }
            // CLEAR DATA SOURCE 
            dgv.DataSource = null;
        }

        private void FormRollMDS_Load(object sender, EventArgs e)
        {
            loading = 1;
            {
                actCtrl = Application.OpenForms[4].ActiveControl; // CHANGE FORM NUM
                lstBox = Application.OpenForms[4].Controls["listBox1"] as ListBox;
                frm = Application.OpenForms[4];
                mainDGV = Application.OpenForms[3].Controls["dataGridView1"] as DataGridView;
                int count;

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
            }
            
            // NAME TABS
            {
                tabPage1.Text = "Cognitive Level";
                tabPage2.Text = "Function Score";
                tabPage3.Text = "Clinical Category";
                tabPage4.Text = "Speech-Language Pathology";
                tabPage5.Text = "Non-Therapy Ancillary";
                tabPage6.Text = "Extensive Services";
                tabPage7.Text = "Depression";
                tabPage8.Text = "Special Care High";
                tabPage9.Text = "Special Care Low";
                tabPage10.Text = "Clinically Complex";
                tabPage11.Text = "Behavioral & Cognitive";
                tabPage12.Text = "SLP Related Comorbidities";
                tabPage13.Text = "Presence of Conditions";
                tabPage11.Text = "Behavioral Symptoms";
                tabPage14.Text = "Restorative Nursing";
            }

            Load_Process();

            dataGridView1.CellMouseClick += new DataGridViewCellMouseEventHandler(this.Mouse_Click);
            dataGridView2.CellMouseClick += new DataGridViewCellMouseEventHandler(this.Mouse_Click);
            dataGridView3.CellMouseClick += new DataGridViewCellMouseEventHandler(this.Mouse_Click);
            dataGridView12.CellMouseClick += new DataGridViewCellMouseEventHandler(this.Mouse_Click);
            dataGridView13.CellMouseClick += new DataGridViewCellMouseEventHandler(this.Mouse_Click);
            dataGridView5.CellMouseClick += new DataGridViewCellMouseEventHandler(this.Mouse_Click);
            dataGridView6.CellMouseClick += new DataGridViewCellMouseEventHandler(this.Mouse_Click);
            dataGridView7.CellMouseClick += new DataGridViewCellMouseEventHandler(this.Mouse_Click);
            dataGridView8.CellMouseClick += new DataGridViewCellMouseEventHandler(this.Mouse_Click);
            dataGridView9.CellMouseClick += new DataGridViewCellMouseEventHandler(this.Mouse_Click);
            dataGridView10.CellMouseClick += new DataGridViewCellMouseEventHandler(this.Mouse_Click);
            dataGridView14.CellMouseClick += new DataGridViewCellMouseEventHandler(this.Mouse_Click);
            dataGridView15.CellMouseClick += new DataGridViewCellMouseEventHandler(this.Mouse_Click);
            loading = 0;
        }

        public void FuntionScore_Loader()
        {
            if (DesignMode) return;
            int i;

            // GET TABLE AND SELECT
            SQL_Variable.ExecQuery("SELECT * FROM " + tbl_Name + ";");

            dgv.DataSource = SQL_Variable.DBDT;

            // ROW HEADER DISABLE
            dgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            // SET COLUMNS
            dgv.Columns[0].Visible = false;
            dgv.Columns[1].Width = 240;
            dgv.Columns[2].Width = 300;
            dgv.Columns[3].Width = 60;
            dgv.Columns[4].Width = 60;
            dgv.Columns[2].DefaultCellStyle.SelectionBackColor = Color.White;
            dgv.Columns[2].DefaultCellStyle.ForeColor = Color.White;
            dgv.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            foreach (DataGridViewColumn Col in dgv.Columns)
            {
                Col.SortMode = DataGridViewColumnSortMode.NotSortable;
                Col.ReadOnly = true;
            }

            // DON'T ALLOW
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.FirstDisplayedScrollingRowIndex = 0;

            // MAKE ROWS READ ONLY
            tabCtrl.SelectedIndex = 1;
            dataGridView2.Show();
            for (i = 0; i <= dgv.RowCount - 1; i++)
            {
                if (new int[] { 0, 2, 4, 6, 9, 13 }.Contains(i))
                {
                    dgv.Rows[i].ReadOnly = true;
                    dgv.Rows[i].DefaultCellStyle.SelectionBackColor = SystemColors.Control;
                    dgv.Rows[i].DefaultCellStyle.SelectionForeColor = SystemColors.ControlDark;
                    dgv.Rows[i].DefaultCellStyle.BackColor = SystemColors.Control;
                    dgv.Rows[i].DefaultCellStyle.ForeColor = SystemColors.ControlDark;
                }
            }

            // ADD SUBMIT NAME
            if (actCtrl.Name != "btnAdd")
            {
                configName.Text = lstBox.Text;
            } 
        }

        public virtual void Function_sldr_Fill()
        {
            string name = "sldr";
            int i;

            for (i = 0; i <= dgv.RowCount - 1; i++)
            {
                if (new int[] { 0, 2, 4, 6, 9, 13 }.Contains(i)) continue;
                RangeSlider sldrCtrl = (RangeSlider)dgv.Controls[name + i];

                // MAX VALS
                if (dgv.Rows[i].Cells[4].Value != DBNull.Value)
                {
                    sldrCtrl.SliderMax = Convert.ToInt32(dgv.Rows[i].Cells[4].Value);
                }
                // MIN VALS
                if (dgv.Rows[i].Cells[3].Value != DBNull.Value)
                {
                    sldrCtrl.SliderMin = Convert.ToInt32(dgv.Rows[i].Cells[3].Value);
                }
            }
        }
        public void Loader()
        {
            int i;
            int r;
            int j;
            // DGV CTRLS 

            terminate = 1;
            
            SQL_Verse.ExecQuery("SELECT * FROM " + tbl_Name + ";");

            // REFRESH ROWS & COLUMNS
            dgv.AllowUserToAddRows = false;
            dgv.Rows.Clear();
            dgv.Columns.Clear();

            // SET DGV NUMBER OF ROWS BY REFRESHING TBL ROWCOUNT 
            SQL_Verse.ExecQuery("SELECT * FROM " + tbl_Name + ";");
            dgv.RowCount = SQL_Verse.RecordCount;
            dgv.ColumnCount = Col_Count;

            // CREATE GRIDVIEW COLUMNS
            Add_Check();

            // SET HEADERS AND NON SORT
            for (i = 0; i <= dgv.ColumnCount - 1; i++)
            {
                dgv.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgv.Columns[i].HeaderText = Header_Rename[i];
            }

            // FILL DATAGRID FROM DATA TABLE
            chckCol = dgv.ColumnCount - 1;
            for (r = 0; r <= SQL_Verse.RecordCount - 1; r++)
            {
                for (i = 0; i <= Col_Count - 1; i++)
                {
                    if (i != chckCol)
                    {
                        dgv.Rows[r].Cells[i].Value = SQL_Verse.DBDT.Rows[r][i];
                    }
                    else
                    {
                        if (r > 0)
                        {
                            switch (Convert.ToString(SQL_Verse.DBDT.Rows[r][i]))
                            {
                                case "1":
                                    {
                                        dgv.Rows[r].Cells[i].Value = true;
                                    }
                                    break;
                                case "0":
                                    {
                                        dgv.Rows[r].Cells[i].Value = false;
                                    }
                                    break;
                                case "":
                                    {
                                        dgv.Rows[r].Cells[i].Value = false;
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            continue;
                        } 
                    }
                }
            }

            // MAKE LAST COLUMN READ ONLY
            dgv.Rows[0].Cells[dgv.ColumnCount - 1].ReadOnly = true;

            // FREEZE COLUMNS & VISIBILITY
            dgv.Columns[0].Visible = false;
            dgv.Columns[dgv.ColumnCount - 2].Visible = false;
            dgv.Columns[dgv.ColumnCount - 3].Visible = false;

            // PLACEHOLDER
            for (i = 0; i <= dgv.RowCount - 1; i++)
            {
                if (new int[] { 7, 12, 17, 22, 27, 32 }.Contains(i))
                {

                }
            }

            // MAKE 1ST COLUMN READ ONLY
            for (i = 0; i <= dgv.RowCount - 1; i++)
            {
                dgv.Rows[i].Cells[1].ReadOnly = true;
            }

            // MAKE 1ST COLUMN STATIC WHITE
            dgv.Columns[1].DefaultCellStyle.SelectionBackColor = Color.White;
            dgv.Columns[1].DefaultCellStyle.SelectionForeColor = Color.Black;

            //DGV NUM
            int Diff;
            string name = "dataGridView";
            int gridNum;

            Diff = dgv.Name.Length - name.Trim().Length;
            gridNum = Convert.ToInt32(dgv.Name.Substring(dgv.Name.Length - Diff, Diff));

            // COLUMN ALIGNMENT & WIDTH
            dgv.Columns[1].Width = 300;
            switch (dgv.ColumnCount)
            {
                case 6:
                    {
                        switch (gridNum)
                        {
                            case 5:
                                {
                                    dgv.Rows[0].Frozen = true;
                                }
                                break;
                            default:
                                break;
                        }
                        
                        dgv.Columns[dgv.ColumnCount - 4].Width = 300;
                        // MAKE 2ND COLUMN READ ONLY
                        //dgv.Rows[0].Cells[dgv.ColumnCount - 4].ReadOnly = true;
                        dgv.Columns[dgv.ColumnCount - 4].ReadOnly = true;
                        dgv.Columns[dgv.ColumnCount - 4].DefaultCellStyle.SelectionBackColor = Color.White;
                        dgv.Columns[dgv.ColumnCount - 4].DefaultCellStyle.SelectionForeColor = Color.Black;
                        // MAKE 2ND COLUMN STATIC DISABLE   
                        dgv.Rows[0].Cells[dgv.ColumnCount - 4].Style.SelectionBackColor = SystemColors.Control;
                        dgv.Rows[0].Cells[dgv.ColumnCount - 4].Style.SelectionForeColor = SystemColors.ControlDark;
                        dgv.Rows[0].Cells[dgv.ColumnCount - 4].Style.BackColor = SystemColors.Control;
                        dgv.Rows[0].Cells[dgv.ColumnCount - 4].Style.ForeColor = SystemColors.ControlDark;

                    }
                    break;
                case 7:
                    {
                        // COLUMN 2
                        dgv.Columns[dgv.ColumnCount - 5].Width = 150;
                        // MAKE 2ND COLUMN READ ONLY
                        //dgv.Rows[0].Cells[dgv.ColumnCount - 4].ReadOnly = true;
                        dgv.Columns[dgv.ColumnCount - 5].ReadOnly = true;
                        dgv.Columns[dgv.ColumnCount - 5].DefaultCellStyle.SelectionBackColor = Color.White;
                        dgv.Columns[dgv.ColumnCount - 5].DefaultCellStyle.SelectionForeColor = Color.Black;
                        // MAKE 2ND COLUMN STATIC DISABLE   
                        dgv.Rows[0].Cells[dgv.ColumnCount - 5].Style.SelectionBackColor = SystemColors.Control;
                        dgv.Rows[0].Cells[dgv.ColumnCount - 5].Style.SelectionForeColor = SystemColors.ControlDark;
                        dgv.Rows[0].Cells[dgv.ColumnCount - 5].Style.BackColor = SystemColors.Control;
                        dgv.Rows[0].Cells[dgv.ColumnCount - 5].Style.ForeColor = SystemColors.ControlDark;

                        // COLUMN 3
                        switch (gridNum)
                        {
                            case 7:
                                {
                                    dgv.Columns[1].Frozen = true;
                                    dgv.Columns[dgv.ColumnCount - 4].Width = 150;
                                }
                                break;
                            default:
                                {
                                    dgv.Columns[dgv.ColumnCount - 4].Width = 150;
                                }
                                break;
                        }
                        
                        // MAKE 2ND COLUMN READ ONLY
                        //dgv.Rows[0].Cells[dgv.ColumnCount - 4].ReadOnly = true;
                        dgv.Columns[dgv.ColumnCount - 4].ReadOnly = true;
                        dgv.Columns[dgv.ColumnCount - 4].DefaultCellStyle.SelectionBackColor = Color.White;
                        dgv.Columns[dgv.ColumnCount - 4].DefaultCellStyle.SelectionForeColor = Color.Black;
                        // MAKE 2ND COLUMN STATIC DISABLE   
                        dgv.Rows[0].Cells[dgv.ColumnCount - 4].Style.SelectionBackColor = SystemColors.Control;
                        dgv.Rows[0].Cells[dgv.ColumnCount - 4].Style.SelectionForeColor = SystemColors.ControlDark;
                        dgv.Rows[0].Cells[dgv.ColumnCount - 4].Style.BackColor = SystemColors.Control;
                        dgv.Rows[0].Cells[dgv.ColumnCount - 4].Style.ForeColor = SystemColors.ControlDark;

                    }
                    break;
                default:
                    break;
            }

            switch (gridNum)
            {
                case 5:
                    {
                        dgv.Columns[dgv.ColumnCount - 1].Width = 60;
                    }
                    break;
                default:
                    {
                        dgv.Columns[dgv.ColumnCount - 1].Width = 60;
                    }
                    break;
            }

            dgv.Columns[dgv.ColumnCount - 1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[dgv.ColumnCount - 1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            terminate = 0;

            // ADD SUBMIT NAME
            if (actCtrl.Name != "btnAdd")
            {
                configName.Text = lstBox.Text;
            }
        }

        public void Score_Dynamic_CTRLs()
        {
            int Counter = dgv.RowCount - 1;
            int x;
            int y;
            int i;
            int Width;
            int Height;
            Rectangle rect; // STORES A SET OF FOUR INTEGERS

            tabCtrl.SelectedIndex = 1;
            dataGridView2.Show();

            for (i = 0; i <= Counter; i++)
            {
                int switchExpr = i;
                switch (switchExpr)
                {
                    case 0:
                    case 2:
                    case 4:
                    case 6:
                    case 9:
                    case 13:
                        {
                            break;
                        }
                    default:
                        {
                            var sldrCtrl = new RangeSlider();
                            sldrCtrl.Name = "sldr" + i;
                            sldrCtrl.VisualStyle = RangeSlider.RangeSliderStyle.Metro;
                            sldrCtrl.RangeColor = SystemColors.Highlight;
                            sldrCtrl.HighlightedThumbColor = SystemColors.Highlight;
                            sldrCtrl.ThumbColor = SystemColors.Highlight;
                            sldrCtrl.PushedThumbColor = SystemColors.Highlight;
                            sldrCtrl.ChannelColor = SystemColors.ControlDark;
                            sldrCtrl.BackColor = Color.White;
                            sldrCtrl.Minimum = 0;
                            sldrCtrl.Maximum = 4;
                            sldrCtrl.SliderMin = 0;
                            sldrCtrl.SliderMax = 0;
                            sldrCtrl.Enabled = false;

                            dgv.Controls.Add(sldrCtrl);

                            // SET POSITION
                            rect = dgv.GetCellDisplayRectangle(2, i, false);
                            x = rect.X;
                            y = rect.Y;
                            Width = rect.Width;
                            Height = rect.Height;

                            sldrCtrl.SetBounds(x, y, Width, Height);
                            sldrCtrl.Visible = true;

                            // ADD EVENT HANDLER
                            sldrCtrl.Click += new EventHandler(Function_sldrCrtl_Click);
                            sldrCtrl.ValueChanged += new EventHandler(Function_sldrCrtl_ValueChanged);
                            sldrCtrl.MouseDoubleClick += new MouseEventHandler(Function_sldrCtrl_DoubleClick);
                            sldrCtrl.LostFocus += new EventHandler(Function_sldrCtrl_Leave);
                            break;
                        }
                }

            }
            tabCtrl.SelectedIndex = 0;
            dataGridView1.Show();
        }
        public virtual void Function_sldrCrtl_Click(object sender, EventArgs e)
        {
            RangeSlider sldrCtrl = (RangeSlider)sender;
            int Diff;
            var rowNum = default(int);
            string name = "sldr";

            sldrCtrl.Enabled = true;

            try
            {
                Diff = sldrCtrl.Name.Length - name.Trim().Length;
                rowNum = Convert.ToInt32(sldrCtrl.Name.Substring(sldrCtrl.Name.Length - Diff, Diff));
            }
            catch (Exception ex)
            {
            }

            dataGridView2.CurrentCell = dataGridView2.Rows[rowNum].Cells[2];
        }

        public void Function_sldrCtrl_DoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            RangeSlider sldrCtrl = (RangeSlider)sender;
            if (e.Button == MouseButtons.Left)
            {
                // RESET
                sldrCtrl.SliderMin = 0;
                sldrCtrl.SliderMax = 0;
            }

        }
        public void Function_sldrCtrl_Leave(object sender, EventArgs e)
        {
            RangeSlider sldrCtrl = (RangeSlider)sender;
            sldrCtrl.Enabled = false;
        }


        public virtual void Function_sldrCrtl_ValueChanged(object sender, EventArgs e)
        {
            RangeSlider sldrCtrl = (RangeSlider)sender;
            int Diff;
            int rowNum = 0;
            double low;
            double high;
            string name = "sldr";
            int i;
            int x;
            int y;
            int Width;
            Rectangle rect;
            double avg = 0;
            int ticker = 0;
            double dontExceed;
            double avgRange;

            if (loading > 0) return;

            try
            {
                Diff = sldrCtrl.Name.Length - name.Trim().Length;
                rowNum = Convert.ToInt32(sldrCtrl.Name.Substring(sldrCtrl.Name.Length - Diff, Diff));
            }
            catch (Exception ex)
            {
            }

            if (dataGridView2.CurrentCell.RowIndex != rowNum)
            {
                return;
            }

            // SET MIN & MAX TO CELL
            dataGridView2.Rows[rowNum].Cells[3].Value = sldrCtrl.SliderMin;
            dataGridView2.Rows[rowNum].Cells[4].Value = sldrCtrl.SliderMax;
        }

        public void Dynamic_CTRLs()
        {
            int x;
            int y;
            int Width;
            int Height;

            Rectangle rect; // STORES A SET OF FOUR INTEGERS
            RangeSlider sldrCtrl = new RangeSlider();

            sldrCtrl.Name = "sldr" + Counter;
            sldrCtrl.VisualStyle = RangeSlider.RangeSliderStyle.Metro;
            sldrCtrl.RangeColor = SystemColors.Highlight;
            sldrCtrl.HighlightedThumbColor = SystemColors.Highlight;
            sldrCtrl.ThumbColor = SystemColors.Highlight;
            sldrCtrl.PushedThumbColor = SystemColors.Highlight;
            sldrCtrl.ChannelColor = SystemColors.ControlDark;
            sldrCtrl.BackColor = Color.White;

            sldrCtrl.Minimum = 0;
            sldrCtrl.Maximum = dgv.RowCount - 1;
            sldrCtrl.SliderMin = 0;
            sldrCtrl.SliderMax = 0;
            sldrCtrl.Enabled = false;

            // SET POSITION
            dgv.Controls.Add(sldrCtrl);
            rect = dataGridView1.GetCellDisplayRectangle(sldrCol, 0, false); //check this
            x = rect.X;
            y = rect.Y;
            Width = rect.Width;
            Height = rect.Height;
            sldrCtrl.SetBounds(x, y, Width, Height);
            sldrCtrl.Visible = true;

            // ADD EVENT HANDLER
            sldrCtrl.Click += new EventHandler(sldrCrtl_Click);
            sldrCtrl.ValueChanged += new EventHandler(sldrCrtl_ValueChanged);
            sldrCtrl.MouseDoubleClick += new MouseEventHandler(sldrCtrl_DoubleClick);
            sldrCtrl.LostFocus += new EventHandler(sldrCtrl_Leave);
        }

        public void Add_Check()
        {
            int i;

            // CREATE GRIDVIEW COLUMNS
            for (i = 0; i <= dgv.RowCount - 1; i++)
            {
                switch (i)
                {
                    case 0:
                        {
                            continue;
                        }
                    default:
                        {
                            var chk = new DataGridViewCheckBoxCell();
                            dgv.Rows[i].Cells[dgv.ColumnCount - 1] = chk;
                            chk.FlatStyle = FlatStyle.System;
                            chk.Style.Alignment = (DataGridViewContentAlignment)ContentAlignment.MiddleCenter;
                        }
                        break;
                }
            }
        }

        public virtual void sldrCrtl_Click(object sender, EventArgs e)
        {
            RangeSlider sldrCtrl = (RangeSlider)sender;
            int Diff;
            var rowNum = default(int);
            string name = "sldr";

            sldrCtrl.Enabled = true;

            try
            {
                Diff = sldrCtrl.Name.Length - name.Trim().Length;
                rowNum = Convert.ToInt32(sldrCtrl.Name.Substring(sldrCtrl.Name.Length - Diff, Diff));
            }
            catch (Exception ex)
            {
            }

            switch (rowNum)
            {
                case 1:
                    {
                        dataGridView1.CurrentCell = dataGridView1[sldrCol, 0];
                    }
                    break;
                case 3:
                    {
                        dataGridView3.CurrentCell = dataGridView3[sldrCol, 0];
                    }
                    break;
                case 12:
                    {
                        dataGridView12.CurrentCell = dataGridView12[sldrCol, 0];
                    }
                    break;
                case 13:
                    {
                        dataGridView13.CurrentCell = dataGridView13[sldrCol, 0];
                    }
                    break;
                case 5:
                    {
                        dataGridView5.CurrentCell = dataGridView5[sldrCol, 0];
                    }
                    break;
                case 6:
                    {
                        dataGridView6.CurrentCell = dataGridView6[sldrCol, 0];
                    }
                    break;
                case 7:
                    {
                        dataGridView7.CurrentCell = dataGridView7[sldrCol, 0];
                    }
                    break;
                case 8:
                    {
                        dataGridView8.CurrentCell = dataGridView8[sldrCol, 0];
                    }
                    break;
                case 9:
                    {
                        dataGridView9.CurrentCell = dataGridView9[sldrCol, 0];
                    }
                    break;
                case 10:
                    {
                        dataGridView10.CurrentCell = dataGridView10[sldrCol, 0];
                    }
                    break;
                case 14:
                    {
                        dataGridView14.CurrentCell = dataGridView14[sldrCol, 0];
                    }
                    break;
                case 15:
                    {
                        dataGridView15.CurrentCell = dataGridView15[sldrCol, 0];
                    }
                    break;
            }

        }

        public virtual void sldrCrtl_ValueChanged(object sender, EventArgs e)
        {
            RangeSlider sldrCtrl = (RangeSlider)sender;
            DataGridView thisDGV = default;
            int Diff;
            int rowNum = 0;
            string name = "sldr";

            if (loading > 0) return;

            try
            {
                Diff = sldrCtrl.Name.Length - name.Trim().Length;
                rowNum = Convert.ToInt32(sldrCtrl.Name.Substring(sldrCtrl.Name.Length - Diff, Diff));
            }
            catch (Exception ex)
            {
            }

            switch (rowNum)
            {
                case 1:
                    {
                        thisDGV = dataGridView1;
                    }
                    break;
                case 3:
                    {
                        thisDGV = dataGridView3;
                    }
                    break;
                case 12:
                    {
                        thisDGV = dataGridView12;
                    }
                    break;
                case 13:
                    {
                        thisDGV = dataGridView13;
                    }
                    break;
                case 5:
                    {
                        thisDGV = dataGridView5;
                    }
                    break;
                case 6:
                    {
                        thisDGV = dataGridView6;
                    }
                    break;
                case 7:
                    {
                        thisDGV = dataGridView7;
                    }
                    break;
                case 8:
                    {
                        thisDGV = dataGridView8;
                    }
                    break;
                case 9:
                    {
                        thisDGV = dataGridView9;
                    }
                    break;
                case 10:
                    {
                        thisDGV = dataGridView10;
                    }
                    break;
                case 14:
                    {
                        thisDGV = dataGridView14;
                    }
                    break;
                case 15:
                    {
                        thisDGV = dataGridView15;
                    }
                    break;
                default:
                    break;
            }

            if (thisDGV.CurrentCell.RowIndex != 0)
            {
                return;
            }

            // SET MIN & MAX TO CELL
            thisDGV.Rows[0].Cells[thisDGV.ColumnCount - 3].Value = string.Format("{0:N0}", sldrCtrl.SliderMin);
            thisDGV.Rows[0].Cells[thisDGV.ColumnCount - 2].Value = string.Format("{0:N0}", sldrCtrl.SliderMax);

            if (sldrCtrl.SliderMin == 0 && sldrCtrl.SliderMax == 0)
            {
                thisDGV.Rows[0].Cells[thisDGV.ColumnCount - 1].Value = null;
            }
            else
            {
                thisDGV.Rows[0].Cells[thisDGV.ColumnCount - 1].Value = string.Format("{0:N0}", sldrCtrl.SliderMin) + " - " + string.Format("{0:N0}", sldrCtrl.SliderMax);
            }

            // REMOVE CHECKBOX CHECK 
            foreach (DataGridViewRow row in thisDGV.Rows)
            {
                try
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[thisDGV.ColumnCount - 1];
                    if (Convert.ToBoolean(chk.Value) == true)
                    {
                        chk.Value = false;
                    }
                }
                catch (Exception ex)
                {
                }
                
            }
        }

        public void Mouse_Click(object sender, MouseEventArgs e)
        {
            DataGridView thisDGV = (DataGridView)sender;
            int i;
            string sldeName = "sldr";
            string name = "dataGridView";
            int Diff;
            int dgvNum = 0;

            if (loading > 0) return;

            try
            {
                Diff = thisDGV.Name.Length - name.Trim().Length;
                dgvNum = Convert.ToInt32(thisDGV.Name.Substring(thisDGV.Name.Length - Diff, Diff));
            }
            catch (Exception ex)
            {
            }

            if (e.Button == MouseButtons.Left)
            {
                switch (dgvNum)
                {
                    case 2:
                        {
                            switch (thisDGV.CurrentCell.RowIndex)
                            {
                                case 0:
                                case 2:
                                case 4:
                                case 6:
                                case 9:
                                case 13:
                                    {
                                        return;
                                    }

                                default:
                                    {
                                        RangeSlider sldrCtrl = (RangeSlider)thisDGV.Controls[sldeName + thisDGV.CurrentCell.RowIndex];
                                        sldrCtrl.Enabled = false;

                                        if (thisDGV.CurrentCell == thisDGV.Rows[thisDGV.CurrentCell.RowIndex].Cells[2]) //***COME BACK***
                                        {
                                            sldrCtrl.Enabled = true;
                                        }
                                    }
                                    break;
                            }  
                        }
                        break;
                    default:
                        {
                            RangeSlider sldrCtrl = (RangeSlider)thisDGV.Controls[sldeName + dgvNum];
                            sldrCtrl.Enabled = false;

                            if (thisDGV.CurrentCell == thisDGV.Rows[0].Cells[sldrCol]) //***COME BACK***
                            {
                                sldrCtrl.Enabled = true;
                            }
                            else
                            {
                                if (thisDGV.CurrentCell is DataGridViewCheckBoxCell)
                                {
                                    sldrCtrl.SliderMin = 0;
                                    sldrCtrl.SliderMax = 0;
                                    thisDGV.Rows[0].Cells[thisDGV.ColumnCount - 3].Value = 0;
                                    thisDGV.Rows[0].Cells[thisDGV.ColumnCount - 2].Value = 0;
                                    thisDGV.Rows[0].Cells[thisDGV.ColumnCount - 1].Value = null;
                                }
                            }
                        }
                        break;
                }
                
            }
        }

        public void sldrCtrl_DoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            RangeSlider sldrCtrl = (RangeSlider)sender;
            if (e.Button == MouseButtons.Left)
            {
                // RESET
                sldrCtrl.SliderMin = 0;
                sldrCtrl.SliderMax = 0;
                sldrCtrl.Enabled = false;
            }
        }

        public void sldrCtrl_Leave(object sender, EventArgs e)
        {
            RangeSlider sldrCtrl = (RangeSlider)sender;
            sldrCtrl.Enabled = false;
        }
        public void Submit_Minor()
        {
            int i;
            int y;
            string title = "TINUUM SOFTWARE";

            // ENSURE NO BLANKS OR CONFIGURE
            for (y = 0; y <= dgv.ColumnCount - 1; y++)
            {
                if (new int[] { 0, 1, 2 }.Contains(y))
                {
                    continue;
                }
                for (i = 0; i <= dgv.RowCount - 1; i++)
                {
                    if (new int[] { 0, 2, 4, 6, 9, 13 }.Contains(i))
                    {
                        continue;
                    }
                    else
                    {
                        if (dgv.Rows[i].Cells[y].Value == null || dgv.Rows[i].Cells[y].Value == DBNull.Value)
                        {
                            MessageBox.Show("You must enter scores for each item. Retry.", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            dgv.Parent.Show();
                            dgv.CurrentCell = dgv.Rows[i].Cells[y];
                            err += 1;
                            return;
                        }
                    }

                }
            }

            SQL_Variable.DBDA.Update(SQL_Variable.DBDT);
        }
        public void Submit_Major()
        {
            int i;
            int count = default;
            string title = "TINUUM SOFTWARE";
            string cmdUpdate;
            int Diff;
            string name = "dataGridView";
            int gridNum;

            Diff = dgv.Name.Length - name.Trim().Length;
            gridNum = Convert.ToInt32(dgv.Name.Substring(dgv.Name.Length - Diff, Diff));


            switch (gridNum)
            {
                case 1:
                case 3:
                    {
                        if (Convert.ToInt32(dgv.Rows[0].Cells[dgv.ColumnCount - 2].Value) == 0 && Convert.ToInt32(dgv.Rows[0].Cells[dgv.ColumnCount - 3].Value) == 0)
                        {
                            for (i = 0; i <= dgv.RowCount - 1; i++)
                            {
                                if (i == 0) continue;
                                DataGridViewCheckBoxCell cell = dgv.Rows[i].Cells[dgv.ColumnCount - 1] as DataGridViewCheckBoxCell;

                                if (cell.Value != DBNull.Value)
                                {
                                    if (Convert.ToBoolean(cell.Value) == true)
                                    {
                                        count += 1;
                                    }
                                }
                            }

                            if (count == 0)
                            {
                                MessageBox.Show("You must check at least one item before continuing.", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                TabPage slctTab = dgv.Parent as TabPage;
                                tabCtrl.SelectedTab = slctTab;
                                dgv.Parent.Show();
                                err += 1;
                                return;
                            }
                        }
                    }
                    break;
                default:
                    break;
            }   

            for (i = 0; i <= dgv.RowCount - 1; i++)
            {
                if (i > 0)
                {
                    // ADD PARAMS
                    SQL_Verse.AddParam("@PrimKey", dgv.Rows[i].Cells[0].Value);
                    if (Convert.ToBoolean(dgv.Rows[i].Cells[dgv.ColumnCount - 1].Value) == true)
                    {
                        SQL_Verse.AddParam("@vals", 1);
                    }
                    else
                    {
                        SQL_Verse.AddParam("@vals", 0);
                    }

                    // UPDATE STATEMENT FOR MAINVERSE
                    cmdUpdate = "UPDATE " + tbl_Name + " SET [Select]=@vals WHERE Prime=@PrimKey;";
                    SQL_Verse.ExecQuery(cmdUpdate);
                }
                else
                {
                    SQL_Verse.AddParam("@PrimKey", dgv.Rows[i].Cells[0].Value);
                    SQL_Verse.AddParam("@val1", dgv.Rows[i].Cells[dgv.ColumnCount - 2].Value);
                    SQL_Verse.AddParam("@val2", dgv.Rows[i].Cells[dgv.ColumnCount - 3].Value);
                    cmdUpdate = "UPDATE " + tbl_Name + " SET [Slider_High]=@val1, [Slider_Low]=@val2 WHERE Prime=@PrimKey;";
                    SQL_Verse.ExecQuery(cmdUpdate);
                }
            }
        }

        public virtual void Delegate()
        {
            SQLQueries.tblRollMDSCreate();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            int i;
            int z;
            int tabs = 15;
            string title = "TINUUM SOFTWARE";
            int countIt = default;

            // ENSURE NAME FIELD NOT BLANK
            if (configName.Text == "")
            {
                MessageBox.Show("You must enter a name for the collection. Retry.", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.ActiveControl = configName;
                return;
            }

            // ENSURE NO DUPLICATE ENTRIES
            {
                if (lstBox.Items.Count > 0)
                {
                    for (i = 0; i <= lstBox.Items.Count - 1; i++)
                    {
                        if (i == lstIndex) continue;

                        drv = (DataRowView)lstBox.Items[i];
                        if (drv[slctCol].ToString().ToLower() == configName.Text.ToString().ToLower())
                        {
                            countIt += 1;
                        }
                    }

                    if (countIt > 0)
                    {
                        configName.Text = "";
                        this.ActiveControl = configName;
                        MessageBox.Show("You cannot enter duplicate values in this field. Retry.", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            for (z = 1; z <= tabs; z++)
            {
                switch (z)
                {
                    case 1:
                        {
                            tbl_Name = tbl_BIMS + primeKey;
                            dgv = dataGridView1;
                            Submit_Major();
                            Counter = z;
                        }
                        break;
                    case 2:
                        {
                            tbl_Name = tbl_Function + primeKey;
                            dgv = dataGridView2;
                            Submit_Minor();
                            Counter = z;
                        }
                        break;
                    case 3:
                        {
                            tbl_Name = tbl_Clinical + primeKey;
                            dgv = dataGridView3;
                            Submit_Major();
                            Counter = z;
                        }
                        break;
                    case 12:
                        {
                            tbl_Name = tbl_Morbid + primeKey;
                            dgv = dataGridView12;
                            Submit_Major();
                            Counter = z;
                        }
                        break;
                    case 13:
                        {
                            tbl_Name = tbl_Disorder + primeKey;
                            dgv = dataGridView13;
                            Submit_Major();
                            Counter = z;
                        }
                        break;
                    case 5:
                        {
                            tbl_Name = tbl_NTA + primeKey;
                            dgv = dataGridView5;
                            Submit_Major();
                            Counter = z;
                        }
                        break;
                    case 6:
                        {
                            tbl_Name = tbl_Extensive + primeKey;
                            dgv = dataGridView6;
                            Submit_Major();
                            Counter = z;
                        }
                        break;
                    case 7:
                        {
                            tbl_Name = tbl_Depression + primeKey;
                            dgv = dataGridView7;
                            Submit_Major();
                            Counter = z;
                        }
                        break;
                    case 8:
                        {
                            tbl_Name = tbl_SCH + primeKey;
                            dgv = dataGridView8;
                            Submit_Major();
                            Counter = z;
                        }
                        break;
                    case 9:
                        {
                            tbl_Name = tbl_SCL + primeKey;
                            dgv = dataGridView9;
                            Submit_Major();
                            Counter = z;
                        }
                        break;
                    case 10:
                        {
                            tbl_Name = tbl_Complex + primeKey;
                            dgv = dataGridView10;
                            Submit_Major();
                            Counter = z;
                        }
                        break;
                    case 14:
                        {
                            tbl_Name = tbl_Behavioral + primeKey;
                            dgv = dataGridView14;
                            Submit_Major();
                            Counter = z;
                        }
                        break;
                    case 15:
                        {
                            tbl_Name = tbl_Restorative + primeKey;
                            dgv = dataGridView15;
                            Submit_Major();
                            Counter = z;
                        }
                        break;
                    default:
                        {
                            continue;
                        }
                }
                if (err > 0)
                {
                    err = 0;
                    return;
                }
            }
            
            update_active();
            Write_Detail();
            this.Dispose();
        }

        public void call_cancel()
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
                        dlt_tbl_BIMS = tbl_BIMS + primeKey;
                        dlt_tbl_FunctionScore = tbl_Function + primeKey;
                        dlt_tbl_Clinical = tbl_Clinical + primeKey;
                        dlt_tbl_Morbid = tbl_Morbid + primeKey;
                        dlt_tbl_Disorder = tbl_Disorder + primeKey;
                        dlt_tbl_NTA = tbl_NTA + primeKey;
                        dlt_tbl_Extensive = tbl_Extensive + primeKey;
                        dlt_tbl_Depression = tbl_Depression + primeKey;
                        dlt_tbl_SCH = tbl_SCH + primeKey;
                        dlt_tbl_SCL = tbl_SCL + primeKey;
                        dlt_tbl_Complex = tbl_Complex + primeKey;
                        dlt_tbl_Behavioral = tbl_Behavioral + primeKey;
                        dlt_tbl_Restorative = tbl_Restorative + primeKey;

                        // DROP TABLE
                        SQL_Variable.ExecQuery("DROP TABLE " + dlt_tbl_BIMS + ";");
                        SQL_Variable.ExecQuery("DROP TABLE " + dlt_tbl_FunctionScore + ";");
                        SQL_Variable.ExecQuery("DROP TABLE " + dlt_tbl_Clinical + ";");
                        SQL_Variable.ExecQuery("DROP TABLE " + dlt_tbl_Morbid + ";");
                        SQL_Variable.ExecQuery("DROP TABLE " + dlt_tbl_Disorder + ";");
                        SQL_Variable.ExecQuery("DROP TABLE " + dlt_tbl_NTA + ";");
                        SQL_Variable.ExecQuery("DROP TABLE " + dlt_tbl_Extensive + ";");
                        SQL_Variable.ExecQuery("DROP TABLE " + dlt_tbl_Depression + ";");
                        SQL_Variable.ExecQuery("DROP TABLE " + dlt_tbl_SCH + ";");
                        SQL_Variable.ExecQuery("DROP TABLE " + dlt_tbl_SCL + ";");
                        SQL_Variable.ExecQuery("DROP TABLE " + dlt_tbl_Complex + ";");
                        SQL_Variable.ExecQuery("DROP TABLE " + dlt_tbl_Behavioral + ";");
                        SQL_Variable.ExecQuery("DROP TABLE " + dlt_tbl_Restorative + ";");

                        // DELETE ENTRY FROM TABLE
                        SQL_Variable.AddParam("@PrimeKey", primeKey);
                        SQL_Variable.ExecQuery("DELETE FROM " + tbl_Active + " WHERE Prime=@PrimeKey;");

                        // clean up
                        mainDGV.CurrentCell = mainDGV.Rows[FormRoll_PPS._keyNum].Cells[18];
                        //mainDGV.Rows[FormRoll_PPS._keyNum].Cells[18].Value = "";
                        frm.Enabled = true;
                        this.Close();
                    }
                    else
                    {
                        mainDGV.CurrentCell = mainDGV.Rows[FormRoll_PPS._keyNum].Cells[18];
                        // mainDGV.Rows[FormRoll_PPS._keyNum].Cells[18].Value = "";
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
        public void Write_Detail()
        {
            // NO NEED TO SET CURRENT CELL - SET ON CLICK EVENT IN PARENT FRM
            mainDGV.CurrentCell = mainDGV.Rows[FormRoll_PPS._keyNum].Cells[18];
            frm.Enabled = true;
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

        private void FormRollMDS_FormClosing(object sender, FormClosingEventArgs e)
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

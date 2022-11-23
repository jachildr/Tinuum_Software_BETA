using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tinuum_Software_BETA.Icon_Masters;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualBasic;
using Tinuum_Software_BETA.Popups.Roster;
using Syncfusion.Windows.Forms.Tools;

namespace Tinuum_Software_BETA.Icon_Masters
{
    public partial class FormMain : Form
    {
        protected string tbl_Inventory = "dtbInventoryVerse";
        protected string tbl_SF = "dtbInventoryDetail_SF";
        protected string tbl_Home = "dtbHome";
        protected SQLControl SQL_Main = new SQLControl();
        protected int beds;
        protected int units;
        protected int SF;
        protected int Comp;
        protected int ttlBed;
        protected string MSA;
        protected double Wage = default;

        public FormMain()
        {
            InitializeComponent();
        }

        private void getValues()
        {
            int i;
            ttlBed = 0;
            try
            {
                SQL_Main.ExecQuery("SELECT * FROM " + tbl_Inventory + ";");
                units = Convert.ToInt32(SQL_Main.DBDT.Rows[0][8]);
                beds = Convert.ToInt32(SQL_Main.DBDT.Rows[0][9]);
                Comp = SQL_Main.RecordCount - 1;

                for (i = 0; i <= SQL_Main.RecordCount - 1; i++)
                {
                    ttlBed += Convert.ToInt32(SQL_Main.DBDT.Rows[0][9]);
                }

                SQL_Main.ExecQuery("SELECT * FROM " + tbl_SF + ";");
                SF = Convert.ToInt32(SQL_Main.DBDT.Rows[0][2]);

                SQL_Main.ExecQuery("SELECT * FROM " + tbl_Home + ";");
                Wage = Convert.ToDouble(SQL_Main.DBDT.Rows[0][9]);
                MSA = Convert.ToString(SQL_Main.DBDT.Rows[0][6]);
            }
            catch
            {

            }

            if (Information.IsNumeric(Comp))
            {
                if (Comp != 0)
                {
                    textBox27.Text = Comp.ToString();
                    //textBox27.BackColor = SystemColors.ButtonHighlight;
                }
                else
                {
                    textBox27.Text = "";
                    textBox27.BackColor = SystemColors.Control;
                }
            }
            else
            {
                textBox27.Text = "";
                textBox27.BackColor = SystemColors.Control;
            }

            if (Information.IsNumeric(ttlBed))
            {
                if (ttlBed != 0)
                {
                    textBox29.Text = ttlBed.ToString();
                    //textBox29.BackColor = SystemColors.ButtonHighlight;
                }
                else
                {
                    textBox29.Text = "";
                    textBox29.BackColor = SystemColors.Control;
                }
            }
            else
            {
                textBox29.Text = "";
                textBox29.BackColor = SystemColors.Control;
            }

            if (Information.IsNumeric(beds))
            {
                if (beds != 0)
                {
                    textBox10.Text = beds.ToString();
                    //textBox10.BackColor = SystemColors.ButtonHighlight;
                }
                else
                {
                    textBox10.Text = "";
                    textBox10.BackColor = SystemColors.Control;
                }
            }
            else
            {
                textBox10.Text = "";
                textBox10.BackColor = SystemColors.Control;
            }

            //if (Information.IsNumeric(units))
            //{
            //    if (units != 0)
            //    {
            //        textBox11.Text = units.ToString();
            //        textBox11.BackColor = SystemColors.ButtonHighlight;
            //    }
            //    else
            //    {
            //        textBox11.Text = "";
            //        textBox11.BackColor = SystemColors.Control;
            //    }
            //}
            //else
            //{
            //    textBox11.Text = "";
            //    textBox11.BackColor = SystemColors.Control;
            //}

            if (Information.IsNumeric(SF))
            {
                if (SF != 0)
                {
                    textBox13.Text = String.Format("{0:n0}", SF);
                    //textBox13.BackColor = SystemColors.ButtonHighlight;
                }
                else
                {
                    textBox13.Text = "";
                    textBox13.BackColor = SystemColors.Control;
                }
            }
            else
            {
                textBox13.Text = "";
                textBox13.BackColor = SystemColors.Control;
            }

            if (Information.IsNumeric(Wage))
            {
                if (Wage != 0)
                {
                    textBox15.Text = Wage.ToString();
                    //textBox15.BackColor = SystemColors.ButtonHighlight;
                    textBox16.Text = MSA + " Wage Index";
                }
                else
                {
                    textBox15.Text = "";
                    textBox15.BackColor = SystemColors.Control;
                }
            }
            else
            {
                textBox15.Text = "";
                textBox15.BackColor = SystemColors.Control;
            }
        }

        private void FormMaster_Load(object sender, EventArgs e)
        {
            getValues();

            button1.Image = Image.FromFile("C:\\Users\\jchil\\Pictures\\icon\\Picture1.png");
            button1.ImageAlign = ContentAlignment.MiddleCenter;
            button1.TextImageRelation = TextImageRelation.ImageBeforeText;

            button2.Image = Image.FromFile("C:\\Users\\jchil\\Pictures\\icon\\Picture2.png");
            button2.ImageAlign = ContentAlignment.MiddleCenter;
            button2.TextImageRelation = TextImageRelation.ImageBeforeText;

            button3.Image = Image.FromFile("C:\\Users\\jchil\\Pictures\\icon\\Picture4.png"); // SWITCH
            button3.ImageAlign = ContentAlignment.MiddleCenter;
            button3.TextImageRelation = TextImageRelation.ImageBeforeText;

            button4.Image = Image.FromFile("C:\\Users\\jchil\\Pictures\\icon\\Picture3.png"); // SWITCH
            button4.ImageAlign = ContentAlignment.MiddleCenter;
            button4.TextImageRelation = TextImageRelation.ImageBeforeText;

            button5.Image = Image.FromFile("C:\\Users\\jchil\\Pictures\\icon\\Picture5.png");
            button5.ImageAlign = ContentAlignment.MiddleCenter;
            button5.TextImageRelation = TextImageRelation.ImageBeforeText;

            button6.Image = Image.FromFile("C:\\Users\\jchil\\Pictures\\icon\\Picture6.png");
            button6.ImageAlign = ContentAlignment.MiddleCenter;
            button6.TextImageRelation = TextImageRelation.ImageBeforeText;

            button7.Image = Image.FromFile("C:\\Users\\jchil\\Pictures\\icon\\Picture7.png");
            button7.ImageAlign = ContentAlignment.MiddleCenter;
            button7.TextImageRelation = TextImageRelation.ImageBeforeText;

            button8.Image = Image.FromFile("C:\\Users\\jchil\\Pictures\\icon\\Picture8.png");
            button8.ImageAlign = ContentAlignment.MiddleCenter;
            button8.TextImageRelation = TextImageRelation.ImageBeforeText;

            button9.Image = Image.FromFile("C:\\Users\\jchil\\Pictures\\icon\\Picture9.png");
            button9.ImageAlign = ContentAlignment.MiddleCenter;
            button9.TextImageRelation = TextImageRelation.ImageBeforeText;

            button10.Image = Image.FromFile("C:\\Users\\jchil\\Pictures\\icon\\Picture10.png");
            button10.ImageAlign = ContentAlignment.MiddleCenter;
            button10.TextImageRelation = TextImageRelation.ImageBeforeText;

            //pictureBox1.BackColor = Color.Transparent;
        }

        private void disable()
        {
            foreach (Control ctrl in this.panel1.Controls)
            {
                if (ctrl is Button)
                {
                    if (this.ActiveControl == ctrl) continue;
                    // ctrl.Enabled = false;
                }
            }
        }

        private void enable()
        {
            foreach (Control ctrl in this.panel1.Controls)
            {
                if (ctrl is Button)
                {
                    // ctrl.Enabled = true;
                    ctrl.BackColor = Color.FromArgb(34, 34, 34);
                }
                if (ctrl is TextBox)
                {
                    ctrl.Visible = false;
                }
            }
        }
        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (panel3.Controls.Count > 0) return;

            button1.BackColor = Color.Black;
            textBox1.Visible = true;
            disable();

            FormGeneral myForm = new FormGeneral();
            myForm.TopLevel = false;
            myForm.AutoScroll = true;
            this.panel3.Controls.Add(myForm);
            myForm.FormBorderStyle = FormBorderStyle.None;
            myForm.Dock = DockStyle.Fill;
            myForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (panel3.Controls.Count > 0) return;

            button2.BackColor = Color.Black;
            textBox2.Visible = true;
            disable();

            FormRates myForm = new FormRates();
            myForm.TopLevel = false;
            myForm.AutoScroll = true;
            this.panel3.Controls.Add(myForm);
            myForm.FormBorderStyle = FormBorderStyle.None;
            myForm.Dock = DockStyle.Fill;
            myForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (panel3.Controls.Count > 0) return;

            button3.BackColor = Color.Black;
            textBox3.Visible = true;
            disable();

            FormMarket myForm = new FormMarket();
            myForm.TopLevel = false;
            myForm.AutoScroll = true;
            this.panel3.Controls.Add(myForm);
            myForm.FormBorderStyle = FormBorderStyle.None;
            myForm.Dock = DockStyle.Fill;
            myForm.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (panel3.Controls.Count > 0) return;

            button8.BackColor = Color.Black;
            textBox8.Visible = true;
            disable();

            FormRoster myForm = new FormRoster();
            myForm.TopLevel = false;
            myForm.AutoScroll = true;
            this.panel3.Controls.Add(myForm);
            myForm.FormBorderStyle = FormBorderStyle.None;
            myForm.Dock = DockStyle.Fill;
            myForm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (panel3.Controls.Count > 0) return;

            button4.BackColor = Color.Black;
            textBox4.Visible = true;
            disable();

            FormInventory myForm = new FormInventory();
            myForm.TopLevel = false;
            myForm.AutoScroll = true;
            this.panel3.Controls.Add(myForm);
            myForm.FormBorderStyle = FormBorderStyle.None;
            myForm.Dock = DockStyle.Fill;
            myForm.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (panel3.Controls.Count > 0) return;

            button5.BackColor = Color.Black;
            textBox5.Visible = true;
            disable();

            FormRoll myForm = new FormRoll();
            myForm.TopLevel = false;
            myForm.AutoScroll = true;
            this.panel3.Controls.Add(myForm);
            myForm.FormBorderStyle = FormBorderStyle.None;
            myForm.Dock = DockStyle.Fill;
            myForm.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (panel3.Controls.Count > 0) return;

            button6.BackColor = Color.Black;
            textBox6.Visible = true;
            disable();

            FormExpense myForm = new FormExpense();
            myForm.TopLevel = false;
            myForm.AutoScroll = true;
            this.panel3.Controls.Add(myForm);
            myForm.FormBorderStyle = FormBorderStyle.None;
            myForm.Dock = DockStyle.Fill;
            myForm.Show();
        }

        private void panel3_ControlRemoved(object sender, ControlEventArgs e)
        {
            enable();
            getValues();

            foreach (Control ctrl in panel1.Controls)
            {
                if (ctrl is Button)
                {
                    (ctrl as Button).FlatAppearance.MouseOverBackColor = Color.Black;
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_2(object sender, EventArgs e)
        {

        }

        private void panel4_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void textBox28_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox30_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            //if (panel3.Controls.Count > 0) button1.FlatAppearance.MouseOverBackColor = Color.FromArgb(34, 34, 34);
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            //if (panel3.Controls.Count > 0) button2.FlatAppearance.MouseOverBackColor = Color.FromArgb(34, 34, 34);
        }

        private void button3_MouseHover(object sender, EventArgs e)
        {
            //if (panel3.Controls.Count > 0) button3.FlatAppearance.MouseOverBackColor = Color.FromArgb(34, 34, 34);
        }

        private void button4_MouseHover(object sender, EventArgs e)
        {
            //if (panel3.Controls.Count > 0) button4.FlatAppearance.MouseOverBackColor = Color.FromArgb(34, 34, 34);
        }

        private void button5_MouseHover(object sender, EventArgs e)
        {
            //if (panel3.Controls.Count > 0) button5.FlatAppearance.MouseOverBackColor = Color.FromArgb(34, 34, 34);
        }

        private void button6_MouseHover(object sender, EventArgs e)
        {
            //if (panel3.Controls.Count > 0) button6.FlatAppearance.MouseOverBackColor = Color.FromArgb(34, 34, 34);
        }

        private void button7_MouseHover(object sender, EventArgs e)
        {
            //if (panel3.Controls.Count > 0) button7.FlatAppearance.MouseOverBackColor = Color.FromArgb(34, 34, 34);
        }

        private void button8_MouseHover(object sender, EventArgs e)
        {
            //if (panel3.Controls.Count > 0) button8.FlatAppearance.MouseOverBackColor = Color.FromArgb(34, 34, 34);
        }

        private void button9_MouseHover(object sender, EventArgs e)
        {
            //if (panel3.Controls.Count > 0) button9.FlatAppearance.MouseOverBackColor = Color.FromArgb(34, 34, 34);
        }

        private void button10_MouseHover(object sender, EventArgs e)
        {
            //if (panel3.Controls.Count > 0) button10.FlatAppearance.MouseOverBackColor = Color.FromArgb(34, 34, 34);
        }

        private void panel3_ControlAdded(object sender, ControlEventArgs e)
        {

            foreach (Control ctrl in panel1.Controls)
            {
                if (ctrl is Button)
                {
                    if (ctrl.BackColor == Color.Black) continue;
                    (ctrl as Button).FlatAppearance.MouseOverBackColor = Color.FromArgb(34, 34, 34);
                }
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            string title = "TINNUM SOFTWARE";

            if (panel3.Controls.Count > 0)
            {
                e.Cancel = true;
                MessageBox.Show("Exit open form before closing. Retry.", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                e.Cancel = false;
            }
        }

        private void textBox11_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox31_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox25_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox26_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox27_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox29_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox33_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox35_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox34_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox36_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox35_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox36_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox33_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox34_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox31_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox33_TextChanged_2(object sender, EventArgs e)
        {

        }

        private void textBox34_TextChanged_2(object sender, EventArgs e)
        {

        }

        private void textBox35_TextChanged_2(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged_2(object sender, EventArgs e)
        {

        }

        private void FormMain_SizeChanged(object sender, EventArgs e)
        {
            foreach (Control ctrl in panel2.Controls)
            {
                int start = pictureBox1.Location.X + pictureBox1.Width;
                int space = 6;
                int ttlSpace = space * 6;
                int frmWidth = this.Width - start - ttlSpace;
                int pnlWidth = (frmWidth / 5) - (space / 2);

                if (ctrl is Panel)
                {
                    switch (ctrl.Name)
                    {
                        case "panel5":
                            {
                                ctrl.Size = new Size(pnlWidth, ctrl.Height);
                                ctrl.Left = start + (space * 1) + (pnlWidth * 0);
                            }
                            break;
                        case "panel6":
                            {
                                ctrl.Size = new Size(pnlWidth, ctrl.Height);
                                ctrl.Left = start + (space * 2) + (pnlWidth * 1);
                            }
                            break;
                        case "panel9":
                            {
                                ctrl.Size = new Size(pnlWidth, ctrl.Height);
                                ctrl.Left = start + (space * 3) + (pnlWidth * 2);
                            }
                            break;
                        case "panel8":
                            {
                                ctrl.Size = new Size(pnlWidth, ctrl.Height);
                                ctrl.Left = start + (space * 4) + (pnlWidth * 3);
                            }
                            break;
                        case "panel7":
                            {
                                ctrl.Size = new Size(pnlWidth, ctrl.Height);
                                ctrl.Left = start + (space * 5) + (pnlWidth * 4);
                            }
                            break;
                    }
                    
                    
                }
            }
        }
    }
}

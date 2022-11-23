
namespace Tinuum_Software_BETA.Popups.Expense
{
    partial class FormSelector_Payor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSelector_Payor));
            this.listBox_Input = new System.Windows.Forms.ListBox();
            this.listBox_Output = new System.Windows.Forms.ListBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.configName = new System.Windows.Forms.TextBox();
            this.btnAll_Right = new System.Windows.Forms.Button();
            this.btnSlct_Right = new System.Windows.Forms.Button();
            this.btnSlct_Left = new System.Windows.Forms.Button();
            this.btnAll_Left = new System.Windows.Forms.Button();
            this.gridBagLayout1 = new Syncfusion.Windows.Forms.Tools.GridBagLayout(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gridBagLayout1)).BeginInit();
            this.SuspendLayout();
            // 
            // listBox_Input
            // 
            this.listBox_Input.FormattingEnabled = true;
            this.listBox_Input.Location = new System.Drawing.Point(21, 84);
            this.listBox_Input.Name = "listBox_Input";
            this.listBox_Input.Size = new System.Drawing.Size(275, 433);
            this.listBox_Input.TabIndex = 0;
            this.listBox_Input.Click += new System.EventHandler(this.listBox_Input_Click);
            // 
            // listBox_Output
            // 
            this.listBox_Output.FormattingEnabled = true;
            this.listBox_Output.Location = new System.Drawing.Point(395, 84);
            this.listBox_Output.Name = "listBox_Output";
            this.listBox_Output.Size = new System.Drawing.Size(275, 433);
            this.listBox_Output.TabIndex = 1;
            this.listBox_Output.Click += new System.EventHandler(this.listBox_Output_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(393, 29);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(551, 29);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(120, 23);
            this.btnSubmit.TabIndex = 3;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // configName
            // 
            this.configName.Location = new System.Drawing.Point(21, 31);
            this.configName.Name = "configName";
            this.configName.Size = new System.Drawing.Size(275, 20);
            this.configName.TabIndex = 4;
            // 
            // btnAll_Right
            // 
            this.btnAll_Right.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAll_Right.Location = new System.Drawing.Point(325, 231);
            this.btnAll_Right.Name = "btnAll_Right";
            this.btnAll_Right.Size = new System.Drawing.Size(40, 20);
            this.btnAll_Right.TabIndex = 5;
            this.btnAll_Right.Text = ">>";
            this.btnAll_Right.UseVisualStyleBackColor = true;
            this.btnAll_Right.Click += new System.EventHandler(this.btnAll_Right_Click);
            // 
            // btnSlct_Right
            // 
            this.btnSlct_Right.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSlct_Right.Location = new System.Drawing.Point(325, 257);
            this.btnSlct_Right.Name = "btnSlct_Right";
            this.btnSlct_Right.Size = new System.Drawing.Size(40, 20);
            this.btnSlct_Right.TabIndex = 6;
            this.btnSlct_Right.Text = ">";
            this.btnSlct_Right.UseVisualStyleBackColor = true;
            this.btnSlct_Right.Click += new System.EventHandler(this.btnSlct_Right_Click);
            // 
            // btnSlct_Left
            // 
            this.btnSlct_Left.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSlct_Left.Location = new System.Drawing.Point(325, 283);
            this.btnSlct_Left.Name = "btnSlct_Left";
            this.btnSlct_Left.Size = new System.Drawing.Size(40, 20);
            this.btnSlct_Left.TabIndex = 7;
            this.btnSlct_Left.Text = "<";
            this.btnSlct_Left.UseVisualStyleBackColor = true;
            this.btnSlct_Left.Click += new System.EventHandler(this.btnSlct_Left_Click);
            // 
            // btnAll_Left
            // 
            this.btnAll_Left.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAll_Left.Location = new System.Drawing.Point(325, 309);
            this.btnAll_Left.Name = "btnAll_Left";
            this.btnAll_Left.Size = new System.Drawing.Size(40, 20);
            this.btnAll_Left.TabIndex = 8;
            this.btnAll_Left.Text = "<<";
            this.btnAll_Left.UseVisualStyleBackColor = true;
            this.btnAll_Left.Click += new System.EventHandler(this.btnAll_Left_Click);
            // 
            // FormSelector_Payor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 541);
            this.Controls.Add(this.btnAll_Left);
            this.Controls.Add(this.btnSlct_Left);
            this.Controls.Add(this.btnSlct_Right);
            this.Controls.Add(this.btnAll_Right);
            this.Controls.Add(this.configName);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.listBox_Output);
            this.Controls.Add(this.listBox_Input);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSelector_Payor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Selection Portal";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSelector_Payor_FormClosing);
            this.Load += new System.EventHandler(this.FormSelector_Payor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridBagLayout1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox_Input;
        private System.Windows.Forms.Button btnAll_Right;
        private System.Windows.Forms.Button btnSlct_Right;
        private System.Windows.Forms.Button btnSlct_Left;
        private System.Windows.Forms.Button btnAll_Left;
        private Syncfusion.Windows.Forms.Tools.GridBagLayout gridBagLayout1;
        public System.Windows.Forms.ListBox listBox_Output;
        public System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.Button btnSubmit;
        public System.Windows.Forms.TextBox configName;
    }
}
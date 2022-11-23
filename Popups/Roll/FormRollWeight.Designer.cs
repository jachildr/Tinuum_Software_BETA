namespace Tinuum_Software_BETA.Popups.Roll
{
    partial class FormRollWeight
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
            this.configName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(343, 480);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(480, 20);
            this.btnDelete.Visible = false;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(474, 480);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(350, 20);
            this.btnAdd.Visible = false;
            // 
            // configName
            // 
            this.configName.Location = new System.Drawing.Point(12, 24);
            this.configName.Name = "configName";
            this.configName.Size = new System.Drawing.Size(244, 20);
            this.configName.TabIndex = 16;
            // 
            // FormRollWeight
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(603, 526);
            this.Controls.Add(this.configName);
            this.Name = "FormRollWeight";
            this.Text = "Component Weights";
            this.Controls.SetChildIndex(this.btnAdd, 0);
            this.Controls.SetChildIndex(this.btnSubmit, 0);
            this.Controls.SetChildIndex(this.btnDelete, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.configName, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox configName;
    }
}

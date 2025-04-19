namespace DVLD_Presentation
{
    partial class ShowPersonLicensesHistoryForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.ctrlPersonLicensesHistory1 = new DVLD_Presentation.ctrlPersonLicensesHistory();
            this.ctrlPersonDetails1 = new DVLD_Presentation.ctrlPersonDetails();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Britannic Bold", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(313, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(363, 174);
            this.label1.TabIndex = 4;
            this.label1.Text = "Licenses \r\n     Record";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Image = global::DVLD_Presentation.Properties.Resources.Close_32;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(853, 832);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(123, 28);
            this.button1.TabIndex = 19;
            this.button1.Text = "   Close";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ctrlPersonLicensesHistory1
            // 
            this.ctrlPersonLicensesHistory1.BackColor = System.Drawing.Color.White;
            this.ctrlPersonLicensesHistory1.Location = new System.Drawing.Point(13, 469);
            this.ctrlPersonLicensesHistory1.Name = "ctrlPersonLicensesHistory1";
            this.ctrlPersonLicensesHistory1.Size = new System.Drawing.Size(963, 357);
            this.ctrlPersonLicensesHistory1.TabIndex = 5;
            // 
            // ctrlPersonDetails1
            // 
            this.ctrlPersonDetails1.BackColor = System.Drawing.Color.White;
            this.ctrlPersonDetails1.Location = new System.Drawing.Point(124, 207);
            this.ctrlPersonDetails1.Name = "ctrlPersonDetails1";
            this.ctrlPersonDetails1.Size = new System.Drawing.Size(751, 256);
            this.ctrlPersonDetails1.TabIndex = 1;
            // 
            // ShowPersonLicensesHistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(988, 867);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ctrlPersonLicensesHistory1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ctrlPersonDetails1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ShowPersonLicensesHistoryForm";
            this.Text = "Show Person Licenses History";
            this.Load += new System.EventHandler(this.ShowPersonLicensesHistoryForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private ctrlPersonDetails ctrlPersonDetails1;
        private System.Windows.Forms.Label label1;
        private ctrlPersonLicensesHistory ctrlPersonLicensesHistory1;
        private System.Windows.Forms.Button button1;
    }
}
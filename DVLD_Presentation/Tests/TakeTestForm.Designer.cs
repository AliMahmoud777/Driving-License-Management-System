namespace DVLD_Presentation.Tests
{
    partial class TakeTestForm
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
            this.ctrlTakeTest1 = new DVLD_Presentation.ctrlTakeTest();
            this.SuspendLayout();
            // 
            // ctrlTakeTest1
            // 
            this.ctrlTakeTest1.Location = new System.Drawing.Point(3, 0);
            this.ctrlTakeTest1.Name = "ctrlTakeTest1";
            this.ctrlTakeTest1.Size = new System.Drawing.Size(562, 715);
            this.ctrlTakeTest1.TabIndex = 0;
            // 
            // TakeTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 719);
            this.Controls.Add(this.ctrlTakeTest1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "TakeTestForm";
            this.Text = "Take Test";
            this.Load += new System.EventHandler(this.TakeTestForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlTakeTest ctrlTakeTest1;
    }
}
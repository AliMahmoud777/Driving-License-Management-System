﻿namespace DVLD_Presentation
{
    partial class ctrlPersonInfo
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.txtSecondName = new System.Windows.Forms.TextBox();
            this.txtThirdName = new System.Windows.Forms.TextBox();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNationalNo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.rbMale = new System.Windows.Forms.RadioButton();
            this.rbFemale = new System.Windows.Forms.RadioButton();
            this.label9 = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.cbCountry = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.lblRemove = new System.Windows.Forms.LinkLabel();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.pbPersonImage = new System.Windows.Forms.PictureBox();
            this.pictureBox9 = new System.Windows.Forms.PictureBox();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPersonImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // txtFirstName
            // 
            this.txtFirstName.Location = new System.Drawing.Point(144, 41);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(130, 20);
            this.txtFirstName.TabIndex = 1;
            this.txtFirstName.Validating += new System.ComponentModel.CancelEventHandler(this.textBox1_Validating);
            // 
            // txtSecondName
            // 
            this.txtSecondName.Location = new System.Drawing.Point(306, 41);
            this.txtSecondName.Name = "txtSecondName";
            this.txtSecondName.Size = new System.Drawing.Size(130, 20);
            this.txtSecondName.TabIndex = 2;
            this.txtSecondName.TextChanged += new System.EventHandler(this.txtSecondName_TextChanged);
            this.txtSecondName.Validating += new System.ComponentModel.CancelEventHandler(this.txtSecondName_Validating);
            // 
            // txtThirdName
            // 
            this.txtThirdName.Location = new System.Drawing.Point(468, 41);
            this.txtThirdName.Name = "txtThirdName";
            this.txtThirdName.Size = new System.Drawing.Size(130, 20);
            this.txtThirdName.TabIndex = 3;
            // 
            // txtLastName
            // 
            this.txtLastName.Location = new System.Drawing.Point(630, 41);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(130, 20);
            this.txtLastName.TabIndex = 4;
            this.txtLastName.Validating += new System.ComponentModel.CancelEventHandler(this.textBox4_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(191, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "First";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(352, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Second";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(514, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "Third";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(682, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "Last";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(17, 77);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 16);
            this.label6.TabIndex = 10;
            this.label6.Text = "National No:";
            // 
            // txtNationalNo
            // 
            this.txtNationalNo.Location = new System.Drawing.Point(144, 76);
            this.txtNationalNo.Name = "txtNationalNo";
            this.txtNationalNo.Size = new System.Drawing.Size(130, 20);
            this.txtNationalNo.TabIndex = 11;
            this.txtNationalNo.TextChanged += new System.EventHandler(this.textBox5_TextChanged);
            this.txtNationalNo.Validating += new System.ComponentModel.CancelEventHandler(this.txtNationalNo_Validating);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(320, 77);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(95, 16);
            this.label7.TabIndex = 13;
            this.label7.Text = "Date of Birth:";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(459, 76);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(130, 20);
            this.dateTimePicker1.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(17, 113);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 16);
            this.label8.TabIndex = 16;
            this.label8.Text = "Gender:";
            // 
            // rbMale
            // 
            this.rbMale.AutoSize = true;
            this.rbMale.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbMale.Location = new System.Drawing.Point(144, 111);
            this.rbMale.Name = "rbMale";
            this.rbMale.Size = new System.Drawing.Size(52, 20);
            this.rbMale.TabIndex = 17;
            this.rbMale.TabStop = true;
            this.rbMale.Text = "Male";
            this.rbMale.UseVisualStyleBackColor = true;
            this.rbMale.CheckedChanged += new System.EventHandler(this.rbMale_CheckedChanged);
            // 
            // rbFemale
            // 
            this.rbFemale.AutoSize = true;
            this.rbFemale.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbFemale.Location = new System.Drawing.Point(239, 111);
            this.rbFemale.Name = "rbFemale";
            this.rbFemale.Size = new System.Drawing.Size(67, 20);
            this.rbFemale.TabIndex = 18;
            this.rbFemale.TabStop = true;
            this.rbFemale.Text = "Female";
            this.rbFemale.UseVisualStyleBackColor = true;
            this.rbFemale.CheckedChanged += new System.EventHandler(this.rbFemale_CheckedChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(363, 113);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 16);
            this.label9.TabIndex = 21;
            this.label9.Text = "Phone:";
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(459, 113);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(130, 20);
            this.txtPhone.TabIndex = 22;
            this.txtPhone.Validating += new System.ComponentModel.CancelEventHandler(this.textBox6_Validating);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(17, 152);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(44, 16);
            this.label10.TabIndex = 25;
            this.label10.Text = "Email:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(351, 148);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(64, 16);
            this.label11.TabIndex = 27;
            this.label11.Text = "Country:";
            // 
            // cbCountry
            // 
            this.cbCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCountry.FormattingEnabled = true;
            this.cbCountry.Location = new System.Drawing.Point(459, 147);
            this.cbCountry.Name = "cbCountry";
            this.cbCountry.Size = new System.Drawing.Size(130, 21);
            this.cbCountry.TabIndex = 29;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(17, 190);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(66, 16);
            this.label12.TabIndex = 30;
            this.label12.Text = "Address:";
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(144, 190);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(445, 92);
            this.txtAddress.TabIndex = 31;
            this.txtAddress.Validating += new System.ComponentModel.CancelEventHandler(this.textBox8_Validating);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.Location = new System.Drawing.Point(627, 196);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(56, 13);
            this.linkLabel1.TabIndex = 34;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Set Image";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // lblRemove
            // 
            this.lblRemove.AutoSize = true;
            this.lblRemove.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemove.Location = new System.Drawing.Point(714, 197);
            this.lblRemove.Name = "lblRemove";
            this.lblRemove.Size = new System.Drawing.Size(46, 13);
            this.lblRemove.TabIndex = 35;
            this.lblRemove.TabStop = true;
            this.lblRemove.Text = "Remove";
            this.lblRemove.Visible = false;
            this.lblRemove.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblRemove_LinkClicked);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Image = global::DVLD_Presentation.Properties.Resources.Close_321;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(393, 288);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 28);
            this.button1.TabIndex = 37;
            this.button1.Text = "      Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSave
            // 
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::DVLD_Presentation.Properties.Resources.Save_32;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(494, 288);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(95, 28);
            this.btnSave.TabIndex = 36;
            this.btnSave.Text = "      Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pbPersonImage
            // 
            this.pbPersonImage.Image = global::DVLD_Presentation.Properties.Resources.Male_512;
            this.pbPersonImage.Location = new System.Drawing.Point(630, 76);
            this.pbPersonImage.Name = "pbPersonImage";
            this.pbPersonImage.Size = new System.Drawing.Size(130, 117);
            this.pbPersonImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbPersonImage.TabIndex = 33;
            this.pbPersonImage.TabStop = false;
            // 
            // pictureBox9
            // 
            this.pictureBox9.Image = global::DVLD_Presentation.Properties.Resources.Address_32;
            this.pictureBox9.Location = new System.Drawing.Point(112, 190);
            this.pictureBox9.Name = "pictureBox9";
            this.pictureBox9.Size = new System.Drawing.Size(26, 20);
            this.pictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox9.TabIndex = 32;
            this.pictureBox9.TabStop = false;
            // 
            // pictureBox8
            // 
            this.pictureBox8.Image = global::DVLD_Presentation.Properties.Resources.Country_32;
            this.pictureBox8.Location = new System.Drawing.Point(427, 148);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(26, 20);
            this.pictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox8.TabIndex = 28;
            this.pictureBox8.TabStop = false;
            // 
            // pictureBox7
            // 
            this.pictureBox7.Image = global::DVLD_Presentation.Properties.Resources.Email_32;
            this.pictureBox7.Location = new System.Drawing.Point(112, 148);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(26, 20);
            this.pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox7.TabIndex = 26;
            this.pictureBox7.TabStop = false;
            // 
            // pictureBox6
            // 
            this.pictureBox6.Image = global::DVLD_Presentation.Properties.Resources.Phone_32;
            this.pictureBox6.Location = new System.Drawing.Point(427, 113);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(26, 20);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox6.TabIndex = 23;
            this.pictureBox6.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = global::DVLD_Presentation.Properties.Resources.Woman_32;
            this.pictureBox5.Location = new System.Drawing.Point(207, 111);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(26, 20);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox5.TabIndex = 20;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::DVLD_Presentation.Properties.Resources.Man_32;
            this.pictureBox4.Location = new System.Drawing.Point(112, 111);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(26, 20);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 19;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::DVLD_Presentation.Properties.Resources.Calendar_32;
            this.pictureBox3.Location = new System.Drawing.Point(427, 77);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(26, 20);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 15;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::DVLD_Presentation.Properties.Resources.Number_32;
            this.pictureBox2.Location = new System.Drawing.Point(112, 77);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(26, 20);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 12;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD_Presentation.Properties.Resources.Person_32;
            this.pictureBox1.Location = new System.Drawing.Point(112, 41);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(26, 20);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(144, 148);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(130, 20);
            this.txtEmail.TabIndex = 24;
            this.txtEmail.Validating += new System.ComponentModel.CancelEventHandler(this.txtEmail_Validating);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // ctrlPersonInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblRemove);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.pbPersonImage);
            this.Controls.Add(this.pictureBox9);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.cbCountry);
            this.Controls.Add(this.pictureBox8);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.pictureBox7);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.pictureBox6);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.rbFemale);
            this.Controls.Add(this.rbMale);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.txtNationalNo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtLastName);
            this.Controls.Add(this.txtThirdName);
            this.Controls.Add(this.txtSecondName);
            this.Controls.Add(this.txtFirstName);
            this.Controls.Add(this.label1);
            this.Name = "ctrlPersonInfo";
            this.Size = new System.Drawing.Size(780, 341);
            this.Load += new System.EventHandler(this.ctrlPersonInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPersonImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.TextBox txtSecondName;
        private System.Windows.Forms.TextBox txtThirdName;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtNationalNo;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RadioButton rbMale;
        private System.Windows.Forms.RadioButton rbFemale;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.PictureBox pictureBox8;
        private System.Windows.Forms.ComboBox cbCountry;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.PictureBox pictureBox9;
        private System.Windows.Forms.PictureBox pbPersonImage;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.LinkLabel lblRemove;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

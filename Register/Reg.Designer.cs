namespace Register
{
	partial class Reg
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			panel1 = new Panel();
			label1 = new Label();
			panel2 = new Panel();
			label10 = new Label();
			label9 = new Label();
			districtComboBox = new ComboBox();
			cityComboBox = new ComboBox();
			phoneTextBox = new TextBox();
			nameTextBox = new TextBox();
			addressTextBox = new TextBox();
			birthdayTextBox = new TextBox();
			idNumberTextBox = new TextBox();
			genderComboBox = new ComboBox();
			label8 = new Label();
			label7 = new Label();
			label6 = new Label();
			ageTextBox = new TextBox();
			label5 = new Label();
			label4 = new Label();
			label3 = new Label();
			label2 = new Label();
			panel3 = new Panel();
			groupBox1 = new GroupBox();
			regNumberTextBox = new TextBox();
			label14 = new Label();
			doctorComboBox = new ComboBox();
			label13 = new Label();
			label12 = new Label();
			departmentComboBox = new ComboBox();
			regDateTextBox = new TextBox();
			label11 = new Label();
			panel4 = new Panel();
			printButton = new Button();
			exitButton = new Button();
			clearButton = new Button();
			confirmButton = new Button();
			label15 = new Label();
			panel1.SuspendLayout();
			panel2.SuspendLayout();
			panel3.SuspendLayout();
			groupBox1.SuspendLayout();
			panel4.SuspendLayout();
			SuspendLayout();
			// 
			// panel1
			// 
			panel1.BackColor = Color.FromArgb(0, 90, 156);
			panel1.Controls.Add(label1);
			panel1.Location = new Point(100, 20);
			panel1.Name = "panel1";
			panel1.Size = new Size(1000, 100);
			panel1.TabIndex = 0;
			// 
			// label1
			// 
			label1.Font = new Font("微軟正黑體", 22.2F, FontStyle.Bold, GraphicsUnit.Point);
			label1.ForeColor = Color.White;
			label1.Location = new Point(330, 25);
			label1.Name = "label1";
			label1.Size = new Size(323, 39);
			label1.TabIndex = 0;
			label1.Text = "憨吉聯合大醫院";
			// 
			// panel2
			// 
			panel2.BackColor = Color.FromArgb(230, 247, 255);
			panel2.Controls.Add(label15);
			panel2.Controls.Add(label10);
			panel2.Controls.Add(label9);
			panel2.Controls.Add(districtComboBox);
			panel2.Controls.Add(cityComboBox);
			panel2.Controls.Add(phoneTextBox);
			panel2.Controls.Add(nameTextBox);
			panel2.Controls.Add(addressTextBox);
			panel2.Controls.Add(birthdayTextBox);
			panel2.Controls.Add(idNumberTextBox);
			panel2.Controls.Add(genderComboBox);
			panel2.Controls.Add(label8);
			panel2.Controls.Add(label7);
			panel2.Controls.Add(label6);
			panel2.Controls.Add(ageTextBox);
			panel2.Controls.Add(label5);
			panel2.Controls.Add(label4);
			panel2.Controls.Add(label3);
			panel2.Controls.Add(label2);
			panel2.Location = new Point(50, 140);
			panel2.Name = "panel2";
			panel2.Size = new Size(1100, 238);
			panel2.TabIndex = 1;
			panel2.Tag = "";
			// 
			// label10
			// 
			label10.BackColor = Color.FromArgb(230, 247, 255);
			label10.BorderStyle = BorderStyle.FixedSingle;
			label10.Font = new Font("微軟正黑體", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
			label10.Location = new Point(610, 185);
			label10.Name = "label10";
			label10.Size = new Size(80, 35);
			label10.TabIndex = 17;
			label10.Text = "市/區";
			label10.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// label9
			// 
			label9.BackColor = Color.FromArgb(230, 247, 255);
			label9.BorderStyle = BorderStyle.FixedSingle;
			label9.Font = new Font("微軟正黑體", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
			label9.Location = new Point(330, 185);
			label9.Name = "label9";
			label9.Size = new Size(80, 35);
			label9.TabIndex = 16;
			label9.Text = "縣/市";
			label9.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// districtComboBox
			// 
			districtComboBox.FormattingEnabled = true;
			districtComboBox.Location = new Point(440, 185);
			districtComboBox.Name = "districtComboBox";
			districtComboBox.Size = new Size(160, 35);
			districtComboBox.TabIndex = 15;
			// 
			// cityComboBox
			// 
			cityComboBox.FormattingEnabled = true;
			cityComboBox.Items.AddRange(new object[] { "台北市", "新北市", "花蓮縣", "宜蘭縣" });
			cityComboBox.Location = new Point(160, 185);
			cityComboBox.Name = "cityComboBox";
			cityComboBox.Size = new Size(160, 35);
			cityComboBox.TabIndex = 14;
			// 
			// phoneTextBox
			// 
			phoneTextBox.BorderStyle = BorderStyle.FixedSingle;
			phoneTextBox.Font = new Font("微軟正黑體", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
			phoneTextBox.Location = new Point(760, 115);
			phoneTextBox.Name = "phoneTextBox";
			phoneTextBox.Size = new Size(200, 35);
			phoneTextBox.TabIndex = 13;
			phoneTextBox.Text = "0939-123456";
			phoneTextBox.TextChanged += PhoneTextBox_TextChanged;
			// 
			// nameTextBox
			// 
			nameTextBox.BorderStyle = BorderStyle.FixedSingle;
			nameTextBox.Font = new Font("微軟正黑體", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
			nameTextBox.Location = new Point(460, 27);
			nameTextBox.Name = "nameTextBox";
			nameTextBox.Size = new Size(180, 35);
			nameTextBox.TabIndex = 12;
			nameTextBox.Text = "趙大同";
			// 
			// addressTextBox
			// 
			addressTextBox.BorderStyle = BorderStyle.FixedSingle;
			addressTextBox.Font = new Font("微軟正黑體", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
			addressTextBox.Location = new Point(700, 185);
			addressTextBox.Name = "addressTextBox";
			addressTextBox.Size = new Size(360, 35);
			addressTextBox.TabIndex = 11;
			addressTextBox.Text = "中央路3段789號";
			// 
			// birthdayTextBox
			// 
			birthdayTextBox.BorderStyle = BorderStyle.FixedSingle;
			birthdayTextBox.Font = new Font("微軟正黑體", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
			birthdayTextBox.Location = new Point(160, 115);
			birthdayTextBox.Name = "birthdayTextBox";
			birthdayTextBox.Size = new Size(160, 35);
			birthdayTextBox.TabIndex = 10;
			birthdayTextBox.Text = "1001120";
			birthdayTextBox.TextChanged += BirthdayTextBox_TextChanged;
			// 
			// idNumberTextBox
			// 
			idNumberTextBox.BorderStyle = BorderStyle.FixedSingle;
			idNumberTextBox.Font = new Font("微軟正黑體", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
			idNumberTextBox.Location = new Point(160, 27);
			idNumberTextBox.Name = "idNumberTextBox";
			idNumberTextBox.Size = new Size(160, 35);
			idNumberTextBox.TabIndex = 9;
			idNumberTextBox.Text = "U123456789";
			idNumberTextBox.TextChanged += IdNumberTextBox_TextChanged;
			// 
			// genderComboBox
			// 
			genderComboBox.Font = new Font("微軟正黑體", 12F, FontStyle.Regular, GraphicsUnit.Point);
			genderComboBox.FormattingEnabled = true;
			genderComboBox.Items.AddRange(new object[] { "0.女", "1.男", "2.中性", "3.未知" });
			genderComboBox.Location = new Point(760, 27);
			genderComboBox.Name = "genderComboBox";
			genderComboBox.Size = new Size(100, 35);
			genderComboBox.TabIndex = 8;
			// 
			// label8
			// 
			label8.BackColor = Color.FromArgb(230, 247, 255);
			label8.BorderStyle = BorderStyle.FixedSingle;
			label8.Font = new Font("微軟正黑體", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
			label8.Location = new Point(680, 115);
			label8.Name = "label8";
			label8.Size = new Size(72, 35);
			label8.TabIndex = 7;
			label8.Text = "電 話";
			label8.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// label7
			// 
			label7.BackColor = Color.FromArgb(230, 247, 255);
			label7.BorderStyle = BorderStyle.FixedSingle;
			label7.Font = new Font("微軟正黑體", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
			label7.Location = new Point(40, 185);
			label7.Name = "label7";
			label7.Size = new Size(108, 35);
			label7.TabIndex = 6;
			label7.Text = "地    址";
			label7.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// label6
			// 
			label6.BackColor = Color.FromArgb(230, 247, 255);
			label6.BorderStyle = BorderStyle.FixedSingle;
			label6.Font = new Font("微軟正黑體", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
			label6.Location = new Point(430, 115);
			label6.Name = "label6";
			label6.Size = new Size(36, 35);
			label6.TabIndex = 5;
			label6.Text = "歲";
			label6.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// ageTextBox
			// 
			ageTextBox.BackColor = Color.FromArgb(255, 192, 128);
			ageTextBox.Location = new Point(360, 115);
			ageTextBox.Name = "ageTextBox";
			ageTextBox.Size = new Size(60, 35);
			ageTextBox.TabIndex = 4;
			ageTextBox.Text = "15";
			ageTextBox.TextAlign = HorizontalAlignment.Center;
			// 
			// label5
			// 
			label5.BackColor = Color.FromArgb(230, 247, 255);
			label5.BorderStyle = BorderStyle.FixedSingle;
			label5.Font = new Font("微軟正黑體", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
			label5.Location = new Point(680, 27);
			label5.Name = "label5";
			label5.Size = new Size(72, 35);
			label5.TabIndex = 3;
			label5.Text = "性 別";
			label5.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// label4
			// 
			label4.BackColor = Color.FromArgb(230, 247, 255);
			label4.BorderStyle = BorderStyle.FixedSingle;
			label4.Font = new Font("微軟正黑體", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
			label4.Location = new Point(40, 115);
			label4.Name = "label4";
			label4.Size = new Size(108, 35);
			label4.TabIndex = 2;
			label4.Text = "生    日";
			label4.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// label3
			// 
			label3.BackColor = Color.FromArgb(230, 247, 255);
			label3.BorderStyle = BorderStyle.FixedSingle;
			label3.Font = new Font("微軟正黑體", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
			label3.Location = new Point(360, 27);
			label3.Name = "label3";
			label3.Size = new Size(96, 35);
			label3.TabIndex = 1;
			label3.Text = "姓   名";
			label3.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// label2
			// 
			label2.BackColor = Color.FromArgb(230, 247, 255);
			label2.BorderStyle = BorderStyle.FixedSingle;
			label2.Font = new Font("微軟正黑體", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
			label2.Location = new Point(40, 27);
			label2.Name = "label2";
			label2.Size = new Size(108, 35);
			label2.TabIndex = 0;
			label2.Text = "病歷號碼";
			label2.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// panel3
			// 
			panel3.BackColor = Color.FromArgb(230, 247, 255);
			panel3.Controls.Add(groupBox1);
			panel3.Controls.Add(doctorComboBox);
			panel3.Controls.Add(label13);
			panel3.Controls.Add(label12);
			panel3.Controls.Add(departmentComboBox);
			panel3.Controls.Add(regDateTextBox);
			panel3.Controls.Add(label11);
			panel3.Location = new Point(50, 395);
			panel3.Name = "panel3";
			panel3.Size = new Size(1100, 295);
			panel3.TabIndex = 2;
			// 
			// groupBox1
			// 
			groupBox1.BackColor = Color.White;
			groupBox1.Controls.Add(regNumberTextBox);
			groupBox1.Controls.Add(label14);
			groupBox1.Location = new Point(160, 80);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new Size(780, 160);
			groupBox1.TabIndex = 15;
			groupBox1.TabStop = false;
			// 
			// regNumberTextBox
			// 
			regNumberTextBox.BorderStyle = BorderStyle.FixedSingle;
			regNumberTextBox.Font = new Font("微軟正黑體", 25.8000011F, FontStyle.Regular, GraphicsUnit.Point);
			regNumberTextBox.ForeColor = Color.Red;
			regNumberTextBox.Location = new Point(360, 40);
			regNumberTextBox.Name = "regNumberTextBox";
			regNumberTextBox.Size = new Size(160, 70);
			regNumberTextBox.TabIndex = 11;
			regNumberTextBox.Text = "12";
			regNumberTextBox.TextAlign = HorizontalAlignment.Center;
			// 
			// label14
			// 
			label14.BackColor = Color.FromArgb(0, 90, 156);
			label14.BorderStyle = BorderStyle.FixedSingle;
			label14.Font = new Font("微軟正黑體", 18F, FontStyle.Bold, GraphicsUnit.Point);
			label14.ForeColor = Color.White;
			label14.Location = new Point(160, 55);
			label14.Name = "label14";
			label14.Size = new Size(180, 40);
			label14.TabIndex = 1;
			label14.Text = "掛號的號碼";
			label14.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// doctorComboBox
			// 
			doctorComboBox.Font = new Font("微軟正黑體", 12F, FontStyle.Regular, GraphicsUnit.Point);
			doctorComboBox.FormattingEnabled = true;
			doctorComboBox.Items.AddRange(new object[] { "101.趙一", "102.錢二", "103.孫三", "201.李四", "202.周五", "203.吳六", "301.鄭七", "302.王八", "303.馮九", "401.陳十" });
			doctorComboBox.Location = new Point(760, 17);
			doctorComboBox.Name = "doctorComboBox";
			doctorComboBox.Size = new Size(160, 35);
			doctorComboBox.TabIndex = 14;
			// 
			// label13
			// 
			label13.BackColor = Color.FromArgb(0, 90, 156);
			label13.BorderStyle = BorderStyle.FixedSingle;
			label13.Font = new Font("微軟正黑體", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
			label13.ForeColor = Color.White;
			label13.Location = new Point(660, 20);
			label13.Name = "label13";
			label13.Size = new Size(100, 35);
			label13.TabIndex = 13;
			label13.Text = "醫 師";
			label13.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// label12
			// 
			label12.BackColor = Color.FromArgb(0, 90, 156);
			label12.BorderStyle = BorderStyle.FixedSingle;
			label12.Font = new Font("微軟正黑體", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
			label12.ForeColor = Color.White;
			label12.Location = new Point(360, 20);
			label12.Name = "label12";
			label12.Size = new Size(100, 35);
			label12.TabIndex = 12;
			label12.Text = "科   別";
			label12.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// departmentComboBox
			// 
			departmentComboBox.Font = new Font("微軟正黑體", 12F, FontStyle.Regular, GraphicsUnit.Point);
			departmentComboBox.FormattingEnabled = true;
			departmentComboBox.Items.AddRange(new object[] { "1.內科", "2.外科", "3.皮膚科", "4.眼科", "5.牙科", "6.小兒科", "7.婦產科" });
			departmentComboBox.Location = new Point(460, 17);
			departmentComboBox.Name = "departmentComboBox";
			departmentComboBox.Size = new Size(160, 35);
			departmentComboBox.TabIndex = 11;
			// 
			// regDateTextBox
			// 
			regDateTextBox.BorderStyle = BorderStyle.FixedSingle;
			regDateTextBox.Font = new Font("微軟正黑體", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
			regDateTextBox.Location = new Point(160, 17);
			regDateTextBox.Name = "regDateTextBox";
			regDateTextBox.Size = new Size(160, 35);
			regDateTextBox.TabIndex = 10;
			regDateTextBox.Text = "U123456789";
			// 
			// label11
			// 
			label11.BackColor = Color.FromArgb(0, 90, 156);
			label11.BorderStyle = BorderStyle.FixedSingle;
			label11.Font = new Font("微軟正黑體", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
			label11.ForeColor = Color.White;
			label11.Location = new Point(40, 20);
			label11.Name = "label11";
			label11.Size = new Size(120, 35);
			label11.TabIndex = 1;
			label11.Text = "掛號日期";
			label11.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// panel4
			// 
			panel4.BackColor = Color.FromArgb(0, 90, 156);
			panel4.Controls.Add(printButton);
			panel4.Controls.Add(exitButton);
			panel4.Controls.Add(clearButton);
			panel4.Controls.Add(confirmButton);
			panel4.Location = new Point(100, 700);
			panel4.Name = "panel4";
			panel4.Size = new Size(1000, 110);
			panel4.TabIndex = 3;
			// 
			// printButton
			// 
			printButton.Font = new Font("微軟正黑體", 13F, FontStyle.Regular, GraphicsUnit.Point);
			printButton.Location = new Point(280, 28);
			printButton.Name = "printButton";
			printButton.Size = new Size(200, 55);
			printButton.TabIndex = 3;
			printButton.Text = "列印掛號單";
			printButton.UseVisualStyleBackColor = true;
			printButton.Click += PrintButton_Click;
			// 
			// exitButton
			// 
			exitButton.Font = new Font("微軟正黑體", 13F, FontStyle.Regular, GraphicsUnit.Point);
			exitButton.Location = new Point(720, 28);
			exitButton.Name = "exitButton";
			exitButton.Size = new Size(200, 55);
			exitButton.TabIndex = 2;
			exitButton.Text = "結束/離開";
			exitButton.UseVisualStyleBackColor = true;
			exitButton.Click += ExitButton_Click;
			// 
			// clearButton
			// 
			clearButton.Font = new Font("微軟正黑體", 13F, FontStyle.Regular, GraphicsUnit.Point);
			clearButton.Location = new Point(500, 28);
			clearButton.Name = "clearButton";
			clearButton.Size = new Size(200, 55);
			clearButton.TabIndex = 1;
			clearButton.Text = "清空螢幕";
			clearButton.UseVisualStyleBackColor = true;
			clearButton.Click += ClearButton_Click;
			// 
			// confirmButton
			// 
			confirmButton.Font = new Font("微軟正黑體", 13F, FontStyle.Regular, GraphicsUnit.Point);
			confirmButton.Location = new Point(60, 28);
			confirmButton.Name = "confirmButton";
			confirmButton.Size = new Size(200, 55);
			confirmButton.TabIndex = 0;
			confirmButton.Text = "掛號確認";
			confirmButton.UseVisualStyleBackColor = true;
			confirmButton.Click += ConfirmButton_Click;
			// 
			// label15
			// 
			label15.BackColor = Color.FromArgb(0, 90, 156);
			label15.BorderStyle = BorderStyle.FixedSingle;
			label15.Font = new Font("微軟正黑體", 12F, FontStyle.Regular, GraphicsUnit.Point);
			label15.ForeColor = Color.White;
			label15.Location = new Point(870, 27);
			label15.Name = "label15";
			label15.Size = new Size(160, 35);
			label15.TabIndex = 18;
			label15.Text = "初診/複診";
			label15.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// Reg
			// 
			AutoScaleDimensions = new SizeF(9F, 19F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.FromArgb(240, 242, 245);
			ClientSize = new Size(1200, 850);
			Controls.Add(panel4);
			Controls.Add(panel3);
			Controls.Add(panel2);
			Controls.Add(panel1);
			Font = new Font("微軟正黑體", 9F, FontStyle.Regular, GraphicsUnit.Point);
			Name = "Reg";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "憨吉聯合大醫院 - 掛號系統";
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			panel2.ResumeLayout(false);
			panel2.PerformLayout();
			panel3.ResumeLayout(false);
			panel3.PerformLayout();
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			panel4.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		private Panel panel1;
		private Panel panel2;
		private Panel panel3;
		private Panel panel4;
		private Label label1;
		private Label label2;
		private TextBox ageTextBox;
		private Label label5;
		private Label label4;
		private Label label3;
		private Label label8;
		private Label label7;
		private Label label6;
		private ComboBox genderComboBox;
		private TextBox idNumberTextBox;
		private TextBox phoneTextBox;
		private TextBox nameTextBox;
		private TextBox addressTextBox;
		private TextBox birthdayTextBox;
		private Label label9;
		private ComboBox districtComboBox;
		private ComboBox cityComboBox;
		private Label label10;
		private TextBox regDateTextBox;
		private Label label11;
		private Label label12;
		private ComboBox departmentComboBox;
		private ComboBox doctorComboBox;
		private Label label13;
		private Button confirmButton;
		private Button exitButton;
		private Button clearButton;
		private GroupBox groupBox1;
		private Label label14;
		private TextBox regNumberTextBox;
		private Button printButton;
		private Label label15;
	}
}
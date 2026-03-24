namespace Register
{
	public class ComboItem
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public override string ToString() => Name;
	}

	public partial class Reg : Form
	{
		public Reg()
		{
			InitializeComponent();
			LoadInitialData();
		}

		private void LoadInitialData()
		{
			departmentComboBox.Items.Clear();
			timeSlotComboBox.Items.Clear();
			
			using (var conn = new Microsoft.Data.SqlClient.SqlConnection(DatabaseHelper.ConnectionString))
			{
				conn.Open();
				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = "SELECT Id, Name FROM Departments";
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							departmentComboBox.Items.Add(new ComboItem { Id = reader.GetInt32(0), Name = reader.GetString(1) });
						}
					}
				}
				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = "SELECT Id, Name FROM TimeSlots";
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							timeSlotComboBox.Items.Add(new ComboItem { Id = reader.GetInt32(0), Name = reader.GetString(1) });
						}
					}
				}
			}

			departmentComboBox.DisplayMember = "Name";
			departmentComboBox.ValueMember = "Id";
			timeSlotComboBox.DisplayMember = "Name";
			timeSlotComboBox.ValueMember = "Id";
			doctorComboBox.DisplayMember = "Name";
			doctorComboBox.ValueMember = "Id";

			genderComboBox.SelectedIndex = 0;

			// 載入縣市列表
			cityComboBox.Items.Clear();
			using (var conn = new Microsoft.Data.SqlClient.SqlConnection(DatabaseHelper.ConnectionString))
			{
				conn.Open();
				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = "SELECT Id, Name FROM Cities";
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							cityComboBox.Items.Add(new ComboItem { Id = reader.GetInt32(0), Name = reader.GetString(1) });
						}
					}
				}
			}
			cityComboBox.DisplayMember = "Name";
			cityComboBox.ValueMember = "Id";
			districtComboBox.DisplayMember = "Name";
			districtComboBox.ValueMember = "Id";
			
			departmentComboBox.SelectedIndexChanged += DepartmentOrTimeSlot_Changed;
			timeSlotComboBox.SelectedIndexChanged += DepartmentOrTimeSlot_Changed;
			doctorComboBox.SelectedIndexChanged += Doctor_Changed;
			cityComboBox.SelectedIndexChanged += City_Changed;
			
			// 綁定查詢按鈕
			btnCheckId.Click += BtnCheckId_Click;

			if (cityComboBox.Items.Count > 0)
			{
				cityComboBox.SelectedIndex = 0; // 這將會觸發 City_Changed，自動載入轄下行政區
			}
			
			// 初始鎖定所有輸入框，要求先輸入身分證
			SetFormState(false);
		}

		private void SetFormState(bool enabled)
		{
			// 基本資料區塊
			nameTextBox.Enabled = enabled;
			genderComboBox.Enabled = enabled;
			birthdayTextBox.Enabled = enabled;
			phoneTextBox.Enabled = enabled;
			cityComboBox.Enabled = enabled;
			districtComboBox.Enabled = enabled;
			addressTextBox.Enabled = enabled;

			// 掛號資訊區塊
			regDateTextBox.Enabled = enabled;
			departmentComboBox.Enabled = enabled;
			timeSlotComboBox.Enabled = enabled;
			doctorComboBox.Enabled = enabled;

			// 確認按鈕
			confirmButton.Enabled = enabled;
		}

		private void City_Changed(object sender, EventArgs e)
		{
			districtComboBox.Items.Clear();
			if (cityComboBox.SelectedItem == null) return;
			var city = (ComboItem)cityComboBox.SelectedItem;
			using (var conn = new Microsoft.Data.SqlClient.SqlConnection(DatabaseHelper.ConnectionString))
			{
				conn.Open();
				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = "SELECT Id, Name FROM Districts WHERE CityId = @cId";
					cmd.Parameters.AddWithValue("@cId", city.Id);
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							districtComboBox.Items.Add(new ComboItem { Id = reader.GetInt32(0), Name = reader.GetString(1) });
						}
					}
				}
			}
			if (districtComboBox.Items.Count > 0) districtComboBox.SelectedIndex = 0;
		}

		private void DepartmentOrTimeSlot_Changed(object sender, EventArgs e)
		{
			doctorComboBox.Items.Clear();
			if (departmentComboBox.SelectedItem == null || timeSlotComboBox.SelectedItem == null)
				return;
				
			var dept = (ComboItem)departmentComboBox.SelectedItem;
			var time = (ComboItem)timeSlotComboBox.SelectedItem;

			using (var conn = new Microsoft.Data.SqlClient.SqlConnection(DatabaseHelper.ConnectionString))
			{
				conn.Open();
				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = @"
						SELECT d.Id, d.Name 
						FROM Doctors d
						JOIN DoctorSchedules ds ON d.Id = ds.DoctorId
						WHERE d.DepartmentId = @deptId AND ds.TimeSlotId = @timeId";
					cmd.Parameters.AddWithValue("@deptId", dept.Id);
					cmd.Parameters.AddWithValue("@timeId", time.Id);

					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							doctorComboBox.Items.Add(new ComboItem { Id = reader.GetInt32(0), Name = reader.GetString(1) });
						}
					}
				}
			}
		}

		private void Doctor_Changed(object sender, EventArgs e)
		{
			UpdateWaitingNumber();
		}

		private void UpdateWaitingNumber()
		{
			if (doctorComboBox.SelectedItem == null || timeSlotComboBox.SelectedItem == null)
            {
				regNumberTextBox.Text = "";
				return;
            }

			var doc = (ComboItem)doctorComboBox.SelectedItem;
			var time = (ComboItem)timeSlotComboBox.SelectedItem;
			string regDate = string.IsNullOrWhiteSpace(regDateTextBox.Text) ? DateTime.Now.ToString("yyyy-MM-dd") : regDateTextBox.Text.Trim();

			using (var conn = new Microsoft.Data.SqlClient.SqlConnection(DatabaseHelper.ConnectionString))
			{
				conn.Open();
				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = @"
						SELECT MAX(RegNumber) 
						FROM Registrations 
						WHERE DoctorId = @doctorId AND TimeSlotId = @timeId AND RegDate = @regDate";
					cmd.Parameters.AddWithValue("@doctorId", doc.Id);
					cmd.Parameters.AddWithValue("@timeId", time.Id);
					cmd.Parameters.AddWithValue("@regDate", regDate);

					var result = cmd.ExecuteScalar();
					int maxNum = 0;
					if (result != DBNull.Value && result != null)
					{
						maxNum = Convert.ToInt32(result);
					}
					regNumberTextBox.Text = (maxNum + 1).ToString();
				}
			}
		}

		private void IdNumberTextBox_TextChanged(object sender, EventArgs e)
		{
		}

		private void BtnCheckId_Click(object sender, EventArgs e)
		{
			string nid = nationalIdTextBox.Text.Trim();
			if (string.IsNullOrWhiteSpace(nid))
			{
				MessageBox.Show("請輸入身分證或居留證號碼！", "系統提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			using (var conn = new Microsoft.Data.SqlClient.SqlConnection(DatabaseHelper.ConnectionString))
			{
				conn.Open();
				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = "SELECT MedicalRecordNo, Name, Gender, BirthDate, Phone, Address FROM Patients WHERE NationalId = @nid";
					cmd.Parameters.AddWithValue("@nid", nid);
					using (var reader = cmd.ExecuteReader())
					{
						if (reader.Read())
						{
							// 查有此人，自動帶出舊有資料
							idNumberTextBox.Text = reader.IsDBNull(0) ? "" : reader.GetString(0);
							nameTextBox.Text = reader.IsDBNull(1) ? "" : reader.GetString(1);
							
							string gender = reader.IsDBNull(2) ? "" : reader.GetString(2);
							if (!string.IsNullOrEmpty(gender))
							{
								int idx = genderComboBox.FindStringExact(gender);
								if (idx >= 0) genderComboBox.SelectedIndex = idx;
							}
							
							birthdayTextBox.Text = reader.IsDBNull(3) ? "" : reader.GetString(3);
							phoneTextBox.Text = reader.IsDBNull(4) ? "" : reader.GetString(4);
							addressTextBox.Text = reader.IsDBNull(5) ? "" : reader.GetString(5);
							
							// 解鎖畫面允許掛號
							SetFormState(true);
						}
						else
						{
							// 查無此人 (初診)，清空基本資料讓櫃台填寫
							idNumberTextBox.Text = "";
							nameTextBox.Text = "";
							birthdayTextBox.Text = "";
							ageTextBox.Text = "";
							phoneTextBox.Text = "";
							addressTextBox.Text = "";
							genderComboBox.SelectedIndex = -1;
							
							// 跳出提示並解鎖與導向姓名欄位建檔
							MessageBox.Show("系統查無此人，請建立初診病患資料！", "初診建檔", MessageBoxButtons.OK, MessageBoxIcon.Information);
							SetFormState(true);
							nameTextBox.Focus();
						}
					}
				}
			}
		}

		private void BirthdayTextBox_TextChanged(object sender, EventArgs e)
		{
			string input = birthdayTextBox.Text.Replace("_", "").Replace(" ", "").Trim();
			if (input.Length == 7) // 民國年 YYYMMDD
			{
				try
				{
					string yearStr = input.Substring(0, 3);
					if (int.TryParse(yearStr, out int rocYear))
					{
						int solarYear = rocYear + 1911;
						int currentYear = DateTime.Now.Year;
						int age = currentYear - solarYear;
						if (age >= 0 && age < 150)
						{
							ageTextBox.Text = age.ToString();
						}
					}
				}
				catch { }
			}
		}

		private void PhoneTextBox_TextChanged(object sender, EventArgs e)
		{
		}

		private void ConfirmButton_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(nationalIdTextBox.Text)) {
				MessageBox.Show("身分證/居留證為必填！", "系統訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
			}
			if (doctorComboBox.SelectedItem == null || departmentComboBox.SelectedItem == null || timeSlotComboBox.SelectedItem == null) {
				MessageBox.Show("請完整選擇看診科別、時段與醫師！", "系統訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
			}
			if (string.IsNullOrWhiteSpace(nameTextBox.Text)) {
				MessageBox.Show("姓名為必填！", "系統訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
			}

			string nid = nationalIdTextBox.Text.Trim();
			int patientId = 0;
			string mrNo = "";
			bool isFirstTime = false;
			string regDate = string.IsNullOrWhiteSpace(regDateTextBox.Text) ? DateTime.Now.ToString("yyyy-MM-dd") : regDateTextBox.Text.Trim();

			using (var conn = new Microsoft.Data.SqlClient.SqlConnection(DatabaseHelper.ConnectionString))
			{
				conn.Open();
				
				// 1. 查找此病患是否已存在
				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = "SELECT Id, MedicalRecordNo FROM Patients WHERE NationalId = @nid";
					cmd.Parameters.AddWithValue("@nid", nid);
					using (var reader = cmd.ExecuteReader())
					{
						if (reader.Read())
						{
							patientId = reader.GetInt32(0);
							mrNo = reader.GetString(1);
						}
					}
				}

				if (patientId > 0)
				{
					// 舊病患 (複診)，更新個人資料
					using (var cmd = conn.CreateCommand())
					{
						cmd.CommandText = "UPDATE Patients SET Name=@Name, Gender=@Gender, BirthDate=@BirthDate, Phone=@Phone, Address=@Address WHERE Id=@Id";
						cmd.Parameters.AddWithValue("@Name", nameTextBox.Text.Trim());
						cmd.Parameters.AddWithValue("@Gender", genderComboBox.SelectedItem?.ToString() ?? "");
						cmd.Parameters.AddWithValue("@BirthDate", birthdayTextBox.Text.Trim());
						cmd.Parameters.AddWithValue("@Phone", phoneTextBox.Text.Trim());
						cmd.Parameters.AddWithValue("@Address", $"{cityComboBox.Text}{districtComboBox.Text}{addressTextBox.Text}".Trim());
						cmd.Parameters.AddWithValue("@Id", patientId);
						cmd.ExecuteNonQuery();
					}
					isFirstTime = false;
				}
				else
				{
					// 新病患 (初診)，取得最大病歷號並發配新號 (+1)
					isFirstTime = true;
					int maxMrNo = 0;
					using (var cmd = conn.CreateCommand())
					{
						cmd.CommandText = "SELECT MAX(CAST(MedicalRecordNo AS INT)) FROM Patients WHERE MedicalRecordNo IS NOT NULL AND ISNUMERIC(MedicalRecordNo) = 1";
						var result = cmd.ExecuteScalar();
						if (result != DBNull.Value && result != null)
						{
							maxMrNo = Convert.ToInt32(result);
						}
					}
					maxMrNo++;
					mrNo = maxMrNo.ToString("D3"); // 001 流水編號

					// 新增病患
					using (var cmd = conn.CreateCommand())
					{
						cmd.CommandText = @"INSERT INTO Patients (MedicalRecordNo, NationalId, Name, Gender, BirthDate, Phone, Address) 
											OUTPUT INSERTED.Id 
											VALUES (@MrNo, @Nid, @Name, @Gender, @BirthDate, @Phone, @Address)";
						cmd.Parameters.AddWithValue("@MrNo", mrNo);
						cmd.Parameters.AddWithValue("@Nid", nid);
						cmd.Parameters.AddWithValue("@Name", nameTextBox.Text.Trim());
						cmd.Parameters.AddWithValue("@Gender", genderComboBox.SelectedItem?.ToString() ?? "");
						cmd.Parameters.AddWithValue("@BirthDate", birthdayTextBox.Text.Trim());
						cmd.Parameters.AddWithValue("@Phone", phoneTextBox.Text.Trim());
						cmd.Parameters.AddWithValue("@Address", $"{cityComboBox.Text}{districtComboBox.Text}{addressTextBox.Text}".Trim());
						patientId = (int)cmd.ExecuteScalar();
					}
				}

				// 自動填上病歷號
				idNumberTextBox.Text = mrNo;

				int waitNum = 1;
				if (int.TryParse(regNumberTextBox.Text, out int parsedNum)) waitNum = parsedNum;

				var doc = (ComboItem)doctorComboBox.SelectedItem;
				var time = (ComboItem)timeSlotComboBox.SelectedItem;

				// 2. 新增掛號記錄
				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = @"INSERT INTO Registrations (PatientId, DoctorId, TimeSlotId, RegDate, RegNumber, IsFirstTime)
										VALUES (@PId, @DId, @TId, @Date, @RNum, @IsFirst)";
					cmd.Parameters.AddWithValue("@PId", patientId);
					cmd.Parameters.AddWithValue("@DId", doc.Id);
					cmd.Parameters.AddWithValue("@TId", time.Id);
					cmd.Parameters.AddWithValue("@Date", regDate);
					cmd.Parameters.AddWithValue("@RNum", waitNum);
					cmd.Parameters.AddWithValue("@IsFirst", isFirstTime);
					cmd.ExecuteNonQuery();
				}
			}

			MessageBox.Show($"{nameTextBox.Text} 先生/女士，您已掛號成功！\n病歷號碼：{mrNo}\n看診號嗎：{regNumberTextBox.Text}", "掛號完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
			UpdateWaitingNumber();
		}

		private void ClearButton_Click(object sender, EventArgs e)
		{
			idNumberTextBox.Clear();
			nationalIdTextBox.Clear();
			nameTextBox.Clear();
			birthdayTextBox.Clear();
			ageTextBox.Clear();
			phoneTextBox.Clear();
			addressTextBox.Clear();
			regDateTextBox.Clear();
			regNumberTextBox.Clear();
			
			genderComboBox.SelectedIndex = 0;
			if (cityComboBox.Items.Count > 0) cityComboBox.SelectedIndex = 0;
			departmentComboBox.SelectedIndex = -1;
			timeSlotComboBox.SelectedIndex = -1;
			doctorComboBox.SelectedIndex = -1;
			confirmButton.Enabled = false;
			
			// 恢復初始狀態：要求先搜身分證
			SetFormState(false);
			nationalIdTextBox.Focus();
		}

		private void ExitButton_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void PrintButton_Click(object sender, EventArgs e)
		{
			MessageBox.Show("正在列印掛號單...", "列印中", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void BtnSchedule_Click(object sender, EventArgs e)
		{
			new DoctorScheduleForm().ShowDialog();
		}

		private void BtnPatients_Click(object sender, EventArgs e)
		{
			new PatientManagementForm().ShowDialog();
		}

		private void BtnDept_Click(object sender, EventArgs e)
		{
			new DepartmentManagementForm().ShowDialog();
		}

		private void BtnReport_Click(object sender, EventArgs e)
		{
			new ReportForm().ShowDialog();
		}
	}
}

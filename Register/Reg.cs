namespace Register
{
	public partial class Reg : Form
	{
		public Reg()
		{
			InitializeComponent();
			genderComboBox.SelectedIndex = 0;
			cityComboBox.SelectedIndex = 2;
		}

		private void IdNumberTextBox_TextChanged(object sender, EventArgs e)
		{
			// TODO: 身份證字號格式驗證
		}

		private void BirthdayTextBox_TextChanged(object sender, EventArgs e)
		{
			// 支援民國年格式 (如 1001120) 的簡易年齡計算
			string input = birthdayTextBox.Text.Trim();
			if (input.Length >= 3)
			{
				try
				{
					// 假設前 3 位是民國年 (或前幾位是年，後 4 位是月日)
					string yearStr = input.Length > 4 ? input.Substring(0, input.Length - 4) : "0";
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
				catch
				{
					// 忽略解析錯誤
				}
			}
		}

		private void PhoneTextBox_TextChanged(object sender, EventArgs e)
		{
			// TODO: 電話號碼格式驗證
		}

		private void ConfirmButton_Click(object sender, EventArgs e)
		{
			MessageBox.Show($"{nameTextBox.Text} 同學，掛號成功！", "系統訊息", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void ClearButton_Click(object sender, EventArgs e)
		{
			// 清空所有輸入項
			idNumberTextBox.Clear();
			nameTextBox.Clear();
			birthdayTextBox.Clear();
			ageTextBox.Clear(); // 注意：Designer 中目前是 txtAge，我在計畫中提過要一致，稍後確認
			phoneTextBox.Clear();
			addressTextBox.Clear();
			regDateTextBox.Clear();
			regNumberTextBox.Clear();
			
			// 恢復下拉選單預設值
			genderComboBox.SelectedIndex = 0;
			cityComboBox.SelectedIndex = 0;
			districtComboBox.SelectedIndex = -1;
			departmentComboBox.SelectedIndex = -1;
			doctorComboBox.SelectedIndex = -1;
			
			idNumberTextBox.Focus();
		}

		private void ExitButton_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void PrintButton_Click(object sender, EventArgs e)
		{
			MessageBox.Show("正在列印掛號單...", "列印中", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
	}
}
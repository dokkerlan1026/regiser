using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Register
{
	public class PatientManagementForm : Form
	{
		private DataGridView dgv;
		public PatientManagementForm()
		{
			this.Text = "掛號與病患資料管理";
			this.Size = new Size(1000, 600);
			this.StartPosition = FormStartPosition.CenterParent;

			dgv = new DataGridView
			{
				Dock = DockStyle.Fill,
				AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
				AllowUserToAddRows = false,
				ReadOnly = true
			};
			this.Controls.Add(dgv);

			LoadData();
		}

		private void LoadData()
		{
			using (var conn = new SqlConnection(DatabaseHelper.ConnectionString))
			{
				conn.Open();
				// 從資料庫中讀出病患與掛號資料。
				string query = @"
					SELECT p.MedicalRecordNo AS '病歷號',
						   p.NationalId AS '身分證號',
						   p.Name AS '姓名',
						   p.Gender AS '性別',
						   p.BirthDate AS '生日',
						   r.RegDate AS '掛號日期',
						   d.Name AS '看診醫師',
						   t.Name AS '時段'
					FROM Patients p
					LEFT JOIN Registrations r ON p.Id = r.PatientId
					LEFT JOIN Doctors d ON r.DoctorId = d.Id
					LEFT JOIN TimeSlots t ON r.TimeSlotId = t.Id
					ORDER BY p.Id DESC";
					
				try
				{
					using (var cmd = conn.CreateCommand())
					{
						cmd.CommandText = query;
						using (var reader = cmd.ExecuteReader())
						{
							DataTable dt = new DataTable();
							dt.Load(reader);
							dgv.DataSource = dt;
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("讀取資料失敗: " + ex.Message);
				}
			}
		}
	}
}

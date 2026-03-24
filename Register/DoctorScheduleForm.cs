using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Register
{
	public class DoctorScheduleForm : Form
	{
		private DataGridView dgv;
		public DoctorScheduleForm()
		{
			this.Text = "醫師排班管理";
			this.Size = new Size(800, 600);
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
				string query = @"
					SELECT ds.Id AS '排班代碼', d.Name AS '醫師姓名', dept.Name AS '科別', t.Name AS '時段'
					FROM DoctorSchedules ds
					JOIN Doctors d ON ds.DoctorId = d.Id
					JOIN Departments dept ON d.DepartmentId = dept.Id
					JOIN TimeSlots t ON ds.TimeSlotId = t.Id
					ORDER BY dept.Id, d.Id, t.Id";
				
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
		}
	}
}

using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Register
{
	public class ReportForm : Form
	{
		private DataGridView dgv;
		public ReportForm()
		{
			this.Text = "各科別與醫師掛號統計報表";
			this.Size = new Size(800, 500);
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
					SELECT dept.Name AS '科別名稱',
						   d.Name AS '醫師名稱',
						   COUNT(r.Id) AS '總掛號人數'
					FROM Departments dept
					LEFT JOIN Doctors d ON dept.Id = d.DepartmentId
					LEFT JOIN Registrations r ON d.Id = r.DoctorId
					GROUP BY dept.Name, d.Name
					ORDER BY dept.Name, d.Name";
				
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

using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Register
{
	public class DepartmentManagementForm : Form
	{
		private DataGridView dgv;
		public DepartmentManagementForm()
		{
			this.Text = "科別與醫師管理";
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
					SELECT dept.Id AS '科別代碼', 
						   dept.Name AS '科別名稱',
						   STRING_AGG(d.Name, ', ') AS '所屬醫師'
					FROM Departments dept
					LEFT JOIN Doctors d ON dept.Id = d.DepartmentId
					GROUP BY dept.Id, dept.Name
					ORDER BY dept.Id";
				
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

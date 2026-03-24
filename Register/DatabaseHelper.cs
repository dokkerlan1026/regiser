using System;
using System.IO;
using Microsoft.Data.SqlClient;

namespace Register
{
    public static class DatabaseHelper
    {
        private const string DbName = "ClinicDB";
        private static string MasterConnectionString => "Server=.;Database=master;Integrated Security=True;TrustServerCertificate=True;";
        public static string ConnectionString => $"Server=.;Database={DbName};Integrated Security=True;TrustServerCertificate=True;";

        public static void InitializeDatabase()
        {
            bool isNew = false;
            
            // 1. 確認並建立 Database
            using (var masterConn = new SqlConnection(MasterConnectionString))
            {
                masterConn.Open();
                using (var cmd = masterConn.CreateCommand())
                {
                    cmd.CommandText = $"SELECT count(*) FROM sys.databases WHERE name = '{DbName}'";
                    int count = (int)cmd.ExecuteScalar();
                    if (count == 0)
                    {
                        cmd.CommandText = $"CREATE DATABASE {DbName}";
                        cmd.ExecuteNonQuery();
                        isNew = true;
                    }
                }
            }

            // 2. 切換連線進入該資料庫，並建立 Tables
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                // 建立城市表
                var createCities = @"
                    IF OBJECT_ID(N'Cities', N'U') IS NULL
                    CREATE TABLE Cities (
                        Id INT PRIMARY KEY IDENTITY(1,1),
                        Name NVARCHAR(50) NOT NULL
                    );";
                
                // 建立行政區表
                var createDistricts = @"
                    IF OBJECT_ID(N'Districts', N'U') IS NULL
                    CREATE TABLE Districts (
                        Id INT PRIMARY KEY IDENTITY(1,1),
                        CityId INT REFERENCES Cities(Id),
                        Name NVARCHAR(50) NOT NULL
                    );";

                // 建立科別表
                var createDepartments = @"
                    IF OBJECT_ID(N'Departments', N'U') IS NULL
                    CREATE TABLE Departments (
                        Id INT PRIMARY KEY IDENTITY(1,1),
                        Name NVARCHAR(50) NOT NULL
                    );";

                // 建立時段表
                var createTimeSlots = @"
                    IF OBJECT_ID(N'TimeSlots', N'U') IS NULL
                    CREATE TABLE TimeSlots (
                        Id INT PRIMARY KEY IDENTITY(1,1),
                        Name NVARCHAR(50) NOT NULL
                    );";

                // 建立醫生表
                var createDoctors = @"
                    IF OBJECT_ID(N'Doctors', N'U') IS NULL
                    CREATE TABLE Doctors (
                        Id INT PRIMARY KEY IDENTITY(1,1),
                        Name NVARCHAR(50) NOT NULL,
                        DepartmentId INT,
                        FOREIGN KEY (DepartmentId) REFERENCES Departments(Id)
                    );";

                // 建立醫生排班表
                var createDoctorSchedules = @"
                    IF OBJECT_ID(N'DoctorSchedules', N'U') IS NULL
                    CREATE TABLE DoctorSchedules (
                        Id INT PRIMARY KEY IDENTITY(1,1),
                        DoctorId INT,
                        TimeSlotId INT,
                        FOREIGN KEY (DoctorId) REFERENCES Doctors(Id),
                        FOREIGN KEY (TimeSlotId) REFERENCES TimeSlots(Id)
                    );";

                // 建立病患表
                var createPatients = @"
                    IF OBJECT_ID(N'Patients', N'U') IS NULL
                    CREATE TABLE Patients (
                        Id INT PRIMARY KEY IDENTITY(1,1),
                        MedicalRecordNo NVARCHAR(50) UNIQUE,
                        NationalId NVARCHAR(50) UNIQUE,
                        Name NVARCHAR(100) NOT NULL,
                        Gender NVARCHAR(10),
                        BirthDate NVARCHAR(20),
                        Phone NVARCHAR(50),
                        Address NVARCHAR(200)
                    );";

                // 建立掛號表
                var createRegistrations = @"
                    IF OBJECT_ID(N'Registrations', N'U') IS NULL
                    CREATE TABLE Registrations (
                        Id INT PRIMARY KEY IDENTITY(1,1),
                        PatientId INT,
                        DoctorId INT,
                        TimeSlotId INT,
                        RegDate NVARCHAR(50),
                        RegNumber INT,
                        IsFirstTime BIT,
                        FOREIGN KEY (PatientId) REFERENCES Patients(Id),
                        FOREIGN KEY (DoctorId) REFERENCES Doctors(Id),
                        FOREIGN KEY (TimeSlotId) REFERENCES TimeSlots(Id)
                    );";

                ExecuteQuery(connection, createCities);
                ExecuteQuery(connection, createDistricts);
                ExecuteQuery(connection, createDepartments);
                ExecuteQuery(connection, createTimeSlots);
                ExecuteQuery(connection, createDoctors);
                ExecuteQuery(connection, createDoctorSchedules);
                ExecuteQuery(connection, createPatients);
                ExecuteQuery(connection, createRegistrations);

                                if (isNew)
                {
                    SeedData(connection);
                }
                else
                {
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "SELECT COUNT(*) FROM Cities";
                        int cityCount = (int)cmd.ExecuteScalar();
                        if (cityCount == 0)
                        {
                            var insertCities = "INSERT INTO Cities (Name) VALUES (N'臺北市'), (N'新北市'), (N'桃園市'), (N'臺中市'), (N'臺南市'), (N'高雄市');";
                            ExecuteQuery(connection, insertCities);
                            var insertDistricts = @"
                                INSERT INTO Districts (CityId, Name) VALUES 
                                (1, N'中正區'), (1, N'大同區'), (1, N'中山區'), (1, N'松山區'), (1, N'大安區'), (1, N'信義區'),
                                (2, N'板橋區'), (2, N'三重區'), (2, N'中和區'), (2, N'永和區'), (2, N'新莊區'), (2, N'新店區'),
                                (3, N'桃園區'), (3, N'中壢區'), (3, N'平鎮區'), (3, N'八德區'),
                                (4, N'中區'), (4, N'東區'), (4, N'南區'), (4, N'西區'), (4, N'北區'), (4, N'西屯區'), (4, N'南屯區'),
                                (5, N'東區'), (5, N'南區'), (5, N'中西區'), (5, N'北區'), (5, N'安平區'),
                                (6, N'鹽埕區'), (6, N'鼓山區'), (6, N'左營區'), (6, N'楠梓區'), (6, N'三民區'), (6, N'新興區');
                            ";
                            ExecuteQuery(connection, insertDistricts);
                        }

            // 預設病患與掛號資料
            int patientCount = 0;
            using (var cmdPatient = connection.CreateCommand())
            {
                cmdPatient.CommandText = "SELECT COUNT(*) FROM Patients";
                patientCount = (int)cmdPatient.ExecuteScalar();
            }
            if (patientCount == 0)
            {
                var insertPatients = @"
                    INSERT INTO Patients (MedicalRecordNo, NationalId, Name, Gender, BirthDate, Phone, Address) VALUES 
                    (N'001', N'A123456789', N'王小明', N'1.男', N'0800101', N'0912345678', N'臺北市中正區重慶南路一段1號'),
                    (N'002', N'B222333444', N'李美玲', N'0.女', N'0750505', N'0922888999', N'新北市板橋區文化路二段100號'),
                    (N'003', N'C111222333', N'張三豐', N'1.男', N'0601212', N'0933555666', N'桃園市中壢區中正路200號');
                ";
                ExecuteQuery(connection, insertPatients);

                var insertRegistrations = @"
                    INSERT INTO Registrations (PatientId, DoctorId, TimeSlotId, RegDate, RegNumber, IsFirstTime) VALUES 
                    (1, 1, 1, CONVERT(NVARCHAR(10), GETDATE(), 120), 1, 1),
                    (2, 3, 2, CONVERT(NVARCHAR(10), GETDATE(), 120), 1, 1);
                ";
                ExecuteQuery(connection, insertRegistrations);
            }
                    }
                }
            }
        }

        private static void ExecuteQuery(SqlConnection connection, string query)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = query;
                command.ExecuteNonQuery();
            }
        }

        private static void SeedData(SqlConnection connection)
        {
            // 縣市與行政區
            var insertCities = "INSERT INTO Cities (Name) VALUES (N'臺北市'), (N'新北市'), (N'桃園市'), (N'臺中市'), (N'臺南市'), (N'高雄市');";
            ExecuteQuery(connection, insertCities);
            var insertDistricts = @"
                INSERT INTO Districts (CityId, Name) VALUES 
                (1, N'中正區'), (1, N'大同區'), (1, N'中山區'), (1, N'松山區'), (1, N'大安區'), (1, N'信義區'),
                (2, N'板橋區'), (2, N'三重區'), (2, N'中和區'), (2, N'永和區'), (2, N'新莊區'), (2, N'新店區'),
                (3, N'桃園區'), (3, N'中壢區'), (3, N'平鎮區'), (3, N'八德區'),
                (4, N'中區'), (4, N'東區'), (4, N'南區'), (4, N'西區'), (4, N'北區'), (4, N'西屯區'), (4, N'南屯區'),
                (5, N'東區'), (5, N'南區'), (5, N'中西區'), (5, N'北區'), (5, N'安平區'),
                (6, N'鹽埕區'), (6, N'鼓山區'), (6, N'左營區'), (6, N'楠梓區'), (6, N'三民區'), (6, N'新興區');
            ";
            ExecuteQuery(connection, insertDistricts);

            // 預設科別
            var insertDepts = "INSERT INTO Departments (Name) VALUES (N'內科'), (N'外科'), (N'皮膚科'), (N'眼科'), (N'牙科'), (N'小兒科'), (N'婦產科');";
            ExecuteQuery(connection, insertDepts);

            // 預設時段
            var insertTimes = "INSERT INTO TimeSlots (Name) VALUES (N'上午 (09:00-12:00)'), (N'下午 (14:00-17:00)'), (N'晚上 (18:30-21:30)');";
            ExecuteQuery(connection, insertTimes);

            // 預設醫師與排班 (每個科別分配2名醫師)
            var insertDocs = @"
                INSERT INTO Doctors (Name, DepartmentId) VALUES 
                (N'趙一 (內科)', 1), (N'錢二 (內科)', 1),
                (N'孫三 (外科)', 2), (N'李四 (外科)', 2),
                (N'周五 (皮膚科)', 3), (N'吳六 (皮膚科)', 3),
                (N'鄭七 (眼科)', 4), (N'王八 (眼科)', 4),
                (N'馮九 (牙科)', 5), (N'陳十 (牙科)', 5),
                (N'黃十一 (小兒科)', 6), (N'張十二 (小兒科)', 6),
                (N'林十三 (婦產科)', 7), (N'劉十四 (婦產科)', 7);
            ";
            ExecuteQuery(connection, insertDocs);

            // 每位醫師都預設所有時段
            var insertSchedules = @"
                INSERT INTO DoctorSchedules (DoctorId, TimeSlotId)
                SELECT d.Id, t.Id FROM Doctors d CROSS JOIN TimeSlots t;
            ";
            ExecuteQuery(connection, insertSchedules);

            // 預設病患與掛號資料
            int patientCount = 0;
            using (var cmdPatient = connection.CreateCommand())
            {
                cmdPatient.CommandText = "SELECT COUNT(*) FROM Patients";
                patientCount = (int)cmdPatient.ExecuteScalar();
            }
            if (patientCount == 0)
            {
                var insertPatients = @"
                    INSERT INTO Patients (MedicalRecordNo, NationalId, Name, Gender, BirthDate, Phone, Address) VALUES 
                    (N'001', N'A123456789', N'王小明', N'1.男', N'0800101', N'0912345678', N'臺北市中正區重慶南路一段1號'),
                    (N'002', N'B222333444', N'李美玲', N'0.女', N'0750505', N'0922888999', N'新北市板橋區文化路二段100號'),
                    (N'003', N'C111222333', N'張三豐', N'1.男', N'0601212', N'0933555666', N'桃園市中壢區中正路200號');
                ";
                ExecuteQuery(connection, insertPatients);

                var insertRegistrations = @"
                    INSERT INTO Registrations (PatientId, DoctorId, TimeSlotId, RegDate, RegNumber, IsFirstTime) VALUES 
                    (1, 1, 1, CONVERT(NVARCHAR(10), GETDATE(), 120), 1, 1),
                    (2, 3, 2, CONVERT(NVARCHAR(10), GETDATE(), 120), 1, 1);
                ";
                ExecuteQuery(connection, insertRegistrations);
            }
        }
    }
}




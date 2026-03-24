![憨吉聯合大醫院](憨吉聯合大醫院.png)

# 憨吉聯合大醫院掛號系統 — 完整說明文件

> **系統名稱**: 憨吉聯合大醫院掛號系統
> **技術平台**: .NET 7 · Windows Forms · C# · SQL Server
> **文件版本**: v2.1.0  ·  2026-03-24

---

## 一、系統架構與資料庫設計
本系統採用微軟正統關聯式資料庫 `SQL Server` (`ClinicDB`) 進行資料儲存，完全相容於 SSMS，支援多個管理頁面與複雜掛號交易邏輯。

### 1.1 資料表結構 (共 6 個表)

#### 1. Departments (科別表)
| 鍵值 | 欄位名稱 | 中文說明 | 型態 | 屬性 | 說明 |
|---|---|---|---|---|---|
| PK | Id | 科別代號 | INT | PRIMARY KEY IDENTITY(1,1) | 唯一識別碼 |
| | Name | 科別名稱 | NVARCHAR(50) | NOT NULL | 儲存 7 大科別 |

#### 2. TimeSlots (時段表)
| 鍵值 | 欄位名稱 | 中文說明 | 型態 | 屬性 | 說明 |
|---|---|---|---|---|---|
| PK | Id | 時段代號 | INT | PRIMARY KEY IDENTITY(1,1) | 唯一識別碼 |
| | Name | 時段名稱 | NVARCHAR(50) | NOT NULL | 儲存上午、下午、晚上等時段 |

#### 3. Doctors (醫師表)
| 鍵值 | 欄位名稱 | 中文說明 | 型態 | 屬性 | 說明 |
|---|---|---|---|---|---|
| PK | Id | 醫師代號 | INT | PRIMARY KEY IDENTITY(1,1) | 唯一識別碼 |
| | Name | 醫師姓名 | NVARCHAR(50) | NOT NULL | 醫生全名 |
| FK | DepartmentId | 所屬科別 | INT | REFERENCES Departments(Id)| 掛在特定的醫學科別下 |

![科別醫師](科別醫師.png)

#### 4. DoctorSchedules (醫師排班表)
| 鍵值 | 欄位名稱 | 中文說明 | 型態 | 屬性 | 說明 |
|---|---|---|---|---|---|
| PK | Id | 排班代號 | INT | PRIMARY KEY IDENTITY(1,1) | 唯一識別碼 |
| FK | DoctorId | 醫師代號 | INT | REFERENCES Doctors(Id) | 掛號連動篩選重點 |
| FK | TimeSlotId | 時段代號 | INT | REFERENCES TimeSlots(Id)| 掛號連動篩選重點 |

![排班](排班.png)

#### 5. Patients (病患資料表)
| 鍵值 | 欄位名稱 | 中文說明 | 型態 | 屬性 | 說明 |
|---|---|---|---|---|---|
| PK | Id | 系統代號 | INT | PRIMARY KEY IDENTITY(1,1) | 唯一識別碼 |
| | MedicalRecordNo| 病歷號碼 | NVARCHAR(50) | UNIQUE | 病患的診所專屬病歷號碼 |
| | NationalId| 身分證號 | NVARCHAR(50) | UNIQUE | 病患的身分證/居留證字號 |
| | Name | 姓名 | NVARCHAR(100) | NOT NULL | 病患中文全名 |
| | Gender | 性別 | NVARCHAR(10) | | (0.女, 1.男, 2.中性, 3.未知) |
| | BirthDate| 生日 | NVARCHAR(20) | | 儲存 7 碼民國年字串 (如 0920101) |
| | Phone| 聯絡電話 | NVARCHAR(50) | | 病患的手機或市話 |
| | Address| 居住地址 | NVARCHAR(200) | | 門牌及詳細地址 |

![病人資料](病人資料.png)

#### 6. Registrations (掛號紀錄表)
| 鍵值 | 欄位名稱 | 中文說明 | 型態 | 屬性 | 說明 |
|---|---|---|---|---|---|
| PK | Id | 掛號代號 | INT | PRIMARY KEY IDENTITY(1,1) | 唯一識別碼 |
| FK | PatientId | 病患代號 | INT | REFERENCES Patients(Id) | 病患主鍵對應 |
| FK | DoctorId | 看診醫師 | INT | REFERENCES Doctors(Id) | 醫師主鍵對應 |
| FK | TimeSlotId | 看診時段 | INT | REFERENCES TimeSlots(Id)| 時段主鍵對應 |
| | RegDate | 掛號日期 | NVARCHAR(50) | | 申請就醫的日期 |
| | RegNumber | 號牌號碼 | INT | | 當天掛號梯次 (如 12 號) |
| | IsFirstTime| 初診狀態| BIT | | 1=初診, 0=複診 |

![掛號人數](掛號人數.png)

### 1.2 系統操作表單 (共 5 個頁面)
包含主畫面在內，本系統具有 5 個專門的 Windows Forms 頁面：
1. **Reg.cs**：掛號系統主畫面。
2. **DoctorScheduleForm.cs**：醫師排班管理介面，由上方按鈕開啟。
3. **PatientManagementForm.cs**：病患掛號歷史總覽表。
4. **DepartmentManagementForm.cs**：科別管理與轄下醫師清單。
5. **ReportForm.cs**：掛號統計報表頁面。

---

## 二、掛號程式畫面說明

### 2.1 畫面區域與佈局

```text
┌─────────────────────────────────────────────────────────────┐
│  ▌HEADER 標題列 (panel1)                                    │
│    憨吉聯合大醫院    [科別管理] [排班管理] [病患總覽] [統計報表] │
├─────────────────────────────────────────────────────────────┤
│  ▌SECTION A 病患基本資料 (panel2)                           │
│   [ 病歷號碼 ][ 輸入 ] [ 身分證號 ][ 輸入 ]  [ 姓  名 ][ 輸入 ] │
│                                         [ 性  別 ][下拉框] │
│   [ 生日    ][ 8碼輸入 ] [ 年齡 ] 歲    [ 電  話 ][輸入] │
│   [ 地址    ][ 縣市下拉 ][縣/市][ 區下拉 ][市/區]           │
│                                                [ 詳細地址輸入  ]│
├─────────────────────────────────────────────────────────────┤
│  ▌SECTION B 掛號資訊 (panel3)                               │
│   [ 掛號日期 ][ 輸入 ]  [ 科別 ][下拉] [ 時段 ][下拉] [ 醫師 ][下拉] │
│   ┌──────────────────────────────────────────────────┐      │
│   │     [ 掛號的號碼 ]     [ 12 ]  (紅字大字型)          │      │
│   └──────────────────────────────────────────────────┘      │
├─────────────────────────────────────────────────────────────┤
│  ▌FOOTER 操作按鈕列 (panel4)                                │
│       [掛號確認]    [列印掛號單]    [清空螢幕]    [結束/離開]   │
└─────────────────────────────────────────────────────────────┘
```

### 2.2 欄位對照表

| 區域 | 欄位名稱 | 控制項類型 | 說明 |
|------|---------|-----------|------|
| A | 病歷號碼 | TextBox | 內部病歷識別碼 |
| A | 身分證號 | TextBox | 病患的身分證或居留證號 |
| A | 姓名 | TextBox | 病患中文姓名 |
| A | 性別 | ComboBox | 0.女 / 1.男 / 2.中性 / 3.未知 |
| A | 生日 | MaskedTextBox | 強制 8 碼數字輸入 (例: 19901015) |
| A | 年齡 | TextBox (唯讀) | 系統依生日自動計算 |
| A | 電話、地址 | TextBox/ComboBox | 聯絡方式與居住地 |
| B | 掛號日期 | TextBox | 掛號當日日期 |
| B | 科別 | ComboBox | 動態由 SQL Server 載入 |
| B | 時段 | ComboBox | 動態由 SQL Server 載入 (上下晚上) |
| B | 醫師 | ComboBox | 依所選「科別」與「時段」即時連動篩選 |
| B | 等候號碼 | TextBox (唯讀) | 系統自資料庫查詢目前進度，防呆自動 +1 |

---

## 三、核心資料模型結構

### 3.1 病患模型 (`Patient.cs`)

```csharp
public class Patient
{
    public string IdNumber { get; set; } = string.Empty;   // 病歷號碼
    public string NationalId { get; set; } = string.Empty; // 身分證/居留證字號
    public string Name { get; set; } = string.Empty;       // 姓名
    // 生日限制為 8 碼字串，透過年齡推算轉換
    public string BirthDate { get; set; } = string.Empty;   
    public int Age { get; set; }                          
    // 其他欄位省略...
}
```

### 3.2 類別職責說明

| 類別 / 檔案 | 職責 |
|------------|------|
| [DatabaseHelper.cs](file:///c:/Users/lan/Desktop/Register/Register/DatabaseHelper.cs) | 系統啟動時初始化 SQL Server 資料庫 `ClinicDB`，自動防呆建立六大 T-SQL 資料表與填充假資料。 |
| [Reg.cs](file:///c:/Users/lan/Desktop/Register/Register/Reg.cs) | 主系統入口，處理輸入檢查、動態連動下拉表單、實作病患 UPSERT（依身分證確認身分，自動指派 001 流水病歷號）與等候人數連動更新。 |
| [DoctorScheduleForm.cs](file:///c:/Users/lan/Desktop/Register/Register/DoctorScheduleForm.cs) | 行政管理頁面 1：羅列所有門診排班（唯讀）。 |
| [PatientManagementForm.cs](file:///c:/Users/lan/Desktop/Register/Register/PatientManagementForm.cs) | 行政管理頁面 2：顯示看診病患總覽與掛號對應單。 |
| [DepartmentManagementForm.cs](file:///c:/Users/lan/Desktop/Register/Register/DepartmentManagementForm.cs) | 行政管理頁面 3：檢閱所有科別及其 14 位專任醫師。 |
| [ReportForm.cs](file:///c:/Users/lan/Desktop/Register/Register/ReportForm.cs) | 行政管理頁面 4：基於 GROUP BY 等查詢，統計各科與各醫師業績。 |

---

## 四、操作手冊與功能亮點

### 4.1 嚴謹的生日驗證機制
> [!IMPORTANT]
> 生日欄位為確保資料傳輸正確，已轉換為強制的 `MaskedTextBox`。  
> 格式限定為：`YYYMMDD`（純數字 7 碼的民國年格式）。
> 範例：若為民國 92 年 1 月 1 日，**必須輸入 `0920101`**。

### 4.2 智慧門診篩選功能
掛號資訊區（組塊 B）支援智慧型連動篩選：
1. 使用者首先於 **【科別】** (Department) 下拉選單選取欲掛入的科別 (例如：外科)。
2. 使用者接著於 **【時段】** (TimeSlot) 下拉選單選擇時段 (例如：早上)。
3. 系統即刻執行 SQL Server 內部聯集查詢 (`JOIN DoctorSchedules`)。
4. 最後，**【醫師】** (Doctor) 下拉選單只會列出現有排班且符合該科別定義的醫師，免除誤選錯誤。

### 4.3 新舊病患判定與自動派號機制
系統透過按下掛號按鈕時，進行以下智慧業務邏輯 (UPSERT)：
- **身分證為唯一識別**：輸入身分證後，系統進行資料庫配對比對。
- **初診派發病歷號**：若為新病患，系統會查詢過去最大的病歷號，從 `001` 開始為其建立全新流水編號並寫入。
- **複診更新資料**：依據身分證查有此人時，系統自動將畫面上新填寫的居住地、電話等資訊更新 (`UPDATE`) 進系統中，保持聯絡資料最新。
- **等候人數防呆**：畫面上的掛號號碼框設定為唯讀，當使用者選擇醫師後，系統就自動計算這科當天已經幾號了，自動加 1 派與給該次掛號。

### 4.4 管理報表的啟用
在主畫面的上方標題列，設置了四個切換按鈕 `[科別管理]` `[排班管理]` `[病患總覽]` `[統計報表]`。點擊任一按鈕，將會跳出 Modal 視窗 (ShowDialog)。每一個管理表單內建獨立的 `DataGridView` 查詢介面，方便櫃台人員隨時進行掛號查核。

---
*文件更新：2026-03-24  (升級為 SQL Server 架構並實裝掛號派號等業務邏輯)*



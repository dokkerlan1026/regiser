namespace Register
{
	/// <summary>
	/// 代表一位掛號病患的基本資料模型。
	/// </summary>
	public class Patient
	{
		/// <summary>
		/// 身份證字號。
		/// </summary>
		public string IdNumber { get; set; } = string.Empty;

		/// <summary>
		/// 姓名。
		/// </summary>
		public string Name { get; set; } = string.Empty;

		/// <summary>
		/// 性別。
		/// </summary>
		public string Gender { get; set; } = string.Empty;

		/// <summary>
		/// 生日 (格式可依需求調整)。
		/// </summary>
		public string BirthDate { get; set; } = string.Empty;

		/// <summary>
		/// 年齡。
		/// </summary>
		public int Age { get; set; }

		/// <summary>
		/// 聯絡電話。
		/// </summary>
		public string Phone { get; set; } = string.Empty;

		/// <summary>
		/// 居住地址。
		/// </summary>
		public string Address { get; set; } = string.Empty;

		/// <summary>
		/// 掛號日期。
		/// </summary>
		public string RegistrationDate { get; set; } = string.Empty;

		/// <summary>
		/// 掛號科別。
		/// </summary>
		public string Department { get; set; } = string.Empty;

		/// <summary>
		/// 看診醫師。
		/// </summary>
		public string Doctor { get; set; } = string.Empty;
	}
}

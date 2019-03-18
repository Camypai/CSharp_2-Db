namespace Charp_2_Db.Models
{
    /// <summary>
    /// Модель хранения данных о сотруднике
    /// </summary>
    public class Employee : BaseModel
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get; set; }
        /// <summary>
        /// Полное имя
        /// </summary>
        public string FullName => $"{LastName} {FirstName} {MiddleName}";
        /// <summary>
        /// Должность
        /// </summary>
        public string Position { get; set; }
        /// <summary>
        /// Возраст
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// Опыт работы
        /// </summary>
        public int Experience { get; set; }
        /// <summary>
        /// Департамент
        /// </summary>
        public int? DepartmentId { get; set; }
    }
}
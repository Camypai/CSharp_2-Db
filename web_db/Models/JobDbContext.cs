namespace Web_db.Models
{
    public class JobDbContext
    {
        /// <summary>
        /// Хранилище данных о сотрудниках
        /// </summary>
        public readonly SqlEmployeeRepository EmployeeRepository;
        /// <summary>
        /// Хранилище данных о департаментах
        /// </summary>
        public readonly SqlDepartmentRepository DepartmentRepository;

        public JobDbContext()
        {
            EmployeeRepository = new SqlEmployeeRepository();
            DepartmentRepository = new SqlDepartmentRepository();
        }

        /// <summary>
        /// Сохранение данных в БД
        /// </summary>
        public void Save()
        {
            EmployeeRepository.Save();
        }
    }
}
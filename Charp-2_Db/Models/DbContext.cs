namespace Charp_2_Db.Models
{
    /// <summary>
    /// Модель для работы с различными хранилищами данных
    /// </summary>
    public class DbContext
    {
        /// <summary>
        /// Хранилище данных о сотрудниках
        /// </summary>
        public readonly EmployeeRepository EmployeeRepository;
        /// <summary>
        /// Хранилище данных о департаментах
        /// </summary>
        public readonly DepartmentRepository DepartmentRepository;

        public DbContext(string employeesFileName = "employees.json", string departmentFileName = "departments.json")
        {
            EmployeeRepository = new EmployeeRepository(employeesFileName);
            DepartmentRepository = new DepartmentRepository(departmentFileName);
        }
    }
}
namespace Charp_2_Db.Models
{
    public class DbContext
    {
        public readonly EmployeeRepository EmployeeRepository;
        public readonly DepartmentRepository DepartmentRepository;

        public DbContext(string employeesFileName = "employees.json", string departmentFileName = "departments.json")
        {
            EmployeeRepository = new EmployeeRepository(employeesFileName);
            DepartmentRepository = new DepartmentRepository(departmentFileName);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Web_db.Models
{
    /// <summary>
    /// Реализация интерфейса для работы с БД
    /// </summary>
    public class SqlEmployeeRepository : IRepository<Employee>
    {
        private readonly JobContext _db;

        public SqlEmployeeRepository()
        {
            _db = new JobContext();

            if (!_db.Employees.Any())
            {
                _db.Employees.Add(new Employee
                {
                    FirstName = "Тест",
                    LastName = "Тестов",
                    MiddleName = "Тестович",
                    Age = 2,
                    Experience = 2,
                    Position = "Ученик"
                });

                Save();
            }
        }

        /// <summary>
        /// Создание нового затрудника
        /// </summary>
        /// <param name="item">Новый сотрудник</param>
        public void Create(Employee item)
        {
            _db.Employees.Add(item);
        }

        /// <inheritdoc />
        /// <summary>
        /// Выборка сотрудников по id
        /// </summary>
        /// <param name="id">id сотрудника</param>
        /// <returns>Employee</returns>
        public Employee Retrieve(int id)
        {
            return _db.Employees.Include("Department").FirstOrDefault(q => q.Id == id);
        }

        /// <summary>
        /// Выборка сотрудников по условию
        /// </summary>
        /// <param name="predicate">Условие выборки</param>
        /// <returns>IEnumerable</returns>
        public IEnumerable<Employee> RetrieveMultiple(Func<Employee, bool> predicate = null)
        {
            return predicate != null ? _db.Employees.Include("Department").Where(predicate) : _db.Employees.Include("Department");
        }

        /// <summary>
        /// Обновление данных сотрудника
        /// </summary>
        /// <param name="item">Сотрудник для обновления</param>
        public void Update(Employee item)
        {
            var emp = Retrieve(item.Id);
            if (item.DepartmentId != null)
                emp.DepartmentId = item.DepartmentId;
            emp.LastName = item.LastName;
            emp.FirstName = item.FirstName;
            emp.MiddleName = item.MiddleName;
            emp.Age = item.Age;
            emp.Position = item.Position;
            emp.Experience = item.Experience;

            _db.Entry(emp).State = EntityState.Modified;
        }

        /// <summary>
        /// Удаление записи о сотруднике
        /// </summary>
        /// <param name="item">Сотрудник для удаления</param>
        public void Delete(Employee item)
        {
            _db.Employees.Remove(item);
        }

        /// <summary>
        /// Сохранение данных в БД
        /// </summary>
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
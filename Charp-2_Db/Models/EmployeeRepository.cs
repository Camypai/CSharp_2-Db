using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Charp_2_Db.Models
{
    /// <summary>
    /// Реализация интерфейса для работы с хранилищем данных о сотрудниках
    /// </summary>
    public class EmployeeRepository : IRepository<Employee>
    {
        /// <summary>
        /// Лист сотрудников
        /// </summary>
        private List<Employee> _employees;
        /// <summary>
        /// Путь хранения данных
        /// </summary>
        private readonly string _path;

        public EmployeeRepository(string path)
        {
            _path = path;
            
            // Читаем данные из файла, если файла нет, то создаём тестовые данне
            if (File.Exists(_path))
            {
                using (var sr = new StreamReader(_path))
                {
                    var data = sr.ReadToEnd();
                    _employees = JsonConvert.DeserializeObject<List<Employee>>(data);
                }
            }
            else
            {
                _employees = new List<Employee>
                {
                    new Employee
                    {
                        Id = 1,
                        FirstName = "Тест",
                        LastName = "Тестов",
                        MiddleName = "Тестович",
                        Age = 2,
                        Experience = 2,
                        Position = "Ученик"
                    }
                };
            }
        }

        /// <summary>
        /// Обновить лист сотрудников актуальной информацией из хранилища
        /// </summary>
        private void GetActual()
        {
            using (var sr = new StreamReader(_path))
            {
                var data = sr.ReadToEnd();
                _employees = JsonConvert.DeserializeObject<List<Employee>>(data);
            }
        }

        /// <summary>
        /// Создать сотрудника в листе
        /// </summary>
        /// <param name="item"></param>
        public void Create(Employee item)
        {
            _employees.Add(item);
        }

        /// <summary>
        /// Выбрать сотрудника из листа по id
        /// </summary>
        /// <param name="id">id сотрудника</param>
        /// <returns>Employee</returns>
        public Employee Retrieve(int id)
        {
            return _employees.Find(q => q.Id == id);
        }

        /// <summary>
        /// Выбрать сотрудников из листа по условию
        /// </summary>
        /// <param name="predicate">Условие выборки. Если null, то выбираются все</param>
        /// <returns>IEnumerable&lt;Employee&gt;</returns>
        public IEnumerable<Employee> RetrieveMultiple(Func<Employee, bool> predicate = null)
        {
            return predicate != null ? _employees.Where(predicate) : _employees;
        }

        /// <summary>
        /// Обновить сотрудника в листе
        /// </summary>
        /// <param name="item">Сотрудник для обновления</param>
        public void Update(Employee item)
        {
            var emp = _employees.Find(q => q.Id == item.Id);
            emp.DepartmentId = item.DepartmentId;
            emp.LastName = item.LastName;
            emp.FirstName = item.FirstName;
            emp.MiddleName = item.MiddleName;
            emp.Age = item.Age;
            emp.Position = item.Position;
            emp.Experience = item.Experience;
        }

        /// <summary>
        /// Удалить сотрудника из листа
        /// </summary>
        /// <param name="item">Сотрудник для удаления</param>
        public void Delete(Employee item)
        {
            _employees.Remove(item);
        }

        /// <summary>
        /// Сохранить лист в хранилище
        /// </summary>
        public void Save()
        {
            using (var sw = new StreamWriter(_path))
            {
                sw.Write(JsonConvert.SerializeObject(_employees));
            }
            
            GetActual();
        }

        /// <summary>
        /// Сохранить новые данные о сотрудниках в хранилище
        /// </summary>
        /// <param name="items">Данные для сохранения</param>
        public void Save(IEnumerable<Employee> items)
        {
            using (var sw = new StreamWriter(_path))
            {
                sw.Write(JsonConvert.SerializeObject(items));
            }
            
            GetActual();
        }
    }
}
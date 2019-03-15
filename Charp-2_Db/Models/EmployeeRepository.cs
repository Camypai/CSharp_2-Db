using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Charp_2_Db.Models
{
    public class EmployeeRepository : IRepository<Employee>
    {
        private List<Employee> _employees;
        private readonly string _path;

        public EmployeeRepository(string path)
        {
            _path = path;
            
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

        public void Create(Employee item)
        {
            _employees.Add(item);
        }

        public Employee Retrieve(int id)
        {
            return _employees.Find(q => q.Id == id);
        }

        public IEnumerable<Employee> RetrieveMultiple(Func<Employee, bool> predicate = null)
        {
            return predicate != null ? _employees.Where(predicate) : _employees;
        }

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

        public void Delete(Employee item)
        {
            _employees.Remove(item);
        }

        public void Save()
        {
            using (var sw = new StreamWriter(_path))
            {
                sw.Write(JsonConvert.SerializeObject(_employees));
            }
        }

        public void Save(IEnumerable<Employee> items)
        {
            _employees = items.ToList();
            
            using (var sw = new StreamWriter(_path))
            {
                sw.Write(JsonConvert.SerializeObject(items));
            }
        }
    }
}
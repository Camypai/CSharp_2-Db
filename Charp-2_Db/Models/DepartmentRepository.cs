using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Charp_2_Db.Models
{
    public class DepartmentRepository : IRepository<Department>
    {
        private List<Department> _departments;
        private readonly string _path;

        public DepartmentRepository(string path)
        {
            _path = path;

            if (File.Exists(_path))
            {
                using (var sr = new StreamReader(_path))
                {
                    var data = sr.ReadToEnd();
                    _departments = JsonConvert.DeserializeObject<List<Department>>(data);
                }
            }
            else
            {
                _departments = new List<Department>
                {
                    new Department
                    {
                        Id = 1,
                        Name = "Школа"
                    }
                };
            }
        }
        
        public void Create(Department item)
        {
            _departments.Add(item);
        }

        public Department Retrieve(int id)
        {
            return _departments.Find(q => q.Id == id);
        }

        public IEnumerable<Department> RetrieveMultiple(Func<Department, bool> predicate = null)
        {
            return predicate != null ? _departments.Where(predicate) : _departments;
        }

        public void Update(Department item)
        {
            var dep = _departments.Find(q => q.Id == item.Id);
            dep.Name = item.Name;
            dep.Employees = item.Employees;
        }

        public void Delete(Department item)
        {
            _departments.Remove(item);
        }

        public void Save()
        {
            using (var sw = new StreamWriter(_path))
            {
                sw.Write(JsonConvert.SerializeObject(_departments));
            }
        }

        public void Save(IEnumerable<Department> items)
        {
            _departments = items.ToList();
            
            using (var sw = new StreamWriter(_path))
            {
                sw.Write(JsonConvert.SerializeObject(items));
            }
        }
    }
}
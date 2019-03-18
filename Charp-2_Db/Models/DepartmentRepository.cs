using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Charp_2_Db.Models
{
    /// <summary>
    /// Реализация интерфейса для работы с хранилищем данных о департаментах
    /// </summary>
    public class DepartmentRepository : IRepository<Department>
    {
        /// <summary>
        /// Лист департаментов
        /// </summary>
        private List<Department> _departments;
        /// <summary>
        /// Путь хранения данных
        /// </summary>
        private readonly string _path;

        public DepartmentRepository(string path)
        {
            _path = path;

            // Читаем данные из файла, если файла нет, то создаём тестовые данне
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
        
        /// <summary>
        /// Создаём департамент
        /// </summary>
        /// <param name="item">Департамент</param>
        public void Create(Department item)
        {
            _departments.Add(item);
        }

        /// <summary>
        /// Получаем департамент по id
        /// </summary>
        /// <param name="id">id департамента</param>
        /// <returns>Департамент</returns>
        public Department Retrieve(int id)
        {
            return _departments.Find(q => q.Id == id);
        }

        /// <summary>
        /// Получаем все департаменты, удовлетворяющие условию
        /// </summary>
        /// <param name="predicate">Условие выборки. Если null, то выбираются все</param>
        /// <returns>Департаменты</returns>
        public IEnumerable<Department> RetrieveMultiple(Func<Department, bool> predicate = null)
        {
            return predicate != null ? _departments.Where(predicate) : _departments;
        }

        /// <summary>
        /// Обновить департамент
        /// </summary>
        /// <param name="item">Обновляемый департамент</param>
        public void Update(Department item)
        {
            var dep = _departments.Find(q => q.Id == item.Id);
            dep.Name = item.Name;
        }

        /// <summary>
        /// Удалить департамент
        /// </summary>
        /// <param name="item">Удаляемый департамент</param>
        public void Delete(Department item)
        {
            _departments.Remove(item);
        }

        /// <summary>
        /// Сохранить хранилище департаментов
        /// </summary>
        public void Save()
        {
            using (var sw = new StreamWriter(_path))
            {
                sw.Write(JsonConvert.SerializeObject(_departments));
            }
        }

        /// <summary>
        /// Сохранить хранилище департаментов
        /// </summary>
        /// <param name="items">Департаменты для сохранения</param>
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
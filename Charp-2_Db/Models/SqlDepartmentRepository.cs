using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Charp_2_Db.Models
{
    /// <summary>
    /// Реализация интерфейса для работы с БД
    /// </summary>
    public class SqlDepartmentRepository : IRepository<Department>
    {
        /// <summary>
        /// Контекст БД
        /// </summary>
        private readonly JobContext _db;

        public SqlDepartmentRepository()
        {
            _db = new JobContext();
            
            if (!_db.Departments.Any())
            {
                _db.Departments.Add(new Department
                {
                    Name = "Школа"
                });
                
                Save();
            }
        }

        /// <summary>
        /// Создание нового департамента
        /// </summary>
        /// <param name="item"></param>
        public void Create(Department item)
        {
            _db.Departments.Add(item);
        }

        /// <summary>
        /// Выборка департамента по id
        /// </summary>
        /// <param name="id">id искомого департамента</param>
        /// <returns>Department</returns>
        public Department Retrieve(int id)
        {
            return _db.Departments.Find(id);
        }

        /// <summary>
        /// Выборка департаментов по условию
        /// </summary>
        /// <param name="predicate">Условие выборки</param>
        /// <returns>IEnumerable</returns>
        public IEnumerable<Department> RetrieveMultiple(Func<Department, bool> predicate = null)
        {
            return predicate != null ? _db.Departments.Where(predicate) : _db.Departments;
        }

        /// <summary>
        /// Обновление данных департамента
        /// </summary>
        /// <param name="item">Департамент для обновления</param>
        public void Update(Department item)
        {
            var dep = _db.Departments.Find(item.Id);
            dep.Name = item.Name;

            _db.Entry(dep).State = EntityState.Modified;
        }

        /// <summary>
        /// Удаление департамента
        /// </summary>
        /// <param name="item">Департамент для удаления</param>
        public void Delete(Department item)
        {
            _db.Departments.Remove(item);
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
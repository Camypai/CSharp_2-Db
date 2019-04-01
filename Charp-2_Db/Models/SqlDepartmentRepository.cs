using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using Charp_2_Db.Helpers;
using static Charp_2_Db.Helpers.WebDbController;

namespace Charp_2_Db.Models
{
    /// <inheritdoc />
    /// <summary>
    /// Реализация интерфейса для работы с БД
    /// </summary>
    public class SqlDepartmentRepository : IRepository<Department>
    {
        /// <inheritdoc />
        /// <summary>
        /// Создание нового департамента
        /// </summary>
        /// <param name="item"></param>
        public int Create(Department item)
        {
            var result = PostAsync(item);
            return result.Id;
        }

        /// <inheritdoc />
        /// <summary>
        /// Выборка департамента по id
        /// </summary>
        /// <param name="id">id искомого департамента</param>
        /// <returns>Department</returns>
        public Department Retrieve(int id)
        {
            return GetAsync<Department>($"Departments?key={id}");
        }

        /// <summary>
        /// Выборка департаментов по условию
        /// </summary>
        /// <param name="filter">Условие выборки</param>
        /// <returns>IEnumerable</returns>
        public IEnumerable<Department> RetrieveMultiple(string filter = null)
        {
            return filter != null
                ? GetAsync<IEnumerable<Department>>($"{nameof(Department)}s", filter)
                : GetAsync<IEnumerable<Department>>($"{nameof(Department)}s");
        }

        /// <inheritdoc />
        /// <summary>
        /// Обновление данных департамента
        /// </summary>
        /// <param name="item">Департамент для обновления</param>
        public void Update(Department item)
        {
            PatchAsync($"{nameof(Department)}s?key={item.Id}", item);
        }

        /// <inheritdoc />
        /// <summary>
        /// Удаление департамента
        /// </summary>
        /// <param name="item">Департамент для удаления</param>
        public void Delete(Department item)
        {
            DeleteAsync($"{nameof(Department)}s?key={item.Id}");
        }

        /// <inheritdoc />
        /// <summary>
        /// Сохранение данных в БД
        /// </summary>
        public void Save(IEnumerable<Department> items)
        {
            PatchAsync(items);
        }
    }
}
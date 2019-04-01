using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using static Charp_2_Db.Helpers.WebDbController;

namespace Charp_2_Db.Models
{
    /// <inheritdoc />
    /// <summary>
    /// Реализация интерфейса для работы с БД
    /// </summary>
    public class SqlEmployeeRepository : IRepository<Employee>
    {
        /// <inheritdoc />
        /// <summary>
        /// Создание нового затрудника
        /// </summary>
        /// <param name="item">Новый сотрудник</param>
        public int Create(Employee item)
        {
            var result = PostAsync(item);
            return result.Id;
        }

        /// <inheritdoc />
        /// <summary>
        /// Выборка сотрудников по id
        /// </summary>
        /// <param name="id">id сотрудника</param>
        /// <returns>Employee</returns>
        public Employee Retrieve(int id)
        {
            return GetAsync<Employee>($"{nameof(Employee)}s?key={id}");
        }

        /// <inheritdoc />
        /// <summary>
        /// Выборка сотрудников по условию
        /// </summary>
        /// <param name="filter">Условие выборки</param>
        /// <returns>IEnumerable</returns>
        public IEnumerable<Employee> RetrieveMultiple(string filter = null)
        {
            return filter != null 
                ? GetAsync<IEnumerable<Employee>>($"{nameof(Employee)}s", filter)
                : GetAsync<IEnumerable<Employee>>($"{nameof(Employee)}s");
        }

        /// <inheritdoc />
        /// <summary>
        /// Обновление данных сотрудника
        /// </summary>
        /// <param name="item">Сотрудник для обновления</param>
        public void Update(Employee item)
        {
            PatchAsync($"{nameof(Employee)}s?key={item.Id}", item);
        }

        /// <inheritdoc />
        /// <summary>
        /// Удаление записи о сотруднике
        /// </summary>
        /// <param name="item">Сотрудник для удаления</param>
        public void Delete(Employee item)
        {
            DeleteAsync($"{nameof(Employee)}s?key={item.Id}");
        }

        /// <inheritdoc />
        /// <summary>
        /// Сохранение данных в БД
        /// </summary>
        public void Save(IEnumerable<Employee> items)
        {
            var employees = items.ToList();
            PatchAsync(employees);
        }
    }
}
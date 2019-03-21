using System;
using System.Collections.Generic;

namespace Charp_2_Db.Models
{
    /// <summary>
    /// Интерфейс для работы с хранилищем данных
    /// </summary>
    /// <typeparam name="T">Тип хранимых данных</typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// Создать запись в хранилище
        /// </summary>
        /// <param name="item">Запись для создания</param>
        void Create(T item);
        /// <summary>
        /// Получить запись из хранилища по id
        /// </summary>
        /// <param name="id">id записи</param>
        /// <returns></returns>
        T Retrieve(int id);
        /// <summary>
        /// Получить все записи из хранилища, удовлетворяющие условию
        /// </summary>
        /// <param name="predicate">Условие выборки</param>
        /// <returns></returns>
        IEnumerable<T> RetrieveMultiple(Func<T,bool> predicate);
        /// <summary>
        /// Обновить запись в хранилище
        /// </summary>
        /// <param name="item">Обновляемая запись</param>
        void Update(T item);
        /// <summary>
        /// Удалить запись из хранилища
        /// </summary>
        /// <param name="item">Удаляемая запись</param>
        void Delete(T item);
        /// <summary>
        /// Сохранить данные в хранилище
        /// </summary>
        void Save();
    }
}
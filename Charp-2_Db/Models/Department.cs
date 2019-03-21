using System.Collections.Generic;

namespace Charp_2_Db.Models
{
    /// <summary>
    /// Модель хранения данных о департаменте
    /// </summary>
    public class Department : BaseModel
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }
        
        public ICollection<Employee> Employees { get; set; }

        public Department()
        {
            Employees = new List<Employee>();
        }
    }
}
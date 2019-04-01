using System.Collections.Generic;
using Newtonsoft.Json;

namespace Web_db.Models
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
        
//        [JsonIgnore]
        public ICollection<Employee> Employees { get; set; }

        public Department()
        {
            Employees = new List<Employee>();
        }
    }
}
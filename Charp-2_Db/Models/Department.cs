using System.Collections.Generic;

namespace Charp_2_Db.Models
{
    public class Department : BaseModel
    {
        public string Name { get; set; }
        public ICollection<Employee> Employees { get; set; }

        public Department()
        {
            Employees = new List<Employee>();
        }
    }
}
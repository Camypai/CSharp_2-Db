namespace Charp_2_Db.Models
{
    public class Employee : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string FullName => $"{LastName} {FirstName} {MiddleName}";
        public string Position { get; set; }
        public int Age { get; set; }
        public int Experience { get; set; }
        public Department Department { get; set; }
    }
}
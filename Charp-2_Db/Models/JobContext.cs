using System.Data.Entity;
using Charp_2_Db.Models;
using DbContext = System.Data.Entity.DbContext;

public class JobContext : DbContext
{
    /// <summary>
    /// Базовый конструктор для создания подключения через строку подключения DefaultConnection
    /// </summary>
    public JobContext(): base("DefaultConnection")
    {
 
    }
    
    /// <summary>
    /// Хранилище сотрудников
    /// </summary>
    public DbSet<Employee> Employees { get; set; }
    
    /// <summary>
    /// Хранилище департаментов
    /// </summary>
    public DbSet<Department> Departments { get; set; }
}
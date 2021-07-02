using System.Data.Entity;

namespace ApplicationContextData
{
    public class ApplicationContext : DbContext
    {
        public DbSet<NameKB> NamesKb { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeTimesheet> EmployeeTimesheets { get; set; }

        public ApplicationContext() : base("EmployeeTimesheets")
        {
            //Database.Delete();
            Database.CreateIfNotExists();
        }
    }
}

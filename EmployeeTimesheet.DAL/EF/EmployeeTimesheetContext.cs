using System.Data.Entity;
using EmployeeTimesheet.DAL.Entities;

namespace EmployeeTimesheet.DAL.EF
{
    public class EmployeeTimesheetContext :DbContext
    {
        public DbSet<NameKBEntity> NamesKb { get; set; }
        public DbSet<EmployeeEntity> Employees { get; set; }
        public DbSet<EmployeeTimesheetEntity> EmployeeTimesheets { get; set; }

        public EmployeeTimesheetContext(string connectionString) : base(connectionString)
        {
            //Database.Delete();
            Database.CreateIfNotExists();
        }
    }
}

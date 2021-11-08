using Autofac;
using EmployeeTimesheet.DAL.EF;
using EmployeeTimesheet.DAL.Interfaces;
using EmployeeTimesheet.DAL.Repositories;

namespace EmployeeTimesheet.BLL.Infrastructure
{
    public class ServiceModule
    {
        private static string _connectionString;
        private static IContainer Container { get; set; }

        public ServiceModule(string connection)
        {
            _connectionString = connection;
        }

        private static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<IUnitOfWork>().As<EFUnitOfWork>().WithParameter("connectionString", new EmployeeTimesheetContext(_connectionString));
            Container = builder.Build();
        } 


    }
}

using System;
using EmployeeTimesheet.DAL.Entities;

namespace EmployeeTimesheet.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<EmployeeEntity> EmployeeEntitys { get;}
        IRepository<EmployeeTimesheetEntity> EmployeeTimesheetEntitys { get; }
        IRepository<NameKBEntity> NameKBEntitys { get; }
        void Save();
    }
}

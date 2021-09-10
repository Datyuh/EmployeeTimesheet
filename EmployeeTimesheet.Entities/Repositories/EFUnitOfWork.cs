using System;
using EmployeeTimesheet.DAL.EF;
using EmployeeTimesheet.DAL.Entities;
using EmployeeTimesheet.DAL.Interfaces;

namespace EmployeeTimesheet.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly EmployeeTimesheetContext _db;

        public EFUnitOfWork(string connectionString)
        {
            _db = new EmployeeTimesheetContext(connectionString);
        }

        private EmployeeEntityReposotory _employeeEntityReposotory;
        private EmployeeTimesheetEntityReposotory _employeeTimesheetEntityReposotory;
        private NameKBEntityReposotory _nameKbEntityReposotory;

        public IRepository<EmployeeEntity> EmployeeEntitys
        {
            get
            {
                if (_employeeEntityReposotory == null)
                    _employeeEntityReposotory = new EmployeeEntityReposotory(_db);
                return _employeeEntityReposotory;
            }
        }

        public IRepository<EmployeeTimesheetEntity> EmployeeTimesheetEntitys
        {
            get
            {
                if (_employeeTimesheetEntityReposotory == null)
                    _employeeTimesheetEntityReposotory = new EmployeeTimesheetEntityReposotory(_db);
                return _employeeTimesheetEntityReposotory;
            }
        }

        public IRepository<NameKBEntity> NameKBEntitys
        {
            get
            {
                if (_nameKbEntityReposotory == null)
                    _nameKbEntityReposotory = new NameKBEntityReposotory(_db);
                return _nameKbEntityReposotory;
            }
        }


        public void Save()
        {
            _db.SaveChanges();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}

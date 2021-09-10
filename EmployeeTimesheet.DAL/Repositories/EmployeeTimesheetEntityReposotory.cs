using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EmployeeTimesheet.DAL.EF;
using EmployeeTimesheet.DAL.Entities;
using EmployeeTimesheet.DAL.Interfaces;

namespace EmployeeTimesheet.DAL.Repositories
{
    public class EmployeeTimesheetEntityReposotory : IRepository<EmployeeTimesheetEntity>
    {
        private readonly EmployeeTimesheetContext _db;

        public EmployeeTimesheetEntityReposotory(EmployeeTimesheetContext context)
        {
            _db = context;
        }
        public IEnumerable<EmployeeTimesheetEntity> GetAll()
        {
            return _db.EmployeeTimesheets;
        }

        public EmployeeTimesheetEntity Get(int id)
        {
            return _db.EmployeeTimesheets.Find(id);
        }

        public IEnumerable<EmployeeTimesheetEntity> Find(Func<EmployeeTimesheetEntity, bool> predicate)
        {
            return _db.EmployeeTimesheets.Where(predicate).ToList();
        }

        public void Create(EmployeeTimesheetEntity item)
        {
            _db.EmployeeTimesheets.Add(item);
        }

        public void Update(EmployeeTimesheetEntity item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            EmployeeTimesheetEntity book = _db.EmployeeTimesheets.Find(id);
            if (book != null)
                _db.EmployeeTimesheets.Remove(book);
        }
    }
}

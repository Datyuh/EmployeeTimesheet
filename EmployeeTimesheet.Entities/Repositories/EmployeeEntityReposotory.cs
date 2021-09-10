using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EmployeeTimesheet.DAL.EF;
using EmployeeTimesheet.DAL.Entities;
using EmployeeTimesheet.DAL.Interfaces;

namespace EmployeeTimesheet.DAL.Repositories
{
    public class EmployeeEntityReposotory : IRepository<EmployeeEntity>
    {
        private readonly EmployeeTimesheetContext _db;

        public EmployeeEntityReposotory(EmployeeTimesheetContext context)
        {
            _db = context;
        }
        public IEnumerable<EmployeeEntity> GetAll()
        {
            return _db.Employees;
        }

        public EmployeeEntity Get(int id)
        {
            return _db.Employees.Find(id);
        }

        public IEnumerable<EmployeeEntity> Find(Func<EmployeeEntity, bool> predicate)
        {
            return _db.Employees.Where(predicate).ToList();
        }

        public void Create(EmployeeEntity item)
        {
            _db.Employees.Add(item);
        }

        public void Update(EmployeeEntity item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            EmployeeEntity book = _db.Employees.Find(id);
            if (book != null)
                _db.Employees.Remove(book);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EmployeeTimesheet.DAL.EF;
using EmployeeTimesheet.DAL.Entities;
using EmployeeTimesheet.DAL.Interfaces;

namespace EmployeeTimesheet.DAL.Repositories
{
    public class NameKBEntityReposotory : IRepository<NameKBEntity>
    {
        private readonly EmployeeTimesheetContext _db;

        public NameKBEntityReposotory(EmployeeTimesheetContext context)
        {
            _db = context;
        }
        public IEnumerable<NameKBEntity> GetAll()
        {
            return _db.NamesKb;
        }

        public NameKBEntity Get(int id)
        {
            return _db.NamesKb.Find(id);
        }

        public IEnumerable<NameKBEntity> Find(Func<NameKBEntity, bool> predicate)
        {
            return _db.NamesKb.Where(predicate).ToList();
        }

        public void Create(NameKBEntity item)
        {
            _db.NamesKb.Add(item);
        }

        public void Update(NameKBEntity item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            NameKBEntity book = _db.NamesKb.Find(id);
            if (book != null)
                _db.NamesKb.Remove(book);
        }
    }
}

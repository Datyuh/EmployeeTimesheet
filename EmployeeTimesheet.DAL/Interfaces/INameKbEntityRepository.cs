using System;
using System.Collections.Generic;
using EmployeeTimesheet.DAL.Entities;

namespace EmployeeTimesheet.DAL.Interfaces
{
    public interface INameKbEntityRepository
    {
        List<NameKBEntity> GetAllNameKB();
        bool NameKbPasChaeck(NameKBEntity nameKbEntity);
        IEnumerable<NameKBEntity> Find(Func<NameKBEntity, Boolean> predicate);
        void Update(NameKBEntity item);
        void Delete(int id);
    }
}

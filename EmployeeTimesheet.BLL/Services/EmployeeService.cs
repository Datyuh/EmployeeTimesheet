using System.Collections.Generic;
using EmployeeTimesheet.BLL.DTO;
using EmployeeTimesheet.BLL.Interfaces;
using EmployeeTimesheet.DAL.Interfaces;

namespace EmployeeTimesheet.BLL.Services
{
    public class EmployeeService : IEmployeeService
    {
        IUnitOfWork Database { get; set; }

        public EmployeeService(IUnitOfWork uow)
        {
            Database = uow;
        }
       

        public void Dispose()
        {
            Database.Dispose();
        }

        public void MakeEmployee(EmployeeDTO employeeDto)
        {
            throw new System.NotImplementedException();
        }

        public NameKBDTO GetNameKB(int? id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<NameKBDTO> GetNameKBs()
        {
            throw new System.NotImplementedException();
        }
    }
}

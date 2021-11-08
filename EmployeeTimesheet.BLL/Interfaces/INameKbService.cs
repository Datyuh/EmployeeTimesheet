using System.Collections.Generic;
using EmployeeTimesheet.BLL.DTO;

namespace EmployeeTimesheet.BLL.Interfaces
{
    public interface INameKbService
    {
        IEnumerable<NameKBDTO> GetAllName();
        bool NameKbPasCheck(string nameKb);
        void Dispose();

    }
}

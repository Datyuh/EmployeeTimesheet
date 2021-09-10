using System.Collections.Generic;
using EmployeeTimesheet.BLL.DTO;

namespace EmployeeTimesheet.BLL.Interfaces
{
    public interface IEmployeeTimesheetService
    {
        void MakeEmployeeTimesheet(EmployeeDTO employeeDto);
        EmployeeDTO GetEmployeeTimesheet(int? id);
        IEnumerable<EmployeeDTO> GetEmployeeTimesheets();
        void Dispose();
    }
}

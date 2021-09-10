﻿using System.Collections.Generic;
using EmployeeTimesheet.BLL.DTO;

namespace EmployeeTimesheet.BLL.Interfaces
{
    interface IEmployeeService
    {
        void MakeEmployee(EmployeeDTO employeeDto);
        NameKBDTO GetNameKB(int? id);
        IEnumerable<NameKBDTO> GetNameKBs();
        void Dispose();
    }
}
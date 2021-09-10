using System;

namespace EmployeeTimesheet.BLL.DTO
{
    public class EmployeeTimesheetDTO
    { 
        public int Id { get; set; }
        public string Status { get; set; }
        public DateTime DateTimeAddData { get; set; }
        public int EmployeesId { get; set; }
        public DateTime RegistrDateInBase { get; set; }
    }
}

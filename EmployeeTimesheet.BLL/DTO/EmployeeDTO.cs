using System;

namespace EmployeeTimesheet.BLL.DTO
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string Fio { get; set; }
        public string StatusUsers { get; set; }
        public int ServiceNumbers { get; set; }
        public int NameKbId { get; set; }
        public DateTime RegistrDateInBase { get; set; }
    }
}
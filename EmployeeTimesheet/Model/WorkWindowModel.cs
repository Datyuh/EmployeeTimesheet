using System;
using System.Collections.Generic;
using ApplicationContextData;

namespace EmployeeTimesheet.Model
{
    public class WorkWindowModel
    {
        public Employee Employees { get; set; }
        public string Fio { get; set; }
        public int ServiceNumbers { get; set; }
        public int SumDayWork { get; set; }
        public int SumDayOwnExpense { get; set; }
        public int SumDayMedical { get; set; }
        public int SumDayVacation { get; set; }
        public List<string> ListReportCard { get; set; }
        public DateTime DateEnterInBase { get; set; }
        public string ListReportCards { get; set; }
        public DateTime DateEnterInBases { get; set; }
        public bool UpdateUserStatusCheckBox { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using ApplicationContextData;
using EmployeeTimesheet.Resources;

namespace EmployeeTimesheet.Model
{
    class WorkWindowModel
    {
        public string FIO { get; set; }
        public int ServiceNumbers { get; set; }
        public int SumDayWork { get; set; }
        public int SumDayOwnExpense { get; set; }
        public int SumDayMedical { get; set; }
        public string ListReportCard { get; set; }

        public WorkWindowModel(Employee parent)
        {
            parent.Fio = FIO;
            parent.ServiceNumbers = ServiceNumbers;
        }
    }
}
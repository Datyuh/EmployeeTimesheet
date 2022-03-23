using ApplicationContextData;
using System;

namespace EmployeeTimesheet.Model
{
    static class StaticDataModel
    {
        public static NameKB NameKbFromMain { get; set; }
        public static ApplicationContext ApplicationContext { get; set; } = new();
        //public static string NumOrders { get; set; }
        //public static DateTime? DateOrders { get; set; }
    }
}

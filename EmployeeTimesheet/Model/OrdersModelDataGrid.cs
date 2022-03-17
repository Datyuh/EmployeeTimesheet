using SelectedLib;
using System;

namespace EmployeeTimesheet.Model
{
    public class OrdersModelDataGrid
    {      
        public bool EnabledOrderStatusCheckBox { get; set; }
        public bool UpdateOrderStatusCheckBox { get; set; }
        public string NumOrder { get; set; }
        public string StatusOrder { get; set; }
        public DateTime DateWorkInWeekend { get; set; }
        public DateTime? DateOrder { get; set; }       
    }
}

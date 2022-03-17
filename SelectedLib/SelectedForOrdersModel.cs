using ApplicationContextData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelectedLib
{
    public class SelectedForOrdersModel
    {
        private readonly ApplicationContext _db;

        public object StaticDataModel { get; private set; }

        public SelectedForOrdersModel(ApplicationContext db)
        {
            _db = db;
        }

        public ObservableCollection<EmployeeTimesheet> SelectedAllOrders(string fio, int? selectYearOrders)
        {
            var selectedAllOrders = _db.EmployeeTimesheets
                .Where(p => p.Employees.Fio
                .Contains(fio) && p.Employees.StatusUsers == "Работает" && p.NumOrder != null && p.DateTimeAddData.Year == selectYearOrders)
                .Select(p => p).OrderBy(p => p.DateTimeAddData);
            return new ObservableCollection<EmployeeTimesheet>(selectedAllOrders);
        }

        public void ChangeStatusOrders(string fio, string numOrder, DateTime? dateWorkInWeekend)
        {
            var changeStatusOrders = _db.EmployeeTimesheets.Where(p => p.Employees.Fio == fio && p.NumOrder == numOrder && p.DateTimeAddData == dateWorkInWeekend).Select(p => p).ToList();
            if (changeStatusOrders != null)
                foreach (var item in changeStatusOrders)
                    item.StatusOrder = "Отгулял(а)";
            _db.SaveChanges();
        }
    }
}

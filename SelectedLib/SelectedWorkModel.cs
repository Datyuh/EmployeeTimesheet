using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ApplicationContextData;

namespace SelectedLib
{
    public sealed class SelectedWorkModel
    {
        private readonly ApplicationContext _db;
        public int _monthChoices { get; set; }
        public int _yearChoices { get; set; }

        public SelectedWorkModel(ApplicationContext db)
        {
            _db = db;
        }

        private ObservableCollection<EmployeeTimesheet> SelectedEmployeeTimesheet => SelectedEmployeeTimesheets(_monthChoices, _yearChoices);

        public ObservableCollection<Employee> SelectedEmployee(NameKB nameKb)
        {
            var selectedEmployee = _db.Employees
                .Select(p => p)
                .Where(p => p != null && p.NameKbId == nameKb.Id);
            return new ObservableCollection<Employee>(selectedEmployee);
        }

        public ObservableCollection<Employee> SelectedEmployeeForChiefDesigner(NameKB nameKb)
        {
            var selectedEmployee = _db.Employees
                .Select(p => p)
                .Where(p => p != null && p.NameKbId == nameKb.Id);
            return new ObservableCollection<Employee>(selectedEmployee);
        }

        public List<int> SelectedServiceNumbersUsers()
        {
            var selectedServiceNumbersUsers = _db.Employees
                .Select(p => p.ServiceNumbers);
            return new List<int>(selectedServiceNumbersUsers);
        }

        public List<DateTime> NowDateInBase()
        {
            var nowDateInBase = _db.EmployeeTimesheets
                .Select(p => p.DateTimeAddData);
            return new List<DateTime>(nowDateInBase);
        }

        public ObservableCollection<EmployeeTimesheet> SelectedEmployeeTimesheets(int month, int year)
        {
            var selectedEmployeeTimesheets = _db.EmployeeTimesheets
                .Select(p => p)
                .Where(p => p.DateTimeAddData.Month == month
                && p.DateTimeAddData.Year == year);
            return new ObservableCollection<EmployeeTimesheet>(selectedEmployeeTimesheets);
        }

        public List<int> SelectedYearInBase()
        {
            var selectedYearInBase = _db.EmployeeTimesheets
                .Select(p => p.DateTimeAddData.Year)
                .Where(p => p > DateTime.Now.Year - 2)
                .Distinct();

            List<int> selectedYear = new ();

            foreach (var item in selectedYearInBase)
            {
                selectedYear.Add(item);
            }
            return selectedYear;            
        }

        public double SumDayWork(Employee workEmployee)
        {
            var sumDayWork = SelectedEmployeeTimesheet
                .Where(e => e.Status == "Явка"
                            && e.Employees == workEmployee
                            && e.DateTimeAddData.Month == _monthChoices)
                .Select(p => p.DateTimeAddData)
                .Count();
            return sumDayWork;
        }

        public double SumHalfDayWork(Employee workEmployee)
        {
            var sumHalfDayWork = SelectedEmployeeTimesheet
                .Where(e => e.Status == "Пол. дня ОБС"
                            && e.Employees == workEmployee
                            && e.DateTimeAddData.Month == _monthChoices)
                .Select(p => p.DateTimeAddData)
                .Count();
            return Convert.ToDouble(sumHalfDayWork * 0.5);

        }

        public int SumDayOwnExpense(Employee workEmployee)
        {
            var sumDayOwnExpense = SelectedEmployeeTimesheet
                .Where(e => e.Status == "ОБС"
                            && e.Employees == workEmployee
                            && e.DateTimeAddData.Month == _monthChoices)
                .Select(p => p.DateTimeAddData)
                .Count();
            return sumDayOwnExpense;
        }

        public int SumDayMedical(Employee workEmployee)
        {
            var sumDayMedical = SelectedEmployeeTimesheet
                .Where(e => e.Status == "Больничный"
                            && e.Employees == workEmployee
                            && e.DateTimeAddData.Month == _monthChoices)
                .Select(p => p.DateTimeAddData)
                .Count();
            return sumDayMedical;
        }

        public int SumDayVacation(Employee workEmployee)
        {
            var sumDayVacation = SelectedEmployeeTimesheet
                .Where(e => e.Status == "Отпуск осн."
                            && e.Employees == workEmployee
                            && e.DateTimeAddData.Month == _monthChoices)
                .Select(p => p.DateTimeAddData)
                .Count();
            return sumDayVacation;
        }

        public int SumDayWorkWeekends(Employee workEmployee)
        {
            var sumDayWorkWeekends = SelectedEmployeeTimesheet
                .Where(e => e.Status == "Работа в праз. и вых."
                            && e.Employees == workEmployee
                            && e.DateTimeAddData.Month == _monthChoices)
                .Select(p => p.DateTimeAddData)
                .Count();
            return sumDayWorkWeekends;
        }
    }
}

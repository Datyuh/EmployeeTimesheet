﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ApplicationContextData;

namespace SelectedLib
{
    public sealed class SelectedWorkModel
    {
        private ApplicationContext _db = new();

        private ObservableCollection<EmployeeTimesheet> SelectedEmployeeTimesheet => SelectedEmployeeTimesheets();
        
        public ObservableCollection<Employee> SelectedEmployee(NameKB nameKb)
        {
            var selectedEmployee = _db.Employees.Select(p => p).Where(p => p != null && p.NameKbId == nameKb.Id);
            return new ObservableCollection<Employee>(selectedEmployee);
        }

        public List<int> SelectedServiceNumbersUsers()
        {
            var selectedServiceNumbersUsers = _db.Employees.Select(p => p.ServiceNumbers);
            return new List<int>(selectedServiceNumbersUsers);
        }

        public ObservableCollection<EmployeeTimesheet> SelectedEmployeeTimesheets()
        {
            var selectedEmployeeTimesheets = _db.EmployeeTimesheets.Select(p => p);
            return new ObservableCollection<EmployeeTimesheet>(selectedEmployeeTimesheets);
        }

        public int SumDayWork(Employee workEmployee)
        {
            var sumDayWork = SelectedEmployeeTimesheet
                .Where(e => e.Status == "Работал" 
                            && e.Employees == workEmployee
                            && e.DateTimeAddData.Month == DateTime.Now.Month)
                .Select(p => p.DateTimeAddData)
                .Count();
            return sumDayWork;
        }

        public int SumDayOwnExpense(Employee workEmployee)
        {
            var sumDayOwnExpense = SelectedEmployeeTimesheet
                .Where(e => e.Status == "ОБС"
                            && e.Employees == workEmployee
                            && e.DateTimeAddData.Month == DateTime.Now.Month)
                .Select(p => p.DateTimeAddData)
                .Count();
            return sumDayOwnExpense;
        }

        public int SumDayMedical(Employee workEmployee)
        {
            var sumDayMedical = SelectedEmployeeTimesheet
                .Where(e => e.Status == "Больничный"
                            && e.Employees == workEmployee
                            && e.DateTimeAddData.Month == DateTime.Now.Month)
                .Select(p => p.DateTimeAddData)
                .Count();
            return sumDayMedical;
        }
    }
}

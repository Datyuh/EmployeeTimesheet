﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ApplicationContextData;

namespace SelectedLib
{
    public sealed class SelectedWorkModel
    {
        private readonly ApplicationContext _db;

        public SelectedWorkModel(ApplicationContext db)
        {
            _db = db;
        }

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
                .Where(e => e.Status == "Явка"
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

        public int SumDayVacation(Employee workEmployee)
        {
            var sumDayVacation = SelectedEmployeeTimesheet
                .Where(e => e.Status == "Отпуск осн."
                            && e.Employees == workEmployee
                            && e.DateTimeAddData.Month == DateTime.Now.Month)
                .Select(p => p.DateTimeAddData)
                .Count();
            return sumDayVacation;
        }

        public int SumDayWorkWeekends(Employee workEmployee)
        {
            var sumDayWorkWeekends = SelectedEmployeeTimesheet
                .Where(e => e.Status == "Работа в праз. и вых."
                            && e.Employees == workEmployee
                            && e.DateTimeAddData.Month == DateTime.Now.Month)
                .Select(p => p.DateTimeAddData)
                .Count();
            return sumDayWorkWeekends;
        }
    }
}

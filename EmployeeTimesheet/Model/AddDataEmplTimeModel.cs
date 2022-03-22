using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using ApplicationContextData;

namespace EmployeeTimesheet.Model
{
    class AddDataEmplTimeModel
    {
        public readonly bool AddChoice;
        private ObservableCollection<WorkWindowModel> _addDataEmployeeTimesheet;
        private ApplicationContext _dbContext;
        private string statusOrder;

        public AddDataEmplTimeModel(ObservableCollection<WorkWindowModel> addDataEmployeeTimesheet)
        {
            _addDataEmployeeTimesheet = addDataEmployeeTimesheet;
            _dbContext = StaticDataModel.ApplicationContext;
            AddChoice = SearchNullInGrid();
        }

        private bool SearchNullInGrid()
        {
            var checkIncluded = _addDataEmployeeTimesheet.Select(x => x.ListReportCards).ToList();
            var weekend = _addDataEmployeeTimesheet.Select(x => x.DateEnterInBases.DayOfWeek);
            if (weekend.Contains(DayOfWeek.Saturday) is true || weekend.Contains(DayOfWeek.Sunday) is true)            
                return false;
            
            else if (checkIncluded.All(p => p == null) || checkIncluded.All(p => p == ""))            
                return true;
            
            else return false;
            
        }

        public bool CheckDateInBase(string numOrder, DateTime? dateOrder)
        {
            foreach (WorkWindowModel items in _addDataEmployeeTimesheet)
            {
                var dateInBse =
                    _dbContext.EmployeeTimesheets
                        .Where(x => x.EmployeesId == items.Employees.Id &&
                                    x.DateTimeAddData == items.DateEnterInBases).Select(x => x.DateTimeAddData)
                        .Contains(items.DateEnterInBases);
                if (dateInBse is false)
                {
                    if (numOrder == null || dateOrder == null)                    
                        return false;
                    else
                        return AddDataInBase(numOrder, dateOrder);
                }
            }
            return false;
        }

        private bool AddDataInBase(string numOrder, DateTime? dateOrder)
        {
            foreach (WorkWindowModel items in _addDataEmployeeTimesheet.Select(p => p).Where(p => p.ListReportCards != null && p.ListReportCards != ""))
            {
                if (items.DateEnterInBases.Date.DayOfWeek == DayOfWeek.Saturday || items.DateEnterInBases.Date.DayOfWeek == DayOfWeek.Sunday)                
                    statusOrder = "Не отгулял(а)";                
                else statusOrder = null;

                ApplicationContextData.EmployeeTimesheet employeeTimesheet = new()
                {
                    Status = items.ListReportCards,
                    DateTimeAddData = items.DateEnterInBases,
                    Employees = items.Employees,  
                    NumOrder = numOrder,
                    DateOrder = dateOrder,
                    StatusOrder = statusOrder,
                };
                _dbContext.EmployeeTimesheets.Add(employeeTimesheet);
                _dbContext.SaveChanges();
                
            }
            return true;
        }

        public bool CanRedirectDataInBase(MessageBoxResult messageResult)
        {
            return messageResult == MessageBoxResult.Yes && RedirectDataInBase();
        }

        private bool RedirectDataInBase()
        {
            foreach (WorkWindowModel items in _addDataEmployeeTimesheet.Select(p => p).Where(p => p.ListReportCards != null && p.ListReportCards != ""))
            {                
                var employee = _dbContext.EmployeeTimesheets
                    .Select(x => x)
                    .Where(x => x.EmployeesId == items.Employees.Id
                                && x.DateTimeAddData == items.DateEnterInBases);
                foreach (var status in employee)
                {
                    status.Status = items.ListReportCards;
                }

                _dbContext.SaveChanges();               
            }
            return true;
        }
    }
}

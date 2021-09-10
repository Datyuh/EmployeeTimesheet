using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using ApplicationContextData;
using System;

namespace EmployeeTimesheet.Model
{
    class AddDataEmplTimeModel
    {
        public readonly bool AddChoice;
        private ObservableCollection<WorkWindowModel> _addDataEmployeeTimesheet;
        private ApplicationContext _dbContext;

        public AddDataEmplTimeModel(ObservableCollection<WorkWindowModel> addDataEmployeeTimesheet)
        {
            _addDataEmployeeTimesheet = addDataEmployeeTimesheet;
            _dbContext = StaticDataModel.ApplicationContext;
            AddChoice = SearchNullInGrid();
        }

        private bool SearchNullInGrid()
        {
            return _addDataEmployeeTimesheet.Select(x => x.ListReportCards).Contains(null);
        }

        public bool CheckDateInBase()
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
                    return AddDataInBase();
                }
            }
            return false;
        }

        private bool AddDataInBase()
        {
            foreach (WorkWindowModel items in _addDataEmployeeTimesheet.Where(p => p.ListReportCards != null && p.DateEnterInBases != DateTime.Now))
            {
                ApplicationContextData.EmployeeTimesheet employeeTimesheet = new()
                {
                    Status = items.ListReportCards,
                    DateTimeAddData = items.DateEnterInBases,
                    Employees = items.Employees,
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
            foreach (WorkWindowModel items in _addDataEmployeeTimesheet)
            {
                if (items.ListReportCards != null)
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
            }
            return true;
        }
    }
}

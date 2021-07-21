using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ApplicationContextData;
using BaseModelModule.Commands;
using BaseModelModule.ViewsModel.Base;
using EmployeeTimesheet.Model;
using EmployeeTimesheet.Window;
using SelectedLib;

namespace EmployeeTimesheet.ViewModel
{
    public class WorkWindowViewModel : BaseViewModel
    {
        private readonly ApplicationContext _db = new();

        private ObservableCollection<WorkWindowModel> _addDataEmployeeTimesheet;
        public ObservableCollection<WorkWindowModel> AddDataEmployeeTimesheet
        {
            get => _addDataEmployeeTimesheet;
            set => Set(ref _addDataEmployeeTimesheet, value);
        }

        private bool _updateStatusUsers;
        public bool UpdateStatusUsers { get => _updateStatusUsers; set => Set(ref _updateStatusUsers, value); }

        #region Command

        public ICommand AddUsersInBaseCommand { get; }
        private bool CanAddUsersInBaseCommandExecute(object p) => true;

        private void OnAddUsersInBaseCommandExecuted(object p)
        {
            AddUsers addUsers = new(this);
            addUsers.Show();
        }

        public ICommand AddDataInBaseCommand { get; }
        private bool CanAddDataInBaseCommandExecute(object p) => true;

        private void OnAddDataInBaseCommandExecuted(object p)
        {
            foreach (WorkWindowModel items in AddDataEmployeeTimesheet)
            {
                ApplicationContextData.EmployeeTimesheet employeeTimesheet = new()
                {
                    Status = items.ListReportCards,
                    DateTimeAddData = items.DateEnterInBases,
                    EmployeesId = items.Id,
                };
                _db.EmployeeTimesheets.Add(employeeTimesheet);
                _db.SaveChanges();
            }
            AddDataEmployeeTimesheet.Clear();
            AddEployeeTimessheet();
        }

        public ICommand UpdateUserStatusCommand { get; }
        private bool CanUpdateUserStatusCommandExecute(object p)
        {
            return UpdateStatusUsers is true;
        }

        private void OnUpdateUserStatusCommandExecuted(object p)
        {
            foreach (WorkWindowModel items in AddDataEmployeeTimesheet)
            {
                if (items.UpdateUserStatusCheckBox is true)
                {
                    Employee employee = _db.Employees.Find(items.Id);
                    if (employee != null) employee.StatusUsers = "Уволен";
                    _db.SaveChanges();
                }
            }
            AddDataEmployeeTimesheet.Clear();
            AddEployeeTimessheet();
        }

        public ICommand GenerateReportCommand { get; }
        private bool CanGenerateReportCommandExecute(object p) => true;

        private void OnGenerateReportCommandExecuted(object p)
        {
           
        }


        #endregion

        public WorkWindowViewModel()
        {
            AddUsersInBaseCommand = new LambdaCommand(OnAddUsersInBaseCommandExecuted, CanAddUsersInBaseCommandExecute);
            AddDataInBaseCommand = new LambdaCommand(OnAddDataInBaseCommandExecuted, CanAddDataInBaseCommandExecute);
            UpdateUserStatusCommand = new LambdaCommand(OnUpdateUserStatusCommandExecuted, CanUpdateUserStatusCommandExecute);
            GenerateReportCommand = new LambdaCommand(OnGenerateReportCommandExecuted, CanGenerateReportCommandExecute);
            AddEployeeTimessheet();
        }

        public void AddEployeeTimessheet()
        {
            SelectedWorkModel selectedWorkModel = new();
            ObservableCollection<Employee> selectedWorkEmployee = selectedWorkModel.SelectedEmployee(StaticDataModel.NameKbFromMain);
            var listReportCard = new List<string> {"Работал", "ОБС", "Больничный"};
            var dateEnterInBase = DateTime.Now.Date;
            AddDataEmployeeTimesheet = new ObservableCollection<WorkWindowModel>();

            foreach (Employee workEmployee in selectedWorkEmployee
                .Select(p => p).Where(p => p.StatusUsers.Contains("Работает")))
            {
                WorkWindowModel workWindowModel = new()
                {
                    Id = workEmployee.Id,
                    Fio = workEmployee.Fio,
                    ServiceNumbers = workEmployee.ServiceNumbers,
                    SumDayWork = selectedWorkModel.SumDayWork(workEmployee),
                    SumDayMedical = selectedWorkModel.SumDayMedical(workEmployee),
                    SumDayOwnExpense = selectedWorkModel.SumDayOwnExpense(workEmployee),
                    ListReportCard = listReportCard,
                    DateEnterInBases = dateEnterInBase
                };
                AddDataEmployeeTimesheet.Add(workWindowModel);
            }
        }
    }
}

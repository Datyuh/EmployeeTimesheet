using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
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
        private bool _haveDateInBase;
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
            
            if (AddDataEmployeeTimesheet.Select(x => x.ListReportCards).Contains(null))
            {
                MessageBox.Show("Не установлен статус работкника\\ов\nна выбранную дату в графе \"Выбор статуса\"",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                foreach (WorkWindowModel items in AddDataEmployeeTimesheet)
                {
                    var dateInBse =
                        StaticDataModel.ApplicationContext.EmployeeTimesheets
                            .Where(x => x.EmployeesId == items.Employees.Id &&
                                        x.DateTimeAddData == items.DateEnterInBases).Select(x => x.DateTimeAddData)
                            .Contains(items.DateEnterInBases);
                    if (dateInBse is false)
                    {
                        _haveDateInBase = true;
                        ApplicationContextData.EmployeeTimesheet employeeTimesheet = new()
                        {
                            Status = items.ListReportCards,
                            DateTimeAddData = items.DateEnterInBases,
                            Employees = items.Employees,
                        };
                        StaticDataModel.ApplicationContext.EmployeeTimesheets.Add(employeeTimesheet);
                        StaticDataModel.ApplicationContext.SaveChanges();
                    }
                    else
                        _haveDateInBase = false;
                }

                switch (_haveDateInBase)
                {
                    case false:
                        MessageBox.Show("Данные с такой датой занесены в таблицу",
                            "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    default:
                        MessageBox.Show("Данные занесены в таблицу",
                            "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
                        AddDataEmployeeTimesheet.Clear();
                        AddEployeeTimessheet();
                        break;
                }
            }
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
                    Employee employee = StaticDataModel.ApplicationContext.Employees.Find(items.Employees.Id);
                    if (employee != null) employee.StatusUsers = "Уволен";
                    StaticDataModel.ApplicationContext.SaveChanges();
                }
            }
            AddDataEmployeeTimesheet.Clear();
            AddEployeeTimessheet();
        }

        public ICommand GenerateReportCommand { get; }
        private bool CanGenerateReportCommandExecute(object p) => true;

        private void OnGenerateReportCommandExecuted(object p)
        {
            ReportOutputWindow reportOutput = new(AddDataEmployeeTimesheet);
            reportOutput.ShowDialog();
        }

        public ICommand AboutProgramCommand { get; }
        private bool CanAboutProgramCommandExecute(object p) => true;

        private void OnAboutProgramCommandExecuted(object p)
        {
            AboutProgram aboutProgram = new();
            aboutProgram.ShowDialog();
        }


        #endregion

        public WorkWindowViewModel()
        {
            AddUsersInBaseCommand = new LambdaCommand(OnAddUsersInBaseCommandExecuted, CanAddUsersInBaseCommandExecute);
            AddDataInBaseCommand = new LambdaCommand(OnAddDataInBaseCommandExecuted, CanAddDataInBaseCommandExecute);
            UpdateUserStatusCommand = new LambdaCommand(OnUpdateUserStatusCommandExecuted, CanUpdateUserStatusCommandExecute);
            GenerateReportCommand = new LambdaCommand(OnGenerateReportCommandExecuted, CanGenerateReportCommandExecute);
            AboutProgramCommand = new LambdaCommand(OnAboutProgramCommandExecuted, CanAboutProgramCommandExecute);
            AddEployeeTimessheet();
        }

        public void AddEployeeTimessheet()
        {
            SelectedWorkModel selectedWorkModel = new(StaticDataModel.ApplicationContext);
            ObservableCollection<Employee> selectedWorkEmployee = selectedWorkModel.SelectedEmployee(StaticDataModel.NameKbFromMain);
            var listReportCard = new List<string> { "Явка", "ОБС", "Больничный", "Отпуск осн.", "Командировка", "Работа в праз. и вых.", "Праздн. и вых. дни" };
            var dateEnterInBase = DateTime.Now.Date;
            AddDataEmployeeTimesheet = new ObservableCollection<WorkWindowModel>();

            foreach (Employee workEmployee in selectedWorkEmployee
                .Select(p => p).Where(p => p.StatusUsers.Contains("Работает")))
            {
                WorkWindowModel workWindowModel = new()
                {
                    Employees = workEmployee,
                    Fio = workEmployee.Fio,
                    ServiceNumbers = workEmployee.ServiceNumbers,
                    SumDayWork = selectedWorkModel.SumDayWork(workEmployee),
                    SumDayWorkWeekends = selectedWorkModel.SumDayWorkWeekends(workEmployee),
                    SumDayMedical = selectedWorkModel.SumDayMedical(workEmployee),
                    SumDayOwnExpense = selectedWorkModel.SumDayOwnExpense(workEmployee),
                    SumDayVacation = selectedWorkModel.SumDayVacation(workEmployee),
                    ListReportCard = listReportCard,
                    DateEnterInBases = dateEnterInBase
                };
                AddDataEmployeeTimesheet.Add(workWindowModel);
            }
        }
    }
}

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
        private string _selectedNameKb;
        private ObservableCollection<Employee> _selectedWorkEmployee;

        private ObservableCollection<WorkWindowModel> _addDataEmployeeTimesheet;
        public ObservableCollection<WorkWindowModel> AddDataEmployeeTimesheet
        {
            get => _addDataEmployeeTimesheet;
            set => Set(ref _addDataEmployeeTimesheet, value);
        }

        private bool _updateStatusUsers;
        public bool UpdateStatusUsers { get => _updateStatusUsers; set => Set(ref _updateStatusUsers, value); }

        private bool _forChiefDesignerEnabled = false;
        public bool ForChiefDesignerEnabled { get => _forChiefDesignerEnabled; set => Set(ref _forChiefDesignerEnabled, value); }

        private ObservableCollection<NameKB> _listNameKb;
        public ObservableCollection<NameKB> ListNameKb { get => _listNameKb; set => Set(ref _listNameKb, value); }

        private NameKB _selectedNameKbs;
        public NameKB SelectedNameKbs { get => _selectedNameKbs; set => Set(ref _selectedNameKbs, value); }

        #region Command

        public ICommand AddUsersInBaseCommand { get; }
        private bool CanAddUsersInBaseCommandExecute(object p)
        {
            return _selectedNameKb != "Главный констр";
        }

        private void OnAddUsersInBaseCommandExecuted(object p)
        {
            AddUsers addUsers = new(this);
            addUsers.Show();
        }

        public ICommand AddDataInBaseCommand { get; }

        private bool CanAddDataInBaseCommandExecute(object p)
        {
            return _selectedNameKb != "Главный констр";
        }

        private void OnAddDataInBaseCommandExecuted(object p)
        {
            AddDataEmplTimeModel addDataEmplTimeModel = new(AddDataEmployeeTimesheet);
            var searchNullInGrid = addDataEmplTimeModel.AddChoice;
            var nowDateInBase = new SelectedWorkModel(StaticDataModel.ApplicationContext).NowDateInBase().Contains(DateTime.Now.Date);
            if (searchNullInGrid is true && nowDateInBase is false)
            {
                MessageBox.Show("Не установлен статус работкника\\ов\nна выбранную дату в графе \"Выбор статуса\"",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                var checkDateInBase = addDataEmplTimeModel.CheckDateInBase();
                _haveDateInBase = checkDateInBase is true;

                switch (_haveDateInBase)
                {
                    case false:
                        var addInBase = MessageBox.Show("Данные с такой датой уже занесены в таблицу.\nИзменить статус?",
                            "Ошибка", MessageBoxButton.YesNo, MessageBoxImage.Information);
                        var canRedirect = addDataEmplTimeModel.CanRedirectDataInBase(addInBase);
                        if (canRedirect is true)
                            goto default;
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
            return UpdateStatusUsers is true && _selectedNameKb != "Главный констр";
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
            if (_selectedNameKb == "Главный констр")
            {
                StaticDataModel.NameKbFromMain = SelectedNameKbs;
            }
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

        public ICommand ShowEmployeeStatusCommand { get; }
        private bool CanShowEmployeeStatusCommandExecute(object p)
        {
            return _selectedNameKb == "Главный констр";
        }

        private void OnShowEmployeeStatusCommandExecuted(object p)
        {
            AddEployeeTimessheet();
        }


        #endregion

        public WorkWindowViewModel(ObservableCollection<NameKB> nameKbs, string selectedNameKb)
        {
            ListNameKb = nameKbs;
            _selectedNameKb = selectedNameKb;
            AddUsersInBaseCommand = new LambdaCommand(OnAddUsersInBaseCommandExecuted, CanAddUsersInBaseCommandExecute);
            AddDataInBaseCommand = new LambdaCommand(OnAddDataInBaseCommandExecuted, CanAddDataInBaseCommandExecute);
            UpdateUserStatusCommand = new LambdaCommand(OnUpdateUserStatusCommandExecuted, CanUpdateUserStatusCommandExecute);
            GenerateReportCommand = new LambdaCommand(OnGenerateReportCommandExecuted, CanGenerateReportCommandExecute);
            AboutProgramCommand = new LambdaCommand(OnAboutProgramCommandExecuted, CanAboutProgramCommandExecute);
            ShowEmployeeStatusCommand = new LambdaCommand(OnShowEmployeeStatusCommandExecuted, CanShowEmployeeStatusCommandExecute);
            AddEployeeTimessheet();
        }

        public void AddEployeeTimessheet()
        {
            if (_selectedNameKb == "Главный констр")
            {
                ForChiefDesignerEnabled = true;
                StaticDataModel.NameKbFromMain = SelectedNameKbs;
            }
            SelectedWorkModel selectedWorkModel = new(StaticDataModel.ApplicationContext);
            if (StaticDataModel.NameKbFromMain != null)
            {
                _selectedWorkEmployee = selectedWorkModel.SelectedEmployee(StaticDataModel.NameKbFromMain);
                var listReportCard = new List<string>
                {
                    /*
                        Если данные поменялись, то их необходимо изменить 
                        в классе WorkingWithExcelModel 
                        в методе AddRowDate
                     */
                    "Явка", "ОБС", "Пол. дня ОБС", "Больничный",
                    "Отпуск осн.", "Командировка",
                    "Работа в праз. и вых.", "Праздн. и вых. дни"
                };

                var dateEnterInBase = DateTime.Now.Date;
                AddDataEmployeeTimesheet = new ObservableCollection<WorkWindowModel>();

                foreach (Employee workEmployee in _selectedWorkEmployee
                    .Select(p => p).Where(p => p.StatusUsers.Contains("Работает")))
                {
                    WorkWindowModel workWindowModel = new()
                    {
                        Employees = workEmployee,
                        Fio = workEmployee.Fio,
                        ServiceNumbers = workEmployee.ServiceNumbers,
                        SumDayWork = selectedWorkModel.SumDayWork(workEmployee) +
                                     selectedWorkModel.SumHalfDayWork(workEmployee),
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
}

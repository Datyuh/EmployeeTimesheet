using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
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

        private DateTime? DateOrder { get; set; }
        private string NumOrder { get; set; }

        private ObservableCollection<Employee> _selectedWorkEmployee;

        private ObservableCollection<WorkWindowModel> _addDataEmployeeTimesheet;
        public ObservableCollection<WorkWindowModel> AddDataEmployeeTimesheet
        {
            get => _addDataEmployeeTimesheet;
            set => Set(ref _addDataEmployeeTimesheet, value);
        }

        #region WorkWithChoiceData

        private string[] _nameMonthAdd;
        public string[] NameMonthAdd { get => _nameMonthAdd; set => Set(ref _nameMonthAdd, value); }

        private string _nameMonthSelect;
        public string NameMonthSelect { get => _nameMonthSelect; set => Set(ref _nameMonthSelect, value); }

        private int _nameMonthChoice;
        public int NameMonthChoice { get => _nameMonthChoice; set => Set(ref _nameMonthChoice, value); }

        private List<int> _nameYearAdd;
        public List<int> NameYearAdd { get => _nameYearAdd; set => Set(ref _nameYearAdd, value); }

        private int _nameYearSelect;
        public int NameYearSelect { get => _nameYearSelect; set => Set(ref _nameYearSelect, value); }

        private int _nameYearChoice;
        public int NameYearChoice { get => _nameYearChoice; set => Set(ref _nameYearChoice, value); }

        #endregion

        private int _excelExecution;
        public int ExcelExecution { get => _excelExecution; set => Set(ref _excelExecution, value); }

        private bool _updateStatusUsers;
        public bool UpdateStatusUsers { get => _updateStatusUsers; set => Set(ref _updateStatusUsers, value); }

        private bool _UpdateStatusUsersEnabled = true;
        public bool UpdateStatusUsersEnabled { get => _UpdateStatusUsersEnabled; set => Set(ref _UpdateStatusUsersEnabled, value); }

        private bool _updateKbUsers;
        public bool UpdateKbUsers { get => _updateKbUsers; set => Set(ref _updateKbUsers, value); }

        private bool _updateKbUsersEnabled = true;
        public bool UpdateKbUsersEnabled { get => _updateKbUsersEnabled; set => Set(ref _updateKbUsersEnabled, value); }

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
            var ordersOutGrid = AddDataEmployeeTimesheet.Select(p => p.ListReportCards).Contains("Работа в праз. и вых.");
            AddDataEmplTimeModel addDataEmplTimeModel = new(AddDataEmployeeTimesheet);
            var searchNullInGrid = addDataEmplTimeModel.AddChoice;
            var nowDateInBase = new SelectedWorkModel(StaticDataModel.ApplicationContext).NowDateInBase().Contains(DateTime.Now.Date);
            if (searchNullInGrid is true)
            {
                MessageBox.Show("Не установлен статус работкника\\ов\nна выбранную дату в графе \"Выбор статуса\"",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                NumOrder = null;
                DateOrder = null;
                if (ordersOutGrid == true)
                {
                    AddNumOrder addNumOrder = new();
                    addNumOrder.ShowDialog();
                    NumOrder = addNumOrder.OutOrders().NumOrder;
                    DateOrder = addNumOrder.OutOrders().DateOrder;
                }

                var checkDateInBase = addDataEmplTimeModel.CheckDateInBase(NumOrder, DateOrder);
                _haveDateInBase = checkDateInBase;

                switch (_haveDateInBase)
                {
                    case false:
                        var addInBase = MessageBox.Show("Данные с такой датой уже занесены в таблицу.\nИзменить статус?",
                        "Ошибка", MessageBoxButton.YesNo, MessageBoxImage.Information);
                        var canRedirect = addDataEmplTimeModel.CanRedirectDataInBase(addInBase, NumOrder, DateOrder);
                        if (canRedirect is true)
                            goto default;
                        break;

                    default:
                        MessageBox.Show("Данные занесены в таблицу",
                            "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                        RefreshEployeeTimessheet();
                        //AddDataEmployeeTimesheet.Clear();
                        //AddEployeeTimessheet();
                        break;
                }

            }
        }

        public ICommand UpdateUserStatusCommand { get; }
        private bool CanUpdateUserStatusCommandExecute(object p)
        {
            return UpdateStatusUsers is true || UpdateKbUsers is true && _selectedNameKb != "Главный констр";
        }

        private void OnUpdateUserStatusCommandExecuted(object p)
        {
            foreach (WorkWindowModel items in AddDataEmployeeTimesheet)
            {
                if (items.UpdateUserStatusCheckBox is true)
                {
                    if (UpdateStatusUsers is true)
                    {
                        Employee employee = StaticDataModel.ApplicationContext.Employees.Find(items.Employees.Id);
                        if (employee != null) employee.StatusUsers = "Уволен";
                    }

                    else if (UpdateKbUsers is true)
                    {
                        Employee employee = StaticDataModel.ApplicationContext.Employees.Find(items.Employees.Id);
                        if (employee != null) employee.NameKbId = SelectedNameKbs.Id;
                    }

                    StaticDataModel.ApplicationContext.SaveChanges();
                }
            }
            AddDataEmployeeTimesheet.Clear();
            AddEployeeTimessheet();
        }

        #region CheckBoxCommand

        public ICommand UpdateStatusUsersCommand { get; }
        private bool CanUpdateStatusUsersCommandExecute(object p)
        {
            return UpdateKbUsers is false && _selectedNameKb != "Главный констр";
        }

        private void OnUpdateStatusUsersCommandExecuted(object p)
        {

        }

        public ICommand UpdateKbUsersCommand { get; }
        private bool CanUpdateKbUsersCommandExecute(object p)
        {
            return UpdateStatusUsers is false && _selectedNameKb != "Главный констр";
        }

        private void OnUpdateKbUsersCommandExecuted(object p)
        {
            if (UpdateKbUsers is true)
            {
                ForChiefDesignerEnabled = true;
            }
            else
            {
                //ListNameKb.Clear();
                ForChiefDesignerEnabled = false;
            }
        }

        public ICommand ShowWorkWeekendsCommand { get; }
        private bool CanShowWorkWeekendsCommandExecute(object p)
        {
            return _selectedNameKb != "Главный констр";
        }

        private void OnShowWorkWeekendsCommandExecuted(object p)
        {
            OrdersWindow ordersWindow = new(AddDataEmployeeTimesheet, NameYearAdd);
            ordersWindow.ShowDialog();
        }

        #endregion


        public ICommand GenerateReportCommand { get; }
        private bool CanGenerateReportCommandExecute(object p) => true;

        private void OnGenerateReportCommandExecuted(object p)
        {
            if (_selectedNameKb == "Главный констр")
            {
                StaticDataModel.NameKbFromMain = SelectedNameKbs;
            }
            ReportOutputWindow reportOutput = new(AddDataEmployeeTimesheet, NameMonthSelect, NameYearSelect);
            reportOutput.ShowDialog();
        }

        public ICommand AboutProgramCommand { get; }
        private bool CanAboutProgramCommandExecute(object p) => true;

        private void OnAboutProgramCommandExecuted(object p)
        {
            AboutProgram aboutProgram = new();
            aboutProgram.ShowDialog();
        }

        //public ICommand ShowEmployeeStatusCommand { get; }
        //private bool CanShowEmployeeStatusCommandExecute(object p) => true;

        //private void OnShowEmployeeStatusCommandExecuted(object p)
        //{
        //    RefreshEployeeTimessheet();
        //    //AddEployeeTimessheet();
        //}


        #endregion

        public WorkWindowViewModel(ObservableCollection<NameKB> nameKbs, string selectedNameKb)
        {
            ListNameKb = new ObservableCollection<NameKB>(nameKbs.Where(p => p.NameKbOgk != "Главный констр").Select(p => p));
            _selectedNameKb = selectedNameKb;

            AddUsersInBaseCommand = new LambdaCommand(OnAddUsersInBaseCommandExecuted, CanAddUsersInBaseCommandExecute);
            AddDataInBaseCommand = new LambdaCommand(OnAddDataInBaseCommandExecuted, CanAddDataInBaseCommandExecute);
            UpdateUserStatusCommand = new LambdaCommand(OnUpdateUserStatusCommandExecuted, CanUpdateUserStatusCommandExecute);
            GenerateReportCommand = new LambdaCommand(OnGenerateReportCommandExecuted, CanGenerateReportCommandExecute);
            AboutProgramCommand = new LambdaCommand(OnAboutProgramCommandExecuted, CanAboutProgramCommandExecute);
            //ShowEmployeeStatusCommand = new LambdaCommand(OnShowEmployeeStatusCommandExecuted, CanShowEmployeeStatusCommandExecute);
            UpdateStatusUsersCommand = new LambdaCommand(OnUpdateStatusUsersCommandExecuted, CanUpdateStatusUsersCommandExecute);
            UpdateKbUsersCommand = new LambdaCommand(OnUpdateKbUsersCommandExecuted, CanUpdateKbUsersCommandExecute);
            ShowWorkWeekendsCommand = new LambdaCommand(OnShowWorkWeekendsCommandExecuted, CanShowWorkWeekendsCommandExecute);

            NameYearAdd = new SelectedWorkModel(StaticDataModel.ApplicationContext).SelectedYearInBase();
            NameMonthAdd = DateTimeFormatInfo.CurrentInfo.MonthNames;

            NameMonthChoice = DateTime.Now.Month - 1;
            NameYearChoice = NameYearAdd.Count - 1;
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
            selectedWorkModel._monthChoices = NameMonthRetInt.NameMonth(NameMonthSelect);
            selectedWorkModel._yearChoices = NameMonthRetInt.NameYear(NameYearSelect);
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
                    "Явка", "Удаленная работа", "ОБС", "Пол. дня ОБС", "Больничный",
                    "Отпуск осн.", "Командировка",
                    "Работа в праз. и вых.", "Праздн. и вых. дни", "",
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
                        SumDayOwnExpense = selectedWorkModel.SumDayOwnExpense(workEmployee) +
                                     selectedWorkModel.SumHalfDayWork(workEmployee),
                        SumDayRemoteWork = selectedWorkModel.SumDayRemoteWork(workEmployee),
                        SumDayVacation = selectedWorkModel.SumDayVacation(workEmployee),
                        SumDayWeekendWork = selectedWorkModel.SumWeekendWork(workEmployee),
                        ListReportCard = listReportCard,
                        DateEnterInBases = dateEnterInBase
                    };
                    AddDataEmployeeTimesheet.Add(workWindowModel);
                }
            }
        }

        public void RefreshEployeeTimessheet()
        {
            SelectedWorkModel selectedWorkModel = new(StaticDataModel.ApplicationContext);
            selectedWorkModel._monthChoices = NameMonthRetInt.NameMonth(NameMonthSelect);
            selectedWorkModel._yearChoices = NameMonthRetInt.NameYear(NameYearSelect);
            foreach (Employee workEmployee in _selectedWorkEmployee
                    .Select(p => p).Where(p => p.StatusUsers.Contains("Работает")))
            {
                WorkWindowModel workWindowModelItem = AddDataEmployeeTimesheet.FirstOrDefault(p => p.Fio == workEmployee.Fio);
                workWindowModelItem.SumDayWork = selectedWorkModel.SumDayWork(workEmployee) +
                                         selectedWorkModel.SumHalfDayWork(workEmployee);
                workWindowModelItem.SumDayWorkWeekends = selectedWorkModel.SumDayWorkWeekends(workEmployee);
                workWindowModelItem.SumDayMedical = selectedWorkModel.SumDayMedical(workEmployee);
                workWindowModelItem.SumDayOwnExpense = selectedWorkModel.SumDayOwnExpense(workEmployee) +
                                     selectedWorkModel.SumHalfDayWork(workEmployee);
                workWindowModelItem.SumDayRemoteWork = selectedWorkModel.SumDayRemoteWork(workEmployee);
                workWindowModelItem.SumDayVacation = selectedWorkModel.SumDayVacation(workEmployee);
                workWindowModelItem.SumDayWeekendWork = selectedWorkModel.SumWeekendWork(workEmployee);
            }
            CollectionViewSource.GetDefaultView(AddDataEmployeeTimesheet).Refresh();
        }
    }
}

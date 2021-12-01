using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using BaseModelModule.Commands;
using BaseModelModule.ViewsModel.Base;
using EmployeeTimesheet.Model;
using SelectedLib;

namespace EmployeeTimesheet.ViewModel
{
    class ReportOutputViewModel : BaseViewModel
    {
        public Action CloseAction { get; set; }

        private ObservableCollection<WorkWindowModel> _addDataEmployeeTimesheet;

        private string[] _nameMonthAdd;
        public string[] NameMonthAdd { get => _nameMonthAdd; set => Set(ref _nameMonthAdd, value); }

        private string _nameMonthSelect;
        public string NameMonthSelect { get => _nameMonthSelect; set => Set(ref _nameMonthSelect, value); }

        private int _nameMonthChoice;
        public int NameMonthChoice { get => _nameMonthChoice; set => Set(ref _nameMonthChoice, value); }

        private ObservableCollection<int> _nameYearAdd;
        public ObservableCollection<int> NameYearAdd { get => _nameYearAdd; set => Set(ref _nameYearAdd, value); }

        private int _nameYearSelect;
        public int NameYearSelect { get => _nameYearSelect; set => Set(ref _nameYearSelect, value); }

        private int _nameYearChoice;
        public int NameYearChoice { get => _nameYearChoice; set => Set(ref _nameYearChoice, value); }

        public ICommand OutputFullReportCommand { get; }
        private bool CanOutputFullReportCommandExecute(object p) => true;

        private void OnOutputFullReportCommandExecuted(object p)
        {
            CloseAction();
            var nameMonthChoice = NameMonthRetInt.NameMonth(NameMonthSelect);            
            var selectedEmployee = new SelectedForExcel(StaticDataModel.ApplicationContext);
            _ = new WorkingWithExcelModel(selectedEmployee.SelectedDateEmplTime(StaticDataModel.NameKbFromMain), nameMonthChoice, NameYearSelect);
        }

        public ICommand OutputShortReportCommand { get; }
        private bool CanOutputShortReportCommandExecute(object p) => true;

        private void OnOutputShortReportCommandExecuted(object p)
        {
            CloseAction();
            var nameMonthChoice = NameMonthRetInt.NameMonth(NameMonthSelect);
            _ = new WorkingExcelShortReportModel(_addDataEmployeeTimesheet, nameMonthChoice);
        }

        public ReportOutputViewModel(ObservableCollection<WorkWindowModel> addDataEmployeeTimesheet)
        {
            _addDataEmployeeTimesheet = addDataEmployeeTimesheet;
            OutputFullReportCommand =
                new LambdaCommand(OnOutputFullReportCommandExecuted, CanOutputFullReportCommandExecute);
            OutputShortReportCommand =
                new LambdaCommand(OnOutputShortReportCommandExecuted, CanOutputShortReportCommandExecute);
            NameMonthAdd = DateTimeFormatInfo.CurrentInfo.MonthNames;
            NameYearAdd = new SelectedWorkModel(StaticDataModel.ApplicationContext).SelectedYearInBase();
            NameMonthChoice = DateTime.Now.Month - 1;
        }
    }
}
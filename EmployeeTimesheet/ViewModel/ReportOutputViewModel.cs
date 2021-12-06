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

        private int _nameYearSelect;
        private string _nameMonthSelect;
     
        public ICommand OutputFullReportCommand { get; }
        private bool CanOutputFullReportCommandExecute(object p) => true;

        private void OnOutputFullReportCommandExecuted(object p)
        {
            CloseAction();
            var nameMonthChoice = NameMonthRetInt.NameMonth(_nameMonthSelect);
            var selectedEmployee = new SelectedForExcel(StaticDataModel.ApplicationContext);
            _ = new WorkingWithExcelModel(selectedEmployee.SelectedDateEmplTime(StaticDataModel.NameKbFromMain), nameMonthChoice, _nameYearSelect);
        }

        public ICommand OutputShortReportCommand { get; }
        private bool CanOutputShortReportCommandExecute(object p) => true;

        private void OnOutputShortReportCommandExecuted(object p)
        {
            CloseAction();
            var nameMonthChoice = NameMonthRetInt.NameMonth(_nameMonthSelect);
            _ = new WorkingExcelShortReportModel(_addDataEmployeeTimesheet, nameMonthChoice);
        }

        public ReportOutputViewModel(ObservableCollection<WorkWindowModel> addDataEmployeeTimesheet, string nameMonthSelect, int nameYearSelect)
        {
            _nameMonthSelect = nameMonthSelect;
            _nameYearSelect = nameYearSelect;
            _addDataEmployeeTimesheet = addDataEmployeeTimesheet;
            OutputFullReportCommand =
                new LambdaCommand(OnOutputFullReportCommandExecuted, CanOutputFullReportCommandExecute);
            OutputShortReportCommand =
                new LambdaCommand(OnOutputShortReportCommandExecuted, CanOutputShortReportCommandExecute);           
        }
    }
}
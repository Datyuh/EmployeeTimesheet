using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using BaseModelModule.Commands;
using EmployeeTimesheet.Model;
using SelectedLib;

namespace EmployeeTimesheet.ViewModel
{
    class ReportOutputViewModel
    {
        public Action CloseAction { get; set; }

        private ObservableCollection<WorkWindowModel> _addDataEmployeeTimesheet;
        public ICommand OutputFullReportCommand { get; }
        private bool CanOutputFullReportCommandExecute(object p) => true;

        private void OnOutputFullReportCommandExecuted(object p)
        {
            CloseAction();
            var selectedEmployee = new SelectedForExcel(StaticDataModel.ApplicationContext);
            _ = new WorkingWithExcelModel(selectedEmployee.SelectedDateEmplTime(StaticDataModel.NameKbFromMain));
        }

        public ICommand OutputShortReportCommand { get; }
        private bool CanOutputShortReportCommandExecute(object p) => true;

        private void OnOutputShortReportCommandExecuted(object p)
        {
            CloseAction();
            _ = new WorkingExcelShortReportModel(_addDataEmployeeTimesheet);
        }

        public ReportOutputViewModel(ObservableCollection<WorkWindowModel> addDataEmployeeTimesheet)
        {
            _addDataEmployeeTimesheet = addDataEmployeeTimesheet;
            OutputFullReportCommand =
                new LambdaCommand(OnOutputFullReportCommandExecuted, CanOutputFullReportCommandExecute);
            OutputShortReportCommand =
                new LambdaCommand(OnOutputShortReportCommandExecuted, CanOutputShortReportCommandExecute);
        }
    }
}

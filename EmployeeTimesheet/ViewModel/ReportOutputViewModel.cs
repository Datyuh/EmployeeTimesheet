using System.Collections.ObjectModel;
using System.Windows.Input;
using BaseModelModule.Commands;
using EmployeeTimesheet.Model;

namespace EmployeeTimesheet.ViewModel
{
    class ReportOutputViewModel
    {
        private ObservableCollection<WorkWindowModel> _addDataEmployeeTimesheet;
        public ICommand OutputFullReportCommand { get; }
        private bool CanOutputFullReportCommandExecute(object p) => true;

        private void OnOutputFullReportCommandExecuted(object p)
        {
           
        }

        public ICommand OutputShortReportCommand { get; }
        private bool CanOutputShortReportCommandExecute(object p) => true;

        private void OnOutputShortReportCommandExecuted(object p)
        {
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

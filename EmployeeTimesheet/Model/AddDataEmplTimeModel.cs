using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace EmployeeTimesheet.Model
{
    class AddDataEmplTimeModel
    {
        public readonly bool AddChoice;
        private ObservableCollection<WorkWindowModel> _addDataEmployeeTimesheet;
       
        public AddDataEmplTimeModel(ObservableCollection<WorkWindowModel> addDataEmployeeTimesheet)
        {
            _addDataEmployeeTimesheet = addDataEmployeeTimesheet;
            AddChoice = SearchNullInGrid();
        }

        private bool SearchNullInGrid()
        {
            return _addDataEmployeeTimesheet.Select(x => x.ListReportCards).Contains(null);
        }

        public bool CheckDateInBase()
        {
            return true;
        }

        private bool AddDataInBase()
        {
            return true;
        }

        public bool CanRedirectDataInBase(MessageBoxResult messageResult)
        {
            return messageResult == MessageBoxResult.Yes && RedirectDataInBase();
        }

        private bool RedirectDataInBase()
        {
            return true;
        }
    }
}

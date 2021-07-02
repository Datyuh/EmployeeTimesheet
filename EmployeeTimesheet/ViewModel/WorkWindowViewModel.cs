using System.Collections.ObjectModel;
using System.Windows.Input;
using ApplicationContextData;
using BaseModelModule.Commands;
using BaseModelModule.ViewsModel.Base;
using EmployeeTimesheet.Model;
using EmployeeTimesheet.Window;
using SelectedLib;

namespace EmployeeTimesheet.ViewModel
{
    class WorkWindowViewModel : BaseViewModel
    {
        private readonly SelectedItems _selected = new();

        //private List<string> _listReportCard;
        //public List<string> ListReportCard
        //{
        //    get => _listReportCard;
        //    set => Set(ref _listReportCard, value);
        //}

        private bool _updateUserStatusCheckBox;
        public bool UpdateUserStatusCheckBox
        {
            get => _updateUserStatusCheckBox;
            set => Set(ref _updateUserStatusCheckBox, value);
        }

        private ObservableCollection<object> _addDataEmployeeTimesheet;
        public ObservableCollection<object> AddDataEmployeeTimesheet
        {
            get => _addDataEmployeeTimesheet;
            set => Set(ref _addDataEmployeeTimesheet, value);
        }

        public ICommand AddUsersInBaseCommand { get; }
        private bool CanAddUsersInBaseCommandExecute(object p) => true;
        private void OnAddUsersInBaseCommandExecuted(object p)
        {
            var addUsers = new AddUsers();
            addUsers.ShowDialog();
        }

        public WorkWindowViewModel()
        {
            SelectedWorkModel selectedWorkModel = new();
            //ListReportCard = new List<string> { "Работал", "ОБС", "Больничный" };
            AddUsersInBaseCommand = new LambdaCommand(OnAddUsersInBaseCommandExecuted, CanAddUsersInBaseCommandExecute);
            AddDataEmployeeTimesheet = new ObservableCollection<object>
            {
                new WorkWindowModel(new Employee())

            };
        }
    }
}

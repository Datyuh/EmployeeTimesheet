using EmployeeTimesheet.ViewModel;

namespace EmployeeTimesheet.Window
{
    /// <summary>
    /// Логика взаимодействия для AddUsers.xaml
    /// </summary>
    public partial class AddUsers
    {
        private AddUsersViewModel _addUsersViewModel;
        public AddUsers(WorkWindowViewModel owner)
        {
            _addUsersViewModel = new AddUsersViewModel(owner);
            InitializeComponent();
            DataContext = _addUsersViewModel;
        }
    }
}

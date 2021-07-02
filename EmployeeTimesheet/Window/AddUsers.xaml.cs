using EmployeeTimesheet.ViewModel;

namespace EmployeeTimesheet.Window
{
    /// <summary>
    /// Логика взаимодействия для AddUsers.xaml
    /// </summary>
    public partial class AddUsers
    {
        public AddUsers()
        {
            InitializeComponent();
            DataContext = new AddUsersViewModel();
        }
    }
}

using System.Collections.ObjectModel;
using EmployeeTimesheet.Model;
using EmployeeTimesheet.ViewModel;

namespace EmployeeTimesheet.Window
{
    /// <summary>
    /// Логика взаимодействия для ReportOutputWindow.xaml
    /// </summary>
    public partial class ReportOutputWindow : System.Windows.Window
    {
        public ReportOutputWindow(ObservableCollection<WorkWindowModel> addDataEmployeeTimesheet)
        {
            InitializeComponent();
            DataContext = new ReportOutputViewModel(addDataEmployeeTimesheet);
        }
    }
}

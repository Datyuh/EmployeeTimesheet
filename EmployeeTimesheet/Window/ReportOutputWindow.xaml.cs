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
        public ReportOutputWindow(ObservableCollection<WorkWindowModel> addDataEmployeeTimesheet, string nameMonthSelect, int nameYearSelect)
        {
            InitializeComponent();
            ReportOutputViewModel reportOutputViewModel = new(addDataEmployeeTimesheet, nameMonthSelect, nameYearSelect);
            DataContext = reportOutputViewModel;
            reportOutputViewModel.CloseAction = Close;
        }
    }
}

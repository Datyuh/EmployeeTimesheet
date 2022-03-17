using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ApplicationContextData;
using EmployeeTimesheet.ViewModel;

namespace EmployeeTimesheet.Window
{
    /// <summary>
    /// Логика взаимодействия для WorkWindow.xaml
    /// </summary>
    public partial class WorkWindow
    {
        private WorkWindowViewModel _workWindowViewModel;
        public WorkWindow(ObservableCollection<NameKB> nameKbs, string selectedNameKb)
        {
            InitializeComponent();
            _workWindowViewModel = new WorkWindowViewModel(nameKbs, selectedNameKb);
            DataContext = _workWindowViewModel;
        }

        private void CheckBox_MouseEnter(object sender, MouseEventArgs e)
        {
            TextWarning.Text = "Обновляет статус работника отмеченый галочкой в таблице с работает на уволен. После нажатия кнопи обновить даный работник больше не будет показываться в таблице";
        }

        private void CheckBox_MouseEnter_1(object sender, MouseEventArgs e)
        {
            TextWarning.Text = "Переводит сотрудника в другое КБ. Перевод может осуществлять как передающие КБ так и принимающие.";        
        }

        private async void CheckBox_MouseLeave(object sender, MouseEventArgs e)
        {
            await Task.Delay(15000);
            TextWarning.Text = null;
        }

        private async void CheckBox_MouseLeave_1(object sender, MouseEventArgs e)
        {
            await Task.Delay(15000);
            TextWarning.Text = null;
        }

        private void ComboBox_DropDownClosed(object sender, System.EventArgs e)
        {
            _workWindowViewModel.RefreshEployeeTimessheet();
        }
    }
}

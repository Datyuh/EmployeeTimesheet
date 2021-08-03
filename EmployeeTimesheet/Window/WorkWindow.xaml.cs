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
        public WorkWindow(ObservableCollection<NameKB> nameKbs, string selectedNameKb)
        {
            InitializeComponent();
            DataContext = new WorkWindowViewModel(nameKbs, selectedNameKb);
        }

        private async void CheckBox_MouseEnter(object sender, MouseEventArgs e)
        {
            TextWarning.Text = "Обновляет статус работника отмеченый галочкой в таблице с работает на уволен. После нажатия кнопи обновить даный работник больше не будет показываться в таблице";
            await Task.Delay(10000);
            TextWarning.Text = null;
        }
    }
}

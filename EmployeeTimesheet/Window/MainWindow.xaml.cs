using System.Threading.Tasks;
using EmployeeTimesheet.ViewModel;

namespace EmployeeTimesheet.Window
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MainWindowViewModel mainWindowViewModel = new();
            DataContext = mainWindowViewModel;
            mainWindowViewModel.CloseAction = Close;
        }

        private async void TextBox_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Attention.Text = "При отсутствии пароля обратиться к системному администратору!!!";
            await Task.Delay(10000);
            Attention.Text = null;
        }
    }
}

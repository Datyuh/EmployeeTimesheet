using System.Collections.ObjectModel;
using System.Windows.Input;
using BaseModelModule.Commands;
using BaseModelModule.ViewsModel.Base;
using EmployeeTimesheet.Model;

namespace EmployeeTimesheet.ViewModel
{
    class AddUsersViewModel : BaseViewModel
    {
        private bool _nullRowGrid;
        private readonly WorkWindowViewModel _workWindow;
        private ObservableCollection<AddUserModel> _addUsersInGrid = new();
        public ObservableCollection<AddUserModel> AddUsersInGrid { get => _addUsersInGrid; set => Set(ref _addUsersInGrid, value); }

        public ICommand AddUsersNewRowCommand { get; }
        private bool CanAddUsersNewRowCommandExecute(object p) => true;
        private void OnAddUsersNewRowCommandExecuted(object p)
        {
            AddNullRow();
        }

        public ICommand AddUsersInBaseCommand { get; }
        private bool CanAddUsersInBaseCommandExecute(object p) => true;
        private void OnAddUsersInBaseCommandExecuted(object p)
        {
            //SelectedWorkModel selectedWorkModel = new(StaticDataModel.ApplicationContext);
            //List<int> checkUsers = selectedWorkModel.SelectedServiceNumbersUsers();
            //try
            //{
            //    foreach (AddUserModel item in AddUsersInGrid)
            //    {
            //        if (item.FirstName != null && item.LastName != null &&
            //            item.PatronymicName != null && item.ServiceNumbers != null)
            //        {
            //            int convertServiceNumbers = Convert.ToInt32(item.ServiceNumbers);

            //            Employee employee = new()
            //            {
            //                Fio = $"{item.LastName} {item.FirstName} {item.PatronymicName}",
            //                ServiceNumbers = convertServiceNumbers,
            //                StatusUsers = "Работает",
            //                NameKbs = StaticDataModel.NameKbFromMain,
            //            };

            //            if (checkUsers.Contains(employee.ServiceNumbers))
            //            {
            //                MessageBox.Show("Проверте введенные данные\nОдин или несколько сотрудников уже имеются в таблице\n" +
            //                                "Для удаления из таблицы выделите сотрудника и нажмите кнопку Delete",
            //                    "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
            //            }
            //            else
            //            {
            //                StaticDataModel.ApplicationContext.Employees.Add(employee);
            //                StaticDataModel.ApplicationContext.SaveChanges();
            //                _workWindow.AddDataEmployeeTimesheet.Clear();
            //                _workWindow.AddEployeeTimessheet();
            //                _nullRowGrid = true;
            //            }
            //        }
            //        else
            //            _nullRowGrid = false;
            //    }

            //    switch (_nullRowGrid)
            //    {
            //        case false:
            //            MessageBox.Show("Не все поля заполнены", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
            //            break;
            //        default:
            //            MessageBox.Show("Данные добавлены в базу", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
            //            break;
            //    }
            //}
            //catch (FormatException)
            //{
            //    MessageBox.Show("Введите целые числа в столбец \"Таб. номер\"", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
            //}
        }

        public AddUsersViewModel(WorkWindowViewModel owner)
        {
            _workWindow = owner;
            AddUsersInGrid = new ObservableCollection<AddUserModel> { new(null, null, null, null) };
            AddUsersNewRowCommand = new LambdaCommand(OnAddUsersNewRowCommandExecuted, CanAddUsersNewRowCommandExecute);
            AddUsersInBaseCommand = new LambdaCommand(OnAddUsersInBaseCommandExecuted, CanAddUsersInBaseCommandExecute);
        }

        private void AddNullRow()
        {
            for (int i = 0; i < AddUsersInGrid.Count;)
            {
                AddUsersInGrid.Add(new AddUserModel(null, null, null, null));
                break;
            }
        }
    }
}

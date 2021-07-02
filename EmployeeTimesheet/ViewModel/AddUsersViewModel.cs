using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using ApplicationContextData;
using BaseModelModule.Commands;
using BaseModelModule.ViewsModel.Base;
using EmployeeTimesheet.Model;

namespace EmployeeTimesheet.ViewModel
{
    class AddUsersViewModel : BaseViewModel
    {
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
            ApplicationContext addUsersInBase = new();
            try
            {
                foreach (var item in AddUsersInGrid)
                {

                    var convertServiceNumbers = Convert.ToInt32(item.ServiceNumbers);

                    Employee employee = new Employee()
                    {
                        Fio = $"{item.LastName} {item.FirstName} {item.PatronymicName}",
                        ServiceNumbers = convertServiceNumbers,
                        NameKbs = StaticDataModel.NameKbFromMain,
                    };
                    addUsersInBase.Employees.Add(employee);
                    addUsersInBase.SaveChanges();
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Введите целые числа.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            MessageBox.Show("Данные добавлены в базу", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public AddUsersViewModel()
        {
            AddUsersInGrid = new ObservableCollection<AddUserModel>();
            AddUsersInGrid.Add(new(null, null, null, null));
            AddUsersNewRowCommand = new LambdaCommand(OnAddUsersNewRowCommandExecuted, CanAddUsersNewRowCommandExecute);
            AddUsersInBaseCommand = new LambdaCommand(OnAddUsersInBaseCommandExecuted, CanAddUsersInBaseCommandExecute);
        }

        private void AddNullRow()
        {
            for (int i = 0; i < AddUsersInGrid.Count;)
            {
                AddUsersInGrid.Add(new(null, null, null, null));
                break;
            }
        }
    }
}

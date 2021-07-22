using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using ApplicationContextData;
using BaseModelModule.ViewsModel.Base;
using BaseModelModule.Commands;
using EmployeeTimesheet.Model;
using EmployeeTimesheet.Window;
using SelectedLib;

namespace EmployeeTimesheet.ViewModel
{
    class MainWindowViewModel : BaseViewModel
    {
        private readonly SelectedItems _selected = new(StaticDataModel.ApplicationContext);

        private ObservableCollection<NameKB> _nameKbOgkSource;
        public ObservableCollection<NameKB> NameKbOgkSource { get => _nameKbOgkSource; set => Set(ref _nameKbOgkSource, value); }

        private string _selectTextPassword;
        public string SelectTextPassword { get => _selectTextPassword; set => Set(ref _selectTextPassword, value); }

        private NameKB _selectedNameKb;
        public NameKB SelectedNameKb { get => _selectedNameKb; set => Set(ref _selectedNameKb, value); }

        public Action CloseAction { get; set; }

        public ICommand GetInWorkWindowCommand { get; }
        private bool CanGetInWorkWindowCommandExecute(object p) => true;
        private void OnGetInWorkWindowCommandExecuted(object p)
        {
            if (SelectedNameKb != null && SelectTextPassword != null)
            {
                StaticDataModel.NameKbFromMain = SelectedNameKb;
                _selected.Log = SelectedNameKb.NameKbOgk;
                _selected.Pas = SelectTextPassword;
                WorkWindow workWindow = new();
                if (_selected.PasswordKbs is true)
                {
                    workWindow.Show();
                    CloseAction();
                }
                else
                    ErrorPasOrLogMes();
            }
            else
                ErrorPasOrLogMes();
        }

        public MainWindowViewModel()
        {
            AddNameKb addStart = new();
            addStart.AddNameKbAndPass();
            GetInWorkWindowCommand =
                new LambdaCommand(OnGetInWorkWindowCommandExecuted, CanGetInWorkWindowCommandExecute);
            NameKbOgkSource = _selected.SelectedNameKbOgks;
        }

        private void ErrorPasOrLogMes()
        {
            MessageBox.Show("Неверно введен логин или пароль", "Ошибка", MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
    }
}

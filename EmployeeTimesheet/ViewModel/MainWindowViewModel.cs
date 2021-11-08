using System;
using System.Windows.Input;
using BaseModelModule.ViewsModel.Base;
using BaseModelModule.Commands;
using EmployeeTimesheet.Model;

namespace EmployeeTimesheet.ViewModel
{
    class MainWindowViewModel : BaseViewModel
    {
        
        private NameKbModel _nameKbOgkSource;
        public NameKbModel NameKbOgkSource { get => _nameKbOgkSource; set => Set(ref _nameKbOgkSource, value); }

        private string _selectTextPassword;
        public string SelectTextPassword { get => _selectTextPassword; set => Set(ref _selectTextPassword, value); }

        private NameKbModel _selectedNameKb;
        public NameKbModel SelectedNameKb { get => _selectedNameKb; set => Set(ref _selectedNameKb, value); }

        public Action CloseAction { get; set; }

        public ICommand GetInWorkWindowCommand { get; }
        private bool CanGetInWorkWindowCommandExecute(object p) => true;
        private void OnGetInWorkWindowCommandExecuted(object p)
        {
           
        }

        public MainWindowViewModel()
        {
            GetInWorkWindowCommand =
                new LambdaCommand(OnGetInWorkWindowCommandExecuted, CanGetInWorkWindowCommandExecute);
            NameKbOgkSource = new NameKbModel();
        }
    }
}

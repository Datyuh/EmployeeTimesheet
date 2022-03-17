using BaseModelModule.Commands;
using BaseModelModule.ViewsModel.Base;
using EmployeeTimesheet.Model;
using System;
using System.Windows.Input;

namespace EmployeeTimesheet.ViewModel
{
    public class AddNumOrderViewModel : BaseViewModel
    {
        public Action CloseAction { get; set; }

        private string _numOrder;
        public string NumOrder { get => _numOrder; set => Set(ref _numOrder, value); }

        private DateTime? _dateOrder;
        public DateTime? DateOrder { get => _dateOrder; set => Set(ref _dateOrder, value); }

        #region AllCommands
        public ICommand AddOrdersCommand { get; }
        private bool CanAddOrdersCommandExecute(object p) => true;
        private void OnAddOrdersCommandExecuted(object p)
        {
            StaticDataModel.NumOrders = $"№{NumOrder}";
            StaticDataModel.DateOrders = DateOrder;
            CloseAction();
        }
        #endregion

        public AddNumOrderViewModel()
        {
            AddOrdersCommand = new LambdaCommand(OnAddOrdersCommandExecuted, CanAddOrdersCommandExecute);
        }
    }
}

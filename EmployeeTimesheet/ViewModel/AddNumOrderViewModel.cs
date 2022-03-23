using BaseModelModule.Commands;
using BaseModelModule.ViewsModel.Base;
using EmployeeTimesheet.Model;
using System;
using System.Windows;
using System.Windows.Input;

namespace EmployeeTimesheet.ViewModel
{
    public class AddNumOrderViewModel : BaseViewModel
    {
        public Action CloseAction { get; set; }

        public string NumOrdersOut { get; private set; }
        public DateTime? DateOrdersOut { get; private set; }

        private string _numOrder;
        public string NumOrder { get => _numOrder; set => Set(ref _numOrder, value); }

        private DateTime _dateOrder;
        public DateTime DateOrder { get => _dateOrder; set => Set(ref _dateOrder, value); }

        private DateTime _dateOrders;       
        public DateTime DateOrders { get => _dateOrders; set => Set(ref _dateOrders, value); }

        #region AllCommands
        public ICommand AddOrdersCommand { get; }
        private bool CanAddOrdersCommandExecute(object p) => true;
        private void OnAddOrdersCommandExecuted(object p)
        {
            if (NumOrder == "")
            {
                MessageBox.Show("Введите номер и дату распоряжения",
                           "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                NumOrdersOut = $"№{NumOrder}";
                DateOrdersOut = DateOrder; 
                CloseAction();
            }
        }
        #endregion

        public AddNumOrderViewModel()
        {
            DateOrder = DateTime.Now;
            DateOrders = DateTime.Now;
            AddOrdersCommand = new LambdaCommand(OnAddOrdersCommandExecuted, CanAddOrdersCommandExecute);
        }
    }
}

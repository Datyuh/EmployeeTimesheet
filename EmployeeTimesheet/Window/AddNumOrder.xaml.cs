using EmployeeTimesheet.ViewModel;
using System;

namespace EmployeeTimesheet.Window
{
    /// <summary>
    /// Логика взаимодействия для AddNumOrder.xaml
    /// </summary>
    public partial class AddNumOrder : System.Windows.Window
    {
        private AddNumOrderViewModel addNumOrderViewModel;
        public AddNumOrder()
        {
            InitializeComponent();
            addNumOrderViewModel = new AddNumOrderViewModel();
            DataContext = addNumOrderViewModel;
            addNumOrderViewModel.CloseAction = Close;
        }

        public (string NumOrder, DateTime? DateOrder) OutOrders()
        {
            return (NumOrder: addNumOrderViewModel.NumOrdersOut, DateOrder: addNumOrderViewModel.DateOrdersOut);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}

using EmployeeTimesheet.ViewModel;
using System;

namespace EmployeeTimesheet.Window
{
    /// <summary>
    /// Логика взаимодействия для AddNumOrder.xaml
    /// </summary>
    public partial class AddNumOrder : System.Windows.Window
    {
        public DateTime? DateOrder { get; set; }
        public string NumOrder { get; set; }

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
    }
}

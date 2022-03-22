using EmployeeTimesheet.ViewModel;
using System;

namespace EmployeeTimesheet.Window
{
    /// <summary>
    /// Логика взаимодействия для AddNumOrder.xaml
    /// </summary>
    public partial class AddNumOrder : System.Windows.Window
    {
        public AddNumOrder()
        {
            InitializeComponent();
            AddNumOrderViewModel addNumOrderViewModel = new AddNumOrderViewModel();
            DataContext = addNumOrderViewModel;
            addNumOrderViewModel.CloseAction = Close;
        }
    }
}

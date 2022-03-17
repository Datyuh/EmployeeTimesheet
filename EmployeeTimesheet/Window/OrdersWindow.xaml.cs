using EmployeeTimesheet.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace EmployeeTimesheet.Window
{
    /// <summary>
    /// Логика взаимодействия для OrdersWindow.xaml
    /// </summary>
    public partial class OrdersWindow
    {
        private OrdersViewModel _ordersViewModel1;
        public OrdersWindow(ObservableCollection<WorkWindowModel> addDataEmployeeTimesheet, List<int> nameYearAdd)
        {
            InitializeComponent();
            OrdersViewModel ordersViewModel = new OrdersViewModel(addDataEmployeeTimesheet, nameYearAdd);
            DataContext = ordersViewModel;
            _ordersViewModel1 = ordersViewModel;
            ordersViewModel.CloseAction = Close;
        }

        private void ComboBox_DropDownClosed(object sender, System.EventArgs e)
        {
            _ordersViewModel1.AddInDataGrid();
        }
    }
}

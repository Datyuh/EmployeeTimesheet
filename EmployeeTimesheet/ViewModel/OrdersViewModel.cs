using BaseModelModule.Commands;
using BaseModelModule.ViewsModel.Base;
using SelectedLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace EmployeeTimesheet.Model
{
    public class OrdersViewModel : BaseViewModel
    {
        public Action CloseAction { get; set; }

        private List<int> _yearOrdersAdd;
        public List<int> YearOrdersAdd { get => _yearOrdersAdd; set => Set(ref _yearOrdersAdd, value); }

        private int _selectYearOrders;
        public int SelectYearOrders { get => _selectYearOrders; set => Set(ref _selectYearOrders, value); }

        private int _listNameEmployeesChoice;
        public int ListNameEmployeesChoice { get => _listNameEmployeesChoice; set => Set(ref _listNameEmployeesChoice, value); }

        private int _yearOrdersChoice;
        public int YearOrdersChoice { get => _yearOrdersChoice; set => Set(ref _yearOrdersChoice, value); }

        private ObservableCollection<WorkWindowModel> _listNameEmployees;
        public ObservableCollection<WorkWindowModel> ListNameEmployees { get => _listNameEmployees; set => Set(ref _listNameEmployees, value); }

        private WorkWindowModel _listNameEmployee;
        public WorkWindowModel ListNameEmployee { get => _listNameEmployee; set => Set(ref _listNameEmployee, value); }

        private ObservableCollection<OrdersModelDataGrid> _addDataOrders;
        public ObservableCollection<OrdersModelDataGrid> AddDataOrders { get => _addDataOrders; set => Set(ref _addDataOrders, value); }

        private string _sumAllOrders;
        public string SumAllOrders { get => _sumAllOrders; set => Set(ref _sumAllOrders, value); }

        #region Command
        public ICommand ShowStatusOrdersCommand { get; }
        private bool CanShowStatusOrdersCommandExecute(object p) => true;

        private void OnShowStatusOrdersCommandExecuted(object p)
        {
            AddInDataGrid();
        }

        public ICommand UpdateStatusOrdersCommand { get; }
        private bool CanUpdateStatusOrdersCommandExecute(object p) => true;

        private void OnUpdateStatusOrdersCommandExecuted(object p)
        {
            foreach (var item in AddDataOrders)
            {
                if (item.UpdateOrderStatusCheckBox is true)
                {
                    new SelectedForOrdersModel(StaticDataModel.ApplicationContext).ChangeStatusOrders(ListNameEmployee.Fio, item.NumOrder, item.DateWorkInWeekend);
                }
            }
            AddDataOrders.Clear();
            AddInDataGrid();
        }
        #endregion


        public OrdersViewModel(ObservableCollection<WorkWindowModel> addDataEmployeeTimesheet, List<int> nameYearAdd)
        {
            ListNameEmployees = addDataEmployeeTimesheet;
            YearOrdersAdd = nameYearAdd;
            YearOrdersChoice = YearOrdersAdd.Count - 1;
            ListNameEmployeesChoice = 0;

            ShowStatusOrdersCommand = new LambdaCommand(OnShowStatusOrdersCommandExecuted, CanShowStatusOrdersCommandExecute);
            UpdateStatusOrdersCommand = new LambdaCommand(OnUpdateStatusOrdersCommandExecuted, CanUpdateStatusOrdersCommandExecute);
            AddInDataGrid();
        }

        public void AddInDataGrid()
        {
            string fio;
            int year;
            AddDataOrders = new ObservableCollection<OrdersModelDataGrid>();
            var selectedOrders = new SelectedForOrdersModel(StaticDataModel.ApplicationContext);
            switch (ListNameEmployee)
            {
                case not null:
                    fio = ListNameEmployee.Fio;
                    year = SelectYearOrders;
                    break;
                case null:
                    fio = ListNameEmployees.Select(p => p.Fio).FirstOrDefault().ToString();
                    year = YearOrdersAdd.FirstOrDefault();
                    break;
            }

            foreach (var item in selectedOrders.SelectedAllOrders(fio, year))
            {
                OrdersModelDataGrid ordersModelDataGrid = new()
                {
                    NumOrder = item.NumOrder,
                    StatusOrder = item.StatusOrder,
                    DateOrder = item.DateOrder,
                    DateWorkInWeekend = item.DateTimeAddData,
                    EnabledOrderStatusCheckBox = item.StatusOrder.Contains("Не отгулял(а)"),
                };
                AddDataOrders.Add(ordersModelDataGrid);
            }

            SumAllOrders = $"Общие кол-во не отгуленных: {AddDataOrders.Select(p => p.StatusOrder).Count(p => p == "Не отгулял(а)")}";
        }

    }
}

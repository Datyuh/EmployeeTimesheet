using System.Collections.ObjectModel;
using System.Linq;
using ApplicationContextData;

namespace SelectedLib
{
    public class SelectedForExcel
    {
        private readonly ApplicationContext _dbContext;

        public SelectedForExcel(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ObservableCollection<Employee> SelectedEmployee()
        {
            var selectedDateEmplTime = _dbContext.Employees.Select(x => x);
            return new ObservableCollection<Employee>(selectedDateEmplTime);
        }

        public ObservableCollection<EmployeeTimesheet> SelectedDateEmplTime()
        {
            var selectedDateEmplTime = _dbContext.EmployeeTimesheets.Select(x => x);
            return new ObservableCollection<EmployeeTimesheet>(selectedDateEmplTime);
        }
    }
}

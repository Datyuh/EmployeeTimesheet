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

        public ObservableCollection<EmployeeTimesheet> SelectedDateEmplTime()
        {
            var selectedDateEmplTime = _dbContext.EmployeeTimesheets.Select(x => x).OrderBy(x => x.EmployeesId);
            return new ObservableCollection<EmployeeTimesheet>(selectedDateEmplTime);
        }
    }
}

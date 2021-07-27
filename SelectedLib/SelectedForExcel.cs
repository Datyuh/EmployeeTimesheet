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

        public ObservableCollection<EmployeeTimesheet> SelectedDateEmplTime(NameKB nameKb)
        {
            var selectedDateEmplTime = _dbContext.EmployeeTimesheets.Select(x => x).Where(x => x.Employees.NameKbs.NameKbOgk == nameKb.NameKbOgk);
            return new ObservableCollection<EmployeeTimesheet>(selectedDateEmplTime);
        }
    }
}

using System.Collections.ObjectModel;
using System.Linq;

namespace ApplicationContextData
{
    public class SelectedItems
    {
        private readonly ApplicationContext _dbContext = new();

        public string Pas { get; set; }
        public string Log { get; set; }
        public object NameKbOgkId { get; set; }

        public ObservableCollection<object> SelectedNameKbOgks => SelectedNameKbOgk();
        public bool PasswordKbs => PasswordKb(Pas, Log);
        public ObservableCollection<object> SelectedEmployees => SelectedEmployee(NameKbOgkId);
        public ObservableCollection<Employee> SelectedEmpoyeeTimesheets => SelectedEmpoyeeTimesheet();


        private ObservableCollection<object> SelectedNameKbOgk()
        {
            var selectedNameKb = new ObservableCollection<object>(_dbContext.NamesKb.Select(p => p).Where(p => p != null));
            return selectedNameKb;
        }

        private bool PasswordKb(string pas, string log)
        {
            var checkLoginAndPass = _dbContext.NamesKb.Any(p => p.NameKbOgk == log && p.PasswordsKb == pas);
            return checkLoginAndPass;
        }

        private ObservableCollection<object> SelectedEmployee(object nameKbOgkId)
        {
            var selectedEmployees =
                new ObservableCollection<object>(_dbContext.Employees.Select(p => p).Where(p => p == nameKbOgkId));
            return selectedEmployees;
        }

        private ObservableCollection<Employee> SelectedEmpoyeeTimesheet()
        {
            var selectedEmpoyeeTimesheet = new ObservableCollection<Employee>(_dbContext.Employees.Select(p => p));
            return selectedEmpoyeeTimesheet;
        }
    }
}
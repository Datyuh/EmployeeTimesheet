using System.Collections.ObjectModel;

namespace SelectedLib
{
    public class SelectedItems
    {

        public string Pas { get; set; }
        public string Log { get; set; }
        public object NameKbOgkId { get; set; }
        public ApplicationContext DbContext { get; set; }

        public ObservableCollection<NameKB> SelectedNameKbOgks => SelectedNameKbOgk(DbContext);
        public bool PasswordKbs => PasswordKb(DbContext,Pas, Log);
        public ObservableCollection<object> SelectedEmployees => SelectedEmployee(DbContext, NameKbOgkId);
        public ObservableCollection<Employee> SelectedEmpoyeeTimesheets => SelectedEmpoyeeTimesheet(DbContext);

        public SelectedItems(ApplicationContext dbContext)
        {
            DbContext = dbContext;
        }
        private ObservableCollection<NameKB> SelectedNameKbOgk(ApplicationContext dbContext)
        {
            var selectedNameKb = new ObservableCollection<NameKB>(dbContext.NamesKb.Select(p => p).Where(p => p != null));
            return selectedNameKb;
        }

        private bool PasswordKb(ApplicationContext dbContext, string pas, string log)
        {
            var checkLoginAndPass = dbContext.NamesKb.Any(p => p.NameKbOgk == log && p.PasswordsKb == pas);
            return checkLoginAndPass;
        }

        private ObservableCollection<object> SelectedEmployee(ApplicationContext dbContext, object nameKbOgkId)
        {
            var selectedEmployees =
                new ObservableCollection<object>(dbContext.Employees.Select(p => p).Where(p => p == nameKbOgkId));
            return selectedEmployees;
        }

        private ObservableCollection<Employee> SelectedEmpoyeeTimesheet(ApplicationContext dbContext)
        {
            var selectedEmpoyeeTimesheet = new ObservableCollection<Employee>(dbContext.Employees.Select(p => p));
            return selectedEmpoyeeTimesheet;
        }
    }
}
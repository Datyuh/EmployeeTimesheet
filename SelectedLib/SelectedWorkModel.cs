using System.Collections.ObjectModel;
using System.Linq;
using ApplicationContextData;

namespace SelectedLib
{
    public class SelectedWorkModel
    {
        private ApplicationContext _db = new();


        public string SelectedFio()
        {
            foreach (var selectedFio in _db.Employees.Select(p => p.Fio).Where(p => p != null))
            {
                return selectedFio;
            }

            return null;
        }

        public int SelectedServiceNumbers(string fio)
        {
            foreach (var selectedServiceNumbers in _db.Employees.Where(p => p.Fio == fio).Select(p => p.ServiceNumbers))
            {
                return selectedServiceNumbers;
            }
            return 0;
        }
    }
}

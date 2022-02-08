using System.Collections.ObjectModel;
using System.Linq;

namespace EmployeeTimesheet.Model
{
    class WorkingExcelShortReportModel
    {
        private int _columsExcel = 3;

        private readonly Microsoft.Office.Interop.Excel.Application _objExcel;
        private readonly Microsoft.Office.Interop.Excel.Workbook _objWorkBook;
        private readonly Microsoft.Office.Interop.Excel.Worksheet _objWorkSheet;
        private readonly ObservableCollection<WorkWindowModel> _workWindowModel;
        private readonly int _nameMonthChoice;

        public WorkingExcelShortReportModel(ObservableCollection<WorkWindowModel> workWindowModel, int nameMonthChoice)
        {
            _workWindowModel = workWindowModel;
            _objExcel = new Microsoft.Office.Interop.Excel.Application();
            _nameMonthChoice = nameMonthChoice;
            
            //Книга.
            _objWorkBook = _objExcel.Workbooks.Add(System.Reflection.Missing.Value);
            _objExcel.Visible = true;
            //Таблица.
            _objWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)_objWorkBook.Sheets[1];

            AddColumnName();
            AddRowDate();
        }

        private void AddColumnName()
        {
            var excelColumnFios = (Microsoft.Office.Interop.Excel.Range)_objWorkSheet.Cells[2, 2];
            excelColumnFios.Value2 = "Ф. И. О.";
            var excelColumnServNumbrs = (Microsoft.Office.Interop.Excel.Range)_objWorkSheet.Cells[2, 3];
            excelColumnServNumbrs.Value2 = "Таб. №";
            var excelTotal = (Microsoft.Office.Interop.Excel.Range)_objWorkSheet.Cells[2, 4];
            excelTotal.Value2 = "Итого отраб. дней";
            var excelTotalRemote = (Microsoft.Office.Interop.Excel.Range)_objWorkSheet.Cells[2, 5];
            excelTotalRemote.Value2 = "Итого удал. раб.";
            var excelTotalWeekends = (Microsoft.Office.Interop.Excel.Range)_objWorkSheet.Cells[2, 6];
            excelTotalWeekends.Value2 = "Итого раб. в вых. дни";
        }

        private void AddRowDate()
        {
            foreach (var windowModel in _workWindowModel.Where(e => e.Employees.StatusUsers == "Работает").Select(e => e))
            {
                var excelColumnFio = (Microsoft.Office.Interop.Excel.Range)_objWorkSheet.Cells[_columsExcel, 2];
                excelColumnFio.Value2 = windowModel.Employees.Fio;

                var excelServNomb = (Microsoft.Office.Interop.Excel.Range)_objWorkSheet.Cells[_columsExcel, 3];
                excelServNomb.Value2 = windowModel.Employees.ServiceNumbers;


                var excelTotalStatus = (Microsoft.Office.Interop.Excel.Range)_objWorkSheet.Cells[_columsExcel, 4];
                excelTotalStatus.Value2 = windowModel.SumDayWork;

                var excelTotalRemoteStatus = (Microsoft.Office.Interop.Excel.Range)_objWorkSheet.Cells[_columsExcel, 5];
                excelTotalRemoteStatus.Value2 = windowModel.SumDayRemoteWork;

                var excelTotalWeekendsStatus = (Microsoft.Office.Interop.Excel.Range)_objWorkSheet.Cells[_columsExcel, 6];
                excelTotalWeekendsStatus.Value2 = windowModel.SumDayWorkWeekends;

                _columsExcel++;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Excel = Microsoft.Office.Interop.Excel;

namespace EmployeeTimesheet.Model
{
    class WorkingWithExcelModel
    {
        private int _rowExcel = 4;
        private int _columnExcelServNum = 3;
        private int _columsExcel = 2;
        private int _total;
        private IEnumerable<DateTime> _sevenDaysAWeek;

        private readonly List<DateTime> _dateOfMonths = new();
        private readonly Excel.Application _objExcel;
        private readonly Excel.Workbook _objWorkBook;
        private readonly Excel.Worksheet _objWorkSheet;
        private readonly ObservableCollection<ApplicationContextData.EmployeeTimesheet> _allEmployeeTimesheets;

        public WorkingWithExcelModel(ObservableCollection<ApplicationContextData.EmployeeTimesheet> allEmployeeTimesheets)
        {
            _allEmployeeTimesheets = allEmployeeTimesheets;
            _objExcel = new Excel.Application();

            //Книга.
            _objWorkBook = _objExcel.Workbooks.Add(System.Reflection.Missing.Value);
            _objExcel.Visible = true;
            //Таблица.
            _objWorkSheet = (Excel.Worksheet)_objWorkBook.Sheets[1];

            DateOfMonthNow();
            AddColumnName();
            AddRowDate();
        }

        private void DateOfMonthNow()
        {
            var year = DateTime.Now.Year;
            var month = DateTime.Now.Month;
            var startDay = new DateTime(year, month, 1);
            var endDay = startDay.AddMonths(1);

            for (var date = startDay; date != endDay; date = date.AddDays(1))
            {
                _dateOfMonths.Add(date.Date);
            }

            _sevenDaysAWeek = _dateOfMonths
                .Where(e => e.DayOfWeek != DayOfWeek.Saturday && e.DayOfWeek != DayOfWeek.Sunday)
                .Select(e => e.Date);

            _total = _sevenDaysAWeek.Count() + _rowExcel;
        }

        private void AddColumnName()
        {
            var excelColumnFios = (Excel.Range)_objWorkSheet.Cells[2, 2];
            excelColumnFios.Value2 = "Ф. И. О.";
            var excelColumnServNumbrs = (Excel.Range)_objWorkSheet.Cells[2, 3];
            excelColumnServNumbrs.Value2 = "Таб. №";
            var excelTotal = (Excel.Range)_objWorkSheet.Cells[2, _total];
            excelTotal.Value2 = "Итого отработанных дней";
        }

        private void AddRowDate()
        {
            foreach (var allEmployeeTimesheet in _allEmployeeTimesheets.Where(e => e.Employees.StatusUsers == "Работает").Select(e => e))
            {
                _rowExcel = 4;

                var x = _objWorkSheet.Cells[_columsExcel, 2].Text;
                if (x != allEmployeeTimesheet.Employees.Fio)
                {
                    var excelColumnFio = (Excel.Range)_objWorkSheet.Cells[_columnExcelServNum, 2];
                    excelColumnFio.Value2 = allEmployeeTimesheet.Employees.Fio;

                    var excelServNomb = (Excel.Range)_objWorkSheet.Cells[_columnExcelServNum, 3];
                    excelServNomb.Value2 = allEmployeeTimesheet.Employees.ServiceNumbers;

                    var excelTotalStatus = (Excel.Range)_objWorkSheet.Cells[_columnExcelServNum, _total];
                    excelTotalStatus.Value2 = _allEmployeeTimesheets
                        .Select(e => e)
                        .Count(e => e.Status == "Работал" && e.Employees.Fio == allEmployeeTimesheet.Employees.Fio);
                    _columnExcelServNum++;
                    _columsExcel++;
                }

                foreach (var dateOfMonth in _sevenDaysAWeek)
                {
                    if (x != allEmployeeTimesheet.Employees.Fio)
                    {
                        var excelDate = (Excel.Range)_objWorkSheet.Cells[2, _rowExcel];
                        excelDate.NumberFormatLocal = "ДД.ММ.ГГГГ";
                        excelDate.Value2 = dateOfMonth.Date;
                    }

                    if (dateOfMonth.Date == allEmployeeTimesheet.DateTimeAddData)
                    {
                        var excelStatusEmpl = (Excel.Range)_objWorkSheet.Cells[_columsExcel, _rowExcel];
                        excelStatusEmpl.Value2 = allEmployeeTimesheet.Status switch
                        {
                            "Работал" => "+",
                            "ОБС" => "Н",
                            "Больничный" => "Б",
                            "Отпуск" => "О",
                            _ => excelStatusEmpl.Value2
                        };
                    }
                    _rowExcel++;
                }
            }
        }
    }
}

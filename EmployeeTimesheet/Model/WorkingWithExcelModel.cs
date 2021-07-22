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
        private int _columsExcel = 3;
        private int _total;
        private readonly List<DateTime> _dateOfMonths = new();
        private Excel.Application ObjExcel;
        private Excel.Workbook ObjWorkBook;
        private Excel.Worksheet ObjWorkSheet;
        private readonly ObservableCollection<ApplicationContextData.EmployeeTimesheet> _allEmployeeTimesheets;

        public WorkingWithExcelModel(ObservableCollection<ApplicationContextData.EmployeeTimesheet> allEmployeeTimesheets)
        {
            _allEmployeeTimesheets = allEmployeeTimesheets;
            _total = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) + _rowExcel + 1;
            ObjExcel = new Excel.Application();
            //Книга.
            ObjWorkBook = ObjExcel.Workbooks.Add(System.Reflection.Missing.Value);
            ObjExcel.Visible = true;
            //Таблица.
            ObjWorkSheet = (Excel.Worksheet)ObjWorkBook.Sheets[1];

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
        }

        private void AddColumnName()
        {
            var excelColumnFios = (Excel.Range)ObjWorkSheet.Cells[2, 2];
            excelColumnFios.Value2 = "Ф. И. О.";
            var excelColumnServNumbrs = (Excel.Range)ObjWorkSheet.Cells[2, 3];
            excelColumnServNumbrs.Value2 = "Таб. №";
            var excelTotal = (Excel.Range)ObjWorkSheet.Cells[2, _total];
            excelTotal.Value2 = "Итого отработанных дней";
        }

        private void AddRowDate()
        {
            foreach (var allEmployeeTimesheet in _allEmployeeTimesheets)
            {
                _rowExcel = 4;
                var excelColumnFio = (Excel.Range)ObjWorkSheet.Cells[_columsExcel, 2];
                excelColumnFio.Value2 = allEmployeeTimesheet.Employees.Fio;

                var excelServNomb = (Excel.Range)ObjWorkSheet.Cells[_columsExcel, 3];
                excelServNomb.Value2 = allEmployeeTimesheet.Employees.ServiceNumbers;

                var excelTotalStatus = (Excel.Range)ObjWorkSheet.Cells[_columsExcel, _total];
                excelTotalStatus.Value2 = _allEmployeeTimesheets
                    .Select(x => x)
                    .Count(x => x.Status == "Работал" && x.Employees.Fio == allEmployeeTimesheet.Employees.Fio);

                foreach (var dateOfMonth in _dateOfMonths)
                {
                    var excelDate = (Excel.Range)ObjWorkSheet.Cells[2, _rowExcel];
                    excelDate.NumberFormatLocal = "ДД.ММ.ГГГГ";
                    excelDate.Value2 = dateOfMonth.Date;

                    if (dateOfMonth.Date == allEmployeeTimesheet.DateTimeAddData)
                    {
                        var excelStatusEmpl = (Excel.Range)ObjWorkSheet.Cells[_columsExcel, _rowExcel];
                        excelStatusEmpl.Value2 = allEmployeeTimesheet.Status switch
                        {
                            "Работал" => "+",
                            "ОБС" => "Н",
                            "Больничный" => "Б",
                            _ => excelStatusEmpl.Value2
                        };
                    }
                    _rowExcel++;
                }
                _columsExcel++;
            }
        }
    }
}

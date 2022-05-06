using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly int _nameMonthChoice;
        private readonly int _nameYearSelect;
        private readonly List<DateTime> _daysHolidayWeekend;

        public WorkingWithExcelModel(ObservableCollection<ApplicationContextData.EmployeeTimesheet> allEmployeeTimesheets, int nameMonthChoice, int nameYearSelect)
        {
            _allEmployeeTimesheets = allEmployeeTimesheets;
            _objExcel = new Excel.Application();
            _nameMonthChoice = nameMonthChoice;
            _nameYearSelect = nameYearSelect;
            var daysHolidayWeekend = Task.Run(() => new ConsultantParsing().ConsultantParsings(nameYearSelect));
            _daysHolidayWeekend = daysHolidayWeekend.Result;

            //Книга
            _objWorkBook = _objExcel.Workbooks.Add(System.Reflection.Missing.Value);
            _objExcel.Visible = false;
            //Таблица.
            _objWorkSheet = (Excel.Worksheet)_objWorkBook.Sheets[1];

            DateOfMonthNow();
            AddColumnName();
            AddRowDate();
        }

        private void DateOfMonthNow()
        {
            var year = _nameYearSelect;
            var month = _nameMonthChoice;
            var startDay = new DateTime(year, month, 1);
            var endDay = startDay.AddMonths(1);

            for (var date = startDay; date != endDay; date = date.AddDays(1))
            {
                _dateOfMonths.Add(date.Date);
            }

            _sevenDaysAWeek = _dateOfMonths
                .Where(e => e.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
                .Select(e => e.Date);

            _total = _dateOfMonths.Count() + _rowExcel;
        }

        private void AddColumnName()
        {
            var excelColumnFios = _objWorkSheet.Cells[2, 2] as Excel.Range;
            excelColumnFios.Value2 = "Ф. И. О.";
            var excelColumnServNumbrs = _objWorkSheet.Cells[2, 3] as Excel.Range;
            excelColumnServNumbrs.Value2 = "Таб. №";
            var excelTotal = _objWorkSheet.Cells[2, _total] as Excel.Range;
            excelTotal.Value2 = "Итого отраб. дней";
            var excelTotalRemote = _objWorkSheet.Cells[2, _total + 1] as Excel.Range;
            excelTotalRemote.Value2 = "Итого удал. раб.";
            var excelTotalWeekends = _objWorkSheet.Cells[2, _total + 2] as Excel.Range;
            excelTotalWeekends.Value2 = "Итого раб. в вых. дни";
        }

        private void AddRowDate()
        {
            var a = _allEmployeeTimesheets
                .Where(e => e.Employees.StatusUsers == "Работает" && e.Status != null)
                .Select(e => e);
            foreach (var allEmployeeTimesheet in a)
            {
                _rowExcel = 4;

                var x = _objWorkSheet.Cells[_columsExcel, 2].Text;
                if (x != allEmployeeTimesheet.Employees.Fio)
                {
                    var sumDayWork = _allEmployeeTimesheets
                        .Count(g => g.Status == "Явка" && g.Employees.Fio == allEmployeeTimesheet.Employees.Fio 
                        && g.DateTimeAddData.Month == _nameMonthChoice);
                    var sumHalfDayWork = _allEmployeeTimesheets
                        .Count(g => g.Status == "Пол. дня ОБС" && g.Employees.Fio == allEmployeeTimesheet.Employees.Fio 
                        && g.DateTimeAddData.Month == _nameMonthChoice) * 0.5;
                    var sumBusinessTrip = _allEmployeeTimesheets
                        .Count(g => g.Status == "Командировка" && g.Employees.Fio == allEmployeeTimesheet.Employees.Fio
                        && g.DateTimeAddData.Month == _nameMonthChoice);
                    var excelTotalRemoteStatus = _objWorkSheet.Cells[_columnExcelServNum, _total + 1] as Excel.Range;
                    excelTotalRemoteStatus.Value2 = _allEmployeeTimesheets
                        .Count(e => e.Status == "Удаленная работа" && e.Employees.Fio == allEmployeeTimesheet.Employees.Fio
                                                                   && e.Status != null && e.DateTimeAddData.Month == _nameMonthChoice);
                    var excelTotalWeekendsStatus =  _objWorkSheet.Cells[_columnExcelServNum, _total + 2] as Excel.Range;
                    excelTotalWeekendsStatus.Value2 = _allEmployeeTimesheets
                        .Count(e => e.Status == "Работа в праз. и вых." && e.Employees.Fio == allEmployeeTimesheet.Employees.Fio
                                                                        && e.Status != null && e.DateTimeAddData.Month == _nameMonthChoice);

                    var sumDayWorks = sumDayWork + sumHalfDayWork + sumBusinessTrip;

                    var excelColumnFio =  _objWorkSheet.Cells[_columnExcelServNum, 2] as Excel.Range;
                    excelColumnFio.Value2 = allEmployeeTimesheet.Employees.Fio;

                    var excelServNomb =  _objWorkSheet.Cells[_columnExcelServNum, 3] as Excel.Range;
                    excelServNomb.Value2 = allEmployeeTimesheet.Employees.ServiceNumbers;

                    var excelTotalStatus =  _objWorkSheet.Cells[_columnExcelServNum, _total] as Excel.Range;
                    excelTotalStatus.Value2 = sumDayWorks;

                    _columnExcelServNum++;
                    _columsExcel++;
                }

                foreach (var dateOfMonth in _dateOfMonths)
                {
                    if (x != allEmployeeTimesheet.Employees.Fio)
                    {
                        var excelDate = _objWorkSheet.Cells[2, _rowExcel] as Excel.Range;
                        excelDate.NumberFormatLocal = "ДД.ММ.ГГГГ";
                        excelDate.Value2 = dateOfMonth.Date;
                        if (dateOfMonth.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
                        {
                            var excelStatusEmpl =  _objWorkSheet.Cells[_columsExcel, _rowExcel] as Excel.Range;
                            excelStatusEmpl.Value2 = "В";
                        }
                        foreach (var daysHolidayWeekend in _daysHolidayWeekend)
                        {
                            if (dateOfMonth == daysHolidayWeekend)
                            {
                                var excelStatusEmpl = _objWorkSheet.Cells[_columsExcel, _rowExcel] as Excel.Range;
                                excelStatusEmpl.Value2 = "В";
                            }
                        }                       
                    }

                    /*
                     Должно приходить с базы:
                        "Явка, ОБС, Пол. дня ОБС, Больничный,
                        Отпуск осн., Командировка,
                        Работа в праз. и вых., Праздн. и вых. дни"
                    для изменения в switch. 
                    Эти данные добовляются в combobox в WorkWindowViewModel в методе AddEployeeTimessheet
                    */
                    if (dateOfMonth.Date == allEmployeeTimesheet.DateTimeAddData)
                    {
                        var excelStatusEmpl =  _objWorkSheet.Cells[_columsExcel, _rowExcel] as Excel.Range;
                        excelStatusEmpl.Value2 = allEmployeeTimesheet.Status switch
                        {
                            "Явка" => "Я",
                            "Удаленная работа" => "Я",
                            "ОБС" => "ДО",
                            "Больничный" => "Б",
                            "Отпуск осн." => "ОТ",
                            "Командировка" => "К",
                            "Работа в праз. и вых." => "РВ",
                            "Праздн. и вых. дни" => "В",
                            "Пол. дня ОБС" => "Пол. ДО",
                            _ => excelStatusEmpl.Value2
                        };
                    }
                    _rowExcel++;
                }
            }

            Excel.Range excelCells1 =
                _objWorkSheet.Range[$"B{_columnExcelServNum + 1}", $"O{_columnExcelServNum + 2}"].Cells;
            excelCells1.Merge(Type.Missing);
            excelCells1.Interior.Color = Excel.XlRgbColor.rgbPaleVioletRed;
            excelCells1.Value2 =
                "Я => Явка, Удаленная работа; ДО => ОБС;  Б =>   Больничный;  ОТ => Отпуск осн.;    К => Командировка\r\n" +
                "   РВ => Работа в праз. и вых.;    В => Праздн. и вых. дни;    Пол. ДО => Пол. дня ОБС";
            _objExcel.Visible = true;           
        }
        
    }
}

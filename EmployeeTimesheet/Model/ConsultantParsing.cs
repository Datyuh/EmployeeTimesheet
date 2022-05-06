using AngleSharp;
using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeTimesheet.Model
{
    public class ConsultantParsing
    {
        //public List<DateTime> DayOfWeekend { get; set; }
        public ConsultantParsing()
        {
           // _ = ConsultantParsings(year);
        }

        public async Task<List<DateTime>> ConsultantParsings(int year)
        {
            var dayOfWeekend = new List<DateTime>();

            var config = Configuration.Default.WithDefaultLoader();
            using var context = BrowsingContext.New(config);

            var url = $"http://www.consultant.ru/law/ref/calendar/proizvodstvennye/{year}";

            using var doc = await context.OpenAsync(url);

            //var title = doc.Title;

            //Console.WriteLine(title);
            
            var weekend = doc.QuerySelectorAll("table").Where(p => p.ClassName != null && p.ClassName.Contains("cal"));

            foreach (var par in weekend)
            {
                var monthWeekends = par.QuerySelectorAll("th").Where(p => p.ClassName != null && p.ClassName.Contains("month"));
                foreach (var month in monthWeekends)
                {
                    var mm = NameMonthRetInt.NameMonth(month.Text().Trim());
                    var dayWeekends = par.QuerySelectorAll("td").Where(p => p.ClassName != null && p.ClassName.Contains("holiday weekend"));
                    foreach (var dayWeekend in dayWeekends)
                    {
                        var dd = Convert.ToInt32(dayWeekend.Text().Trim());
                        dayOfWeekend.Add(new DateTime(year, mm, dd));
                    }                    
                }
            }
            return dayOfWeekend;
        }
    }
}

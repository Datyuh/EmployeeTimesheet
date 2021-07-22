using ApplicationContextData;

namespace EmployeeTimesheet.Model
{
    static class StaticDataModel
    {
        public static NameKB NameKbFromMain { get; set; }
        public static ApplicationContext ApplicationContext { get; set; } = new();
    }
}

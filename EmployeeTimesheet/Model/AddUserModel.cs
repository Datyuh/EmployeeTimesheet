namespace EmployeeTimesheet.Model
{
    class AddUserModel
    {
        public AddUserModel(string LastName, string FirstName, string PatronymicName, string ServiceNumbers)
        {
            this.LastName = LastName;
            this.FirstName = FirstName;
            this.PatronymicName = PatronymicName;
            this.ServiceNumbers = ServiceNumbers;
        }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string PatronymicName { get; set; }
        public string ServiceNumbers { get; set; }
    }
}

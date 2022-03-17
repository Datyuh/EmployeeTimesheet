namespace ApplicationContextData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateOrderStToDT : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EmployeeTimesheets", "DateOrder", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EmployeeTimesheets", "DateOrder", c => c.String());
        }
    }
}

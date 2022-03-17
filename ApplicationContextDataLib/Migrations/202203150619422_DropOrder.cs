namespace ApplicationContextData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropOrder : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.EmployeeTimesheets", "NumOrder");
            DropColumn("dbo.EmployeeTimesheets", "DateOrder");
            DropColumn("dbo.EmployeeTimesheets", "StatusOrder");
        }

        public override void Down()
        {
            AddColumn("dbo.EmployeeTimesheets", "NumOrder", c => c.String(maxLength: 20));
            AddColumn("dbo.EmployeeTimesheets", "DateOrder", c => c.String());
            AddColumn("dbo.EmployeeTimesheets", "StatusOrder", c => c.String());
        }
    }
}

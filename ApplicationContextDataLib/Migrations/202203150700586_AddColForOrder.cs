namespace ApplicationContextData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColForOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmployeeTimesheets", "NumOrder", c => c.String(maxLength: 20));
            AddColumn("dbo.EmployeeTimesheets", "DateOrder", c => c.String());
            AddColumn("dbo.EmployeeTimesheets", "StatusOrder", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EmployeeTimesheets", "StatusOrder");
            DropColumn("dbo.EmployeeTimesheets", "DateOrder");
            DropColumn("dbo.EmployeeTimesheets", "NumOrder");
        }
    }
}

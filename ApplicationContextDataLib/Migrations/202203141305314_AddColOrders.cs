namespace ApplicationContextData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColOrders : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmployeeTimesheets", "NumOrder", c => c.String(maxLength: 20));
            AddColumn("dbo.EmployeeTimesheets", "DateOrder", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.EmployeeTimesheets", "NumOrder");
            DropColumn("dbo.EmployeeTimesheets", "DateOrder");
        }
    }    
}

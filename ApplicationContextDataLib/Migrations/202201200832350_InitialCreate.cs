namespace ApplicationContextData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fio = c.String(nullable: false),
                        StatusUsers = c.String(nullable: false),
                        ServiceNumbers = c.Int(nullable: false),
                        NameKbId = c.Int(nullable: false),
                        RegistrDateInBase = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NameKBs", t => t.NameKbId, cascadeDelete: true)
                .Index(t => t.NameKbId);
            
            CreateTable(
                "dbo.EmployeeTimesheets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.String(nullable: false),
                        DateTimeAddData = c.DateTime(nullable: false),
                        EmployeesId = c.Int(nullable: false),
                        RegistrDateInBase = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeesId, cascadeDelete: true)
                .Index(t => t.EmployeesId);
            
            CreateTable(
                "dbo.NameKBs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameKbOgk = c.String(nullable: false),
                        PasswordsKb = c.String(nullable: false),
                        RegistrDateInBase = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "NameKbId", "dbo.NameKBs");
            DropForeignKey("dbo.EmployeeTimesheets", "EmployeesId", "dbo.Employees");
            DropIndex("dbo.EmployeeTimesheets", new[] { "EmployeesId" });
            DropIndex("dbo.Employees", new[] { "NameKbId" });
            DropTable("dbo.NameKBs");
            DropTable("dbo.EmployeeTimesheets");
            DropTable("dbo.Employees");
        }
    }
}

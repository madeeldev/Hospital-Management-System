namespace HospitalManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDepartment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Departments", "Description", c => c.String(nullable: false));
            AddColumn("dbo.Departments", "Status", c => c.String(nullable: false));
            AlterColumn("dbo.Departments", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Departments", "Name", c => c.String());
            DropColumn("dbo.Departments", "Status");
            DropColumn("dbo.Departments", "Description");
        }
    }
}

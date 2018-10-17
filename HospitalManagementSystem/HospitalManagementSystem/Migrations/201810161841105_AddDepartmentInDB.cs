namespace HospitalManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDepartmentInDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Doctors", "DepartmentsId", c => c.Int(nullable: false));
            CreateIndex("dbo.Doctors", "DepartmentsId");
            AddForeignKey("dbo.Doctors", "DepartmentsId", "dbo.Departments", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Doctors", "DepartmentsId", "dbo.Departments");
            DropIndex("dbo.Doctors", new[] { "DepartmentsId" });
            DropColumn("dbo.Doctors", "DepartmentsId");
            DropTable("dbo.Departments");
        }
    }
}

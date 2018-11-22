namespace HospitalManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAssinDocAndPatInDB2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AssignedDoctorsAndPatients", "DoctorsId", c => c.Int(nullable: false));
            CreateIndex("dbo.AssignedDoctorsAndPatients", "DoctorsId");
            AddForeignKey("dbo.AssignedDoctorsAndPatients", "DoctorsId", "dbo.Doctors", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AssignedDoctorsAndPatients", "DoctorsId", "dbo.Doctors");
            DropIndex("dbo.AssignedDoctorsAndPatients", new[] { "DoctorsId" });
            DropColumn("dbo.AssignedDoctorsAndPatients", "DoctorsId");
        }
    }
}

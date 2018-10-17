namespace HospitalManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAssinDocAndPatInDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssignedDoctorsAndPatients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PatientsId = c.Byte(nullable: false),
                        Patients_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Patients", t => t.Patients_Id)
                .Index(t => t.Patients_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AssignedDoctorsAndPatients", "Patients_Id", "dbo.Patients");
            DropIndex("dbo.AssignedDoctorsAndPatients", new[] { "Patients_Id" });
            DropTable("dbo.AssignedDoctorsAndPatients");
        }
    }
}

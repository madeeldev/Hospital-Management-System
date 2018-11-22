namespace HospitalManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPrescriptionToDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Prescriptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Prescription = c.String(),
                        PatientsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Patients", t => t.PatientsId, cascadeDelete: true)
                .Index(t => t.PatientsId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Prescriptions", "PatientsId", "dbo.Patients");
            DropIndex("dbo.Prescriptions", new[] { "PatientsId" });
            DropTable("dbo.Prescriptions");
        }
    }
}

namespace HospitalManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAssinDocAndPatInDB1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AssignedDoctorsAndPatients", "Patients_Id", "dbo.Patients");
            DropIndex("dbo.AssignedDoctorsAndPatients", new[] { "Patients_Id" });
            DropColumn("dbo.AssignedDoctorsAndPatients", "PatientsId");
            RenameColumn(table: "dbo.AssignedDoctorsAndPatients", name: "Patients_Id", newName: "PatientsId");
            AlterColumn("dbo.AssignedDoctorsAndPatients", "PatientsId", c => c.Int(nullable: false));
            AlterColumn("dbo.AssignedDoctorsAndPatients", "PatientsId", c => c.Int(nullable: false));
            CreateIndex("dbo.AssignedDoctorsAndPatients", "PatientsId");
            AddForeignKey("dbo.AssignedDoctorsAndPatients", "PatientsId", "dbo.Patients", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AssignedDoctorsAndPatients", "PatientsId", "dbo.Patients");
            DropIndex("dbo.AssignedDoctorsAndPatients", new[] { "PatientsId" });
            AlterColumn("dbo.AssignedDoctorsAndPatients", "PatientsId", c => c.Int());
            AlterColumn("dbo.AssignedDoctorsAndPatients", "PatientsId", c => c.Byte(nullable: false));
            RenameColumn(table: "dbo.AssignedDoctorsAndPatients", name: "PatientsId", newName: "Patients_Id");
            AddColumn("dbo.AssignedDoctorsAndPatients", "PatientsId", c => c.Byte(nullable: false));
            CreateIndex("dbo.AssignedDoctorsAndPatients", "Patients_Id");
            AddForeignKey("dbo.AssignedDoctorsAndPatients", "Patients_Id", "dbo.Patients", "Id");
        }
    }
}

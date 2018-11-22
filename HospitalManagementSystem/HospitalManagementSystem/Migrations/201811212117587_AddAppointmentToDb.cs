namespace HospitalManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAppointmentToDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        NumberOfDaysSick = c.Int(nullable: false),
                        DoctorsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Doctors", "Appointments_Id", c => c.Int());
            CreateIndex("dbo.Doctors", "Appointments_Id");
            AddForeignKey("dbo.Doctors", "Appointments_Id", "dbo.Appointments", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Doctors", "Appointments_Id", "dbo.Appointments");
            DropIndex("dbo.Doctors", new[] { "Appointments_Id" });
            DropColumn("dbo.Doctors", "Appointments_Id");
            DropTable("dbo.Appointments");
        }
    }
}

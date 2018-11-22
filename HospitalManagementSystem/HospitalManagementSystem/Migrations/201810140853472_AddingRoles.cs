namespace HospitalManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingRoles : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into AspNetRoles(Id, Name) Values(1, 'Doctor')");
            Sql("Insert into AspNetRoles(Id, Name) Values(2, 'Patient')");
        }
        
        public override void Down()
        {
        }
    }
}

namespace ISIC_DATA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IsicDogs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Today_reg = c.String(),
                        Reg = c.String(),
                        Name = c.String(),
                        S = c.String(),
                        Born = c.DateTime(nullable: false),
                        Date_Estimation2 = c.DateTime(nullable: false),
                        Reg_F = c.String(),
                        Father = c.String(),
                        Reg_M = c.String(),
                        Mother = c.String(),
                        B = c.String(),
                        C = c.String(),
                        Color = c.String(),
                        Breeder = c.String(),
                        Owner = c.String(),
                        HD = c.String(),
                        HD2 = c.String(),
                        Inbreeding = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.IsicDogs");
        }
    }
}

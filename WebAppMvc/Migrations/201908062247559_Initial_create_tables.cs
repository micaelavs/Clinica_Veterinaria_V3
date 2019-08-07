namespace WebAppMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_create_tables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        Name = c.String(),
                        Gender = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Facturas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdTurno = c.Int(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        Monto = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Turnoes", t => t.IdTurno, cascadeDelete: true)
                .Index(t => t.IdTurno);
            
            CreateTable(
                "dbo.Turnoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdPaciente = c.Int(nullable: false),
                        IdSala = c.Int(nullable: false),
                        IdDoctor = c.Int(nullable: false),
                        TipoEspecialidad = c.Int(nullable: false),
                        Estado = c.Int(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        Hora = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Doctors", t => t.IdDoctor, cascadeDelete: true)
                .ForeignKey("dbo.Patients", t => t.IdPaciente, cascadeDelete: true)
                .ForeignKey("dbo.Rooms", t => t.IdSala, cascadeDelete: true)
                .Index(t => t.IdPaciente)
                .Index(t => t.IdSala)
                .Index(t => t.IdDoctor);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Location = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Facturas", "IdTurno", "dbo.Turnoes");
            DropForeignKey("dbo.Turnoes", "IdSala", "dbo.Rooms");
            DropForeignKey("dbo.Turnoes", "IdPaciente", "dbo.Patients");
            DropForeignKey("dbo.Turnoes", "IdDoctor", "dbo.Doctors");
            DropForeignKey("dbo.Patients", "ClientId", "dbo.Clients");
            DropIndex("dbo.Turnoes", new[] { "IdDoctor" });
            DropIndex("dbo.Turnoes", new[] { "IdSala" });
            DropIndex("dbo.Turnoes", new[] { "IdPaciente" });
            DropIndex("dbo.Facturas", new[] { "IdTurno" });
            DropIndex("dbo.Patients", new[] { "ClientId" });
            DropTable("dbo.Rooms");
            DropTable("dbo.Turnoes");
            DropTable("dbo.Facturas");
            DropTable("dbo.Doctors");
            DropTable("dbo.Patients");
            DropTable("dbo.Clients");
        }
    }
}

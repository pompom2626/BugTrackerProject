namespace BugTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1st_Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PriorityOfTickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TicketModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Name = c.String(),
                        Created = c.DateTimeOffset(nullable: false, precision: 7),
                        Updated = c.DateTimeOffset(precision: 7),
                        TicketPriorityId = c.Int(nullable: false),
                        TicketStatusId = c.Int(nullable: false),
                        TicketTypeId = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                        CreatingId = c.String(maxLength: 128),
                        AssignedId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.AssignedId)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatingId)
                .ForeignKey("dbo.PriorityOfTickets", t => t.TicketPriorityId, cascadeDelete: true)
                .ForeignKey("dbo.StatusOfTickets", t => t.TicketStatusId, cascadeDelete: true)
                .ForeignKey("dbo.TypeOfTickets", t => t.TicketTypeId, cascadeDelete: true)
                .Index(t => t.TicketPriorityId)
                .Index(t => t.TicketStatusId)
                .Index(t => t.TicketTypeId)
                .Index(t => t.ProjectId)
                .Index(t => t.CreatingId)
                .Index(t => t.AssignedId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        FinalName = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        TicketModel_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.TicketModels", t => t.TicketModel_Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.TicketModel_Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.TicketAttachmentsModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TicketId = c.Int(nullable: false),
                        FilePath = c.String(),
                        Description = c.String(),
                        Created = c.DateTimeOffset(nullable: false, precision: 7),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TicketModels", t => t.TicketId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.TicketId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.TicketCommentsModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comment = c.String(),
                        Created = c.DateTimeOffset(nullable: false, precision: 7),
                        TicketId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TicketModels", t => t.TicketId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.TicketId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.TicketHistoriesModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TicketId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                        Property = c.String(),
                        OldValue = c.String(),
                        NewValue = c.String(),
                        Changed = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TicketModels", t => t.TicketId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.TicketId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.StatusOfTickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TypeOfTickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.ProjectApplicationUsers",
                c => new
                    {
                        Project_Id = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Project_Id, t.ApplicationUser_Id })
                .ForeignKey("dbo.Projects", t => t.Project_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.Project_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUsers", "TicketModel_Id", "dbo.TicketModels");
            DropForeignKey("dbo.TicketModels", "TicketTypeId", "dbo.TypeOfTickets");
            DropForeignKey("dbo.TicketModels", "TicketStatusId", "dbo.StatusOfTickets");
            DropForeignKey("dbo.TicketModels", "TicketPriorityId", "dbo.PriorityOfTickets");
            DropForeignKey("dbo.TicketModels", "CreatingId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TicketModels", "AssignedId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TicketHistoriesModels", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TicketHistoriesModels", "TicketId", "dbo.TicketModels");
            DropForeignKey("dbo.TicketCommentsModels", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TicketCommentsModels", "TicketId", "dbo.TicketModels");
            DropForeignKey("dbo.TicketAttachmentsModels", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TicketAttachmentsModels", "TicketId", "dbo.TicketModels");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProjectApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProjectApplicationUsers", "Project_Id", "dbo.Projects");
            DropForeignKey("dbo.TicketModels", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.ProjectApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ProjectApplicationUsers", new[] { "Project_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.TicketHistoriesModels", new[] { "UserId" });
            DropIndex("dbo.TicketHistoriesModels", new[] { "TicketId" });
            DropIndex("dbo.TicketCommentsModels", new[] { "UserId" });
            DropIndex("dbo.TicketCommentsModels", new[] { "TicketId" });
            DropIndex("dbo.TicketAttachmentsModels", new[] { "UserId" });
            DropIndex("dbo.TicketAttachmentsModels", new[] { "TicketId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "TicketModel_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.TicketModels", new[] { "AssignedId" });
            DropIndex("dbo.TicketModels", new[] { "CreatingId" });
            DropIndex("dbo.TicketModels", new[] { "ProjectId" });
            DropIndex("dbo.TicketModels", new[] { "TicketTypeId" });
            DropIndex("dbo.TicketModels", new[] { "TicketStatusId" });
            DropIndex("dbo.TicketModels", new[] { "TicketPriorityId" });
            DropTable("dbo.ProjectApplicationUsers");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.TypeOfTickets");
            DropTable("dbo.StatusOfTickets");
            DropTable("dbo.TicketHistoriesModels");
            DropTable("dbo.TicketCommentsModels");
            DropTable("dbo.TicketAttachmentsModels");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Projects");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.TicketModels");
            DropTable("dbo.PriorityOfTickets");
        }
    }
}

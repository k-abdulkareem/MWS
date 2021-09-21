using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MWS_SocialNetwork.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MWS_SocialNetwork.Data
{
    public class DatabaseContext: IdentityDbContext<ApplicationUser>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
          
        }

        protected override void OnModelCreating(ModelBuilder modelBuilde)
        {
            Seed(modelBuilde);

            modelBuilde.Entity<ApplicationUser>().HasOne(x => x.SocialEntity).WithOne(x => x.ApplicationUser).HasForeignKey<ApplicationUser>(x => x.Id);
            modelBuilde.Entity<Group>().HasOne(x => x.SocialEntity).WithOne(x => x.Group).HasForeignKey<Group>(x => x.Id);
            modelBuilde.Entity<RelationshipType>().HasOne(x => x.SocialEntity).WithOne(x => x.RelationshipType).HasForeignKey<RelationshipType>(x => x.Id);
            modelBuilde.Entity<GroupMember>().HasIndex(x => new { x.GroupId, x.UserId }).IsUnique();

            foreach (var relationship in modelBuilde.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.Restrict;

          //  modelBuilde.Entity<DisallowedEntity>().HasOne(x => x.PrivacySetting).WithMany(x => x.).hasfor
            base.OnModelCreating(modelBuilde);
            
        }

        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<DisallowedEntity> DisallowedEntities { get; set; }
        public DbSet<DisallowedPermission> DisallowedPermissions { get; set; }
        public DbSet<Education> Education { get; set; }
        public DbSet<EducationDegree> EducationDegrees { get; set; }
        public DbSet<EntityType> EntityTypes { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupMember> GroupMembers { get; set; }
        
        public DbSet<Interaction> Interactions { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationType> NotificationTypes { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionType> PermissionTypes { get; set; }


        public DbSet<Post> Posts { get; set; }


        public DbSet<PrivacySetting> PrivacySettings { get; set; }
        public DbSet<RelationshipType> RelationshipTypes { get; set; }
        public DbSet<SocialEntity> SocialEntities { get; set; }
        public DbSet<U2PRelationship> U2PRelationships { get; set; }
        public DbSet<U2PRelationshipType> U2PRelationshipTypes { get; set; }
        public DbSet<U2URelationship> U2URelationships { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Work> Work { get; set; }
        public DbSet<ConflictResolutionPolicy> ConflictResolutionPolicies { get; set; }



        private void Seed(ModelBuilder builder)
        {
            //seed Entity Types
            builder.Entity<EntityType>().HasData(new EntityType { Id = 1, Title = "User" });
            builder.Entity<EntityType>().HasData(new EntityType { Id = 2, Title = "Group" });
            builder.Entity<EntityType>().HasData(new EntityType { Id = 3, Title = "Relationship" });

            // seed SocialEntity : relationshiptypes
            builder.Entity<SocialEntity>().HasData(new SocialEntity { Id = "191c5f8d-141b-46ee-b024-604281936a72", EntityTypeId = 3 });
            builder.Entity<RelationshipType>().HasData(new RelationshipType { Id = "191c5f8d-141b-46ee-b024-604281936a72", Title = "Family" , Order = 1});

            builder.Entity<SocialEntity>().HasData(new SocialEntity { Id = "be68e446-e31f-463c-8176-31e0d3efc7ef", EntityTypeId = 3 });
            builder.Entity<RelationshipType>().HasData(new RelationshipType { Id = "be68e446-e31f-463c-8176-31e0d3efc7ef", Title = "Friend", Order = 2 });

            builder.Entity<SocialEntity>().HasData(new SocialEntity { Id = "faf04a8d-350a-4153-8afe-bbbd0bcf7410", EntityTypeId = 3 });
            builder.Entity<RelationshipType>().HasData(new RelationshipType { Id = "faf04a8d-350a-4153-8afe-bbbd0bcf7410", Title = "Colleague", Order = 3 });

            builder.Entity<SocialEntity>().HasData(new SocialEntity { Id = "07d39285-1d5c-4149-9964-9682d44063b7", EntityTypeId = 3 });
            builder.Entity<RelationshipType>().HasData(new RelationshipType { Id = "07d39285-1d5c-4149-9964-9682d44063b7", Title = "Coworker", Order = 4});

            //seed U2P relationship types
            builder.Entity<U2PRelationshipType>().HasData(new U2PRelationshipType { Id = 1, Title = "Owner", Caption= "My own Post" });
            builder.Entity<U2PRelationshipType>().HasData(new U2PRelationshipType { Id = 2, Title = "Contributor", Caption = "Post that I Add to others" });
            builder.Entity<U2PRelationshipType>().HasData(new U2PRelationshipType { Id = 3, Title = "Tagged", Caption = "Post that I am tagged in" });
            builder.Entity<U2PRelationshipType>().HasData(new U2PRelationshipType { Id = 4, Title = "Shareholder", Caption = "Post that I share" });

            //seed Permissions Types
            builder.Entity<PermissionType>().HasData(new PermissionType { Id = 1, Title = "Contributing" });
            builder.Entity<PermissionType>().HasData(new PermissionType { Id = 2, Title = "Tagging" });
            builder.Entity<PermissionType>().HasData(new PermissionType { Id = 3, Title = "Sharing" });


            //seed Countries
            builder.Entity<Country>().HasData(new Country { Id = 1, Name = "Not Specified" });
            builder.Entity<Country>().HasData(new Country { Id = 2, Name = "Syria" });
            builder.Entity<Country>().HasData(new Country { Id = 3, Name = "Lebanon" });
            builder.Entity<Country>().HasData(new Country { Id = 4, Name = "France" });
            builder.Entity<Country>().HasData(new Country { Id = 5, Name = "Egypt" });
            builder.Entity<Country>().HasData(new Country { Id = 6, Name = "Germany" });
            builder.Entity<Country>().HasData(new Country { Id = 7, Name = "Denmark" });

            //seed education degrees
            builder.Entity<EducationDegree>().HasData(new EducationDegree { Id = 1, Name = "Not Specified" });
            builder.Entity<EducationDegree >().HasData(new EducationDegree { Id = 2, Name = "High School" });
            builder.Entity<EducationDegree>().HasData(new EducationDegree { Id = 3, Name = "Bachelor" });
            builder.Entity<EducationDegree>().HasData(new EducationDegree { Id = 4, Name = "Master" });
            builder.Entity<EducationDegree>().HasData(new EducationDegree { Id = 5, Name = "Ph.D." });

            //seed Notification Types
            builder.Entity<NotificationType>().HasData(new NotificationType { Id = 1, Code = "AddRelationship", Action = "" , Controller ="" });
            builder.Entity<NotificationType>().HasData(new NotificationType { Id = 2, Code = "ChangeConflictPolicy", Action ="" , Controller = ""});
            builder.Entity<NotificationType>().HasData(new NotificationType { Id = 3, Code = "ChangeRelationship", Action = "Index" , Controller = "UserProfile" });
            builder.Entity<NotificationType>().HasData(new NotificationType { Id = 4, Code = "GroupInvitation", Action = "Index", Controller = "GroupProfile" });
            builder.Entity<NotificationType>().HasData(new NotificationType { Id = 5, Code = "Tag", Action = "Index", Controller = "MyPosts" });
            builder.Entity<NotificationType>().HasData(new NotificationType { Id = 6, Code = "Contribute", Action = "Index", Controller = "MyPosts" });


            //seed Conflict resolution Policies
            builder.Entity<ConflictResolutionPolicy>().HasData(new ConflictResolutionPolicy { Id = 1, PolicyCode = "All", Active = true , Description = "Sum of decisions must equal to controllers count" }  );
            builder.Entity<ConflictResolutionPolicy>().HasData(new ConflictResolutionPolicy { Id = 2, PolicyCode = "One", Active = false, Description = "It's enough to get at least one approval , so Sum of decisions must be Larger or equal to 1" });
            builder.Entity<ConflictResolutionPolicy>().HasData(new ConflictResolutionPolicy { Id = 3, PolicyCode = "Majority", Active = false, Description = "It's required to get the majority of approvals, so Sum of decisions must be larger or equalt to half of controllers count" });

          


        }

    }
    }

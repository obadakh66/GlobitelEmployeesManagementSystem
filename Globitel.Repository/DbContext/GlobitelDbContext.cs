using Globitel.Domain.Common;
using Globitel.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Globitel.Repository.DbContext
{
    public partial class GlobitelDbContext : IdentityDbContext<ApplicationUser, Roles, long, UserClaims, UserRoles, UserLogins, RoleClaims, AspNetUserTokens>
    {
        public GlobitelDbContext()
        {


        }

        public GlobitelDbContext(DbContextOptions<GlobitelDbContext> options)
            : base(options)
        {

        }

        public virtual DbSet<ApplicationUser> ApplicationUser { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            AppConfig appConfiguration = new AppConfig();
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer(appConfiguration.ConnectionString);
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");


                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });


            modelBuilder.Entity<RoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<UserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<UserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<UserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserId);
            });
            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser { 
                    Id = 1L, 
                    AccessFailedCount = 0, 
                    ConcurrencyStamp = "06ea7e61-ebc4-4d16-9cf1-e4ea8ee65e18", 
                    Email = "admin@admin.com", 
                    EmailConfirmed = true, 
                    FullNameEN = "Obada Alkhdor", 
                    FullNameAR = "عبادة الخضور", 
                    NormalizedEmail = "ADMIN@ADMIN.COM", 
                    NormalizedUserName = "SUPERADMIN", 
                    PasswordHash = "AQAAAAEAACcQAAAAELr73GmhxCifQepLABo6ilVa+fVkEu+M40P4tfg2C5xiBxeEROKT2xfyxcIoU57roQ==", 
                    PhoneNumberConfirmed = true, 
                    SecurityStamp = "RCWSSGGSUGXLM4LBICRGPK75IGT77DIY", 
                    UserName = "superadmin", 
                    IsActive = true
                }
                );

            modelBuilder.Entity<Roles>().HasData(
             new Roles { Id = 1L, Name = "Admin", NormalizedName = "ADMIN" },
             new Roles { Id = 2L, Name = "Employee", NormalizedName = "EMPLOYEE" }
             );
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

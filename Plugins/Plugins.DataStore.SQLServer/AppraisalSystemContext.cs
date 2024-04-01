using CoreBusiness;
using Microsoft.EntityFrameworkCore;

namespace Plugins.DataStore.SQLServer
{
    public class AppraisalSystemContext : DbContext
    {
        public AppraisalSystemContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Appraisal> Appraisals { get; set; }
        public DbSet<AppraisalDetailsCompetency> AppraisalDetailsCompetencies { get; set; }
        public DbSet<AppraisalDetailsObjective> AppraisalDetailsObjectives { get; set; }
        public DbSet<Competency> Competencies { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleCompetencyDetails> RoleCompetencyDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure AppraisalId as primary key for the Appraisal entity
            modelBuilder.Entity<Appraisal>()
                .HasKey(a => a.AppraisalId);

            // Configure foreign key relationship with Employee entity for employee
            modelBuilder.Entity<Appraisal>()
                .HasOne(a => a.Employee)
                .WithMany(e => e.EmployeeAppraisals)
                .HasForeignKey(a => a.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure foreign key relationship with Employee entity for manager
/*            modelBuilder.Entity<Appraisal>()
                .HasOne(a => a.Manager)
                .WithMany(e => e.ManagerAppraisals)
                .HasForeignKey(a => a.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);*/

            // Configure composite primary key for AppraisalDetailsCompetency entity
            modelBuilder.Entity<AppraisalDetailsCompetency>()
                .HasKey(ad => ad.DetailId);

            // Configure foreign key relationships for AppraisalDetailsCompetency
            modelBuilder.Entity<AppraisalDetailsCompetency>()
                .HasOne(ad => ad.Appraisal)
                .WithMany(a => a.AppraisalDetailsCompetencies)
                .HasForeignKey(ad => ad.AppraisalId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AppraisalDetailsCompetency>()
                .HasOne(ad => ad.Employee)
                .WithMany(e => e.AppraisalDetailsCompetencies)
                .HasForeignKey(ad => ad.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AppraisalDetailsCompetency>()
                .HasOne(ad => ad.Manager)
                .WithMany()
                .HasForeignKey(ad => ad.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure composite primary key for AppraisalDetailsObjective entity
            modelBuilder.Entity<AppraisalDetailsObjective>()
                .HasKey(ad => ad.DetailId);

            // Configure foreign key relationships for AppraisalDetailsObjective
            modelBuilder.Entity<AppraisalDetailsObjective>()
                .HasOne(ad => ad.Appraisal)
                .WithMany(a => a.AppraisalDetailsObjectives)
                .HasForeignKey(ad => ad.AppraisalId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AppraisalDetailsObjective>()
                .HasOne(ad => ad.Employee)
                .WithMany(e => e.AppraisalDetailsObjectives)
                .HasForeignKey(ad => ad.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AppraisalDetailsObjective>()
                .HasOne(ad => ad.Manager)
                .WithMany()
                .HasForeignKey(ad => ad.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure foreign key relationship with Employee entity for manager
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Manager)
                .WithMany(m => m.ManagedEmployees)
                .HasForeignKey(e => e.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);


            // Configure composite primary key for RoleCompetencyDetails entity
            modelBuilder.Entity<RoleCompetencyDetails>()
                .HasKey(rcd => rcd.DetailId);

            // Configure foreign key relationships for RoleCompetencyDetails
            modelBuilder.Entity<RoleCompetencyDetails>()
                .HasOne(rcd => rcd.Role)
                .WithMany(r => r.RoleCompetencyDetails)
                .HasForeignKey(rcd => rcd.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RoleCompetencyDetails>()
                .HasOne(rcd => rcd.Competency)
                .WithMany(c => c.RoleCompetencyDetails)
                .HasForeignKey(rcd => rcd.CompetencyId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seeding data
            modelBuilder.Entity<Employee>().HasData(
                new Employee { EmployeeId = 1, EmployeeName = "Jacob", Mobile = "654321", Email = "jacob@gmail.com", EmployeeDesignation = "Sr.Dev", ManagerId = null, Password = "jacob", AdminPermission = 2 },
                new Employee { EmployeeId = 10, EmployeeName = "John", Mobile = "321456", Email = "john@gmail.com", EmployeeDesignation = "Dev", ManagerId = 1, Password = "jacob", AdminPermission = 3 }
            );

            modelBuilder.Entity<Competency>().HasData(
                new Competency { CompetencyId = 1, CompetencyName = "CyberSecurity", competencyType = CompetencyType.Technical },
                new Competency { CompetencyId = 2, CompetencyName = "CloudComputing", competencyType = CompetencyType.Technical }
            );

            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = 1, RoleName = "Sr.dev" },
                new Role { RoleId = 2, RoleName = "Dev" }
            );

            modelBuilder.Entity<RoleCompetencyDetails>().HasData(
                new RoleCompetencyDetails { DetailId = 1, RoleId = 1, CompetencyId = 1 },
                new RoleCompetencyDetails { DetailId = 2, RoleId = 2, CompetencyId = 1 },
                new RoleCompetencyDetails { DetailId = 3, RoleId = 1, CompetencyId = 2 },
                new RoleCompetencyDetails { DetailId = 4, RoleId = 2, CompetencyId = 2 }
            );

            
        }
    }
}

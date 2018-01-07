using Microsoft.EntityFrameworkCore;
using P01_HospitalDatabase.Data.Models;

namespace P01_HospitalDatabase.Data
{
    public class HospitalContext : DbContext
    {
        public HospitalContext() { }

        public HospitalContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Diagnose> Diagnoses { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<PatientMedicament> PatientsMedicaments { get; set; }
        public DbSet<Visitation> Visitations { get; set; }
        public DbSet<Doctor> Doctors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>(entity =>
                {
                    entity.HasKey(e => e.PatientId);

                    entity.Property(e => e.FirstName)
                        .IsRequired(true)
                        .IsUnicode(true)
                        .HasMaxLength(50);

                    entity.Property(e => e.LastName)
                        .IsRequired(true)
                        .IsUnicode(true)
                        .HasMaxLength(50);

                    entity.Property(e => e.Address)
                        .IsRequired(true)
                        .IsUnicode(true)
                        .HasMaxLength(250);

                    entity.Property(e => e.Email)
                        .IsRequired(true)
                        .IsUnicode(false)
                        .HasMaxLength(80);

                    entity.Property(e => e.HasInsurance)
                        .HasDefaultValue(true);
                });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => e.DoctorId);

                entity.Property(p => p.Name)
                    .IsRequired()
                    .IsUnicode()
                    .HasMaxLength(100);

                entity.Property(p => p.Specialty)
                    .IsRequired()
                    .IsUnicode()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Visitation>(entity =>
            {
                entity.HasKey(e => e.VisitationId);

                entity.Property(p => p.Date)
                    .IsRequired()
                    .HasColumnName("DATETIME2")
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(p => p.Comments)
                    .IsRequired(false)
                    .IsUnicode()
                    .HasMaxLength(250);

                entity.HasOne(e => e.Patient)
                    .WithMany(p => p.Visitations)
                    .HasForeignKey(e => e.PatientId)
                    .HasConstraintName("FK_Visitation_Patient");

                entity.Property(e => e.DoctorId)
                .IsRequired(false);

                entity.HasOne(e => e.Doctor)
                    .WithMany(p => p.Visitations)
                    .HasForeignKey(e => e.DoctorId)
                    .HasConstraintName("FK_Visitation_Doctor");
            });

            modelBuilder.Entity<Diagnose>(entity =>
            {
                entity.HasKey(e => e.DiagnoseId);

                entity.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(p => p.Comments)
                    .IsRequired(false)
                    .IsUnicode()
                    .HasMaxLength(250);

                entity.HasOne(e => e.Patient)
                    .WithMany(p => p.Diagnoses)
                    .HasForeignKey(e => e.PatientId)
                    .HasConstraintName("FK_Diagnose_Patient");
            });

            modelBuilder.Entity<Medicament>(entity =>
            {
                entity.HasKey(e => e.MedicamentId);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .IsUnicode()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<PatientMedicament>(entity =>
            {
                entity.HasKey(e => new { e.PatientId, e.MedicamentId });

                entity.HasOne(e => e.Medicament)
                    .WithMany(m => m.Prescriptions)
                    .HasForeignKey(e => e.MedicamentId);

                entity.HasOne(e => e.Patient)
                    .WithMany(m => m.Prescriptions)
                    .HasForeignKey(e => e.PatientId);
            });
        }
    }
}

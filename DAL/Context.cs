namespace DAL.Entities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Context : DbContext
    {
        public Context()
            : base("Context")
        {
        }

        public virtual DbSet<Costs> Costss { get; set; }
        public virtual DbSet<CostsCategory> CostsCategories { get; set; }
        public virtual DbSet<Income> Incomes { get; set; }
        public virtual DbSet<IncomeCategory> IncomeCategories { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Costs>()
                .Property(e => e.Sum)
                .HasPrecision(19, 4);

            modelBuilder.Entity<CostsCategory>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<CostsCategory>()
                .HasMany(e => e.Costs)
                .WithOptional(e => e.CostsCategory)
                .HasForeignKey(e => e.ID_CostsCategory);

            modelBuilder.Entity<Income>()
                .Property(e => e.Sum)
                .HasPrecision(19, 4);

            modelBuilder.Entity<IncomeCategory>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<IncomeCategory>()
                .HasMany(e => e.Income)
                .WithOptional(e => e.IncomeCategory)
                .HasForeignKey(e => e.ID_IncomeCategory);

            modelBuilder.Entity<User>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Login)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Costs)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.ID_User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Income)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.ID_User)
                .WillCascadeOnDelete(false);
        }
    }
}

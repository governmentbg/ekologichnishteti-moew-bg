using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Reflection;
using Zopoesht.Data.AdministrativeExpenses;
using Zopoesht.Data.Applications.ApplicationOne;
using Zopoesht.Data.Applications.ApplicationTwo;
using Zopoesht.Data.Applications.ApplicationTwo.FinancialAssurance;
using Zopoesht.Data.Applications.Common.Models;
using Zopoesht.Data.Common.Interfaces;
using Zopoesht.Data.Emails;
using Zopoesht.Data.Nomenclatures;
using Zopoesht.Data.Nomenclatures.Operators;
using Zopoesht.Data.Nomenclatures.Settlements;
using Zopoesht.Data.Nomenclatures.StateAdministration;
using Zopoesht.Data.Users;
using Zopoesht.Infrastructure.Interfaces.Contexts;
using Zopoesht.Infrastructure.Interfaces.Users;

namespace Zopoesht.Persistence.Contexts
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        private readonly IUserContext userContext;
        public AppDbContext(
            IUserContext userContext,
            DbContextOptions<AppDbContext> options)
            : base(options)
        {
            this.userContext = userContext;
        }

        #region Users
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<PasswordToken> PasswordTokens { get; set; }
        #endregion

        #region Nomenclatures
        public DbSet<Code> Codes { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<SubActivity> SubActivities { get; set; }
        public DbSet<Settlement> Settlements { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Municipality> Municipalities { get; set; }
        public DbSet<Year> Years { get; set; }
        public DbSet<Period> Periods { get; set; }

        public DbSet<Prosecutor> Prosecutors {  get; set; }
        public DbSet<MinistryOfInterior> MinistriesOfInterior { get; set; }
        public DbSet<AdministrativeCourt> AdministrativeCourts { get; set; }

        public DbSet<Operator> Operators { get; set; }
        #endregion

        #region Emails
        public DbSet<Email> Emails { get; set; }
        public DbSet<EmailAddressee> EmailAddressees { get; set; }
        #endregion

        #region Applications
        public DbSet<ApplicationHistory> ApplicationHistories { get; set; }
        public DbSet<ApplicationLock> ApplicationLocks { get; set; }

        // ApplicationOne
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<LegalEntity> LegalEntities { get; set; }
        public DbSet<Individual> Individuals { get; set; }
        public DbSet<ApplicationOneAuthority> ApplicationOneAuthorities { get; set; }
        public DbSet<ApplicationOneBasic> ApplicationOneBasics { get; set; }
        public DbSet<ApplicationOneLegalEntity> ApplicationOneLegalEntities { get; set; }
        public DbSet<ApplicationOneThreat> ApplicationOneThreats { get; set; }
        public DbSet<ApplicationOneDamage> ApplicationOneDamages { get; set; }
        public DbSet<ApplicationOne> ApplicationOnes { get; set; }
        public DbSet<OperatorCorrespondence> OperatorCorrespondences { get; set; }
        public DbSet<ActivityOffering> ActivityOfferings { get; set; }
        public DbSet<Terrain> Terrains { get; set; }
        public DbSet<ApplicationOneFile> ApplicationOneFiles { get; set; }
        public DbSet<ApplicationOneTerritorialRange> ApplicationOneTerritorialRanges { get; set; }
        public DbSet<ApplicationOneActivityOffering> ApplicationOneActivityOfferings { get; set; }
        public DbSet<ApplicationOneAffectedCountry> ApplicatoAffectedCountries { get; set; }

        // ApplicationTwo

        public DbSet<ApplicationTwo> ApplicationTwos { get; set; }
        public DbSet<ApplicationTwoCode> ApplicationTwoCodes { get; set; }
        public DbSet<ApplicationTwoFile> ApplicationTwoFiles { get; set; }
        public DbSet<ApplicationTwoAuthority> ApplicationTwoAuthorities { get; set; }
        public DbSet<ApplicationTwoProsecutor> ApplicationTwoProsecutors { get; set; }
        public DbSet<ApplicationTwoMinistryOfInterior> ApplicationTwoMinistriesOfInterior { get; set; }
        public DbSet<ApplicationTwoAdministrativeCourt> ApplicationTwoAdministrativeCourts { get; set; }
        public DbSet<ApplicationTwoActivityOffering> ApplicationTwoActivityOfferings { get; set; }

        public DbSet<InsurancePolicy> InsurancePolicies { get; set; }
        public DbSet<BankGuarantee> BankGuarantees { get; set; }
        public DbSet<Mortgage> Mortgages { get; set; }
        public DbSet<Stake> Stakes { get; set; }

        #endregion\

        #region AdministrativeExpenses

        public DbSet<AnnualAdministrativeExpenses> AnnualAdministrativeExpenses { get; set; }
        public DbSet<AnnualAdministrativeExpensesHistory> AnnualAdministrativeExpensesHistory { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                // Configure name mappings
                entity.SetTableName(entity.ClrType.Name.ToLower());

                if (typeof(IEntity).IsAssignableFrom(entity.ClrType))
                {
                    modelBuilder.Entity(entity.ClrType)
                        .HasKey(nameof(IEntity.Id));
                }

                if (typeof(IConcurrency).IsAssignableFrom(entity.ClrType))
                {
                    modelBuilder.Entity(entity.ClrType)
                        .Property(nameof(IConcurrency.Version))
                        .IsConcurrencyToken()
                        .HasDefaultValue(0);
                }

                entity.GetProperties()
                    .ToList()
                    .ForEach(e => e.SetColumnName(e.Name.ToLower()));

                entity.GetForeignKeys()
                    .Where(e => !e.IsOwnership && e.DeleteBehavior == DeleteBehavior.Cascade)
                    .ToList()
                    .ForEach(e => e.DeleteBehavior = DeleteBehavior.Restrict);
            }

            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                   .Where(t => t.GetInterfaces().Any(gi =>
                       gi.IsGenericType
                       && gi.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)) && t.Namespace == "Zopoesht.Persistence.Contexts")
                   .ToList();
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }
        }

        public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            return Database.BeginTransactionAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (typeof(IAuditable).IsAssignableFrom(entry.Entity.GetType()) && entry.State == EntityState.Added)
                {
                    var entity = entry.Entity as IAuditable;
                    entity.CreateDate = DateTime.Now;
                    //entity.CreatorUserId = this.userContext.UserId;
                }

                if (typeof(IConcurrency).IsAssignableFrom(entry.Entity.GetType()) && entry.State == EntityState.Modified)
                {
                    var entity = entry.Entity as IConcurrency;
                    entity.Version++;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}

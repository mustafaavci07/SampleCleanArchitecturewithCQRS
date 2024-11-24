using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using System.IO;
using SampleCleanArchitecture.Core.Domain;
using SampleCleanArchitecture.Core.Domain.Journeys;
using SampleCleanArchitecture.Core.Domain.Passengers;
using SampleCleanArchitecture.Core.Domain.Payments;

using SmartEnum.EFCore;

using System.Reflection;

namespace SampleCleanArchitecture.Infrastructure.Persistence
{
    public class SampleContext(DbContextOptions<SampleContext> options): DbContext(options)
    {
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<PassengerJourney> PassengerJourneys { get; set; }
        public DbSet<Journey> Journeys { get; set; }
        public DbSet<Payment> Payment { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.ConfigureSmartEnum();
            //modelBuilder.Entity<AuditableBaseEntity>().Property(p => p.Id).HasConversion<UlidTypeConverter>();
            modelBuilder.Entity<Passenger>().ToTable("Passenger");
            modelBuilder.Entity<Journey>().ToTable("Journey");
            modelBuilder.Entity<Payment>().ToClassTable("Payment");
            modelBuilder.Entity<PassengerJourney>().ToTable("PassengerJourney");
            modelBuilder.ApplyConfiguration<PassengerJourney>(new PassengerJourneyConfigure());
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<Ulid>()
                .HaveConversion<UlidToStringConverter>()
                .HaveConversion<UlidToBytesConverter>()
                .HaveMaxLength(26);
            configurationBuilder.ConfigureSmartEnum();
            base.ConfigureConventions(configurationBuilder);
        }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                
                optionsBuilder.UseNpgsql(configuration.GetConnectionString("PostgresConnection"));
            }
        }*/
    }

    public class PassengerJourneyConfigure : IEntityTypeConfiguration<PassengerJourney>
    {
        public void Configure(EntityTypeBuilder<PassengerJourney> builder)
        {
            builder.HasOne(p => p.Passenger)
                .WithMany(p => p.Journeys);

            builder.Property(p=>p.PassengerId).HasConversion<UlidToStringConverter>().HasConversion<UlidToBytesConverter>();
            builder.Property(p=>p.JourneyId).HasConversion<UlidToStringConverter>().HasConversion<UlidToBytesConverter>();
        }
    }

    public class SampleContextFactory: IDesignTimeDbContextFactory<SampleContext>
    {
        public SampleContextFactory() : base() { }

        public SampleContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "../SampleCleanArchitecture.Presentation.WebApi","appsettings.json"))
            .Build();
            var optionsBuilder = new DbContextOptionsBuilder<SampleContext>();
            
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("PostgresConnection"));

            return new SampleContext(optionsBuilder.Options);
        }
    }

    public class UlidToBytesConverter : ValueConverter<Ulid, byte[]>
    {
        private static readonly ConverterMappingHints defaultHints = new ConverterMappingHints(size: 16);

        public UlidToBytesConverter() : this(null)
        {
        }

        public UlidToBytesConverter(ConverterMappingHints mappingHints = null)
            : base(
                    convertToProviderExpression: x => x.ToByteArray(),
                    convertFromProviderExpression: x => new Ulid(x),
                    mappingHints: defaultHints.With(mappingHints))
        {
        }
    }

    public class UlidToStringConverter : ValueConverter<Ulid, string>
    {
        private static readonly ConverterMappingHints defaultHints = new ConverterMappingHints(size: 26);

        public UlidToStringConverter() : this(null)
        {
        }

        public UlidToStringConverter(ConverterMappingHints mappingHints = null)
            : base(
                    convertToProviderExpression: x => x.ToString(),
                    convertFromProviderExpression: x => Ulid.Parse(x),
                    mappingHints: defaultHints.With(mappingHints))
        {
        }
    }


}

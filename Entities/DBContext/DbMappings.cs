using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ARQ.Maqueta.Entities
{
    public partial class DbMappings 
    {
        public virtual void Configure(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            #region Mapeo de la entidad Flight y la tabla dbo.Flight

            modelBuilder.Entity<Flight>().HasKey(t => t.Id).ToTable("Flight", "dbo");

            modelBuilder.Entity<Flight>().Property(t => t.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Flight>().Property(p => p.Airline)
                .HasColumnName("Airline")
                .HasMaxLength(255);

            modelBuilder.Entity<Flight>().Property(p => p.SourceAirportID)
                .HasColumnName("SourceAirportID")
                .IsRequired()
                .HasMaxLength(5);

            modelBuilder.Entity<Flight>().Property(p => p.SourceAirportName)
                .HasColumnName("SourceAirportName")
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<Flight>().Property(p => p.DestinationAirportID)
                .HasColumnName("DestinationAirportID")
                .IsRequired()
                .HasMaxLength(5);

            modelBuilder.Entity<Flight>().Property(p => p.DestinationAirportName)
                 .HasColumnName("DestinationAirportName")
                 .IsRequired()
                 .HasMaxLength(255);

            modelBuilder.Entity<Flight>().Property(p => p.FuelNeeded)
                .HasColumnName("FuelNeeded");

            modelBuilder.Entity<Flight>().Property(p => p.Stops)
                .HasColumnName("Stops");

            modelBuilder.Entity<Flight>().Property(p => p.Distance)
                .HasColumnName("Distance");

            modelBuilder.Entity<Flight>().Property(p => p.Active)
                .HasColumnName("Active")
                .IsRequired();

            modelBuilder.Entity<Flight>().Property(p => p.LastModifiedUser)
                .HasColumnName("LastModifiedUser")
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<Flight>().Property(p => p.LastModifiedDate)
                .HasColumnName("LastModifiedDate")
                .IsRequired()
                .HasColumnType("datetime2");

            #endregion

        }
    }
}
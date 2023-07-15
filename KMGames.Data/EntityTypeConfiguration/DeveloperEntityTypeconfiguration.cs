using KMGames.Entities;
using KMGames.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMGames.Data.EntityTypeConfiguration
{
    public class DeveloperEntityTypeconfiguration : EntityTypeConfiguration<Developer>
    {
        public DeveloperEntityTypeconfiguration()
        {
            ToTable("Developers");

            HasKey(d => d.DeveloperId);
            HasMany(d => d.Games).
                WithRequired(g => g.Developer).
                WillCascadeOnDelete(false);

            Property(d => d.RowVersion).IsRowVersion();
            Property(d => d.Name).IsRequired().HasMaxLength(100);
            Property(d => d.CountryId).IsRequired();
            Property(d => d.CityId).IsRequired();

            HasIndex(d => d.Name).IsUnique().HasName("IX_Developers_Name");
        }
    }
}

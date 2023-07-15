using KMGames.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMGames.Data.EntityTypeConfiguration
{
    public class CityEntityTypeConfiguration : EntityTypeConfiguration<City>
    {
        public CityEntityTypeConfiguration()
        {
            ToTable("Cities");
            HasKey(c => c.CityId);
            Property(c => c.Name).IsRequired().HasMaxLength(50);
            //HasIndex(c => c.Name).IsUnique().HasName("IX_Cities_Name");
            Property(c => c.RowVersion).IsRowVersion();
            Property(c => c.CountryId).IsRequired();
        }
    }
}

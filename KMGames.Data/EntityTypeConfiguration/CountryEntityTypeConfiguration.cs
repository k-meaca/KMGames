using KMGames.Data.Migrations;
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
    public class CountryEntityTypeConfiguration : EntityTypeConfiguration<Country>
    {
        public CountryEntityTypeConfiguration()
        {
            ToTable("Countries");
            HasKey(c => c.CountryId);
            Property(c => c.Name).IsRequired().HasMaxLength(50);
            HasIndex(c => c.Name).IsUnique().HasName("IX_Countries_Name");
            Property(c => c.RowVersion).IsRowVersion();
        }
    }
}

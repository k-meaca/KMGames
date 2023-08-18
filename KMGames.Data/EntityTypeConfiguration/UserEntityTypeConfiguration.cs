using KMGames.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMGames.Data.EntityTypeConfiguration
{
    public class UserEntityTypeConfiguration : EntityTypeConfiguration<User>
    {
        public UserEntityTypeConfiguration() 
        {
            ToTable("Users");

            HasKey(u => u.UserId);
            Property(u => u.UserId).HasMaxLength(168);
            Property(u => u.FirstName).HasMaxLength(256);
            Property(u => u.LastName).HasMaxLength(256);
            Property(u => u.Address).HasMaxLength(256);
            Property(u => u.Email).HasMaxLength(256);
            Property(u => u.DNI).HasMaxLength(25);

            Property(u => u.CityId).IsOptional();

            Property(u => u.RowVersion).IsRowVersion();
        }
    }
}

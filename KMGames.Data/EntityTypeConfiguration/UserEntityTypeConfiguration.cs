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
    public class UserEntityTypeConfiguration : EntityTypeConfiguration<User>
    {
        public UserEntityTypeConfiguration() 
        {
            ToTable("Users");
            
            HasKey(u => u.UserId);
            Property(u => u.NickName).HasMaxLength(int.MaxValue);
            Property(u => u.Email).HasMaxLength(int.MaxValue);
            Property(u => u.Address).HasMaxLength(int.MaxValue);
            Property(u => u.RowVersion).IsRowVersion();
        }
    }
}

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
    public class PlayerTypeEntityTypeConfiguration : EntityTypeConfiguration<PlayerType>
    {
        public PlayerTypeEntityTypeConfiguration()
        {
            ToTable("PlayerTypes");

            HasKey(p => p.PlayerTypeId);
            HasMany(p => p.PlayersGames).
                WithRequired(pg => pg.PlayerType).
                WillCascadeOnDelete(false);
            HasIndex(p => p.Type).IsUnique().HasName("IX_PlayerTypes_Type");

            Property(p => p.Type).IsRequired().HasMaxLength(100);
            Property(p => p.RowVersion).IsRowVersion();
        }
    }
}

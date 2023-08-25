using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMGames.Entities;
using KMGames.Entities.Entities;

namespace KMGames.Data.EntityTypeConfiguration
{
    public class GameEntityTypeConfiguration : EntityTypeConfiguration<Game>
    {
        public GameEntityTypeConfiguration()
        {
            ToTable("Games");
            HasKey(g => g.GameId);
            
            Property(g => g.Title).IsRequired().HasMaxLength(100);
            Property(g => g.ActualPrice).IsRequired();
            Property(g => g.RowVersion).IsRowVersion();
            Property(g => g.DeveloperId).IsRequired();
            Property(g => g.Release).IsRequired();
            Property(g => g.Description).IsOptional().HasMaxLength(250);
            Property(g => g.Image).HasMaxLength(int.MaxValue);
            
            HasMany(g => g.GameCategories).
                WithRequired(gc => gc.Game).
                WillCascadeOnDelete(false);
            HasMany(g => g.PlayersGame).
                WithRequired(pg => pg.Game).
                WillCascadeOnDelete(false);
            HasMany(g => g.SalesDetails).
                WithRequired(pg => pg.Game).
                WillCascadeOnDelete(false);
            HasMany(g => g.Carts)
                .WithRequired(gc => gc.Game)
                .WillCascadeOnDelete(false);

            HasIndex(g => g.Title).IsUnique().HasName("IX_Games_Title");
        }
    }
}

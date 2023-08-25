using KMGames.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMGames.Data.EntityTypeConfiguration
{
    public class GameInCartEntityTypeConfiguration : EntityTypeConfiguration<GameInCart>
    {
        public GameInCartEntityTypeConfiguration()
        {
            ToTable("GamesInCarts");

            HasKey(gc => new { gc.CartId, gc.GameId });

            Property(gc => gc.RowVersion).IsRowVersion();
        }
    }
}

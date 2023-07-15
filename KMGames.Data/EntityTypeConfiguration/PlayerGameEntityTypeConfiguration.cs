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
    public class PlayerGameEntityTypeConfiguration : EntityTypeConfiguration<PlayerGame>
    {
        public PlayerGameEntityTypeConfiguration() 
        {
            ToTable("PlayersGames");
            HasKey(pg => new { pg.GameId, pg.PlayerTypeId });

            Property(pg => pg.RowVersion).IsRowVersion();
        }
    }
}

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
    public class GameCategoryEntityTypeConfiguration : EntityTypeConfiguration<GameCategory>
    {
        public GameCategoryEntityTypeConfiguration()
        {
            ToTable("GameCategories");
            HasKey(gc => new { gc.CategoryId, gc.GameId });
            Property(gc => gc.RowVersion).IsRowVersion();
        }
    }
}

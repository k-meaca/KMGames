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
    public class SaleDetailEntityTypeConfiguration : EntityTypeConfiguration<SaleDetail>
    {
        public SaleDetailEntityTypeConfiguration()
        {
            ToTable("SalesDetails");
            HasKey(sd => new { sd.SaleId, sd.GameId });
            Property(sd => sd.RowVersion).IsRowVersion();
        }
    }
}

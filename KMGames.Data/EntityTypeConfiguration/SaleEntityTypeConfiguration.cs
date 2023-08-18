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
    public class SaleEntityTypeConfiguration : EntityTypeConfiguration<Sale>
    {
        public SaleEntityTypeConfiguration()
        {
            ToTable("Sales");

            HasKey(s => s.SaleId);
            HasMany(s => s.SalesDetails).
                WithRequired(sd => sd.Sale).
                WillCascadeOnDelete(false);

            Property(s => s.RowVersion).IsRowVersion();
            Property(s => s.Total).IsOptional();
            //Property(s => s.UserId).IsRequired();
        }
    }
}

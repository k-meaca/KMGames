using KMGames.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMGames.Data.EntityTypeConfiguration
{
    public class CartEntityTypeConfiguration : EntityTypeConfiguration<Cart>
    {
        public CartEntityTypeConfiguration() 
        {
            ToTable("Carts");

            HasKey(c => c.CartId);

            HasMany(c => c.Games)
                .WithRequired(gc => gc.Cart)
                .WillCascadeOnDelete(false);

            Property(c => c.RowVersion).IsRowVersion();
            Property(c => c.UserId).IsRequired();
        
        }
    }
}

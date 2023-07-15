using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMGames.Entities.Entities;

namespace KMGames.Data.EntityTypeConfiguration
{
    public class CategoryEntityTypeConfiguration : EntityTypeConfiguration<Category>
    {
        public CategoryEntityTypeConfiguration()
        {
            ToTable("Categories");
            HasKey(c => c.CategoryId);
            Property(c => c.Name).IsRequired().HasMaxLength(50);
            Property(c => c.RowVersion).IsRowVersion();
            HasMany(c => c.GameCategories).
                WithRequired(gm => gm.Category).
                WillCascadeOnDelete(false);
            HasIndex(c => c.Name).IsUnique().HasName("IX_Categories_Name");
            
        }
    }
}

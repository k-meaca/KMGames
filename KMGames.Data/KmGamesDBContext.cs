using KMGames.Data.EntityTypeConfiguration;
using KMGames.Entities.Entities;
using System;
using System.Data.Entity;
using System.Data.Entity.Hierarchy;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace KMGames.Data
{
    public class KmGamesDBContext : DbContext
    {
        // El contexto se ha configurado para usar una cadena de conexión 'KmGamesDBContext' del archivo 
        // de configuración de la aplicación (App.config o Web.config). De forma predeterminada, 
        // esta cadena de conexión tiene como destino la base de datos 'KMGames.Data.KmGamesDBContext' de la instancia LocalDb. 
        // 
        // Si desea tener como destino una base de datos y/o un proveedor de base de datos diferente, 
        // modifique la cadena de conexión 'KmGamesDBContext'  en el archivo de configuración de la aplicación.
        public KmGamesDBContext()
            : base("name=KmGamesDBContext")
        {
        }

        public virtual DbSet<Game> Games { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<GameCategory> GameCategories { get; set; }

        public virtual DbSet<Developer> Developers { get; set; }

        public virtual DbSet<Country> Countries { get; set; }

        public virtual DbSet<PlayerType> PlayerTypes { get; set; }

        public virtual DbSet<PlayerGame> PlayersGames { get; set; }

        public virtual DbSet<Sale> Sales { get; set; }

        public virtual DbSet<SaleDetail> SalesDetais { get; set; }

        public virtual DbSet<City> Cities { get; set; }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new GameEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new CategoryEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new GameCategoryEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new DeveloperEntityTypeconfiguration());
            modelBuilder.Configurations.Add(new CountryEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new PlayerTypeEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new PlayerGameEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new SaleEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new SaleDetailEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new CityEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new UserEntityTypeConfiguration());


            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}
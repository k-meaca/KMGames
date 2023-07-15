using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMGames.Entities.Entities
{
    public class Game
    {
        //------------PROPERTIES-------------//

        public int GameId { get; set; }

        public string Title { get; set; }

        public decimal ActualPrice { get; set; }

        public string Description { get; set; }

        public DateTime Release { get; set; }

        public byte[] RowVersion { get; set; }

        public virtual ICollection<GameCategory> GameCategories { get; set; }

        public int DeveloperId { get; set; }

        public Developer Developer { get; set; }

        public virtual ICollection<PlayerGame> PlayersGame { get; set; }

        public virtual ICollection<SaleDetail> SalesDetails { get; set; }

        //----------CONSTRUCTOR----------//

        public Game()
        {
            GameCategories = new HashSet<GameCategory>();
            PlayersGame = new HashSet<PlayerGame>();
            SalesDetails = new HashSet<SaleDetail>();
        }



    }
}

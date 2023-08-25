using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMGames.Entities.DTOs.GameDTO
{
    public class GameListDto
    {
        //----------PROPERTIES----------//

        public int GameId { get; set; }

        public string Title { get; set; }

        public decimal ActualPrice { get; set; }

        public DateTime Release { get; set; }

        public string Developer { get; set; }

        public string Image { get; set; }
    }
}

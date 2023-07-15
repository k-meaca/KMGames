using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMGames.Entities.DTOs.Developer
{
    public class DeveloperListDto
    {
        //-----------PROPERTIES-----------//

        public int DeveloperId { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public string City { get; set; }
    }
}

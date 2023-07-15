using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMGames.Entities.DTOs.Category
{
    public class CategoryCheckDto
    {
        //----------PROPERTIES----------//

        public int CategoryId { get; set; }

        public string Category { get; set; }

        public bool Selected { get; set; }
    }
}

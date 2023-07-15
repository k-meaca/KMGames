using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMGames.Entities.DTOs.Category
{
    public class CategoryListDto
    {
        //----------PROPERTIES----------//

        public int CategoryId { get; set; }

        public string Name { get; set; }

        public byte[] RowVersion { get; set; }
    }
}

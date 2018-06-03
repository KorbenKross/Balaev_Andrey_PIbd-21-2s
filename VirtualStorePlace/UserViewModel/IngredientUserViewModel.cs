using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualStorePlace.UserViewModel
{
    public class IngredientUserViewModel
    {
        public int Id { get; set; }

        public string IngredientName { get; set; }

        public decimal Price { get; set; }

        public List<IngredientElementUserViewModel> IngredientElement { get; set; }
    }
}

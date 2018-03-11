using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualStorePlace.UserViewModel
{
    public class CustomerSelectionUserViewModel
    {
        public int Id { get; set; }

        public int BuyerId { get; set; }

        public string BuyerFIO { get; set; }

        public int IngredientId { get; set; }

        public string IngredientName { get; set; }

        public int? KitchinerId { get; set; }

        public string KitchinerName { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }

        public string Status { get; set; }

        public string DateCreate { get; set; }

        public string DateCook { get; set; }
    }
}

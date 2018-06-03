using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualStorePlace.ConnectingModel
{
    public class CustomerSelectionModel
    {
        public int Id { get; set; }

        public int BuyerId { get; set; }

        public int IngredientId { get; set; }

        public int? KitchenerId { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }
    }
}

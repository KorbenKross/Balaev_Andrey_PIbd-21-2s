using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualStore
{
    public class CustomerSelection
    {
        public int Id { get; set; }

        public int BuyerId { get; set; }

        public int IngredientId { get; set; }

        public int? KitchenerId { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }

        public CustomerSelectionCondition Status { get; set; }

        public DateTime DateCreate { get; set; }

        public DateTime? DateImplement { get; set; }
    }
}

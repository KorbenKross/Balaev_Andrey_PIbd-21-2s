using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualStorePlace.ConnectingModel
{
    public class IngredientConnectingModel
    {
        public int Id { get; set; }

        public string IngredientName { get; set; }

        public decimal Value { get; set; }

        public List<IngredientElementConnectingModel> IngredientElement { get; set; }
    }
}

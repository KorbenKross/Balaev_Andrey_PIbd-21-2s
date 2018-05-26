using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace VirtualStorePlace.ConnectingModel
{
    [DataContract]
    public class CustomerSelectionModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int BuyerId { get; set; }

        [DataMember]
        public int IngredientId { get; set; }

        [DataMember]
        public int? KitchenerId { get; set; }

        [DataMember]
        public int Count { get; set; }

        [DataMember]
        public decimal Sum { get; set; }
    }
}

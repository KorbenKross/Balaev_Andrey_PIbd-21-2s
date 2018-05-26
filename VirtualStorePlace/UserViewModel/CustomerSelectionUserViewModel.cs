using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace VirtualStorePlace.UserViewModel
{
    [DataContract]
    public class CustomerSelectionUserViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int BuyerId { get; set; }

        [DataMember]
        public string BuyerFIO { get; set; }

        [DataMember]
        public int IngredientId { get; set; }

        [DataMember]
        public string IngredientName { get; set; }

        [DataMember]
        public int? KitchinerId { get; set; }

        [DataMember]
        public string KitchinerName { get; set; }

        [DataMember]
        public int Count { get; set; }

        [DataMember]
        public decimal Sum { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public string DateCreate { get; set; }

        [DataMember]
        public string DateCook { get; set; }
    }
}

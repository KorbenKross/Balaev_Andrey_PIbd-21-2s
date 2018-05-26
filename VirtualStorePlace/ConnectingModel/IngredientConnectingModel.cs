using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace VirtualStorePlace.ConnectingModel
{
    [DataContract]
    public class IngredientConnectingModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string IngredientName { get; set; }

        [DataMember]
        public decimal Value { get; set; }

        [DataMember]
        public List<IngredientElementConnectingModel> IngredientElement { get; set; }
    }
}

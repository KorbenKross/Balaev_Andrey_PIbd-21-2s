using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace VirtualStorePlace.UserViewModel
{
    [DataContract]
    public class IngredientUserViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string IngredientName { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public List<IngredientElementUserViewModel> IngredientElement { get; set; }
    }
}

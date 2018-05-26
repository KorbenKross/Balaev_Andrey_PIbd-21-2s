﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace VirtualStorePlace.UserViewModel
{
    [DataContract]
    public class IngredientElementUserViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int IngredientId { get; set; }

        [DataMember]
        public int ElementId { get; set; }

        [DataMember]
        public string ElementName { get; set; }

        [DataMember]
        public int Count { get; set; }
    }
}

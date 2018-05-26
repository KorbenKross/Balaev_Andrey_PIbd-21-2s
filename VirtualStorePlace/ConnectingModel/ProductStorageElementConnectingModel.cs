using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace VirtualStorePlace.ConnectingModel
{
    [DataContract]
    public class ProductStorageElementConnectingModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int ProductStorageId { get; set; }

        [DataMember]
        public int ElementId { get; set; }

        [DataMember]
        public int Count { get; set; }
    }
}

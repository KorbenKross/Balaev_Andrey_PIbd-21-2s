using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace VirtualStorePlace.ConnectingModel
{
    [DataContract]
    public class BuyerConnectingModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string BuyerFIO { get; set; }
    }
}

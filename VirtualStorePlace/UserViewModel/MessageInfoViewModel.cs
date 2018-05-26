using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace VirtualStorePlace.UserViewModel
{
    [DataContract]
    public class MessageInfoViewModel
    {
        [DataMember]
        public string MessageId { get; set; }

        [DataMember]
        public string BuyerName { get; set; }

        [DataMember]
        public DateTime DateDelivery { get; set; }

        [DataMember]
        public string Subject { get; set; }

        [DataMember]
        public string Body { get; set; }
    }
}

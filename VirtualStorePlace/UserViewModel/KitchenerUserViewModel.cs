using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace VirtualStorePlace.UserViewModel
{
    [DataContract]
    public class KitchenerUserViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string KitchenerFIO { get; set; }
    }
}

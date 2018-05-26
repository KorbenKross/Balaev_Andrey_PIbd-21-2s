using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace VirtualStorePlace.UserViewModel
{
    [DataContract]
    public class ProductStorageLoadUserViewModel
    {
        [DataMember]
        public string ProductStorageName { get; set; }

        [DataMember]
        public int TotalCount { get; set; }

        [DataMember]
        public IEnumerable<Tuple<string, int>> Elements { get; set; }
    }
}

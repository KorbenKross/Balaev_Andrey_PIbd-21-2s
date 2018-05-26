using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace VirtualStoreView
{
    [DataContract]
    public  class HttpErrorMessage
    {
        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string ExceptionMessage { get; set; }

        [DataMember]
        public string MessageDetail { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualStore
{
    public class MessageInfo
    {
        public int Id { get; set; }

        public string MessageId { get; set; }

        public string FromMailAddress { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public DateTime DateDelivery { get; set; }

        public int? BuyerId { get; set; }

        public virtual Buyer Buyer { get; set; }
    }
}

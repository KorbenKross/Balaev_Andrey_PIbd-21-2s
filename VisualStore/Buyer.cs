using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualStore
{
    public class Buyer
    {

        public int Id { get; set; }

        [Required]
        public string BuyerFIO { get; set; }

        public string Mail { get; set; }

        [ForeignKey("BuyerId")]
        public virtual List<CustomerSelection> CustomerSelections { get; set; }

        [ForeignKey("BuyerId")]
        public virtual List<MessageInfo> MessageInfos { get; set; }
    }
}

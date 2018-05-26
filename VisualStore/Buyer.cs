using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualStore
{
    public class Buyer
    {

        public int Id { get; set; }

        [Required]
        public string BuyerFIO { get; set; }

        [ForeignKey("BuyerId")]
        public virtual List<CustomerSelection> Orders { get; set; }
    }
}

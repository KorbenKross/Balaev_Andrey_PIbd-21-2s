using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualStore
{
    public class Kitchener
    {
        public int Id { get; set; }

        [Required]
        public string KitchenerFIO { get; set; }

        [ForeignKey("KitchenerId")]
        public virtual List<CustomerSelection> CustomerSelection { get; set; }
    }
}

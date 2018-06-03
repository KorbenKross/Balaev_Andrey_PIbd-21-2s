using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualStore
{
    public class Kitchener
    {
        public int Id { get; set; }

        [Required]
        public string KitchenerFIO { get; set; }

        [ForeignKey("KitchenerId")]
        public virtual List<CustomerSelection> CustomerSelections { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualStore
{
    public class Element
    {
        public int Id { get; set; }

        [Required]
        public string ElementName { get; set; }

        [ForeignKey("ElementId")]
        public virtual List<IngredientElement> ProductComponents { get; set; }

        [ForeignKey("ElementId")]
        public virtual List<IngredientElement> StockComponents { get; set; }
    }
}

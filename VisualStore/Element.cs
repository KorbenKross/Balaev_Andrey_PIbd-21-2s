using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace VirtualStore
{
    public class Element
    {
        public int Id { get; set; }

        [Required]
        public string ElementName { get; set; }

        [ForeignKey("ElementId")]
        public virtual List<IngredientElement> IngredientElements { get; set; }

        [ForeignKey("ElementId")]
        public virtual List<ProductStorageElement> ProductStorageElements { get; set; }
    }
}

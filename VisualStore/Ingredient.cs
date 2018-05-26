using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualStore
{
    public class Ingredient
    {
        public int Id { get; set; }

        [Required]
        public string IngredientName { get; set; }

        [Required]
        public decimal Cost { get; set; }

        [ForeignKey("IngredientId")]
        public virtual List<CustomerSelection> CustomerSelection { get; set; }

        [ForeignKey("IngredientId")]
        public virtual List<IngredientElement> IngredientElements { get; set; }
    }
}

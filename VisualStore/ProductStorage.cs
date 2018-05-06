using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualStore
{
    public class ProductStorage
    {
        public int Id { get; set; }

        [Required]
        public string ProductStorageName { get; set; }

        [ForeignKey("ProductStorageId")]
        public virtual List<ProductStorageElement> ProductStorageElement { get; set; }
    }
}

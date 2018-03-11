using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualStorePlace.UserViewModel
{
    public class ProductStorageUserViewModel
    {
        public int Id { get; set; }

        public string ProductStorageName { get; set; }

        public List<ProductStorageElementViewModel> ProductStorageElements { get; set; }
    }
}

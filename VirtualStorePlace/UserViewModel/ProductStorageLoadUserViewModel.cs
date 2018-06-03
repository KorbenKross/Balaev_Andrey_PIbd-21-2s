using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualStorePlace.UserViewModel
{
    public class ProductStorageLoadUserViewModel
    {
        public string ProductStorageName { get; set; }

        public int TotalCount { get; set; }

        public IEnumerable<Tuple<string, int>> Elements { get; set; }
    }
}

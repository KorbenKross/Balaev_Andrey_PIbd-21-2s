﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualStore
{
    public class ProductStorageElement
    {
        public int Id { get; set; }

        public int ProductStorageId { get; set; }

        public int ElementId { get; set; }

        public int Count { get; set; }

        public virtual ProductStorage ProductStorage { get; set; }

        public virtual Element Element { get; set; }

    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualStorePlace.UserViewModel
{
    public class IngredientElementUserViewModel
    {
        public int Id { get; set; }

        public int IngredientId { get; set; }

        public int ElementId { get; set; }

        public string ElementName { get; set; }

        public int Count { get; set; }
    }
}

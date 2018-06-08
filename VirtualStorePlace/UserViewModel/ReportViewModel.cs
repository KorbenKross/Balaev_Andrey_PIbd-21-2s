﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualStorePlace.UserViewModel
{
    public class ReportViewModel
    {
        public class StocksLoadViewModel
        {
            public string StockName { get; set; }

            public int TotalCount { get; set; }

            public IEnumerable<Tuple<string, int>> Components { get; set; }
        }

        public class ClientOrdersModel
        {
            public string ClientName { get; set; }

            public string DateCreate { get; set; }

            public string ProductName { get; set; }

            public int Count { get; set; }

            public decimal Sum { get; set; }

            public string Status { get; set; }
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualStorePlace.ConnectingModel;
using VirtualStorePlace.UserViewModel;

namespace VirtualStorePlace.LogicInterface
{
    public interface IReportService
    {

        void SaveProductPrice(ReportConnectingModel model);

        List<ProductStorageLoadUserViewModel> GetStocksLoad();

        void SaveStocksLoad(ReportConnectingModel model);

        List<BuyerOrderModel> GetClientOrders(ReportConnectingModel model);

        void SaveClientOrders(ReportConnectingModel model);
    }
}

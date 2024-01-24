using Models_Layer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bussiness_layer.Interface
{
    public interface IOrdersBussiness
    {
        public string OrderCreated(int User_Id, OrdersModel ordersModel);
    }
}

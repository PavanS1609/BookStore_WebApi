using Models_Layer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interface
{
    public interface IOrdersRepo
    {

        public string OrderCreated(int User_Id, OrdersModel ordersModel);
       
    }
}

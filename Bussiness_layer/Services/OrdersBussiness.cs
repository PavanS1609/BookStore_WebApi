using Bussiness_layer.Interface;
using Models_Layer;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bussiness_layer.Services
{
    public class OrdersBussiness:IOrdersBussiness
    {
        private readonly IOrdersRepo iordersRepo;
        public OrdersBussiness(IOrdersRepo iordersRepo) 
        {
            this.iordersRepo = iordersRepo;
        }
        public string OrderCreated(int User_Id,OrdersModel ordersModel)
        {
           return this.iordersRepo.OrderCreated(User_Id,ordersModel);
        }
    }
}

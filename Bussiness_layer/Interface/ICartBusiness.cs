using Models_Layer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bussiness_layer.Interface
{
    public interface ICartBusiness
    {
        public string AddToCart(int User_Id, int Book_Id);
        public IEnumerable<CartModel> CartList();

        public string DeleteCart(int Cart_Id);
    }
}

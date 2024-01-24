using Bussiness_layer.Interface;
using Models_Layer;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bussiness_layer.Services
{
    public class CartBusiness:ICartBusiness
    {
        private readonly ICartRepo icartRepo;

        public CartBusiness(ICartRepo icartRepo)
        {
            this.icartRepo = icartRepo;
        }
        public string AddToCart(int User_Id, int Book_Id)
        {
            return icartRepo .AddToCart(User_Id, Book_Id);
        }

        public IEnumerable<CartModel> CartList()
        {
            return icartRepo.CartList();

        }

        public string DeleteCart(int Cart_Id)
        {
            return icartRepo.DeleteCart(Cart_Id);

        }
    }
}

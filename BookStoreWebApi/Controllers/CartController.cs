using Bussiness_layer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models_Layer;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace BookStoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartBusiness icartBusiness;
        public CartController(ICartBusiness icartBusiness)
        {
            this.icartBusiness = icartBusiness;
        }

        [Authorize]
        [HttpPost("Cart_Register")]
        public IActionResult Cart_Register( int Book_Id)
        {
            int User_Id=Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type== "User_Id").Value);
            try
            {
                var result = icartBusiness.AddToCart(User_Id, Book_Id);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<string> { Status = true, Message = "Cart Registration successfull", Data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Cart Registration unsuccessufull" });
                }
            }

            catch (Exception ex)  
            {
                return this.BadRequest(new { success = false, message = ex.Message });

            } 

        }

        [Authorize] 
        [HttpGet]
        [Route("CartList")] 
        public IActionResult CartList() 
        {
            List<CartModel> cartList = (List<CartModel>)icartBusiness.CartList();
            if (cartList != null)
            {
                return Ok(new ResponseModel<List<CartModel>> { Status = true, Message = "CartList", Data = cartList });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { Status = false, Message = "Cart not exists." });
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("DeleteCart")]
        public IActionResult DeleteCart(int Cart_Id)
        {
            var result = icartBusiness.DeleteCart(Cart_Id);
            if (result != null)
            {
                return Ok(new ResponseModel<string> { Status = true, Message = "Cart deleted successfully." });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { Status = false, Message = "Cart deletion failed." });
            }
        }

    }
}

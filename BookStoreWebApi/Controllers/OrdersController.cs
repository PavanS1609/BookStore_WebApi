using Bussiness_layer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models_Layer;
using System.Linq;
using System;

namespace BookStoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersBussiness iordersBussiness;
        public OrdersController(IOrdersBussiness iordersBussiness)
        {
            this.iordersBussiness = iordersBussiness;
        }

        [Authorize]
        [HttpPost("Orders")]
        public IActionResult Orders(OrdersModel ordersModel)
        {
            int User_Id = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "User_Id").Value);
            try
            {
                var result = iordersBussiness.OrderCreated(User_Id, ordersModel);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<string> { Status = true, Message = "order Registration successfull", Data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "order Registration unsuccessufull" });
                }
            }

            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });

            }

        }
    }
}

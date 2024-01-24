using Bussiness_layer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models_Layer;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace BookStoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressBusiness iAddressBusiness;
        public AddressController(IAddressBusiness iAddressBusiness)
        {
            this.iAddressBusiness = iAddressBusiness;
        }

        [Authorize]
        [HttpPost("Address_Register")]
        public IActionResult Address_Register( AddressModel addressModel)
        {
            int User_Id = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "User_Id").Value);
            try
            {
                var result = iAddressBusiness.AddAddress(User_Id, addressModel);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<string> { Status = true, Message = "Address Registration successfull", Data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Address Registration unsuccessufull" });
                }
            }

            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });

            }

        }

        [HttpGet]
        [Route("AddressList")]
        public IActionResult AddressList()
        {
            List<AddressModel> addressList = (List<AddressModel>)iAddressBusiness.AddressList();
            if (addressList != null)
            {
                return Ok(new ResponseModel<List<AddressModel>> { Status = true, Message = "AddressList", Data = addressList });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { Status = false, Message = "Address not exists." });
            }
        }


        [HttpPut]
        [Route("UpdateAddress")]
        public IActionResult UpdateAddress(int Address_Id)
        {
            var result = iAddressBusiness.UpdateAddresss(Address_Id);

            if (result != null)
            {
                return Ok(new ResponseModel<string> { Status = true, Message = "Address Updated successfully." });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { Status = false, Message = "Address Updation failed." });
            }
        }


        [HttpDelete]
        [Route("DeleteAddress")]
        public IActionResult DeleteAddress(int Address_Id)
        {
            var result = iAddressBusiness.UpdateAddresss(Address_Id);
            if (result != null)
            {
                return Ok(new ResponseModel<string> { Status = true, Message = "Address deleted successfully." });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { Status = false, Message = "Address deletion failed." });
            }
        }
    }
}

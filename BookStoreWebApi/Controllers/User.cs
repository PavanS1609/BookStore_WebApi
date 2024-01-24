using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models_Layer;
using Bussiness_layer.Interface;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace BookStoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class User : ControllerBase
    {
        private readonly IUserBusiness iuserBusiness;

        public User(IUserBusiness iuserBusiness)
        {
            this.iuserBusiness = iuserBusiness;
        }
        [HttpPost("Register")]
        public IActionResult Register(UserModel userModel)
        {
            try
            {
                var result = iuserBusiness.UserRegistration(userModel);
                if (result != null)
                {

                    return this.Ok(new ResponseModel<string> { Status = true, Message = "login successfully with token", Data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Error" });

                }
            }
            catch (Exception ex)
            {

                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }


        [HttpPost("login-with-jwt")]
        public IActionResult LoginWithJwt(UserLogin userLogin)
        {
            try
            {
                var result = iuserBusiness.UserLogin(userLogin);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<string> { Status = true, Message = "login successfully with token", Data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Error" });
                }
            }
            catch (Exception ex)
            {

                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }




        [HttpPost("Forget-Password")]
        public IActionResult ForgetPassword(string Email_Id)
        {
            try
            {
                string result = iuserBusiness.ForgetPassword(Email_Id);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<string> { Status = true, Message = "forget password" ,Data= result});
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Error" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [Authorize]
        [HttpPut("reset-password")]
        public IActionResult resetpassword(string Password, string ConfirmPassword)
        {
            try
            {
                string Email_Id=User.Claims.FirstOrDefault(x =>x.Type == "Email_Id").Value;
                var result = iuserBusiness.resetPassword(Email_Id, Password, ConfirmPassword);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<string> { Status = true, Message = "reset password" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "error" });
                }
            }
            catch (Exception ex)
            {

                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }


            [HttpPut("Update-User")]
        public IActionResult UpdateUser( UserModel userModel)
        {
            try
            {
                string result = iuserBusiness.UpdateUser(userModel);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<UserModel> { Status = true, Message = "updated user successfully" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, message = "unable to update user" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }


        [HttpPut("UpdateUserorcreate")]
        public IActionResult UpdateUserorcreate(int User_Id,UserModel userModel)
        {
            string result =iuserBusiness.updateCreate(User_Id,userModel);
            if (result != null)
            {
                return this.Ok(new ResponseModel<UserModel> { Status = true, Message = "updated or created user successfully" });
            }
            else
            {
                return this.BadRequest(new { Status = false, message = "unable to update user" });
            }

        }




    }
}

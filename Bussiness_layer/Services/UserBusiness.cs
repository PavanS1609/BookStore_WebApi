using Bussiness_layer.Interface;
using Models_Layer;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bussiness_layer.Services
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserRepo iuserRepo;
        public UserBusiness(IUserRepo iuserRepo )
        {
            this.iuserRepo = iuserRepo;
        }

        public string UserRegistration(UserModel userModel)
        {
            return iuserRepo.UserRegistration(userModel);

        }
        public string UserLogin(UserLogin userLogin)
        {
            return iuserRepo.UserLogin(userLogin);
        }
        public string UpdateUser(UserModel userModel)
        {
            return iuserRepo.UpdateUser(userModel);

        }
        public string DeleteUser(int User_Id)
        {
            return iuserRepo.DeleteUser(User_Id);
        }
        public string ForgetPassword(string Email_Id)
        {
            return iuserRepo.ForgetPassword(Email_Id);

        }
        public string resetPassword(string Email_Id, string Password, string ConfirmPassword)
        {
            return iuserRepo.resetpassword(Email_Id, Password, ConfirmPassword);

        }
        public string updateCreate(int User_Id, UserModel userModel)
        {
          return iuserRepo.updateCreate(User_Id,userModel);
        }
    }
}

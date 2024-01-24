using Models_Layer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interface
{
    public interface IUserRepo
    {
        public string UserRegistration(UserModel userModel);
        public string UserLogin(UserLogin userLogin);
        public string UpdateUser(UserModel userModel);
        public string DeleteUser(int User_Id);
        public string ForgetPassword(string Email_Id);

        public string resetpassword(string Email_Id, string Password, string Confirmpassword);

        public string updateCreate(int User_Id,UserModel userModel);




    }
}

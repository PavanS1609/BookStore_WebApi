using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models_Layer;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace Repository_Layer.Service
{
    public class UserRepo : IUserRepo
    {

        private readonly IConfiguration configuration;

        public UserRepo(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string UserRegistration(UserModel userModel)
        { 
            if (userModel != null) 
            { 
            
                using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("BookStoreApI")))
                {
                    SqlCommand cmd = new SqlCommand("spUserRegistration", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FullName", userModel.FullName);
                    cmd.Parameters.AddWithValue("@MobileNumber", userModel.MobileNumber);
                    cmd.Parameters.AddWithValue("@Email_Id", userModel.Email_Id);
                    cmd.Parameters.AddWithValue("@Password",EncryptPass(userModel.Password));
                    cmd.Parameters.AddWithValue("@Address", userModel.Address);
                    cmd.Parameters.AddWithValue("@City", userModel.City);
                    cmd.Parameters.AddWithValue("@State", userModel.State);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                }
                return "User Registration Succefull";
            }
            else
            {
                return null;
            }
            
        }

        public string UserLogin(UserLogin userLogin)
        {
            string EncodedPassword = EncryptPass(userLogin.Password);
            if (userLogin != null)
            {
                UserModel userModel = new UserModel();
                using (SqlConnection con = new SqlConnection(configuration["ConnectionStrings:BookStoreApI"]))
                {
                    SqlCommand cmd = new SqlCommand("spUserLogin", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email_Id", userLogin.Email_Id);
                    cmd.Parameters.AddWithValue("@Password", EncodedPassword);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows == true)
                    {
                        while (rdr.Read())
                        {
                            userModel.User_Id = rdr.GetInt32("User_Id");
                            userModel.FullName = rdr["FullName"].ToString();
                            userModel.MobileNumber = rdr.GetInt64("MobileNumber");
                            userModel.Email_Id = rdr["Email_Id"].ToString();
                            userModel.Password = rdr["Password"].ToString();
                            userModel.Address = rdr["Address"].ToString();
                            userModel.City = rdr["City"].ToString();
                            userModel.State = rdr["State"].ToString();
                           // userModel.City = rdr["City"].ToString();
                        }

                        var token = this.GenerateToken(userModel.Email_Id, userModel.User_Id);
                        return token;
                    }
                    else
                    { 
                        return null; 
                    }
                }
              
            }
            else
            {
                return null;
            }
        }
        public string UpdateUser(UserModel userModel)
        {
            using (SqlConnection con = new SqlConnection(configuration["ConnectionStrings:BookStoreApI"]))
            {
                SqlCommand cmd = new SqlCommand("spUpdateEmployeeDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@User_id", userModel.User_Id);
                cmd.Parameters.AddWithValue("@FullName", userModel.FullName);
                cmd.Parameters.AddWithValue("@MobileNumber", userModel.MobileNumber);
                cmd.Parameters.AddWithValue("@Password", userModel.Password);
                cmd.Parameters.AddWithValue("@State", userModel.State);
                cmd.Parameters.AddWithValue("@City", userModel.City);
                cmd.Parameters.AddWithValue("@Address", userModel.Address);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return "Details updated Successfully";
            
            }
        } 

        public string DeleteUser(int User_Id)
        {
            if (User_Id != 0)
            {
                using (SqlConnection con = new SqlConnection(configuration["ConnectionStrings:BookStoreApI"]))
                {
                    SqlCommand cmd = new SqlCommand("spDeleteUser", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@User_Id", User_Id);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();


                }
                return "Employee Details found & deleted";
            }
            else
            {
                return null;
            }
        }

        public string GenerateToken(string Email_Id, int User_Id)
        { 
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
               new Claim("Email_Id", Email_Id),
               new Claim("User_Id", User_Id.ToString())
            };
            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public string ForgetPassword(string Email_Id)
        {
            if (Email_Id != null)
            {
                UserModel userModel = new UserModel();
                using (SqlConnection con = new SqlConnection(configuration["ConnectionStrings:BookStoreApI"]))
                {
                    SqlCommand cmd = new SqlCommand("spForgotPassword", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email_Id", Email_Id);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows == true)
                    {
                        while (rdr.Read())
                        {
                            userModel.User_Id = rdr.GetInt32("User_Id");
                             userModel.FullName = rdr["FullName"].ToString();
                            // userModel.MobileNumber = Convert.ToInt32(rdr["MobileNumber"]);
                            userModel.Email_Id = rdr["Email_Id"].ToString();
                            userModel.Password = rdr["Password"].ToString();
                            //userModel.Address = rdr["Address"].ToString();
                            //userModel.City = rdr["City"].ToString();
                            //userModel.State = rdr["State"].ToString();
                            //userModel.City = rdr["City"].ToString();
                        }
                        var token = this.GenerateToken(userModel.Email_Id, userModel.User_Id);
                        MSMQ_Model mSMQModel = new MSMQ_Model();
                        mSMQModel.SendMessage(token, userModel.Email_Id, userModel.FullName);
                        return token.ToString();
                    }
                    else
                    {
                        return null;
                    }
                }

            }
            else
            {
                return null;
            }
        }
        public static string EncryptPass(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }


        public string resetpassword(string Email_Id, string Password, string Confirmpassword)
        {
            var password = EncryptPass(Password);
            var confirmPassword = EncryptPass(Confirmpassword);
            try
            {
                if (password.Equals(confirmPassword))
                {
                    using (SqlConnection con = new SqlConnection(configuration["ConnectionStrings:BookStoreApI"]))
                    {
                        SqlCommand cmd = new SqlCommand("uspResetNewPwd", con);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Email_Id", Email_Id);
                        cmd.Parameters.AddWithValue("@Password", password);
                        cmd.Parameters.AddWithValue("@Confirmpassword", confirmPassword);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    return "Password resetted Successfully..........";
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }


        }


        public string updateCreate(int User_Id,UserModel userModel)
        {
            if(userModel.User_Id == null)
            {
                using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("BookStoreApI")))
                {
                    SqlCommand cmd = new SqlCommand("spUserRegistration", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FullName", userModel.FullName);
                    cmd.Parameters.AddWithValue("@MobileNumber", userModel.MobileNumber);
                    cmd.Parameters.AddWithValue("@Email_Id", userModel.Email_Id);
                    cmd.Parameters.AddWithValue("@Password", EncryptPass(userModel.Password));
                    cmd.Parameters.AddWithValue("@Address", userModel.Address);
                    cmd.Parameters.AddWithValue("@City", userModel.City);
                    cmd.Parameters.AddWithValue("@State", userModel.State);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                }
                return "User Registration Succefull";
            }
            else
            {
                using (SqlConnection con = new SqlConnection(configuration["ConnectionStrings:BookStoreApI"]))
                {
                    SqlCommand cmd = new SqlCommand("spUserUpdate", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@User_id", userModel.User_Id);
                    cmd.Parameters.AddWithValue("@Email_Id", userModel.Email_Id);
                    cmd.Parameters.AddWithValue("@FullName", userModel.FullName);
                    cmd.Parameters.AddWithValue("@MobileNumber", userModel.MobileNumber);
                    cmd.Parameters.AddWithValue("@Password", userModel.Password);
                    cmd.Parameters.AddWithValue("@State", userModel.State);
                    cmd.Parameters.AddWithValue("@City", userModel.City);
                    cmd.Parameters.AddWithValue("@Address", userModel.Address);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return "Details updated Successfully";

                }
            }
            return "Inserted";

        }



    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Microsoft.Extensions.Configuration;
using Repository_Layer.Interface;
using Models_Layer;

namespace Repository_Layer.Service
{
    public class CartRepo :ICartRepo
    {
        private readonly IConfiguration configuration;
         
        public CartRepo(IConfiguration configuration)
        {
            this.configuration = configuration; 
        }
        public string AddToCart(int User_Id, int Book_Id)
         {
            if (User_Id > 0 && Book_Id > 1)
            {

                using (SqlConnection con = new SqlConnection(configuration["ConnectionStrings:BookStoreApI"]))
                {
                    SqlCommand cmd = new SqlCommand("spAddToCart", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@User_Id", User_Id);
                    cmd.Parameters.AddWithValue("@Book_Id", Book_Id);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return "Added to Cart Successfully..................";
            }
            else
            {
                return null;
            }

        }

        public IEnumerable<CartModel> CartList()
        {
            List<CartModel> cartlist = new List<CartModel>();

            using (SqlConnection con = new SqlConnection(configuration["ConnectionStrings:BookStoreApI"]))
            {
                SqlCommand cmd = new SqlCommand("spCartList", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {

                    CartModel cart = new CartModel();
                    cart.Cart_Id = rdr.GetInt32("Cart_Id");
                    cart.User_Id = rdr.GetInt32("User_Id");
                    cart.Book_Id = rdr.GetInt32("Book_Id");
                    cart.Book_Name = rdr["Book_Name"].ToString();
                    cart.Author_Name = rdr["Author_Name"].ToString();
                    cart.Book_Price = Convert.ToSingle(rdr["Book_Price"]);
                    cart.Quantity = rdr.GetInt32("Quantity");
                    cart.Book_Image = rdr["Book_Image"].ToString();
                    cartlist.Add(cart);

                }
            }
            return cartlist;
        }


        public string DeleteCart(int Cart_Id)
        {
                if (Cart_Id != 0)
                {

                using (SqlConnection con = new SqlConnection(configuration["ConnectionStrings:BookStoreApI"]))
                {
                        SqlCommand cmd = new SqlCommand("spDeleteCart", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Cart_Id", Cart_Id);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    return "Cart_Id Is Deleted Successfully......................";
                }
                else
                {
                    return null;
                }
            
        }
    }
}

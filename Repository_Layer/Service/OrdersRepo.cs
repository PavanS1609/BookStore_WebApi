using Microsoft.Extensions.Configuration;
using Models_Layer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Repository_Layer.Interface;

namespace Repository_Layer.Service
{
    public class OrdersRepo:IOrdersRepo
    {

        private readonly IConfiguration configuration;

        public OrdersRepo(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string OrderCreated (int User_Id, OrdersModel ordersModel)
        {
            if (User_Id != null)
            {
                using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("BookStoreApI")))
                {
                    SqlCommand cmd = new SqlCommand("spOrdered_Details", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@User_Id", User_Id);
                    cmd.Parameters.AddWithValue("@Cart_Id", ordersModel.Cart_Id);
                    cmd.Parameters.AddWithValue("@Book_Id", ordersModel.Book_Id);
                    cmd.Parameters.AddWithValue("@Price", ordersModel.Price);
                    cmd.Parameters.AddWithValue("@Quantity", ordersModel.Quantity);
                    cmd.Parameters.AddWithValue("@Book_Name", ordersModel.Book_Name);
                    cmd.Parameters.AddWithValue("@Ordered_At", DateTime.Now);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                }
                return "OrderCreated Successfully";
            }
            else
            {
                return null;
            }
        }
    }
}

using Microsoft.Extensions.Configuration;
using Models_Layer;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace Repository_Layer.Service
{
    public class AddressRepo:IAddressRepo
    {
        private readonly IConfiguration configuration;
            public AddressRepo(IConfiguration configuration)
            {
                this.configuration = configuration;
            }
       
        public string AddAddress(int User_Id,AddressModel addressModel)
        {
            if (addressModel != null)
            {

                using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("BookStoreApI")))
                {
                    SqlCommand cmd = new SqlCommand("spCustomerDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@User_Id", User_Id);
                    cmd.Parameters.AddWithValue("@Customer_Name", addressModel.Customer_Name);
                    cmd.Parameters.AddWithValue("@Customer_Number", addressModel.Customer_Number);
                    cmd.Parameters.AddWithValue("@Address", addressModel.Address);
                    cmd.Parameters.AddWithValue("@State", addressModel.State);
                    cmd.Parameters.AddWithValue("@City", addressModel.City);
                 
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                }
                return "Address Registration Succefull";
            }
            else
            {
                return null;
            }
        }


        public IEnumerable<AddressModel> AddressList()
        {
            List<AddressModel> addressList = new List<AddressModel>();

            using (SqlConnection con = new SqlConnection(configuration["ConnectionStrings:BookStoreApI"]))

            {
                SqlCommand cmd = new SqlCommand("spCustomerAddressList", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    AddressModel address = new AddressModel();
                    address.Customer_Name= rdr["Customer_Name"].ToString();
                    address.Customer_Number = rdr.GetInt32("Customer_Number");
                    address.Address = rdr["Address"].ToString();
                    address.State = rdr["State"].ToString();
                    address.City = rdr["City"].ToString();
                
                    addressList.Add(address);
                }
                con.Close();
            }
            return addressList;
        }


        public string DeleteAddress(int Address_Id)
        {
            if (Address_Id != 0)
            {

                using (SqlConnection con = new SqlConnection(configuration["ConnectionStrings:BookStoreApI"]))
                {
                    SqlCommand cmd = new SqlCommand("spDeleteAddress", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Address_Id", Address_Id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return "Address Is Deleted Successfully......................";
            }
            else
            {
                return null;
            }
        }

        public AddressModel UpdateAddresss(int Address_Id)
        {
            if (Address_Id != 0)
            {
                AddressModel addressModel = new AddressModel();


                using (SqlConnection con = new SqlConnection(configuration["ConnectionStrings:BookStoreApI"]))
                {
                    SqlCommand cmd = new SqlCommand("spUpdateCustomerAddress", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Address_Id", Address_Id);
                   cmd.Parameters.AddWithValue("@Customer_Name", addressModel.Customer_Name);
                    cmd.Parameters.AddWithValue("@Customer_Number", addressModel.Customer_Number);
                    cmd.Parameters.AddWithValue("@Address", addressModel.Address);
                    cmd.Parameters.AddWithValue("@City", addressModel.City);
                    cmd.Parameters.AddWithValue("@State", addressModel.State);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return addressModel;
            }
            else
            {
                return null;
            }
        }
        
    }
}

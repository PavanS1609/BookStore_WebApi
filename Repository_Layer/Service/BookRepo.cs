using Microsoft.Extensions.Configuration;
using Models_Layer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Repository_Layer.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Principal;
using Microsoft.AspNetCore.Http;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace Repository_Layer.Service
{
    public class BookRepo:IBookRepo
    {
        private readonly IConfiguration configuration;
        public BookRepo(IConfiguration configuration) 
        {
            this.configuration = configuration;
        }

        public AddBookModel AddBook(AddBookModel book)
        {
            using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("BookStoreApI")))
            {
                SqlCommand cmd = new SqlCommand("spAddBook", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Book_Name", book.Book_Name);
                cmd.Parameters.AddWithValue("@Author_Name", book.Author_Name);
                cmd.Parameters.AddWithValue("@Book_Details", book.Book_Details);
                cmd.Parameters.AddWithValue("@Book_Image", book.Book_Image);
                cmd.Parameters.AddWithValue("@Book_Price", book.Book_Price);
                cmd.Parameters.AddWithValue("@Quantity", book.Quantity);
                cmd.Parameters.AddWithValue("@Book_Rating", book.Book_Rating);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
            return book;
        }
            
        public IEnumerable<AddBookModel> GetBookLIst()
        {
            List<AddBookModel> booklist = new List<AddBookModel>();

            using (SqlConnection con = new SqlConnection(configuration["ConnectionStrings:BookStoreApI"]))
            {
                SqlCommand cmd = new SqlCommand("spBooksList", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    AddBookModel book = new AddBookModel();
                    book.Book_Id = rdr.GetInt32("Book_Id");
                    book.Book_Name = rdr["Book_Name"].ToString();
                    book.Author_Name = rdr["Author_Name"].ToString();
                    book.Book_Details = rdr["Book_Details"].ToString();
                    book.Book_Price = Convert.ToSingle(rdr["Book_Price"]);
                    book.Book_Rating = Convert.ToSingle(rdr["Book_Rating"]);
                    book.Quantity = rdr.GetInt32("Quantity");
                    book.Book_Image = rdr["Book_Image"].ToString();
                    booklist.Add(book);
                }
                con.Close();
            }
            return booklist;
        }



        public string UpdateBook(AddBookModel book)
        {
            if (book != null)
            {
                using (SqlConnection con = new SqlConnection(configuration["ConnectionStrings:BookStoreApI"]))
                {
                    SqlCommand cmd = new SqlCommand("spUpdateBook", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Book_Id", book.Book_Id);
                    cmd.Parameters.AddWithValue("@Book_Name", book.Book_Name);
                    cmd.Parameters.AddWithValue("@Author_Name", book.Author_Name);
                    cmd.Parameters.AddWithValue("@Book_Details", book.Book_Details);
                    cmd.Parameters.AddWithValue("@Book_Image", book.Book_Image);
                    cmd.Parameters.AddWithValue("@Book_Price", book.Book_Price);
                    cmd.Parameters.AddWithValue("@Book_Rating", book.Book_Rating);
                    cmd.Parameters.AddWithValue("@Quantity", book.Quantity);
                  

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return "Book is Updated Successfully..........";
            }
            else
            {
                return null;
            }
        }


        public string DeleteBook(int Book_Id)
        {
            if (Book_Id >= 1)
            {
                using (SqlConnection con = new SqlConnection(configuration["ConnectionStrings:BookStoreApI"]))
                {
                    SqlCommand cmd = new SqlCommand("spDeleteBook", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Book_Id", Book_Id);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return "Book Is Deleted Successfully......................";
            }
            else
            {
                return null;
            }
        }


        ///search book by author name

        public bool Search(string Author_Name) 
        {
            if (Author_Name != null)
            {
                using (SqlConnection con = new SqlConnection(configuration["ConnectionStrings:BookStoreApI"]))
                {
                    SqlCommand cmd = new SqlCommand("Search", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Author_Name", Author_Name);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return true;
            }
            else
            {
                return false;
            }


        } 


        public string AddBookImage(int Book_Id, IFormFile formFile)
        {
            if (Book_Id <= 0 || formFile == null || formFile.Length == 0)
            {
                return "Invalid book ID or empty image file.";
            }

            try
            {
                Account acc = new Account(
                    this.configuration["CloudinarySettings:CloudName"],
                    this.configuration["CloudinarySettings:ApiKey"],
                    this.configuration["CloudinarySettings:ApiSecret"]);

                Cloudinary cloudinary = new Cloudinary(acc);

                using (var stream = formFile.OpenReadStream())
                {
                    var ulP = new ImageUploadParams()
                    {
                        File = new FileDescription(formFile.FileName, stream),
                    };

                    var uploadResult = cloudinary.Upload(ulP);
                    string imagepath = uploadResult.Url.ToString();

                    if (!string.IsNullOrEmpty(imagepath))
                    {
                        using (SqlConnection con = new SqlConnection(this.configuration["ConnectionStrings:BookStoreApI"]))
                        {
                            SqlCommand cmd = new SqlCommand("spAddImage", con);
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@Book_Id", Book_Id);
                            cmd.Parameters.AddWithValue("@Book_Image", imagepath);

                            con.Open();
                            cmd.ExecuteNonQuery();
                        }
                        return "Image Uploaded Successfully";
                    }
                    else
                    {
                        return "Failed to upload image to Cloudinary.";
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return "An error occurred while uploading the image: " + ex.Message;
            }
        }

    }
}

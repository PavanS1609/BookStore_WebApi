using Bussiness_layer.Interface;
using Microsoft.AspNetCore.Http;
using Models_Layer;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bussiness_layer.Services
{
    public class BookBusiness : IBookBusiness
    {
        private readonly IBookRepo ibookRepo;
        public BookBusiness(IBookRepo ibookRepo) 
        { 
            this.ibookRepo = ibookRepo; 

        }
        public AddBookModel AddBook(AddBookModel book)
        {
            return ibookRepo.AddBook(book);
        }

        public IEnumerable<AddBookModel> GetBookLIst()
        {
            return ibookRepo.GetBookLIst();

        }
        public string UpdateBook(AddBookModel book)
        {
            return ibookRepo.UpdateBook(book);
        }

        public string DeleteBook(int Book_id)
        {
            return ibookRepo.DeleteBook(Book_id);
        }
        public bool Search(string Author_Name)
        {
            return ibookRepo.Search(Author_Name);
        }
        public string AddBookImage(int Book_Id, IFormFile formFile)
        {
            return ibookRepo.AddBookImage(Book_Id, formFile);   
        }
    }
}

using Microsoft.AspNetCore.Http;
using Models_Layer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interface
{
    public interface IBookRepo
    {
        public AddBookModel AddBook(AddBookModel book);
        public IEnumerable<AddBookModel> GetBookLIst();
        public string UpdateBook(AddBookModel book);

        public string DeleteBook(int Book_id);
        public bool Search(string Author_Name);

        public string AddBookImage(int Book_Id, IFormFile formFile);
    }
}

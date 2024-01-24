using Bussiness_layer.Interface;
using Bussiness_layer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models_Layer;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;

namespace BookStoreWebApi.Controllers
{  
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookBusiness ibookBusiness;
        public BookController(IBookBusiness ibookBusiness)
        {
            this.ibookBusiness = ibookBusiness;
        }


        [HttpPost("Book_Register")]
        public IActionResult Book_Register(AddBookModel addBookModel)
        {
            try
            {
                var result = ibookBusiness.AddBook(addBookModel);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<AddBookModel> { Status = true, Message = "Book Registration successfull", Data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Book Registration unsuccessufull" });
                }
            }

            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });

            }

        }

        [HttpGet]
        [Route("BooksList")]
        public IActionResult BooksList()
        {
            List<AddBookModel> allBooks = (List<AddBookModel>)ibookBusiness.GetBookLIst();
            if (allBooks != null)
            {
                return Ok(new ResponseModel<List<AddBookModel>> { Status = true, Message = "All Books", Data = allBooks });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { Status = false, Message = "Books not exists." });
            }
        }


        [HttpPut]
        [Route("UpdateBook")]
        public IActionResult UpdateBook(AddBookModel book)
        {
            var result = ibookBusiness.UpdateBook(book);

            if (result != null)
            {
                return Ok(new ResponseModel<string> { Status = true, Message = "Book Updated successfully." });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { Status = false, Message = "Book Updation failed." });
            }
        }


        [HttpDelete]
        [Route("DeleteBook")]
        public IActionResult DeleteBook(int id)
        {
            var result = ibookBusiness.DeleteBook(id);
            if (result != null)
            {
                return Ok(new ResponseModel<string> { Status = true, Message = "Book deleted successfully." });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { Status = false, Message = "Book deletion failed." });
            }
        }

        [HttpGet]
        [Route("GetBook")]
        public IActionResult GetBook(string Author_Name)
        {
            var result = ibookBusiness.Search(Author_Name);
            if (result != null)
            {
                return Ok(new ResponseModel<string> { Status = true, Message = "Book found successfully." });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { Status = false, Message = "Book finding failed." });
            }
        }

        [HttpPost]
        [Route("GetBook")]
        public IActionResult AddImage(int Book_Id,IFormFile formFile)
        {
            var result=ibookBusiness.AddBookImage(Book_Id, formFile);
            if(result != null)
            {
                return Ok(new ResponseModel<string> { Status = true, Message = "Image added successfully." });
            }
            else
            {

                return BadRequest(new ResponseModel<string> { Status = false, Message = "Image uploading failed." });
            }
        }

        

    }
}

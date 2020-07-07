using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksManagement.Business.Managers;
using BooksManagement.Repositories.Interface;
using BooksManagement.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BooksManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BooksController : ControllerBase
    {
        private readonly IBooksRepository _booksRepository;

        public BooksController(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        // GET: api/Books
        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<BooksObj> Index()
        {
            return BooksManager.GetAllBooks(_booksRepository);
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public BooksObj Get(string id)
        {
            return BooksManager.GetBookById(_booksRepository, id);
        }

        // POST: api/Books
        [HttpPost]
        [AllowAnonymous]
        public BooksObj Post(BooksObj newBook)
        {
            return BooksManager.AddBook(_booksRepository, newBook);
        }

        // PUT: api/Books/5
        [HttpPut("{id}")]
        public BooksObj Put(string id, BooksObj book)
        {
            return BooksManager.UpdateBook(_booksRepository, id, book);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BooksObj>> Delete(string id)
        {
            BooksObj book = await BooksManager.DeleteBook(_booksRepository, id);
            
            if (book == null)
                return NotFound();

            return book;
        }
    }
}

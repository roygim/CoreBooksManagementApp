using BooksManagement.Repositories.Interface;
using BooksSQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksManagement.Models;

namespace BooksManagement.Repositories
{
    public class BooksMockRepository : IBooksRepository
    {
        private List<BooksObj> _booksList;

        public BooksMockRepository()
        {
            _booksList = new List<BooksObj> 
            { 
                new BooksObj() { Id = "1", Name = "mock_aaa" }, 
                new BooksObj() { Id = "2", Name = "mock_bbb" } 
            };
        }

        public BooksObj AddBook(BooksObj newBook)
        {
            throw new NotImplementedException();
        }

        public async Task<BooksObj> DeleteBook(string id)
        {
            BooksObj book = _booksList.Find(b => b.Id == id);
            
            if (book == null)
            {
                return null;
            }

            _booksList.Remove(book);

            return book;
        }

        public IEnumerable<BooksObj> GetAllBooks()
        {
            return _booksList;
        }

        public BooksObj GetBookById(string Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BooksObj> GetStoredProcedureExample(BooksObj book)
        {
            throw new NotImplementedException();
        }

        public BooksObj UpdateBook(string id, BooksObj book)
        {
            throw new NotImplementedException();
        }
    }
}

using BooksManagement.Repositories.Interface;
using BooksSQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksManagement.Repositories
{
    public class BooksMockRepository : IBooksRepository
    {
        private List<Books> _booksList;

        public BooksMockRepository()
        {
            _booksList = new List<Books> 
            { 
                new Books() { Id = 1, Name = "mock_aaa" }, 
                new Books() { Id = 2, Name = "mock_bbb" } 
            };
        }

        public Books AddBook(Books newBook)
        {
            throw new NotImplementedException();
        }

        public async Task<Books> DeleteBook(int id)
        {
            Books book = _booksList.Find(b => b.Id == id);
            
            if (book == null)
            {
                return null;
            }

            _booksList.Remove(book);

            return book;
        }

        public IEnumerable<Books> GetAllBooks()
        {
            return _booksList;
        }

        public Books GetBookById(int Id)
        {
            throw new NotImplementedException();
        }

        public Books UpdateBook(int id, Books book)
        {
            throw new NotImplementedException();
        }
    }
}

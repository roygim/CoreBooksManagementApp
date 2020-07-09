using BooksManagement.Repositories.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using BooksManagement.Models;
using BooksMongoDB.DataService;
using BooksMongoDB.Models;

namespace BooksManagement.Repositories
{
    public class BooksMongoDBRepository : IBooksRepository
    {
        private readonly BooksMongoDBService _booksService;
        
        public BooksMongoDBRepository(BooksMongoDBService booksService)
        {
            _booksService = booksService;
        }

        public IEnumerable<BooksObj> GetAllBooks()
        {
            List<BooksObj> books = new List<BooksObj>();

            IEnumerable<Books> sqlBooks = _booksService.Get();

            foreach (Books b in sqlBooks)
            {
                books.Add(GetBooksObj(b));
            }

            return books;
        }

        public BooksObj GetBookById(string Id)
        {
            return null;
        }

        public BooksObj AddBook(BooksObj newBook)
        {
            return null;
        }

        public BooksObj UpdateBook(string id, BooksObj book)
        {
            return null;
        }

        public async Task<BooksObj> DeleteBook(string id)
        {
            return null;
        }
        
        private Books GetSqlBooks(BooksObj book)
        {
            Books sqlBook = new Books();

            if(!string.IsNullOrEmpty(book.Id))
                sqlBook.Id = book.Id;

            sqlBook.Name = book.Name;
            sqlBook.Year = book.Year.Value;
            sqlBook.Author = book.Author;

            return sqlBook;
        }

        private BooksObj GetBooksObj(Books book)
        {
            BooksObj booksObj = new BooksObj();

            booksObj.Id = book.Id.ToString();
            booksObj.Name = book.Name;
            booksObj.Year = book.Year;
            booksObj.Author = book.Author;

            return booksObj;
        }
    }
}

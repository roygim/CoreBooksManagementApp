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

            IEnumerable<Books> dbBooks = _booksService.Get();

            foreach (Books b in dbBooks)
            {
                books.Add(GetBooksObj(b));
            }

            return books;
        }

        public BooksObj GetBookById(string Id)
        {
            Books dbBook = _booksService.Get(Id);

            if (dbBook == null)
                return null;

            return GetBooksObj(dbBook);
        }

        public BooksObj AddBook(BooksObj newBook)
        {
            Books dbBook = GetMongoBooks(newBook);

            return GetBooksObj(_booksService.Create(dbBook));
        }

        public BooksObj UpdateBook(string id, BooksObj book)
        {
            return null;
        }

        public async Task<BooksObj> DeleteBook(string id)
        {
            return null;
        }
        
        private Books GetMongoBooks(BooksObj book)
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

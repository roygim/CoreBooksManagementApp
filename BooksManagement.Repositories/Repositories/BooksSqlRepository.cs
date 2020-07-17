using BooksManagement.Repositories.Interface;
using BooksSQL.DataContexts;
using BooksSQL.DataService;
using BooksSQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksManagement.Models;

namespace BooksManagement.Repositories
{
    public class BooksSqlRepository : IBooksRepository
    {
        private readonly BooksSqlDBContext _booksContext;
        
        public BooksSqlRepository(BooksSqlDBContext booksContext)
        {
            _booksContext = booksContext;
        }

        public IEnumerable<BooksObj> GetAllBooks()
        {
            List<BooksObj> books = new List<BooksObj>();

            IEnumerable<Books> dbBook = BooksSqlDBService.GetAllBooks(_booksContext);

            foreach (Books b in dbBook)
            {
                books.Add(GetBooksObj(b));
            }

            return books;
        }

        public BooksObj GetBookById(string Id)
        {
            Books dbBook = BooksSqlDBService.GetBookById(_booksContext, int.Parse(Id));

            if (dbBook == null)
                return null;

            return GetBooksObj(dbBook); 
        }

        public BooksObj AddBook(BooksObj newBook)
        {
            Books dbBook = GetSqlBooks(newBook);

            return GetBooksObj(BooksSqlDBService.AddBook(_booksContext, dbBook));
        }

        public BooksObj UpdateBook(string id, BooksObj book)
        {
            Books dbBook = GetSqlBooks(book);

            return GetBooksObj(BooksSqlDBService.UpdateBook(_booksContext, int.Parse(id), dbBook));
        }

        public async Task<BooksObj> DeleteBook(string id)
        {
            Books dbBook = await BooksSqlDBService.DeleteBook(_booksContext, int.Parse(id));

            return GetBooksObj(dbBook);
        }
        
        private Books GetSqlBooks(BooksObj book)
        {
            Books sqlBook = new Books();

            if(!string.IsNullOrEmpty(book.Id))
                sqlBook.Id = int.Parse(book.Id);

            sqlBook.Name = book.Name;
            sqlBook.Year = book.Year;
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

        public IEnumerable<BooksObj> GetStoredProcedureExample(BooksObj book)
        {
            List<BooksObj> books = new List<BooksObj>();

            Books dbBook = GetSqlBooks(book);

            IEnumerable<Books> dbBooks = BooksSqlDBService.GetStoredProcedureExample(_booksContext, dbBook);

            foreach (Books b in dbBooks)
            {
                books.Add(GetBooksObj(b));
            }

            return books;
        }
    }
}

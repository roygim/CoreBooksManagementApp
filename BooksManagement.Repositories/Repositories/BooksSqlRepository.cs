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

            IEnumerable<Books> sqlBooks = BooksSqlDBService.GetAllBooks(_booksContext);

            foreach (Books b in sqlBooks)
            {
                books.Add(GetBooksObj(b));
            }

            return books;
        }

        public BooksObj GetBookById(string Id)
        {
            Books sqlBook = BooksSqlDBService.GetBookById(_booksContext, int.Parse(Id));

            if (sqlBook == null)
                return null;

            return GetBooksObj(sqlBook); 
        }

        public BooksObj AddBook(BooksObj newBook)
        {
            Books sqlBook = GetSqlBooks(newBook);

            return GetBooksObj(BooksSqlDBService.AddBook(_booksContext, sqlBook));
        }

        public BooksObj UpdateBook(string id, BooksObj book)
        {
            Books sqlBook = GetSqlBooks(book);

            return GetBooksObj(BooksSqlDBService.UpdateBook(_booksContext, int.Parse(id), sqlBook));
        }

        public async Task<BooksObj> DeleteBook(string id)
        {
            Books sqlBook = await BooksSqlDBService.DeleteBook(_booksContext, int.Parse(id));

            return GetBooksObj(sqlBook);
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
    }
}

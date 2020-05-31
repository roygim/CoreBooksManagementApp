using BooksManagement.Repositories.Interface;
using BooksSQL.DataContexts;
using BooksSQL.DataService;
using BooksSQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksManagement.Repositories
{
    public class BooksSqlRepository : IBooksRepository
    {
        private readonly BooksSqlDBContext _booksContext;
        
        public BooksSqlRepository(BooksSqlDBContext booksContext)
        {
            _booksContext = booksContext;
        }

        public IEnumerable<Books> GetAllBooks()
        {
            return BooksSqlDBService.GetAllBooks(_booksContext);
        }

        public Books GetBookById(int Id)
        {
            return BooksSqlDBService.GetBookById(_booksContext, Id);
        }

        public Books AddBook(Books newBook)
        {
            return BooksSqlDBService.AddBook(_booksContext, newBook);
        }

        public Books UpdateBook(int id, Books book)
        {
            return BooksSqlDBService.UpdateBook(_booksContext, id, book);
        }

        public async Task<Books> DeleteBook(int id)
        {
            return await BooksSqlDBService.DeleteBook(_booksContext, id);
        }
    }
}

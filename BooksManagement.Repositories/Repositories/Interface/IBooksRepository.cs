using BooksManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksManagement.Repositories.Interface
{
    public interface IBooksRepository
    {   
        IEnumerable<BooksObj> GetAllBooks();
        BooksObj GetBookById(string Id);
        BooksObj AddBook(BooksObj newBook);
        BooksObj UpdateBook(string id, BooksObj book);
        Task<BooksObj> DeleteBook(string id);
        IEnumerable<BooksObj> GetStoredProcedureExample(BooksObj book);
    }
}

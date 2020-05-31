using BooksSQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksManagement.Repositories.Interface
{
    public interface IBooksRepository
    {   
        IEnumerable<Books> GetAllBooks();   
        Books GetBookById(int Id);
        Books AddBook(Books newBook);
        Books UpdateBook(int id, Books book);
        Task<Books> DeleteBook(int id);
    }
}

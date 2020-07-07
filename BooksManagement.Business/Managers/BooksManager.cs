using BooksManagement.Repositories.Interface;
using BooksManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BooksManagement.Business.Managers
{
    public class BooksManager
    {
        public static IEnumerable<BooksObj> GetAllBooks(IBooksRepository booksRepository)
        {
            return booksRepository.GetAllBooks();
        }

        public static BooksObj GetBookById(IBooksRepository booksRepository, string id)
        {
            return booksRepository.GetBookById(id);
        }

        public static BooksObj AddBook(IBooksRepository booksRepository, BooksObj newBook)
        {
            return booksRepository.AddBook(newBook);
        }

        public static BooksObj UpdateBook(IBooksRepository booksRepository, string id, BooksObj book)
        {
            return booksRepository.UpdateBook(id, book);
        }

        public static async Task<BooksObj> DeleteBook(IBooksRepository booksRepository, string id)
        {
            return await booksRepository.DeleteBook(id);
        }
    }
}

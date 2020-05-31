using BooksManagement.Repositories.Interface;
using BooksSQL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BooksManagement.Business.Managers
{
    public class BooksManager
    {
        public static IEnumerable<Books> GetAllBooks(IBooksRepository booksRepository)
        {
            return booksRepository.GetAllBooks();
        }

        public static Books GetBookById(IBooksRepository booksRepository, int id)
        {
            return booksRepository.GetBookById(id);
        }

        public static Books AddBook(IBooksRepository booksRepository, Books newBook)
        {
            return booksRepository.AddBook(newBook);
        }

        public static Books UpdateBook(IBooksRepository booksRepository, int id, Books book)
        {
            return booksRepository.UpdateBook(id, book);
        }

        public static async Task<Books> DeleteBook(IBooksRepository booksRepository, int id)
        {
            return await booksRepository.DeleteBook(id);
        }
    }
}

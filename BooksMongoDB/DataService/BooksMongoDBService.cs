using BooksMongoDB.DataSettings;
using BooksMongoDB.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BooksMongoDB.DataService
{
    public class BooksMongoDBService
    {
        private readonly IMongoCollection<Books> _books;

        public BooksMongoDBService(IBooksMongoDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _books = database.GetCollection<Books>(settings.BooksCollectionName);
        }

        public List<Books> Get()
        {
            return _books.Find(book => true).ToList();
        }

        public Books Get(string id)
        {
            Books b = null;

            try
            {
                b = _books.Find<Books>(book => book.Id == id).FirstOrDefault();
            }
            catch (System.Exception ex)
            {
            }

            return b;
        }   

        public Books Create(Books book)
        {
            _books.InsertOne(book);
            return book;
        }

        public void Update(string id, Books bookIn) =>
            _books.ReplaceOne(book => book.Id == id, bookIn);

        public void Remove(Books bookIn) =>
            _books.DeleteOne(book => book.Id == bookIn.Id);

        public void Remove(string id) =>
            _books.DeleteOne(book => book.Id == id);
    }
}
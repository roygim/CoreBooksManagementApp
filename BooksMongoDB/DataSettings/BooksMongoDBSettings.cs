namespace BooksMongoDB.DataSettings
{
    public class BooksMongoDBSettings : IBooksMongoDBSettings
    {
        public string BooksCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IBooksMongoDBSettings
    {
        string BooksCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
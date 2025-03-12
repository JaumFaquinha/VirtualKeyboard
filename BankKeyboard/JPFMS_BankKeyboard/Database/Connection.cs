using MongoDB.Bson;
using MongoDB.Driver;
using System.Runtime.CompilerServices;

namespace JPFMS_BankKeyboard.Database
{
    public static class Connection
    {
        public static MongoClient Client = new MongoClient();

        public static void ConnectMongDB()
        {
            const string connectionUri = "mongodb+srv://joaofachini01:TB7aNVBSGNSYSyrn@techspace.2727n.mongodb.net/?retryWrites=true&w=majority&appName=TechSpace";
            var settings = MongoClientSettings.FromConnectionString(connectionUri);
            settings.ServerSelectionTimeout = TimeSpan.FromSeconds(10);
            // Set the ServerApi field of the settings object to set the version of the Stable API on the client
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            // Create a new client and connect to the server
            Client = new MongoClient(settings);
            // Send a ping to confirm a successful connection
            try
            {
                var result = Client.GetDatabase("admin").RunCommand<BsonDocument>(new BsonDocument("ping", 1));
                Console.WriteLine("Pinged your deployment. You successfully connected to MongoDB!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    
        public static IMongoDatabase GetDatabase()
        {
            return Client.GetDatabase("BankLogin");
        }
    }
}

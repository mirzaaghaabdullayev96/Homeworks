using MongoDB.Driver;
using Pustok.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Data.DAL
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            _database = client.GetDatabase("Pustok");
        }

        public IMongoCollection<SettingMongoDb> Settings => _database.GetCollection<SettingMongoDb>("Settings");
    }
}

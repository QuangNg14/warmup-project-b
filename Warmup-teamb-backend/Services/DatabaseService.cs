using System;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Models;

namespace Warmup_teamb_backend.Services
{
	public class DatabaseService
	{
        private readonly IMongoCollection<Room> _roomsCollection;
        private readonly IMongoCollection<CustomData> _customDataCollection;

        public DatabaseService(IOptions<DbSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

            _roomsCollection = mongoDatabase.GetCollection<Room>("rooms");
            _customDataCollection = mongoDatabase.GetCollection<CustomData>("customDataItems");
        }

        public async Task CreateAsync(Room room) =>
            await _roomsCollection.ReplaceOneAsync(
                doc => doc.Name == room.Name,
                room,
                new ReplaceOptions { IsUpsert = true });

        public async Task CreateAsync(CustomData dataItem) =>
            await _customDataCollection.InsertOneAsync(dataItem);
    }
}


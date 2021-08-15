using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SimpleBotCore.Config;
using SimpleBotCore.Models;
using System.Threading.Tasks;

namespace SimpleBotCore.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly IMongoClient _client;
        private readonly IMongoCollection<Question> _questionCollection;
        private readonly IMongoDatabase _database;

        private readonly MongoDBConnection _config;
        private readonly MongoClientSettings _settings;

        public QuestionRepository(IOptions<MongoDBConnection> options)
        {
            _config = options.Value;

            _settings = MongoClientSettings.FromConnectionString(_config.GetConnectionDefault());
            _client = new MongoClient(_settings);

            _database = _client.GetDatabase(_config.Database);
            _questionCollection = _database.GetCollection<Question>("questions");
        }

        public async Task CreateAsync(string text)
        {
            var question = new Question
            {
                Content = text,
            };

            await _questionCollection.InsertOneAsync(question);
        }
    }
}
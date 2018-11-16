using System;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Web;
using MongoDB.Driver;
using MongoDB.Driver.Core;
using MongoDB.Driver.Linq;
using MongoDB.Bson;
using EasyPostApp.Models;

namespace EasyPostApp.Services
{
    public class PostServices
    {

        private MongoClient _mongoClient;
        private IMongoDatabase _mongoDB;
        private CancellationTokenSource _tokenSource;
        private TaskCompletionSource<List<PostModel>> _taskCompletionSource;      

        public PostServices()
        {

            string connectionString = ConfigurationManager.AppSettings["MONGO_CONNECTION_URL"];
            var settings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));

            settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };

            _mongoClient = new MongoClient(settings);

            var dbName = ConfigurationManager.AppSettings["MONGO_DB_NAME"];
            _mongoDB = _mongoClient.GetDatabase(dbName);

            _tokenSource = new CancellationTokenSource();
            _taskCompletionSource = new TaskCompletionSource<List<PostModel>>();

        }

        public async Task<List<PostModel>> GetPostsAsync(string postIdString)
        {

            List<PostModel> allPosts = new List<PostModel>();
            await Task.Run(() =>
            {

                var collectionName = ConfigurationManager.AppSettings["MONGO_DOCUMENT_POSTS"];
                var doc = _mongoDB.GetCollection<PostModel>(collectionName);

                if (string.IsNullOrEmpty(postIdString) == true)
                    allPosts  = doc.Find(x => x._id != null).ToList();
                else
                    allPosts = doc.Find(x => x._id == ObjectId.Parse(postIdString)).ToList();

            });
    
            return allPosts;

        }

        public async Task InsertPostAsync(PostModel postModel)
        {

            var collectionName = ConfigurationManager.AppSettings["MONGO_DOCUMENT_POSTS"];
            var doc = _mongoDB.GetCollection<PostModel>(collectionName);
            await doc.InsertOneAsync(postModel, null, _tokenSource.Token);

            var sortDefinition = Builders<PostModel>.Sort.Descending("_id");
            var list = doc.Find(x => x._id != null).Sort(sortDefinition);
            var lst = list.ToList();


        }

        public async Task<bool> UpdatePostAsync(string postIdString, PostModel updatePostModel)
        {

            var collectionName = ConfigurationManager.AppSettings["MONGO_DOCUMENT_POSTS"];
            var doc = _mongoDB.GetCollection<PostModel>(collectionName);

            var filterDefinition = Builders<PostModel>.Filter.Eq("_id", ObjectId.Parse(postIdString));
            var updateDefinition = Builders<PostModel>.Update.Set("message",
                                                                  updatePostModel.message);
            var updateResult = await doc.UpdateOneAsync(filterDefinition, updateDefinition, null,
                                                        _tokenSource.Token);
            return updateResult.IsAcknowledged;
            

        }

        public async Task<bool> DeletePostAsync(string postIdString)
        {

            var collectionName = ConfigurationManager.AppSettings["MONGO_DOCUMENT_POSTS"];
            var doc = _mongoDB.GetCollection<PostModel>(collectionName);

            var filterDefinition = Builders<PostModel>.Filter.Eq("_id", ObjectId.Parse(postIdString));            
            var deleteResult = await doc.DeleteManyAsync(filterDefinition, null,
                                                            _tokenSource.Token);
            return deleteResult.IsAcknowledged;


        }




    }
}
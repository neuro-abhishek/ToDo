using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Linq.Expressions;

class MongoOperationDal<T> : IMongoOperationDal<T>
    where T : IHasId
{
    private readonly IMongoDatabase _Database;
    private readonly IMongoCollection<T> _CollectionName;
    public MongoOperationDal(IMongoClient client, IOptions<MongoDbSettings> options)
    {
        _Database = client.GetDatabase(options.Value.DatabaseName);
        _CollectionName = GetCollectionName();
        // _Task = 
    }

    public IMongoCollection<T> GetCollectionName()
    {
        // var collName = typeof(T).ToString();
        // Console.WriteLine(collName);
        // _CollectionName = _Database.GetCollection<T>(collName);
        var collName = _Database.GetCollection<T>(typeof(T).ToString());
        Console.WriteLine(collName);
        return collName;
    }

    // public Task<IMongoCollection<T>> GetCollection<T>()
    // {
    //     return _Task;
    // }

    public async Task<List<T>> GetData()
    {
        try
        {
            List<T> data = await _CollectionName.Find(document => true).ToListAsync();
            return data;
        }
        catch (System.Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task PostData(T data)
    {
        try
        {
            await _CollectionName.InsertOneAsync(data);
        }
        catch (System.Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<T> GetDataById(string id)
    {
        try
        {
            T result = await _CollectionName.Find(document => document.Id == id).FirstOrDefaultAsync();
            if (result == null)
            {
                throw new Exception("There is no data with this Id");
            }
            return result;
        }
        catch (System.Exception e)  
        {
            throw new Exception(e.Message);
        }
    }

    public async Task DeleteDataById(string id)
    {
        try
        {
            await _CollectionName.DeleteOneAsync(document => document.Id == id);
        }
        catch (System.Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task UpdateData(string id, T data)
    {
        try
        {
            var filter = Builders<T>.Filter.Eq(document => document.Id, id);
            // var update = Builders<T>.Update.Set(document => document.Description, data.Description);

            await _CollectionName.ReplaceOneAsync(filter, data);
        }
        catch (System.Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<T> FindOneAsync(Expression<Func<T, bool>> filter)
    {
        try
        {
            return await _CollectionName.Find(filter).FirstOrDefaultAsync();
        }
        catch (System.Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}
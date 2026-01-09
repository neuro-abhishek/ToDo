using MongoDB.Driver;
using System.Linq.Expressions;

public interface IMongoOperationDal<T>
{
    public Task<List<T>> GetData();
    public Task PostData(T Content);
    public Task<T> GetDataById(string Id);
    public Task DeleteDataById(string Id);
    public Task UpdateData(string Id, T Data);
    public IMongoCollection<T> GetCollectionName();
    public Task<T> FindOneAsync(Expression<Func<T, bool>> filter);
}
using MongoDB.Driver;

public interface IMongoOperationDal<T>
{
    public Task<List<T>> GetData();
    public Task PostData(T Content);
    public Task<T> GetDataById(string Id);
    public Task DeleteDataById(string Id);
    public Task UpdateData(string Id, T Data);
    public IMongoCollection<T> GetCollectionName();
}
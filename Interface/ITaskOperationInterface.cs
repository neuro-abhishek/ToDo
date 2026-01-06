public interface ITaskOperationInterface
{
    public Task<List<ToDoTask>> GetData();
    public Task<bool> AddData(ToDoTask Data);
    public Task<ToDoTask> GetDataById(string Id);
    public Task<bool> DeleteDataById(string Id);
    public Task<bool> ChangeDataDescription(string Id, ToDoTask Data);

}
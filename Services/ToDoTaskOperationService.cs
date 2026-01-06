public class TaskOperation : ITaskOperationInterface
{
    IMongoOperationDal<ToDoTask> _MongoOperation;
    public TaskOperation(IMongoOperationDal<ToDoTask> mongoOperation)
    {
        _MongoOperation = mongoOperation;
    }

    public async Task<List<ToDoTask>> GetData()
    {
        try
        {
            List<ToDoTask> taskArray = await _MongoOperation.GetData();
            return taskArray;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }

    }

    public async Task<bool> AddData(ToDoTask data)
    {
        try
        {
            await _MongoOperation.PostData(data);
            return true;
        }
        catch (System.Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<ToDoTask> GetDataById(string id)
    {
        try
        {
            ToDoTask result = await _MongoOperation.GetDataById(id);
            return result;
        }
        catch (System.Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<bool> DeleteDataById(string id)
    {
        try
        {
            ToDoTask result = await _MongoOperation.GetDataById(id);
            await _MongoOperation.DeleteDataById(id);
            return true;
        }
        catch (System.Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<bool> ChangeDataDescription(string id, ToDoTask data)
    {
        try
        {
            ToDoTask result = await _MongoOperation.GetDataById(id);
            await _MongoOperation.UpdateData(id, data);
            return true;

        }
        catch (System.Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}
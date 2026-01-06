public interface IFileOperationInterface
{
    public Task<ToDoTask[]> ReadData(string FilePath);
    public Task WriteData(string FilePath, ToDoTask[] Content);
}
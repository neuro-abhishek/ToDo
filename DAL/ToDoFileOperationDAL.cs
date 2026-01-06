using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Newtonsoft.Json;


public class ToDoFileOperation : IFileOperationInterface
{
    public async Task<ToDoTask[]> ReadData(string FilePath)
    {
        try
        {
            string FileData =  await File.ReadAllTextAsync(FilePath);
            // Console.WriteLine(FileData);     // FilePath = data.json
            ToDoTask[] arr = JsonConvert.DeserializeObject<ToDoTask[]>(FileData);
            return arr;
        }
        catch (System.Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task WriteData(string FilePath, ToDoTask[] Content)
    {
        try
        {
            string Data = JsonConvert.SerializeObject(Content);
            await File.WriteAllTextAsync(FilePath, Data);
        }
        catch (System.Exception e)
        {
            throw new Exception(e.Message);
        }
    }

}
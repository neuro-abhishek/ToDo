public class ToDoTask : IHasId
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ToDoTask()
    {
        Id = "1";
        Name = "";
        Description = "";
    }
}
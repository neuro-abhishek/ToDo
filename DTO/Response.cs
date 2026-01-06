using System.Runtime.CompilerServices;

class Response<T>
{
    public int StatusCode {get; set;}
    public T Data {get; set;}
    public string Message {get; set;}
    public string  ErrorMessage {get; set;}

    public Response(T Value)
    {
        Data = Value;
    }
}
namespace Poster.Application.Common;

public class ResultOfT<T> : Result
{
    public T Value { get; set; }
    
    public ResultOfT(T value, bool success, string error) 
        : base(success, error)
    {
        Value = value;
    }
}
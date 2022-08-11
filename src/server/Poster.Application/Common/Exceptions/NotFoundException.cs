namespace Poster.Application.Common.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string name, string id) 
        : base($"{name} with {id} has not been found") { }
}
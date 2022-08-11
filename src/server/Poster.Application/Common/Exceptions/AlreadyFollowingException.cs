namespace Poster.Application.Common.Exceptions;

public class AlreadyFollowingException : Exception
{
    public AlreadyFollowingException(string entity, string id) 
        : base($"{entity} has already following {id}") { }
}
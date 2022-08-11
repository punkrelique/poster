namespace Poster.Application.Common.Exceptions;

public class IsNotFollowingException : Exception
{
    public IsNotFollowingException(string entity, string id) 
        : base($"{entity} is not following {id}") { }
}
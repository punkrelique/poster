namespace Poster.Application.Common.Exceptions;

public class AuthorException : Exception
{
    public AuthorException(string messageId, string userId) 
        : base($"Message's author with {messageId} is not {userId}") { }
}
using System.Text.RegularExpressions;

namespace Poster.Api.Utils;

public static class RegexIdentifiers
{
    public static readonly Regex MailRegex = 
        new Regex(
            @"(\w+@[a-zA-Z_.]+\.[a-zA-Z]{2,255})",
            RegexOptions.Compiled);
}
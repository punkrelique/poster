using System.Text.RegularExpressions;

namespace Poster.Api.Utils;

public static class RegexIdentifiers
{
    public static readonly Regex MailRegex = 
        new (
            @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$",
            RegexOptions.Compiled);

    public static readonly Regex UsernameRegex =
        new (
            @"^\w+$",
            RegexOptions.Compiled);
}
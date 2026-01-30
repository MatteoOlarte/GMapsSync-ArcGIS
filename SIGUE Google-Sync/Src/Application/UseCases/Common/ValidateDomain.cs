#nullable enable

using System;

namespace GMapsSync.Src.Application.UseCases.Common;

#if RELEASE
public class ValidateDomain
{
    public static bool Invoke()
    {

        string? domain = Environment.UserDomainName;
        return string.Equals(domain, "ACUEDUCTO", StringComparison.OrdinalIgnoreCase);
    }
}
#else
public class ValidateDomain
{
    public static bool Invoke() => true;
}
#endif

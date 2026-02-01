using System.Reflection;

namespace SharedKernel;

public record QueryStringParameter(int PageNumber = 1, int PageSize = 10)
{
    /// <summary>
    /// TryParse implementation for .NET 10 minimal APIs query parameter binding
    /// </summary>
    public static bool TryParse(string? value, out QueryStringParameter? result)
    {
        result = null;

        if (string.IsNullOrEmpty(value))
        {
            result = new QueryStringParameter();
            return true;
        }

        try
        {
            // Parse query string parameters
            var parameters = new Dictionary<string, string>();
            foreach (var param in value.Split('&'))
            {
                var parts = param.Split('=');
                if (parts.Length == 2)
                {
                    parameters[parts[0].ToLower()] = parts[1];
                }
            }

            int pageNumber = 1;
            int pageSize = 10;

            if (parameters.TryGetValue("pagenumber", out var pageNumberStr) &&
                int.TryParse(pageNumberStr, out var pn))
            {
                pageNumber = pn;
            }

            if (parameters.TryGetValue("pagesize", out var pageSizeStr) &&
                int.TryParse(pageSizeStr, out var ps))
            {
                pageSize = ps;
            }

            result = new QueryStringParameter(pageNumber, pageSize);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
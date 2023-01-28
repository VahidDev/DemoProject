using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project.Infrastructure.DAL;

public static class EfDbTools
{
    private enum DbObjectType
    {
        Procedure = 1,
        Function = 2
    }

    public static IList<T> ExecuteProcedure<T>(
        string procedureName,
        params SqlParameter[] parameters)
        where T : class
    {
        if (parameters != null && IsAnyParamNull(parameters))
        {
            return new List<T>();
        }

        using var context = new AppDbContext();

        var parametersStr = parameters != null
            ? string.Join(", ", parameters.Select(p => $"@{p.ParameterName} = @{p.ParameterName}"))
            : "";

        var query =
            $"exec {procedureName} {parametersStr}";

        var parametersList = new List<object>();

        if (parameters != null)
        {
            parametersList.AddRange(parameters);
        }

        return context
            .Set<T>()
            .FromSqlRaw(query, parametersList.ToArray())
            .ToList();
    }

    private static bool IsAnyParamNull(SqlParameter[] parameters)
    {
        return parameters.Any(p => p.Value == null);
    }

    public static IList<T> ExecuteProcedure<T>(
        string procedureName,
        IList<SqlParameter> parameters = null)
        where T : class
    {
        return ExecuteProcedure<T>(procedureName, parameters.ToArray());
    }

    /// <summary>
    /// Add parameter to list
    /// </summary>
    public static void AddParam(this IList<SqlParameter> list, string paramName, object paramValue)
    {
        list.Add(new SqlParameter(paramName, paramValue));
    }
}
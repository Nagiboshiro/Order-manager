using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace OrderManager.DataAccess.Postgres.Exceptions;

public sealed class DuplicateException : IExceptionExtensionsDataAccess
{
    private const string PostgresDuplicateErrorCode = "23505";

    public bool IsDuplicateException(DbUpdateException ex)
    {
        return ex.InnerException is PostgresException
        {
            SqlState: PostgresDuplicateErrorCode
        };
    }
}
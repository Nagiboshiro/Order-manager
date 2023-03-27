
using Microsoft.EntityFrameworkCore;

namespace OrderManager.DataAccess;

public interface IExceptionExtensionsDataAccess
{
    public bool IsDuplicateException(DbUpdateException ex);
}
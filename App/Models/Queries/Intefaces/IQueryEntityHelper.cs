using Microsoft.Data.SqlClient;
using Models.DTO_s;

namespace Models.Queries.Intefaces
{
    public interface IQueryEntityHelper
    {
        Task<ResponseQuery<T>> ExecDatabaseQuery<T>(ContactsContext context, string query) where T : class, new();
    }
}

using FastMember;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Models.DTO_s;
using Models.Queries.Intefaces;
using System.Data;

namespace Models
{
    public class QueryEntityHelper : IQueryEntityHelper
    {
        public async Task<ResponseQuery<T>> ExecDatabaseQuery<T>(ContactsContext context, string query) where T : class, new()
        {
            var entities = new List<T>();

            try
            {
                using (var command = context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;

                    await context.Database.OpenConnectionAsync();

                    using (SqlDataReader result = (SqlDataReader)await command.ExecuteReaderAsync())
                    {
                        while (await result.ReadAsync())
                        {
                            var entity = await ConvertToObject<T>(result);
                            entities.Add(entity);
                        }
                    }
                    await context.Database.CloseConnectionAsync();
                }

                if (entities.Count < 1)
                    return new ResponseQuery<T>() { Success = true, Message = "Empty query" };
                else
                    return new ResponseQuery<T>() { Success = true, Results = entities };
            }
            catch (Exception ex)
            {
                return new ResponseQuery<T>() { Success = true, Message = ex.Message };
            }
        }

        private async Task<T> ConvertToObject<T>(SqlDataReader dataReader) where T : class, new()
        {
            var type = typeof(T);
            var accessor = TypeAccessor.Create(type);
            var members = accessor.GetMembers();
            var entity = new T();

            for (int i = 0; i < dataReader.FieldCount; i++)
            {
                if (!await dataReader.IsDBNullAsync(i))
                {
                    var fieldName = dataReader.GetName(i);

                    if (members.Any(x => string.Equals(x.Name, fieldName, StringComparison.OrdinalIgnoreCase)))
                    {
                        accessor[entity, fieldName] = dataReader.GetValue(i);
                    }
                }
            }
            return entity;
        }
    }
}

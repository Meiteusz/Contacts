using FastMember;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Models
{
    public static class ConversorObject
    {
        public async static Task<T> ConvertToObject<T>(this SqlDataReader dataReader) where T : class, new()
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

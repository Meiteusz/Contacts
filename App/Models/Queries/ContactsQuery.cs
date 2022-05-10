using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Models.DTO_s;
using Models.Entities;
using Models.Queries.Intefaces;
using System.Data;
using System.Text;

namespace Models.Queries
{
    public class ContactsQuery : IContactsQuery, IBaseQuery<User>
    {
        public async Task<ResponseQuery<User>> GetAllAsync()
        {
            try
            {
                using (var context = new ContactsContext())
                {
                    var contactsList = await context.Users.ToListAsync();

                    if (contactsList.Count > 0)
                    {
                        return new ResponseQuery<User>() { Success = true, Results = contactsList };
                    }
                    return new ResponseQuery<User>() { Message = "None contact added on the contacts list yet" };
                }
            }
            catch (Exception ex)
            {
                return new ResponseQuery<User>() { Message = ex.Message };
            }
        }

        public async Task<User?> GetFirstOrDefaultAsync(int? id)
        {
            using (var context = new ContactsContext())
            {
                return await context.Users
                                    .Where(x => x.Id == id)
                                    .FirstOrDefaultAsync();
            }
        }

        public async Task<ResponseQuery<User>> GetContactsByFilterAsync(User userFilter)
        {
            var entities = new List<User>();

            try
            {
                using (var context = new ContactsContext())
                {
                    using (var command = context.Database.GetDbConnection().CreateCommand())
                    {
                        command.CommandText = BuildUserFilterQuery(userFilter);
                        command.CommandType = CommandType.Text;

                        await context.Database.OpenConnectionAsync();

                        using (SqlDataReader result = (SqlDataReader)await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                var entity = await result.ConvertToObject<User>();
                                entities.Add(entity);
                            }
                        }
                    }
                    await context.Database.CloseConnectionAsync();
                    return new ResponseQuery<User>() { Success = true, Results = entities };
                }
            }
            catch (Exception ex)
            {
                return new ResponseQuery<User>() { Message = ex.Message };
            }
        }

        public async Task<bool> CheckMainEmail(int idContact, string email)
        {
            using (var context = new ContactsContext())
            {
                return await context.Users.Where(x => x.Id == idContact)
                                           .Select(f => f.MainEmail)
                                           .SingleOrDefaultAsync() == email;
            }
        }

        public string BuildUserFilterQuery(User userFilter)
        {
            var query = new StringBuilder()
            .Append($"SELECT * FROM USERS WHERE NAME LIKE '{userFilter.Name}%' ");

            if (userFilter.Id.IsValidId())
                query.Append($"AND ID = {userFilter.Id} ");

            if (!string.IsNullOrEmpty(userFilter.MainEmail))
                query.Append($"AND MAINEMAIL = '{userFilter.MainEmail}' ");

            if (!string.IsNullOrEmpty(userFilter.Company))
                query.Append($"AND COMPANY = '{userFilter.Company}' ");

            if (!string.IsNullOrEmpty(userFilter.CommercialPhone))
                query.Append($"AND COMMERCIALPHONE = '{userFilter.CommercialPhone}' ");

            if (!string.IsNullOrEmpty(userFilter.PersonalPhone))
                query.Append($"AND PERSONALPHONE = '{userFilter.PersonalPhone}' ");

            return query.ToString();
        }

        public async Task<string> GetContactNameById(int idContact)
        {
            using (var context = new ContactsContext())
            {
                return await context.Users.Where(x => x.Id == idContact)
                                    .Select(f => f.Name)
                                    .SingleOrDefaultAsync();
            }
        } 
    }
}

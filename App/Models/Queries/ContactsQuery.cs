using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Models.DTO_s;
using Models.Entities;
using Models.Queries.Intefaces;
using System.Data;
using System.Text;

namespace Models.Queries
{
    public class ContactsQuery : IContactsQuery, IBaseQuery<Contact>
    {
        private readonly IQueryEntityHelper _queryEntityHelper;

        public ContactsQuery(IQueryEntityHelper queryEntityHelper)
        {
            this._queryEntityHelper = queryEntityHelper;
        }

        public async Task<ResponseQuery<Contact>> GetAllAsync()
        {
            try
            {
                using (var context = new ContactsContext())
                {
                    var contactsList = await context.Users.ToListAsync();

                    if (contactsList.Count > 0)
                    {
                        return new ResponseQuery<Contact>() { Success = true, Results = contactsList };
                    }
                    return new ResponseQuery<Contact>() { Message = "None contact added on the contacts list yet" };
                }
            }
            catch (Exception ex)
            {
                return new ResponseQuery<Contact>() { Message = ex.Message };
            }
        }

        public async Task<Contact?> GetFirstOrDefaultAsync(int? id)
        {
            using (var context = new ContactsContext())
            {
                return await context.Users
                                    .Where(x => x.Id == id)
                                    .FirstOrDefaultAsync();
            }
        }

        public async Task<ResponseQuery<Contact>> GetContactsByFilterAsync(Contact userFilter)
        {
            try
            {
                using (var context = new ContactsContext())
                {
                    return await _queryEntityHelper.ExecDatabaseQuery<Contact>(context, BuildUserFilterQuery(userFilter));
                }
            }
            catch (Exception ex)
            {
                return new ResponseQuery<Contact>() { Message = ex.Message };
            }
        }

        public async Task<bool> CheckMainEmail(int contactId, string email)
        {
            using (var context = new ContactsContext())
            {
                return await context.Users.Where(x => x.Id == contactId)
                                           .Select(f => f.MainEmail)
                                           .SingleOrDefaultAsync() == email;
            }
        }

        public string BuildUserFilterQuery(Contact userFilter)
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

        public async Task<string> GetContactNameById(int contactId)
        {
            using (var context = new ContactsContext())
            {
                return await context.Users.Where(x => x.Id == contactId)
                                    .Select(f => f.Name)
                                    .SingleOrDefaultAsync() ?? string.Empty;
            }
        } 
    }
}

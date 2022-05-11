using Microsoft.EntityFrameworkCore;
using Models.DTO_s;
using Models.Entities;
using Models.Queries.Intefaces;

namespace Models.Queries
{
    public class EmailListQuery : IEmailListQuery, IBaseQuery<EmailList>
    {
        public Task<ResponseQuery<EmailList>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<EmailList?> GetFirstOrDefaultAsync(int? id)
        {
            using (var context = new ContactsContext())
            {
                return await context.EmailList
                                    .Where(x => x.Id == id)
                                    .FirstOrDefaultAsync();
            }
        }

        public async Task<ResponseQuery<EmailList>> GetAllContactEmails(int contactId)
        {
            try
            {
                using (var context = new ContactsContext())
                {
                    var contactsEmailList = await context.EmailList
                                                         .Where(x => x.UserId == contactId)
                                                         .ToListAsync();

                    if (contactsEmailList.Count > 0)
                    {
                        return new ResponseQuery<EmailList>() { Success = true, Results = contactsEmailList };
                    }
                    return new ResponseQuery<EmailList>() { Message = "None email registered to this user yet" };
                }
            }
            catch (Exception ex)
            {
                return new ResponseQuery<EmailList>() { Message = ex.Message };
            }
        }

        public async Task<int> GetEmailIdByEmail(string email)
        {
            using (var context = new ContactsContext())
            {
                return await context.EmailList.Where(x => x.Email == email)
                                               .Select(f => f.Id)
                                               .FirstOrDefaultAsync();
            }
        }

        public async Task<string> GetEmailById(int idEmail)
        {
            using (var context = new ContactsContext())
            {
                return await context.EmailList.Where(x => x.Id == idEmail)
                                              .Select(f => f.Email)
                                              .FirstOrDefaultAsync();
            }
        }
    }
}

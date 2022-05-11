using Models.DTO_s;
using Models.Entities;

namespace Models.Queries.Intefaces
{
    public interface IEmailListQuery
    {
        Task<ResponseQuery<EmailList>> GetAllContactEmails(int contactId);
        Task<EmailList?> GetFirstOrDefaultAsync(int? emailId);
        Task<int> GetEmailIdByEmail(string email);
        Task<string> GetEmailById(int emailId);
    }
}

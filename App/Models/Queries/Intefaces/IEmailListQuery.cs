using Models.DTO_s;
using Models.Entities;

namespace Models.Queries.Intefaces
{
    public interface IEmailListQuery
    {
        Task<ResponseQuery<EmailList>> GetAllContactEmails(int idContact);
        Task<EmailList?> GetFirstOrDefaultAsync(int? id);
        Task<int> GetEmailIdByEmail(string email);
        Task<string> GetEmailById(int idEmail);
    }
}

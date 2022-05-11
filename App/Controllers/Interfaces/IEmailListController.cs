using Models.DTO_s;
using Models.Entities;

namespace Controllers
{
    public interface IEmailListController
    {
        Task<Response> RegisterEmailList(EmailList emailList);
        Task<ResponseQuery<EmailList>> GetAllContactEmails(int contactId);
        Task<int> GetEmailIdByEmail(string email);
        Task<Response> UpdateEmail(int emailId, string email);
    }
}

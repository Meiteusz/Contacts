using Models;
using Models.DTO_s;
using Models.Entities;

namespace Controllers
{
    public interface IEmailListController
    {
        Task<Response> RegisterEmailList(EmailList emailList, bool isMainEmail);
        Task<ResponseQuery<EmailList>> GetAllContactEmails(int idContact);
        Task<int> GetEmailIdByEmail(string email);
        Task<Response> UpdateEmail(int idEmail, string email);
    }
}

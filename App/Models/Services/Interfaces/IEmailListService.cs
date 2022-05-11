using Models.Entities;

namespace Models.Services.Interfaces
{
    public interface IEmailListService
    {
        Task<ResponseId> CreateAsync(EmailList emailList);
        Task<ResponseId> UpdateEmailAsync(int emailId, string email);
    }
}

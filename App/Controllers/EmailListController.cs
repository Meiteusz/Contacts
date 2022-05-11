using Models;
using Models.DTO_s;
using Models.Entities;
using Models.Queries.Intefaces;
using Models.Services.Interfaces;

namespace Controllers
{
    public class EmailListController : IEmailListController
    {
        private readonly IEmailListService _emailListService;
        private readonly IEmailListQuery _emailListQuery;

        public EmailListController(IEmailListService emailListService, IEmailListQuery emailListQuery)
        {
            this._emailListService = emailListService;
            this._emailListQuery = emailListQuery;
        }

        public async Task<Response> RegisterEmailList(EmailList emailList)
        {
            emailList.UserId = Globals.GetContact().ToLong();
            return await _emailListService.CreateAsync(emailList);
        }

        public async Task<ResponseQuery<EmailList>> GetAllContactEmails(int contactId)
        {
            return await _emailListQuery.GetAllContactEmails(contactId);
        }

        public async Task<int> GetEmailIdByEmail(string email)
            => await _emailListQuery.GetEmailIdByEmail(email);

        public async Task<Response> UpdateEmail(int emailId, string email)
        {
            var previousEmail = await _emailListQuery.GetEmailById(emailId);

            if (previousEmail == email)
                return new Response() { Message = "Email is equal"};

            return await _emailListService.UpdateEmailAsync(emailId, email);
        }
    }
}

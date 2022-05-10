using Models;
using Models.DTO_s;
using Models.Entities;
using Models.Queries.Intefaces;
using Models.Services.Interfaces;

namespace Controllers
{
    public class EmailListController : IEmailListController
    {
        private readonly IUserController _userController;
        private readonly IEmailListService _emailListService;
        private readonly IEmailListQuery _emailListQuery;

        public EmailListController(IUserController userController, IEmailListService emailListService, IEmailListQuery emailListQuery)
        {
            this._userController = userController;
            this._emailListService = emailListService;
            this._emailListQuery = emailListQuery;
        }

        public async Task<Response> RegisterEmailList(EmailList emailList, bool isMainEmail)
        {
            emailList.UserId = Globals.GetContact().ToLong();

            var response =  await emailList.CreateAsync();

            if (isMainEmail)
                await _userController.SetMainEmailContact(emailList.UserId, emailList.Email); //change this, this method it's make 2 things
                                                                                              //change the form how the email list is saved, put all emails on a list and saveAll then
            return response;
        }

        public async Task<ResponseQuery<EmailList>> GetAllContactEmails(int idContact)
        {
            return await _emailListQuery.GetAllContactEmails(idContact);
        }

        public async Task<int> GetEmailIdByEmail(string email)
            => await _emailListQuery.GetEmailIdByEmail(email);

        public async Task<Response> UpdateEmail(int idEmail, string email)
        {
            var previousEmail = await _emailListQuery.GetEmailById(idEmail);

            if (previousEmail == email)
                return new Response() { Message = "Email is equal"};

            return await _emailListService.UpdateEmailAsync(idEmail, email);
        }
    }
}

using Models.Queries.Intefaces;
using Models.Services.Interfaces;

namespace Models.Services
{
    public class EmailListService : IEmailListService
    {
        private IEmailListQuery _emailListQuery;

        public EmailListService(IEmailListQuery emailListQuery)
        {
            this._emailListQuery = emailListQuery;
        }

        public async Task<ResponseId> UpdateEmailAsync(int idEmail, string email)
        {
            try
            {
                using (var context = new ContactsContext())
                {
                    var emailList = await _emailListQuery.GetFirstOrDefaultAsync(idEmail);

                    emailList.Email = email;
                    context.EmailList.Update(emailList);
                    var response = await context.ResponseSaveChangesAsync();
                    response.IdReturn = emailList.Id;
                    return response;
                }
            }
            catch (Exception ex)
            {
                return new ResponseId() { IdReturn = EntityBaseValidator.Invalid_Id, Message = ex.Message };
            }
        }
    }
}

using Models.DTO_s;
using Models.Entities;

namespace Models.Queries.Intefaces
{
    public interface IContactsQuery
    {
        Task<ResponseQuery<Contact>> GetAllAsync();
        Task<Contact?> GetFirstOrDefaultAsync(int? id);
        Task<ResponseQuery<Contact>> GetContactsByFilterAsync(Contact userFilter);
        Task<bool> CheckMainEmail(int contactId, string email);
        Task<string> GetContactNameById(int contactId);
    }
}

using Models.DTO_s;
using Models.Entities;

namespace Models.Queries.Intefaces
{
    public interface IContactsQuery
    {
        Task<ResponseQuery<User>> GetAllAsync();
        Task<User?> GetFirstOrDefaultAsync(int? id);
        Task<ResponseQuery<User>> GetContactsByFilterAsync(User userFilter);
        Task<bool> CheckMainEmail(int idContact, string email);
        Task<string> GetContactNameById(int idContact);
    }
}

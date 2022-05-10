using Models;
using Models.DTO_s;
using Models.Entities;

namespace Controllers
{
    public interface IUserController
    {
        Task<ResponseId> RegisterUser(User contact);
        Task<ResponseId> UpdateContact(User contact);
        Task<ResponseQuery<User>> GetAllContacts();
        Task<User?> GetFirstOrDefault(int idContact);
        Task<ResponseQuery<User>> GetContactsByFilters(User userFilter);
        Task<bool> CheckMainEmail(int idContact, string email);
        Task<Response> SetMainEmailContact(int idContact, string mainEmailContact);
        Task<Response> DeleteContact(int idContact);
        Task<string> GetContactNameById(int idContact);
    }
}

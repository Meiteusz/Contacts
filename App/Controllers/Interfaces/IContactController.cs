using Models;
using Models.DTO_s;
using Models.Entities;

namespace Controllers
{
    public interface IContactController
    {
        Task<ResponseId> RegisterContact(Contact contact);
        Task<ResponseId> UpdateContact(Contact contact);
        Task<ResponseQuery<Contact>> GetAllContacts();
        Task<Contact?> GetFirstOrDefault(int contactId);
        Task<ResponseQuery<Contact>> GetContactsByFilters(Contact userFilter);
        Task<bool> CheckMainEmail(int contactId, string email);
        Task<Response> SetContactMainEmail(int contactId, string mainEmailContact);
        Task<Response> DeleteContact(int contactId);
        Task<string> GetContactNameById(int contactId);
    }
}

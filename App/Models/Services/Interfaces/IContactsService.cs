using Models.DTO_s;
using Models.Entities;

namespace Models.Services.Interfaces
{
    public interface IContactsService
    {
        Task<ResponseId> CreateAsync(Contact contact);
        Task<Response> DeleteAsync(Contact contact);
        Task<Response> UpdateMainEmailContact(int contactId, string mainEmailContact);
        Task<ResponseId> UpdateContactAsync(Contact contact);
    }
}

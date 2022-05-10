using Models.DTO_s;
using Models.Entities;

namespace Models.Services.Interfaces
{
    public interface IContactsService
    {
        Task<Response> UpdateMainEmailContact(int idContact, string mainEmailContact);
        Task<ResponseId> UpdateContactAsync(User contact);
    }
}

using Models;
using Models.DTO_s;
using Models.Entities;
using Models.Queries;
using Models.Queries.Intefaces;
using Models.Services.Interfaces;

namespace Controllers
{
    public class UserController : IUserController
    {
        private readonly IContactsService _contactsService;
        private readonly IContactsQuery _contactsQuery;

        public UserController(IContactsQuery contactsQuery, IContactsService contactsService)
        {
            _contactsQuery = contactsQuery;
            _contactsService = contactsService;
        }

        public async Task<ResponseId> RegisterUser(User contact)
        {
            return await contact.CreateAsync();
        }

        public async Task<ResponseId> UpdateContact(User contact)
        {
            if (contact.MainEmail == null)
                contact.MainEmail = string.Empty;

            return await _contactsService.UpdateContactAsync(contact);
        }

        public async Task<ResponseQuery<User>> GetAllContacts()
        {
            return await _contactsQuery.GetAllAsync();
        }

        public async Task<User?> GetFirstOrDefault(int idContact)
        {
            return await _contactsQuery.GetFirstOrDefaultAsync(idContact);
        }

        public async Task<ResponseQuery<User>> GetContactsByFilters(User userFilter)
        {
            return await _contactsQuery.GetContactsByFilterAsync(userFilter);
        }

        public async Task<bool> CheckMainEmail(int idContact, string email)
            => await _contactsQuery.CheckMainEmail(idContact, email);

        public async Task<Response> SetMainEmailContact(int idContact, string mainEmailContact)
        {
            if (idContact.IsInvalidId())
                return new Response() { Message = "Contact not exists"};

            if (string.IsNullOrWhiteSpace(mainEmailContact))
                return new Response() { Message = "Invalid Email" };

            return await _contactsService.UpdateMainEmailContact(idContact, mainEmailContact);
        }

        public async Task<Response> DeleteContact(int idContact)
        {
            var contact = await _contactsQuery.GetFirstOrDefaultAsync(idContact);

            if (contact.IsNotNull())
            {
                return await contact.DeleteAsync();
            }
            return new Response() { Message = "Contact not exists" };
        }

        public async Task<string> GetContactNameById(int idContact)
            => await _contactsQuery.GetContactNameById(idContact);
    }
}
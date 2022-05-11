using Models;
using Models.DTO_s;
using Models.Entities;
using Models.Queries.Intefaces;
using Models.Services.Interfaces;

namespace Controllers
{
    public class ContactController : IContactController
    {
        private readonly IContactsService _contactsService;
        private readonly IContactsQuery _contactsQuery;

        public ContactController(IContactsQuery contactsQuery, IContactsService contactsService)
        {
            _contactsQuery = contactsQuery;
            _contactsService = contactsService;
        }

        public async Task<ResponseId> RegisterContact(Contact contact)
        {
            //validations
           contact.MainEmail = string.Empty;
           return  await _contactsService.CreateAsync(contact);
        }

        public async Task<ResponseId> UpdateContact(Contact contact)
        {
            if (contact.MainEmail == null)
                contact.MainEmail = string.Empty;

            return await _contactsService.UpdateContactAsync(contact);
        }

        public async Task<ResponseQuery<Contact>> GetAllContacts()
            => await _contactsQuery.GetAllAsync();

        public async Task<Contact?> GetFirstOrDefault(int contactId)
            => await _contactsQuery.GetFirstOrDefaultAsync(contactId);

        public async Task<ResponseQuery<Contact>> GetContactsByFilters(Contact userFilter)
            => await _contactsQuery.GetContactsByFilterAsync(userFilter);

        public async Task<bool> CheckMainEmail(int contactId, string email)
            => await _contactsQuery.CheckMainEmail(contactId, email);

        public async Task<Response> SetContactMainEmail(int contactId, string mainEmailContact)
        {
            if (contactId.IsInvalidId())
                return new Response() { Message = "Contact not exists"};

            if (string.IsNullOrWhiteSpace(mainEmailContact))
                return new Response() { Message = "Invalid Email" };

            return await _contactsService.UpdateMainEmailContact(contactId, mainEmailContact);
        }

        public async Task<Response> DeleteContact(int contactId)
        {
            var contact = await _contactsQuery.GetFirstOrDefaultAsync(contactId);

            if (contact.IsNotNull())
            {
                return await _contactsService.DeleteAsync(contact);
            }
            return new Response() { Message = "Contact not exists" };
        }

        public async Task<string> GetContactNameById(int contactId)
            => await _contactsQuery.GetContactNameById(contactId);
    }
}
using Microsoft.EntityFrameworkCore;
using Models.DTO_s;
using Models.Entities;
using Models.Queries.Intefaces;
using Models.Services.Interfaces;

namespace Models.Services
{
    public class ContactsService : IContactsService
    {
        private readonly IContactsQuery _contactsQuery;

        public ContactsService(IContactsQuery contactsQuery)
        {
            this._contactsQuery = contactsQuery;
        }

        public async Task<ResponseId> CreateAsync(Contact contact)
        {
            try
            {
                await contact.Validate(contact);

                using (var context = new ContactsContext())
                {
                    await context.AddAsync(contact);
                    var response = await contact.SaveChangesAsync(context);

                    if (response.Success)
                    {
                        await contact.SavedChangesAsync();
                        return new ResponseId() { Success = true, IdReturn = contact.Id.ToLong() };
                    }
                    return response;
                }
            }
            catch (Exception ex)
            {
                return new ResponseId() { Message = ex.Message };
            }
        }

        public async Task<Response> UpdateMainEmailContact(int contactId, string mainEmailContact)
        {
            try
            {
                using (var context = new ContactsContext())
                {
                    var contact = await _contactsQuery.GetFirstOrDefaultAsync(contactId);

                    contact.MainEmail = mainEmailContact;
                    context.Users.Update(contact);
                    return await context.ResponseSaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                return new Response() { Message = ex.Message };
            }
        }

        public async Task<Response> DeleteAsync(Contact contact)
        {
            try
            {
                using (var context = new ContactsContext())
                {
                    context.Users.Remove(contact);
                    var response = await contact.SaveChangesAsync(context);

                    if (response.Success)
                    {
                        return new Response() { Success = true };
                    }
                    return response;
                }
            }
            catch (Exception ex)
            {
                return new Response() { Message = ex.Message };
            }
        }

        public async Task<ResponseId> UpdateContactAsync(Contact contact)
        {
            try
            {
                using (var context = new ContactsContext())
                {
                    context.Entry(contact).State = EntityState.Modified;
                    var response = await context.ResponseSaveChangesAsync();
                    response.IdReturn = contact.Id;
                    return response;
                }
            }
            catch (Exception ex)
            {
                return new ResponseId() { Message = ex.Message };
            }
        }
    }
}

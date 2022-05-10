using Models.DTO_s;

namespace Models
{
    public abstract class EntityBase<T>
    {
        public async virtual Task<Response> Validate(T entity)
        {
            if (entity != null)
            {
                return new Response() { Success = true };
            }
            return new Response();
        }

        public async virtual Task<ResponseId> SaveChangesAsync(ContactsContext context)
        {
            context.ValidateStateOfEntity(this);
            return await context.ResponseSaveChangesAsync();
        }

        public async virtual Task<ResponseId> CreateAsync() => new ResponseId();

        public async virtual Task<Response> SavedChangesAsync() => new Response();

        public async virtual Task<Response> DeleteAsync() => new Response();
        public async virtual Task<Response> DeletedAsync() => new Response();
    }
}

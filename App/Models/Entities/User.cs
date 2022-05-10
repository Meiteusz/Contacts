using Models.DTO_s;
using Models.Queries;
using System.ComponentModel.DataAnnotations;

namespace Models.Entities
{
    public class User : EntityBase<User>
    {
        #region Properties

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(70)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Company { get; set; }

        [StringLength(200)]
        public string MainEmail { get; set; }

        [StringLength(15)]
        public string PersonalPhone { get; set; }

        [StringLength(15)]
        public string CommercialPhone { get; set; }

        #endregion

        public async override Task<Response> Validate(User entity)
        {
            return await base.Validate(entity);
        }

        public async override Task<ResponseId> CreateAsync()
        {
            try
            {
                await this.Validate(this);

                using (var context = new ContactsContext())
                {
                    await context.AddAsync(this);
                    var response = await base.SaveChangesAsync(context);

                    if (response.Success)
                    {
                        await base.SavedChangesAsync();
                        return new ResponseId() { Success = true, IdReturn = this.Id.ToLong() };
                    }
                    return response;
                }
            }
            catch (Exception ex)
            {
                return new ResponseId() { Message = ex.Message };
            }
        }

        public async override Task<Response> SavedChangesAsync()
        {
            return await base.SavedChangesAsync();
        }

        public override async Task<Response> DeleteAsync()
        {
            try
            {
                using (var context = new ContactsContext())
                {
                    context.Users.Remove(this);
                    var response = await base.SaveChangesAsync(context);

                    if (response.Success)
                    {
                        await base.DeletedAsync();
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

        public override async Task<Response> DeletedAsync()
        {
            return await base.DeletedAsync();
        }
    }
}

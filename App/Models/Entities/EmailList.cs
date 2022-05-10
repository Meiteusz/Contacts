using Microsoft.EntityFrameworkCore;
using Models.DTO_s;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    [Index(nameof(Email), IsUnique = true)]
    public class EmailList : EntityBase<EmailList>
    {
        #region Properties

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Email { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        #endregion

        public async override Task<ResponseId> CreateAsync()
        {
            try
            {
                using (var contactsContext = new ContactsContext())
                {
                    await contactsContext.AddAsync(this);
                    var response = await base.SaveChangesAsync(contactsContext);

                    if (response.Success)
                    {
                        await base.SavedChangesAsync();
                        return new ResponseId() { Success = true, IdReturn = this.Id.ToLong()};
                    }
                    return new ResponseId();
                }
            }
            catch (Exception ex)
            {
                return new ResponseId() { Message = ex.Message };
            }
        }
    }
}

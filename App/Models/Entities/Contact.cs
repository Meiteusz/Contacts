using Models.DTO_s;
using Models.Queries;
using System.ComponentModel.DataAnnotations;

namespace Models.Entities
{
    public class Contact : EntityBase<Contact>
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

        public async override Task<Response> Validate(Contact entity)
        {
            return await base.Validate(entity);
        }

        public async override Task<Response> SavedChangesAsync()
        {
            return await base.SavedChangesAsync();
        }
    }
}

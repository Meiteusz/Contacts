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
        public Contact User { get; set; }

        #endregion
    }
}

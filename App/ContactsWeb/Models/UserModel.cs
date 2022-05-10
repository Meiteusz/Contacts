using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ContactsWeb.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Company { get; set; }

        [DisplayName("Main Email")]
        public string MainEmail { get; set; }

        [DisplayName("Personal Phone")]
        public string PersonalPhone { get; set; }

        [DisplayName("Commercial Phone")]
        public string CommercialPhone { get; set; }
    }
}

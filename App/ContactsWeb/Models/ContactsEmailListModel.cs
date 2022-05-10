namespace ContactsWeb.Models
{
    public class ContactsEmailListModel
    {
        public UserModel ContactModel { get; set; } = new UserModel();
        public IEnumerable<EmailListModel> ContactsEmailList { get; set; } = new List<EmailListModel>();
        public int idEmail { get; set; }
        public bool IsMainEmail { get; set; }
    }
}

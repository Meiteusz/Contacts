namespace ContactsWeb.Models
{
    public class ContactsEmailListModel
    {
        public ContactModel ContactModel { get; set; } = new ContactModel();
        public IEnumerable<EmailListModel> ContactsEmailList { get; set; } = new List<EmailListModel>();
        public int idEmail { get; set; }
        public bool IsMainEmail { get; set; }
    }
}

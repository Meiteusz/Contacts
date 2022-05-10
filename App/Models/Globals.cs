namespace Models
{
    public class Globals
    {
        private static int Contact { get; set; }

        public static void SetContact(int value) => Contact = value;
        public static int GetContact() => Contact;
    }
}

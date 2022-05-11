using AutoMapper;
using ContactsWeb.Models;
using Models.Entities;

namespace ContactsWeb
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Contact, ContactModel>().ReverseMap();
            CreateMap<EmailList, EmailListModel>().ReverseMap();
        }

        // Provisionally code below
        
        public static ContactsEmailListModel UniqueUserModelReverseMap(Contact user)
        {
            var userModel = new ContactModel()
            {
                Id = user.Id,
                Name = user.Name,
                Company = user.Company,
                MainEmail = user.MainEmail,
                CommercialPhone = user.CommercialPhone,
                PersonalPhone = user.PersonalPhone
            };
            return new ContactsEmailListModel() { ContactModel = userModel };
        }

        public static IEnumerable<ContactModel> UserModelReverseMap(List<Contact> userList)
        {
            var contactsModel = new List<ContactModel>();

            foreach (var user in userList)
            {
                var contactModel = new ContactModel();
                contactModel.Id = user.Id;
                contactModel.Name = user.Name;
                contactModel.Company = user.Company;
                contactModel.MainEmail = user.MainEmail;
                contactModel.PersonalPhone = user.PersonalPhone;
                contactModel.CommercialPhone = user.CommercialPhone;
                contactsModel.Add(contactModel);
            }
            return contactsModel;
        }

        public static IEnumerable<EmailListModel> EmailListModelReverseMap(List<EmailList> emailList)
        {
            var EmailListModel = new List<EmailListModel>();

            foreach (var email in emailList)
            {
                var emailModel = new EmailListModel();
                emailModel.Id = email.Id;
                emailModel.Email = email.Email;
                EmailListModel.Add(emailModel);
            }
            return EmailListModel;
        }
    }
}

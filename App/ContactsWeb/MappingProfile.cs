using AutoMapper;
using ContactsWeb.Models;
using Models.Entities;

namespace ContactsWeb
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<EmailList, EmailListModel>().ReverseMap();
        }

        public static ContactsEmailListModel UniqueUserModelReverseMap(User user)
        {
            var userModel = new UserModel()
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

        public static IEnumerable<UserModel> UserModelReverseMap(List<User> userList)
        {
            var contactEmailListModel = new List<UserModel>();

            foreach (var item in userList)
            {
                var contactsModel = new UserModel();
                contactsModel.Id = item.Id;
                contactsModel.Name = item.Name;
                contactsModel.Company = item.Company;
                contactsModel.MainEmail = item.MainEmail;
                contactsModel.PersonalPhone = item.PersonalPhone;
                contactsModel.CommercialPhone = item.CommercialPhone;
                contactEmailListModel.Add(contactsModel);
            }
            return contactEmailListModel;
        }
    }
}

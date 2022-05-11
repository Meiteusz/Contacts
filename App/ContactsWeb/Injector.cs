using Controllers;
using Models;
using Models.Queries;
using Models.Queries.Intefaces;
using Models.Services;
using Models.Services.Interfaces;

namespace ContactsWeb
{
    public class Injector
    {
        public static void InjectService(IServiceCollection service)
        {
            service.AddScoped<IContactController, ContactController>();
            service.AddScoped<IEmailListController, EmailListController>();

            service.AddScoped<IContactsService, ContactsService>();
            service.AddScoped<IEmailListService, EmailListService>();

            service.AddScoped<IContactsQuery, ContactsQuery>();
            service.AddScoped<IEmailListQuery, EmailListQuery>();
            service.AddScoped<IQueryEntityHelper, QueryEntityHelper>();
        }
    }
}

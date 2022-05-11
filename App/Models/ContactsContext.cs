using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models.DTO_s;
using Models.Entities;

namespace Models
{
    public class ContactsContext : DbContext
    {
        public DbSet<Contact> Users { get; set; }
        public DbSet<EmailList> EmailList { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                                              .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                              .AddJsonFile("appsettings.json")
                                              .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("ContactsDefaultConnection"));
        }

        public async Task<ResponseId> ResponseSaveChangesAsync()
        {
            try
            {
                var changedRecords = await this.SaveChangesAsync();

                if (changedRecords > 0)
                {
                    return new ResponseId() { Success = true, Message = "Success" };
                }
                return new ResponseId();
            }
            catch (Exception ex)
            {
                return new ResponseId() { Message = ex.Message};
            }
        }
        public void ValidateStateOfEntity<T>(T entity)
        {
            if (this.Entry(entity).State == EntityState.Detached)
                throw new Exception(string.Format("Can't save the entity, state is invalid. State: {0}", EntityState.Detached.ToString()));
        }
    }
}
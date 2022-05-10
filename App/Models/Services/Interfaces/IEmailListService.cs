namespace Models.Services.Interfaces
{
    public interface IEmailListService
    {
        Task<ResponseId> UpdateEmailAsync(int idEmail, string email);
    }
}

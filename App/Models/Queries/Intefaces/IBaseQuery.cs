using Models.DTO_s;

namespace Models.Queries.Intefaces
{
    public interface IBaseQuery<T>
    {
        Task<ResponseQuery<T>> GetAllAsync();
        Task<T?> GetFirstOrDefaultAsync(int? id);
    }
}

namespace Models.DTO_s
{
    public class ResponseQuery<T> : Response
    {
        public IEnumerable<T> Results { get; set; } = new List<T>();
    }
}

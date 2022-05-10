namespace Models.DTO_s
{
    public class ResponseData<T> : Response
    {
        public T? Data { get; set; }
    }
}

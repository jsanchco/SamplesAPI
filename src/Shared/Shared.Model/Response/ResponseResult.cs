namespace Shared.Model.Response
{
    public class ResponseResult<T>
    {
        public bool Succesful { get; set; }
        public Error Error { get; set; }
        public T Data { get; set; }
    }
}

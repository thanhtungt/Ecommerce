namespace Utilities.ServiceResult
{
    public class ServiceResult<T>
    {
        public bool IsSuccessed { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
    }
}

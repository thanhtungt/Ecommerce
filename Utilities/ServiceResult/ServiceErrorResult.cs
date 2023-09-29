namespace Utilities.ServiceResult
{
    public class ServiceErrorResult<T> : ServiceResult<T>
    {
        public ServiceErrorResult()
        {
            IsSuccessed = false;
        }
        public ServiceErrorResult(string ms)
        {
            IsSuccessed = false;
            Message = ms;
        }
    }
}

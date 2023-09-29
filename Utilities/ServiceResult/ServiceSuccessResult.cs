namespace Utilities.ServiceResult
{
    public class ServiceSuccessResult<T> : ServiceResult<T>
    {
        public ServiceSuccessResult()
        {
            IsSuccessed = true;
        }

        public ServiceSuccessResult(T obj)
        {
            IsSuccessed= true;
            Result = obj;
        }
    }
}

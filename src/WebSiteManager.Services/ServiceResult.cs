using System.Collections.Generic;
using System.Linq;

namespace WebSiteManager.Services
{
    public class ServiceResult
    {
        public ServiceResult()
        {
            Errors = new Dictionary<ErrorType, ServiceResultError>();
        }

        public Dictionary<ErrorType, ServiceResultError> Errors { get; set; }

        public ServiceResult AddError(ErrorType type, ServiceResultError error)
        {
            Errors.Add(type, error);

            return this;
        }

        public bool HasErrors => Errors.Any();
    }

    public class ServiceResult<TData> : ServiceResult
    {
        public TData Data { get; set; }
    }
}
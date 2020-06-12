using System.Collections.Generic;
using System.Linq;

namespace WebSiteManager.Services
{
    public class ServiceResult<TData>
    {
        public ServiceResult()
        {
            Errors = new Dictionary<ErrorType, ServiceResultError>();
        }

        public Dictionary<ErrorType, ServiceResultError> Errors { get; set; }

        public ServiceResult<TData> AddError(ErrorType type, ServiceResultError error)
        {
            Errors.Add(type, error);

            return this;
        }

        public TData Data { get; set; }

        public bool HasErrors => Errors.Any();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using CleanEvoBPM.Application.Common.Exceptions;

namespace CleanEvoBPM.WebAPI.Filters
{
    public static class BusinessLogicExceptionUtils
    {
        public static HttpStatusCode GetStatusCodeFromBusinessLogicExceptionCode(BusinessExceptionCode code)
        {
            switch (code)
            {
                case BusinessExceptionCode.NOT_FOUND:
                    return HttpStatusCode.NotFound;
                case BusinessExceptionCode.NOT_ACTIVATED:
                    return HttpStatusCode.Unauthorized;
                default:
                    return HttpStatusCode.BadRequest;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.Common.Exceptions
{
    public class BusinessLogicException : Exception
    {
        public string FriendlyMessage { get; set; }
        public BusinessExceptionCode Code { get; set; }
        public BusinessLogicException(string message, BusinessExceptionCode code)
        {
            FriendlyMessage = message;
            Code = code;
        }
    }
}

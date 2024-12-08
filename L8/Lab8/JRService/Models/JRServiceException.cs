using System;

namespace JRService.Models
{
    public class JRServiceException : Exception
    {
        public JRServiceException(ErrorCode code)
            : base()
        {
            Code = code;
        }

        public ErrorCode Code { get; }
    }
}

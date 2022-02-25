using HexaEmployee.Domain.Exceptions;
using System.Net;

namespace HexaEmployee.Api.Services
{
    public static class ErrorCodeMapper
    {
        public static HttpStatusCode Map(string errorCode) => errorCode switch
        {
            ErrorCode.InvalidData => HttpStatusCode.BadRequest,
            _ => HttpStatusCode.InternalServerError,
        };
    }
}

using System;
using HeeP.Models.Application;

namespace HeeP.Models.Exceptions
{
    public class AuthorizationExeption : Exception
    {
        public AuthorizationError Error { get; set; }
        public AuthorizationExeption(AuthorizationError error, string message)
            : base(message)
        {
            Error = error;
        }

        public AuthorizationExeption(string message) : base(message)
        {
        }
    }
}

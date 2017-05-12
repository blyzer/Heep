using System;
using HeeP.Models.Application;
using HeeP.Models.BusinessModel;

namespace HeeP.Core.Rules
{
    public interface ISecurityRules : IDisposable
    {
        void Authorize(ApplicationResource resoruce, Permission permission, AuthenticationContext authContext);
        UserProfile Authenticate(AuthenticationContext authContext);
    }
}

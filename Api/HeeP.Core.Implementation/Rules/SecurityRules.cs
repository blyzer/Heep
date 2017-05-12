using HeeP.Common.Security;
using HeeP.Core.Repository;
using HeeP.Core.Rules;
using HeeP.Models.Application;
using HeeP.Models.BusinessModel;
using HeeP.Models.Exceptions;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Ncf360.Core.Implementation.Rules
{
    public class SecurityRules : ISecurityRules
    {
        private readonly ISecurityRepository _repository;

        public SecurityRules(ISecurityRepository repository)
        {
            _repository = repository;
        }

        public UserProfile Authenticate(AuthenticationContext authContext)
        {
            var user = _repository.Db.Set<User>()
            .Include(u => u.Roles.Select(r => r.Access))
            .FirstOrDefault(u => u.UserName == authContext.UserName);

            if (user == null)
            {
                throw new AuthorizationExeption(AuthorizationError.InvalidCredentials, "Invalid user name or password!");
            }

            var profile = AuthenticateWithToken(authContext.Token, user);
            return profile == null ? AuthenticateWithPassword(authContext.Token, user) : null;
        }

        private UserProfile AuthenticateWithPassword(string password, User user)
        {
            if (user.Password != EncriptionUtils.HashValueToString(password, user.IdentificationNumber))
            {
                throw new AuthorizationExeption(AuthorizationError.InvalidCredentials, "Invalid user name or password!");
            }

            var token = new UserToken
            {
                Token = Guid.NewGuid().ToString("N"),
                UserId = user.UserId,
                CreatedAt = DateTime.Now,
                DueDate = DateTime.Now.AddHours(1)

            };

            _repository.Db.Set<UserToken>().Add(token);
            _repository.SaveChanges();

            return ToUserProfile(user, token);
        }

        private UserProfile AuthenticateWithToken(string token, User user)
        {
            var currentToken = _repository.Db.Set<UserToken>()
                .Include(t => t.User)
                .FirstOrDefault(t => t.Token == token && t.UserId == user.UserId);

            if (currentToken == null)
            {
                return null;
            }

            currentToken.DueDate = DateTime.Now.AddHours(1);
            _repository.SaveChanges();

            return ToUserProfile(user, currentToken);
        }

        private UserProfile ToUserProfile(User user, UserToken token)
        {
            var accessList = user.Roles
               .SelectMany(r => r.Access)
               .GroupBy(a => a.AccessId)
               .Select(gp => gp.First())
               .ToList();

            accessList.ForEach(a => a.Roles = null);

            return new UserProfile
            {
                Token = token.Token,
                UserName = user.UserName,
                AccessList = accessList
            };
        }

        public virtual void Authorize(ApplicationResource resoruce, Permission permission, AuthenticationContext authContext)
        {
            var user = _repository.Db.Set<User>()
                .Include(u => u.Roles.Select(r => r.Access))
                .FirstOrDefault(u => u.UserName == authContext.UserName);

            if (user == null)
            {
                throw new AuthorizationExeption("Unauthorized Access");
            }

            var access = user.Roles.SelectMany(r => r.Access);
            if (access.All(a => a.Description != resoruce.ToString()))
            {
                throw new AuthorizationExeption("Unauthorized Access");
            }
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}

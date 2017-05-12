using System;
using System.Collections.Generic;
using System.Linq;
using HeeP.Common.Security;
using HeeP.Core.Repository;
using HeeP.Core.Rules;
using HeeP.Models.Application;
using HeeP.Models.BusinessModel;
using HeeP.Models.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace HeeP.Core.Implementation.Rules
{
    public abstract class BaseRules<T> : IRules<T>
        where T : class
    {
        protected IRepository<T> Repository;
        protected BaseRules(IRepository<T> repository)
        {
            Repository = repository;
        }

        public virtual ValidationException Validate(T model)
        {
            return new ValidationException(nameof(T));
        }

        public UserProfile Authenticate(AuthenticationContext authContext)
        {
            var user = Repository.Db.Set<User>()
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

            Repository.Db.Set<UserToken>().Add(token);
            Repository.SaveChanges();

            return ToUserProfile(user, token);
        }

        private UserProfile AuthenticateWithToken(string token, User user)
        {
            var currentToken = Repository.Db.Set<UserToken>()
                .Include(t => t.User)
                .FirstOrDefault(t => t.Token == token && t.UserId == user.UserId);

            if (currentToken == null)
            {
                return null;
            }

            currentToken.DueDate = DateTime.Now.AddHours(1);
            Repository.SaveChanges();

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

        public virtual T Add(T model)
        {
            Repository.Add(model);
            Repository.SaveChanges();
            return model;
        }

        public virtual void Dispose()
        {
            Repository.Dispose();
        }

        public virtual T Get(int id)
        {
            return Repository.Get(id);
        }

        public virtual ICollection<T> GetAll()
        {
            return Repository.GetAll()
                .ToList();
        }

        public virtual T Modify(T model)
        {
            Repository.Modify(model);
            Repository.SaveChanges();
            return model;
        }

    }
}

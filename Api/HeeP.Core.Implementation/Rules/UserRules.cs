using HeeP.Common.Security;
using HeeP.Core.Repository;
using HeeP.Core.Rules;
using HeeP.Models.BusinessModel;
using System.Collections.Generic;
using System.Linq;

namespace HeeP.Core.Implementation.Rules
{
    public class UserRules : BaseRules<User>, IUserRules
    {
        public UserRules(IUserRepository repository)
            : base(repository)
        {
        }

        public override ICollection<User> GetAll()
        {
            var users = base.GetAll().ToList();
            users.ForEach(u => u.Password = null);
            return users;
        }

        public override User Get(int id)
        {
            var user = base.Get(id);
            user.Password = null;
            return user;
        }

        public override User Add(User model)
        {
            model.Password = EncriptionUtils.HashValueToString(model.Password, model.IdentificationNumber);
            return base.Add(model);
        }

        public override User Modify(User model)
        {
            User currentRole = Get(model.UserId);
            UpdateRoles(currentRole, model);

            if (!string.IsNullOrEmpty(model.Password))
            {
                model.Password = EncriptionUtils.HashValueToString(model.Password, model.IdentificationNumber);
            }

            Repository.Db.Entry(currentRole).CurrentValues.SetValues(model);
            Repository.SaveChanges();
            return model;
        }

        private void UpdateRoles(User currentUser, User updatedUser)
        {
            HandleRemovedRoles(currentUser, updatedUser);
            HandleAddedRoles(currentUser, updatedUser);
        }

        private void HandleAddedRoles(User currentUser, User updatedUser)
        {
            var context = Repository.Db.Set<Role>();
            var addedRoles = updatedUser.Roles.Where(au => currentUser.Roles.All(a => a.RoleId != au.RoleId));
            foreach (var role in addedRoles)
            {
                currentUser.Roles.Add(role);
                context.Attach(role);
            }
        }

        private void HandleRemovedRoles(User currentUser, User updatedUser)
        {
            for (int i = currentUser.Roles.Count - 1; i >= 0; i--)
            {
                var curentRole = currentUser.Roles.ElementAt(i);
                if (updatedUser.Roles.All(a => a.RoleId != curentRole.RoleId))
                {
                    currentUser.Roles.Remove(curentRole);
                }
            }
        }
    }
}

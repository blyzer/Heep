using HeeP.Core.Repository;
using HeeP.Core.Rules;
using HeeP.Models.BusinessModel;
using System.Linq;

namespace HeeP.Core.Implementation.Rules
{
    public class RoleRules : BaseRules<Role>, IRoleRules
    {

        public RoleRules(IRoleRepository repository) : base(repository)
        {
        }

        public override Role Modify(Role model)
        {
            Role currentRole = base.Get(model.RoleId);
            UpdateAcess(currentRole, model);
            UpdateUsers(currentRole, model);
            Repository.Db.Entry(currentRole).CurrentValues.SetValues(model);
            Repository.SaveChanges();
            return model;
        }

        private void UpdateAcess(Role currentRole, Role updatedRole)
        {
            HandleDeletedAccess(currentRole, updatedRole);
            HandleAddedAccess(currentRole, updatedRole);
        }

        private void HandleAddedAccess(Role currentRole, Role updatedRole)
        {
            var context = Repository.Db.Set<Access>();
            var addedAccess = updatedRole.Access.Where(au => currentRole.Access.All(a => a.AccessId != au.AccessId));
            foreach (var acess in addedAccess)
            {
                currentRole.Access.Add(acess);
                context.Attach(acess);
            }
        }

        private void HandleDeletedAccess(Role currentRole, Role updatedRole)
        {
            for (int i = currentRole.Access.Count - 1; i >= 0; i--)
            {
                var currentAcess = currentRole.Access.ElementAt(i);
                if (updatedRole.Access.All(a => a.AccessId != currentAcess.AccessId))
                {
                    currentRole.Access.Remove(currentAcess);
                }
            }
        }

        private void UpdateUsers(Role currentRole, Role updatedRole)
        {
            HandleDeletedUsers(currentRole, updatedRole);
            HandleAddedUsers(currentRole, updatedRole);
        }

        private void HandleAddedUsers(Role currentRole, Role updatedRole)
        {
            var context = Repository.Db.Set<User>();
            var addedUsers = updatedRole.Users.Where(au => currentRole.Users.All(a => a.UserId != au.UserId));
            foreach (var user in addedUsers)
            {
                currentRole.Users.Add(user);
                context.Attach(user);
            }
        }

        private void HandleDeletedUsers(Role currentRole, Role updatedRole)
        {
            for (int i = currentRole.Users.Count - 1; i >= 0; i--)
            {
                var currentUser = currentRole.Users.ElementAt(i);
                if (updatedRole.Users.All(a => a.UserId != currentUser.UserId))
                {
                    currentRole.Users.Remove(currentUser);
                }
            }
        }
    }
}

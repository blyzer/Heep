using System.Linq;
using HeeP.Core.Implementation.Repository;
using HeeP.Core.Repository;
using HeeP.Data;
using HeeP.Models.BusinessModel;
using Microsoft.EntityFrameworkCore;

namespace Ncf360.Core.Implementation.Repository
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(IDbContext context) : base(context)
        {

        }

        public override Role Get(params object[] arguments)
        {
            var roleId = (int)arguments[0];
            return GetIQueriable()
                .FirstOrDefault(r => r.RoleId == roleId);
        }

        public override IQueryable<Role> GetAll()
        {
            return GetIQueriable();
        }

        private IQueryable<Role> GetIQueriable()
        {
            return Db.Set<Role>()
                .Include(r => r.Access)
                .Include(r => r.Users);
        }
    }
}

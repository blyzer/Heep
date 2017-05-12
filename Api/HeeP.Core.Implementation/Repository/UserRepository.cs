using System.Linq;
using HeeP.Core.Implementation.Repository;
using HeeP.Core.Repository;
using HeeP.Data;
using HeeP.Models.BusinessModel;
using Microsoft.EntityFrameworkCore;

namespace Ncf360.Core.Implementation.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IDbContext context)
            : base(context)
        {

        }

        public override IQueryable<User> GetAll()
        {
            return GetIQueryable();

        }

        public override User Get(params object[] arguments)
        {
            var id = (int)arguments[0];
            return GetIQueryable().FirstOrDefault(u => u.UserId == id);
        }

        public IQueryable<User> GetIQueryable()
        {
            return Db.Set<User>()
                .Include(u => u.Roles);
        }
    }
}

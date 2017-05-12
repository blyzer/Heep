using System.Linq;
using HeeP.Core.Repository;
using HeeP.Data;
using HeeP.Models.BusinessModel;
using Microsoft.EntityFrameworkCore;

namespace HeeP.Core.Implementation.Repository
{
    public class SecurityRepository : BaseRepository<User>, ISecurityRepository
    {
        public SecurityRepository(ISecurityDbContext context) : base(context)
        {

        }

        public User Get(int id)
        {
            return Db.Set<User>()
                .Include(u => u.Roles.Select(r => r.Access))
                .FirstOrDefault(u => u.UserId == id);
        }
    }
}

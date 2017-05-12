using HeeP.Core.Implementation.Repository;
using HeeP.Core.Repository;
using HeeP.Data;
using HeeP.Models.BusinessModel;

namespace Ncf360.Core.Implementation.Repository
{
    public class AccessRepository : BaseRepository<Access>, IAccessRepository
    {
        public AccessRepository(IDbContext context)
            : base(context)
        {

        }
    }
}

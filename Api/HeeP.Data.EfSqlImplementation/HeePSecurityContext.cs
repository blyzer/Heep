using Microsoft.EntityFrameworkCore;

namespace HeeP.Data.EfSqlImplementation
{
    public class NHeePSecurityContext : HeePDbContext, ISecurityDbContext
    {
        public NHeePSecurityContext(DbContextOptions options) : base(options)
        {
        }
    }
}

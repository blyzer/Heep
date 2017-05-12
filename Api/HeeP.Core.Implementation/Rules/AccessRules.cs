using HeeP.Core.Repository;
using HeeP.Core.Rules;
using HeeP.Models.BusinessModel;

namespace HeeP.Core.Implementation.Rules
{
    public class AccessRules : BaseRules<Access>, IAccessRules
    {
        public AccessRules(IAccessRepository repository) : base(repository)
        {
        }
    }
}

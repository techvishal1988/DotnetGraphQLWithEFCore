using Framework.DataAccess.Repository;
using Framework.DataAccess;
using Framework.Entity;
using GraphQL.Types;

namespace Geography.Business.GraphQL
{
    public interface ITopLevelMutation
    {
        void RegisterField(ObjectGraphType graphType);
    }
}
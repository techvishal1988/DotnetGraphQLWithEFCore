using Geography.Business.State;
using Geography.DataAccess.Repository;
using GraphQL;
using GraphQL.Types;
using System.Threading.Tasks;

namespace Geography.Business.Country
{
    public class CountryType : ObjectGraphType<Entity.Entities.Country>
    {        
        public CountryType(IStateRepository stateRepository)
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.Name);
            Field(x => x.IsoCode);
            Field<ListGraphType<StateType>>("States");             
        }        
    }
}

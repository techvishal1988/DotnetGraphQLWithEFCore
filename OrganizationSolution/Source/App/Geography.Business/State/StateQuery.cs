using Framework.DataAccess.Repository;
using Framework.Service.Utilities.Criteria;
using Geography.Business.GraphQL;
using Geography.Business.State;
using Geography.DataAccess;
using Geography.DataAccess.Repository;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Geography.Business.State
{
    public class StateQuery : ITopLevelQuery
    {
        private readonly IStateRepository _stateRepository;
        public StateQuery(IStateRepository stateRepository)
        {
            _stateRepository = stateRepository;
        }
        public void RegisterField(ObjectGraphType graphType)
        {
            graphType.Field<ListGraphType<StateType>>("States")
            .ResolveAsync(async context => await ResolveStates(_stateRepository));

            graphType.Field<StateType>("state")
            .Argument<NonNullGraphType<IdGraphType>>("stateId", "id of the state")
            .ResolveAsync(ResolveState(_stateRepository)
            );
        }

        private static async Task<IEnumerable<Entity.Entities.State>> ResolveStates(IStateRepository repository)
        {
            FilterCriteria<Entity.Entities.State> filterCriteria = new FilterCriteria<Entity.Entities.State>();
            filterCriteria.Includes.Add(a => a.Country);
            return await repository.FetchByCriteriaAsync(filterCriteria).ConfigureAwait(false);
        }

        private static Func<IResolveFieldContext<object>, Task<object>> ResolveState(IStateRepository repository)
        {
            return async context =>
            {
                var id = context.GetArgument<long>("stateId");
                if (id > 0)
                {
                    return await repository.FetchByIdAsync(id);
                }
                else
                {
                    context.Errors.Add(new ExecutionError("Wrong value for id"));
                    return null;
                }
            };
        }
    }
}

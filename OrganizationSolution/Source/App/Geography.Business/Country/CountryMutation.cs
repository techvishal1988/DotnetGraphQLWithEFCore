using AutoMapper;
using Framework.DataAccess.Repository;
using Geography.Business.Country.Models;
using Geography.Business.GraphQL;
using Geography.Business.GraphQL.Model;
using Geography.DataAccess;
using Geography.DataAccess.Repository;
using Geography.Entity.Entities;
using GraphQL;
using GraphQL.Types;
using LinqKit;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Geography.Business.Country
{
    public class CountryMutation : ITopLevelMutation
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IStateRepository _stateRepository;
        private static IMapper _mapper;
        public CountryMutation(ICountryRepository countryRepository, IStateRepository stateRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _stateRepository = stateRepository;
            _mapper = mapper;
        }
        public void RegisterField(ObjectGraphType graphType)
        {
            graphType.Field<CountryType>("CreateCountry")
                .Argument<NonNullGraphType<CountryCreateInputType>>("country", "object of country")
                .ResolveAsync(ResolveCreateCountry());

            graphType.Field<CountryType>("UpdateCountry")
                .Argument<NonNullGraphType<CountryUpdateInputType>>("country", "object of country")
                .ResolveAsync(ResolveUpdateCountry());

            graphType.Field<StringGraphType>("deleteCountry")
            .Argument<NonNullGraphType<IdGraphType>>("countryId", "id of country")
            .ResolveAsync(ResolveDeleteCountry());
        }

        private Func<IResolveFieldContext<object>, System.Threading.Tasks.Task<object>> ResolveCreateCountry()
        {
            return async context =>
            {
                try
                {
                    var country = context.GetArgument<CountryCreateModel>("country");

                    var dbData = await _countryRepository.FetchByAsync(item => item.Name == country.Name, item => item.Name);
                    if (dbData.Any())
                    {
                        context.Errors.Add(new ExecutionError("Country already exists"));
                        return null;
                    }


                    var dbCountry = _mapper.Map<Entity.Entities.Country>(country);
                    var addedCountry = await _countryRepository.Insert(new[] { dbCountry }).ConfigureAwait(false);

                    return dbCountry;
                }
                catch (Exception ex)
                {
                    return null;
                }
            };
        }

        private Func<IResolveFieldContext<object>, System.Threading.Tasks.Task<object>> ResolveUpdateCountry()
        {
            return async context =>
            {
                var countryUpdateModel = context.GetArgument<CountryUpdateModel>("country");
                var dbData = await _countryRepository.FetchByAsync(item => item.Id == countryUpdateModel.Id, data => data.Id);
                if (!dbData.Any())
                {
                    context.Errors.Add(new ExecutionError("Couldn't find country in db."));
                    return null;
                }
                var dbCountry = _mapper.Map<Entity.Entities.Country>(countryUpdateModel);
                return await _countryRepository.Update(dbCountry);
            };
        }

        private Func<IResolveFieldContext<object>, System.Threading.Tasks.Task<object>> ResolveDeleteCountry()
        {
            return async context =>
            {
                var countryId = context.GetArgument<long>("countryId");

                var dbCountryId = await _countryRepository.FetchByAsync(item => item.Id == countryId, data => data.Id);

                if (!dbCountryId.Any())
                {
                    context.Errors.Add(new ExecutionError("Couldn't find country in db."));
                    return null;
                }
                var dbStateIds = await _stateRepository.FetchByAsync(item => item.CountryId == countryId, data => data.Id);
                await _countryRepository.RemoveCountryWithDependency(dbCountryId.First(), dbStateIds);                

                var res = new MutationResponse()
                {
                    Message = $"The country with the id: {countryId} has been successfully deleted from db."
                };
                return res.Message;
            };
        }


    }
}

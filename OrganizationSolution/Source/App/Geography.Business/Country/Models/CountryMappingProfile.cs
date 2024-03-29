﻿namespace Geography.Business.Country.Models
{
    using AutoMapper;
    using Entity.Entities;

    /// <summary>
    /// Defines the <see cref="CountryMappingProfile" />.
    /// </summary>
    public class CountryMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CountryMappingProfile"/> class.
        /// </summary>
        public CountryMappingProfile()
        {
            CreateMap<Country, CountryReadModel>();

            CreateMap<CountryCreateModel, Country>()
                .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<CountryUpdateModel, Country>();
        }
    }
}

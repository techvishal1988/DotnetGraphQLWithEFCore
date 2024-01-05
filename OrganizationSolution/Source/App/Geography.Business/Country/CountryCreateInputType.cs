using Geography.Business.State;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geography.Business.Country
{
    public class CountryCreateInputType : InputObjectGraphType
    {
        public CountryCreateInputType()
        {
            Name = "CountryCreateInput";            
            Field<NonNullGraphType<StringGraphType>>("name");
            Field<NonNullGraphType<StringGraphType>>("isoCode");
            Field<ListGraphType<StateCreateInputType>>("states");
        }
    }

    public class CountryUpdateInputType : InputObjectGraphType
    {
        public CountryUpdateInputType()
        {
            Name = "CountryUpdateInput";
            Field<NonNullGraphType<IdGraphType>>("Id");
            Field<StringGraphType>("name");
            Field<StringGraphType>("isoCode");
            Field<ListGraphType<StateUpdateInputType>>("states");
        }
    }
}

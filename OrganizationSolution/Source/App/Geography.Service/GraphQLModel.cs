using GraphQL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Geography.Service
{
    public class GraphQLModel
    {
        public string Query { get; set; }
        public Dictionary<string, object> Variables { get; set; }        
    }
}

﻿using Geography.Business.State.Models;
using System.Collections.Generic;

namespace Geography.Business.Country.Models
{

    /// <summary>
    /// Defines the <see cref="CountryCreateModel" />.
    /// </summary>
    public class CountryCreateModel
    {
        public CountryCreateModel()
        {
            States = new List<StateCreateModel>();
        }

        public string Name { get; set; }
        public string IsoCode { get; set; }

        public IEnumerable<StateCreateModel> States { get; set; }
    }
}

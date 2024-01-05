namespace Geography.Entity.Entities
{
    using Framework.Entity;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="Country" />.
    /// </summary>
    public class Country : EntityWithIdName
    {        
        /// <summary>
        /// Gets or sets the IsoCode.
        /// </summary>
        public string IsoCode { get; set; }

        /// <summary>
        /// Gets or sets the States.
        /// </summary>
        public List<State> States { get; set; }

    }
}

namespace Geography.DataAccess.Repository
{
    using Framework.DataAccess.Repository;
    using Geography.Entity.Entities;
    using LinqKit;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="StateRepository" />.
    /// </summary>
    public class StateRepository : GenericRepository<GeographyDbContext, State>, IStateRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StateQueryRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The dbContext<see cref="GeographyDbContext"/>.</param>
        public StateRepository(GeographyDbContext dbContext) :
            base(dbContext)
        {
        }                 
    }
}

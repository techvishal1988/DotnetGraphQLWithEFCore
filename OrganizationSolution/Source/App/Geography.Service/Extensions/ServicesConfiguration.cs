namespace Geography.Service
{
    using Framework.Configuration.Models;
    using Framework.Constant;
    using Geography.DataAccess;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Authentication.JwtBearer;

    /// <summary>
    /// Defines the <see cref="ServicesConfiguration" />.
    /// </summary>
    public static class ServicesConfiguration
    {
        public const  string AuthenticationScheme = "Bearer";

        /// <summary>
        /// The ConfigureClientServices.
        /// </summary>
        /// <param name="services">The services<see cref="IServiceCollection"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection ConfigureClientServices(this IServiceCollection services)
        {
            var v = string.Empty;
            var serviceProvider = services.BuildServiceProvider();
            var applicationOptions = serviceProvider.GetRequiredService<ApplicationOptions>();
            return services
                .ConfigureDbServices(applicationOptions.ConnectionString);

        }               

        public static IServiceCollection ConfigureAwsCongnitoSecurity(this IServiceCollection services)
        {
            services.AddCognitoIdentity();


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = "https://cognito-idp.us-west-2.amazonaws.com/us-west-2_JojgZLDdW";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = false
                };
            });
            return services;
        }
    }
}

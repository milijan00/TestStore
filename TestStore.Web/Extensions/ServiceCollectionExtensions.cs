using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;
using TestStore.Application.Usecases.Commands;
using TestStore.Application.Usecases.Queries;
using TestStore.Domain;
using TestStore.Implementation.DataAccess;
using TestStore.Implementation.Usecases.Ef.Commands;
using TestStore.Implementation.Usecases.Ef.Queries;
using TestStore.Implementation.Validators;
using TestStore.Web.Core;

namespace TestStore.Web.Extensions
{
    public static  class ServiceCollectionExtensions
    {
        public static void AddUsecases(this IServiceCollection services)
        {
            // usecases
            services.AddTransient<ICreateUsecaseCommand, EfCreateUsecaseComand>();
            services.AddTransient<IUpdateUsecaseCommand, EfUpdateUsecaseCommand>();
            services.AddTransient<IDeleteUsecaseCommand, EfDeleteUsecaseCommand>();
            services.AddTransient<IGetUsecasesQuery, EfGetUsecasesQuery>();
            services.AddTransient<IGetUsecaseQuery, EfGetUsecaseQuery>();



            // navigation links
            services.AddTransient<IGetNavLinksQuery, EfGetNavLinksQuery>();
            services.AddTransient<IGetNavLinkQuery, EfGetNavLinkQuery>();
            services.AddTransient<ICreateNavLinkCommand, EfCreateNavLinkCommand>();
            services.AddTransient<IUpdateNavLinkCommand, EfUpdateNavLinkCommand>();
            services.AddTransient<IDeleteNavLinkCommand, EfDeleteNavLinkCommand>();


            //users
            services.AddTransient<IGetUsersQuery, EfGetUsersQuery>();
            services.AddTransient<IGetUserQuery, EfGetUserQuery>();
            services.AddTransient<IUpdateUserCommand, EfUpdateUserCommand>();
            services.AddTransient<ICreateUserCommand, EfCreateUserCommand>();
            services.AddTransient<IDeleteUserCommand, EfDeleteUserCommand>();

            //products
            services.AddTransient<IGetProductsQuery, EfGetProductsQuery>();
            services.AddTransient<IGetProductQuery, EfGetProductQuery>();
            services.AddTransient<ICreateProductCommand, EfCreateProductCommand>();
            services.AddTransient<IUpdateProductCommand, EfUpdateProductCommand>();
            services.AddTransient<IDeleteProductCommand, EfDeleteProductCommand>();

            //categories
            services.AddTransient<IGetCategoriesQuery, EfGetCategoriesQuery>();
            services.AddTransient<IGetCategoryQuery, EfGetCategoryQuery>();
            services.AddTransient<ICreateCategoryCommand, EfCreateCategoryCommand>();
            services.AddTransient<IUpdateCategoryCommand, EfUpdateCategoryCommand>();
            services.AddTransient<IDeleteCategoryCommand, EfDeleteCategoryCommand>();

            //brands
            services.AddTransient<IGetBrandsQuery, EfGetBrandsQuery>();
            services.AddTransient<IGetBrandQuery, EfGetBrandQuery>();
            services.AddTransient<ICreateBrandCommand, EfCreateBrandCommand>();
            services.AddTransient<IUpdateBrandCommand, EfUpdateBrandCommand>();
            services.AddTransient<IDeleteBrandCommand, EfDeleteBrandCommand>();


            //cart
            services.AddTransient<IDeleteCartCommand, EfDeleteCartCommand>();
            services.AddTransient<ICreateCartCommand, EfCreateCartCommand>();

            // specifications 
            services.AddTransient<ICreateSpecificationCommand, EfCreateSpecificationCommand>();
            services.AddTransient<IUpdateSpecificationCommand, EfUpdateSpecificationCommand>();
            services.AddTransient<IDeleteSpecificationCommand, EfDeleteSpecificationCommand>();
            services.AddTransient<IGetSpecificationsQuery, EfGetSpecificationsQuery>();
            services.AddTransient<IGetSpecificationQuery, EfGetSpecificationQuery>();

            // specification values
            services.AddTransient<ICreateSpecificationValueCommand, EfCreateSpecificationValueCommand>();
            services.AddTransient<IUpdateSpecificationValueCommand, EfUpdateSpecificationValueCommand>();
            services.AddTransient<IUpdateProductSpecfiicationCommand, EfUpdateProductSpecificationCommand>();
            services.AddTransient<IDeleteSpecificationValueCommand, EfDeleteSpecificationValueCommand>();
            services.AddTransient<IDeleteSpecificationValueFromProductsCollectionCommand, EfDeleteSpecificationValueFromProductsCollectionCommand>();
            services.AddTransient<IGetSpecificationValuesQuery, EfGetSpecificationValuesQuery>();
            services.AddTransient<IGetAllSpecificationsValuesQuery, EfGetAllSpecificationsValuesQuery>();
            services.AddTransient<IGetProductsSpecificationsQuery, EfGetProductsSpecificationsQuery>();

            // product specification
            services.AddTransient<ICreateProductSpecificationCommand, EfCreateProductSpecificationCommand>();
            services.AddTransient<IDeleteProductSpecificationCommand, EfDeleteProductSpecificationCommand>();

            // roles
            services.AddTransient<ICreateRoleCommand, EfCreateRoleCommand>();
            services.AddTransient<IUpdateRoleCommand, EfUpdateRoleCommand>();
            services.AddTransient<IDeleteRoleCommand, EfDeleteRoleCommand>();
            services.AddTransient<IGetRolesQuery, EfGetRolesQuery>();
            services.AddTransient<IGetRoleQuery, EfGetRoleQuery>();
        }

        public static void AddValidators(this IServiceCollection services)
        {
            services.AddTransient<UsecasesBaseValidator>() ;
            services.AddTransient<UpdateUsecaseValidator>() ;

            services.AddTransient<CreateNavLinkValidator>() ;
            services.AddTransient<UpdateNavLinkValidator>() ;

            services.AddTransient<UpdateUserValidator>() ;
            services.AddTransient<CreateUserValidator>() ;

            services.AddTransient<CreateProductValidator>() ;
            services.AddTransient<UpdateProductValidator>() ;

            services.AddTransient<CategoryBaseValidator>() ;
            services.AddTransient<UpdateCategoryValidator>() ;

            services.AddTransient<BrandBaseValidator>() ;
            services.AddTransient<UpdateBrandValidator>() ;

            services.AddTransient<CartBaseValidator>();

            services.AddTransient<BaseSpecificationValidator>();
            services.AddTransient<UpdateSpecificationValidator>();

            services.AddTransient<SpecificationValueValidator>();
            services.AddTransient<UpdateSpecificationValueValidator>();

            services.AddTransient<ProductSpecificationValidator>();
            services.AddTransient<DeleteProductSpecificationValidator>();


            services.AddTransient<BaseRoleValidator>();
            services.AddTransient<UpdateRoleValidator>();
            services.AddTransient<UpdateProductSpecificationValidator>();
        }

        public static void AddJwt(this IServiceCollection services, AppSettings settings)
        {
            services.AddTransient(x =>
            {
                var context = x.GetService<TestStoreDbContext>();
                var settings = x.GetService<AppSettings>();

                return new JWTManager(context, settings.JWTSettings);
            });

            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = settings.JWTSettings.Issuer,
                    ValidateIssuer = true,
                    ValidAudience = "Any",
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.JWTSettings.AccessSecretKey)),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

        public static void AddActionUser(this IServiceCollection services)
        {
            services.AddTransient<IActionUser>(x =>
            {
                var accessor = x.GetService<IHttpContextAccessor>();
                var header = accessor.HttpContext.Request.Headers["Authorization"];

                var claims = accessor.HttpContext.User;

                if(claims == null || claims.FindFirst("UserId") == null)
                {
                    return new AnonymousUser();
                }

                var actor = new JWTUser
                {
                    //Email = claims.FindFirst("Email").Value,
                    Username = claims.FindFirst("Username").Value,
                    Id = Int32.Parse(claims.FindFirst("UserId").Value),
                    Identity = claims.FindFirst("Role").Value,
                    AllowedUsecasesIds = JsonConvert.DeserializeObject<List<int>>(claims.FindFirst("UseCases").Value)
                };

                return actor;
            });
        }
    }
}

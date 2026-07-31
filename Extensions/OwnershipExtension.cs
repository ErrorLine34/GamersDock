using GamersDock.Entities;
using GamersDock.Repositories;
using Microsoft.AspNetCore.Identity;

namespace GamersDock.Extensions
{
    public static class OwnershipExtensions
    {

        // Checks if the current user owns the profile specified by the profileId parameter.
        // Returns 403 Forbidden if the user does not own the profile.
        public static RouteHandlerBuilder RequireProfileOwnership(this RouteHandlerBuilder builder)
        {
            return builder.AddEndpointFilterFactory((factoryContext, next) =>
            {
                var parameters = factoryContext.MethodInfo.GetParameters();
                var profileIdIndex = Array.FindIndex(
                    parameters,
                    p => p.Name == "profileId" && p.ParameterType == typeof(int));

                if (profileIdIndex < 0)
                {
                    throw new InvalidOperationException(
                        $"RequireProfileOwnership was applied to '{factoryContext.MethodInfo.Name}', " +
                        "but it has no 'int profileId' parameter to check ownership of.");
                }

                return async invocationContext =>
                {
                    var profileId = invocationContext.GetArgument<int>(profileIdIndex);
                    var httpContext = invocationContext.HttpContext;

                    var profileRepo = httpContext.RequestServices.GetRequiredService<IUserProfileRepository>();
                    var userManager = httpContext.RequestServices.GetRequiredService<UserManager<Users>>();

                    var userId = userManager.GetUserId(httpContext.User);
                    if (userId is null || !await profileRepo.IsOwnedByUserAsync(profileId, userId))
                    {
                        return Results.Forbid();
                    }

                    return await next(invocationContext);
                };
            });
        }
    }
}
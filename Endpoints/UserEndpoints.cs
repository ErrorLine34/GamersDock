using GamersDock.Repositories;
using GamersDock.Dtos;
using GamersDock.Extensions;

namespace GamersDock.Endpoints
{
    public static class UserEndpoints
    {
        public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/users");

            // GET: api/users/me
            group.MapGet("/me", async (IUserRepository repo, HttpContext ctx) =>
            {
                var user = await repo.GetCurrentAsync(ctx.User);
                return user is null ? Results.NotFound() : Results.Ok(user.ToDto());
            });

            // PATCH: api/users/me
            group.MapPatch("/me", async (UpdateUserRequest request, IUserRepository repo, HttpContext ctx) =>
            {
                var updated = await repo.UpdateCurrentAsync(ctx.User, request);
                return updated ? Results.NoContent() : Results.NotFound();
            });

            return app;
        }
    }
}
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
            }).RequireAuthorization();

            // PATCH: api/users/me
            group.MapPatch("/me", async (UpdateUserRequest request, IUserRepository repo, HttpContext ctx) =>
            {
                var updated = await repo.UpdateCurrentAsync(ctx.User, request);
                return updated ? Results.NoContent() : Results.NotFound();
            }).RequireAuthorization();

            // ADMIN ENDPOINTS

            // GET: api/users
            group.MapGet("/", async (IUserRepository repo) =>
            {
                var users = await repo.GetAllAsync();
                return Results.Ok(users.Select(u => u.ToDto()));
            }).RequireAuthorization("AdminOnly");

            // DELETE: api/users/{userId}
            group.MapDelete("/{userId}", async (string userId, IUserRepository repo) =>
            {
                var deleted = await repo.DeleteUserAsync(userId);
                return deleted ? Results.NoContent() : Results.NotFound();
            }).RequireAuthorization("AdminOnly");

            return app;
        }
    }
}
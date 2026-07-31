using GamersDock.Repositories;
using GamersDock.Dtos;
using GamersDock.Entities;
using GamersDock.Extensions;
using Microsoft.AspNetCore.Identity;

namespace GamersDock.Endpoints
{
    public static class ProfileEndpoints
    {
        public static IEndpointRouteBuilder MapProfileEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/profiles");

            // GET: api/profiles
            group.MapGet("/", async (IUserProfileRepository repo) =>
            {
                var profiles = await repo.GetAllAsync();
                return Results.Ok(profiles.Select(p => p.ToDto()));
            }).RequireAuthorization();

            // GET: api/profiles/{profileId}
            group.MapGet("/{profileId}", async (int profileId, IUserProfileRepository repo) =>
            {
                var profile = await repo.GetAsync(profileId);
                return profile is null ? Results.NotFound() : Results.Ok(profile.ToDto());
            }).RequireAuthorization();

            // POST: api/profiles
            group.MapPost("/", async (CreateProfileRequest request, IUserProfileRepository repo, UserManager<Users> userManager, SignInManager<Users> signInManager, HttpContext ctx) =>
            {
                var user = await userManager.GetUserAsync(ctx.User);
                if (user is null) return Results.Unauthorized();

                var created = await repo.CreateAsync(user.Id, request);

                // If role is updated we refresh to ensure the new role is reflected in the current session
                await signInManager.RefreshSignInAsync(user);

                return Results.Created($"/api/profiles/{created.ProfileId}", created.ToDto());
            }).RequireAuthorization();

            // PUT: api/profiles/{profileId}
            group.MapPut("/{profileId}", async (int profileId, UpdateProfileRequest request, IUserProfileRepository repo) =>
            {
                var updated = await repo.UpdateAsync(profileId, request);
                return updated is null ? Results.NotFound() : Results.NoContent();
            }).RequireAuthorization().RequireProfileOwnership();

            // DELETE: api/profiles/{profileId}
            group.MapDelete("/{profileId}", async (int profileId, IUserProfileRepository repo) =>
            {
                var deleted = await repo.DeleteAsync(profileId);
                return deleted ? Results.NoContent() : Results.NotFound();
            }).RequireAuthorization().RequireProfileOwnership();

            // PATCH: api/profiles/{profileId}/avatar
            group.MapPatch("/{profileId}/avatar", async (int profileId, UpdateAvatarRequest request, IUserProfileRepository repo) =>
            {
                var updated = await repo.UpdateAvatarAsync(profileId, request);
                return updated ? Results.NoContent() : Results.NotFound();
            }).RequireAuthorization().RequireProfileOwnership();

            // GET: api/profiles/{profileId}/links
            group.MapGet("/{profileId}/links", async (int profileId, IAccountLinkRepository repo) =>
            {
                var links = await repo.GetAsync(profileId);
                return links is null ? Results.NotFound() : Results.Ok(links.ToDto());
            }).RequireAuthorization().RequireProfileOwnership();

            // PATCH: api/profiles/{profileId}/links
            group.MapPatch("/{profileId}/links", async (int profileId, UpdateAccountLinksRequest request, IAccountLinkRepository repo) =>
            {
                var updated = await repo.UpdateAsync(profileId, request);
                return updated is null ? Results.NotFound() : Results.Ok(updated.ToDto());
            }).RequireAuthorization().RequireProfileOwnership();

            return app;
        }
    }
}
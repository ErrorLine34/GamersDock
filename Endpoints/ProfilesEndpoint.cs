using GamersDock.Repositories;
using GamersDock.Dtos;
using GamersDock.Entities;
using GamersDock.Extensions;

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
            });

            // GET: api/profiles/{profileId}
            group.MapGet("/{profileId}", async (int profileId, IUserProfileRepository repo) =>
            {
                var profile = await repo.GetAsync(profileId);
                return profile is null ? Results.NotFound() : Results.Ok(profile.ToDto());
            });

            // POST: api/profiles
            group.MapPost("/", async (CreateProfileRequest request, IUserProfileRepository repo) =>
            {
                var created = await repo.CreateAsync(request);
                return Results.Created($"/api/profiles/{created.ProfileId}", created.ToDto());
            });

            // PUT: api/profiles/{profileId}
            group.MapPut("/{profileId}", async (int profileId, UpdateProfileRequest request, IUserProfileRepository repo) =>
            {
                var updated = await repo.UpdateAsync(profileId, request);
                return updated is null ? Results.NotFound() : Results.NoContent();
            });

            // DELETE: api/profiles/{profileId}
            group.MapDelete("/{profileId}", async (int profileId, IUserProfileRepository repo) =>
            {
                var deleted = await repo.DeleteAsync(profileId);
                return deleted ? Results.NoContent() : Results.NotFound();
            });

            // PATCH: api/profiles/{profileId}/avatar
            group.MapPatch("/{profileId}/avatar", async (int profileId, UpdateAvatarRequest request, IUserProfileRepository repo) =>
            {
                var updated = await repo.UpdateAvatarAsync(profileId, request);
                return updated ? Results.NoContent() : Results.NotFound();
            });

            // GET: api/profiles/{profileId}/links
            group.MapGet("/{profileId}/links", async (int profileId, IAccountLinkRepository repo) =>
            {
                var links = await repo.GetAsync(profileId);
                return links is null ? Results.NotFound() : Results.Ok(links.ToDto());
            });

            // PATCH: api/profiles/{profileId}/links
            group.MapPatch("/{profileId}/links", async (int profileId, UpdateAccountLinksRequest request, IAccountLinkRepository repo) =>
            {
                var updated = await repo.UpdateAsync(profileId, request);
                return updated is null ? Results.NotFound() : Results.Ok(updated.ToDto());
            });

            return app;
        }
    }
}
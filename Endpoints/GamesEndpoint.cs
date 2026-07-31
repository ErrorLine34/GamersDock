using GamersDock.Repositories;
using GamersDock.Dtos;
using GamersDock.Extensions;

namespace GamersDock.Endpoints
{
    public static class GameEndpoints
    {
        public static IEndpointRouteBuilder MapGameEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/games");

            // GET: api/games?profileId={profileId}
            // profileId is explicit here for now

            group.MapGet("/", async (int profileId, IGameRepository repo) =>
            {
                var games = await repo.GetLibraryAsync(profileId);
                return Results.Ok(games.Select(g => g.ToSummaryDto()));
            }).RequireAuthorization().RequireProfileOwnership();

            // GET: api/games/{id}
            group.MapGet("/{id}", async (int id, IGameRepository repo) =>
            {
                var game = await repo.GetByIdAsync(id);
                return game is null ? Results.NotFound() : Results.Ok(game.ToDetailDto());
            });

            // POST: api/games/{id}/library
            group.MapPost("/{id}/library", async (int id, int profileId, int platformId, IGameRepository repo) =>
            {
                var game = await repo.GetByIdAsync(id);
                if (game is null)
                {
                    return Results.NotFound();
                }
                // Add the game to the user's library
                var added = await repo.AddToLibraryAsync(id, profileId, platformId);
                return added ? Results.Ok(game.ToSummaryDto()) : Results.BadRequest(new { message = "Failed to add game to library." });
            }).RequireAuthorization().RequireProfileOwnership();

            // DELETE: api/games/{id}/library
            group.MapDelete("/{id}/library", async (int id, int profileId, int platformId, IGameRepository repo) =>
            {
                var game = await repo.GetByIdAsync(id);
                if (game is null)
                {
                    return Results.NotFound();
                }
                // Remove the game from the user's library
                var removed = await repo.DeleteFromLibraryAsync(id, profileId, platformId);
                return removed ? Results.NoContent() : Results.NotFound();
            }).RequireAuthorization().RequireProfileOwnership();

            // PATCH: api/games/{id}/status
            group.MapPatch("/{id}/status", async (int id, int profileId, UpdateStatusRequest request, IGameRepository repo) =>
            {
                var updated = await repo.UpdateStatusAsync(id, profileId, request);
                return updated ? Results.NoContent() : Results.NotFound();
            }).RequireAuthorization().RequireProfileOwnership();

            // PATCH: api/games/{id}/manualstatus
            group.MapPatch("/{id}/manualstatus", async (int id, int profileId, UpdateStatusRequest request, IGameRepository repo, DateTime manualdate) =>
            {
                var updated = await repo.UpdateStatusManualDateAsync(id, profileId, request, manualdate);
                return updated ? Results.NoContent() : Results.NotFound();
            }).RequireAuthorization().RequireProfileOwnership();

            // PATCH: api/games/{id}/rating
            group.MapPatch("/{id}/rating", async (int id, int profileId, UpdateRatingRequest request, IGameRepository repo) =>
            {
                var updated = await repo.UpdateRatingAsync(id, profileId, request);
                return updated ? Results.NoContent() : Results.NotFound();
            }).RequireAuthorization().RequireProfileOwnership();

            // PATCH: api/games/{id}/achievements/{achievementId}
            group.MapPatch("/{id}/achievements/{achievementId}", async (int id, string achievementId, int profileId, UpdateAchievementRequest request, IGameRepository repo) =>
            {
                var updated = await repo.ToggleAchievementAsync(id, achievementId, profileId, request);
                return updated ? Results.NoContent() : Results.NotFound();
            }).RequireAuthorization().RequireProfileOwnership();

            // POST: api/games/{id}/journal
            group.MapPost("/{id}/journal", async (int id, int profileId, CreateJournalEntryRequest request, IGameRepository repo) =>
            {
                var created = await repo.AddJournalEntryAsync(id, profileId, request);
                return Results.Created($"/api/games/{id}/journal/{created.JournalEntryId}", created.ToDto());
            }).RequireAuthorization().RequireProfileOwnership();

            // ADMIN ENDPOINTS

            // POST: api/games (admin)
            group.MapPost("/", async (CreateGameRequest request, IGameRepository repo) =>
            {
                var created = await repo.CreateAsync(request);
                return Results.Created($"/api/games/{created.GameId}", created.ToDetailDto());
            }).RequireAuthorization("AdminOnly");

            // DELETE: api/games/{id} (admin)
            group.MapDelete("/{id}", async (int id, IGameRepository repo) =>
            {
                var deleted = await repo.DeleteAsync(id);
                return deleted ? Results.NoContent() : Results.NotFound();
            }).RequireAuthorization("AdminOnly");

            return app;
        }
    }
}
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
            });

            // GET: api/games/{id}
            group.MapGet("/{id}", async (int id, IGameRepository repo) =>
            {
                var game = await repo.GetByIdAsync(id);
                return game is null ? Results.NotFound() : Results.Ok(game.ToDetailDto());
            });

            // PATCH: api/games/{id}/status
            group.MapPatch("/{id}/status", async (int id, int profileId, UpdateStatusRequest request, IGameRepository repo) =>
            {
                var updated = await repo.UpdateStatusAsync(id, profileId, request);
                return updated ? Results.NoContent() : Results.NotFound();
            });

            // PATCH: api/games/{id}/rating
            group.MapPatch("/{id}/rating", async (int id, int profileId, UpdateRatingRequest request, IGameRepository repo) =>
            {
                var updated = await repo.UpdateRatingAsync(id, profileId, request);
                return updated ? Results.NoContent() : Results.NotFound();
            });

            // PATCH: api/games/{id}/achievements/{achievementId}
            group.MapPatch("/{id}/achievements/{achievementId}", async (int id, int achievementId, int profileId, UpdateAchievementRequest request, IGameRepository repo) =>
            {
                var updated = await repo.ToggleAchievementAsync(id, achievementId, profileId, request);
                return updated ? Results.NoContent() : Results.NotFound();
            });

            // POST: api/games/{id}/journal
            group.MapPost("/{id}/journal", async (int id, int profileId, CreateJournalEntryRequest request, IGameRepository repo) =>
            {
                var created = await repo.AddJournalEntryAsync(id, profileId, request);
                return Results.Created($"/api/games/{id}/journal/{created.JournalEntryId}", created.ToDto());
            });

            return app;
        }
    }
}
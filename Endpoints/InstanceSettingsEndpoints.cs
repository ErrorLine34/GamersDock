using GamersDock.Repositories;
using GamersDock.Dtos;
using GamersDock.Extensions;

namespace GamersDock.Endpoints
{
    public static class InstanceSettingsEndpoints
    {
        public static IEndpointRouteBuilder MapInstanceSettingsEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/settings/instance").RequireAuthorization("AdminOnly");

            // GET: api/settings/instance
            group.MapGet("/", async (IInstanceSettingsRepository repo) =>
            {
                var settings = await repo.GetAsync();
                return Results.Ok(settings.ToDto());
            });

            // PATCH: api/settings/instance
            group.MapPatch("/", async (UpdateInstanceSettingsRequest request, IInstanceSettingsRepository repo) =>
            {
                var updated = await repo.UpdateAsync(request);
                return Results.Ok(updated.ToDto());
            });

            return app;
        }
    }
}
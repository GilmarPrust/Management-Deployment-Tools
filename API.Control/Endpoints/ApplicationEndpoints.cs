using API.Control.Models;

namespace API.Control.Endpoints
{
    public static class ApplicationEndpoints
    {
        private static readonly List<Application> applications = new();

        public static void MapApplicationEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/applications");

            group.MapGet("/", () => Results.Ok(applications));

            group.MapPost("/", (Application appData) =>
            {
                applications.Add(appData);
                return Results.Created($"/api/applications/{appData.Id}", appData);
            });

            group.MapGet("/{id:guid}", (Guid id) =>
            {
                var app = applications.FirstOrDefault(a => a.Id == id);
                return app is not null ? Results.Ok(app) : Results.NotFound();
            });

            group.MapDelete("/{id:guid}", (Guid id) =>
            {
                var app = applications.FirstOrDefault(a => a.Id == id);
                if (app is null) return Results.NotFound();
                applications.Remove(app);
                return Results.NoContent();
            });
        }
    }
}

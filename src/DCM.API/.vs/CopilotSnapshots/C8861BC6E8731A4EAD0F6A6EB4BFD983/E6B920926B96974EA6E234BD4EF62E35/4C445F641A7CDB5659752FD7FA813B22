using API.Control.Models;

namespace API.Control.Endpoints
{
    public static class ImageEndpoints
    {
        private static readonly List<Image> imagens = new();

        public static void MapImagenEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/imagens");

            group.MapGet("/", () => Results.Ok(imagens));

            group.MapPost("/", (Image img) =>
            {
                imagens.Add(img);
                return Results.Created($"/api/imagens/{img.Id}", img);
            });

            group.MapGet("/{id:guid}", (Guid id) =>
            {
                var img = imagens.FirstOrDefault(i => i.Id == id);
                return img is not null ? Results.Ok(img) : Results.NotFound();
            });

            group.MapDelete("/{id:guid}", (Guid id) =>
            {
                var img = imagens.FirstOrDefault(i => i.Id == id);
                if (img is null) return Results.NotFound();
                imagens.Remove(img);
                return Results.NoContent();
            });
        }
    }

}

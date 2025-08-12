namespace DCM.API.Helpers
{
    public static class MiddlewareRegistration
    {
        public static IApplicationBuilder UseCustomMiddlewares(this IApplicationBuilder app)
        {
            // Middleware para uso de arquivos estáticos
            app.UseStaticFiles();

            // Middleware para uso de CORS
            app.UseHttpsRedirection();
            app.UseCors();

            // Middleware para tratamento de erros
            app.UseExceptionHandler("/error");

            return app;
        }
    }
}

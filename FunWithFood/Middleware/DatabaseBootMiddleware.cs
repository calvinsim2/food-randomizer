using Microsoft.Data.SqlClient;

namespace FunWithFood.Middleware
{
    public class DatabaseBootMiddleware
    {
        private readonly RequestDelegate _next;

        public DatabaseBootMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (SqlException ex) when (IsDatabaseStartingError(ex))
            {
                context.Response.Redirect("/DatabaseBooting");
            }
        }

        private bool IsDatabaseStartingError(SqlException ex)
        {
            // General SQL Server error codes
            var generalErrors = new HashSet<int> { 18401, 17142, 4060, -2 };

            // Azure-specific error codes for paused or unavailable database
            var azureErrors = new HashSet<int> { 40613, 40197, 40501 };

            return generalErrors.Contains(ex.Number) || azureErrors.Contains(ex.Number);
        }
    }

}

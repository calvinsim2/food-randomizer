using FunWithFoodDomain.Constants;
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
            return ErrorCodes.SqlErrorCodes.Contains(ex.Number) || ErrorCodes.AzureSqlErrorCodes.Contains(ex.Number);
        }
    }

}

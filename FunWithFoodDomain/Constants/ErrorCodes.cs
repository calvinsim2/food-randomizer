namespace FunWithFoodDomain.Constants
{
    public static class ErrorCodes
    {
        // General SQL Server error codes
        public static HashSet<int> SqlErrorCodes = new HashSet<int> { 18401, 17142, 4060, -2 };

        // Azure-specific error codes for paused or unavailable database
        public static HashSet<int> AzureSqlErrorCodes = new HashSet<int> { 40613, 40197, 40501 };
    }
}

using Microsoft.Data.SqlClient;
using OrderLookup.Data.Sql.Factories;
using OrderLookup.Data.Sql.Managers;
using System.Diagnostics;

namespace OrderLookup.Data.Sql.Helpers
{
    public static class SqlHelpers
    {
        public static readonly RepoFactory _repoFactory;
        static SqlHelpers()
        {
            _repoFactory = new RepoFactory(new SqlSettingsManager());
        }
        public static async Task<bool> CheckDbConnection()
        {
            bool result = false;

            var baseRepo = _repoFactory.GetBaseRepo();

            var connectionString = baseRepo.ConnectionString;
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    result = true;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    result = false;
                }
                finally
                {
                    await connection.CloseAsync();
                }

            }
            return result;
        }
    }
}

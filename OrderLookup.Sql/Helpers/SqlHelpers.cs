using Microsoft.Data.SqlClient;
using OrderLookup.Sql.Factories;
using OrderLookup.Sql.Managers;
using OrderLookup.Sql.Repos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderLookup.Sql.Helpers
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

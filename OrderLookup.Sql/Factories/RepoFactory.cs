using OrderLookup.Sql.Managers;
using OrderLookup.Sql.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderLookup.Sql.Factories
{
    public class RepoFactory
    {
        private readonly SqlSettingsManager _sqlSettingsManager;

        public RepoFactory
            (
            SqlSettingsManager sqlSettingsManager
            )
        {
            _sqlSettingsManager = sqlSettingsManager;
        }

        public BaseRepo GetBaseRepo()
        {
            return new BaseRepo(_sqlSettingsManager);
        }
    }
}

using Microsoft.Extensions.Configuration;
using OrderLookup.Sql.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderLookup.Sql
{
    public class BaseRepo
    {
        public string ConnectionString { get; set; }
        private ConfigurationManager _configurationManager;
        private readonly SqlSettingsManager _sqlSettingsManager;

        public BaseRepo(SqlSettingsManager sqlSettingsManager)
        {
            _sqlSettingsManager = sqlSettingsManager;

            ConnectionString = _sqlSettingsManager.GetValue<string>("DefaultConnection");
        }

        public string ConcatStringFromList(List<string> listOfString)
        {
            listOfString.ForEach(x => x.Trim());

            return string.Join(",", listOfString.Select(i => $"'{i}'"));
        }

    }
}

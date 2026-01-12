using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderLookup.Helpers
{
    public static class BaseHelpers
    {
        public static string ConcatStringFromList(List<string> listOfString)
        {
            listOfString.ForEach(x => x.Trim());

            return string.Join(",", listOfString.Select(i => $"'{i}'"));
        }

        public static List<string> ParseTextToList(string text)
        {
            string[] delimiters = { "\n", "\r", "\r\n", " ", "'", "•", "\t" };

            List<string> tempList = new List<string>();

            // removes empty items after split for carriage returns
            tempList.AddRange(text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries));

            return tempList;
        }
    }

}

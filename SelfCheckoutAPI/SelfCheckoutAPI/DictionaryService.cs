using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfCheckoutAPI
{
    public static class DictionaryService
    {
        /// <summary>
        /// This method merges 2 dictionaries.
        /// </summary>
        /// <param name="destionation">Dictionary we would like to insert values into.</param>
        /// <param name="source">Dictionary that contains values we would like to insert.</param>
        public static void Merge(Dictionary<int, int> destionation, Dictionary<int, int> source)
        {
            foreach (var item in source)
            {
                if (!destionation.ContainsKey(item.Key))
                {
                    destionation.Add(item.Key, item.Value);
                }
                else
                {
                    destionation[item.Key] += item.Value;
                }
            }
        }
    }
}
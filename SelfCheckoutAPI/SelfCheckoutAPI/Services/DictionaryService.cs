using System.Collections.Generic;

namespace SelfCheckoutAPI
{
    public static class DictionaryService
    {
        /// <summary>
        /// This method merges 2 dictionaries.
        /// </summary>
        /// <param name="destionation">Dictionary we would like to insert values into.</param>
        /// <param name="source">Dictionary that contains values we would like to insert.</param>
        public static void Merge(Dictionary<string, int> destionation, Dictionary<string, int> source)
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
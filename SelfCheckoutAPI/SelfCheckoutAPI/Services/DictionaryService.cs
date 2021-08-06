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

        /// <summary>
        /// Creates a sorted and reversed list from a given dictionary that contains integer values.
        /// </summary>
        /// <param name="dict">Source dictionary.</param>
        /// <returns>Integer list that contains the key of the dicionary value times.</returns>
        public static List<int> CreateList(Dictionary<string, int> dict)
        {
            var list = new List<int>();
            foreach (var item in dict)
            {
                for (int i = 0; i < item.Value; i++)
                {
                    list.Add(int.Parse(item.Key));
                }
            }

            list.Sort();
            list.Reverse();
            return list;
        }
    }
}
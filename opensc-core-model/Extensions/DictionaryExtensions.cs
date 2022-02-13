using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Extensions
{
    public static class DictionaryExtensions
    {
        public static Dictionary<TKeyOut, TValueOut> Cast<TKeyOut, TValueOut, TKeyIn, TValueIn>(this IDictionary<TKeyIn, TValueIn> dictionary)
        {
            Dictionary<TKeyOut, TValueOut> outputDictionary = new Dictionary<TKeyOut, TValueOut>();
            foreach (KeyValuePair<TKeyIn, TValueIn> kvp in dictionary)
                outputDictionary.Add((TKeyOut)((object)kvp.Key), (TValueOut)((object)kvp.Key));
            return outputDictionary;
        }
    }
}

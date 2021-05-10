using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Signals
{

    public static class RecursionChainHelpers
    {

        public static List<T> ExtendRecursionChainT<T>(this List<T> recursionChain, T newElement) {
            if (recursionChain == null)
                recursionChain = new List<T>();
            recursionChain.Add(newElement);
            return recursionChain;
        }

        public static List<object> ExtendRecursionChain(this List<object> recursionChain, object newElement)
            => ExtendRecursionChainT(recursionChain, newElement);

        public static List<T> CreateRecursionChainT<T>(T firstElement)
        {
            List<T> recursionChain = new List<T>();
            recursionChain.Add(firstElement);
            return recursionChain;
        }

        public static List<object> CreateRecursionChain(object firstElement)
            => CreateRecursionChainT(firstElement);

    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Persistence
{
    internal static class DatabasePersisterHelpers
    {
        public static object[] ExtendIndices(object[] original, int arrayDimension)
        {
            object[] extendedIndices = new object[arrayDimension + 1];
            for (int i = 0; i < arrayDimension; i++)
                extendedIndices[i] = original[i];
            return extendedIndices;
        }
    }
}

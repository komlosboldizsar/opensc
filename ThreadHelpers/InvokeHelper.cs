using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadHelpers
{

    public class InvokeHelper
    {

        private static InvokeHelperForm instance = null;

        public static void Init()
        {
            if (instance != null)
                throw new AlreadyInitializedException();
            instance = new InvokeHelperForm();
        }

        public delegate void InvokedMethod();

        public static void Invoke(InvokedMethod method)
        {

            if (instance == null)
                throw new NotInitializedException();

            if (instance.InvokeRequired)
            {
                instance.Invoke(method);
                return;
            }
            method();

        }

        public class NotInitializedException : Exception
        {
            public NotInitializedException()
            { }
        }

        public class AlreadyInitializedException : Exception
        {
            public AlreadyInitializedException()
            { }
        }

    }

}

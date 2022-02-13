using BMDSwitcherAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace BMD.Switcher
{

    public static class SourceHelpers
    {

        public static IBMDSwitcherInputIterator GetInputIterator(this IBMDSwitcher switcher)
        {
            IBMDSwitcherInputIterator inputIterator = null;
            IntPtr inputIteratorPtr;
            Guid inputIteratorIID = typeof(IBMDSwitcherInputIterator).GUID;
            switcher.CreateIterator(ref inputIteratorIID, out inputIteratorPtr);
            if (inputIteratorPtr != IntPtr.Zero)
                inputIterator = (IBMDSwitcherInputIterator)Marshal.GetObjectForIUnknown(inputIteratorPtr);
            return inputIterator;
        }

        public static IBMDSwitcherInput GetSource(this IBMDSwitcher switcher, long inputId)
        {
            
            IBMDSwitcherInputIterator inputIterator = switcher.GetInputIterator();
            if (inputIterator == null)
                return null;

            IBMDSwitcherInput input;
            inputIterator.Next(out input);
            while (input != null)
            {
                long inputIdFound;
                input.GetInputId(out inputIdFound);
                if (inputIdFound == inputId)
                    return input;
                inputIterator.Next(out input);
            }

            return null;

        }

        public static List<IBMDSwitcherInput> GetPorts(this IBMDSwitcher apiSwitcher)
        {

            IBMDSwitcherInputIterator inputIterator = apiSwitcher.GetInputIterator();
            if (inputIterator == null)
                return null;

            List<IBMDSwitcherInput> inputs = new List<IBMDSwitcherInput>();

            IBMDSwitcherInput input;
            inputIterator.Next(out input);
            while (input != null)
            {
                inputs.Add(input);
                inputIterator.Next(out input);
            }

            return inputs;

        }

        public static long GetSourceId(this IBMDSwitcherInput input)
        {
            input.GetInputId(out long inputId);
            return inputId;
        }

    }

}

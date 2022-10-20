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

    public static class MutlviewHelpers
    {

        public static IBMDSwitcherMultiViewIterator GetMultiviewIterator(this IBMDSwitcher switcher)
        {
            Guid mvIteratorIID = typeof(IBMDSwitcherMultiViewIterator).GUID;
            switcher.CreateIterator(ref mvIteratorIID, out IntPtr mvIteratorPtr);
            if (mvIteratorPtr == IntPtr.Zero)
                return null;
            return (IBMDSwitcherMultiViewIterator)Marshal.GetObjectForIUnknown(mvIteratorPtr);
        }

        public static IBMDSwitcherMultiView GetMultiview(this IBMDSwitcher switcher, int index)
        {
            IBMDSwitcherMultiViewIterator mutlviewIterator = switcher.GetMultiviewIterator();
            if (mutlviewIterator == null)
                return null;
            mutlviewIterator.Next(out IBMDSwitcherMultiView multiview);
            int i = 0;
            while (mutlviewIterator != null)
            {
                if (i == index)
                    return multiview;
                mutlviewIterator.Next(out multiview);
                i++;
            }
            return null;
        }

        public static List<IBMDSwitcherMultiView> GetAllMultiviews(this IBMDSwitcher switcher)
        {
            IBMDSwitcherMultiViewIterator multiviewIterator = switcher.GetMultiviewIterator();
            if (multiviewIterator == null)
                return null;
            List<IBMDSwitcherMultiView> multiviews = new();
            multiviewIterator.Next(out IBMDSwitcherMultiView multiview);
            while (multiview != null)
            {
                multiviews.Add(multiview);
                multiviewIterator.Next(out multiview);
            }
            return multiviews;
        }

    }

}
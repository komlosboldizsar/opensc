using BMDSwitcherAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BMD.Switcher
{

    public static class MixEffectBlockHelpers
    {

        public static IBMDSwitcherMixEffectBlockIterator GetMixEffectBlockIterator(this IBMDSwitcher switcher)
        {
            IBMDSwitcherMixEffectBlockIterator meIterator = null;
            IntPtr meIteratorPtr;
            Guid meIteratorIID = typeof(IBMDSwitcherMixEffectBlockIterator).GUID;
            switcher.CreateIterator(ref meIteratorIID, out meIteratorPtr);
            if (meIteratorPtr != null)
                meIterator = (IBMDSwitcherMixEffectBlockIterator)Marshal.GetObjectForIUnknown(meIteratorPtr);
            return meIterator;
        }

        public static IBMDSwitcherMixEffectBlock GetMixEffectBlock(this IBMDSwitcher switcher, int index)
        {

            IBMDSwitcherMixEffectBlockIterator mixEffectBlockIterator = switcher.GetMixEffectBlockIterator();
            if (mixEffectBlockIterator == null)
                return null;

            IBMDSwitcherMixEffectBlock mixEffectBlock;
            mixEffectBlockIterator.Next(out mixEffectBlock);
            int i = 0;
            if (mixEffectBlockIterator != null)
            {
                if (i == index)
                    return mixEffectBlock;
                mixEffectBlockIterator.Next(out mixEffectBlock);
                i++;
            }

            return null;

        }

        public static List<IBMDSwitcherMixEffectBlock> GetAllMixEffectBlocks(this IBMDSwitcher switcher)
        {

            IBMDSwitcherMixEffectBlockIterator mixEffectBlockIterator = switcher.GetMixEffectBlockIterator();
            if (mixEffectBlockIterator == null)
                return null;

            List<IBMDSwitcherMixEffectBlock> mixEffectBlocks = new List<IBMDSwitcherMixEffectBlock>();
            IBMDSwitcherMixEffectBlock mixEffectBlock;
            mixEffectBlockIterator.Next(out mixEffectBlock);
            while (mixEffectBlockIterator != null)
            {
                mixEffectBlocks.Add(mixEffectBlock);
                mixEffectBlockIterator.Next(out mixEffectBlock);
            }

            return mixEffectBlocks;

        }

    }

}
;
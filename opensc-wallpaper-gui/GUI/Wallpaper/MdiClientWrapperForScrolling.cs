using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI.Wallpaper
{

    internal class MdiClientWrapperForScrolling : NativeWindow
    {

        private MdiClient assignedMdiClient;

        public event ScrollEventHandler Scroll;
        private int oldPosH;
        private int oldPosV;

        private const int WM_HSCROLL = 0x114;
        private const int WM_VSCROLL = 0x115;

        public MdiClientWrapperForScrolling(MdiClient mdiClient)
        {
            assignedMdiClient = mdiClient;
            AssignHandle(mdiClient.Handle);
        }

        protected override void WndProc(ref Message m)
        {
            if ((m.Msg == WM_HSCROLL) || (m.Msg == WM_VSCROLL))
            {
                var scrollEventType = (ScrollEventType)(m.WParam.ToInt32() & 0xFFFF);
                var pos = m.WParam.ToInt32() >> 16;
                ref int oldPos = ref oldPosV;
                ScrollOrientation scrollOrientation = ScrollOrientation.VerticalScroll;
                if (m.Msg == WM_HSCROLL)
                {
                    oldPos = ref oldPosH;
                    scrollOrientation = ScrollOrientation.HorizontalScroll;
                }
                Scroll?.Invoke(this, new ScrollEventArgs(scrollEventType, oldPos, pos, scrollOrientation));
                oldPos = pos;
            }
            base.WndProc(ref m);
        }
    }

}

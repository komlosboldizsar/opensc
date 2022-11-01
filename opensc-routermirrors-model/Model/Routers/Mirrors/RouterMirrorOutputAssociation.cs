using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.Mirrors
{

    public class RouterMirrorOutputAssociation : RouterMirrorAssociation<RouterOutput>
    {

        public RouterMirrorOutputAssociation() : base()
        { }

        public RouterMirrorOutputAssociation(RouterMirror parent, RouterOutput itemA, RouterOutput itemB)
            : base(parent, itemA, itemB)
        {
            bind();
        }

        public override void RemovedFromParent()
        {
            base.RemovedFromParent();
            unbind();
        }

        public override void TotallyRestored()
        {
            base.TotallyRestored();
            ItemA = Parent.RouterA?.Outputs[_itemA];
            ItemB = Parent.RouterB?.Outputs[_itemB];
            bind();
        }

        private void bind()
        {
            if ((ItemA == null) || (ItemB == null))
                return;
            ItemA.CurrentInputChanged += itemAchanged;
            ItemB.CurrentInputChanged += itemBchanged;
        }

        private void unbind()
        {
            if (ItemA != null)
                ItemA.CurrentInputChanged -= itemAchanged;
            if (ItemB != null)
                ItemB.CurrentInputChanged -= itemBchanged;
        }

        private void itemAchanged(RouterOutput output, RouterInput newInput)
            => Parent.OutputChanged(output, newInput, RouterMirrorSide.SideA);

        private void itemBchanged(RouterOutput output, RouterInput newInput)
            => Parent.OutputChanged(output, newInput, RouterMirrorSide.SideB);

    }

}

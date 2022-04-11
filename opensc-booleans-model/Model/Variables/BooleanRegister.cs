using OpenSC.Model.General;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Variables
{

    public class BooleanRegister : ObjectRegisterBase<string, IBoolean>
    {

        #region Singleton
        public static BooleanRegister Instance { get; } = new BooleanRegister();
        private BooleanRegister()
        { }
        #endregion

        public override string GetKey(IBoolean item) => item.Name;

        protected override void keyChangedSubscribeMethod(IBoolean item) => item.NameChanged += itemNameChanged;
        protected override void keyChangedUnsubscribeMethod(IBoolean item) => item.NameChanged -= itemNameChanged;
        private void itemNameChanged(IBoolean item, string oldValue, string newValue) => ItemKeyChanged(item);

        protected override void itemRemovedSubscribeMethod(IBoolean item) { }
        protected override void itemRemovedUnsubscribeMethod(IBoolean item) { }

        public override string ToStringMethod(IBoolean item)
            => string.Format("[{0}] {1}", item.Name, item.Description);

    }

}

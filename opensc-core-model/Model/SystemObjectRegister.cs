﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model
{

    public class SystemObjectRegister : ObjectRegisterBase<string, ISystemObject>
    {

        #region Singleton
        public static SystemObjectRegister Instance { get; } = new SystemObjectRegister();
        private SystemObjectRegister()
        { }
        #endregion

        public override string GetKey(ISystemObject item) => item.GlobalID;

        protected override void keyChangedSubscribeMethod(ISystemObject item) => item.GlobalIdChanged += itemGlobalIdChanged;
        protected override void keyChangedUnsubscribeMethod(ISystemObject item) => item.GlobalIdChanged -= itemGlobalIdChanged;
        private void itemGlobalIdChanged(ISystemObject item, string oldValue, string newValue) => ItemKeyChanged(item);

        protected override void itemRemovedSubscribeMethod(ISystemObject item) { /*TODO*/ }
        protected override void itemRemovedUnsubscribeMethod(ISystemObject item) { /*TODO*/ }

        public override string ToStringMethod(ISystemObject item) => item.GlobalID;

    }

}

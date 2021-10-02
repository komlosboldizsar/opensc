﻿using OpenSC.Model.General;
using OpenSC.Model.Variables;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Signals
{

    public class SignalRegister : ObjectRegisterBase<string, ISignalSourceRegistered>
    {

        #region Singleton
        public static SignalRegister Instance { get; } = new SignalRegister();
        private SignalRegister() { }
        #endregion

        protected override string getKey(ISignalSourceRegistered item) => item.SignalUniqueId;

        protected override void keyChangedSubscribeMethod(ISignalSourceRegistered item) => item.SignalUniqueIdChanged += itemUniqueIdChanged;
        protected override void keyChangedUnsubscribeMethod(ISignalSourceRegistered item) => item.SignalUniqueIdChanged -= itemUniqueIdChanged;
        private void itemUniqueIdChanged(ISignalSourceRegistered item, string newValue) => ItemKeyChanged(item);

        protected override void itemRemovedSubscribeMethod(ISignalSourceRegistered item) { }
        protected override void itemRemovedUnsubscribeMethod(ISignalSourceRegistered item) { }

        public override string ToStringMethod(ISignalSourceRegistered item)
            => string.Format("[{0}] {1}", item.SignalUniqueId, item.SignalLabel);

    }

}

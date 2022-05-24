﻿using OpenSC.Logger;
using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.BlackMagicDesign
{
    public delegate void BmdVideohubAutoReconnectChangingDelegate(BmdVideohub router, bool oldSetting, bool newSetting);
    public delegate void BmdVideohubAutoReconnectChangedDelegate(BmdVideohub router, bool oldSetting, bool newSetting);

    [TypeLabel("BMD Videohub")]
    [TypeCode("bmd")]
    public class BmdVideohub : Router
    {

        private new const string LOG_TAG = "Router/BMD";

        public BmdVideohub()
        {
            initVideohub();
        }

        public override void RestoredOwnFields()
        {
            base.RestoredOwnFields();
            initVideohub();
            startAutoReconnectThread();
        }

        public override void Removed()
        {

            base.Removed();

            Disconnect();
            videohub.CrosspointChanged -= crosspointChangedHandler;
            videohub.ConnectionStateChanged -= connectionStateChangedHandler;
            videohub = null;

            IpAddressChanged = null;
            ConnectionStateChanged = null;
            AutoReconnectChanged = null;

            autoReconnectThreadWorking = false;
            autoReconnectThread = null;

        }

        public void Connect() => videohub.Connect();
        public void Disconnect() => videohub.Disconnect();

        #region Property: IpAddress
        public event PropertyChangedTwoValuesDelegate<BmdVideohub, string> IpAddressChanged;

        [PersistAs("ip_address")]
        private string ipAddress;

        public string IpAddress
        {
            get => ipAddress;
            set => this.setProperty(ref ipAddress, value, IpAddressChanged,
                null, (ov, nv) => { videohub.IpAddress = nv; }, ValidateIpAddress);
        }

        public void ValidateIpAddress(string ipAddress)
        {
            // ... throw new ArgumentException();
        }
        #endregion

        #region Property: Connected
        public event PropertyChangedTwoValuesDelegate<BmdVideohub, bool> ConnectionStateChanged;

        private bool connected;

        public bool Connected
        {
            get => connected;
            set
            {
                AfterChangePropertyDelegate<bool> afterChangeDelegate = (ov, nv) =>
                {
                    if (nv)
                    {
                        State = RouterState.Ok;
                        StateString = "connected";
                        string logMessage = string.Format("Connected to a BlackMagic Design router/videohub (ID: {0}) with IP {1}.", ID, IpAddress);
                        LogDispatcher.I(LOG_TAG, logMessage);
                    }
                    else
                    {
                        State = RouterState.Warning;
                        StateString = "disconnected";
                        string logMessage = string.Format("Disconnected from a BlackMagic Design mixer/switcher (ID: {0}) with IP {1}.", ID, IpAddress);
                        LogDispatcher.I(LOG_TAG, logMessage);
                    }
                };
                this.setProperty(ref connected, value, ConnectionStateChanged, null, afterChangeDelegate);
            }
        }
        #endregion

        #region Auto reconnect
        public event PropertyChangedTwoValuesDelegate<BmdVideohub, bool> AutoReconnectChanged;

        [PersistAs("auto_reconnect")]
        private bool autoReconnect;

        public bool AutoReconnect
        {
            get => autoReconnect;
            set => this.setProperty(ref autoReconnect, value, AutoReconnectChanged);
        }
        #endregion

        #region Auto reconnect thread
        private const int RECONNECT_TRY_INTERVAL = 10000;

        private Thread autoReconnectThread = null;
        private bool autoReconnectThreadWorking = false;

        private void startAutoReconnectThread()
        {
            autoReconnectThread = new Thread(autoReconnectThreadMethod)
            {
                IsBackground = true
            };
            autoReconnectThreadWorking = true;
            autoReconnectThread.Start();
        }

        private void autoReconnectThreadMethod()
        {

            string logMessage = string.Format("Trying auto reconnect to a BlackMagic Design router/videohub (ID: {0}) with IP {1}...",
                ID,
                IpAddress);
            LogDispatcher.I(LOG_TAG, logMessage);

            if (autoReconnect && !connected)
                Connect();
            while(autoReconnectThreadWorking && autoReconnect && !connected)
            {
                Thread.Sleep(RECONNECT_TRY_INTERVAL);
                if (autoReconnect && !connected)
                    Connect();
            }

        }
        #endregion

        private Library.BmdVideohub.VideohubClient videohub = null;

        private void initVideohub()
        {
            videohub = new Library.BmdVideohub.VideohubClient(ipAddress);
            videohub.ConnectionStateChanged += connectionStateChangedHandler;
            videohub.CrosspointChanged += crosspointChangedHandler;
            videohub.LockChanged += lockChangedHandler;
        }

        private void connectionStateChangedHandler(bool state)
        {
            Connected = state;
            if (Connected)
                queryAllStates();
        }

        private void crosspointChangedHandler(Library.BmdVideohub.Crosspoint crosspoint)
        {
            if ((crosspoint.Output == null) || (crosspoint.Input == null))
                return;
            try
            {
                notifyCrosspointChanged((int)crosspoint.Output, (int)crosspoint.Input);
            }
            catch { }
        }

        private void lockChangedHandler(Library.BmdVideohub.LockStateData lockStateData)
        {
            if ((lockStateData.Output == null) || (lockStateData.State == null))
                return;
            notifyLockChanged((int)lockStateData.Output, RouterOutputLockType.Lock, LOCK_STATE_TRANSLATIONS[(Library.BmdVideohub.LockState)lockStateData.State]);
        }

        private static readonly Dictionary<Library.BmdVideohub.LockState, RouterOutputLockState> LOCK_STATE_TRANSLATIONS
            = new Dictionary<Library.BmdVideohub.LockState, RouterOutputLockState>()
            {
                { Library.BmdVideohub.LockState.Unlocked, RouterOutputLockState.Clear },
                { Library.BmdVideohub.LockState.Owned, RouterOutputLockState.LockedLocal },
                { Library.BmdVideohub.LockState.Taken, RouterOutputLockState.LockedRemote }
            };

        protected override void requestCrosspointUpdateImpl(RouterOutput output, RouterInput input)
        {
            try
            {
                videohub.SetCrosspoint(output.Index, input.Index);
            }
            catch (ArgumentOutOfRangeException)
            { }
        }

        protected override void requestCrosspointUpdatesImpl(IEnumerable<RouterCrosspoint> crosspoints)
        {
            IEnumerable<Library.BmdVideohub.Crosspoint> crosspointIndices = crosspoints.Select(cp => new Library.BmdVideohub.Crosspoint(cp.Output.Index, cp.Input.Index));
            try
            {
                videohub.SetCrosspoints(crosspointIndices);
            }
            catch (ArgumentOutOfRangeException)
            { }
        }

        protected override void queryAllStates()
        {
            videohub.QueryAllCrosspoints();
            videohub.QueryAllLockStates();
        }

        protected override void requestLockOperationImpl(RouterOutput output, RouterOutputLockType lockType, RouterOutputLockOperationType lockOperationType)
        {
            if (lockType != RouterOutputLockType.Lock)
                return; // supporting only locks
            switch (lockOperationType)
            {
                case RouterOutputLockOperationType.Lock:
                    videohub.DoLockOperation(output.Index, Library.BmdVideohub.LockOperation.Lock);
                    break;
                case RouterOutputLockOperationType.Unlock:
                    videohub.DoLockOperation(output.Index, Library.BmdVideohub.LockOperation.Lock);
                    break;
                case RouterOutputLockOperationType.ForceUnlock:
                    videohub.DoLockOperation(output.Index, Library.BmdVideohub.LockOperation.ForceUnlock); // TODO
                    break;
            }
        }

        #region Input and output instantiation
        public override RouterInput CreateInput(string name, int index) => new RouterInput(name, this, index);
        public override RouterOutput CreateOutput(string name, int index) => new RouterOutput(name, this, index);

        private static readonly Dictionary<Type, string> OUTPUT_TYPES = new Dictionary<Type, string>()
        {
            {  typeof(BmdVideohubOutput), "bmd" }
        };

        protected override Dictionary<Type, string> OutputTypesDictionaryGetter() => OUTPUT_TYPES;
        #endregion

    }

}

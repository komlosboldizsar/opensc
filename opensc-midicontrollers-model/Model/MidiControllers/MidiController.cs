using OpenSC.Logger;
using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using Melanchall.DryWetMidi.Multimedia;
using Melanchall.DryWetMidi.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.MidiControllers
{
    public class MidiController : ModelBase
    {

        #region Constants
        private const string LOG_TAG = "MidiController";
        #endregion

        #region Lifecycle, restoration
        public MidiController()
        { }

        public override void RestoredOwnFields()
        {
            Init();
        }

        public override void Removed()
        {
            base.Removed();
            DeInit();
            DeviceIndexChanged = null;
            NoteStateChanged = null;
        }
        #endregion

        #region Owner database
        public override sealed IDatabaseBase OwnerDatabase { get; } = MidiControllerDatabase.Instance;
        #endregion

        #region Property: DeviceIndex
        public event PropertyChangedTwoValuesDelegate<MidiController, int> DeviceIndexChanged;

        [PersistAs("device_index")]
        private int deviceIndex;

        public int DeviceIndex
        {
            get => deviceIndex;
            set
            {
                AfterChangePropertyDelegate<int> afterChangeDelegate = (ov, nv) =>
                {
                    if ((deviceIdentifiedBy == IdentifierType.Index) && Initialized && !Updating)
                        ReInit();
                };
                if (!this.setProperty(ref deviceIndex, value, DeviceIndexChanged, null, afterChangeDelegate))
                    return;
            }
        }
        #endregion

        #region Property: DeviceName
        public event PropertyChangedTwoValuesDelegate<MidiController, string> DeviceNameChanged;

        [PersistAs("device_name")]
        private string deviceName;

        public string DeviceName
        {
            get => deviceName;
            set
            {
                AfterChangePropertyDelegate<string> afterChangeDelegate = (ov, nv) =>
                {
                    if ((deviceIdentifiedBy == IdentifierType.Name) && Initialized && !Updating)
                        ReInit();
                };
                if (!this.setProperty(ref deviceName, value, DeviceNameChanged, null, afterChangeDelegate))
                    return;
            }
        }
        #endregion

        #region Property: DeviceIdentifyBy
        public event PropertyChangedTwoValuesDelegate<MidiController, IdentifierType> DeviceIdentifiedByChanged;

        [PersistAs("device_identified_by")]
        private IdentifierType deviceIdentifiedBy;

        public IdentifierType DeviceIdentifiedBy
        {
            get => deviceIdentifiedBy;
            set
            {
                AfterChangePropertyDelegate<IdentifierType> afterChangeDelegate = (ov, nv) =>
                {
                    if (Initialized && !Updating)
                        ReInit();
                };
                if (!this.setProperty(ref deviceIdentifiedBy, value, DeviceIdentifiedByChanged, null, afterChangeDelegate))
                    return;
            }
        }

        public enum IdentifierType
        {
            Index,
            Name
        }
        #endregion

        #region Property: Initialized
        public event PropertyChangedTwoValuesDelegate<MidiController, bool> InitializedChanged;

        protected bool initialized;

        public bool Initialized
        {
            get => initialized;
            set => this.setProperty(ref initialized, value, InitializedChanged);
        }
        #endregion

        private InputDevice inputDevice = null;

        #region Init and DeInit
        public void Init()
        {
            if (inputDevice != null)
                return;
            if (deviceIndex < 0)
                return;
            try
            {
                inputDevice = deviceIdentifiedBy switch
                {
                    IdentifierType.Index => InputDevice.GetByIndex(deviceIndex),
                    IdentifierType.Name => InputDevice.GetByName(deviceName),
                    _ => null
                };
                if (inputDevice == null)
                    return;
                inputDevice.EventReceived += inputDeviceMidiChannelMessageHandler;
                inputDevice.StartEventsListening();
                Initialized = true;
                LogDispatcher.I(LOG_TAG, $"Initialized MIDI controller [{this}].");
            }
            catch (Exception ex)
            {
                if (inputDevice != null)
                {
                    inputDevice.StopEventsListening();
                    inputDevice.EventReceived -= inputDeviceMidiChannelMessageHandler;
                }
                inputDevice = null;
                Initialized = false;
                LogDispatcher.E(LOG_TAG, $"Failed to initialize MIDI controller [{this}], error message: [{ex.Message}].");
            }
        }

        public void DeInit()
        {
            if (inputDevice != null)
            {
                inputDevice.StopEventsListening();
                inputDevice.EventReceived -= inputDeviceMidiChannelMessageHandler;
                try
                {
                    inputDevice.Dispose();
                }
                catch (Exception ex)
                {
                    LogDispatcher.E(LOG_TAG, $"Error occurred during deinitialization of MIDI controller [{this}]: [{ex.Message}].");
                }
            }
            inputDevice = null;
            Initialized = false;
            LogDispatcher.I(LOG_TAG, $"Deinitialized MIDI controller [{this}].");
        }

        public void ReInit()
        {
            DeInit();
            Init();
        }
        #endregion

        #region Message handling
        private void inputDeviceMidiChannelMessageHandler(object sender, MidiEventReceivedEventArgs e)
        {
            switch (e.Event.EventType)
            {
                case MidiEventType.NoteOn:
                    handleNoteChangeMessage(((NoteEvent)e.Event).NoteNumber, false);
                    break;
                case MidiEventType.NoteOff:
                    handleNoteChangeMessage(((NoteEvent)e.Event).NoteNumber, true);
                    break;
            }
        }
        #endregion

        #region Note events
        // @source https://newt.phys.unsw.edu.au/jw/notes.html
        public const int PITCH_C4 = 60;
        public const int PITCH_A4 = 69;

        private void handleNoteChangeMessage(int note, bool on)
        {
            string noteState = on ? "on" : "off";
            LogDispatcher.V(LOG_TAG, $"Note changed on MIDI controller [{this}]: Note [{note}] to [{noteState}].");
            NoteStateChanged?.Invoke(this, note, on);
        }

        public delegate void NoteStateChangedDelegate(MidiController controller, int note, bool state);
        public event NoteStateChangedDelegate NoteStateChanged;
        #endregion

    }

}

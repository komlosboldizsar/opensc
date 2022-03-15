using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using Sanford.Multimedia.Midi;
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
            DeviceIdChanged = null;
            NoteStateChanged = null;
        }
        #endregion

        #region Owner database
        public override sealed IDatabaseBase OwnerDatabase { get; } = MidiControllerDatabase.Instance;
        #endregion

        #region Property: DeviceId
        public event PropertyChangedTwoValuesDelegate<MidiController, int> DeviceIdChanged;

        [PersistAs("device_id")]
        private int deviceId;

        public int DeviceId
        {
            get => deviceId;
            set
            {
                AfterChangePropertyDelegate<int> afterChangeDelegate = (ov, nv) =>
                {
                    if (Initialized && !Updating)
                        ReInit();
                };
                if (!this.setProperty(ref deviceId, value, DeviceIdChanged, null, afterChangeDelegate))
                    return;
            }
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
            if (deviceId < 0)
                return;
            if (deviceId >= InputDevice.DeviceCount)
                return;
            try
            {
                inputDevice = new InputDevice(deviceId);
                inputDevice.ChannelMessageReceived += inputDeviceMidiChannelMessageHandler;
                Initialized = true;
            }
            catch
            {
                inputDevice = null;
                inputDevice.ChannelMessageReceived -= inputDeviceMidiChannelMessageHandler;
                Initialized = true;
            }
        }

        public void DeInit()
        {
            if (inputDevice != null)
            {
                inputDevice.ChannelMessageReceived -= inputDeviceMidiChannelMessageHandler;
                try
                {
                    inputDevice.Close();
                    inputDevice.Dispose();
                }
                catch { }
            }
            inputDevice = null;
            Initialized = false;
        }

        public void ReInit()
        {
            DeInit();
            Init();
        }
        #endregion

        #region Message handling
        private void inputDeviceMidiChannelMessageHandler(object sender, ChannelMessageEventArgs e)
        {
            switch (e.Message.Command)
            {
                case ChannelCommand.NoteOff:
                    handleNoteChangeMessage(e.Message.Data1, false);
                    break;
                case ChannelCommand.NoteOn:
                    handleNoteChangeMessage(e.Message.Data1, true);
                    break;
            }
        }
        #endregion

        #region Note events
        // @source https://newt.phys.unsw.edu.au/jw/notes.html
        public const int PITCH_C4 = 60;
        public const int PITCH_A4 = 69;

        private void handleNoteChangeMessage(int note, bool on)
            => NoteStateChanged?.Invoke(this, note, on);

        public delegate void NoteStateChangedDelegate(MidiController controller, int note, bool state);
        public event NoteStateChangedDelegate NoteStateChanged;
        #endregion

    }

}

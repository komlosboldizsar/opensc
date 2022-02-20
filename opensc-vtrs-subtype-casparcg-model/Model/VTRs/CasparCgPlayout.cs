using Bespoke.Osc;
using OpenSC.Logger;
using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace OpenSC.Model.VTRs
{

    [TypeLabel("CasparCG playout")]
    [TypeCode("casparcg")]
    public class CasparCgPlayout: Vtr
    {

        #region Constants
        private const string LOG_TAG = "Vtr/CasparCG";
        #endregion

        #region Persistence, instantiation
        public override void RestoredOwnFields()
        {
            base.RestoredOwnFields();
            CasparCgPlayoutCommons.Instance.SubscribeToIpChannelLayer(this);
            initStillClearStateDetection();
        }

        public override void Removed()
        {
            base.Removed();
            CasparCgPlayoutCommons.Instance.UnsubscribeFromIpChannelLayer(this);
            deinitStillClearStateDetection();
        }
        #endregion

        #region Property: ListenedIP
        public event PropertyChangedTwoValuesDelegate<CasparCgPlayout, string> ListenedIpChanged;

        [PersistAs("listened_ip")]
        private string listenedIp = "127.0.0.1";

        public string ListenedIp
        {
            get => listenedIp;
            set => this.setProperty(ref listenedIp, value, ListenedIpChanged,
                (ov, nv) => CasparCgPlayoutCommons.Instance.UnsubscribeFromIpChannelLayer(this),
                (ov, nv) => {
                    resetStateAndData();
                    CasparCgPlayoutCommons.Instance.SubscribeToIpChannelLayer(this);
                });
        }
        #endregion

        #region Property: WatchedChannel
        public event PropertyChangedTwoValuesDelegate<CasparCgPlayout, int> WatchedChannelChanged;

        [PersistAs("watched_channel")]
        private int watchedChannel = 1;

        public int WatchedChannel
        {
            get => watchedChannel;
            set => this.setProperty(ref watchedChannel, value, WatchedChannelChanged,
                (ov, nv) => CasparCgPlayoutCommons.Instance.UnsubscribeFromIpChannelLayer(this),
                (ov, nv) => {
                    resetStateAndData();
                    CasparCgPlayoutCommons.Instance.SubscribeToIpChannelLayer(this);
                });
        }
        #endregion

        #region Property: WatchedLayer
        public event PropertyChangedTwoValuesDelegate<CasparCgPlayout, int> WatchedLayerChanged;

        [PersistAs("watched_layer")]
        private int watchedLayer = 10;

        public int WatchedLayer
        {
            get => watchedLayer;
            set => this.setProperty(ref watchedLayer, value, WatchedLayerChanged,
                (ov, nv) => CasparCgPlayoutCommons.Instance.UnsubscribeFromIpChannelLayer(this),
                (ov, nv) => {
                    resetStateAndData();
                    CasparCgPlayoutCommons.Instance.SubscribeToIpChannelLayer(this);
                });
        }
        #endregion

        #region OSC receiving and state processing
        private object stateUpdatingLock = new object();
        private bool isPaused = false;
        private int elapsedFrames;
        private int lastElapsedFrames;
        private const int ELAPSED_FRAMES_CUED_LIMIT = 5;
        private DateTime lastChangeToPausedFalse = DateTime.Now;
        private int frameChangesSinceStill = 0;

        public void ReceiveOscMessage(OscMessage message, string subaddress)
        {
            try
            {
                lock (stateUpdatingLock)
                {
                    switch (subaddress)
                    {
                        case "file/path":
                            Title = message.Data[0].ToString();
                            break;
                        case "file/time":
                            float elapsedTime = (float)message.Data[0];
                            int tElapsed = Convert.ToInt32(elapsedTime);
                            int tFull = Convert.ToInt32(message.Data[1]);
                            if ((lastElapsedTime != -1) && (elapsedTime != lastElapsedTime))
                            {
                                lastElapsedTimeUpdate = DateTime.Now;
                                still = false;
                            }
                            SecondsElapsed = tElapsed;
                            SecondsFull = tFull;
                            SecondsRemaining = tFull - tElapsed;
                            lastElapsedTime = elapsedTime;
                            updateState();
                            break;
                        case "file/frame":
                            elapsedFrames = Convert.ToInt32(message.Data[0]);
                            if (lastElapsedFrames != elapsedFrames)
                                frameChangesSinceStill++;
                            lastElapsedFrames = elapsedFrames;
                            break;
                        case "paused":
                            isPaused = (message.Data[0].ToString() == "True");
                            if (isPaused)
                            {
                                lastChangeToPausedFalse = DateTime.Now;
                                frameChangesSinceStill = 0;
                            }
                            lastPausedStateUpdate = DateTime.Now;
                            updateState();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMessage = string.Format("Error occurred while trying to process an OSC message (VTR ID: {0}). Exception message: [{1}]",
                    ID,
                    ex.Message);
                LogDispatcher.E(LOG_TAG, errorMessage);
            }
        }

        private void updateState()
        {
            if (isPaused)
            {
                State = (elapsedFrames <= ELAPSED_FRAMES_CUED_LIMIT) ? VtrState.Cued : VtrState.Paused;
            }
            else if (!still)
            {
                if ((State != VtrState.Cued) || (frameChangesSinceStill > 0))
                    State = VtrState.Playing;
            }
            else if (State != VtrState.Cued)
            {
                State = VtrState.Stopped;
            }
            else
            {
                TimeSpan diff = DateTime.Now - lastChangeToPausedFalse;
                if (diff.TotalMilliseconds > STILL_CLEAR_STATE_DIFFERENCE_MILLISECONDS)
                    State = VtrState.Stopped;
            }
        }
        #endregion

        #region Still and clear state detection
        private float lastElapsedTime = -1;
        private DateTime lastElapsedTimeUpdate = DateTime.Now;
        private DateTime lastPausedStateUpdate = DateTime.Now;
        private System.Timers.Timer stillClearStateDetectionTimer;
        private const int STILL_CLEAR_STATE_DIFFERENCE_MILLISECONDS = 500;
        private bool still = false;

        private void stillClearStateDetection(object sender, System.Timers.ElapsedEventArgs e)
        {
            lock (stateUpdatingLock)
            {
                TimeSpan diff = DateTime.Now - lastElapsedTimeUpdate;
                if (diff.TotalMilliseconds > STILL_CLEAR_STATE_DIFFERENCE_MILLISECONDS)
                {
                    still = true;
                    frameChangesSinceStill = 0;
                }
                diff = DateTime.Now - lastPausedStateUpdate;
                if (diff.TotalMilliseconds > STILL_CLEAR_STATE_DIFFERENCE_MILLISECONDS)
                    resetStateAndData(VtrState.Stopped);
            }
        }

        private void initStillClearStateDetection()
        {
            if (stillClearStateDetectionTimer != null)
                return;
            stillClearStateDetectionTimer = new System.Timers.Timer(STILL_CLEAR_STATE_DIFFERENCE_MILLISECONDS);
            stillClearStateDetectionTimer.Elapsed += stillClearStateDetection;
            stillClearStateDetectionTimer.AutoReset = true;
            stillClearStateDetectionTimer.Enabled = true;
        }

        private void deinitStillClearStateDetection()
        {
            if (stillClearStateDetectionTimer == null)
                return;
            stillClearStateDetectionTimer.Enabled = false;
            stillClearStateDetectionTimer.Elapsed -= stillClearStateDetection;
            stillClearStateDetectionTimer.Dispose();
            stillClearStateDetectionTimer = null;
        }
        #endregion

    }

}

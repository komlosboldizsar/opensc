using Bespoke.Osc;
using OpenSC.Logger;
using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using OpenSC.Model.SourceGenerators;
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
    public partial class CasparCgPlayout: Vtr
    {

        #region Constants
        private const string LOG_TAG = "Vtr/CasparCG";
        #endregion

        #region Persistence, instantiation
        public override void RestoredOwnFields()
        {
            base.RestoredOwnFields();
            initStillClearUnknownStateDetection();
        }

        public override void Removed()
        {
            base.Removed();
            CasparCgPlayoutCommons.Instance.UnsubscribeFromIpChannelLayer(this);
            deinitStillClearUnknownStateDetection();
        }
        #endregion

        #region Property: ListenedIP
        [AutoProperty]
        [AutoProperty.BeforeChange(nameof(unsubscribeBeforeChange))]
        [AutoProperty.AfterChange(nameof(subscribeAfterChange))]
        [PersistAs("listened_ip")]
        private string listenedIp = "127.0.0.1";
        #endregion

        #region Property: WatchedChannel
        [AutoProperty]
        [AutoProperty.BeforeChange(nameof(unsubscribeBeforeChange))]
        [AutoProperty.AfterChange(nameof(subscribeAfterChange))]
        [PersistAs("watched_channel")]
        private int watchedChannel = 1;
        #endregion

        #region Property: WatchedLayer
        [AutoProperty]
        [AutoProperty.BeforeChange(nameof(unsubscribeBeforeChange))]
        [AutoProperty.AfterChange(nameof(subscribeAfterChange))]
        [PersistAs("watched_layer")]
        private int watchedLayer = 10;
        #endregion

        private void unsubscribeBeforeChange() => CasparCgPlayoutCommons.Instance.UnsubscribeFromIpChannelLayer(this);

        private void subscribeAfterChange()
        {
            resetStateAndData();
            CasparCgPlayoutCommons.Instance.SubscribeToIpChannelLayer(this);
        }

        #region OSC receiving and state processing
        private object stateUpdatingLock = new object();
        private bool isPaused = false;
        private int elapsedFrames;
        private int lastElapsedFrames;
        private const int ELAPSED_FRAMES_CUED_LIMIT = 5;
        private DateTime lastChangeToPausedFalse = DateTime.Now;
        private int frameChangesSinceStill = 0;

        public void ReceiveLayerOscMessage(OscMessage message, string subaddress)
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
                    lastAnyStateUpdate = DateTime.Now;
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

        public void ReceiveChannelOscMessage(OscMessage message, string subaddress)
        {
            lastAnyStateUpdate = DateTime.Now;
            if (State == VtrState.Unknown)
                resetStateAndData(VtrState.Stopped);
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
        private DateTime lastAnyStateUpdate = DateTime.Now;
        private System.Timers.Timer stillClearUnknownStateDetectionTimer;
        private const int STILL_CLEAR_STATE_DIFFERENCE_MILLISECONDS = 500;
        private const int UNKNOWN_STATE_DIFFERENCE_MILLISECONDS = 1000;
        private bool still = false;

        private void stillClearUnknownStateDetection(object sender, System.Timers.ElapsedEventArgs e)
        {
            lock (stateUpdatingLock)
            {
                TimeSpan lastElapsedTimeUpdateDiff = DateTime.Now - lastElapsedTimeUpdate;
                if (lastElapsedTimeUpdateDiff.TotalMilliseconds > STILL_CLEAR_STATE_DIFFERENCE_MILLISECONDS)
                {
                    still = true;
                    frameChangesSinceStill = 0;
                }
                TimeSpan lastAnyStateUpdateDiff = DateTime.Now - lastAnyStateUpdate;
                if (lastAnyStateUpdateDiff.TotalMilliseconds > UNKNOWN_STATE_DIFFERENCE_MILLISECONDS)
                {
                    State = VtrState.Unknown;
                    return;
                }
                TimeSpan lastPausedStateUpdateDiff = DateTime.Now - lastPausedStateUpdate;
                if (lastPausedStateUpdateDiff.TotalMilliseconds > STILL_CLEAR_STATE_DIFFERENCE_MILLISECONDS)
                    resetStateAndData(VtrState.Stopped);
            }
        }

        private void initStillClearUnknownStateDetection()
        {
            if (stillClearUnknownStateDetectionTimer != null)
                return;
            stillClearUnknownStateDetectionTimer = new System.Timers.Timer(STILL_CLEAR_STATE_DIFFERENCE_MILLISECONDS);
            stillClearUnknownStateDetectionTimer.Elapsed += stillClearUnknownStateDetection;
            stillClearUnknownStateDetectionTimer.AutoReset = true;
            stillClearUnknownStateDetectionTimer.Enabled = true;
        }

        private void deinitStillClearUnknownStateDetection()
        {
            if (stillClearUnknownStateDetectionTimer == null)
                return;
            stillClearUnknownStateDetectionTimer.Enabled = false;
            stillClearUnknownStateDetectionTimer.Elapsed -= stillClearUnknownStateDetection;
            stillClearUnknownStateDetectionTimer.Dispose();
            stillClearUnknownStateDetectionTimer = null;
        }
        #endregion

    }

}

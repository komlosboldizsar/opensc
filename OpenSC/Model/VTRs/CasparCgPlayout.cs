using Bespoke.Common.Osc;
using OpenSC.Logger;
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
        public override void Restored()
        {
            base.Restored();
            CasparCgPlayoutCommons.Instance.SubscribeToChannelLayer(this);
            initStoppedStateDetection();
        }

        public override void Removed()
        {
            base.Removed();
            CasparCgPlayoutCommons.Instance.UnsubscribeFromChannelLayer(this);
            deinitStoppedStateDetection();
        }
        #endregion

        #region Property: ListenedIP
        [PersistAs("listened_ip")]
        private string listenedIP = "127.0.0.1";

        public string ListenedIP
        {
            get => listenedIP;
            set { listenedIP = value; }
        }
        #endregion

        #region Property: WatchedChannel
        [PersistAs("watched_channel")]
        private int watchedChannel = 1;

        public int WatchedChannel
        {
            get => watchedChannel;
            set
            {
                if (watchedChannel == value)
                    return;
                CasparCgPlayoutCommons.Instance.SubscribeToChannelLayer(this);
                watchedChannel = value;
                CasparCgPlayoutCommons.Instance.UnsubscribeFromChannelLayer(this);
            }
        }
        #endregion

        #region Property: WatchedLayer
        [PersistAs("watched_layer")]
        private int watchedLayer = 10;

        public int WatchedLayer
        {
            get => watchedLayer;
            set
            {
                if (watchedLayer == value)
                    return;
                CasparCgPlayoutCommons.Instance.UnsubscribeFromChannelLayer(this);
                watchedLayer = value;
                CasparCgPlayoutCommons.Instance.SubscribeToChannelLayer(this);
            }
        }
        #endregion

        #region OSC receiving and state processing
        private object stateUpdatingLock = new object();
        private bool isPaused = false;

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
                                State = VtrState.Playing;
                            }
                            SecondsElapsed = tElapsed;
                            SecondsFull = tFull;
                            SecondsRemaining = tFull - tElapsed;
                            lastElapsedTime = elapsedTime;
                            break;
                        case "paused":
                            isPaused = (message.Data[0].ToString() == "True");
                            if (isPaused)
                                State = VtrState.Paused;
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
        #endregion

        #region Stopped/paused state detection
        private float lastElapsedTime = -1;
        private DateTime lastElapsedTimeUpdate = DateTime.Now;
        private System.Timers.Timer stoppedStateDetectionTimer;
        private const int STOPPED_STATE_DIFFERENCE_MILLISECONDS = 500;

        private void stoppedStateDetection(object sender, System.Timers.ElapsedEventArgs e)
        {
            lock (stateUpdatingLock)
            {
                if (isPaused)
                    return;
                TimeSpan diff = DateTime.Now - lastElapsedTimeUpdate;
                if (diff.TotalMilliseconds > STOPPED_STATE_DIFFERENCE_MILLISECONDS)
                    State = VtrState.Stopped;
            }
        }

        private void initStoppedStateDetection()
        {
            if (stoppedStateDetectionTimer != null)
                return;
            stoppedStateDetectionTimer = new System.Timers.Timer(STOPPED_STATE_DIFFERENCE_MILLISECONDS);
            stoppedStateDetectionTimer.Elapsed += stoppedStateDetection;
            stoppedStateDetectionTimer.AutoReset = true;
            stoppedStateDetectionTimer.Enabled = true;
        }

        private void deinitStoppedStateDetection()
        {
            if (stoppedStateDetectionTimer == null)
                return;
            stoppedStateDetectionTimer.Enabled = false;
            stoppedStateDetectionTimer.Elapsed -= stoppedStateDetection;
            stoppedStateDetectionTimer.Dispose();
            stoppedStateDetectionTimer = null;
        }
        #endregion

    }

}

using OpenSC.Logger;
using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.X32Faders
{

    public class X32Fader : ModelBase
    {

        public const string LOG_TAG = "X32Fader";

        #region Persistence, instantiation
        public X32Fader()
        { }

        public override void Removed()
        { }
        #endregion

        #region Owner database
        public override sealed IDatabaseBase OwnerDatabase { get; } = X32FaderDatabase.Instance;
        #endregion

        #region Property: IpAddress
        public event PropertyChangedTwoValuesDelegate<X32Fader, string> IpAddressChanged;

        private string ipAddress = "192.168.10.41";

        [PersistAs("ip_address")]
        public string IpAddress
        {
            get => ipAddress;
            set => this.setProperty(ref ipAddress, value, IpAddressChanged);
        }
        #endregion

        #region Property: OscPath
        public event PropertyChangedTwoValuesDelegate<X32Fader, string> OscPathChanged;

        private string oscPath = "";

        [PersistAs("osc_path")]
        public string OscPath
        {
            get => oscPath;
            set => this.setProperty(ref oscPath, value, OscPathChanged);
        }
        #endregion

        #region Property: TargetLevel
        public event PropertyChangedTwoValuesDelegate<X32Fader, decimal> TargetLevelChanged;

        private decimal targetLevel = 1.0m;

        [PersistAs("target_level")]
        public decimal TargetLevel
        {
            get => targetLevel;
            set => this.setProperty(ref targetLevel, value, TargetLevelChanged, validator: ValidateTargetLevel);
        }

        public void ValidateTargetLevel(decimal level)
        {
            if ((level < 0) || (level > 1))
                throw new ArgumentException();
        }
        #endregion

        #region Property: Time
        public event PropertyChangedTwoValuesDelegate<X32Fader, int> TimeChanged;

        private int time = 1000;

        [PersistAs("time")]
        public int Time
        {
            get => time;
            set => this.setProperty(ref time, value, TimeChanged, validator: ValidateTime);
        }

        public void ValidateTime(int time)
        {
            if (time < 0)
                throw new ArgumentException();
        }
        #endregion

        #region Property: ReferenceLevelForTime
        public event PropertyChangedTwoValuesDelegate<X32Fader, decimal> ReferenceLevelForTimeChanged;

        private decimal referenceLevelForTime = 0.0m;

        [PersistAs("reference_level_for_time")]
        public decimal ReferenceLevelForTime
        {
            get => referenceLevelForTime;
            set => this.setProperty(ref referenceLevelForTime, value, ReferenceLevelForTimeChanged, validator: ValidateReferenceLevelForTime);
        }

        public void ValidateReferenceLevelForTime(decimal referenceLevelForTime)
        {
            if ((referenceLevelForTime < 0) || (referenceLevelForTime > 1))
                throw new ArgumentException();
        }
        #endregion

        #region Property: UseReferenceLevelForTime
        public event PropertyChangedTwoValuesDelegate<X32Fader, bool> UseReferenceLevelForTimeChanged;

        private bool useReferenceLevelForTime = false;

        [PersistAs("use_reference_level_for_time")]
        public bool UseReferenceLevelForTime
        {
            get => useReferenceLevelForTime;
            set => this.setProperty(ref useReferenceLevelForTime, value, UseReferenceLevelForTimeChanged);
        }
        #endregion

        #region Property: TimeStep
        public event PropertyChangedTwoValuesDelegate<X32Fader, int> TimeStepChanged;

        private int timeStep = 50;

        [PersistAs("time_step")]
        public int TimeStep
        {
            get => timeStep;
            set => this.setProperty(ref timeStep, value, TimeStepChanged, validator: ValidateTimeStep);
        }

        public void ValidateTimeStep(int timeStep)
        {
            if (timeStep < 1)
                throw new ArgumentException();
        }
        #endregion

        public void Do() => Task.Run(_do);

        private async void _do()
        {
            float startLevel = await queryCurrentLevel();
            float startLevelDifference = (float)targetLevel - startLevel;
            float fadeTime = time;
            if (useReferenceLevelForTime)
            {
                float referenceLevelDifference = (float)(targetLevel - referenceLevelForTime);
                fadeTime *= Math.Abs(startLevelDifference) / Math.Abs(referenceLevelDifference);
            }
            float currentLevel = startLevel;
            int steps = (int)(fadeTime / timeStep);
            float levelStep = startLevelDifference / steps;
            for (int s = 0; s < steps; s++)
            {
                currentLevel += levelStep;
                setLevel(currentLevel);
                await Task.Delay(timeStep);
            }
            if (currentLevel != (float)targetLevel)
                setLevel((float)targetLevel);
        }

        private async Task<float> queryCurrentLevel() => await X32FaderCommons.QueryCurrentLevel(oscPath, ipAddress);

        private void setLevel(float level) => X32FaderCommons.SetLevel(oscPath, level, ipAddress);


    }

}

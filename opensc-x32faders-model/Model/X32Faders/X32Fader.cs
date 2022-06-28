using OpenSC.Logger;
using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using OpenSC.Model.SourceGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.X32Faders
{

    public partial class X32Fader : ModelBase
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
        [AutoProperty]
        [PersistAs("ip_address")]
        private string ipAddress;
        #endregion

        #region Property: OscPath
        [AutoProperty]
        [PersistAs("osc_path")]
        private string oscPath = "";
        #endregion

        #region Property: TargetLevel
        [AutoProperty]
        [AutoProperty.Validator(nameof(ValidateTargetLevel))]
        [PersistAs("target_level")]
        private decimal targetLevel = 1.0m;

        public void ValidateTargetLevel(decimal level)
        {
            if ((level < 0) || (level > 1))
                throw new ArgumentException();
        }
        #endregion

        #region Property: Time
        [AutoProperty]
        [AutoProperty.Validator(nameof(ValidateTime))]
        [PersistAs("time")]
        private int time = 1000;

        public void ValidateTime(int time)
        {
            if (time < 0)
                throw new ArgumentException();
        }
        #endregion

        #region Property: ReferenceLevelForTime
        [AutoProperty]
        [AutoProperty.Validator(nameof(ValidateReferenceLevelForTime))]
        [PersistAs("reference_level_for_time")]
        private decimal referenceLevelForTime = 0.0m;

        public void ValidateReferenceLevelForTime(decimal referenceLevelForTime)
        {
            if ((referenceLevelForTime < 0) || (referenceLevelForTime > 1))
                throw new ArgumentException();
        }
        #endregion

        #region Property: UseReferenceLevelForTime
        [AutoProperty]
        [PersistAs("use_reference_level_for_time")]
        private bool useReferenceLevelForTime = false;
        #endregion

        #region Property: TimeStep
        [AutoProperty]
        [AutoProperty.Validator(nameof(ValidateTimeStep))]
        [PersistAs("time_step")]
        private int timeStep = 50;

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

using BMD.Switcher;
using OpenSC.Logger;
using OpenSC.Model.General;
using OpenSC.Model.Mixers;
using OpenSC.Model.Mixers.BlackMagicDesign;
using OpenSC.Model.Persistence;
using OpenSC.Model.Settings;
using OpenSC.Model.SourceGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.BmdAtemMv
{

    [TypeLabel("BMD ATEM Multiview")]
    [TypeCode("bmdatemmv")]
    public partial class BmdAtemMvRouter : Router
    {

        private new const string LOG_TAG = "Router/BmdAtemMv";

        public override void RestoredOwnFields()
        {
            base.RestoredOwnFields();
            if (mixer != null)
                getMultiviews();
        }

        #region Property: Mixer
        [PersistAs("mixer")]
        [AutoProperty]
        [AutoProperty.BeforeChange(nameof(_mixer_beforeChange))]
        [AutoProperty.AfterChange(nameof(_mixer_afterChange))]
        private Mixer mixer;

        private void _mixer_beforeChange(Mixer oldValue, Mixer newValue, BeforeChangePropertyArgs args)
        {
            if (oldValue != null)
                oldValue.StateChanged -= Mixer_StateChanged;
        }

        private void _mixer_afterChange(Mixer oldValue, Mixer newValue)
        {
            if (newValue != null)
            {
                newValue.StateChanged += Mixer_StateChanged;
                getMultiviews();
            }
        }

        private void Mixer_StateChanged(Mixer item, MixerState oldValue, MixerState newValue)
            => getMultiviews();

        private void Multiview_WindowSourceChanged(BMDSwitcherAPI.IBMDSwitcherMultiView apiMultiview, Multiview multiview, uint windowIndex, long sourceId)
            => handleWindowSourceChange(multiview.Index, (int)windowIndex, sourceId);
        #endregion

        #region Multiviews of mixer
        private readonly List<Multiview> multiviews = new();

        private void getMultiviews()
        {
            BmdMixer bmdMixer = (BmdMixer)mixer;
            foreach (Multiview multiview in multiviews)
                multiview.WindowSourceChanged -= Multiview_WindowSourceChanged;
            multiviews.Clear();
            multiviews.AddRange(bmdMixer.ApiSwitcher.GetAllMultiviews());
            foreach (Multiview multiview in multiviews)
                multiview.WindowSourceChanged += Multiview_WindowSourceChanged;
            queryAllStates();
        }
        #endregion

        #region Input and output instantiation
        public override RouterInput CreateInput(string name, int index)
            => new(name, this, index);

        public override RouterOutput CreateOutput(string name, int index)
            => new BmdAtemMvRouterOutput(name, this, index);

        private static readonly Dictionary<Type, string> OUTPUT_TYPES = new()
        {
            { typeof(BmdAtemMvRouterOutput), "bmdatemmv" }
        };

        protected override Dictionary<Type, string> OutputTypesDictionaryGetter()
            => OUTPUT_TYPES;
        #endregion

        #region Setting/getting crosspoints
        protected override void requestCrosspointUpdateImpl(RouterOutput output, RouterInput input)
        {
            int multiviewIndex = output.Index / 100;
            if ((multiviewIndex < 0) || (multiviewIndex >= multiviews.Count))
                throw new ArgumentException($"Invalid output: multiviewer with index #{multiviewIndex} unknown.", nameof(output));
            Multiview multiview = multiviews[multiviewIndex];
            int windowIndex = output.Index % 100;
            if ((windowIndex < 0) || (windowIndex >= multiview.WindowCount))
                throw new ArgumentException($"Invalid output: window with index #{windowIndex} unknown at multiviewer #{multiviewIndex}.", nameof(output));
            multiview.RequestSetWindowSourceId((uint)windowIndex, input.Index);
        }

        protected override void requestCrosspointUpdatesImpl(IEnumerable<RouterCrosspoint> crosspoints)
        {
            foreach (RouterCrosspoint crosspoint in crosspoints)
                requestCrosspointUpdateImpl(crosspoint.Output, crosspoint.Input);
        }

        protected override void queryAllStates()
        {
            int multiviewIndex = 0;
            foreach (Multiview multiview in multiviews)
            {
                long[] windowSourceIds = multiview.GetAllWindowSourceIds();
                for (int windowIndex = 0; windowIndex < windowSourceIds.Length; windowIndex++)
                    handleWindowSourceChange(multiviewIndex, windowIndex, windowSourceIds[windowIndex]);
                multiviewIndex++;
            }
        }

        private void handleWindowSourceChange(int multiviewIndex, int windowIndex, long sourceId)
        {
            int outputIndex = multiviewIndex * 100 + windowIndex;
            RouterOutput output = Outputs.FirstOrDefault(o => o.Index == outputIndex);
            if (output == null)
                return;
            RouterInput input = Inputs.FirstOrDefault(i => i.Index == sourceId);
            output.AssignSource(input);
        }

        protected override void requestLockOperationImpl(RouterOutput output, RouterOutputLockType lockType, RouterOutputLockOperationType lockOperationType)
            => throw new NotImplementedException();
        #endregion

    }

}

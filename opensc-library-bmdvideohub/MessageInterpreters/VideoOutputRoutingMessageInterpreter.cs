namespace OpenSC.Library.BmdVideohub
{
    internal abstract class VideoOutputRoutingMessageInterpreter : BlockMessageInterpreterBase
    {

        public override string Header => ProtocolStrings.BLOCK__VIDEO_OUTPUT_ROUTING;

        public override void InterpretLine(string line)
        {
            Crosspoint crosspoint = Crosspoint.FromProtocolStr(line);
            if (crosspoint == null)
                throw new MessageInterpreterException();
            handleCrosspoint(crosspoint);
        }

        protected abstract void handleCrosspoint(Crosspoint crosspoint);

    }
}

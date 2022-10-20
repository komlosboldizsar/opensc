namespace OpenSC.Library.BmdVideohub
{
    internal abstract class VideoOutputLocksStateMessageInterpreter : BlockMessageInterpreterBase
    {

        public override string Header => ProtocolStrings.BLOCK__VIDEO_OUTPUT_LOCKS;

        public override void InterpretLine(string line)
        {
            LockStateData lockOperationData = LockStateData.FromProtocolStr(line);
            if (lockOperationData == null)
                throw new MessageInterpreterException();
            handleLockState(lockOperationData);
        }

        protected abstract void handleLockState(LockStateData lockStateData);

    }
}

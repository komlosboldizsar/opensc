namespace OpenSC.Library.BmdVideohub
{
    internal abstract class VideoOutputLocksOperationMessageInterpreter : BlockMessageInterpreterBase
    {

        public override string Header => ProtocolStrings.BLOCK__VIDEO_OUTPUT_LOCKS;

        public override void InterpretLine(string line)
        {
            LockOperationData lockOperationData = LockOperationData.FromProtocolStr(line);
            if (lockOperationData == null)
                throw new MessageInterpreterException();
            handleLockOperation(lockOperationData);
        }

        protected abstract void handleLockOperation(LockOperationData lockOperationData);

    }
}

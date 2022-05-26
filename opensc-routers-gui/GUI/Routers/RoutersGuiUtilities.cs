using OpenSC.Model.Routers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI.Routers
{
    public class RoutersGuiUtilities
    {

        public static DialogResult ShowForceUndoPrompt(RouterOutputLock @lock)
            => MessageBox.Show(
                $"{@lock.GetStateSentence()}\r\n{@lock.GetForceUndoQuestion()}",
                $"{@lock.Type.GetDoString(RouterOutputLockOperationType.Unlock, true)} output",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question);

        public static bool ForceUndoWithPrompt(RouterOutputLock @lock)
        {
            if (ShowForceUndoPrompt(@lock) == DialogResult.OK)
            {
                @lock.ForceUndo();
                return true;
            }
            return false;
        }

        public static void ShowLockOperationFailedAlert(RouterOutputLockOperationException exception)
        {
            string word1 = exception.OperatedLock.Type.GetDoingString(exception.Operation, true);
            MessageBox.Show(
                $"{word1} output [{exception.OperatedLock.Output}] failed:\r\n{exception.Message}",
                $"{word1} output failed",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

    }
}

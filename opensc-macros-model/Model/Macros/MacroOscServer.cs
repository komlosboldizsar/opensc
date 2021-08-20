using Bespoke.Osc;
using OpenSC.Logger;
using OpenSC.Model.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ThreadHelpers;

namespace OpenSC.Model.Macros
{

    public class MacroOscServer
    {

        private const string LOG_TAG = "Macro/OscServer";

        public static readonly Setting<int> PortSetting = new IntSetting(
            "macros.oscserver.port",
            "Macros",
            "OSC server port",
            "The port the macros module listens for OSC messages to execute macros.",
            7100,
            1,
            65535
        );

        #region Singleton
        public static MacroOscServer Instance { get; } = new MacroOscServer();
        
        private MacroOscServer()
        { }
        #endregion

        #region Common OSC listener
        private OscServer oscServer;

        public void Start()
        {
            if (oscServer == null)
            {
                oscServer = new OscServer(Bespoke.Common.Net.TransportType.Udp, IPAddress.Any, PortSetting.Value);
                oscServer.Start();
                oscServer.BundleReceived += oscBundleReceived;
                PortSetting.ValueChanged += PortSetting_ValueChanged;
            }
        }

        private void PortSetting_ValueChanged(ISetting setting, object oldValue, object newValue)
        {
            if (oscServer != null)
            {
                Stop();
                Start();
            }
        }

        public void Stop()
        {
            oscServer.Stop();
            oscServer = null;
        }

        private const string EXEC_ADDRESS_REGEXP = @"^/macros/exec/(?<id>[0-9]+)";
        private static readonly Regex execAddressRegexp = new Regex(EXEC_ADDRESS_REGEXP, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        
        private const string COMMAND_ADDRESS_REGEXP = @"^/macros/command";
        private static readonly Regex commandAddressRegexp = new Regex(COMMAND_ADDRESS_REGEXP, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private void oscBundleReceived(object sender, OscBundleReceivedEventArgs e)
        {
            var sourceIP = e.Bundle.SourceEndPoint.Address.ToString();
            foreach (var data in e.Bundle.Data)
            {

                OscMessage message = data as OscMessage;
                if (message == null)
                    continue;

                MatchCollection execAddressRegexpMatches = execAddressRegexp.Matches(message.Address);
                if (execAddressRegexpMatches.Count == 1)
                {
                    string idFromAddress = execAddressRegexpMatches[0].Groups["id"].Value;
                    execMessageReceived(message, idFromAddress);
                    continue;
                }

                MatchCollection commandAddressRegexpMatches = commandAddressRegexp.Matches(message.Address);
                if (commandAddressRegexpMatches.Count == 1)
                {
                    commandMessageReceived(message);
                    continue;
                }

            }
        }

        private void execMessageReceived(OscMessage message, string idFromAddress)
        {
            if (!int.TryParse(idFromAddress, out int id) || (id < 0))
            {
                string errorMessage = string.Format("Received macro execute command through OSC, but ID \"{0}\" for macro is invalid.",
                    id);
                LogDispatcher.I(LOG_TAG, errorMessage);
                return;
            }

            Macro macro = MacroDatabase.Instance.GetTById(id);
            if (macro == null)
            {
                string errorMessage = string.Format("Received macro execute command through OSC, but no macro found with ID #{0}.",
                    id);
                LogDispatcher.I(LOG_TAG, errorMessage);
                return;
            }

            string logMessage = string.Format("Executing macro #{0} ({1}) by OSC.",
                    macro.ID,
                    macro.Name);
            LogDispatcher.I(LOG_TAG, logMessage);
            InvokeHelper.Invoke(() => macro.Run());

        }

        private static MacroCodeInterpreter MACRO_CODE_INTERPRETER = new MacroCodeInterpreter();

        private void commandMessageReceived(OscMessage message)
        {

            int commandCount = message.Data.Count;
            string logMessage = string.Format("Received {0} macro commands by OSC.", commandCount);
            LogDispatcher.I(LOG_TAG, logMessage);
            
            for (int i = 0; i < commandCount; i++)
            {

                string commandString = message.Data[i]?.ToString() ?? "";
                MACRO_CODE_INTERPRETER.Formula = commandString;

                if (MACRO_CODE_INTERPRETER.HasSyntaxError)
                {
                    string errorMessage = string.Format("Command line #{0} [{1}] received by OSC has syntax error.", i, commandString);
                    LogDispatcher.I(LOG_TAG, errorMessage);
                    return;
                }

                if (!MACRO_CODE_INTERPRETER.IsComplete)
                {
                    string errorMessage = string.Format("Command line #{0} [{1}] received by OSC is incomplete.", i, commandString);
                    LogDispatcher.I(LOG_TAG, errorMessage);
                    return;
                }

                if (!MACRO_CODE_INTERPRETER.CommandExists)
                {
                    string errorMessage = string.Format("Command line #{0} [{1}] contains unknown command [{2}].", i, commandString, MACRO_CODE_INTERPRETER.CommandCode);
                    LogDispatcher.I(LOG_TAG, errorMessage);
                    return;
                }

                if (MACRO_CODE_INTERPRETER.ArgumentCountMismatch)
                {
                    string errorMessage = string.Format("Command line #{0} [{1}] contains not enough or too much arguments.", i, commandString);
                    LogDispatcher.I(LOG_TAG, errorMessage);
                    return;
                }

                if (!MACRO_CODE_INTERPRETER.ArgumentTypeMatches.All(atm => atm))
                {
                    string errorMessage = string.Format("Command line #{0} [{1}] contains one or more argument with invalid type.", i, commandString);
                    LogDispatcher.I(LOG_TAG, errorMessage);
                    return;
                }

                logMessage = string.Format("Execution command line #{0} received by OSC: [{1}].", i, commandString);
                LogDispatcher.I(LOG_TAG, logMessage);
                MACRO_CODE_INTERPRETER.GetCommandWithArguments().Run();

            }

        }
        #endregion


    }

}

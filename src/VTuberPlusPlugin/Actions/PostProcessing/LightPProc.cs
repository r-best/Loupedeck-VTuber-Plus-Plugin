namespace Loupedeck.VTuberPlusPlugin
{
    using System;

    using Loupedeck.VTuberPlusPlugin.Helpers;

    // This class implements an example command that counts button presses.

    public class LightPProc : PluginDynamicCommand {

        // Initializes the command class.
        public LightPProc() : base(
                displayName: "Light",
                description: "VTP_Light",
                groupName: "PostProcessing"
        ) {}

        // This method is called when the user executes the command.
        protected override void RunCommand(String actionParameter) {
            WebSocketSingleton.Send($"VTP_Light");
        }

        // This method is called when Loupedeck needs to show the command on the console or the UI.
        protected override String GetCommandDisplayName(String actionParameter, PluginImageSize imageSize) => $"Light";
    }
}

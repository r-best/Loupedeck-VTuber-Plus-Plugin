namespace Loupedeck.VTuberPlusPlugin
{
    using System;

    using Loupedeck.VTuberPlusPlugin.Helpers;

    // This class implements an example command that counts button presses.

    public class CameraSwitch : PluginDynamicCommand {
        private static readonly Int32 NUM_CAMERAS = 9;

        // Initializes the command class.
        public CameraSwitch() : base() {
            for (var i = 1; i <= NUM_CAMERAS; i++)
                this.AddParameter(i.ToString(), $"Camera {i}", "Cameras");
        }

        // This method is called when the user executes the command.
        protected override void RunCommand(String actionParameter) {
            if (Int32.TryParse(actionParameter, out var itemIndex)) {
                WebSocketSingleton.Send($"VTP_Camera:{actionParameter}");
            }
        }

        // This method is called when Loupedeck needs to show the command on the console or the UI.
        protected override String GetCommandDisplayName(String actionParameter, PluginImageSize imageSize) => $"Camera {actionParameter}";
    }
}

namespace Loupedeck.VTuberPlusPlugin
{
    using System;

    using Loupedeck.VTuberPlusPlugin.Helpers;

    // This class implements an example command that counts button presses.

    public class FX : PluginDynamicCommand {
        private static readonly String[] EFFECTS = { "Blur", "Glitch", "Grayscale", "Pixelate", "Rainbow", "Shake", "Wave" };

        // Initializes the command class.
        public FX() : base() {
            foreach (var effect in EFFECTS)
                this.AddParameter(effect, effect, "Effects");
        }

        // This method is called when the user executes the command.
        protected override void RunCommand(String actionParameter) {
            if (Int32.TryParse(actionParameter, out var itemIndex)) {
                WebSocketSingleton.Send($"VTP_FX:{actionParameter}");
            }
        }

        // This method is called when Loupedeck needs to show the command on the console or the UI.
        protected override String GetCommandDisplayName(String actionParameter, PluginImageSize imageSize) => $"FX {actionParameter}";
    }
}

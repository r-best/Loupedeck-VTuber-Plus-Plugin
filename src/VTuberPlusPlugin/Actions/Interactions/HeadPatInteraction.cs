namespace Loupedeck.VTuberPlusPlugin
{
    using System;

    using Loupedeck.VTuberPlusPlugin.Helpers;

    // This class implements an example command that counts button presses.

    public class HeadPatInteraction : MultistateActionEditorCommand {

        private static readonly String PARAM_SPEED = "Speed";

        // Initializes the command class.
        public HeadPatInteraction() {
            this.DisplayName = "HeadPat";
            this.Description = "VTP_HeadPat";
            this.GroupName = "Interactions";

            var speedBox = new ActionEditorTextbox("Speed", "Speed");
            speedBox.SetRequired();
            speedBox.SetFormat(ActionEditorTextboxFormat.Integer);
            speedBox.SetPlaceholder("50");
            speedBox.SetRegex("^[0-9]+$");
            //speedBox.SetMaxLength(2);
            this.ActionEditor.AddControl(speedBox);
        }

        // This method is called when the user executes the command.
        protected override Boolean RunCommand(ActionEditorActionParameters actionParameters) {
            if (!actionParameters.TryGetInt32(PARAM_SPEED, out var speed)) {
                speed = 50;
            }
            WebSocketSingleton.Send($"VTP_HeadPat:{speed}");
            return true;
        }
    }
}

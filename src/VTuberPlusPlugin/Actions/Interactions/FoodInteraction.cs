namespace Loupedeck.VTuberPlusPlugin
{
    using System;

    using Loupedeck.VTuberPlusPlugin.Helpers;

    // This class implements an example command that counts button presses.

    public class FoodInteraction : MultistateActionEditorCommand {

        private static readonly String PARAM_ITEMINDEX = "ItemIndex";
        private static readonly String PARAM_CUSTOMITEMINDEX = "CustomItemIndex";
        private static readonly String PARAM_DAMAGE = "Damage";

        // Initializes the command class.
        public FoodInteraction() {
            this.DisplayName = "Food";
            this.Description = "VTP_Food";
            this.GroupName = "Interactions";

            var itemIndexBox = new ActionEditorTextbox("ItemIndex", "ItemIndex (default -1)");
            itemIndexBox.SetFormat(ActionEditorTextboxFormat.Integer);
            itemIndexBox.SetPlaceholder("-1");
            itemIndexBox.SetRegex("^-?[0-9]+");
            itemIndexBox.SetMaxLength(3);
            this.ActionEditor.AddControl(itemIndexBox);

            var customItemIndexBox = new ActionEditorTextbox("CustomItemIndex", "CustomItemIndex (default -1)");
            customItemIndexBox.SetFormat(ActionEditorTextboxFormat.Integer);
            customItemIndexBox.SetPlaceholder("-1");
            customItemIndexBox.SetRegex("^-?[0-9]+");
            customItemIndexBox.SetMaxLength(3);
            this.ActionEditor.AddControl(customItemIndexBox);

            var damageBox = new ActionEditorTextbox("Damage", "Damage (default 0)");
            damageBox.SetFormat(ActionEditorTextboxFormat.Integer);
            damageBox.SetPlaceholder("0");
            damageBox.SetRegex("^[0-9]+");
            damageBox.SetMaxLength(2);
            this.ActionEditor.AddControl(damageBox);
        }

        // This method is called when the user executes the command.
        protected override Boolean RunCommand(ActionEditorActionParameters actionParameters) {
            if (!actionParameters.TryGetInt32(PARAM_ITEMINDEX, out var itemIndex)) {
                itemIndex = -1;
            }
            if (!actionParameters.TryGetInt32(PARAM_CUSTOMITEMINDEX, out var customItemIndex)) {
                customItemIndex = -1;
            }
            if (!actionParameters.TryGetInt32(PARAM_DAMAGE, out var damage)) {
                damage = 0;
            }
            WebSocketSingleton.Send($"VTP_Food:{itemIndex}:{customItemIndex}:{damage}");
            return true;
        }
    }
}

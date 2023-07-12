namespace Loupedeck.VTuberPlusPlugin
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Loupedeck.VTuberPlusPlugin.Helpers;

    // This class contains the plugin-level logic of the Loupedeck plugin.

    public class VTuberPlusPlugin : Plugin
    {
        public override Boolean UsesApplicationApiOnly => true; // Gets a value indicating whether this is a Universal plugin or an Application plugin.
        public override Boolean HasNoApplication => true; // Gets a value indicating whether this is an API-only plugin.

        // Initializes a new instance of the plugin class.
        public VTuberPlusPlugin() {
            // Initialize the plugin log.
            PluginLog.Init(this.Log);

            // Initialize the plugin resources.
            PluginResources.Init(this.Assembly);
        }

        // This method is called when the plugin is loaded during the Loupedeck service start-up.
        public override void Load() {
            var connected = WebSocketSingleton.Connect(30000);
            if(connected) {
                this.MarkConnected();
            } else {
                this.MarkDisconnected();
            }
        }

        // This method is called when the plugin is unloaded during the Loupedeck service shutdown.
        public override void Unload() {
            WebSocketSingleton.Dispose();
        }

        private void MarkConnected() {
            this.OnPluginStatusChanged(Loupedeck.PluginStatus.Normal, "Connected to VTuber Plus");
            Task.Factory.StartNew(() => {
                while (WebSocketSingleton.IsConnected()) {
                    Thread.Sleep(5000);
                }
                this.MarkDisconnected();
            });
        }

        private void MarkDisconnected() {
            this.OnPluginStatusChanged(Loupedeck.PluginStatus.Error, "Not connected to Vtuber Plus", "https://vtuberplus.com/", "VTuber Plus Website");
            Task.Factory.StartNew(() => {
                while (!WebSocketSingleton.IsConnected()) {
                    WebSocketSingleton.Connect(5000);
                    Thread.Sleep(500);
                }
                this.MarkConnected();
            });
        }
    }
}

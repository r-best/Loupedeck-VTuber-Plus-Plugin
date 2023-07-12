namespace Loupedeck.VTuberPlusPlugin.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using System.Net.WebSockets;
    using System.Threading;

    class WebSocketSingleton {
        private static readonly Uri URL = new Uri("ws://localhost:4430/vtplus");
        private static ClientWebSocket WS;

        private static void Init() {
            if (WS == null || WS.State == WebSocketState.Closed || WS.State == WebSocketState.Aborted || WS.State == WebSocketState.None) {
                WS = new ClientWebSocket();
            }
        }

        public static Boolean Connect(Int32 timeoutMs) {
            Init();
            try {
                var task = WS.ConnectAsync(URL, new CancellationTokenSource(timeoutMs).Token);
                task.Wait();
                return task.Status == TaskStatus.RanToCompletion && IsConnected();
            } catch (Exception e) {
                return false;
            }
        }

        public static void Dispose() {
            Init();
            WS.Dispose();
        }

        public static Boolean IsConnected() {
            Init();
            return WS.State == WebSocketState.Open;
        }

        public static Boolean Send(String text) {
            Init();
            if (!IsConnected()) 
                return false;

            try {
                var task = WS.SendAsync(
                    new ArraySegment<Byte>(Encoding.ASCII.GetBytes(text)),
                    WebSocketMessageType.Text,
                    true,
                    new CancellationToken());
                task.Wait();
                return task.Status == TaskStatus.RanToCompletion;
            } catch (Exception e) {
                return false;
            }
        }
    }
}

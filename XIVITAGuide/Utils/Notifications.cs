using Dalamud.Interface.Internal.Notifications;
using XIVITAGuide.Base;

namespace XIVITAGuide.Utils
{
    /// <summary>
    ///     Handles the sending of notifications to the client.
    /// </summary>
    internal static class Notifications
    {
        /// <summary>
        ///     Sends a toast notification to the user.
        /// </summary>
        /// <param name="message"> The message to send. </param>
        /// <param name="title"> The title of the notification. </param>
        /// <param name="type"> The type of notification to show </param>
        public static void ShowToast(string message, string title = "XIVITA Guide", NotificationType type = NotificationType.None) =>
            PluginService.PluginInterface.UiBuilder.AddNotification(message, title, type);
    }
}

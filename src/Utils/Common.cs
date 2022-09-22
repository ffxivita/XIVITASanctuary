using System.Diagnostics;

namespace XIVITAGuide.Utils
{
    /// <summary>
    ///     Collezione di funzioni di varia utilità
    /// </summary>
    public class Common
    {
        public static void OpenBrowser(string url)
        {
            Process.Start(new ProcessStartInfo { FileName = url, UseShellExecute = true });
        }
    }
}
using System;
using System.Threading;
using System.Windows.Forms;

namespace ProcSpector.Server.Win
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using var mtx = new Mutex(true, nameof(ProcSpector), out var isFirst);
            if (!isFirst)
                return;

            var note = new NotificationIcon
            {
                NotifyIcon = { Visible = true }
            };
            Application.Run();
            note.NotifyIcon.Dispose();
        }
    }
}
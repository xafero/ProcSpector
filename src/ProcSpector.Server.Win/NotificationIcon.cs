using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Prog = ProcSpector.Server.Program;

namespace ProcSpector.Server.Win
{
    public sealed class NotificationIcon
    {
        internal NotifyIcon NotifyIcon;

        public NotificationIcon()
        {
            NotifyIcon = new NotifyIcon();
            var notificationMenu = new ContextMenuStrip();
            notificationMenu.Items.AddRange(InitializeMenu());

            var resources = new ComponentResourceManager(typeof(NotificationIcon));
            NotifyIcon.Icon = (Icon?)resources.GetObject("$this.Icon");
            NotifyIcon.ContextMenuStrip = notificationMenu;

            StartIt();
        }

        private ToolStripItem[] InitializeMenu()
        {
            ToolStripItem[] menu =
            [
                new ToolStripMenuItem("Exit", null, MenuExitClick)
            ];
            return menu;
        }

        private void MenuExitClick(object? sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Start()
        {
            Prog.Main([]);
        }

        private void StartIt()
        {
            var thread = new Thread(Start) { IsBackground = true };
            thread.Start();
        }
    }
}
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

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
            NotifyIcon.Icon = (Icon)resources.GetObject("$this.Icon");
            NotifyIcon.ContextMenuStrip = notificationMenu;
        }

        private ToolStripItem[] InitializeMenu()
        {
            ToolStripItem[] menu =
            [
                new ToolStripMenuItem("Exit", null, MenuExitClick)
            ];
            return menu;
        }

        private void MenuExitClick(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}